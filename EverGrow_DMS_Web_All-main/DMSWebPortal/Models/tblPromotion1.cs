using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblPromotion1
{
   
    public int ProEntry { get; set; }

    public int LineNum { get; set; }

    public int? ItemGroupCode { get; set; }

    public string? ItemGroupName { get; set; }

    public string? PackType { get; set; }

    public string? PromotionGroup { get; set; }

    public int? BPGroup { get; set; }

    public string? BPGroupName { get; set; }

    public string? BPChannelCode { get; set; }

    public string? BPChannelName { get; set; }

    public string? CardCode { get; set; }

    public string? CardName { get; set; }

    public string? Combine { get; set; }

    public string? PromotionType { get; set; }

    public string? FOCType { get; set; }

    public string? Condition { get; set; }

    public decimal? BuyAmt { get; set; }

    public decimal? BuyQty { get; set; }

    public string? BuyUoM { get; set; }

    public decimal? FOCQty { get; set; }

    public string? FOCUoM { get; set; }

    public decimal? DisPer { get; set; }

    public decimal? DisAmt { get; set; }

    public string? Remark { get; set; }

    public DateOnly? ValidFrom { get; set; }

    public DateOnly? ValidTo { get; set; }

    public string? LineStatus { get; set; }
}
