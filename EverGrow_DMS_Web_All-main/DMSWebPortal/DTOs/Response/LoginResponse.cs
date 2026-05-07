namespace DMSWebPortal.DTOs.Response
{
    public class LoginResponse
    {
        
       
        public string? CompanyName { get; set; }    // ✅ Added
        public string? UserType { get; set; }        // ✅ Added
        public string? Manager { get; set; }         // ✅ Added
        public string? UserCode { set; get; }
        public string? UserName { set; get; }
        public string? UserProfile { set; get; }
        public int? SlpCode { get; set; }
        public string? SalesName { get; set; }
        public string? Telephone { get; set; }
        public string? USalesCode { get; set; }
        public string? UWhs { get; set; }
        public string? Email { get; set; }
        public string? Region { get; set; }
        public string? SM { get; set; }
        public string? RSM { get; set; }
        public string? TaxCode { get; set; }
        public string? NonTaxPre { get; set; }
        public string? TaxPre { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string? SalesType { get; set; }
        public string? AutoSync { get; set; }        // ✔ FIX
        //public DateTime? LastEndDay { get; set; }    // ✔ FIX
        public string? LastEndDay { get; set; }
        public string? PrinterName { get; set; }
        public string? MacAddress { get; set; }
        public string? IsAllPrinciple { get; set; }
        public string? IsTax { get; set; }             // ✔ FIX
        public string? IsEndofDay { get; set; }        // ✔ FIX
    }
}
