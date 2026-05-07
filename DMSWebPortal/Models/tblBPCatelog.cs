using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBPCatelog
{
    public string CatCode { get; set; } = null!;

    public string? CardCode { get; set; }

    public string? ItemCode { get; set; }

    public string? ItemName { get; set; }

    public string? BarCode { get; set; }

    public string? Substitute { get; set; }
}
