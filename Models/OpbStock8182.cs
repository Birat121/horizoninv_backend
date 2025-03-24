using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class OpbStock8182
{
    public decimal TransID { get; set; }

    public string? ProductID { get; set; }

    public decimal Qty { get; set; }

    public decimal? StockAmt { get; set; }

    public string? Remarks { get; set; }

    public DateTime? ClosingDate { get; set; }

    public string BranchID { get; set; } = null!;

    public string? LocID { get; set; }

    public string? BatchID { get; set; }
}
