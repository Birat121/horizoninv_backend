using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class User_Log
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public string TransNo { get; set; } = null!;

    public string FName { get; set; } = null!;

    public string? EnteredBy { get; set; }

    public DateTime? EnteredDate { get; set; }

    public string? EditedBy { get; set; }

    public DateTime? EditedDate { get; set; }

    public string? IAction { get; set; }

    public string? MyDesc { get; set; }
}
