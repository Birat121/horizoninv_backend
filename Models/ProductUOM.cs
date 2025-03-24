using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductUOM
{
    public decimal TransID { get; set; }

    public string? ProductID { get; set; }

    public string? Barcode { get; set; }

    public string? UOM { get; set; }

    public decimal? UOMQty { get; set; }

    public decimal? UnitRate { get; set; }

    public decimal? UnitSale { get; set; }

    public decimal? DiscAmt { get; set; }

    public decimal? NetSale { get; set; }

    public decimal? WS { get; set; }

    public decimal? AvrCost { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? IsDel { get; set; }
}
