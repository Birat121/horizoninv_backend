using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class StkLoc
{
    public decimal TransID { get; set; }

    public string BrNo { get; set; } = null!;

    public string LocId { get; set; } = null!;

    public string LocName { get; set; } = null!;

    public string EntBy { get; set; } = null!;

    public string EntPC { get; set; } = null!;

    public DateTime EntDate { get; set; }

    public string? IsDel { get; set; }
}
