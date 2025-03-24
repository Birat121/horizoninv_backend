using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class TempVarchar
{
    public decimal TransID { get; set; }

    public string? ProductID { get; set; }

    public decimal? UnitRate { get; set; }

    public decimal? PackCost { get; set; }
}
