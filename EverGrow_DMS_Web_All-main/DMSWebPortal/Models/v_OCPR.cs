using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_OCPR
{
    public int CntctCode { get; set; }

    public string CardCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Tel1 { get; set; } = null!;

    public string Tel2 { get; set; } = null!;

    public string Cellolar { get; set; } = null!;

    public string Fax { get; set; } = null!;

    public string E_MailL { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime CreateDate_ { get; set; }
}
