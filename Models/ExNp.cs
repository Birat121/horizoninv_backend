using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ExNp
{
    public decimal TransID { get; set; }

    public string CusID { get; set; } = null!;

    public string LKey { get; set; } = null!;

    public string ENP { get; set; } = null!;

    public string ESms { get; set; } = null!;

    public string? IsDel { get; set; }

    public DateTime LUDt { get; set; }
}
