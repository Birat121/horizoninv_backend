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
            var customer = await _context.CustomerMasts.FirstOrDefaultAsync(c => c.CustomerName == customerName);

            return customer?.CustomerId;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
