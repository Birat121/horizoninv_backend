using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Ex_OpbBal
{
    public decimal TransID { get; set; }

    public string? Acn { get; set; }

    public decimal? Dr { get; set; }

    public decimal? Cr { get; set; }

    public string? FilePath { get; set; }

    public DateTime? EntSoftDate { get; set; }

    public DateTime EntSysDate { get; set; }
}
