using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblAlterAppTable
{
    public int DocEntry { get; set; }

    public string? TableName { get; set; }

    public string? NewColumn { get; set; }

    public string? AlterTable { get; set; }

    public DateTime? CreatedDate { get; set; }
}
