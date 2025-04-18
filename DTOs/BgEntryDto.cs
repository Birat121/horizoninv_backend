namespace backend.DTOs
{
    public class BgEntryDto
    {
        public string PartyName { get; set; }
        public string CusId {  get; set; }
        public decimal BGAmt { get; set; }
        public string? BGNo { get; set; }
        public string BankBrName { get; set; }
        public string BankName { get; set; }
        public DateTime ExpDt { get; set; }
        public DateTime IssDt { get; set; }
        public string? Remarks { get; set; }
        public DateTime TrDt { get; set; }
        public string TransNo { get; set; }
    }
}
