using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CreditorMast
{
    public decimal TransID { get; set; }

    public string? CreditorID { get; set; }

    public string? CreditorName { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }
}
