using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class IBossSaleSum
{
    public decimal TransID { get; set; }

    public string record_id { get; set; } = null!;

    public string customer_name { get; set; } = null!;

    public string? customer_email { get; set; }

    public string? customer_mob { get; set; }

    public string? customer_phone { get; set; }

    public string? customer_address { get; set; }

    public string? customer_pan { get; set; }

    public string sales_date { get; set; } = null!;

    public string sales_amount { get; set; } = null!;

    public string payment_mode { get; set; } = null!;

    public string? InvNo { get; set; }

    public string? EntBy { get; set; }

    public string? EntPC { get; set; }

    public DateTime EntDate { get; set; }

    public string? IsDel { get; set; }
}
