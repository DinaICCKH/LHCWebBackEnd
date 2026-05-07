using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_Reason
{
    public int Code { get; set; }

    public string? Reason { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? ReasonKH { get; set; }

    public string? Status { get; set; }

    public string? CreatedByName { get; set; }

    public string? UpdatedByName { get; set; }
}
