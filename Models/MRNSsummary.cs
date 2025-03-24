﻿using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class MRNSsummary
{
    public decimal TransID { get; set; }

    public DateTime? MRNDate { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public string? MRNNo { get; set; }

    public string? BillNo { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? DiscPer { get; set; }

    public decimal? DiscAmt { get; set; }

    public decimal? SubTotalADisc { get; set; }

    public decimal? TotalVatAmt { get; set; }

    public decimal? GrandTotal { get; set; }

    public decimal? DiscPerAVat { get; set; }

    public decimal? DiscAmtAVat { get; set; }

    public decimal? NetPaid { get; set; }

    public string? PaymentType { get; set; }

    public string? VendorID { get; set; }

    public string? ContraCode { get; set; }

    public string? CreditorsAc { get; set; }

    public decimal? PrintCopy { get; set; }

    public string? EntryBy { get; set; }

    public DateTime? EntryDate { get; set; }

    public string? ModifyBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public decimal? ImPer { get; set; }

    public decimal? ImAmt { get; set; }

    public string? PONo { get; set; }

    public string BranchCode { get; set; } = null!;

    public decimal? Taxable { get; set; }

    public decimal? NonTax { get; set; }

    public string? IsDs { get; set; }

    public decimal? OnlTransID { get; set; }

    public string? PurType { get; set; }

    public string? PPDNo { get; set; }

    public decimal? TermsDays { get; set; }

    public string? LcNo { get; set; }

    public string? CurrName { get; set; }

    public decimal? ExcRate { get; set; }

    public string? Rmk { get; set; }

    public decimal? EquvalentAmt { get; set; }

    public decimal? ImportTaxable { get; set; }

    public decimal? ImportVat { get; set; }

    public string? Transporter { get; set; }

    public string? VehicleNo { get; set; }

    public string? DriverName { get; set; }

    public string? LicenseNo { get; set; }

    public string? MobNo { get; set; }

    public string? TermsOfDel { get; set; }
}
