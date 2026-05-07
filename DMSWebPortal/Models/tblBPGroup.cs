using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBPGroup
{
    public short GroupCode { get; set; }

    public string GroupName { get; set; } = null!;

    public string? Status { get; set; }
}
