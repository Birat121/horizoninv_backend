﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class rptTempTable
{
    public decimal TransID { get; set; }

    public DateTime? TransDate { get; set; }

    public string? L0 { get; set; }

    public string? L1 { get; set; }

    public string? L2 { get; set; }

    public string? L3 { get; set; }

    public string? L4 { get; set; }

    public decimal? Dr { get; set; }

    public decimal? Cr { get; set; }

    public string? GName { get; set; }

    public string? VoucherRef { get; set; }

    public string? Narration { get; set; }
}
