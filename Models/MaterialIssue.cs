using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MaterialIssue
{
    public decimal TransID { get; set; }

    public string? ISP { get; set; }

    public DateTime? IssDate { get; set; }

    public string? BranchFrom { get; set; }

    public string? BranchTo { get; set; }

    public string? ProductID { get; set; }

    public decimal? IssQty { get; set; }

    public decimal? IssRate { get; set; }

    public decimal? SaleRate { get; set; }

    public decimal? TotalCost { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? UOM { get; set; }

    public decimal? UOMQty { get; set; }

    public string? LocID { get; set; }

    public string? BatchID { get; set; }
}
