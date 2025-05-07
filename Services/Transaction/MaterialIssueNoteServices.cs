using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;

namespace backend.Services.Transaction
{
    public interface IMaterialIssueNoteServices
    {
        Task AddMaterialIssueNote(MaterialIssueDto materialIssue);
    }

    public class MaterialIssueNoteServices : IMaterialIssueNoteServices
    {
        private readonly IMaterialIssueNoteRepository _repository;

        public MaterialIssueNoteServices(IMaterialIssueNoteRepository repository)
        {
            _repository = repository;
        }

        public async Task AddMaterialIssueNote(MaterialIssueDto materialIssue)
        {
            // Get Product ID
            var productId = await _repository.GetProductIdByNameAsync(materialIssue.ProductName);
            if (productId == null)
                throw new Exception("Product not found");

            // Get Sale Rate and UOM info
            var saleRate = await _repository.GetSaleRateByProductIdAsync(productId);
            var issRate = await _repository.GetIssRateByProductIdAsync(productId);
            var (uom, uomQty) = await _repository.GetUOMInfoAsync(productId);

            // Calculate Total Cost
            var totalCost = issRate * materialIssue.IssQty;

            // Determine branch from/to based on transaction type
            string branchFrom = materialIssue.TransactionType == "RECEIVE" ? materialIssue.BranchTo : "100";
            string branchTo = materialIssue.TransactionType == "RECEIVE" ? "100" : materialIssue.BranchTo;

            // Create entity
            var issue = new MaterialIssue
            {
                ProductID = productId,
                SaleRate = saleRate,
                IssRate = issRate,
                IssQty = materialIssue.IssQty,
                TotalCost = totalCost,
                UOM = uom,
                UOMQty = uomQty,
                BranchFrom = branchFrom,
                BranchTo = branchTo,
                EntryDate = materialIssue.EntryDate,
                IssDate = materialIssue.IssDate,
                ISP= materialIssue.ISP
                
            };

            await _repository.AddMaterialIssueAsync(issue);
        }
    }
}
