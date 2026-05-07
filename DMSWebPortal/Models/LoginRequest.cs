namespace DMSWebPortal.Models
{
    public class LoginRequest
    {
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string DeviceID { get; set; }
    }

    public class LoginResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string? CodeUser { get; set; }
        public string? CompanyName { get; set; }
        public string? DeviceID { get; set; }
        public string? Email { get; set; }
        public string? IsWebUser { get; set; }
        public string? Manager { get; set; }
        public string? Name { get; set; }
        public string? PrinterMac { get; set; }
        public string? PrinterName { get; set; }
        public string? Profile { get; set; }
        public int? SlpCode { get; set; }
        public string? Status { get; set; }
        public string? UserType { get; set; }
    }
}
