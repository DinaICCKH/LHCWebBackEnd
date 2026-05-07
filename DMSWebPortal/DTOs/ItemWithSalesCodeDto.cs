namespace DMSWebPortal.DTOs
{
    public class ItemWithSalesCodeDto
    {
        public string ItemCode { get; set; }
        public string? ItemName { get; set; }
        public short? ItemGroupCode { get; set; }
        public string? ItemGroupName { get; set; }
        public int? UgpEntry { get; set; }
        public decimal? Onhand { get; set; }
        public decimal? OnOrder { get; set; }
        public decimal? IsCommited { get; set; }
        public decimal? Available { get; set; }
        public decimal? MinLevel { get; set; }
        public decimal? MaxLevel { get; set; }
        public string? Status { get; set; }
        public string? ImageUrlServer { get; set; }
        public string? ImageUrlLocal { get; set; }
        public string? FrgnName { get; set; }
        public string? InvUoMcode { get; set; }
        public int? InvUoMentry { get; set; }
        public string? UProGroup { get; set; }
        public string? HasPromotion { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? OcrCode { get; set; }
        public string? OcrCode2 { get; set; }
        public string? OcrCode3 { get; set; }
        public string? OcrCode4 { get; set; }
        public string? U_SalesCode { get; set; }
    }
}
