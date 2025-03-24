using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProdCtrl
{
    public decimal TransID { get; set; }

    public string ProdID { get; set; } = null!;

    public string ConsID { get; set; } = null!;

    public decimal ProdQty { get; set; }

    public decimal ConsQty { get; set; }

    public string pUOM { get; set; } = null!;

    public string cUOM { get; set; } = null!;

    public decimal pUOMQty { get; set; }

    public decimal cUOMQty { get; set; }

    public string cStatus { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public string? EntryBy { get; set; }
}
