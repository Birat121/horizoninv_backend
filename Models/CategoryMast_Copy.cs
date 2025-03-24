using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CategoryMast_Copy
{
    public decimal TransID { get; set; }

    public string CatId { get; set; } = null!;

    public string CatName { get; set; } = null!;

    public decimal OVatR { get; set; }

    public decimal NVatR { get; set; }

    public string EntBy { get; set; } = null!;

    public string EntPc { get; set; } = null!;

    public DateTime EntDate { get; set; }
}
