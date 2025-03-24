using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CountSet
{
    public decimal TransID { get; set; }

    public int CountNo { get; set; }

    public string PCName { get; set; } = null!;

    public string MacID { get; set; } = null!;

    public DateTime SettingDate { get; set; }

    public string? IsDel { get; set; }
}
