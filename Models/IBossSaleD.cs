using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class IBossSaleD
{
    public decimal TransID { get; set; }

    public string record_id { get; set; } = null!;

    public string? category { get; set; }

    public string? sub_category { get; set; }

    public string item_name { get; set; } = null!;

    public string unit_price { get; set; } = null!;

    public string unit_tax { get; set; } = null!;

    public string quantity { get; set; } = null!;

    public string amount { get; set; } = null!;

    public string? EntBy { get; set; }

    public string? EntPC { get; set; }

    public DateTime EntDate { get; set; }

    public string? IsDel { get; set; }
}
