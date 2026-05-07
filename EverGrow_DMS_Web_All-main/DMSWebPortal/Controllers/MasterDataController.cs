using DMSWebPortal.Models;
//ing DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DMSWebPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {

        private readonly AppDbContext _db;

        public MasterDataController(AppDbContext db)
        {
            _db = db;
        }

        // ----------------------------
        // Add or Update Item with optional image upload
        // Endpoint: POST /api/masterdata/AddOrUpdateItem
        // ----------------------------
        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)] // Limit to 10 MB
        public async Task<IActionResult> AddOrUpdateItem([FromForm] Item model, IFormFile imageFile)
        {
            try
            {
                var token = new { UserId = 1, UserName = "Admin", CompanyId = 1001 };
                string mode = string.IsNullOrEmpty(model.ItemCode) ? "Add" : "Update";

                // --------------------
                // Save uploaded image file
                // --------------------
                if (imageFile != null && imageFile.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var filePath = Path.Combine(folderPath, $"{model.ItemCode}{Path.GetExtension(imageFile.FileName)}");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    model.ImageUrlServer = $"/Images/{model.ItemCode}{Path.GetExtension(imageFile.FileName)}";
                }

                // --------------------
                // Build JSON for SP
                // --------------------
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.Mode,
                    model.ItemCode,
                    model.ItemName,
                    model.ItemGroupCode,
                    model.ItemGroupName,
                    model.UgpEntry,
                    model.Onhand,
                    model.OnOrder,
                    model.IsCommited,
                    model.Available,
                    model.MinLevel,
                    model.MaxLevel,
                    model.Status,
                    model.ImageUrlServer,
                    model.ImageUrlLocal,
                    model.FrgnName,
                    model.InvUoMCode,
                    model.InvUoMEntry,
                    model.UpdatedDate,
                    model.OcrCode,
                    model.OcrCode2,
                    model.OcrCode3,
                    model.OcrCode4,
                    model.Manufacturer,
                    model.ManufacturerDes,
                    model.SubGroup,
                    model.SubGroupDes,
                    model.ItemBrand,
                    model.ItemBrandDes,
                    model.ItemType,
                    model.ItemTypeDes,
                    model.ProteinType,
                    model.ProteinTypeDes,
                    model.SubGroup2,
                    model.SubGroup2Des,
                    model.Factory,
                    model.FactoryDes,
                    model.BarCode,
                    model.DefEntry,
                    model.AltQty,
                    model.SellingPrice,
                    CreateBy = token.UserId,
                    UpdateBy = token.UserId
                });

                // --------------------
                // Call Stored Procedure
                // --------------------
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerItem @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "Item"),
                        new SqlParameter("@TranType", model.Mode),
                        new SqlParameter("@EntryPrimary", model.ItemCode ?? ""),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                    return Ok(new { success = true, message = result.Message, primaryKey = result.PrimaryKey });

                return BadRequest(new { success = false, message = result?.Message ?? "Error processing Item" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = $"Operation failed: {ex.Message}" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCustomer([FromBody] Customer model)
        {
            try
            {
                // Temporary user token (replace later with JWT or real user context)
                var token = new { UserId = 1, UserName = "Admin" };

                string mode = model.Mode ?? "Add"; // Optional Mode property for Add/Update

                // ✅ Build JSON for SP
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.CardCode,
                    model.CardName,
                    model.CardFName,
                    model.GroupCode,
                    model.GroupName,
                    model.ID,
                    model.Tel1,
                    model.Tel2,
                    model.Mobile,
                    model.ContactPerson,
                    model.ContactPersonName,
                    model.FullAddress,
                    model.Paymenterm,
                    model.PriceList,
                    model.CreditLimit,
                    CreateBy = token.UserId,
                    UpdateBy = token.UserId
                });

                // Call stored procedure
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerCustomer @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "Customer"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.CardCode ?? ""),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey // ✅ Return generated CardCode
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing Customer"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateWhs([FromBody] Whs model)
        {
            try
            {
                // Temporary user token (replace later with JWT or real user context)
                var token = new { UserId = 1, UserName = "Admin" };

                string mode = model.Mode ?? "Add"; // Mode = Add or Update

                // ✅ Build JSON for Stored Procedure
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.WhsCode,
                    model.WhsName,
                    model.WhsStatus,
                    model.Shows
                });

                // Call Stored Procedure
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerWhs @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "Whs"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.WhsCode ?? ""),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey // Return WhsCode
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing Warehouse"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdatePriceList([FromBody] PriceList model)
        {
            try
            {
                // Temporary user token (replace later with JWT or real user context)
                var token = new { UserId = 1, UserName = "Admin" };

                string mode = model.Mode ?? "Add";

                // Build JSON for Stored Procedure
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.ListNum,
                    model.ListName,
                    model.Status
                });

                // Execute Stored Procedure
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerPriceList @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "PriceList"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.ListNum),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing Price List"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateItemPricing([FromBody] ItemPricing model)
        {
            try
            {
                var token = new { UserId = 1, UserName = "Admin" };

                string mode = model.Mode ?? "Add";

                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.ItemCode,
                    model.PriceListCode,
                    model.UoMEntry,
                    model.Amount,
                    model.Status
                });

                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerItemPricing @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "ItemPricing"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.ItemCode ?? ""),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing Item Pricing"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdatePromotionV2([FromBody] PromotionHeaderV2 model)
        {
            try
            {
                var token = new { UserId = 1, UserName = "Admin" }; // Replace with real JWT user
                string mode = model.Mode ?? "Add";

                // ✅ Build JSON for Stored Procedure
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.Code,
                    model.Name,
                    model.DocEntry,
                    model.Canceled,
                    model.Object,
                    model.LogInst,
                    model.UserSign,
                    model.Transfered,
                    model.CreateDate,
                    model.CreateTime,
                    model.UpdateDate,
                    model.UpdateTime,
                    model.DataSource,
                    model.U_RefNo,
                    model.U_FromDate,
                    model.U_ToDate,
                    model.U_CardCode,
                    model.U_CardName,
                    model.U_PromotionType,
                    model.U_ApplyType,
                    model.U_RemarkCode,
                    model.U_Remark,
                    model.U_ItemGroup,
                    model.U_Status,
                    model.U_Attactment,
                    CreateBy = token.UserId,
                    UpdateBy = token.UserId,

                    // Promotion Detail Rows
                    Rows = model.PromotionRows?.Select(r => new
                    {
                        r.Code,
                        r.LineId,
                        r.Object,
                        r.LogInst,
                        r.U_LevelType,
                        r.U_UOMType,
                        r.U_Code,
                        r.U_Description,
                        r.U_StartQty,
                        r.U_FreeQty,
                        r.U_PromotionPercent,
                        r.U_PromotionAmount,
                        r.U_TransportationPercent,
                        r.U_TransportionAmount,
                        r.U_TransportationAmountKH
                    })
                });

                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerPromotionV2 @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "PromotionV2"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.Code ?? ""),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey // ✅ Return generated Code
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing Promotion"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateUoM([FromBody] tblUoM model)
        {
            try
            {
                // Temporary user token (replace later with JWT or real user context)
                var token = new { UserId = 1, UserName = "Admin" };

                string mode = model.Mode ?? "Add"; // Mode = Add or Update

                // ✅ Build JSON for Stored Procedure
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.UoMEntry,
                    model.UoMCode
                });

                // Call Stored Procedure
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerUoM @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "UoM"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.UoMEntry.ToString()),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey // Return UoMEntry
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing UoM"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateUoMGroup([FromBody] tblUoMGroup model)
        {
            try
            {
                // Temporary user token (replace later with JWT or real user context)
                var token = new { UserId = 1, UserName = "Admin" };

                string mode = model.Mode ?? "Add"; // Mode = Add or Update

                // ✅ Build JSON for Stored Procedure
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.UgpEntry,
                    model.UoMEntry,
                    model.UgpName,
                    model.UoMCode,
                    model.BaseQty,
                    model.AltQty,
                    model.Status
                });

                // Call Stored Procedure
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerUoMGroup @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "UoMGroup"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.UgpEntry.ToString()),
                        new SqlParameter("@JsonBody", jsonBody))
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey // Return UgpEntry
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing UoM Group"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                using var sha = System.Security.Cryptography.SHA256.Create();
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateUser([FromBody] User model)
        {
            // Temporary user token (replace later with JWT or real user context)
            var token = new { UserId = 1, UserName = "Admin" };

            try
            {
                string mode = model.Mode ?? "Add";

                model.CreatedBy = token.UserId.ToString();
                model.UpdatedBy = token.UserId.ToString();

                // Hash password only when provided
                if (!string.IsNullOrEmpty(model.Password))
                {
                    model.Password = PasswordHasher.HashPassword(model.Password);
                }

                // Build JSON body for stored procedure
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.Code,
                    model.Name,
                    model.Email,
                    model.Password,
                    model.Profile,
                    model.CompanyName,
                    model.UserType,
                    model.Status,
                    model.SlpCode,
                    model.IsWebUser,
                    model.IsEndofDay,
                    model.Manager,
                    model.DeviceID,
                    model.PrinterName,
                    model.PrinterMac,
                    model.CreatedBy,
                    model.UpdatedBy
                });

                // Execute Stored Procedure
                var resultList = await _db.Set<SpResult>()
                .FromSqlRaw(
                    "EXEC ControllerUsers @MasterType,@TranType,@EntryPrimary,@JsonBody",
                    new SqlParameter("@MasterType", "Users"),
                    new SqlParameter("@TranType", mode),
                    new SqlParameter("@EntryPrimary", model.Code ?? ""),
                    new SqlParameter("@JsonBody", jsonBody)
                )
                .AsNoTracking()
                .ToListAsync();

                var result = resultList.FirstOrDefault();

                if (result != null && result.Code == 200)
                {
                    return Ok(new
                    {
                        success = true,
                        message = result.Message,
                        primaryKey = result.PrimaryKey
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing user."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = $"Operation failed: {ex.Message}"
                });
            }
        }

    }
}
