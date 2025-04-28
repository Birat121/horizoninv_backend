namespace backend.DTOs
{
    public class CreateInvGDto
    {
        public required string CustomerName { get; set; }
        public required string ContraName { get; set; }
        public string? vRef { get; set; }
        public string? DocNo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscPer { get; set; }
        public decimal DiscAmt { get; set; }
        public decimal VatAmt { get; set; }
        public decimal NetAmt { get; set; }
        public decimal gTotal { get; set; }
        public DateTime TransDate { get; set; }
        public string? EnteredBy { get; set; }
        public string? PanNo { get; set; }
    }
}

