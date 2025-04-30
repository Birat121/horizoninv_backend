using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.InventoryMasterRepository
{
    public interface IInventoryItemMasterRepository
    {
        Task AddProductAsync(ProductMaster product, ProductUOM productUOM);
        Task<decimal> GetVateRateByCategoryIdAsync(string catId);
        Task<string>GetCatIdByCatNameAsync(string catName);
        Task<string> GetSubCatIdBySubCatNameAsync(string subcatName);
        Task<UomMast> GetUOMByNameAsync(string uomName);



    }
    public class InventoryItemMasterRepository :IInventoryItemMasterRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemMasterRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<decimal> GetVateRateByCategoryIdAsync(string catId)
        {
            var category = await _context.CategoryMasts.FirstOrDefaultAsync(c => c.CatId == catId);
            return category?.VatRate ?? 0;

        }

        public async Task<string> GetCatIdByCatNameAsync(string catName)
        {
            var category = await _context.CategoryMasts.Where(c => c.CatName.ToLower() == catName.ToLower()).FirstOrDefaultAsync();

            return category.CatId;

        }

        public async Task<string> GetSubCatIdBySubCatNameAsync(string subcatName)
        {
            var subcategory = await _context.SubCategoryMasts.Where(sc => sc.SubCatName.ToLower() == subcatName.ToLower()).FirstOrDefaultAsync();
            return subcategory?.SubCatID;

        }

        public async Task<UomMast> GetUOMByNameAsync(string uomName)
        {
            return await _context.UomMasts.FirstOrDefaultAsync(u => u.UomDesc == uomName);
        }


        public async Task AddProductAsync(ProductMaster productMaster, ProductUOM productUOM)
        {
            await _context.ProductMasters.AddAsync(productMaster);
            await _context.ProductUOMs.AddAsync(productUOM);
            await _context.SaveChangesAsync();
        }

    }
}
