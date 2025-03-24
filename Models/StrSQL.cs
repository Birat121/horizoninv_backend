using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class StrSQL
{
    public decimal TransID { get; set; }

    public string TransNo { get; set; } = null!;

    public string SQlCmd { get; set; } = null!;

    public DateTime ImpDt { get; set; }

    public string? IsExc { get; set; }

    public DateTime? ExcDt { get; set; }

    public string? IsDel { get; set; }
}
