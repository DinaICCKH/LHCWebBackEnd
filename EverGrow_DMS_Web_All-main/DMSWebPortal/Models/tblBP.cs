using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBP
{
    public string CardCode { get; set; } = null!;

    public string? CardName { get; set; }

    public string? CardFName { get; set; }

    public string? ContactPer { get; set; }

    public short? GroupCode { get; set; }

    public string? Phone { get; set; }

    public string? VATNo { get; set; }

    public short? DefPriceListCode { get; set; }

    public decimal? CreditLimited { get; set; }

    public short? TermCode { get; set; }

    public string? TermName { get; set; }

    public string? Status { get; set; }

    public string? GPSLateLong { get; set; }

    public decimal? Balance { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageUrlServer { get; set; }

    public string? Channel { get; set; }

    public string? Region { get; set; }

    public string? ProCode { get; set; }

    public string? DisCode { get; set; }

    public string? ComCode { get; set; }

    public string? VilName { get; set; }

    public string? AddressCode { get; set; }

    public string? FullAddKH { get; set; }

    public string? FullAddEN { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? ConfimedBy { get; set; }

    public DateTime? ConfirmedDate { get; set; }

    public string? BPSource { get; set; }

    public string? BPRKey { get; set; }

    public string? Sync { get; set; }

    public string? Territory { get; set; }

    public string? AppCode { get; set; }

    public string? VATImage { get; set; }

    public string? SubZone { get; set; }

    public string? AllowDown { get; set; }

    public string? Zone { get; set; }
}
