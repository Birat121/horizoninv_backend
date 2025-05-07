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
            // Try finding customer by name (case-insensitive match is optional)
            var customer = await _context.CustomerMasts
                .FirstOrDefaultAsync(c => c.CustomerName.ToLower() == partyName.ToLower());

            if (customer != null && !string.IsNullOrEmpty(customer.CustomerId))
            {
                return customer.CustomerId;
            }

            // Try finding vendor by name
            var vendor = await _context.VendorMasts
                .FirstOrDefaultAsync(v => v.VendName.ToLower() == partyName.ToLower());

            if (vendor != null && !string.IsNullOrEmpty(vendor.VendId))
            {
                return vendor.VendId;
            }

            // Not found in both
            return null;
        }



    }
}

