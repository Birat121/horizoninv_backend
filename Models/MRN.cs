using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MRN
{
    public decimal TransID { get; set; }

    public DateTime? MRNDate { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string? MRNNo { get; set; }

    public string? BillNo { get; set; }

    public string? ProductID { get; set; }

    public decimal? AccptdQty { get; set; }

    public string? UOM { get; set; }

    public decimal? PackQty { get; set; }

    public decimal? PackCost { get; set; }

    public decimal? UnitRate { get; set; }

    public decimal? UnitSale { get; set; }

    public string? BType { get; set; }

    public decimal? BQty { get; set; }

    public decimal? ItemTotal { get; set; }

    public decimal? ItemVatAmt { get; set; }

    public decimal? ItemGTotal { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public decimal? ImAmt { get; set; }

    public string? PONo { get; set; }

    public string BranchCode { get; set; } = null!;

    public string? IsDs { get; set; }

    public decimal? OnlTransID { get; set; }

    public string? LocID { get; set; }

    public string? BatchID { get; set; }

    public decimal? MRP { get; set; }

    public decimal IDP { get; set; }

    public decimal IDA { get; set; }

    public string? LocName { get; set; }

    public string? BatchName { get; set; }

    public decimal PurAddCost { get; set; }

    public decimal TotalFYC { get; set; }
}
