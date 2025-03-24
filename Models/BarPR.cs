using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class BarPR
{
    public decimal TransID { get; set; }

    public string TransNo { get; set; } = null!;

    public string PID { get; set; } = null!;

    public string PName { get; set; } = null!;

    public decimal Rate { get; set; }

    public int Qty { get; set; }

    public DateTime PDate { get; set; }

    public string? EnteredBy { get; set; }

    public string? EnteredSys { get; set; }
}
