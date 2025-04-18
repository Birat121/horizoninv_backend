namespace backend.DTOs
{
    public class PDCEntryDto
    {
        public string PartyName { get; set; }
        public string AccountNo { get; set; }
        public decimal ChqAmt { get; set; }
        public DateTime ChqDate { get; set; }
        public string ChqNo { get; set; }
        public string ChqType { get; set; }
        public string BankName { get; set; }
        public string BankBrName { get; set; }
        public string DepositBankName { get; set; }
        public string BeneficaryName { get; set; }
        public string? RefNo { get; set; }
        public string? Remarks { get; set; }
        public string VType { get; set; }
        public string TransNo { get; set; }
        public DateTime TrDt { get; set; }
        public DateTime? WithdrawalDt { get; set; }
    }
}
