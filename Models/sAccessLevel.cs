﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class sAccessLevel
{
    public decimal TransID { get; set; }

    public int? LevelNo { get; set; }

    public string? Parent { get; set; }

    public string? ParentMenu { get; set; }

    public string? Child { get; set; }

    public string? ChildMenu { get; set; }
}
