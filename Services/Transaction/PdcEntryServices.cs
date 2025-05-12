using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Transaction
{

    public interface IPdcEntryServices
    {
        Task<bool> CreatePDCEntryAsync(PDCEntryDto dto);
        Task<List<PartyDto>> GetCombinedPartyListAsync();

        Task<string> GetNextTransactionNoAsync();
    }
    public class PdcEntryServices : IPdcEntryServices
    {
        private readonly IPdcEntryRepository _repository;
        private readonly ApplicationDbContext _context;
        public PdcEntryServices(IPdcEntryRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;


        }

        public async Task<bool> CreatePDCEntryAsync(PDCEntryDto dto)
        {
            var cusId = await _repository.GetCusIdOrVenIdAsync(dto.PartyName);

            if (string.IsNullOrEmpty(cusId))
                throw new Exception("Party name not found in customer or vendor table.");

            var pdcEntry = new PDCEntry
            {
                AccountNo = dto.AccountNo,
                ChqAmt = dto.ChqAmt,
                ChqDate = dto.ChqDate,
                ChqNo = dto.ChqNo,
                ChqType = dto.ChqType,
                BankName = dto.BankName,
                BankBrName = dto.BankBrName,
                DepositBankName = dto.DepositBankName,
                BeneficaryName = dto.BeneficaryName,
                RefNo = dto.RefNo,
                Remarks = dto.Remarks,
                VType = dto.VType,
                TransNo = dto.TransNo,
                TrDt = dto.TrDt,
                WithdrawalDt = dto.WithdrawalDt,
                CusId = cusId,
                EntDt = DateTime.Now
            };

            await _repository.AddAsync(pdcEntry);
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

            string nextTransNo = $"PDC10081-{nextNo:D8}";
            return nextTransNo;
        }




    }
}
