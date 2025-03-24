using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductMaster_Copy
{
    public decimal TransID { get; set; }

    public string CatID { get; set; } = null!;

    public string SubCatID { get; set; } = null!;

    public string ProductID { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Desc1 { get; set; }

    public string UOM { get; set; } = null!;

    public decimal UOMQty { get; set; }

    public decimal? WeightQty { get; set; }

    public decimal UnitRate { get; set; }

    public decimal SaleRate { get; set; }

    public decimal WholeSalePcs { get; set; }

    public decimal VatRate { get; set; }

    public int? Reorder { get; set; }

    public decimal? AvrCost { get; set; }

    public string EntBy { get; set; } = null!;

    public string EntPc { get; set; } = null!;

    public DateTime EntDate { get; set; }

    public DateTime ModDate { get; set; }

    public string? HSCode { get; set; }
}
