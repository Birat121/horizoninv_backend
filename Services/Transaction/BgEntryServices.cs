using backend.Data;
using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Transaction
{

    public interface IBgEntryServices
    {
        
        Task<bool> CreateEntryAsync(BgEntryDto bgEntry);
        
    }
    public class BgEntryServices : IBgEntryServices
    {
        private readonly IBgEntryRepository _repository;
 
        

        public BgEntryServices(IBgEntryRepository repository)
        {
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


    }
}
