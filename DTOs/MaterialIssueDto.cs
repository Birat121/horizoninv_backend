namespace backend.DTOs
{
    public class MaterialIssueDto
    {
        public required string ProductName { get; set; }
        public decimal IssQty { get; set; }
    
        
        public required string ISP {  get; set; }
        public DateTime IssDate { get; set; }
        
        public required string BranchTo { get; set; }
        public DateTime EntryDate { get; set; }
        public required string TransactionType { get; set; } // "ISSUE" or "RECEIVE"
    }
}
