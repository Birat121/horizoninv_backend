namespace backend.DTOs
{
    public class JVNEntryLineDto
    {
        public string Acc { get; set; } = string.Empty;
        public decimal Dr { get; set; }
        public decimal Cr { get; set; }
        public string Narration { get; set; } = string.Empty;
    }

    public class JVNEntryDto
    {
        public DateTime TransDate { get; set; }
        public string EntryBy { get; set; } = string.Empty;

        public int? DocNo {  get; set; }

        public string VoucherRef { get; set; }
        public List<JVNEntryLineDto> Entries { get; set; } = new();
    }

    public class SingleJVNEntryDto
    {
        public DateTime TransDate { get; set; }
        public string EntryBy { get; set; } = string.Empty;
        public int? DocNo { get; set; }
        public string VoucherRef { get; set; } = string.Empty;
        public string ContraAcc { get; set; } = string.Empty;
        public string Acc { get; set; } = string.Empty;
        public decimal Amount {  get; set; }

        public string Narration { get; set; } = string.Empty;
        
    }

}
