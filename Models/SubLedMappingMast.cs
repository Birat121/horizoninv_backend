using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class SubLedMappingMast
{
    public decimal TransID { get; set; }

    public string SubLedId { get; set; } = null!;

    public string LedID { get; set; } = null!;

    public DateTime EntDt { get; set; }

    public string? EntBy { get; set; }

    public string? EntPc { get; set; }

    public string? IsDel { get; set; }
}
