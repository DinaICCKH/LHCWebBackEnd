using DMSWebPortal.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _context;

        // Thread-safe dictionary: userId -> connectionId
        public static ConcurrentDictionary<string, string> UserConnections = new();

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }

        // Track connection
        public override Task OnConnectedAsync()
        {
            var userId = GetCurrentUserId();
            if (userId != null)
                UserConnections[userId] = Context.ConnectionId;

            Console.WriteLine($"✅ Client connected: {Context.ConnectionId} (UserId: {userId})");
            return base.OnConnectedAsync();
        }

        // Remove connection
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetCurrentUserId();
            if (userId != null)
                UserConnections.TryRemove(userId, out _);

            return base.OnDisconnectedAsync(exception);
        }

        private string GetCurrentUserId()
        {
            var userIdStr = Context.GetHttpContext()?.Request.Query["usercode"];
            return userIdStr;
        }

        // Broadcast message
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageImport(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessageImport", user, message);
        }

        // Progress bar update
        public async Task SendProgress(string connectionId, int percentage)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveProgress", percentage);
        }

        // ✅ Mark ALL unread notifications for current user as read (personal only)
        public async Task ResetBadge()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return;

            // Only mark personal notifications as viewed
            var unread = await _context.Notifications
                .Where(n => n.RecipientId == userId && n.IsViewed == false)
                .ToListAsync();

            foreach (var n in unread)
                n.IsViewed = true;

            await _context.SaveChangesAsync();

            var unreadCount = await _context.Notifications
                .CountAsync(n => (n.RecipientId == userId) && n.IsViewed == false);

            await Clients.Caller.SendAsync("UpdateBadge", unreadCount);
        }

        // ✅ Get unread count for current user
        public async Task<int> GetCurrentNotificationCount()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return 0;

            return await _context.Notifications
                .CountAsync(n => (n.RecipientId == userId) && n.IsViewed == false);
        }

        // ✅ Send alert to a specific user
        public async Task SendAlertToUser(string userId, string message)
        {
            if (UserConnections.TryGetValue(userId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", "System", message);

                var unreadCount = await _context.Notifications
                    .CountAsync(n => (n.RecipientId == userId ) && n.IsViewed == false);

                await Clients.Client(connectionId).SendAsync("UpdateBadge", unreadCount);
            }
        }

        // ✅ Get notifications (unread only)
        public async Task<List<VNotification>> GetNotifications()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null) return new List<VNotification>();

                var result = await _context.VNotifications
                    .Where(n => (n.RecipientId.ToString() == userId) && n.IsViewed == false)
                    .OrderByDescending(n => n.CreatedDate)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                return new List<VNotification>();
            }
        }

        // ✅ Mark ONE notification as read (skip global)
        public async Task MarkNotificationRead(int notificationId)
        {
            var userId = GetCurrentUserId();
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null)
                return;

            // Do NOT mark global (RecipientId = 0) notifications as read globally
            //if (notification.RecipientId == userId)
            //    return;

            notification.IsViewed = true;
            await _context.SaveChangesAsync();

            // Update badge for that user
            var unreadCount = await _context.Notifications
                .CountAsync(n => (n.RecipientId == notification.RecipientId) && n.IsViewed == false);

            if (UserConnections.TryGetValue(notification.RecipientId, out var connectionId))
                await Clients.Client(connectionId).SendAsync("UpdateBadge", unreadCount);
        }
        public async Task MarkAllNotificationsRead()
        {
            var userId = GetCurrentUserId();
            if (userId==null)
                return;

            var notifications = await _context.Notifications
                .Where(n => n.RecipientId == userId && n.IsViewed==false)
                .ToListAsync();

            if (notifications.Any())
            {
                notifications.ForEach(n => n.IsViewed = true);
                await _context.SaveChangesAsync();

                // Optionally, update badge for this user
                await Clients.User(userId.ToString()).SendAsync("UpdateBadge", 0);
            }
        }
    }
}
