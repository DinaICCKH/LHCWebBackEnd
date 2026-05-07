using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblPromotionGroup
{
    public string ProGroupCode { get; set; } = null!;

    public string? ProDesc { get; set; }

    public string? ProStatus { get; set; }

    public DateTime? CreatedDate { get; set; }
}
