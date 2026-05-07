using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class VisitH
{
    public int DocEntry { get; set; }

    public int? SalesCode { get; set; }

    public int? DocYear { get; set; }

    public string? Remark { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? DocNum { get; set; }

    public string? Status { get; set; }
}
