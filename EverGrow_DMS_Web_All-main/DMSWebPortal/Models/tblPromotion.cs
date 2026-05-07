using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblPromotion
{
    public int ProEntry { get; set; }

    public string? PrincipleCode { get; set; }

    public string? PrincipleDesc { get; set; }

    public string? DocStatus { get; set; }

    public DateOnly? FDate { get; set; }

    public DateOnly? TDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
