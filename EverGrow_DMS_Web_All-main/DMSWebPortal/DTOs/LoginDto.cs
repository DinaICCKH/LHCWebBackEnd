namespace DMSWebPortal.DTOs
{
    public class LoginDto
    {
        public int SlpCode { get; set; }
        public string SalesName { get; set; }
        public string Telephone { get; set; }
        public string USalesCode { get; set; }
        public string UWhs { get; set; }
        public string Email { get; set; }
        public string Region { get; set; }
        public string Sm { get; set; }
        public string Rsm { get; set; }
        public string TaxCode { get; set; }
        public string NonTaxPre { get; set; }
        public string TaxPre { get; set; }
        public double exchangeRate {  get; set; }
        public string SalesType { get; set; }
        public string AutoSync { get; set; }
        public string LastEndDay { get; set; }
        public string printername { get; set; }
        public string macaddress { get; set; }
        public string IsAllPrinciple { get; set; }
        public string IsTax { get; set; }
        public string IsEndofDay { get; set; }
    }
}
