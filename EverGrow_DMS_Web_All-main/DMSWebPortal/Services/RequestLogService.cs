using System.Text;
using Newtonsoft.Json;

namespace DMSWebPortal.Services
{
    public class RequestLogService
    {
        private readonly string _basePath;

        public RequestLogService()
        {
            // Base folder for request logs
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "LogFiles/RequestLogs");

            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }
        public void Log<T>(string docType, string fileName, T requestData)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(docType))
                    docType = "Unknown";
                if (string.IsNullOrWhiteSpace(fileName))
                    fileName = "Request";

                // Create folder structure: DocType / yyyy-MM-dd
                string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
                string folderPath = Path.Combine(_basePath, docType, dateFolder);

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // Create timestamped file name
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string filePath = Path.Combine(folderPath, $"{fileName}_{timestamp}.json");

                // Serialize requestData

                string json = JsonConvert.SerializeObject(requestData, Formatting.Indented);

                // Save to file
                File.WriteAllText(filePath, json, Encoding.UTF8);
            }
            catch
            {
                // Avoid crashing if logging fails
            }
        }
    }
}