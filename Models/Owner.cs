﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Owner
{
    public decimal TransID { get; set; }

    public int LevelNo { get; set; }

    public DateTime Created { get; set; }
}
