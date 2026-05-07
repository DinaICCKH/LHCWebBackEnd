using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public class tblBPRequest
{
    public int DocEntry { get; set; }
    public string? BPKey { get; set; }
    public string? CardCode { get; set; }
    public int? AppCode { get; set; }
    public string? CardName { get; set; }
    public string? CardFName { get; set; }
    public string? ContactPer { get; set; }
    public short? GroupCode { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Phone3 { get; set; }
    public string? VATNo { get; set; }
    public short? DefPriceListCode { get; set; }
    public decimal? CreditLimited { get; set; }   // numeric(2,2) → decimal
    public short? TermCode { get; set; }
    public string? Status { get; set; }
    public string? GPSLateLong { get; set; }
    public string? ImagePath { get; set; }
    public string? Channel { get; set; }
    public string? SubGroup { get; set; }
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
    public string? SalesCode { get; set; }
    public string? IsVAT { get; set; }
    public string? HouseNo { get; set; }
    public string? StreetNo { get; set; }
    public string? LastError { get; set; }        // nvarchar(max) → string
    public string? Email { get; set; }
    public string? VATImage { get; set; }
    public string? JsonRemark { get; set; }
    public string? SAPSyncStatus { set; get; }
    public string? Grade { set; get; }

}