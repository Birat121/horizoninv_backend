using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Transaction
{
    public interface IInvoiceGenerateRepository
    {
        Task AddAsync(InvG invG);
        Task<string> GetCustomerIdByNameAsync(string customerName);
        Task SaveChangesAsync();
    }
    public class InvoiceGenerateRepository : IInvoiceGenerateRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceGenerateRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(InvG invG)
        {
            await _context.InvGs.AddAsync(invG);

        }

        public async Task<string> GetCustomerIdByNameAsync(string customerName)
        {
            // Try to find in CustomerMast
            var customer = await _context.CustomerMasts
                .FirstOrDefaultAsync(c => c.CustomerName == customerName);

            if (customer != null)
            {
                return customer.CustomerId;
            }

            // Try to find in VendorMast
            var vendor = await _context.VendorMasts
                .FirstOrDefaultAsync(v => v.VendName == customerName);

            return vendor?.VendId;
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
