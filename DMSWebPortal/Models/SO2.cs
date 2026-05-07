using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class SO2
{
    public int DocEntry { get; set; }

    public int LineNum { get; set; }

    public int? ReasonCode { get; set; }

    public string? ImageUrl { get; set; }

    public string? Remark { get; set; }
}
