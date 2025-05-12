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

                // Generate next VendId (format: v00001)
                var lastVendor = await _context.VendorMasts
                    .Where(v => v.VendId.StartsWith("V"))
                    .OrderByDescending(v => v.VendId)
                    .FirstOrDefaultAsync();

                int nextNumber = 1;

                if (lastVendor != null && !string.IsNullOrEmpty(lastVendor.VendId))
                {
                    string numberPart = lastVendor.VendId.Substring(1); // Get numeric part after "v"
                    if (int.TryParse(numberPart, out int parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                string nextVendId = $"V{nextNumber.ToString("D5")}"; // e.g., v00001
                vendor.VendId = nextVendId;
                // Ensure you are not setting the identity column (TransID) manually
                vendor.EntryDate = DateTime.UtcNow;

                // Create the associated Acc entry
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

                // Add vendor and associated account record
                await _context.VendorMasts.AddAsync(vendor); // Don't manually set TransID
                await _context.Accs.AddAsync(acc);

                // Save changes
                await _context.SaveChangesAsync();

                return vendor;
            }
            catch (Exception ex)
            {
                // Log the exception properly (use a logging library in real-world applications)
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

                // Updating fields for existing vendor
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

                // Save changes to the database
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

