using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class SOLog
{
    public int SysNo { get; set; }

    public string? SalesCode { get; set; }

    public DateTime? SyncDate { get; set; }

    public string? AppDocNum { get; set; }

    public string? JsonText { get; set; }
}
