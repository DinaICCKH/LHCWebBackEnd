using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblBank
{
    public string Code { get; set; } = null!;

    public string? BankCode { get; set; }

    public string? BankName { get; set; }

    public string? GLAccount { get; set; }

    public string? CurCode { get; set; }

    public string? DocStatus { get; set; }
}
