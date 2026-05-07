namespace DMSWebPortal.DTOs
{

    public class HeaderDto
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string DelAddress { get; set; }
        public DateOnly? DocDate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? AfterDis { get; set; }
        public decimal? Vatamount { get; set; }
        public decimal? Total { get; set; }
        public string SalesCode { get; set; }
        public string Remark { get; set; }
        public int? AppId { get; set; }
        public string? AppDocNo { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string? CheckInLateLong { get; set; }
        public string? CheckInRemark { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string? CheckOutLateLong { get; set; }
        public string? CheckOutRemark { get; set; }
        public string? VatType { get; set; }
        public int? TermCode { get; set; }
        public string? CheckInID { get; set; }
        public string? CheckOutID { get; set; }
        public string? PONo { get; set; }
        public decimal? USDAmount { get; set; }
        public decimal? KHRAmount { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string? SaleType { get; set; }
    }
}
