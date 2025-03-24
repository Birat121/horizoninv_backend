using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class PrinterName
{
    public decimal TransID { get; set; }

    public string? POSPrinter { get; set; }

    public string? KOTPrinter { get; set; }

    public string? BOTPrinter { get; set; }

    public DateTime? SettingDate { get; set; }

    public string? EntryBy { get; set; }
}
