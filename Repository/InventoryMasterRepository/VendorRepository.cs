using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace backend.Repository.InventoryMasterRepository
{
    public interface IVendorRepository
    {
        Task<VendorMast> CreateVendorAsync(VendorMast vendor);
        Task<VendorMast> UpdateVendorAsync(VendorMast vendor);
    }

    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationDbContext _context;

        public VendorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VendorMast> CreateVendorAsync(VendorMast vendor)
        {
            try
            {
                // Ensure TransID is handled correctly if not an identity column
                var maxTransId = await _context.VendorMasts.MaxAsync(v => (decimal?)v.TransID) ?? 0;
                vendor.TransID = maxTransId + 1;
                vendor.EntryDate = DateTime.UtcNow;

                string acc1 = $"{vendor.VendId}Pay";

                var acc = new Acc
                {
                    Acc1 = acc1,
                    Acn = vendor.VendName,
                    Acg = "XX15",
                    Acgg = "XX15",
                    AclO = 0,
                    AclD = 1,
                    L0 = "XX15",
                    L1 = acc1,
                    AcType = "Liabilities",
                    MasterAcType = "Liabilities",
                    CreatDate = DateTime.UtcNow,
                };

                await _context.VendorMasts.AddAsync(vendor);
                await _context.Accs.AddAsync(acc);
                await _context.SaveChangesAsync();
                return vendor;
            }
            catch (Exception ex)
            {
                // Log the exception properly (use a logging library)
                Console.WriteLine($"Error creating vendor: {ex.Message}");
                throw;
            }
        }

        public async Task<VendorMast> UpdateVendorAsync(VendorMast vendor)
        {
            try
            {
                var existingVendor = await _context.VendorMasts.FindAsync(vendor.TransID);
                if (existingVendor == null) return null;

                // Updating fields
                existingVendor.VendName = vendor.VendName;
                existingVendor.Add1 = vendor.Add1;
                existingVendor.CityName = vendor.CityName;
                existingVendor.MobileNo = vendor.MobileNo;
                existingVendor.PhoneNo = vendor.PhoneNo;
                existingVendor.EmailID = vendor.EmailID;
                existingVendor.PanNo = vendor.PanNo;
                existingVendor.ContactName = vendor.ContactName;
                existingVendor.Remarks = vendor.Remarks;
                existingVendor.ModifyBy = vendor.ModifyBy;
                existingVendor.ModifyDate = DateTime.UtcNow;
                existingVendor.BGAmt = vendor.BGAmt;

                await _context.SaveChangesAsync();
                return existingVendor;
            }
            catch (Exception ex)
            {
                // Log the error (use a logger in real-world applications)
                Console.WriteLine($"Error updating vendor: {ex.Message}");
                throw;
            }
        }
    }
}

