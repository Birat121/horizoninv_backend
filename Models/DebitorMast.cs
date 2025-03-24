using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class DebitorMast
{
    public decimal TransID { get; set; }

    public string? DebitorID { get; set; }

    public string? DebitorName { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }
}
