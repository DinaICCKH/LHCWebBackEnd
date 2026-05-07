using System.Text;

namespace DMSWebPortal.Services
{
    public class ErrorLogService
    {
        private readonly string _basePath;
        private static readonly object _lock = new object();

        public ErrorLogService()
        {
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "LogFiles/ErrorLogs");

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public void Log(string message=null, Exception ex = null, string user = null, string url = null)
        {
            try
            {
                // 📁 Folder by date
                string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
                string folderPath = Path.Combine(_basePath, dateFolder);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // 📄 File (optional: split by hour)
                string filePath = Path.Combine(folderPath, "log.txt");

                var log = new StringBuilder();
                log.AppendLine("====================================");
                log.AppendLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                log.AppendLine($"Message: {message}");

                if (!string.IsNullOrEmpty(user))
                    log.AppendLine($"User: {user}");

                if (!string.IsNullOrEmpty(url))
                    log.AppendLine($"URL: {url}");

                if (ex != null)
                {
                    log.AppendLine($"Exception: {ex.Message}");
                    log.AppendLine($"StackTrace: {ex.StackTrace}");

                    if (ex.InnerException != null)
                    {
                        log.AppendLine("---- Inner Exception ----");
                        log.AppendLine($"Inner Message: {ex.InnerException.Message}");
                        log.AppendLine($"Inner Stack: {ex.InnerException.StackTrace}");
                    }
                }

                log.AppendLine("====================================");
                log.AppendLine();

                // 🔒 Thread-safe write
                lock (_lock)
                {
                    File.AppendAllText(filePath, log.ToString());
                }
            }
            catch
            {
                // never throw from logger
            }
        }
    }
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ErrorLogService _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ErrorLogService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Continue to next middleware / request
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log any unhandled exception
                _logger.Log(
                    "Unhandled Exception",
                    ex,
                    context.User?.Identity?.Name, // optional: who triggered it
                    context.Request.Path           // optional: which URL
                );

                // Return 500 to client
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}