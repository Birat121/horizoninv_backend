namespace backend.DTOs
{
    public class MaterialIssueDto
    {
        public required string ProductName { get; set; }
        public required string ProductID { get; set; }   // newly added
        public required string Barcode { get; set; }     // newly added
        public required string UOM { get; set; }         // newly added
        public decimal UOMQty { get; set; }              // newly added
        public decimal UnitCost { get; set; }            // newly added
        public decimal TotalAmount { get; set; }         // newly added

        public decimal IssQty { get; set; }
        public required string ISP { get; set; }
        public DateTime IssDate { get; set; }
        public required string BranchTo { get; set; }
        public DateTime EntryDate { get; set; }
        public required string TransactionType { get; set; } // "ISSUE" or "RECEIVE"
    }
}

