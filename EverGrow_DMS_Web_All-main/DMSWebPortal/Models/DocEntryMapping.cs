namespace DMSWebPortal.Models
{
    public class DocEntryMapping
    {
        public int ID { get; set; }               // Primary key
        public string? DocType { get; set; }       // e.g., "CheckIn", "SaleOrder", "CheckOut"
        public int? AppDocEntry { get; set; }     // Document entry in App
        public int? DMSEntry { get; set; }        // Document entry in DMS
        public int? SAPEntry { get; set; }        // Document entry in SAP
        public string? SalesCode { get; set; }        // Document entry in SAP
    }
}
