using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;

namespace backend.Services.Transaction
{
    public interface IJournalVoucherService
    {
        Task CreateJournalEntryAsync(JVNEntryDto dto);
        Task CreateSingleSideVoucherAsync(SingleJVNEntryDto singleJVNEntry, string voucherType);
    }

    public class JournalVoucherService : IJournalVoucherService
    {
        private readonly IJournalVoucherRepository _journalVoucherRepository;

        public JournalVoucherService(IJournalVoucherRepository journalVoucherRepository)
        {
            _journalVoucherRepository = journalVoucherRepository;
        }

        public async Task CreateJournalEntryAsync(JVNEntryDto entryDto)
        {
            if (entryDto?.Entries == null || entryDto.Entries.Count < 2)
                throw new ArgumentException("At least two entries (one debit and one credit) are required.");

            decimal totalDr = entryDto.Entries.Sum(e => e.Dr);
            decimal totalCr = entryDto.Entries.Sum(e => e.Cr);

            if (totalDr != totalCr)
                throw new ArgumentException("Total debit and credit amounts must be equal.");

            

            var jvns = entryDto.Entries.Select(e => new JVN
            {
                FName="JournalVoucher",
                TransDate = entryDto.TransDate,
                Acc = e.Acc,
                Dr = e.Dr,
                Cr = e.Cr,
                Narration = e.Narration,
                EntryBy = entryDto.EntryBy,
                EnteredDate = DateTime.Now,
                DocNo = entryDto.DocNo?.ToString(), // DocNo as string (adjust if int in DB)
                VoucherRef = "JV"
            }).ToList();

            await _journalVoucherRepository.AddJournalEntriesAsync(jvns);
        }

        public async Task CreateSingleSideVoucherAsync(SingleJVNEntryDto dto, string voucherType)
        {
            if (dto == null || dto.Amount <=0)
                throw new ArgumentException("Valid amount is required.");

            if (string.IsNullOrWhiteSpace(dto.ContraAcc) || string.IsNullOrWhiteSpace(dto.Acc))
                throw new ArgumentException("Main or Contra Account is required");

            int docNo = dto.DocNo ?? await _journalVoucherRepository.GenerateDocNoAsync(voucherType);

            bool isReceipt = voucherType == "RV";
            decimal drAmount = isReceipt ? dto.Amount : 0;
            decimal crAmount = isReceipt ? 0 : dto.Amount;

            
            var entry = new JVN
            {
                TransDate = dto.TransDate,
                Acc= dto.Acc,
                Dr = drAmount,
                Cr = crAmount,
                Narration= dto.Narration,
                EntryBy=dto.EntryBy,
                EnteredDate = DateTime.Now,
                DocNo= docNo.ToString(),
                VoucherRef = voucherType

            };

            var contra = new JVN
            {
                TransDate = dto.TransDate,
                Acc = dto.ContraAcc,
                Dr = drAmount,
                Cr = crAmount,
                Narration = dto.Narration,
                EntryBy = dto.EntryBy,
                EnteredDate = DateTime.Now,

                DocNo = docNo.ToString(),
                VoucherRef = voucherType
            };

            await _journalVoucherRepository.AddJournalEntriesAsync(new List<JVN> { entry,contra});
        }
    }
}

