using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MonthDt
{
    public decimal TransID { get; set; }

    public DateTime? MinDt { get; set; }

    public DateTime? MaxDt { get; set; }

    public string? MName { get; set; }

    public decimal? MYear { get; set; }

    public string? Remarks { get; set; }
}
