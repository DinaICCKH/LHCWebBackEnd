using DMSWebPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace DMSWebPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-alert")]
        [SwaggerOperation(
            Summary = "Get For Telegram Alert",
            Description = "This end-point controll for get alert from database to alert in telegrame."
        )]
        public async Task<IActionResult> GetAlertAsync()
        {
            try
            {
                var result = await _context.ICC_API_Get_AlertsResults
                    .FromSqlRaw("EXEC dbo.ICC_API_Get_Alerts")
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // You may log ex here
                return StatusCode(500, new
                {
                    Message = "Failed to retrieve alerts",
                    Error = ex.Message
                });
            }
        }
        [HttpPost("set-alert")]
        [SwaggerOperation(
            Summary = "Set For Telegram Alert",
            Description = "This end-point controll for set alert from database to alert in telegrame."
        )]
        public async Task<IActionResult> SetAlertAsync([FromBody] SetAlertRequest request)
        {
            if (request == null || request.DocEntry <= 0 || string.IsNullOrEmpty(request.DocType))
                return BadRequest(new { Message = "Invalid parameters" });

            try
            {
                // Use parameterized SQL to prevent injection
                var docEntryParam = new Microsoft.Data.SqlClient.SqlParameter("@DocEntry", request.DocEntry);
                var docTypeParam = new Microsoft.Data.SqlClient.SqlParameter("@DocType", request.DocType);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.ICC_API_Set_Alert @DocEntry, @DocType",
                    docEntryParam,
                    docTypeParam
                );

                return Ok(new
                {
                    Message = "Alert updated successfully",
                    request.DocEntry,
                    request.DocType
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to update alert",
                    Error = ex.Message
                });
            }
        }

    }
}
