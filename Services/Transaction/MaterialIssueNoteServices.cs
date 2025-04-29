using backend.DTOs;
using backend.Models;
using backend.Repository.Transaction;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services.Transaction
{
    public class MaterialIssueNoteServices
    {
        private readonly IMaterialIssueNoteRepository _repository;

        public MaterialIssueNoteServices(IMaterialIssueNoteRepository repository)
        {
            _repository = repository;
        }

        public async Task AddMaterialIssueNote(MaterialIssueDto materialIssue)
        {
            var productId = await _repository.GetProductIdByNameAsync(materialIssue.ProductName) ?? throw new Exception("Product Not found");
            var saleRate = await _repository.GetSaleRateByProductIdAsync(productId);
            var (uom, uomQty) = await _repository.GetUOMInfoAsync(productId);
            var totalCost = saleRate * materialIssue.IssQty;

            string branchFrom = materialIssue.TransactionType == "RECEIVE" ? materialIssue.BranchTo : "100";
            string branchTo = materialIssue.TransactionType == "RECEIVE" ? "100" : materialIssue.BranchTo;

            var issue = new MaterialIssue
            {
                ProductID = productId,
                SaleRate = saleRate,
                IssQty = materialIssue.IssQty,
                TotalCost = totalCost,
                UOM = uom,
                UOMQty = uomQty,
                BranchFrom = branchFrom,
                BranchTo = branchTo,
                EntryDate = materialIssue.EntryDate,
                IssDate = materialIssue.IssDate
            };
            await _repository.AddMaterialIssueAsync(issue);
        }
    }
}
