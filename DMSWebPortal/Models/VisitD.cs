using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class VisitD
{
    public int DocEntry { get; set; }

    public DateOnly VisitDate { get; set; }

    public string CardCode { get; set; } = null!;

    public string? ReasonType { get; set; }

    public string? Remark { get; set; }

    public string? ImageURL { get; set; }
}
