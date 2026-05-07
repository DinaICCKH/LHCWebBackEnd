using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblNewFeed
{
    public int FeedID { get; set; }

    /// <summary>
    /// if new Table, Table If add new column, Column
    /// </summary>
    public string? ObjType { get; set; }

    /// <summary>
    /// If New table, Table Name
    /// </summary>
    public string? ObjName { get; set; }

    /// <summary>
    /// Add,Update,Delete
    /// </summary>
    public string? ObjAction { get; set; }
}
