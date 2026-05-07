using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DMSWebPortal.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        // 🔐 Generate Access Token
        public string GenerateAccessToken(string userId)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtAccessToken:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId), // ✅ ADD
                new Claim(ClaimTypes.Name, userId),
                new Claim("SessionId", Guid.NewGuid().ToString()),
                new Claim("type", "access")
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtAccessToken:Issuer"],
                audience: _config["JwtAccessToken:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtAccessToken:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // 🔄 Generate Refresh Token
        public string GenerateRefreshToken(string userId)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["JwtRefreshToken:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId), // ✅ ADD
                new Claim(ClaimTypes.Name, userId),
                new Claim("type", "refresh")
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtRefreshToken:Issuer"],
                audience: _config["JwtRefreshToken:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(Convert.ToDouble(_config["JwtRefreshToken:DurationInDays"])),
                signingCredentials: creds
            );
            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // 🔍 Validate Token (ACCESS or REFRESH)
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, bool isRefresh = false)
        {
            var key = isRefresh
                ? _config["JwtRefreshToken:Key"]
                : _config["JwtAccessToken:Key"];

            var issuer = isRefresh
                ? _config["JwtRefreshToken:Issuer"]
                : _config["JwtAccessToken:Issuer"];

            var audience = isRefresh
                ? _config["JwtRefreshToken:Audience"]
                : _config["JwtAccessToken:Audience"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // allow expired tokens
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            return principal;
        }
        public void WriteAccessTokenToCookie(HttpContext httpContext, string userId)
        {
            var token = GenerateAccessToken(userId);
            httpContext.Response.Cookies.Append("dms_access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtAccessToken:DurationInMinutes"]+5))// add 5 min because it expire before token expired
            });
        }
    }
}