using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBPPricing
{
    /// <summary>
    /// CardCode+ItemCode+cast(PriceList as nvarchar)+cast(UomEntry as nvarchar) as &apos;BPPriceKey&apos;	
    /// </summary>
    public string BPPriceKey { get; set; } = null!;

    public string CardCode { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public int PriceList { get; set; }

    public int? UomEntry { get; set; }

    public decimal Amount { get; set; }
}
