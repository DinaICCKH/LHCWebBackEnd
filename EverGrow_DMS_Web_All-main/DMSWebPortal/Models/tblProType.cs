using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblProType
{
    public string ProTypeCode { get; set; } = null!;

    public string? ProTypeDesc { get; set; }

    public string? DocStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
