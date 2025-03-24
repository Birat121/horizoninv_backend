using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class PurAddLedMap
{
    public decimal TransID { get; set; }

    public string LedID { get; set; } = null!;

    public DateTime EntDt { get; set; }

    public string? EntBy { get; set; }

    public string? EntPc { get; set; }

    public string? IsDel { get; set; }
}
