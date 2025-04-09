using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Repository.InventoryMasterRepository
{
    public interface ICustomerRepository
    {
        Task<CustomerMast> CreateCustomerAsync(CustomerMast customer);
        Task<CustomerMast> UpdateCustomerAsync(CustomerMast customer);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerMast> CreateCustomerAsync(CustomerMast customer)
        {
            try
            {
                var maxTransId = await _context.CustomerMasts.MaxAsync(v => (decimal?)v.TransID) ?? 0;
                customer.TransID = maxTransId + 1;
                customer.EntryDate = DateTime.UtcNow;

                await _context.CustomerMasts.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating vendor: {ex.Message}");
                throw;
            }

        }
        public async Task<CustomerMast> UpdateCustomerAsync(CustomerMast customer)
        {
            try
            {
                var existingCustomer = await _context.CustomerMasts.FindAsync(customer.TransID);
                if (existingCustomer == null) return null;

                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.Add1 = customer.Add1;
                existingCustomer.CrLimit = customer.CrLimit;
                existingCustomer.TermsDays = customer.TermsDays;
                existingCustomer.PhoneNo = customer.PhoneNo;
                existingCustomer.PanNo = customer.PanNo;


                await _context.SaveChangesAsync();
                return existingCustomer;

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error updating customer:{ex.Message}");
                throw;
            }
        }
    }
}