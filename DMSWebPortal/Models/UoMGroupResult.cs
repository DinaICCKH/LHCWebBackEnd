namespace DMSWebPortal.Models
{
    public class UoMGroupResult
    {
        public int Code { get; set; }
        public string? Message { get; set; }

        public int? UgpEntry { get; set; }
        public int? UoMEntry { get; set; }

        public string? UgpName { get; set; }
        public string? UoMCode { get; set; }

        public decimal? BaseQty { get; set; }
        public decimal? AltQty { get; set; }

        public string? Status { get; set; }
    }
}
