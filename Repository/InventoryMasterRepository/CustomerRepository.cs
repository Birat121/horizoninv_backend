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
                // Generate next CustomerId
                var lastCustomer = await _context.CustomerMasts
                    .Where(c => c.CustomerId.StartsWith("CS"))
                    .OrderByDescending(c => c.CustomerId)
                    .FirstOrDefaultAsync();

                int nextNumber = 1;

                if (lastCustomer != null && !string.IsNullOrEmpty(lastCustomer.CustomerId))
                {
                    string numberPart = lastCustomer.CustomerId.Substring(2); // Get the numeric part after "CS"
                    if (int.TryParse(numberPart, out int parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                string nextCustomerId = $"CS{nextNumber.ToString("D4")}";
                customer.CustomerId = nextCustomerId;
                customer.EntryDate = DateTime.UtcNow;

                string acc1 = $"{customer.CustomerId}Rec";

                var acc = new Acc
                {
                    Acc1 = acc1,
                    Acn = customer.CustomerName,
                    Acg = "XX06",
                    Acgg = "XX06",
                    AclO = 0,
                    AclD = 1,
                    L0 = "XX06",
                    L1 = acc1,
                    AcType = "Receivable",
                    MasterAcType = "Assets",
                    CreatDate = DateTime.UtcNow,
                };

                await _context.CustomerMasts.AddAsync(customer);
                await _context.Accs.AddAsync(acc);
                await _context.SaveChangesAsync();

                return customer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating customer: {ex.Message}");
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