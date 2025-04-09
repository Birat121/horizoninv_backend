using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Con1
{
    public decimal TransID { get; set; }

    public string BranchCode { get; set; } = null!;

    public string TransNo { get; set; } = null!;

    public DateTime TransDate { get; set; }

    public string ProdID { get; set; } = null!;

    public string ConsID { get; set; } = null!;

    public decimal ConsQty { get; set; }

    public string UOM { get; set; } = null!;

    public decimal PackQty { get; set; }

    public decimal? PackRate { get; set; }

    public decimal? ItemTotal { get; set; }

    public int? Lvl { get; set; }

    public DateTime EntryDate { get; set; }

    public string? EntryBy { get; set; }

    public string? LocID { get; set; }

    public string? BatchID { get; set; }
}
