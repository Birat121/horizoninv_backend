using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class OC
{
    public decimal TransID { get; set; }

    public string? OMyIP { get; set; }

    public string? NMyIP { get; set; }

    public string? DBN { get; set; }

    public string? DBPass { get; set; }

    public string? NOU { get; set; }

    public string? SerUID { get; set; }

    public string? SerPwd { get; set; }
}
