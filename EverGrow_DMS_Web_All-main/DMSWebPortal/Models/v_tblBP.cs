using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_tblBP
{
    public string AppCode { get; set; } = null!;

    public string? CardCode { get; set; }

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

    public string BPSource { get; set; } = null!;

    public string SalesCode { get; set; } = null!;

    public string? DeviceID { get; set; }

    public string CreatorName { get; set; } = null!;

    public string UpdatorName { get; set; } = null!;

    public string? ChannelName { get; set; }

    public string? GroupName { get; set; }

    public string? U_Province { get; set; }

    public string? U_ProvinceKh { get; set; }

    public string? U_Khan { get; set; }

    public string? U_KhanKh { get; set; }

    public string? U_Sangkat { get; set; }

    public string? U_SangkatKh { get; set; }

    public string ConfirmerName { get; set; } = null!;
}
