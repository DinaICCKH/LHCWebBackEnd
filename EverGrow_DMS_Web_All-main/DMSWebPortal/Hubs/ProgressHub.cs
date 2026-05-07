using Microsoft.AspNetCore.SignalR;

namespace DMSWebPortal.Hubs
{
    public class ProgressHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendProgress(string connectionId, int progress)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveProgress", progress);
        }
    }
}
