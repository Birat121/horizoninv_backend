using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Nep_Eng
{
    public decimal TransID { get; set; }

    public string NepaliDate { get; set; } = null!;

    public DateTime EngDate { get; set; }

    public string? Remarks { get; set; }

    public string np { get; set; } = null!;
}
