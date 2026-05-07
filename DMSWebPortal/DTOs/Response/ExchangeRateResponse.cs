namespace DMSWebPortal.DTOs.Response
{
    public partial class ExchangeRateResponse
    {
        public int Code { get; set; }

        public string? CurCode { get; set; }

        public decimal? Amount { get; set; }

        public DateOnly? RateDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
