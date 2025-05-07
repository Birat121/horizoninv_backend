using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class DepartmentMast
{
    public decimal TransID { get; set; }

    public string DeptId { get; set; }

    public string DeptName { get; set; } = null!;

    public string EnteredBy { get; set; } 

    public string EnteredSys { get; set; }

    public DateTime EnteredDate { get; set; }
}
