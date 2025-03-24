using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class BarImg
{
    public decimal TransID { get; set; }

    public string PartyName { get; set; } = null!;

    public string PId { get; set; } = null!;

    public string ItemDesc { get; set; } = null!;

    public decimal Rate { get; set; }

    public byte[] Ph { get; set; } = null!;

    public string? MfgDt { get; set; }

    public string? ExpDt { get; set; }
}
