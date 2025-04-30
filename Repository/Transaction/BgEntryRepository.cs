using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Repository.Transaction
{
    public interface IBgEntryRepository
    {
        Task CreateBgEntryAsync(BGEntry entry);
        Task<string?> GetCusIdOrVenIdAsync(string partyName);


    }
    public class BgEntryRepository :IBgEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public BgEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBgEntryAsync(BGEntry entry)
        {
            _context.BGEntries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetCusIdOrVenIdAsync(string partyName)
        {
            var customerId = await _context.CustomerMasts
                .Where(c => c.CustomerName == partyName)
                .Select(c => c.CustomerId)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(customerId))
                return customerId;

            var vendorId = await _context.VendorMasts
                .Where(v => v.VendName == partyName)
                .Select(v => v.VendId)
                .FirstOrDefaultAsync();

            return vendorId;
        }


    }
}

