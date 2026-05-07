using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSWebPortal.Models;

public partial class Reason
{
    
    public int Code { get; set; }

    public string? Reason1 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public string? ReasonKH { get; set; }

    public string? Status { get; set; }

    public string? ReasonEN { get; set; }
}
