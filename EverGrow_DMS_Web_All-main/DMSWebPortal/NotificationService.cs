using DMSWebPortal.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRChat.Hubs;

public class NotificationService
{
    private readonly AppDbContext _context;
    private readonly IHubContext<ChatHub> _hubContext;

    public NotificationService(AppDbContext context, IHubContext<ChatHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task CreateSalesOrderNotification(int salesOrderId, string customerName, string salesCode)
    {
        // ✅ Find the employee
        var employee = await _context.tblSalesEmployees
            .FirstOrDefaultAsync(x => x.U_SalesCode == salesCode);

        if (employee == null) return; // Employee not found, stop here

        //send to all user
        var users =await _context.Users.Where(x => x.Status == "Active").ToListAsync();
        foreach(var x in users)
        {
            // ✅ Create notification
            var notification = new Notification
            {
                RecipientId = x.Code,
                RecipientType = "User",
                SenderId = employee.SlpCode, // You might want SenderId as system or creator, not same as recipient
                SenderType = "Employee",
                Message = $"🧾 New sales order #{salesOrderId} for {customerName} created.",
                Type = "SalesOrderCreated",
                RelatedEntityType = "SalesOrder",
                RelatedEntityId = salesOrderId,
                CreatedDate = DateTime.Now,
                IsViewed = false,
                IsGlobal = false
            };

            // ✅ Save notification in DB
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // ✅ Broadcast to all clients (optional)
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", notification.Message);
            // ✅ Send to specific user
            // ⚠ Requires IUserIdProvider to be registered and client passing usercode
            await _hubContext.Clients.User(x.Code.ToString())
                .SendAsync("ReceiveMessage", "System", notification.Message);

            // ✅ Update badge count for the user
            var unreadCount = await _context.Notifications
                .CountAsync(n => n.RecipientId == x.Code && n.IsViewed == false);

            await _hubContext.Clients.User(employee.SlpCode.ToString())
                .SendAsync("UpdateBadge", unreadCount);
        }

       
    }
}

public class UserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        // Map the usercode from query string
        var userCode = connection.GetHttpContext()?.Request.Query["usercode"];
        return string.IsNullOrEmpty(userCode) ? null : userCode.ToString();
    }
}
