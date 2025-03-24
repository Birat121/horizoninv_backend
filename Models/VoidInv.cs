using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class VoidInv
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public string TransNo { get; set; } = null!;

    public string InvNo { get; set; } = null!;

    public string? BillNo { get; set; }

    public DateTime EntDate { get; set; }

    public string EntBy { get; set; } = null!;

    public string? EntPC { get; set; }

    public string? MacID { get; set; }

    public int Counter { get; set; }

    public string BranchCode { get; set; } = null!;

    public string Reason { get; set; } = null!;
}
