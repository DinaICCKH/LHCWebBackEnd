using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblItemStock
{
    /// <summary>
    /// ItemCode+WhsCode=Primary Key
    /// </summary>
    public string ItemStockKey { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string WhsCode { get; set; } = null!;

    public decimal Onhand { get; set; }

    public decimal OnOrder { get; set; }

    public decimal IsCommited { get; set; }

    public decimal? Available { get; set; }

    public decimal MinStock { get; set; }

    public decimal MaxStock { get; set; }

    public decimal? AltQty { get; set; }
    public DateTime? UpdateDate { get; set; }
}
public class ItemStockResponse
{
    public string ItemStockKey { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string WhsCode { get; set; } = null!;

    public decimal Onhand { get; set; }

    public decimal OnOrder { get; set; }

    public decimal IsCommited { get; set; }

    public decimal? Available { get; set; }

    public decimal MinStock { get; set; }

    public decimal MaxStock { get; set; }

    public decimal? AltQty { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string? SaleType { set; get; }
}
