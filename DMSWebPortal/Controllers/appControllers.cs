using System.Data;
using System.IdentityModel.Tokens;
using DMSWebPortal.DTOs;
using DMSWebPortal.Models;
using DMSWebPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//using Microsoft.Build.Framework;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using Swashbuckle.AspNetCore.Annotations;


namespace DMSWebPortal.Controllers
{
    [Route("api/app")]   // <-- base route: api/actdb
    [ApiController]
    [Authorize] // Secure all methods in this controller
    public class appControllers : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly RequestLogService requestLog;
        //private readonly NotificationService _notificationService;
        public appControllers(AppDbContext context, IConfiguration configuration, RequestLogService requestLog)
        {
            _context = context;
            _configuration = configuration;
            this.requestLog = requestLog;
            //_notificationService = notificationService;
        }
        // GET: api/Login?uSalesCode={uSalesCode}&uSecret={uSecret}
        [AllowAnonymous]
        [HttpGet("Login")]
        [SwaggerOperation(
            Summary = "Login API",
            Description = "Authenticate user and return JWT access + refresh token."
        )]
        public async Task<ActionResult<AuthResponseDto>> GetLoginInfo(
            [FromQuery] string uSalesCode,
            [FromQuery] string uSecret,
            [FromQuery] string uDeviceID,
            [FromServices] JwtTokenService jwtService)
        {
            LoginDto login = null;
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            EncryptDecrypt en = new EncryptDecrypt();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ICC_API_02_Login", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@U_SalesCode", uSalesCode);
                cmd.Parameters.AddWithValue("@U_Secret", en.Encrypt(uSecret));
                cmd.Parameters.AddWithValue("@DeviceID", uDeviceID);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        login = new LoginDto
                        {
                            SlpCode = Convert.ToInt32(reader["slpCode"]),
                            SalesName = reader["salesName"]?.ToString(),
                            Telephone = reader["telephone"]?.ToString(),
                            USalesCode = reader["uSalesCode"]?.ToString(),
                            UWhs = reader["uWhs"]?.ToString(),
                            Email = reader["email"]?.ToString(),
                            Region = reader["region"]?.ToString(),
                            Sm = reader["sm"]?.ToString(),
                            Rsm = reader["rsm"]?.ToString(),
                            TaxCode = reader["taxCode"]?.ToString(),
                            NonTaxPre = reader["nonTaxPre"]?.ToString(),
                            TaxPre = reader["taxPre"]?.ToString(),
                            exchangeRate = Convert.ToDouble(reader["ExchangeRate"]?.ToString()),
                            SalesType = reader["SalesType"]?.ToString(),
                            AutoSync = reader["AutoSync"].ToString(),
                            //LastEndDay = Convert.ToDateTime(reader["LastEndDay"].ToString()).ToString("dd-MMM-yyyy"),
                            LastEndDay = DateTime.TryParse(reader["LastEndDay"]?.ToString(), out DateTime lastEnd)? lastEnd.ToString("dd-MMM-yyyy"): "",
                            printername = reader["printername"].ToString(),
                            macaddress = reader["macaddress"].ToString(),
                            IsAllPrinciple = reader["IsAllPrinciple"].ToString(),
                            IsTax = reader["IsTax"].ToString(),
                            IsEndofDay = reader["IsEndofDay"].ToString()
                        };
                    }
                }
            }

            if (login == null)
                return Unauthorized("Invalid user ID or password.");

            // 🔐 Generate tokens
            var accessToken = jwtService.GenerateAccessToken(login.USalesCode);
            var refreshToken = jwtService.GenerateRefreshToken(login.USalesCode);

            // ✅ Return response (NO CACHE)
            return Ok(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = null
            });
        }
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public ActionResult<AuthResponseDto> RefreshToken([FromBody] RefreshTokenRequestDto request,
                                                  [FromServices] JwtTokenService jwtService)
        {
            if (string.IsNullOrWhiteSpace(request.RefreshToken))
                return BadRequest(new { error = "Refresh token is required" });

            try
            {
                // validate refresh token
                var principal = jwtService.GetPrincipalFromExpiredToken(request.RefreshToken, isRefresh: true);
                var userId = principal.Identity?.Name;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized(new { error = "Invalid refresh token" });

                // ensure token type
                var typeClaim = principal.Claims.FirstOrDefault(c => c.Type == "type")?.Value;
                if (typeClaim != "refresh")
                    return Unauthorized(new { error = "Invalid token type" });

                // generate new tokens
                var newAccessToken = jwtService.GenerateAccessToken(userId);
                var newRefreshToken = jwtService.GenerateRefreshToken(userId);

                return Ok(new AuthResponseDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (SecurityTokenException)
            {
                return Unauthorized(new { error = "Invalid refresh token" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error refreshing token", details = ex.Message });
            }
        }
        [HttpGet("ItemWhsPrice")]
        [SwaggerOperation(
            Summary = "Get Whs and Price",
            Description = "This endpoint is used to get warehouse and price."
        )]
        public async Task<ActionResult<IEnumerable<tblItemWhsPricing>>> GetItemWhsPrice()
        {
            try
            {
                var item = await _context.tblItemWhsPricings
                    .FromSqlRaw("EXEC dbo.ICC_API_31_Item_Whsw_Price")
                    .ToListAsync();
                if (item == null || item.Count == 0)
                {
                    return NotFound("No Item Warehouse price found.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving ProTypes", details = ex.Message });
            }
        }
        // GET: api/ItemList/{U_SalesCode}
        [HttpGet("ItemList/{U_SalesCode}")]
        [SwaggerOperation(
            Summary = "Get Item List By Sales code",
            Description = "This endpoint is used to get item list by sales code"
        )]
        public async Task<ActionResult> GetItemsBySalesCode(string U_SalesCode)
        {
            if (string.IsNullOrWhiteSpace(U_SalesCode))
            {
                return BadRequest(new { error = "U_SalesCode is required." });
            }

            var Items = await _context.tblItems
                .FromSqlRaw("EXEC dbo.ICC_API_01_Get_ItemList_By_U_SalesCode @U_SalesCode = {0}", U_SalesCode)
                .ToListAsync();

            if (Items == null || !Items.Any())
                return NotFound(new { message = $"No items found for U_SalesCode '{U_SalesCode}'." });
            return Ok(Items);
        }

        // GET: /api/Whs
        [HttpGet("Whs")]
        [SwaggerOperation(
            Summary = "Get Whs List",
            Description = "This endpoint is used to get warehouse list."
        )]
        public async Task<ActionResult<IEnumerable<tblWh>>> GetWhs()
        {
            try
            {
                var whs = await _context.tblWhs
                    .FromSqlRaw("EXEC dbo.ICC_API_29_GET_Whs")
                    .ToListAsync();

                if (whs == null || whs.Count == 0)
                {
                    return NotFound("No Whs Found");
                }

                return Ok(whs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving Whs", details = ex.Message });
            }
        }

        // GET: api/ItemStock/{U_SalesCode}
        [HttpGet("ItemStock/{U_SalesCode}")]
        [SwaggerOperation(
            Summary = "Get Item stock by Sales code",
            Description = "This endpoint is used to get item stock by sales code."
        )]
        public async Task<ActionResult> GetItemStockBySalesCode(string U_SalesCode)
        {
            if (string.IsNullOrWhiteSpace(U_SalesCode))
            {
                return BadRequest(new { error = "U_SalesCode is required." });
            }

            var Items = await _context.ItemStockResponses
                .FromSqlRaw("EXEC dbo.ICC_API_30_GET_Item_Stock @U_SalesCode = {0}", U_SalesCode)
                .ToListAsync();

            if (Items == null || !Items.Any())
                return NotFound(new { message = $"No ItemStock found for U_SalesCode '{U_SalesCode}'." });
            return Ok(Items);
        }

        // GET: /api/Image?itemCode={itemCode}&cardCode={cardCode}

        // GET: api/CustomerList/{U_SalesCode}
        [HttpGet("CustomerList/{uSalesCode}")]
        [SwaggerOperation(
            Summary = "Customer List",
            Description = "This endpoint is used to get customer list by sales code."
        )]
        public async Task<IActionResult> GetCustomerList(string uSalesCode)
        {
            if (string.IsNullOrWhiteSpace(uSalesCode))
                return BadRequest(new { error = "U_SalesCode is required." });

            var customers = await _context.tblBPs
                .FromSqlRaw("EXEC dbo.ICC_API_04_Get_CustomerList_By_U_SalesCode @U_SalesCode = {0}", uSalesCode)
                .ToListAsync();

            if (customers == null || !customers.Any())
                return NotFound(new { message = $"No customers found for U_SalesCode '{uSalesCode}'." });

            return Ok(customers);
        }

        // GET: /api/AddressList
        [HttpGet("AddressList")]
        [SwaggerOperation(
            Summary = "Address List",
            Description = "This endpoint is used to get address list."
        )]
        public async Task<ActionResult<IEnumerable<tblAddress>>> GetAddresses()
        {
            try
            {
                var addresses = await _context.tblAddresses
                    .FromSqlRaw("EXEC dbo.ICC_API_05_Get_AddressList")
                    .ToListAsync();

                if (addresses == null || addresses.Count == 0)
                {
                    return NotFound("No addresses found.");
                }

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving address list.", details = ex.Message });
            }
        }


        // GET: /api/ProType
        [HttpGet("ProType")]
        public async Task<ActionResult<IEnumerable<tblProType>>> GetProType()
        {
            try
            {
                var proTypes = await _context.tblProTypes
                    .FromSqlRaw("EXEC dbo.ICC_API_22_GET_ProType")
                    .ToListAsync();

                if (proTypes == null || proTypes.Count == 0)
                {
                    return NotFound("No ProTypes found.");
                }

                return Ok(proTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving ProTypes", details = ex.Message });
            }
        }
        // GET: /api/Zone
        [HttpGet("Zone")]
        [SwaggerOperation(
            Summary = "Zone List",
            Description = "This endpoint is used to get zone list."
        )]
        public async Task<ActionResult<IEnumerable<tblZone>>> GetZone()
        {
            try
            {
                var zone = await _context.tblZones
                    .FromSqlRaw("EXEC dbo.ICC_API_33_GET_Zone")
                    .ToListAsync();

                if (zone == null || zone.Count == 0)
                {
                    return NotFound("No Zone found.");
                }

                return Ok(zone);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving Zone", details = ex.Message });
            }
        }
        // GET: /api/Zone
        [HttpGet("SubZone")]
        [SwaggerOperation(
            Summary = "Get Sub Zone",
            Description = "This endpoint is used to get sub zone."
        )]
        public async Task<ActionResult<IEnumerable<tblSubZone>>> GetSubZone()
        {
            try
            {
                var zone = await _context.tblSubZones
                    .FromSqlRaw("EXEC dbo.ICC_API_32_GET_SubZone")
                    .ToListAsync();

                if (zone == null || zone.Count == 0)
                {
                    return NotFound("No Sub Zone found.");
                }

                return Ok(zone);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving Sub Zone", details = ex.Message });
            }
        }
        // GET: /api/ProCal
        [HttpGet("ProCal")]
        public async Task<ActionResult<IEnumerable<v_promocal>>> GetPromotionCal(string cardcode, string docdate, string sellitem)
        {
            try
            {
                var prolist = await _context.v_RunPromotionResults
                    .FromSqlRaw("EXEC ICC_Get_PromotionDetails '" + cardcode + "','" + docdate + "','" + sellitem + "'")
                    .ToListAsync();

                if (prolist == null || prolist.Count == 0)
                {
                    return NotFound("No Promotion Found");
                }

                return Ok(prolist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving promotion calculation", details = ex.Message });
            }
        }

        // GET: /api/Promotion
        [HttpGet("Promotion")]
        public async Task<ActionResult<IEnumerable<tblPromotion>>> GetPromotion()
        {
            try
            {
                var promotions = await _context.tblPromotions
                    .FromSqlRaw("EXEC dbo.ICC_API_23_GET_Promotion")
                    .ToListAsync();

                if (promotions == null || promotions.Count == 0)
                {
                    return NotFound("No Promotion Found");
                }

                return Ok(promotions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving Promotions", details = ex.Message });
            }
        }


        // GET: /api/Promotion1
        [HttpGet("Promotion1")]
        public async Task<ActionResult<IEnumerable<tblPromotion1>>> GetPromotion1()
        {
            try
            {
                var promotions1 = await _context.tblPromotion1s
                    .FromSqlRaw("EXEC dbo.ICC_API_24_GET_Promotion1")
                    .ToListAsync();

                if (promotions1 == null || promotions1.Count == 0)
                {
                    return NotFound("No Promotion1 Found");
                }

                return Ok(promotions1);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving Promotion1", details = ex.Message });
            }
        }


        // GET: /api/ProMonthly
        [HttpGet("ProMonthly")]
        public async Task<ActionResult<IEnumerable<tblProMonthly>>> GetProMonthly()
        {
            try
            {
                var promonthly = await _context.tblProMonthlies
                    .FromSqlRaw("EXEC dbo.ICC_API_25_GET_ProMonthly")
                    .ToListAsync();

                if (promonthly == null || promonthly.Count == 0)
                {
                    return NotFound("No promonthly Found");
                }

                return Ok(promonthly);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving ProMonthly", details = ex.Message });
            }
        }

        // GET: /api/ProFOCType
        [HttpGet("ProFOCType")]
        public async Task<ActionResult<IEnumerable<tblProFOCType>>> GetProFOCType()
        {
            try
            {
                var proFOCType = await _context.tblProFOCTypes
                    .FromSqlRaw("EXEC dbo.ICC_API_26_GET_ProFOCType")
                    .ToListAsync();

                if (proFOCType == null || proFOCType.Count == 0)
                {
                    return NotFound("No ProFOCType Found");
                }

                return Ok(proFOCType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving ProFOCType", details = ex.Message });
            }
        }

        // GET: /api/ProCondition
        [HttpGet("ProCondition")]
        public async Task<ActionResult<IEnumerable<tblProCondition>>> GetProCondition()
        {
            try
            {
                var procondition = await _context.tblProConditions
                    .FromSqlRaw("EXEC dbo.ICC_API_27_GET_ProCondition")
                    .ToListAsync();

                if (procondition == null || procondition.Count == 0)
                {
                    return NotFound("No procondition Found");
                }

                return Ok(procondition);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving procondition", details = ex.Message });
            }
        }


        //GET : /api/Principle
        [HttpGet("Principle")]
        public async Task<ActionResult<IEnumerable<tblPrinciple>>> GetPrinciple()
        {
            try
            {
                var principle = await _context.tblPrinciples
                    .FromSqlRaw("EXEC dbo.ICC_API_28_GET_Principle")
                    .ToListAsync();

                if (principle == null || principle.Count == 0)
                {
                    return NotFound("No principle Found");
                }

                return Ok(principle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving procondition", details = ex.Message });
            }
        }



        // GET: /api/BankLists
        [HttpGet("BankLists")]
        public async Task<ActionResult<IEnumerable<tblBank>>> GetBankLists()
        {
            try
            {
                var bank = await _context.tblBanks
                    .FromSqlRaw("EXEC dbo.ICC_API_21_GET_BankLists")
                    .ToListAsync();

                if (bank == null || bank.Count == 0)
                {
                    return NotFound("No bank found");
                }
                return Ok(bank);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving bank list.", details = ex.Message });
            }
        }

        // GET: /api/PaymentTermList
        [HttpGet("PaymentTermList")]
        public async Task<ActionResult<IEnumerable<tblPayment>>> GetPaymentTerms()
        {
            var paymentTerms = new List<tblPayment>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ICC_API_06_Get_Payment_Term_List", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var term = new tblPayment
                        {
                            TermCode = Convert.ToInt16(reader["TermCode"]),
                            TermName = reader["TermName"]?.ToString(),
                            AddMonth = reader["AddMonth"] != DBNull.Value ? Convert.ToInt16(reader["AddMonth"]) : null,
                            AddDay = reader["AddDay"] != DBNull.Value ? Convert.ToInt16(reader["AddDay"]) : null,
                            Status = reader["Status"]?.ToString()
                        };
                        paymentTerms.Add(term);
                    }
                }
            }

            if (!paymentTerms.Any())
            {
                return NotFound("No payment terms found.");
            }

            return Ok(paymentTerms);
        }

        // GET: /api/CustomerGroupList
        [HttpGet("CustomerGroupList")]
        public async Task<ActionResult<IEnumerable<tblBPGroup>>> GetCustomerGroups()
        {
            var groups = new List<tblBPGroup>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ICC_API_07_Get_Customer_Group_List", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var group = new tblBPGroup
                        {
                            GroupCode = Convert.ToInt16(reader["GroupCode"]),
                            GroupName = reader["GroupName"]?.ToString(),
                            Status = reader["Status"]?.ToString()
                        };
                        groups.Add(group);
                    }
                }
            }

            if (!groups.Any())
            {
                return NotFound("No customer groups found.");
            }

            return Ok(groups);
        }

        // GET: /api/ChannelList
        [HttpGet("ChannelList")]
        public async Task<ActionResult<IEnumerable<tblBPChannel>>> GetChannels()
        {
            var channels = new List<tblBPChannel>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ICC_API_08_Get_Channel_List", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var channel = new tblBPChannel
                        {
                            Code = reader["Code"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            Status = reader["Status"]?.ToString()
                        };
                        channels.Add(channel);
                    }
                }
            }

            if (!channels.Any())
            {
                return NotFound("No channels found.");
            }

            return Ok(channels);
        }

        [HttpGet("VisitPlans/{uSalesCode}")]
        public async Task<ActionResult<IEnumerable<VisitPlanDto_App>>> GetVisitPlansByUSalesCode(string uSalesCode)
        {
            var visitPlans = new List<VisitPlanDto_App>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ICC_API_09_Get_Visit_Plan_By_U_SalesCode", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@U_SalesCode", uSalesCode);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        visitPlans.Add(new VisitPlanDto_App
                        {

                            DocEntry = reader["DocEntry"] != DBNull.Value ? Convert.ToInt32(reader["DocEntry"]) : 0,
                            Status = reader["Status"]?.ToString(),
                            VisitDate = reader["VisitDate"] != DBNull.Value ? Convert.ToDateTime(reader["VisitDate"]) : DateTime.MinValue,
                            CardCode = reader["CardCode"]?.ToString(),
                            USalesCode = reader["U_SalesCode"]?.ToString()
                        });
                    }
                }
            }

            if (!visitPlans.Any())
                return NotFound("No visit plans found for the provided U_SalesCode.");

            return Ok(visitPlans);
        }

        // GET: /api/CallPlans/{uSalesCode}
        [HttpGet("CallPlans/{uSalesCode}")]
        public async Task<ActionResult<IEnumerable<CallPlanDto>>> GetCallPlansByUSalesCode(string uSalesCode)
        {
            var callPlans = new List<CallPlanDto>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("ICC_API_09_Get_Call_Plan_By_U_SalesCode", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@U_SalesCode", uSalesCode);

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        callPlans.Add(new CallPlanDto
                        {
                            DocEntry = reader["DocEntry"] != DBNull.Value ? Convert.ToInt32(reader["DocEntry"]) : 0,
                            Status = reader["Status"]?.ToString(),
                            VisitDate = reader["VisitDate"] != DBNull.Value ? Convert.ToDateTime(reader["VisitDate"]) : DateTime.MinValue,
                            CardCode = reader["CardCode"]?.ToString(),
                            USalesCode = reader["U_SalesCode"]?.ToString()
                        });
                    }
                }
            }

            if (!callPlans.Any())
                return NotFound("No visit plans found for the provided U_SalesCode.");

            return Ok(callPlans);
        }


        // GET: /api/ItemPricing
        [HttpGet("ItemPricing")]
        [SwaggerOperation(
            Summary = "For Get Item Price",
            Description = "This endpoint is used to get item price."
        )]
        public async Task<ActionResult<IEnumerable<ItemPricingDto>>> GetItemPricing()
        {
            var data = await _context.tblItemPricings
                .FromSqlRaw("EXEC [dbo].[ICC_API_16_GET_ItemPricing]")
                .ToListAsync();

            return Ok(data);
        }

        // GET: /api/UoMGroup
        [HttpGet("UoMGroup")]
        [SwaggerOperation(
            Summary = "For get UoM Group",
            Description = "This endpoint is used to get UoM Group."
        )]
        public async Task<ActionResult<IEnumerable<tblUoMGroup>>> GetUoMGroup()
        {
            var uomGroups = await _context.tblUoMGroups
                .FromSqlRaw("EXEC [dbo].[ICC_API_17_GET_UoMGroup]")
                .ToListAsync();
            if (uomGroups == null || !uomGroups.Any())
            {
                return NotFound("No UoM groups found.");
            }
            return Ok(uomGroups);
        }

        // GET: /api/Reason
        [HttpGet("Reason")]
        [SwaggerOperation(
            Summary = "For get Reason",
            Description = "This endpoint is used to get reason."
        )]
        public async Task<ActionResult<IEnumerable<Reason>>> GetReasons()
        {
            var reasons = await _context.Reasons
                .FromSqlRaw("EXEC [dbo].[ICC_API_18_GET_Reason]")
                .ToListAsync();
            if (reasons == null || !reasons.Any())
            {
                return NotFound("No reasons found.");
            }
            return Ok(reasons);
        }

        // GET: /api/Regional
        [HttpGet("Regional")]
        [SwaggerOperation(
            Summary = "For get Regional",
            Description = "This endpoint is used to get regional."
        )]
        public async Task<ActionResult<IEnumerable<tblRegional>>> GetRegionals()
        {
            var regionals = await _context.tblRegionals
                .FromSqlRaw("EXEC [dbo].[ICC_API_20_Get_Regional]")
                .ToListAsync();
            if (regionals == null || !regionals.Any())
            {
                return NotFound("No regionals found.");
            }
            return Ok(regionals);
        }

        // GET: /api/Regional
        [HttpGet("get_Order_Status/{uSalesCode}")]
        [SwaggerOperation(
            Summary = "For get Order Status",
            Description = "This endpoint is used to get order status."
        )]
        public async Task<ActionResult<IEnumerable<v_Order_Status>>> get_Order_Status(string uSalesCode)
        {
            var regionals = await _context.v_Order_StatusResult
                .FromSqlRaw("EXEC ICC_Get_Order_App_Status @p0", uSalesCode)
                .ToListAsync();
            if (regionals == null || !regionals.Any())
            {
                return NotFound("No Order Status.");
            }
            return Ok(regionals);
        }

        [HttpPost("set_Order_Status")]
        public async Task<IActionResult> set_Order_Status([FromBody] List<v_Order_Status> orderstatus)
        {
            if (orderstatus == null || orderstatus.Count == 0)
            {
                return BadRequest("Order status list is empty.");
            }

            foreach (var x in orderstatus)
            {
                if (x.DocEntry == null)
                    continue; // skip if DocEntry missing

                var so = await _context.SOs.FindAsync(x.DocEntry);

                if (so != null)
                {
                    so.AppStatus = "Done";
                }
            }

            await _context.SaveChangesAsync();  // Save once, not inside loop

            return Ok(new
            {
                Message = "Set Order Status successfully.",
                Data = orderstatus
            });
        }


        // POST: /api/BPRequest
        [HttpPost("BPRequest")]
        [SwaggerOperation(
            Summary = "For add and update BP",
            Description = "This endpoint is used to add and update BP request."
        )]
        public async Task<IActionResult> PostBPRequest([FromForm] BPRequestDto dto)
        {
            try
            {
                requestLog.Log<BPRequestDto>("BPRequest", dto.CardName.ToString(), dto);
                if (dto.AppCode == null)
                    return BadRequest("Missing required data (AppCode)");

                var bp = await _context.tblBPRequests
                    .FirstOrDefaultAsync(x => x.DocEntry == dto.BPEntry);

                if (bp == null)
                {
                    bp = new tblBPRequest
                    {
                        BPKey = $"{dto.SalesCode}-{dto.AppCode}",
                        AppCode = dto.AppCode,
                        CardName = dto.CardName,
                        CardFName = dto.CardFName,
                        Phone1 = dto.Phone1,
                        Phone2 = dto.Phone2,
                        Phone3 = dto.Phone3,
                        Email = dto.Email,
                        ProCode = dto.ProCode,
                        DisCode = dto.DisCode,
                        ComCode = dto.ComCode,
                        VilName = dto.VilName,
                        FullAddEN = dto.FullAddEN,
                        FullAddKH = dto.FullAddKH,
                        AddressCode = dto.AddressCode,
                        GPSLateLong = dto.GPSLateLong,
                        CreatedDate = DateTime.Now,
                        Status = "Draft",
                        SalesCode = dto.SalesCode,
                        CreatedBy = "App",
                        VATImage = dto.VATImage,
                    };
                    await _context.tblBPRequests.AddAsync(bp);

                }
                else
                {
                    bp.CardName = dto.CardName;
                    bp.CardFName = dto.CardFName;
                    bp.Phone1 = dto.Phone1;
                    bp.Phone2 = dto.Phone2;
                    bp.Phone3 = dto.Phone3;
                    bp.Email = dto.Email;
                    bp.ProCode = dto.ProCode;
                    bp.DisCode = dto.DisCode;
                    bp.ComCode = dto.ComCode;
                    bp.VilName = dto.VilName;
                    bp.FullAddEN = dto.FullAddEN;
                    bp.FullAddKH = dto.FullAddKH;
                    bp.GPSLateLong = dto.GPSLateLong;
                    bp.UpdatedDate = DateTime.Now;
                    bp.SalesCode = dto.SalesCode;
                    bp.UpdatedBy = "App";
                }

                // 🔹 SAVE IMAGE IF EXISTS
                if (dto.Image != null && dto.Image.Length > 0)
                {
                    var uploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var folder = Path.Combine(uploadRoot, "Attachments", "BP_Request");

                    // Ensure folder exists
                    Directory.CreateDirectory(folder);

                    // Get file extension
                    var extension = Path.GetExtension(dto.Image.FileName);

                    // Unique file name
                    var fileName = $"App_{dto.AppCode}_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}{extension}";
                    var filePath = Path.Combine(folder, fileName);

                    // Save file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await dto.Image.CopyToAsync(stream);
                    }

                    // ✅ Public URL (for mobile/web access)
                    bp.ImagePath = $"/Attachments/BP_Request/{fileName}";
                }

                bp.JsonRemark = System.Text.Json.JsonSerializer.Serialize(dto);
                await _context.SaveChangesAsync();

                //Save to DocEntry Mapping
                DocEntryMapping mapping = new DocEntryMapping()
                {
                    DMSEntry = bp.DocEntry,
                    AppDocEntry = dto.AppCode,
                    SAPEntry = -1,
                    SalesCode = bp.SalesCode,
                    DocType = "BPRequest",
                };
                await _context.DocEntryMappings.AddAsync(mapping);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    status = "OK",
                    message = "Saved successfully",
                    BPEntry = bp.DocEntry,
                    ServerPath = bp.ImagePath,
                });
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = ex.Message
                });
            }
        }



        [HttpPost("AddCheckCustomer")]
        [SwaggerOperation(
            Summary = "For add checkin and checkout",
            Description = "This endpoint is used to add checkin and checkout and update status of checkin and checkout."
        )]
        public async Task<IActionResult> AddCheckCustomer([FromForm] CheckDto dto)
        {
            try
            {
                if (dto.AppEntry == null)
                    return BadRequest("AppCheckEntry is required.");

                var check = await _context.tblChecks
                    .FirstOrDefaultAsync(x => x.DocEntry == dto.DocEntry);

                if (check == null)
                {
                    check = new tblCheck
                    {
                        AppEntry = dto.AppEntry,
                        AppOrderEntry = dto.AppOrderEntry,
                        CardCode = dto.CardCode,
                        CheckInDate = dto.CheckInDate,
                        CheckInGPS = dto.CheckInGPS,
                        CheckInRemark = dto.CheckInRemark,
                        CheckStatus = "Pending",
                        SalesCode = dto.SalesCode,
                    };

                    await _context.tblChecks.AddAsync(check);



                }
                else
                {
                    check.CheckOutDate = dto.CheckOutDate;
                    check.CheckOutGPS = dto.CheckOutGPS;
                    check.CheckOutRemark = dto.CheckOutRemark;
                    check.DMSOrderEntry = dto.DMSOrderEntry;
                    check.AppOrderEntry = dto.AppOrderEntry;
                    check.CheckStatus = "Completed";

                    // Deserialize reasons using Newtonsoft.Json
                    List<tblCheckOutReason> reasons = new List<tblCheckOutReason>();
                    if (!string.IsNullOrEmpty(dto.CheckOutReasonsJson))
                    {
                        reasons = JsonConvert.DeserializeObject<List<tblCheckOutReason>>(dto.CheckOutReasonsJson)!;

                        // Save checkout reasons
                        foreach (var r in reasons)
                        {
                            _context.tblCheckOutReasons.Add(new tblCheckOutReason
                            {
                                CheckInEntry = check.DocEntry,
                                ReasonCode = r.ReasonCode,
                                ReasonRemark = r.ReasonRemark
                            });
                        }

                    }

                }


                // ==========================
                // IMAGE STORAGE (SAME FOLDER)
                // ==========================
                var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var folderPath = Path.Combine(rootPath, "Attachments", "Check");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // ---------- CHECK-IN IMAGE ----------
                if (dto.CheckInImageFile != null && dto.CheckInImageFile.Length > 0)
                {
                    var ext = Path.GetExtension(dto.CheckInImageFile.FileName);
                    var fileName = $"CHECKIN_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await dto.CheckInImageFile.CopyToAsync(stream);
                    }

                    check.CheckInImage = $"/Attachments/Check/{fileName}";
                }

                // ---------- CHECK-OUT IMAGE ----------
                if (dto.CheckOutImageFile != null && dto.CheckOutImageFile.Length > 0)
                {
                    var ext = Path.GetExtension(dto.CheckOutImageFile.FileName);
                    var fileName = $"CHECKOUT_{DateTime.Now:yyyyMMdd_HHmmss}{ext}";
                    var fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await dto.CheckOutImageFile.CopyToAsync(stream);
                    }

                    check.CheckOutImage = $"/Attachments/Check/{fileName}";
                }
                await _context.SaveChangesAsync();
                //update docentry mapping
                DocEntryMapping mapping = new DocEntryMapping()
                {
                    AppDocEntry = dto.AppEntry,
                    DMSEntry = check.DocEntry,
                    SAPEntry = -1,
                    SalesCode = dto.SalesCode,
                    DocType = "Check"
                };
                await _context.DocEntryMappings.AddAsync(mapping);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    status = "OK",
                    message = "Saved successfully",
                    DMSEntry = check.DocEntry,
                    DMSOrderEntry = check.DMSOrderEntry,
                    CheckInImage = check.CheckInImage,
                    CheckOutImage = check.CheckOutImage
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "Failed",
                    message = ex.Message
                });
            }
        }



        [NonAction]
        public string? GetSODocNum(string salescode, string docdate)
        {
            string? docnum = "";
            try
            {
                var documentNumber = _context.DocumentNumbers
                  .FromSqlRaw("EXEC ICC_Get_Last_SO_DocNum '" + salescode + "','" + docdate + "'")
                  .AsEnumerable()
                  .FirstOrDefault();
                docnum = documentNumber?.docnum;
            }
            catch (Exception ex)
            {
                docnum = "";
            }
            return docnum;
        }

        [HttpPost("Orders_And_Reason_Order")]
        [SwaggerOperation(
            Summary = "For Add Order",
            Description = "This endpoint is used to add new sale order."
        )]
        public async Task<IActionResult> PostOrder([FromBody] OrderRequestDto request)
        {
            try
            {
                requestLog.Log<OrderRequestDto>("SO", request.Header.AppDocNo, request);
                //check approval
                // check approval
                var custoemr = _context.tblBPs.Where(x => x.CardCode == request.Header.CardCode).FirstOrDefault();
                string itemlist = string.Join("|", request.DetailsOne.Select(x => x.ItemCode));
                string uomlist = string.Join("|", request.DetailsOne.Select(x => x.UoMentry.ToString()));
                string qtylist = string.Join("|", request.DetailsOne.Select(x => x.Quantity.ToString()));
                string salePrices = string.Join("|", request.DetailsOne.Select(x => x.UnitPrice.ToString()));
                string priceListCodes = string.Join("|", request.DetailsOne.Select(x => custoemr?.DefPriceListCode.ToString())); // <-- new segment

                string spData = $"{itemlist};{uomlist};{qtylist};{salePrices};{priceListCodes}";

                var priceCheckResults = await _context.PriceApprovalResults
                    .FromSqlRaw("EXEC dbo.ICC_Web_Item_Price @p0", spData)
                    .ToListAsync();

                bool anyNeedsApproval = priceCheckResults.Any(x => x.ApprovalStatus == "NEED_APPROVAL");


                SO header = new SO();
                List<SO1> detail = new List<SO1>();
                HeaderDto h = request.Header;
                header.CardCode = h.CardCode;
                header.CardName = h.CardName;
                header.DelAddress = h.DelAddress;
                header.DocDate = h.DocDate;
                header.DueDate = h.DocDate;
                header.TaxDate = h.DocDate;
                header.SalesCode = h.SalesCode;
                header.Remark = h.Remark;
                header.VATType = h.VatType;
                header.TermCode = h.TermCode;
                header.ChecKInID = h.CheckInID;
                header.ChecKOutID = h.CheckOutID;
                header.CheckInDate = h.CheckInDate;
                header.CheckInLateLong = h.CheckInLateLong;
                header.CheckInRemark = h.CheckInRemark;
                header.CheckOutDate = h.CheckOutDate;
                header.CheckOutLateLong = h.CheckOutLateLong;
                header.CheckOutRemark = h.CheckOutRemark;
                header.PONo = h.PONo;
                header.SaleType = h.SaleType;

                header.SubTotal = (decimal)h.SubTotal;
                header.DisAmount = (decimal)h.DisAmount;
                header.AfterDis = (decimal)h.AfterDis;
                //header.DisPer = header.DisPer;
                header.VATAmount = (decimal)h.Vatamount;
                header.Total = (decimal)h.Total;//(afterdiscount + vatamount);
                // Insert New Header
                header.DocNo = GetSODocNum(header.SalesCode, header.DocDate.Value.ToString("yyyy/MM/dd"));
                header.CreatedDate = DateTime.Now;
                header.DocStatus = anyNeedsApproval ? "Draft" : "Approved";
                header.JsonRemark = System.Text.Json.JsonSerializer.Serialize(request);
                var currentuser = HttpContext.Session.GetString("username");
                header.CreatedBy = currentuser;
                header.Source = "App";
                await _context.SOs.AddAsync(header);
                //For generate Json Web Data
                await _context.SaveChangesAsync();

                // Insert New Details
                int linenum = 0;
                foreach (var x in request.DetailsOne)
                {
                    detail.Add(new SO1()
                    {
                        DocEntry = header.DocEntry,
                        LineNum = linenum,
                        ItemCode = x.ItemCode,
                        ItemName = x.ItemName,
                        Quantity = x.Quantity,
                        UoMEntry = x.UoMentry,
                        UnitPrice = x.UnitPrice,
                        DisAmount = x.DisAmount,
                        DisPer = x.DisPer,
                        LineTotal = x.LineTotal,
                        SaleType = x.SaleType,
                        WhsCode = x.WhsCode,
                    });
                    linenum++;
                }
                await _context.SO1s.AddRangeAsync(detail);
                await _context.SaveChangesAsync();

                if (h.USDAmount > 0)
                {
                    tblIncome inc = new tblIncome();
                    inc.SODocEntry = header.DocEntry;
                    inc.SOBalance = header.Total;
                    inc.BankCode = "";
                    inc.BankAmount = 0;
                    inc.CashAmount = h.USDAmount;
                    inc.CurCode = "USD";
                    inc.SAPIncomeDocEntry = -1;
                    inc.IntegrationStatus = "Pending";
                    await _context.tblIncomes.AddAsync(inc);
                    await _context.SaveChangesAsync();
                }
                if (h.KHRAmount > 0)
                {
                    tblIncome inc = new tblIncome();
                    inc.SODocEntry = header.DocEntry;
                    inc.SOBalance = header.Total;
                    inc.BankCode = "";
                    inc.BankAmount = 0;
                    inc.CashAmount = h.KHRAmount;
                    inc.CurCode = "KHR";
                    inc.SAPIncomeDocEntry = -1;
                    inc.IntegrationStatus = "Pending";
                    await _context.tblIncomes.AddAsync(inc);
                    await _context.SaveChangesAsync();
                }
                if (anyNeedsApproval)
                {
                    await _context.Database.ExecuteSqlRawAsync("EXEC get_Order_Approval @p0", new SqlParameter("@p0", header.DocEntry));

                }
                //Save to DocEntry Mapping
                DocEntryMapping mapping = new DocEntryMapping()
                {
                    DMSEntry = header.DocEntry,
                    AppDocEntry = request.Header.AppId,
                    SAPEntry = -1,
                    SalesCode = h.SalesCode,
                    DocType = "SO",
                };
                await _context.DocEntryMappings.AddAsync(mapping);
                await _context.SaveChangesAsync();
                return Ok(new { status = "OK", message = "Inserted successfully", SOEntry = header.DocEntry, SODocNo = header.DocNo });
            }
            catch (Exception ex)
            {
                return Ok(new { status = "Failed", message = ex.Message, SOEntry = -1, SODocNo = "" });
            }
        }


        [HttpGet("Get_Available_Order/{uSalesCode}")]
        [SwaggerOperation(
            Summary = "For Get Avaliable Order",
            Description = "This endpoint is used to get avaliable order."
        )]
        public async Task<ActionResult<List<v_Order>>> Get_Available_Order(string uSalesCode)
        {
            try
            {
                var order = _context.Get_Order_Available_Results.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Available_Order_For_App @p0", uSalesCode).AsEnumerable().ToList();
                if (order == null)
                    return NotFound();
                foreach (var x in order)
                {
                    x.order1 = _context.Get_Order_Available1_Results.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Order1 @p0", x.DocEntry).AsEnumerable().ToList();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An error occurred while retrieving available orders.",
                    details = ex.Message
                });
            }
        }
        // POST: /api/Orders_And_Reason_Order
        [HttpPost("Set_Available_Order")]
        [SwaggerOperation(
            Summary = "For Set Avaliable Order",
            Description = "This endpoint is used to set avaliable order."
        )]
        public async Task<IActionResult> Set_Available_Order([FromBody] List<v_Order_Status> request)
        {
            try
            {

                foreach (var x in request)
                {
                    if (x.DocEntry == null)
                        continue; // skip if DocEntry missing

                    var so = await _context.SOs.FindAsync(x.DocEntry);

                    if (so != null)
                    {
                        so.AllowHistory = "Done";
                        so.UpdateDate = DateTime.Now;
                    }
                }
                await _context.SaveChangesAsync();

                return Ok(new { message = "Update Order Status successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }




        // POST: /api/Income
        [HttpPost("Income")]
        [SwaggerOperation(
            Summary = "For Add Income",
            Description = "This endpoint is used to add income."
        )]
        public async Task<IActionResult> PostIncome([FromBody] IncomeDtoApp dto)
        {
            requestLog.Log("Income", dto.soDocEntry.ToString(), dto);
            if (dto == null)
                return BadRequest("Income data is required.");
            List<tblIncome> incom = new List<tblIncome>();
            try
            {
                if (dto.bankAmountUSD > 0 || dto.cashAmountUSD > 0)
                {
                    incom.Add(new tblIncome()
                    {
                        SODocEntry = dto.soDocEntry,
                        SOBalance = dto.soBalance,
                        BankCode = dto.bankCode,
                        BankAmount = dto.bankAmountUSD,
                        CashAmount = dto.cashAmountUSD,
                        CurCode = "USD",
                        SAPIncomeDocEntry = -1,
                        IntegrationStatus = "Pending",
                        LastError = ""
                    });
                }
                if (dto.bankAmountKHR > 0 || dto.cashAmountKHR > 0)
                {
                    incom.Add(new tblIncome()
                    {
                        SODocEntry = dto.soDocEntry,
                        SOBalance = dto.soBalance,
                        BankCode = dto.bankCode,
                        BankAmount = dto.bankAmountKHR,
                        CashAmount = dto.cashAmountKHR,
                        CurCode = "KHR",
                        SAPIncomeDocEntry = -1,
                        IntegrationStatus = "Pending",
                        LastError = ""
                    });
                }
                if (incom.Any())
                {
                    _context.tblIncomes.AddRange(incom);
                    await _context.SaveChangesAsync();
                }
                return Ok(new
                {
                    Message = "Income added successfully.",
                    Data = incom // includes DocEntry after save
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error adding income.", Details = ex.Message });
            }
        }
        [HttpPost("GetSAPNo")]
        public async Task<IActionResult> SAPSONo([FromBody] List<v_OrderSAP> order)
        {
            if (order == null)
                return BadRequest("Order data is required.");
            try
            {
                foreach (var x in order)
                {
                    var ord = _context.SOs.Where(a => a.SalesCode == x.SalesCode && a.AppId == x.AppId).FirstOrDefault();
                    if (ord != null)
                    {
                        x.SAPDocNo = ord.SAPDocNum;
                    }
                }
                return Ok(new
                {
                    Message = "Get SO successfully.",
                    Data = order // includes DocEntry after save
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error adding income.", Details = ex.Message });
            }
        }



        [HttpGet("get_update_status/{salescode}")]
        [SwaggerOperation(
            Summary = "Get Update Status in App",
            Description = "This endpoint is used to get status to update in App."
        )]
        public async Task<IActionResult> GetUpdateDNStatus(string salescode)
        {
            try
            {
                var result = await _context.ICC_Get_Order_App_Status_Results
                    .FromSqlRaw("EXEC ICC_Get_Order_App_Status @p0", salescode)
                    .ToListAsync();

                if (result.Any())
                {
                    var docEntries = result.Select(x => x.DocEntry).ToList();
                    var salesOrders = await _context.SOs
                        .Where(so => docEntries.Contains(so.DocEntry))
                        .ToListAsync();

                    foreach (var so in salesOrders)
                    {
                        so.AppStatus = "No";
                    }

                    _context.SOs.UpdateRange(salesOrders);
                    await _context.SaveChangesAsync();
                }

                return Ok(new { Code = 200, Status = "Success", Data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Status = "Error", Message = ex.Message });
            }
        }
        [HttpGet("get-today-exchange-rate")]
        [SwaggerOperation(
            Summary = "Get toady Exchange rate",
            Description = "This endpoint is used to get today exchange rate."
        )]
        public async Task<IActionResult> GetTodayExchangeRate()
        {
            try
            {
                var result = await _context.ExchangeRateResponses
                    .FromSqlRaw("EXEC ICC_API_34_GET_Today_Exchange_Rate")
                    .ToListAsync();

                return Ok(new { code = 200, status = "Success", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { code = 500, status = "Error", message = ex.Message });
            }
        }
    }
}

