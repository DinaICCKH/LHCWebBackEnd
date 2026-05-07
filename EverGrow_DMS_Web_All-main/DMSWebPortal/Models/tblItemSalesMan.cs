using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblItemSalesMan
{
    public int DocEntry { get; set; }
    public int LineNum { get; set; }

    public int SlpCode { get; set; }

    public string ItemCode { get; set; } = null!;

    public string? DocStatus { get; set; }

    public string? Sync { get; set; }
}
