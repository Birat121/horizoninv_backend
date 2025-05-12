using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Transaction
{

    public interface IBgEntryServices
    {
        
        Task<bool> CreateEntryAsync(BgEntryDto bgEntry);
        Task<List<PartyDto>> GetCombinedPartyListAsync();

        Task<string> GetNextTransactionNoAsync();

    }
    public class BgEntryServices : IBgEntryServices
    {
        private readonly IBgEntryRepository _repository;
        private readonly ApplicationDbContext _context;
 
        

        public BgEntryServices(IBgEntryRepository repository, ApplicationDbContext context)
        {
            _context = context;
            _repository = repository;
          
            
        }

        public async Task<bool> CreateEntryAsync(BgEntryDto bgEntry)
        {
            var cusId = await _repository.GetCusIdOrVenIdAsync(bgEntry.PartyName);

            if (string.IsNullOrEmpty(cusId))
                throw new Exception("Party name not found in customer or vendor table.");

            var BgEntry = new BGEntry
            {
                CusId = cusId,
                BankBrName = bgEntry.BankBrName,
                BGAmt = bgEntry.BGAmt,
                BankName = bgEntry.BankName,
                BGNo = bgEntry.BGNo,
                IssDt = bgEntry.IssDt,
                ExpDt = bgEntry.ExpDt,
                TransNo = bgEntry.TransNo,
                TrDt = bgEntry.TrDt,
                EntDt = DateTime.Now,

            };

            await _repository.CreateBgEntryAsync(BgEntry);
            return true;


        }

        public async Task<List<PartyDto>> GetCombinedPartyListAsync()
        {
            var customers = await _context.CustomerMasts
                .Select(c => new PartyDto
                {
                    Id = c.CustomerId,
                    Name = c.CustomerName,
                    Type = "Customer"
                }).ToListAsync();

            var vendors = await _context.VendorMasts
                .Select(v => new PartyDto
                {
                    Id = v.VendId,
                    Name = v.VendName,
                    Type = "Vendor"
                }).ToListAsync();

            return customers.Concat(vendors).OrderBy(p => p.Name).ToList();
        }

        public async Task<string> GetNextTransactionNoAsync()
        {
            var lastEntry = await _context.BGEntries
                .OrderByDescending(e => e.TransNo)
                .FirstOrDefaultAsync();

            int nextNo = 1;

            if (lastEntry != null && lastEntry.TransNo.Length >= 8)
            {
                var parts = lastEntry.TransNo.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int lastNumeric))
                {
                    nextNo = lastNumeric + 1;
                }
            }

            string nextTransNo = $"BG10081-{nextNo:D8}";
            return nextTransNo;
        }



    }
}
