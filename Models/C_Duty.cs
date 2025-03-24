using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class C_Duty
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public string? Acc { get; set; }

    public string Acn { get; set; } = null!;

    public string sAcn { get; set; } = null!;

    public string AcType { get; set; } = null!;

    public string Add_Less { get; set; } = null!;

    public string Vatable { get; set; } = null!;

    public string cStatus { get; set; } = null!;

    public int Sno { get; set; }

    public string? EntryBy { get; set; }

    public DateTime EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }
}
