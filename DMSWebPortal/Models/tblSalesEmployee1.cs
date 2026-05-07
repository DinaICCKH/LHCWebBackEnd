using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSWebPortal.Models;

[Table("tblSalesEmployee1")]
public partial class tblSalesEmployee1
{
    public int DocEntry { get; set; }
    public int LineNum { get; set; }
    public int SlpCode { get; set; }
    public string Region { get; set; }
    public string DocStatus { get; set; }
}
