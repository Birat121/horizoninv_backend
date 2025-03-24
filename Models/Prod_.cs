using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Prod_
{
    public decimal TransID { get; set; }

    public string BranchCode { get; set; } = null!;

    public string TransNo { get; set; } = null!;

    public DateTime TransDate { get; set; }

    public string? ChallanNo { get; set; }

    public string ProdID { get; set; } = null!;

    public decimal ProdQty { get; set; }

    public string UOM { get; set; } = null!;

    public decimal PackQty { get; set; }

    public decimal? PackRate { get; set; }

    public decimal? ItemTotal { get; set; }

    public int? Lvl { get; set; }

    public string? pRemarks { get; set; }

    public string? cRemarks { get; set; }

    public DateTime EntryDate { get; set; }

    public string? EntryBy { get; set; }

    public string? LocID { get; set; }

    public string? BatchID { get; set; }
}
