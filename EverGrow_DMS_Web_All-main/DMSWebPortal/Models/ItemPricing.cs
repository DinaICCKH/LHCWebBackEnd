namespace DMSWebPortal.Models
{
    public class ItemPricing
    {
        public string Mode { get; set; }
        public string ItemCode { get; set; }
        public string PriceListCode { get; set; }
        public string UoMEntry { get; set; }
        public decimal? Amount { get; set; }
        public string? Status { get; set; }
    }
}
