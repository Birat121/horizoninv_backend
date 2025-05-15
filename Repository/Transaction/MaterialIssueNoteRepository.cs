using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Transaction
{
    public interface IMaterialIssueNoteRepository
    {

        Task<string> GenerateNewISPAsync();
        Task<string> GenerateNewISP2Async();
        Task<string?> GetProductIdByNameAsync(string productName);
        Task<decimal> GetSaleRateByProductIdAsync(string productId);
        Task<decimal> GetIssRateByProductIdAsync(string productId);
        Task<(string UOM, decimal UOMQty)> GetUOMInfoAsync(string productId);
        Task AddMaterialIssueAsync(MaterialIssue materialIssue);
        Task SaveChangesAsync();
        Task<List<string>> GetBranchNamesAsync();
    }

    public class MaterialIssueNoteRepository : IMaterialIssueNoteRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialIssueNoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetBranchNamesAsync()
        {
            return await _context.DepartmentMasts
                .Select(d => d.DeptName)
                .ToListAsync();
        }

        public async Task<string> GenerateNewISPAsync()
        {
            string prefix = "Iss10081-";
            int lastNumber = 8200000;

            // Filter only ISPs starting with "Iss10081-"
            var lastISP = await _context.MaterialIssues
                .Where(m => m.ISP.StartsWith(prefix))
                .OrderByDescending(m => m.ISP)
                .Select(m => m.ISP)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(lastISP))
            {
                string numPart = lastISP.Replace(prefix, "");
                if (int.TryParse(numPart, out int num))
                {
                    lastNumber = num;
                }
            }

            string newISP = $"{prefix}{lastNumber + 1}";
            return newISP;
        }

        public async Task<string> GenerateNewISP2Async()
        {
            string prefix = "Rec10081-";
            int lastNumber = 8200000;

            // Filter only ISPs starting with "Iss10081-"
            var lastISP = await _context.MaterialIssues
                .Where(m => m.ISP.StartsWith(prefix))
                .OrderByDescending(m => m.ISP)
                .Select(m => m.ISP)
                .FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(lastISP))
            {
                string numPart = lastISP.Replace(prefix, "");
                if (int.TryParse(numPart, out int num))
                {
                    lastNumber = num;
                }
            }

            string newISP = $"{prefix}{lastNumber + 1}";
            return newISP;
        }


        public async Task<string?> GetProductIdByNameAsync(string productName)
        {
            return await _context.ProductMasters
                .Where(p => p.ProductName == productName)
                .Select(p => p.ProductID)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetSaleRateByProductIdAsync(string productId)
        {
            return (decimal)await _context.ProductMasters
                .Where(p => p.ProductID == productId)
                .Select(p => p.WholeSalePcs)
                .FirstOrDefaultAsync();
        }

        public async Task<decimal> GetIssRateByProductIdAsync(string productId)
        {
            return (decimal)await _context.ProductMasters
                .Where(p => p.ProductID == productId)
                .Select(p => p.UnitRate)
                .FirstOrDefaultAsync();
        }

        public async Task<(string UOM, decimal UOMQty)> GetUOMInfoAsync(string productId)
        {
            var data = await _context.ProductUOMs
                .Where(u => u.ProductID == productId)
                .Select(u => new { u.UOM, u.UOMQty })
                .FirstOrDefaultAsync();

            return ((string UOM, decimal UOMQty))(data == null ? ("", 0) : (data.UOM, data.UOMQty));
        }

        public async Task AddMaterialIssueAsync(MaterialIssue materialIssue)
        {
            _context.MaterialIssues.Add(materialIssue);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
