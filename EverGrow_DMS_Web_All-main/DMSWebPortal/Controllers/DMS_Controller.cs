using DMSWebPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace DMSWebPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DMS_Controller : ControllerBase
    {
        private readonly AppDbContext _db;

        public DMS_Controller(AppDbContext db)
        {
            _db = db;
        }

        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                // ✅ Add null check!
                if (string.IsNullOrEmpty(password))
                    return string.Empty;

                using var sha = System.Security.Cryptography.SHA256.Create();
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                // Hash the password first
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                // Build JSON for SP
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });


                // Call SP
                var resultList = await _db.LoginResults
                    .FromSqlRaw("EXEC dbo.Get_Login @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result == null)
                    return BadRequest(new { success = false, message = "Login failed." });

                if (result.Code != 200)
                {
                    // Login failed
                    return Unauthorized(new { success = false, message = result.Message });
                }

                // Login success
                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    data = new
                    {
                        result.CodeUser,
                        result.Name,
                        result.Email,
                        result.CompanyName,
                        result.DeviceID,
                        result.IsWebUser,
                        result.Manager,
                        result.PrinterMac,
                        result.PrinterName,
                        result.Profile,
                        result.SlpCode,
                        result.Status,
                        result.UserType
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> GetItems([FromBody] LoginRequest model)
        {
            try
            {
                // Hash password
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                // Build JSON body
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                // Call Stored Procedure
                var resultList = await _db.ItemLoginResults
                    .FromSqlRaw("EXEC dbo.Get_Item_By_Login @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                if (resultList == null || resultList.Count == 0)
                    return BadRequest(new { success = false, message = "No data returned." });

                var first = resultList.FirstOrDefault();

                if (first.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = first.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomer([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.CustomerResults
                    .FromSqlRaw("EXEC dbo.Get_Customer @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result == null)
                    return BadRequest(new { success = false, message = "No data returned." });

                if (result.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = result.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = result.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetWhs([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.WhsResults
                    .FromSqlRaw("EXEC dbo.Get_Whs @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var first = resultList.FirstOrDefault();

                if (first == null)
                    return BadRequest(new { success = false, message = "No data returned." });

                if (first.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = first.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetUoM([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.UoMResults
                    .FromSqlRaw("EXEC dbo.Get_UoM @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var first = resultList.FirstOrDefault();

                if (first == null)
                    return BadRequest(new { success = false, message = "No data returned." });

                if (first.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = first.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetUoMGroup([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.UoMGroupResults
                    .FromSqlRaw("EXEC dbo.Get_UoMGroup @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var first = resultList.FirstOrDefault();

                if (first == null)
                    return BadRequest(new { success = false, message = "No data returned." });

                if (first.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = first.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetPriceList([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.PriceListResults
                    .FromSqlRaw("EXEC dbo.Get_PriceList @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var first = resultList.FirstOrDefault();

                if (first == null)
                    return BadRequest(new { success = false, message = "No data returned." });

                if (first.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = first.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetItemPricing([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.ItemPricingResults
                    .FromSqlRaw("EXEC dbo.Get_ItemPricing @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var first = resultList.FirstOrDefault();

                if (first == null)
                    return BadRequest(new { success = false, message = "No data returned." });

                if (first.Code != 200)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }


                return Ok(new
                {
                    success = true,
                    message = first.Message,
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> GetVisitPlan([FromBody] LoginRequest model)
        {
            try
            {
                var hashedPassword = PasswordHasher.HashPassword(model.Password);

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.UserCode,
                    PasswordHash = hashedPassword,
                    model.DeviceID
                });

                var resultList = await _db.VisitPlanResults
                    .FromSqlRaw("EXEC dbo.Get_Vistplan @JsonBody",
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var first = resultList.FirstOrDefault();

                if (first == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "No data returned."
                    });
                }

                // 🔥 Handle Unauthorized (from SP)
                if (first.Code == 401)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = first.Message
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Success",
                    total = resultList.Count,
                    data = resultList
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        // API take data from web database 
        [HttpGet("SORead")]
        public async Task<IActionResult> GetSOHeader(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] string? docStatus,
            [FromQuery] string? cardCode,
            [FromQuery] string? cardName,
            [FromQuery] string? salesCode,
            [FromQuery] string? sapSyncStatus,
            [FromQuery] string? source,
            [FromQuery] string? saleType,
            [FromQuery] string? appStatus,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // ✅ Build WHERE conditions dynamically
                var conditions = new List<string> { "1=1" };
                var parameters = new List<SqlParameter>();

                if (fromDate.HasValue)
                {
                    conditions.Add("DocDate >= @fromDate");
                    parameters.Add(new SqlParameter("@fromDate", fromDate.Value));
                }

                if (toDate.HasValue)
                {
                    conditions.Add("DocDate <= @toDate");
                    parameters.Add(new SqlParameter("@toDate", toDate.Value));
                }

                if (!string.IsNullOrEmpty(docStatus))
                {
                    conditions.Add("DocStatus = @docStatus");
                    parameters.Add(new SqlParameter("@docStatus", docStatus));
                }

                if (!string.IsNullOrEmpty(cardCode))
                {
                    conditions.Add("CardCode = @cardCode");
                    parameters.Add(new SqlParameter("@cardCode", cardCode));
                }

                if (!string.IsNullOrEmpty(cardName))
                {
                    conditions.Add("CardName LIKE @cardName");
                    parameters.Add(new SqlParameter("@cardName", $"%{cardName}%"));
                }

                if (!string.IsNullOrEmpty(salesCode))
                {
                    conditions.Add("SalesCode = @salesCode");
                    parameters.Add(new SqlParameter("@salesCode", salesCode));
                }

                if (!string.IsNullOrEmpty(sapSyncStatus))
                {
                    conditions.Add("SAPSyncStatus = @sapSyncStatus");
                    parameters.Add(new SqlParameter("@sapSyncStatus", sapSyncStatus));
                }

                if (!string.IsNullOrEmpty(source))
                {
                    conditions.Add("Source = @source");
                    parameters.Add(new SqlParameter("@source", source));
                }

                if (!string.IsNullOrEmpty(saleType))
                {
                    conditions.Add("SaleType = @saleType");
                    parameters.Add(new SqlParameter("@saleType", saleType));
                }

                if (!string.IsNullOrEmpty(appStatus))
                {
                    conditions.Add("AppStatus = @appStatus");
                    parameters.Add(new SqlParameter("@appStatus", appStatus));
                }

                var whereClause = string.Join(" AND ", conditions);

                // ✅ Count total records
                var countSql = $"SELECT COUNT(*) FROM SO WHERE {whereClause}";
                var totalRecords = 0;

                await using (var countCmd = _db.Database.GetDbConnection().CreateCommand())
                {
                    countCmd.CommandText = countSql;
                    countCmd.Parameters.AddRange(parameters
                        .Select(p => new SqlParameter(p.ParameterName, p.Value))
                        .ToArray());

                    await _db.Database.OpenConnectionAsync();
                    totalRecords = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
                }

                // ✅ Fetch paginated data
                var offset = (pageNumber - 1) * pageSize;
                var dataSql = $@"
                    SELECT * FROM SO
                    WHERE {whereClause}
                    ORDER BY CreatedDate DESC
                    OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";

                var data = await _db.Database
                    .SqlQueryRaw<SO_Response>(dataSql, parameters.ToArray())
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    totalRecords,
                    pageNumber,
                    pageSize,
                    totalPages = (int)Math.Ceiling((double)totalRecords / pageSize),
                    data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Failed to retrieve SO Header: {ex.Message}"
                });
            }
        }

        // ============================================================
        // ENDPOINT 2: GET SO1 ROWS BY DocEntry
        // GET: /api/DMS_/SORows/5
        // ============================================================
        [HttpGet("SORows/{docEntry}")]
        public async Task<IActionResult> GetSORows(int docEntry)
        {
            try
            {
                // ✅ Check SO exists
                var checkSql = "SELECT COUNT(*) FROM SO WHERE DocEntry = @docEntry";
                var checkParam = new SqlParameter("@docEntry", docEntry);

                await using (var cmd = _db.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = checkSql;
                    cmd.Parameters.Add(new SqlParameter("@docEntry", docEntry));
                    await _db.Database.OpenConnectionAsync();
                    var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        return NotFound(new
                        {
                            success = false,
                            message = $"SO with DocEntry {docEntry} not found."
                        });
                    }
                }

                // ✅ Fetch SO1 rows
                var sql = "SELECT * FROM SO1 WHERE DocEntry = @docEntry ORDER BY LineNum";
                var rows = await _db.Database
                    .SqlQueryRaw<SO1_Response>(sql, new SqlParameter("@docEntry", docEntry))
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    docEntry,
                    totalLines = rows.Count,
                    data = rows
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Failed to retrieve SO Rows: {ex.Message}"
                });
            }
        }

        
        // PUT: /api/DMS_/SOStatus/5
       
        [HttpPut("SOStatus/{docEntry}")]
        public async Task<IActionResult> UpdateSOStatus(
            int docEntry,
            [FromBody] UpdateSOStatusRequest request)
        {
            try
            {
                // ✅ Validate AppStatus
                var allowedStatuses = new[] { "P", "A", "R" };
                if (!allowedStatuses.Contains(request.AppStatus))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid AppStatus. Allowed values: 'Pending', 'Approved', 'Rejected'"
                    });
                }

                // ✅ Update using raw SQL
                var sql = @"UPDATE SO 
                            SET AppStatus = @appStatus,
                                UpdateDate = @updateDate,
                                Remark = CASE WHEN @remark IS NULL THEN Remark ELSE @remark END
                            WHERE DocEntry = @docEntry";

                var rowsAffected = await _db.Database.ExecuteSqlRawAsync(sql,
                    new SqlParameter("@appStatus", request.AppStatus),
                    new SqlParameter("@updateDate", DateTime.Now),
                    new SqlParameter("@remark", (object?)request.Remark ?? DBNull.Value),
                    new SqlParameter("@docEntry", docEntry));

                if (rowsAffected == 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"SO with DocEntry {docEntry} not found."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = $"SO DocEntry {docEntry} AppStatus updated to '{request.AppStatus}' successfully.",
                    data = new
                    {
                        DocEntry = docEntry,
                        AppStatus = request.AppStatus,
                        UpdateDate = DateTime.Now,
                        Remark = request.Remark
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Failed to update SO AppStatus: {ex.Message}"
                });
            }
        }
    }



}




