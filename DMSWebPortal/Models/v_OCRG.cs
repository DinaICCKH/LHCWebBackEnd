using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_OCRG
{
    public short GroupCode { get; set; }

    public string GroupName { get; set; } = null!;

    public string? GroupType { get; set; }

    public string? Locked { get; set; }

    public string? DataSource { get; set; }

    public short? UserSign { get; set; }

    public short? PriceList { get; set; }

    public string? DiscRel { get; set; }

    public string? EffecPrice { get; set; }
}
