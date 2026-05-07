using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSWebPortal.Models;

[Table("tblSalesEmployee")]
public partial class tblSalesEmployee
{
    public int SlpCode { get; set; }

    public string SalesName { get; set; } = null!;
    public string? U_Whs { get; set; }

    public string? U_SalesCode { get; set; }

    public string? Status { get; set; }
    public string? SALType { get; set; }
    public string? IsAllPrinciple { get; set; }
    public string? IsTax { get; set; }
    public string? PrincipleAssign { get; set; }
    public string? IsDepo{ set; get; }
    public string? DepoID { set; get; }
    public string? MainWhs { set; get; }

}
