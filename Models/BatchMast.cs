using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class BatchMast
{
    public decimal TransID { get; set; }

    public string BranchID { get; set; } = null!;

    public string BatchID { get; set; } = null!;

    public string PID { get; set; } = null!;

    public string BatchNo { get; set; } = null!;

    public DateTime MfgDt { get; set; }

    public DateTime ExpDt { get; set; }

    public string? EnteredBy { get; set; }

    public string? EnteredSys { get; set; }

    public DateTime EnteredDate { get; set; }

    public string? EditedBy { get; set; }

    public string? EditedSys { get; set; }

    public DateTime? EditedDate { get; set; }

    public decimal MRP { get; set; }

    public decimal TP { get; set; }
}
