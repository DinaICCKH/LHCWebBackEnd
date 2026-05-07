using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblProMonthly
{
    public int Code { get; set; }

    public string? Dscription { get; set; }

    public string? DocStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
