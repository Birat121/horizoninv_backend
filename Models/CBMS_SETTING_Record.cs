﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CBMS_SETTING_Record
{
    public decimal TransID { get; set; }

    public string RealTime { get; set; } = null!;

    public DateTime SchedulerStartFrom { get; set; }

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public string Uid { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public string Uri { get; set; } = null!;
}
