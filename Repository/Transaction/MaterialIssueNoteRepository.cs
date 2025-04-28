using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Transaction
{
    public interface IMaterialIssueNoteRepository
    {
        Task<string?> GetProductIdByNameAsync(string productName);
        Task<decimal> GetSaleRateByProductIdAsync(string productId);
        Task<(string UOM, decimal UOMQty)> GetUOMInfoAsync(string productId);
        Task AddMaterialIssueAsync(MaterialIssue materialIssue);
    }
    public class MaterialIssueNoteRepository : IMaterialIssueNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialIssueNoteRepository(ApplicationDbContext context) {
            _context = context; }

        public async Task<string?> GetProductIdByNameAsync(string productName)
        {
            return await _context.ProductMasters.Where(p => p.ProductID == productName)
                .Select(p => p.ProductID)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetSaleRateByProductIdAsync(string productId)
        {
            return (decimal)await _context.ProductMasters
                .Where(p => p.ProductID == productId)
                .Select (p => p.SaleRate)
                .FirstOrDefaultAsync();
        }

        public async Task<(string UOM, decimal UOMQty)> GetUOMInfoAsync(string productId)
        {
            var data = await _context.ProductUOMs
                .Where(u => u.ProductID == productId)
                .Select(u => new { u.UOM, u.UOMQty })
                .FirstOrDefaultAsync();

            return ((string UOM, decimal UOMQty))(data == null ? ("", 0) : (data.UOM,data.UOMQty));
        }

        public async Task AddMaterialIssueAsync(MaterialIssue materialIssue)
        {
            _context.MaterialIssues.Add(materialIssue);
            await _context.SaveChangesAsync();
        }
    }
}
