namespace DMSWebPortal.Models
{
    public class Customer
    {
        public string? Mode { get; set; }
        public string? CardCode { get; set; }           // nvarchar(50), Primary Key
        public string? CardName { get; set; }           // nchar(10)
        public string? CardFName { get; set; }          // nchar(10)
        public int? GroupCode { get; set; }             // int, nullable
        public string? GroupName { get; set; }          // nvarchar(200), nullable
        public string? ID { get; set; }                 // nvarchar(50), nullable
        public string? Tel1 { get; set; }               // nvarchar(50), nullable
        public string? Tel2 { get; set; }               // nvarchar(50), nullable
        public string? Mobile { get; set; }             // nvarchar(50), nullable
        public string? ContactPerson { get; set; }      // nvarchar(50), nullable
        public string? ContactPersonName { get; set; }  // nvarchar(500), nullable
        public string? FullAddress { get; set; }        // nvarchar(500), nullable
        public string? Paymenterm { get; set; }         // nvarchar(50), nullable
        public string? PriceList { get; set; }          // nvarchar(50), nullable
        public decimal? CreditLimit { get; set; }       // numeric(19,6), nullable
    }
}
