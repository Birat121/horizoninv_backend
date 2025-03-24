using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class Ex_ChartOfAcc
{
    public decimal TransID { get; set; }

    public string? CatName { get; set; }

    public string? gName { get; set; }

    public string? SubGName { get; set; }

    public string? SubGName1 { get; set; }

    public string? SubGName2 { get; set; }

    public string? AccountHead { get; set; }

    public DateTime? EntSoftDate { get; set; }

    public DateTime EntSysDate { get; set; }
}
