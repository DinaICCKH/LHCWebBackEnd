using Microsoft.AspNetCore.Http;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public static class UserHelper
{
    /// <summary>
    /// Get the UserId from Claims, JWT header, or cookie.
    /// Works even if the endpoint is [AllowAnonymous].
    /// </summary>
    public static string? GetUserId(HttpContext httpContext)
    {
        if (httpContext == null) return null;

        // 1️⃣ Try ClaimsPrincipal first
        if (httpContext.User?.Identity?.IsAuthenticated == true)
        {
            var id = httpContext.User.FindFirst("userId")?.Value;
            if (!string.IsNullOrEmpty(id)) return id;

            id = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(id)) return id;

            id = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(id)) return id;
        }

        // 2️⃣ Read token from header or cookie
        var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "")
                    ?? httpContext.Request.Cookies["dms_access_token"];

        if (string.IsNullOrEmpty(token)) return null;

        // 3️⃣ Parse JWT
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // 4️⃣ Read claims
        var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (!string.IsNullOrEmpty(userId)) return userId;

        userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId)) return userId;

        userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (!string.IsNullOrEmpty(userId)) return userId;

        // 5️⃣ fallback to SessionId as last resort
        userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;
        if (!string.IsNullOrEmpty(userId)) return userId;

        return null;
    }
}