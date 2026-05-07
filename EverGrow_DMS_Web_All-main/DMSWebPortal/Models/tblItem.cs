using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblItem
{
    public string ItemCode { get; set; } = null!;

    public string? ItemName { get; set; }

    public short? ItemGroupCode { get; set; }

    public string? ItemGroupName { get; set; }

    public int? UgpEntry { get; set; }

    public decimal? Onhand { get; set; }

    public decimal? OnOrder { get; set; }

    public decimal? IsCommited { get; set; }

    public decimal? Available { get; set; }

    public decimal? MinLevel { get; set; }

    public decimal? MaxLevel { get; set; }

    public string? Status { get; set; }

    public string? ImageUrlServer { get; set; }

    public string? ImageUrlLocal { get; set; }

    public string? FrgnName { get; set; }

    public string? InvUoMCode { get; set; }

    public int? InvUoMEntry { get; set; }

    public string? U_ProGroup { get; set; }

    public string? HasPromotion { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? OcrCode { get; set; }

    public string? OcrCode2 { get; set; }

    public string? OcrCode3 { get; set; }

    public string? OcrCode4 { get; set; }

    public string? PrincipleCode { get; set; }

    public string? MainCat { get; set; }

    public string? SubCat { get; set; }

    public string? PackageType { get; set; }

    public string? BarCode { get; set; }

    public int? DefEntry { get; set; }
    public decimal? AltQty { get; set; }
}
public partial class TblItemBatch
{
    public int AbsEntry { get; set; }
    public string ItemCode { get; set; }
    public string BatchNum { get; set; }
    public DateTime? InDate { get; set; }
    public DateTime? ExpDate { get; set; }
    public DateTime? MnfDate { get; set; }
    public string WhsCode { get; set; }
    public decimal BatchQty { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
