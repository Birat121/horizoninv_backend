using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class DmgG
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public string TransNo { get; set; } = null!;

    public string PID { get; set; } = null!;

    public decimal AvlQ { get; set; }

    public decimal? Qty { get; set; }

    public decimal Rate { get; set; }

    public decimal tAmt { get; set; }

    public DateTime EntDate { get; set; }

    public string EntBy { get; set; } = null!;

    public string? EntPC { get; set; }

    public string? IsDel { get; set; }

    public string? BranchID { get; set; }

    public string? LocID { get; set; }

    public string? BatchID { get; set; }
}
