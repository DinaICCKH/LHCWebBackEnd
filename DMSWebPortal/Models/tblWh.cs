using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblWh
{
    public string WhsCode { get; set; } = null!;

    public string? WhsName { get; set; }

    public string? WhsStatus { get; set; }

    public string? Shows { set; get; }
}
