using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblMainCat
{
    public string Code { get; set; } = null!;

    public string? Dscription { get; set; }

    public string? DocStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
