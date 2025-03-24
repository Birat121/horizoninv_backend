using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class UCnt
{
    public decimal TransID { get; set; }

    public string PcName { get; set; } = null!;

    public string MacId { get; set; } = null!;

    public DateTime LUDt { get; set; }
}
