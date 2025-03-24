using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class HSalesI_D
{
    public decimal TransID { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string InvoiceNo { get; set; } = null!;

    public string ProductID { get; set; } = null!;

    public decimal SaleQ { get; set; }

    public string UOM { get; set; } = null!;

    public decimal PackQty { get; set; }

    public decimal UnitRate { get; set; }

    public decimal ItemTotal { get; set; }

    public decimal ItemVat { get; set; }

    public decimal ItemGTotal { get; set; }

    public decimal? WeAvrC { get; set; }

    public decimal? WeAvrP { get; set; }

    public string EntryBy { get; set; } = null!;

    public DateTime EntDate { get; set; }

    public string? Is_Deleted { get; set; }
}
