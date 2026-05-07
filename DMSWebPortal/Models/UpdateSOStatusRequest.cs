namespace DMSWebPortal.Models
{
    public class UpdateSOStatusRequest
    {
        // Allowed values: "Pending", "Approved", "Rejected"
        public string AppStatus { get; set; } = string.Empty;
        public string? Remark { get; set; }
    }
}
