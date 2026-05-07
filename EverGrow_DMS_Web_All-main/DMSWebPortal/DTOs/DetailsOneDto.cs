namespace DMSWebPortal.DTOs
{
    public class DetailsOneDto
    {
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? UoMentry { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? LineTotal { get; set; }
        public string WhsCode { get; set; }
        public int? RefLineNum { get; set; }
        public string SaleType { get; set; }
        public string ProCode { get; set; }
        public string ProLineNum { get; set; }
        public string PromotionType { get; set; }
    }
}
