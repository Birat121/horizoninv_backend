namespace backend.DTOs
{
    public class BgEntryDto
    {
        public required string PartyName { get; set; }
        public  string? CusId {  get; set; }
        public decimal BGAmt { get; set; }
        public string? BGNo { get; set; }
        public required string BankBrName { get; set; }
        public required string BankName { get; set; }
        public DateTime ExpDt { get; set; }
        public DateTime IssDt { get; set; }
        public string? Remarks { get; set; }
        public DateTime TrDt { get; set; }
        public required string TransNo { get; set; }
    }
}
