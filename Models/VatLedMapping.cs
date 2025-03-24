using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class VatLedMapping
{
    public decimal TransID { get; set; }

    public string TransName { get; set; } = null!;

    public string? VatLedID { get; set; }

    public string TransType { get; set; } = null!;

    public DateTime EntDt { get; set; }

    public string? EntBy { get; set; }

    public string? IsDel { get; set; }
}
