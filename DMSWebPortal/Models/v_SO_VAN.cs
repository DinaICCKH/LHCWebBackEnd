using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_SO_VAN
{
    public int DocEntry { get; set; }

    public string? DocNo { get; set; }

    public string? CardCode { get; set; }

    public string? CardName { get; set; }

    public int? ContactPer { get; set; }

    public string? DelAddress { get; set; }

    public DateOnly? DocDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public DateOnly? TaxDate { get; set; }

    public string? DocCur { get; set; }

    public decimal? DocRate { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? DisPer { get; set; }

    public decimal? DisAmount { get; set; }

    public decimal? AfterDis { get; set; }

    public decimal? VATAmount { get; set; }

    public decimal? Total { get; set; }

    public string? SalesCode { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? AppId { get; set; }

    public string? AppDocNo { get; set; }

    public string? APIStatus { get; set; }

    public string? APIErrMessage { get; set; }

    public string? VATStatus { get; set; }

    public string? DocStatus { get; set; }

    public int? SAPDocEntry { get; set; }

    public int? SAPDocNum { get; set; }

    public DateOnly? UpdateDate { get; set; }

    public DateTime? CheckInDate { get; set; }

    public string? CheckInLateLong { get; set; }

    public string? CheckInRemark { get; set; }

    public string? ImageURL { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public string? CheckOutLateLong { get; set; }

    public string? CheckOutRemark { get; set; }

    public string? SAPSyncStatus { get; set; }

    public string? SAPLastError { get; set; }

    public string? VATType { get; set; }

    public string? FromLoc { get; set; }

    public string? LicTradNum { get; set; }

    public string SalesName { get; set; } = null!;
    public string? NextApprover { get; set; } = null!;
    public string? CreatedBy { get; set; } = null!;
}
