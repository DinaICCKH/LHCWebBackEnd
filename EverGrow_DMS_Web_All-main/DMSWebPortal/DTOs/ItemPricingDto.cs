namespace DMSWebPortal.DTOs
{
    public class ItemPricingDto
    {
        public int PricingKey { get; set; }
        public string ItemCode { get; set; } = null!;
        public int PriceListCode { get; set; }
        public decimal Amount { get; set; }
        public int? UoMEntry { get; set; }
    }

}
