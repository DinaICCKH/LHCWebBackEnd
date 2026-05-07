namespace DMSWebPortal.DTOs
{
    public class ProFOCTypeDto
    {
        public string Code { get; set; } = null!;
        public string? Dscription { get; set; }
        public string? DocStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
