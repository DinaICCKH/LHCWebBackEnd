namespace DMSWebPortal.DTOs
{
    public class IncomeDto
    {
        public int DocEntry { get; set; }
        public int? SODocEntry { get; set; }
        public decimal? SOBalance { get; set; }
        public string? BankCode { get; set; }
        public decimal? BankAmount { get; set; }
        public decimal? CashAmount { get; set; }
        public string? CurCode { get; set; }
        public int? SAPIncomeDocEntry { get; set; }
        public string? IntegrationStatus { get; set; }
        public string? LastError { get; set; }
    }
    public class IncomeDtoApp
    {
        public int docEntry { get; set; }
        public int? soDocEntry { get; set; }
        public decimal? soBalance { get; set; }
        public string? bankCode { get; set; }
        public decimal? bankAmountUSD { get; set; }
        public decimal? bankAmountKHR { get; set; }
        public decimal? cashAmountUSD { get; set; }
        public decimal? cashAmountKHR { get; set; }
        public string? sapIncomeDocEntry { get; set; }
        public string? integrationStatus { get; set; }
        public string? lastError { get; set; }
    }
}

