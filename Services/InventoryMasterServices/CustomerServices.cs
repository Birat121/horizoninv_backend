using backend.Models;
using backend.Repository.InventoryMasterRepository;

namespace backend.Services.InventoryMasterServices
{
    public interface ICustomerServices
    {
        Task<CustomerMast> CreateCustomerAsync(CustomerMast customer);
        Task<CustomerMast> UpdateCustomerAsync(CustomerMast customer);
    }

    public class CustomerServices : ICustomerServices
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
            
        public async Task<CustomerMast> CreateCustomer(CustomerMast customer)
        {
            return await _customerRepository.CreateCustomerAsync(customer);
        }

        public async Task<CustomerMast> UpdateCustomer(CustomerMast customer)
        {
            return await _customerRepository.UpdateCustomerAsync(customer);
        }
    }

}