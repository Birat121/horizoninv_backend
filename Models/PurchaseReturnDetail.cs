using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class PurchaseReturnDetail
{
    public decimal TransID { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string? ReturnNo { get; set; }

    public string? BillNo { get; set; }

    public string? ProductID { get; set; }

    public decimal? ReturnQty { get; set; }

    public string? UOM { get; set; }

    public decimal? PackQty { get; set; }

    public decimal? PackCost { get; set; }

    public decimal? UnitRate { get; set; }

    public decimal? UnitSale { get; set; }

    public decimal? ItemTotal { get; set; }

    public decimal? ItemVatAmt { get; set; }

    public decimal? ItemGTotal { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? LocID { get; set; }

    public string? BatchID { get; set; }

    public decimal? MRP { get; set; }

    public string? BranchCode { get; set; }
}
