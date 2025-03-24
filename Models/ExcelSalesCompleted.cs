using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ExcelSalesCompleted
{
    public decimal TransID { get; set; }

    public string UploadID { get; set; } = null!;

    public DateTime SalesDate { get; set; }

    public string? EntBy { get; set; }

    public string? EntPC { get; set; }

    public DateTime EntDate { get; set; }

    public string? IsDel { get; set; }
}
