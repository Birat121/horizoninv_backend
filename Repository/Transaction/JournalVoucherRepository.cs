using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Transaction
{

    public interface IJournalVoucherRepository
    {
        Task AddJournalEntriesAsync(List<JVN> entries);
        Task<int> GenerateDocNoAsync(string voucherType);
    }
    public class JournalVoucherRepository : IJournalVoucherRepository
    {
        private readonly ApplicationDbContext _context;

        public JournalVoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddJournalEntriesAsync(List<JVN> entries)
        {
            await _context.JVNs.AddRangeAsync(entries);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GenerateDocNoAsync(string voucherType)
        {
            var latestDoc = await _context.JVNs.Where(j => j.VoucherRef == voucherType).
                OrderByDescending(j => j.TransID).Select(j => j.DocNo).FirstOrDefaultAsync();

            return int.TryParse(latestDoc, out var doc)? doc+1:1;
        }
    }
}
