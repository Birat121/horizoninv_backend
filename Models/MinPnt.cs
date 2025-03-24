using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MinPnt
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public decimal Pnt { get; set; }

    public DateTime EntDate { get; set; }

    public string EntBy { get; set; } = null!;

    public string? EntPC { get; set; }
}
