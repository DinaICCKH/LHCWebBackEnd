
using System.IdentityModel.Tokens;
using System.Security.Claims;
using DMSWebPortal.DTOs;
using DMSWebPortal.Models;
using DMSWebPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace DMSWebPortal.Controllers
{
    [Route("api/auth")]   // <-- base route: api/actdb
    [ApiController]
    [Authorize] // Secure all methods in this controller
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly RequestLogService requestLog;
        public AuthController(AppDbContext context, IConfiguration configuration, RequestLogService requestLog)
        {
            _context = context;
            _configuration = configuration;
            this.requestLog = requestLog;
        }

        [AllowAnonymous]
        [HttpGet("login")]
        [SwaggerOperation(
            Summary = "Login API",
            Description = "Authenticate user and return JWT access + refresh token."
        )]
        public async Task<ActionResult<AuthResponseDto>> GetLoginInfo(
            [FromQuery] string uSalesCode,
            [FromQuery] string uSecret,
            [FromQuery] string uDeviceID,
            [FromQuery] string uClientType,
            [FromServices] JwtTokenService jwtService)
        {
            try
            {
                //uSecret = new EncryptDecrypt().Encrypt(uSecret);
                uSecret = DMS_Controller.PasswordHasher.HashPassword(uSecret);

                Console.WriteLine($"Encrypted Password: {uSecret}");

                // 🔹 Call stored procedure using EF Core asynchronously
                var result = await _context.LoginResponses
                    .FromSqlRaw(
                        "EXEC [dbo].[ICC_API_02_Login] @p0, @p1, @p2, @p3",
                        uSalesCode, uSecret, uDeviceID, uClientType
                    ).ToListAsync();             // <--- asynchronous call

                if (!result.Any())
                    return Unauthorized("Invalid user ID or password.");
                var user = result.FirstOrDefault();
                // 🔐 Generate tokens
                var accessToken = jwtService.GenerateAccessToken(user.USalesCode ?? "");
                var refreshToken = jwtService.GenerateRefreshToken(user.USalesCode ?? "");
                jwtService.WriteAccessTokenToCookie(HttpContext, user.USalesCode ?? "");

                // ✅ Return response
                return Ok(new AuthResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    User = user
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { error = "Error login", details = ex.Message });
            }
            
        }
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [SwaggerOperation(
            Summary = "Refresh Token",
            Description = "Authenticate user and return new JWT access + new refresh token."
        )]
        public ActionResult<AuthResponseDto> RefreshToken([FromBody] RefreshTokenRequestDto request,
                                                  [FromServices] JwtTokenService jwtService)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest(new { error = "Refresh token is required" });

            try
            {
                // validate refresh token
                var principal = jwtService.GetPrincipalFromExpiredToken(request.RefreshToken, isRefresh: true);
                var userId = principal.Identity?.Name;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { error = "Invalid refresh token" });

                // ensure token type
                var typeClaim = principal.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
                if (typeClaim != "refresh")
                    return Unauthorized(new { error = "Invalid token type" });

                // generate new tokens
                var newAccessToken = jwtService.GenerateAccessToken(userId);
                var newRefreshToken = jwtService.GenerateRefreshToken(userId);
                jwtService.WriteAccessTokenToCookie(HttpContext, userId);
                return Ok(new AuthResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (SecurityTokenException)
            {
                return Unauthorized(new { error = "Invalid refresh token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error refreshing token", details = ex.Message });
            }
        }
    }
}
