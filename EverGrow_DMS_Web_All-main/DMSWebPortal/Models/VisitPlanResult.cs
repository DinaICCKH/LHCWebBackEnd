namespace DMSWebPortal.Models
{
    public class VisitPlanResult
    {
        public int Code { get; set; }
        public string? Message { get; set; }

        public int? DocEntry { get; set; }

        public int? SalesCode { get; set; }

        public int? DocYear { get; set; }

        public string? RemarkH { get; set; }   // T1.Remark

        public string? DocNum { get; set; }

        public string? Status { get; set; }

        public DateTime? VisitDate { get; set; }

        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? Tel1 { get; set; }

        public string? ContactPersonName { get; set; }

        public string? ReasonType { get; set; }

        public string? Remark { get; set; }   // T2.Remark

        public string? Synced { get; set; }

        public int? DetailEntry { get; set; }

        public string? FullAddress { get; set; }
    }
}
