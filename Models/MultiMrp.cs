using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MultiMrp
{
    public decimal TransID { get; set; }

    public string PID { get; set; } = null!;

    public string PName { get; set; } = null!;

    public decimal Rt { get; set; }

    public string EnteredBy { get; set; } = null!;

    public string EnteredSys { get; set; } = null!;

    public DateTime EnteredDate { get; set; }

    public string? IsDel { get; set; }
}
