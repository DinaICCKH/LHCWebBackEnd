using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblUoMGroup
{
    /// <summary>
    /// UgpEntry+UoMEntry=Primary Key
    /// </summary>
    /// 

    //add more marketing document 
    public string Status { get; set; }
    public string Mode { get; set; }

    //-------------------------------------
    public string UgpKey { get; set; }
    public int UgpEntry { get; set; }
    public string UgpName { get; set; }
    public int UoMEntry { get; set; }
    public string UoMCode { get; set; }
    public decimal? BaseQty { get; set; }
    public decimal? AltQty { get; set; }
}
public partial class tblUoM
{
    //add more marketing document 
    public string Mode { get; set; }
    //-------------------------------------
    public int UoMEntry { get; set; }
    public string UoMCode { get; set; } = null!;
}

