using DMSWebPortal.DTOs.Response;

namespace DMSWebPortal.DTOs
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { set; get; }
        public LoginResponse User { get; set; }
    }
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; }
    }

}
