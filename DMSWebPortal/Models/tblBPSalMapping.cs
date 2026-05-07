using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBPSalMapping
{
    public string Code { get; set; } = null!;

    public string? CardCode { get; set; }

    public int? SlpCode { get; set; }
}
