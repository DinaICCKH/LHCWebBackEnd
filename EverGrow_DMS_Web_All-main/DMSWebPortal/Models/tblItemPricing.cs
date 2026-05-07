using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DMSWebPortal.Models;

public partial class tblItemPricing
{
    /// <summary>
    /// ItemCode+PriceListCode+UoMEntry=Primary Key
    /// </summary>
    public string PricingKey { get; set; } = null!;

    public string? ItemCode { get; set; }

    public int? PriceListCode { get; set; }

    public decimal? Amount { get; set; }

    public int? UoMEntry { get; set; }
}

public partial class tblItemWhsPricing
{
    /// <summary>
    /// ItemCode+PriceListCode+UoMEntry=Primary Key
    /// </summary>
    public string Code { get; set; } = null!;

    public string ItemCode { get; set; }

    public string WhsCode { get; set; }

    public int UoMEntry { get; set; }

    public double Amount { get; set; }
}

[Table("tblAR")]
public class TblAR
{
    [Key]
    public int DocEntry { get; set; }
    [Required]
    public DateTime DocDate { get; set; }
    [Required]
    [StringLength(20)]
    public string WebNo { get; set; }
    [Column(TypeName = "numeric(19,6)")]
    public decimal Amount { get; set; }
    [Column(TypeName = "numeric(19,6)")]
    public decimal PaidAmount { get; set; } = 0;
    public DateTime? UpdateDate { get; set; }
}