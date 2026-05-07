using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBPChannel
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Status { get; set; }
}
