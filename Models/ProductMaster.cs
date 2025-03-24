using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductMaster
{
    public decimal TransID { get; set; }

    public string? CatID { get; set; }

    public string? SubCatID { get; set; }

    public string? ProductID { get; set; }

    public string? ProductName { get; set; }

    public string? Desc1 { get; set; }

    public string? UOM { get; set; }

    public decimal? UOMQty { get; set; }

    public decimal? WeightQty { get; set; }

    public decimal? UnitRate { get; set; }

    public decimal? SaleRate { get; set; }

    public decimal? WholeSalePcs { get; set; }

    public decimal? VatRate { get; set; }

    public decimal? Reorder { get; set; }

    public decimal? AvrCost { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? Variant { get; set; }

    public string? EngineNo { get; set; }

    public string? RegNo { get; set; }

    public string? Colour { get; set; }

    public string? MakeYear { get; set; }

    public string? HSCode { get; set; }
}
