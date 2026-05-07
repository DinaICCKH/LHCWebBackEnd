using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBPContact
{
    public int ContactCode { get; set; }

    public string CardCode { get; set; } = null!;

    public string? Tel1 { get; set; }

    public string Status { get; set; } = null!;

    public string ContactName { get; set; } = null!;
}
