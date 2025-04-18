using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;

namespace backend.Services.Transaction
{

    public interface IPdcEntryServices
    {
        Task<bool> CreatePDCEntryAsync(PDCEntryDto dto);
    }
    public class PdcEntryServices : IPdcEntryServices
    {
        private readonly IPdcEntryRepository _repository;
        public PdcEntryServices(IPdcEntryRepository repository)
        {
            _repository = repository;

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

    }
}
