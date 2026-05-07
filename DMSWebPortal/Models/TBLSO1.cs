using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSWebPortal.Models
{
    [Table("TBLSO1")]
    public class TBLSO1
    {
        [Key]
        [Column(Order = 0)]
        public int DocEntry { get; set; }

        [Key]
        [Column(Order = 1)]
        public int LineNum { get; set; }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? UoMEntry { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public int? RefLineNum { get; set; }
        public string SaleType { get; set; }
        public string ProCode { get; set; }
        public int? ProLineNo { get; set; }
        public string PromotionType { get; set; }
    }
}
