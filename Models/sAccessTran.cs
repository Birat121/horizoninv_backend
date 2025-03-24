using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class sAccessTran
{
    public decimal TransID { get; set; }

    public string? MenuName { get; set; }

    public string? Access { get; set; }
}
