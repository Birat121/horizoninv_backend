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

