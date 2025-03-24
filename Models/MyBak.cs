﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MyBak
{
    public decimal TransID { get; set; }

    public DateTime BakDate { get; set; }

    public string BakBy { get; set; } = null!;

    public string BakPC { get; set; } = null!;

    public DateTime StartOfDay { get; set; }

    public string? MyStatus { get; set; }
}
