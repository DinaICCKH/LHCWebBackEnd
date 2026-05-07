namespace DMSWebPortal.Models
{
    public class ItemPricingResult
    {
        public int Code { get; set; }
        public string? Message { get; set; }

        public string? ItemCode { get; set; }
        public string? PriceListCode { get; set; }
        public string? UoMEntry { get; set; }

        public decimal? Amount { get; set; }
        public string? Status { get; set; }
    }
}
