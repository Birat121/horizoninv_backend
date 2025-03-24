using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class InvG_D
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public string vRef { get; set; } = null!;

    public string Acc { get; set; } = null!;

    public decimal Amt { get; set; }

    public string EnteredBy { get; set; } = null!;

    public DateTime? EntSoftDate { get; set; }

    public DateTime EntSysDate { get; set; }
}
