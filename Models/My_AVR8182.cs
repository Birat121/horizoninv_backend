using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class My_AVR8182
{
    public decimal TransID { get; set; }

    public string PID { get; set; } = null!;

    public decimal? OPA { get; set; }

    public decimal? PA { get; set; }

    public decimal? OPQ { get; set; }

    public decimal? PQ { get; set; }

    public decimal? EachAvr { get; set; }

    public string? EntryBy { get; set; }

    public DateTime EntryDate { get; set; }

    public string BrID { get; set; } = null!;
}
