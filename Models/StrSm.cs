using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class StrSm
{
    public decimal TransID { get; set; }

    public string TransNo { get; set; } = null!;

    public string sSms { get; set; } = null!;

    public string cAct { get; set; } = null!;

    public DateTime ImpDt { get; set; }

    public string? IsDel { get; set; }

    public DateTime? DelDt { get; set; }
}
