
using DMSWebPortal.Models;
using System.Text;
using System.Text.Json;
using static DMSWebPortal.Models.SO_ApiModels;

namespace DMSWebPortal.Services
{
    public class SOApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _jsonOptions;

        public SOApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["SOApi:BaseUrl"] ?? "http://localhost:5008";
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

       
        // ENDPOINT 1: GET SO Header with all filters
   
        public async Task<SO_HeaderApiResponse?> GetSOHeaderAsync(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string? docStatus = null,
            string? cardCode = null,
            string? cardName = null,
            string? salesCode = null,
            string? sapSyncStatus = null,
            string? source = null,
            string? saleType = null,
            string? appStatus = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                //  Build query string with filters
                var queryParams = new List<string>();

                if (fromDate.HasValue)
                    queryParams.Add($"fromDate={fromDate.Value:yyyy-MM-dd}");
                if (toDate.HasValue)
                    queryParams.Add($"toDate={toDate.Value:yyyy-MM-dd}");
                if (!string.IsNullOrEmpty(docStatus))
                    queryParams.Add($"docStatus={docStatus}");
                if (!string.IsNullOrEmpty(cardCode))
                    queryParams.Add($"cardCode={cardCode}");
                if (!string.IsNullOrEmpty(cardName))
                    queryParams.Add($"cardName={cardName}");
                if (!string.IsNullOrEmpty(salesCode))
                    queryParams.Add($"salesCode={salesCode}");
                if (!string.IsNullOrEmpty(sapSyncStatus))
                    queryParams.Add($"sapSyncStatus={sapSyncStatus}");
                if (!string.IsNullOrEmpty(source))
                    queryParams.Add($"source={source}");
                if (!string.IsNullOrEmpty(saleType))
                    queryParams.Add($"saleType={saleType}");
                if (!string.IsNullOrEmpty(appStatus))
                    queryParams.Add($"appStatus={appStatus}");

                queryParams.Add($"pageNumber={pageNumber}");
                queryParams.Add($"pageSize={pageSize}");

                var queryString = string.Join("&", queryParams);
                var url = $"{_baseUrl}/api/DMS_/SORead?{queryString}";

                //  Call hosted API
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new SO_HeaderApiResponse { Success = false };

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SO_HeaderApiResponse>(json, _jsonOptions);
            }
            catch (Exception ex)
            {
                return new SO_HeaderApiResponse { Success = false };
            }
        }

        
        // ENDPOINT 2: GET SO Rows by DocEntry
        
        public async Task<SO_RowsApiResponse?> GetSORowsAsync(int docEntry)
        {
            try
            {
                var url = $"{_baseUrl}/api/DMS_/SORows/{docEntry}";

                //  Call hosted API
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return new SO_RowsApiResponse { Success = false };

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SO_RowsApiResponse>(json, _jsonOptions);
            }
            catch (Exception ex)
            {
                return new SO_RowsApiResponse { Success = false };
            }
        }

       
        // ENDPOINT 3: Update SO AppStatus
        
        public async Task<SO_UpdateStatusResponse?> UpdateSOStatusAsync(
            int docEntry,
            string appStatus,
            string? remark = null)
        {
            try
            {
                var url = $"{_baseUrl}/api/DMS_/SOStatus/{docEntry}";

                //  Build request body
                var requestBody = new SO_UpdateStatusRequest
                {
                    AppStatus = appStatus,
                    Remark = remark
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Call hosted API
                var response = await _httpClient.PutAsync(url, content);

                var responseJson = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<SO_UpdateStatusResponse>(responseJson, _jsonOptions);
            }
            catch (Exception ex)
            {
                return new SO_UpdateStatusResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}