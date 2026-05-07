using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblRegional
{
    public string RegionalCode { get; set; } = null!;

    public string? RegionalName { get; set; }

    public string? CC3Loc { get; set; }

    public string? Status { get; set; }
}
