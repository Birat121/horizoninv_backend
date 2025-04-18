using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Transaction
{

    public interface IPdcEntryRepository
    {
        Task AddAsync(PDCEntry entry);
        Task<string?> GetCusIdOrVenIdAsync(string partyName);
    }
    public class PdcEntryRepository : IPdcEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public PdcEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PDCEntry entry)
        {
            _context.PDCEntries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetCusIdOrVenIdAsync(string partyName)
        {
            var customer = await _context.CustomerMasts.FirstOrDefaultAsync(c => c.CustomerName == partyName);
            if (customer != null)
            {
                return customer.CustomerId;
            }

            var vendor = await _context.VendorMasts.FirstOrDefaultAsync(v => v.VendName == partyName);
            
            
            return vendor?.VendId;
          

        }
    }
}
