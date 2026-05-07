using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Hubs;

namespace DMSWebPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("notify")]
        public async Task<IActionResult> Notify()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", "New Purchase Order created!");
            await _hubContext.Clients.All.SendAsync("UpdateBadge", 1);
            return Ok("Notification sent");
        }
    }
}
