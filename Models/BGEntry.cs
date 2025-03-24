using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class BGEntry
{
    public decimal TransID { get; set; }

    public string TransNo { get; set; } = null!;

    public DateTime TrDt { get; set; }

    public string CusId { get; set; } = null!;

    public string BGNo { get; set; } = null!;

    public DateTime IssDt { get; set; }

    public DateTime ExpDt { get; set; }

    public string BankName { get; set; } = null!;

    public string BankBrName { get; set; } = null!;

    public decimal BGAmt { get; set; }

    public string? Remarks { get; set; }

    public DateTime EntDt { get; set; }

    public string? EntBy { get; set; }

    public string? IsDel { get; set; }
}
