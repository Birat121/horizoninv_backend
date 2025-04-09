using backend.Models;
using backend.Repository.InventoryMasterRepository;
using System.Threading.Tasks;

namespace backend.Services.InventoryMasterServices
{
    public interface IVendorService
    {
        Task<VendorMast> CreateVendorAsync(VendorMast vendor);
        Task<VendorMast> UpdateVendorAsync(VendorMast vendor);
    }

    public class VendorService : IVendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<VendorMast> CreateVendorAsync(VendorMast vendor)
        {
            return await _vendorRepository.CreateVendorAsync(vendor);
        }

        public async Task<VendorMast> UpdateVendorAsync(VendorMast vendor)
        {
            return await _vendorRepository.UpdateVendorAsync(vendor);
        }
    }
}
