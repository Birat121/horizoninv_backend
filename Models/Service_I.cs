using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Service_I
{
    public decimal TransID { get; set; }

    public  string? Itemcode { get; set; }

    public string Description { get; set; } = null!;

    public decimal Rt { get; set; }

    public string? EnteredBy { get; set; }

    public string? EnteredSys { get; set; }

    public DateTime EnteredDate { get; set; }

    public string? HSCode { get; set; }
}
