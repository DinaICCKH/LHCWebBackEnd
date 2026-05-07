using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblPayment
{
    public int TermCode { get; set; }

    public string? TermName { get; set; }

    public int? AddMonth { get; set; }

    public int? AddDay { get; set; }

    public string? Status { get; set; }
}
