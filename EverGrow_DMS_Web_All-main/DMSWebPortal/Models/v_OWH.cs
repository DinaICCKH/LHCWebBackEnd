using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_OWH
{
    public string WhsCode { get; set; } = null!;

    public string? WhsName { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string State { get; set; } = null!;

    public DateTime createDate { get; set; }
}
