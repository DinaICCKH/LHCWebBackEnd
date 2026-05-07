using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblProFOCSetup
{
    public int Code { get; set; }

    public string? ProFOCTypeCode { get; set; }

    public string? DocStatus { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
