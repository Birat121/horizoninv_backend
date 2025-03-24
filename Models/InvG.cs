using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class InvG
{
    public decimal TransID { get; set; }

    public DateTime TransDate { get; set; }

    public string vRef { get; set; } = null!;

    public string? DocNo { get; set; }

    public string CusId { get; set; } = null!;

    public string ConId { get; set; } = null!;

    public string? PanNo { get; set; }

    public decimal SubTotal { get; set; }

    public decimal? DiscPer { get; set; }

    public decimal? DiscAmt { get; set; }

    public decimal NetAmt { get; set; }

    public decimal? VatAmt { get; set; }

    public decimal gTotal { get; set; }

    public string AmtWord { get; set; } = null!;

    public string EnteredBy { get; set; } = null!;

    public DateTime? EntSoftDate { get; set; }

    public DateTime EntSysDate { get; set; }
}
