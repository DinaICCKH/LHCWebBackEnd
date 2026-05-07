using Azure.Core;
using ClosedXML.Excel;
using DMSWebPortal.DTOs;
using DMSWebPortal.DTOs.Response;
using DMSWebPortal.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IdentityModel.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
namespace DMSWebPortal.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("dms")]
    [Authorize]
    public class DMSController : Controller
    {
        private readonly AppDbContext _context;
        public DMSController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
           
            return Login();
        }
        
        [HttpGet("home")]
        [AllowAnonymous]
        public async Task<IActionResult> Home()
        {
            var itemcount =await _context.tblItems.Where(x => x.Status == "Active").CountAsync();
            var bpcount = await _context.tblBPs.Where(x=>x.Status=="Active").CountAsync();
            var salecount = await _context.tblSalesEmployees.CountAsync();
            var visitcount = await _context.VisitHs.CountAsync();
            var socount = await _context.SOs.CountAsync();
            var promotioncount = await _context.tblPromotions.CountAsync();
            var reasoncount = await _context.Reasons.CountAsync();
            var user = await _context.Users.CountAsync();
            ViewBag.itemcount = itemcount;
            ViewBag.itemcount = itemcount;
            ViewBag.bpcount = bpcount;
            ViewBag.salecount = salecount;
            ViewBag.visitcount = visitcount;
            ViewBag.socount = socount;
            ViewBag.promotioncount = promotioncount;
            ViewBag.reasoncount = reasoncount;
            ViewBag.user = user;
            return View();
        }

        [HttpGet("item-master-data")]
        [AllowAnonymous]
        public IActionResult ItemMasterData() => View();

        [HttpGet("bp-master-data")]
        [AllowAnonymous]
        public IActionResult BPMasterData() => View();

        [HttpGet("uom")]
        [AllowAnonymous]
        public IActionResult UOM() => View();

        [HttpGet("uom-group")]
        [AllowAnonymous]
        public IActionResult UOMGroup() => View();

        [HttpGet("sale-employee")]
        [AllowAnonymous]
        public IActionResult SaleEmployee() => View();
        [HttpGet("sale-employee-detail/{key?}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSaleEmpDetail(int? key)
        {
            if (key == null)
                return BadRequest("Invalid key");

            // Get single sales employee
            var salesList = await _context.SaleEmployeeMasterDataResults
                 .FromSqlRaw(
                     "EXEC [dbo].[ICC_Get_Sale_Employee_Master_Data] @p0, @p1, @p2, @p3, @p4",
                     0, 10000, "All", "", key.ToString())
                 .ToListAsync();

            var sales = salesList.FirstOrDefault(x => x.SlpCode == key.ToString());

            if (sales == null)
                return NotFound("Invalid sale employee");

            // Get items for that employee
            var items = await _context.ItemMasterDataResults
                .FromSqlRaw(
                    "EXEC [dbo].[ICC_Get_Item_Master_Data] @p0, @p1, @p2, @p3, @p4",
                    0, 1000, "All", "", key.ToString())
                .ToListAsync();

            // Get BP for that employee
            var bps = await _context.ICC_Get_BP_Master_Data_Result
                .FromSqlRaw(
                    "EXEC [dbo].[ICC_Get_BP_Master_Data] @p0, @p1, @p2, @p3, @p4",
                    0, 1000, "All", "", key.ToString())
                .ToListAsync();

            ViewBag.sales = sales;
            ViewBag.items = items;
            ViewBag.bps = bps;

            return View();
        }

        [HttpGet("new-visit-plan")]
        [AllowAnonymous]
        public async Task<IActionResult> NewVisitPlanAsync() {
            //var salesList = await _context.SaleEmployeeMasterDataResults
            //     .FromSqlRaw(
            //         "EXEC [dbo].[ICC_Get_Sale_Employee_Master_Data] @p0, @p1, @p2, @p3, @p4",
            //         0, 10000, "All", "", "")
            //     .ToListAsync();
            var currentuser =  UserHelper.GetUserId(HttpContext);
            var salesList = await _context.tblSalesEmployees
                    .FromSqlRaw("EXEC Get_SalesMan_by_User @p0,@p1", currentuser, "")
                    .ToListAsync();

            var docnumnumber = _context.DocumentNumbers
             .FromSqlRaw("EXEC [dbo].[ICC_Get_Last_Visitor_DocNum]")
             .AsEnumerable()
             .FirstOrDefault();

            ViewBag.sales = salesList;
            ViewBag.docnum = docnumnumber?.docnum;
            
            return View();
        }
        [HttpGet("popup-customer")]
        [AllowAnonymous]
        public IActionResult PopCustomer(int day, int month, int year, int key)
        {
            DateTime dateValue = new DateTime(year, month, day); // Initialize DateTime with year, month, and day
            string date = dateValue.ToString("dd-MM-yyyy"); // Format the date as dd-MM-yyyy
            ViewBag.date = date;
            return View();
        }
        [HttpGet("popup-customer-visit")]
        [AllowAnonymous]
        public IActionResult PopCustomervisitplan(int day, int month, int year, int key)
        {
            DateTime dateValue = new DateTime(year, month, day); // Initialize DateTime with year, month, and day
            string date = dateValue.ToString("dd-MM-yyyy"); // Format the date as dd-MM-yyyy
            ViewBag.date = date;
            return View();
        }

        [HttpGet("edit-visit-plan/{key?}")]
        [AllowAnonymous]
        public async Task<IActionResult> EditVisitPlanAsync(int? key)
        {
            var salesList = await _context.SaleEmployeeMasterDataResults
                 .FromSqlRaw(
                     "EXEC [dbo].[ICC_Get_Sale_Employee_Master_Data] @p0, @p1, @p2, @p3, @p4",
                     0, 10000, "All", "", "")
                 .ToListAsync();
            var plan = await _context.VisitHs.Where(x=>x.DocEntry==key).FirstOrDefaultAsync();
            ViewBag.sales = salesList;
            ViewBag.plan = plan;
            return View();
        }
        [HttpGet("get-item-master-data")]
        public async Task< IActionResult> GetItemMasterData(int start = 0,int limit = 20,string type = "All",string search = "",string salecode = "",string principle = "All")
        {
            try
            {
                var result = await _context.ItemMasterDataResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_Item_Master_Data] @p0, @p1, @p2, @p3, @p4, @p5",
                start, limit, type, search, salecode, principle)
                .ToListAsync();

                return Ok(new {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("bp-request-list")]
        [AllowAnonymous]
        public async Task<IActionResult> BPRequestList()
        {
            return View();
        }
        [HttpGet("bp-request/{key?}")]
        [AllowAnonymous]
        public async Task<IActionResult> BPRequest(int? key = 0)
        {
            // Example: fetch data using the key
            var bpRequest = await _context.tblBPRequests.FindAsync(key);
            ViewBag.bp = bpRequest;
            ViewBag.province  =await _context.v_Provinces.ToListAsync();
            ViewBag.district = await _context.v_Districts.ToListAsync();
            ViewBag.commune = await _context.v_Communes.ToListAsync();
            ViewBag.payment = await _context.tblPayments.ToListAsync();
            ViewBag.pricelist = await _context.tblPriceLists.ToListAsync();
            ViewBag.group = await _context.tblBPGroups.ToListAsync();
            ViewBag.address = await _context.tblAddresses.ToListAsync();
            ViewBag.subgroup = await _context.tblSubGroups.ToListAsync();
            ViewBag.grade = await _context.tblGrades.ToListAsync();
            ViewBag.regional = await _context.tblRegionals.ToListAsync();
            ViewBag.salelist = await _context.tblSalesEmployees.ToListAsync();

            return View(bpRequest);
        }

        [HttpGet("get-bp-request-list")]
        public async Task<IActionResult> GetBPRequest(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "",string status="All")
        {
            try
            {
                var result = await _context.ICC_Get_BP_Request_Data_Result.FromSqlRaw(
                "EXEC ICC_Get_BP_Request_Data @p0, @p1, @p2, @p3, @p4, @p5",
                start, limit, type, search, salecode,status)
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet("get-bp-request-data")]
        //public async Task<IActionResult> GetBPRequest(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "")
        //{
        //    try
        //    {
        //        var result = await _context.ICC_Get_BP_Master_Data_Result.FromSqlRaw(
        //        "EXEC [dbo].[ICC_Get_BP_Master_Data] @p0, @p1, @p2, @p3, @p4",
        //        start, limit, type, search, salecode)
        //        .ToListAsync();
        //        return Ok(new
        //        {
        //            success = true,
        //            message = "Success",
        //            data = result,
        //            totalrow = result.Count > 0 ? result[0].TotalRow : 0
        //        }); // Return JSON
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        
        [HttpGet("get-bp-master-data")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBPMasterData(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "")
        {
            try
            {
                var result = await _context.ICC_Get_BP_Master_Data_Result.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_BP_Master_Data] @p0, @p1, @p2, @p3, @p4",
                start, limit, type, search, salecode)
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-bp-master-data-plan")]
        public async Task<IActionResult> GetBPMasterDataPlan(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "")
        {
            try
            {
                var result = await _context.BPMasterDataResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_BP_Master_Data_Plan] @p0, @p1, @p2, @p3, @p4",
                start, limit, type, search, salecode)
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-sale-employee-master-data")]
        public async Task<IActionResult> GetSaleEmloyeeMasterData(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "")
        {
            try
            {
                var result = await _context.SaleEmployeeMasterDataResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_Sale_Employee_Master_Data] @p0, @p1, @p2, @p3, @p4",
                start, limit, type, search, salecode)
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("reason-list")]
        [AllowAnonymous]
        public async Task<IActionResult> ReasonList(string search)
        {
            
            if (_context.Reasons == null)
            {
                return Problem("Table 'Reason' is Emtry");
            }
            var reason = from r in _context.Reasons select r;
            if (!string.IsNullOrEmpty(search))
            {
                reason = reason.Where(x => x.Reason1.Contains(search) || x.ReasonEN.Contains(search) || x.ReasonKH.Contains(search));
            }
            return View(await reason.ToListAsync());
        }
        [HttpPost("save-change-reason")]
        public async Task<IActionResult> SaveReason([FromBody]Reason reason)
        {
            if (reason == null)
                return BadRequest("Reason data is missing.");

            try
            {
                var r = await _context.Reasons
                    .FirstOrDefaultAsync(x => x.Code == reason.Code);
                if (r == null)
                {
                    r = new Reason();
                    r = reason;
                    r.CreatedDate = DateTime.Now;
                    await _context.Reasons.AddAsync(r);
                }
                else
                {
                    r.ReasonEN = reason.ReasonEN;
                    r.ReasonKH = reason.ReasonKH;
                    r.Reason1 = reason.Reason1;
                    r.Status = reason.Status;
                    r.UpdatedBy = reason.CreatedBy;
                    r.UpdatedDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Reason saved successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-reason-list")]
        public async Task<IActionResult> GetReasonList(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "")
        {
            try
            {
                var reason =await _context.Reasons.Where(x => x.Reason1.Contains(search) || x.ReasonEN.Contains(search) || x.ReasonKH.Contains(search)).ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = reason?.Skip(start).Take(limit),
                    totalrow = reason?.Count() ?? 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost("save-visit-plan")]
        public async Task<IActionResult> SaveVisitPlan([FromBody] VisitPlanDto to)
        {
            int code = 200;
            string message = "";

            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    VisitH header = to.Header;
                    List<VisitD> detail = to.Detail;

                    var h = await _context.VisitHs.FirstOrDefaultAsync(x => x.DocYear==header.DocYear  && x.SalesCode == header.SalesCode);
                    var saleman = await _context.tblSalesEmployees.Where(x => x.SlpCode == header.SalesCode).FirstOrDefaultAsync();
                    if (h == null)
                    {
                        //var docnumnumber = _context.DocumentNumbers
                        // .FromSqlRaw("EXEC [dbo].[ICC_Get_Last_Visitor_DocNum]")
                        // .AsEnumerable()
                        // .FirstOrDefault();
                        // Insert header
                        header.DocNum = saleman?.U_SalesCode + "-" + header.DocYear;
                        header.CreatedDate = DateTime.Now;
                        header.Status = "Active";
                        await _context.VisitHs.AddAsync(header);
                        await _context.SaveChangesAsync();

                        // Insert details
                        foreach (var a in detail)
                        {
                            var d = _context.VisitDs.FirstOrDefault(x =>
                                x.DocEntry == header.DocEntry &&
                                x.VisitDate == a.VisitDate &&
                                x.CardCode == a.CardCode);

                            if (d == null)
                            {
                                d = new VisitD
                                {
                                    DocEntry = header.DocEntry,
                                    VisitDate = a.VisitDate,
                                    CardCode = a.CardCode,
                                    ReasonType = a.ReasonType,
                                    Remark = a.Remark
                                };
                                await _context.VisitDs.AddAsync(d);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        // Update header
                        h.UpdatedBy = header.CreatedBy;
                        h.SalesCode = header.SalesCode;
                        h.DocYear = header.DocYear;
                        h.Remark = header.Remark;
                        h.UpdatedDate = DateTime.Now;
                        await _context.SaveChangesAsync();

                        //// Replace details
                        //var removes = await _context.VisitDs.Where(x => x.DocEntry == h.DocEntry).ToListAsync();
                        //_context.VisitDs.RemoveRange(removes);
                        //await _context.SaveChangesAsync();

                        foreach (var a in detail)
                        {
                            var det = _context.VisitDs.FirstOrDefault(x =>
                                x.DocEntry == h.DocEntry &&
                                x.VisitDate == a.VisitDate &&
                                x.CardCode == a.CardCode);
                            if (det == null)
                            {
                                var d = new VisitD
                                {
                                    DocEntry = h.DocEntry,
                                    VisitDate = a.VisitDate,
                                    CardCode = a.CardCode,
                                    ReasonType = a.ReasonType,
                                    Remark = a.Remark
                                };
                                await _context.VisitDs.AddAsync(d);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }

                // ✅ commit
                scope.Complete();
            }
        }
        catch (Exception ex)
        {
            code = 400;
            message = ex.Message;
        }
        return Ok(new { code, message });
    }
    

        [HttpGet("visit-plan-tracking")]
        [AllowAnonymous]
        public async Task<IActionResult> PlanTracking()
        {
            var currentuser =  UserHelper.GetUserId(HttpContext);

            var salesList = await _context.tblSalesEmployees
                .FromSqlRaw("EXEC Get_SalesMan_by_User @p0,@p1", currentuser, "")
                .ToListAsync();

            ViewBag.saleslist = salesList;

            return View();
        }
        [HttpGet("get-visit-plan-list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPlanTrackingList(int start = 0, int limit = 20, string type = "All", string search = "", string salecode = "")
        {
            try
            {
                var result = await _context.PlanTrackingResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Plan_tracking_list] @p0, @p1, @p2, @p3, @p4, @p5",
                start, limit, type, search, salecode,  UserHelper.GetUserId(HttpContext))
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-visit-plan-detail/{key?}")]
        public async Task<IActionResult> GetVisitPlanDetail(int? key)
        {
            try
            {
                if (key == null)
                    return BadRequest(new { success = false, message = "Key is required" });

                var h = await _context.VisitHs
                    .FirstOrDefaultAsync(x => x.DocEntry == key);

                if (h == null)
                    return NotFound(new { success = false, message = "Visit plan not found" });

                // Preload BPs into dictionary to avoid N+1 queries
                var cardCodes = await _context.VisitDs
                    .Where(x => x.DocEntry == key)
                    .Select(x => x.CardCode)
                    .Distinct()
                    .ToListAsync();

                var bpDict = await _context.tblBPs
                    .Where(bp => cardCodes.Contains(bp.CardCode))
                    .ToDictionaryAsync(bp => bp.CardCode, bp => bp.CardName);

                var d = await _context.VisitDs
                    .Where(x => x.DocEntry == key)
                    .Select(x => new
                    {
                        x.DocEntry,
                        x.VisitDate,
                        DateText = x.VisitDate.ToString("dd-MM-yyyy"),
                        x.CardCode,
                        x.ReasonType,
                        x.Remark,
                        x.ImageURL,
                        CardName = bpDict.ContainsKey(x.CardCode) ? bpDict[x.CardCode] : null
                    })
                    .ToListAsync();

                return Ok(new
                {
                    header = h,
                    detail = d
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("new-sale-order")]
        [AllowAnonymous]
        public async Task<IActionResult> NewSaleOrder()
        {
            var salesList = await _context.SaleEmployeeMasterDataResults
                 .FromSqlRaw(
                     "EXEC [dbo].[ICC_Get_Sale_Employee_Master_Data] @p0, @p1, @p2, @p3, @p4",
                     0, 10000, "All", "", "")
                 .ToListAsync();
            ViewBag.sales = salesList.Where(x=>x.SALType.ToLower()=="pre").ToList();

            //var docnum = GetSODocNum("","");
            ViewBag.docnum = "";// docnum"";
            var principles = await _context.tblPrinciples.ToListAsync();
            ViewBag.principle = principles;
            var userId = UserHelper.GetUserId(HttpContext); // ✅ global function
            ViewBag.username = userId;
            return View();
        }

        [HttpGet("sale-order-list")]
        [AllowAnonymous]
        //public IActionResult SOList()
        public async Task<IActionResult> SOList()
        {

            //var docnum = GetSODocNum("","");
            //ViewBag.docnum = docnum;
            var currentuser =  UserHelper.GetUserId(HttpContext);
            var salesList = _context.tblSalesEmployees
                .FromSqlRaw("EXEC Get_SalesMan_by_User @p0,@p1", currentuser,"").ToList();
            ViewBag.saleslist = salesList;
            return View();
        }

        [HttpGet("get-sonum")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSODocNumBySalesman(string salescode, string docdate)
        {
            try
            {
                var documentNumber = _context.DocumentNumbers
                 .FromSqlRaw("EXEC ICC_Get_Last_SO_DocNum '" + salescode + "','" + docdate + "'")
                 .AsEnumerable()
                 .FirstOrDefault();
                return Ok(new
                {
                    docnum = documentNumber?.docnum
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        [AllowAnonymous]
        public string? GetSODocNum(string salescode,string docdate)
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
            catch(Exception ex)
            {
                docnum = "";
            }
            return docnum;
        }

        [HttpGet("open-sale-order/{key?}")]
        [AllowAnonymous]
        public async Task< IActionResult> EditSaleOrder(int? key)
        {
            if (key == null)
            {
                return BadRequest("Invalid key");
            }
            var head = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == key);
            if (head == null)
            {
                return NotFound("SO not found.");
            }
            ViewBag.head = head;
            var salesList = await _context.SaleEmployeeMasterDataResults
                 .FromSqlRaw(
                     "EXEC [dbo].[ICC_Get_Sale_Employee_Master_Data] @p0, @p1, @p2, @p3, @p4",
                     0, 10000, "All", "", "")
                 .ToListAsync();
            var selectedsale = salesList.Where(a => a.SalesCode == head.SalesCode).FirstOrDefault();

            ViewBag.sales = salesList.Where(x => x.SALType.ToLower() == selectedsale.SALType.ToLower()).ToList();
            var principles = await _context.tblPrinciples.ToListAsync();
            ViewBag.principle = principles;
            var username= UserHelper.GetUserId(HttpContext);
            ViewBag.username = username;
            ViewBag.salestype = selectedsale.SALType.ToLower();
            return View();
        }
        
        [HttpGet("get-contact-person")]
        [AllowAnonymous]
        public async Task<IActionResult> GetContactPerson(string? CardCode)
        {
           
            try
            {
                var list =await _context.v_OCPRs.Where(x => x.CardCode == CardCode).ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = list,
                }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("get-saleman-cardcode")]
        public async Task<IActionResult> GetSalesmanbyCardCode(string? CardCode)
        {

            try
            {
                var currentuser =  UserHelper.GetUserId(HttpContext);
                var salesList = await _context.tblSalesEmployees
                    .FromSqlRaw("EXEC Get_SalesMan_by_User @p0,@p1", currentuser, CardCode)
                    .ToListAsync();
                //var slpCodes = salesList.Select(a => a.SlpCode).ToList();
                //var result = await (from m in _context.tblBPSalMappings
                //                    join s in _context.tblSalesEmployees on m.SlpCode equals s.SlpCode
                //                    where m.CardCode == CardCode
                //                    //&& s.SALType.ToLower()=="pre"
                //                    && slpCodes.Contains(s.SlpCode.ToString())
                //                    select new
                //                    {
                //                        s.SlpCode,
                //                        s.SalesName,
                //                        s.U_Whs,
                //                        s.U_SalesCode
                //                    }).ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = salesList,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("get-item-price")]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemPrice(string? itemcode,int uomentry,int pricelist,string whscode)
        {
            double price=0;
            try
            {
                var list = _context.DocumentNumbers.FromSqlRaw("exec ICC_Get_Web_ItemPrice @p0,@p1,@p2,@p3", itemcode, uomentry, pricelist, whscode).AsEnumerable().FirstOrDefault();
                price =Convert.ToDouble(list.docnum);
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    price = price,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("get-item-stock")]
        [AllowAnonymous]
        public async Task<IActionResult> GetItemStock(string? itemcode, int uomentry,string whscode)
        {
            double price = 0;
            try
            {
                var list = _context.DocumentNumbers.FromSqlRaw("exec ICC_Get_Web_ItemStock @p0,@p1,@p2", itemcode, uomentry, whscode).AsEnumerable().FirstOrDefault();
                price = Convert.ToDouble(list.docnum);
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    price = price,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("get-uom-by-item")]
        [AllowAnonymous]
        public async Task< IActionResult> GetUOMListByItem(string? itemcode)
        {
            try
            {
                var list = await _context.UomListByItemCodeResults.FromSqlRaw("[dbo].[ICC_Get_UOM_List_By_Item_Code] @p0",itemcode).ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = list,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("get-warehouse-list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWarehouseList(string salescode,int DocEntry)
        {
            try
            {
                var currentuser =  UserHelper.GetUserId(HttpContext);
                var result = await _context.v_OWHs.FromSqlRaw("EXEC ICC_Web_Get_Whs_List @p0, @p1,@p2", salescode, currentuser,DocEntry).ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpPost("run-promotion")]
        public async Task<IActionResult> RunPromotion([FromBody] SaleOrder order)
        {
            try
            {
                string itemlist = "";
                string uom = "";
                string qty = "";
                string linetotal = "";
                string all_mix = "";
                var uomlist = await _context.tbluomresults.FromSqlRaw("select * from v_UoM").ToListAsync();
                string uomname = "";
                foreach (var x in order.Detail)
                {
                    itemlist = (itemlist == "" ? x.ItemCode : itemlist + "|" + x.ItemCode);
                    uomname = uomlist.Where(a => a.UoMEntry == x.UoMEntry).FirstOrDefault().UoMCode;
                    uom = (uom == "" ? uomname : uom + "|" + uomname);
                    qty = (qty == "" ? x.Quantity.ToString() : qty + "|" + x.Quantity.ToString());
                    linetotal = (linetotal == "" ? (x.Quantity * x.UnitPrice).ToString() : linetotal + "|" + (x.Quantity * x.UnitPrice).ToString());
                }
                all_mix = itemlist + ";" + uom + ";" + qty + ";" + linetotal;
                string data = string.Format("EXEC [dbo].ICC_Get_PromotionDetails {0}, {1}, {2}", order.Header.CardCode, order.Header.DocDate.Value.ToString("yyyy/MM/dd"), all_mix);
                var result = await _context.v_RunPromotionResults.FromSqlRaw("EXEC [dbo].ICC_Get_PromotionDetails @p0, @p1, @p2",
                order.Header.CardCode, order.Header.DocDate.Value.ToString("yyyy/MM/dd"), all_mix)
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpPost("update-so")]
        public async Task<IActionResult> UpdateSO([FromBody] SaleOrder order)
        {
            SO header;
            List<SO1> detail;
            try
            {
                header = order.Header;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var existingHeader = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == header.DocEntry);
                    if (existingHeader != null)
                    {
                        existingHeader.CardCode = header.CardCode;
                        existingHeader.CardName = header.CardName;
                        existingHeader.DelAddress = header.DelAddress;
                        existingHeader.UpdateDate = DateTime.Now;
                        await _context.SaveChangesAsync();
                    }
                    scope.Complete();
                    return Ok(new { success = true, message = "add or update successful" });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }

        }

        [HttpPost("manager-save-so")]
        public async Task<IActionResult> ManagerSaveSO([FromBody] SaleOrder order)
        {
            SO header;
            List<SO1> detail;
            try
            {
                header = order.Header;
                detail = order.Detail;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var existingHeader = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == header.DocEntry);
                    //double subtotal = (double)detail.Sum(a => a.LineTotal);
                    //double discountamt = 0;
                    //double afterdiscount = 0;
                    //double vatamount = 0;
                    //if (header.DisPer > 0)
                    //{
                    //    discountamt = subtotal * (double)(header.DisPer / 100);
                    //}
                    //afterdiscount = subtotal - discountamt;
                    //if (existingHeader.VATType.ToLower() == "tax")
                    //{
                    //    vatamount = afterdiscount * 0.1;
                    //}
                    //header.SubTotal = (decimal)subtotal;
                    //header.DisAmount = (decimal)discountamt;
                    //header.AfterDis = (decimal)afterdiscount;
                    //header.DisPer = header.DisPer;
                    //header.VATAmount = (decimal)vatamount;
                    //header.Total = (decimal)(afterdiscount + vatamount);

                    if (existingHeader != null)
                    {
                        existingHeader.SubTotal = header.SubTotal;
                        existingHeader.DisPer = header.DisPer;
                        existingHeader.DisAmount = header.DisAmount;
                        existingHeader.AfterDis = header.AfterDis;
                        existingHeader.VATAmount = header.VATAmount;
                        existingHeader.Total = header.Total;
                        existingHeader.AllowHistory = "Yes";
                        await _context.SaveChangesAsync();
                        //int lastLineNum = 0;
                        //lastLineNum = _context.SO1s.Where(x => x.DocEntry == existingHeader.DocEntry).Select(x => x.LineNum).ToList().LastOrDefault();
                        // Process Details
                        //For Remove
                        //foreach (var item in detail.Where(a => a.RefLineNum != 0 && a.LineNum != -1).ToList())
                        //{
                        //    var existingDetail = _context.SO1s.Where(x => x.DocEntry == header.DocEntry && x.LineNum == item.RefLineNum).FirstOrDefault();
                        //    if (existingDetail != null)
                        //    {
                        //        // Remove Line
                        //        _context.SO1s.Remove(existingDetail);
                        //    }
                        //}
                        //For Update
                        foreach (var item in detail.Where(a => a.RefLineNum == 0 && a.LineNum != -1).ToList())
                        {
                            var existingDetail = _context.SO1s.Where(a => a.DocEntry == header.DocEntry && a.LineNum == item.LineNum).FirstOrDefault();
                            if (existingDetail != null)
                            {
                                // Update Line
                                existingDetail.ItemCode = item.ItemCode;
                                existingDetail.ItemName = item.ItemName;
                                //existingDetail.UoMEntry = item.UoMEntry;
                                //existingDetail.Quantity = item.Quantity;
                                existingDetail.UnitPrice = item.UnitPrice;
                                existingDetail.DisPer = item.DisPer;
                                existingDetail.DisAmount = item.DisAmount;
                                existingDetail.LineTotal = item.LineTotal;
                                existingDetail.WhsCode = item.WhsCode;
                                existingDetail.ProCode = item.ProCode;
                                existingDetail.ProLineNo = item.ProLineNo;
                                existingDetail.PromotionType = item.PromotionType;
                            }
                        }
                        ////For New Item
                        //foreach (var item in detail.Where(a => a.RefLineNum == 0 && a.LineNum == -1).ToList())
                        //{
                        //    // Insert New Line
                        //    lastLineNum++;
                        //    var newDetail = item;
                        //    newDetail.DocEntry = existingHeader.DocEntry;
                        //    newDetail.LineNum = lastLineNum;
                        //    await _context.SO1s.AddAsync(newDetail);
                        //}
                    }
                    await _context.SaveChangesAsync();
                    scope.Complete();
                    return Ok(new { success = true, message = "add or update successful" });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }
        }

        [HttpPost("save-so")]
        public async Task<IActionResult> SaveSO([FromBody] SaleOrder order)
        {
            SO header;
            List<SO1> detail;
            try
            {
                header = order.Header;
                detail = order.Detail;
               
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // check approval
                    var custoemr = _context.tblBPs.Where(x => x.CardCode == header.CardCode).FirstOrDefault();
                    string itemlist = string.Join("|", detail.Select(x => x.ItemCode));
                    string uomlist = string.Join("|", detail.Select(x => x.UoMEntry.ToString()));
                    string qtylist = string.Join("|", detail.Select(x => x.Quantity.ToString()));
                    string salePrices = string.Join("|", detail.Select(x => x.UnitPrice.ToString()));
                    string priceListCodes = string.Join("|", detail.Select(x => custoemr?.DefPriceListCode.ToString())); // <-- new segment

                    string spData = $"{itemlist};{uomlist};{qtylist};{salePrices};{priceListCodes}";

                    var priceCheckResults = await _context.PriceApprovalResults
                        .FromSqlRaw("EXEC dbo.ICC_Web_Item_Price @p0", spData)
                        .ToListAsync();

                    bool anyNeedsApproval = priceCheckResults.Any(x => x.ApprovalStatus == "NEED_APPROVAL");

                    header.DocStatus = anyNeedsApproval ? "Draft" : "Approved";

                    //process save

                    var existingHeader = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == header.DocEntry);
                    double subtotal=(double) detail.Sum(a => a.LineTotal);
                    double discountamt = 0;
                    double afterdiscount = 0;
                    double vatamount = 0;
                    if (header.DisPer > 0)
                    {
                        discountamt=subtotal * (double)(header.DisPer / 100);
                    }
                    afterdiscount = subtotal - discountamt;
                    if (header.VATType.ToLower() == "tax")
                    {
                        vatamount= afterdiscount * 0.1;
                    }
                    header.SubTotal = (decimal)subtotal;
                    header.DisAmount = (decimal)discountamt;
                    header.AfterDis = (decimal)afterdiscount;
                    header.DisPer = header.DisPer;
                    header.VATAmount = (decimal)vatamount;
                    header.Total = (decimal)(afterdiscount + vatamount);
                    

                    if (existingHeader == null)
                    {
                        var sempl = await _context.tblSalesEmployees.Where(a => a.SlpCode.ToString() == header.SalesCode).FirstOrDefaultAsync();
                        // Insert New Header
                        header.DocNo = GetSODocNum(sempl?.U_SalesCode, header.DocDate.Value.ToString("yyyy/MM/dd"));
                        header.SalesCode = sempl?.U_SalesCode;
                        header.CreatedDate = DateTime.Now;
                        //header.DocStatus = "Draft";
                        var currentuser =  UserHelper.GetUserId(HttpContext);
                        var result = await _context.OneColumnResults
                        .FromSqlRaw("EXEC get_Order_Next_Approver @p0", currentuser).ToListAsync();
                        //if (result[0].Approver == "")
                        //{
                        //    header.DocStatus = "Approved";
                        //}
                        //header.NextApprover = result[0].Approver;
                        header.NextApprover=header.DocStatus=="Draft"? result[0].Approver:"";
                        header.CreatedBy= currentuser;
                        header.Source = "Web";
                        header.SaleType = sempl.SALType ;
                        await _context.SOs.AddAsync(header);
                        //For generate Json Web Data
                        await _context.SaveChangesAsync();

                        //Remove row 
                        detail.ForEach((data) => {
                            if (data.RefLineNum == -1)
                            {
                                detail.Remove(data);
                            }
                        });
                        // Insert New Details
                        int linenum = 0;
                        detail.ForEach(x =>
                        {
                            x.DocEntry = header.DocEntry;
                            x.LineNum = linenum++;
                        });
                        await _context.SO1s.AddRangeAsync(detail);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Update Header
                        existingHeader.CardCode = header.CardCode;
                        existingHeader.CardName = header.CardName;
                        existingHeader.DelAddress = header.DelAddress;
                        existingHeader.TaxDate = header.TaxDate;
                        existingHeader.ContactPer = header.ContactPer;
                        existingHeader.SubTotal = header.SubTotal;
                        existingHeader.DisPer = header.DisPer;
                        existingHeader.DisAmount = header.DisAmount;
                        existingHeader.AfterDis = header.AfterDis;
                        existingHeader.Total = header.Total;
                        existingHeader.UpdateDate = DateTime.Now;
                        existingHeader.Remark = header.Remark;
                        existingHeader.AllowHistory = "Yes";
                        await _context.SaveChangesAsync();
                        ////Reset Line Number
                        //int linenum = 0;
                        //detail.ForEach((data) => {
                        //    data.DocEntry = header.DocEntry;
                        //    data.LineNum = linenum;
                        //    linenum++;
                        //});
                        int lastLineNum = 0;
                        lastLineNum = _context.SO1s.Where(x => x.DocEntry == existingHeader.DocEntry).Select(x => x.LineNum).ToList().LastOrDefault();
                        // Process Details
                        //For Remove
                        foreach (var item in detail.Where(a => a.RefLineNum != 0 && a.LineNum != -1).ToList())
                        {
                            var existingDetail = _context.SO1s.Where(x => x.DocEntry == header.DocEntry && x.LineNum == item.RefLineNum).FirstOrDefault();
                            if (existingDetail != null)
                            {
                                // Remove Line
                                _context.SO1s.Remove(existingDetail);
                            }
                        }
                        //For Update
                        foreach (var item in detail.Where(a => a.RefLineNum == 0 && a.LineNum != -1).ToList())
                        {
                            var existingDetail = _context.SO1s.Where(a => a.DocEntry == header.DocEntry && a.LineNum == item.LineNum).FirstOrDefault();
                            if (existingDetail != null)
                            {
                                // Update Line
                                existingDetail.ItemCode = item.ItemCode;
                                existingDetail.ItemName = item.ItemName;
                                existingDetail.UoMEntry = item.UoMEntry;
                                existingDetail.Quantity = item.Quantity;
                                existingDetail.UnitPrice = item.UnitPrice;
                                existingDetail.DisPer = item.DisPer;
                                existingDetail.DisAmount = item.DisAmount;
                                existingDetail.LineTotal = item.LineTotal;
                                existingDetail.WhsCode = item.WhsCode;
                                existingDetail.ProCode = item.ProCode;
                                existingDetail.ProLineNo = item.ProLineNo;
                                existingDetail.PromotionType = item.PromotionType;
                            }
                        }
                        //For New Item
                        foreach (var item in detail.Where(a => a.RefLineNum == 0 && a.LineNum==-1).ToList())
                        {
                            // Insert New Line
                            lastLineNum++;
                            var newDetail = item;
                            newDetail.DocEntry = existingHeader.DocEntry;
                            newDetail.LineNum = lastLineNum;
                            await _context.SO1s.AddAsync(newDetail);
                        }
                    }

                    try
                    {
                        v_Order h = new v_Order();
                        h = _context.Get_Order_Available_Results.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Available_Order_For_App @p0", header.DocEntry.ToString()).AsEnumerable().FirstOrDefault();
                        if (h == null)
                        {
                            h = new v_Order();
                        }
                        h.order1 = _context.Get_Order_Available1_Results.FromSqlRaw("EXEC dbo.ICC_Api_SAP_Get_Order1 @p0", h.DocEntry).AsEnumerable().ToList();
                        if (h != null)
                        {
                            //header.JsonWeb = JsonSerializer.Serialize(h);
                            header.JsonWeb = JsonConvert.SerializeObject(h);
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    await _context.SaveChangesAsync();
                    scope.Complete();
                    return Ok(new { success = true, message = "add or update successful" });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return StatusCode(500, new { success = false, message = ex.Message });
                }
            }

        }
        [HttpGet("search-so-list-none")]
        public async Task<IActionResult> SearchSONoneListAsync(int start, int limit, string status, string type, string salecode, string search, string fromdate, string todate)
        {
            try
            {
                var result = await _context.NonSaleOrderListResults.FromSqlRaw(
                "EXEC ICC_Get_SO_Non_List @p0, @p1, @p2, @p3, @p4, @p5, @p6,@p7,@p8",
                start, limit, status, type, search, salecode, fromdate, todate,  UserHelper.GetUserId(HttpContext))
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("search-so-list")]
        public async Task<IActionResult> SearchSOListAsync( int start, int limit ,string status,string type,string salecode,string search,string fromdate,string todate)
        {
            try
            {
                //string store ="EXEC [dbo].[ICC_Get_SO_List] @p0, @p1, @p2, @p3, @p4, @p5, @p6,@p7,@p8",
                //start, limit, status, type, search, salecode, fromdate, todate,  UserHelper.GetUserId(HttpContext)

                var result = await _context.SaleOrderListResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_SO_List] @p0, @p1, @p2, @p3, @p4, @p5, @p6,@p7,@p8",
                start, limit,status, type, search, salecode,fromdate,todate,  UserHelper.GetUserId(HttpContext))
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("search-so-van-list")]
        public async Task<IActionResult> SearchSOVANListAsync(int start, int limit, string status, string type, string salecode, string search, string fromdate, string todate)
        {
            try
            {
                var result = await _context.SaleOrderListResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_SO_List_VAN] @p0, @p1, @p2, @p3, @p4, @p5, @p6,@p7,@p8",
                start, limit, status, type, search, salecode, fromdate, todate,  UserHelper.GetUserId(HttpContext))
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("get-so-detail/{key?}")]
        public async Task<IActionResult> GetSODetail(int? key)
        {
            try
            {
                var h = await _context.v_SOs.Where(x => x.DocEntry == key).FirstOrDefaultAsync();
                var d = await _context.SO1s.Where(x => x.DocEntry == key).ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = new { 
                        header=h,
                        detail=d,
                    }
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("soaction/{key:int}/{status}")]
        public async Task<IActionResult> SOAction(int? key,string status)
        {
            if (key == null)
            {
                return BadRequest("Invalid key");
            }
            var currentuser =  UserHelper.GetUserId(HttpContext);

            var result = await _context.OneColumnResults
            .FromSqlRaw("EXEC get_Order_Next_Approver @p0", currentuser).ToListAsync();

            var head = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == key);
            if (head == null)
            {
                return NotFound("SO not found.");
            }
            try
            {
                //if (status.ToLower() == "approved")
                //{
                //    //if (result[0].Approver == "")
                //    //{
                //    //    head.DocStatus = status;
                //    //    head.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                //    //}
                //    //else
                //    //{
                //    //    head.NextApprover = result[0].Approver;
                //    //    head.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                //    //}
                //    head.DocStatus = status;
                //    head.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                //}
                //else
                //{
                //    head.DocStatus = status;
                //    head.UpdateDate = DateOnly.FromDateTime(DateTime.Now);
                //}
                head.DocStatus = status;
                head.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Document was " + status
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("integrate-so/{key?}")]
        public async Task<IActionResult> IntegrateSO(int? key)
        {
            if (key == null)
            {
                return BadRequest("Invalid key");
            }
            var head = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == key);
            if (head == null)
            {
                return NotFound("SO not found.");
            }
            try
            {

                head.SAPSyncStatus = null;
                head.SAPLastError = null;
                await _context.SaveChangesAsync();
              
                return Ok(new
                {
                    success = true,
                    message = "command for integrate success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
            
        }
        [HttpPost("cancel-so/{key?}")]
        public async Task<IActionResult> CancelSO(int? key)
        {
            if (key == null)
            {
                return BadRequest("Invalid key");
            }
            var head = await _context.SOs.FirstOrDefaultAsync(x => x.DocEntry == key);
            if (head == null)
            {
                return NotFound("SO not found.");
            }
            try
            {

                head.DocStatus = "C";
                head.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "command for integrate success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }
        [HttpGet("promotion-setup")]
        [AllowAnonymous]
        public IActionResult PromotionSetup()
        {
            return View();
        }
        [HttpGet("promotion-list")]
        [AllowAnonymous]
        public IActionResult PromotionList()
        {
            return View();
        }
        [HttpGet("search-promotion-list")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchPromotionList(int start, int limit, string status, string type, string salecode, string search, string fromdate, string todate)
        {
            try
            {
                var result = await _context.PromotionListResults.FromSqlRaw(
                "EXEC [dbo].[ICC_Get_Promotion_list] @p0, @p1, @p2, @p3, @p4, @p5, @p6,@p7",
                start, limit, status, type, search, salecode, fromdate, todate)
                .ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                }); // Return JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("promotion-detail/{key?}")]
        [AllowAnonymous]
        public async Task<IActionResult> PromotionDetail(int? key)
        {
            if (key == null)
            {
                return BadRequest("Invalid key");
            }
            var head = await _context.tblPromotions.FirstOrDefaultAsync(x => x.ProEntry == key);
            if (head == null)
            {
                return NotFound("Promotion not found.");
            }
            ViewBag.head = head;
            return View();
        }
        [HttpGet("get-promotion-detail/{key?}")]
        public async Task<IActionResult> GetPromotionDetail(int? key)
        {
            if (key == null)
            {
                return BadRequest("Invalid key");
            }
            var head = await _context.tblPromotions.FirstOrDefaultAsync(x => x.ProEntry == key);
            if (head == null)
            {
                return NotFound("Promotion not found.");
            }
            try
            {
                var detail = await _context.tblPromotion1s.Where(x => x.ProEntry == key).ToListAsync();
                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data=new
                    {
                        header = head,
                        detail = detail,
                    }
                }); // Return
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

        }
        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet("get-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLogin(string usercode, string password)
        {
            int code = 200;
            string message = "";
            User user = new User();
            try
            {
                password = new EncryptDecrypt().Encrypt(password);
                var checkusercode = await _context.Users.Where(x => x.Code.ToLower() == usercode && (x.IsWebUser=="Web" || x.IsWebUser=="Both")).FirstOrDefaultAsync();
                if (checkusercode != null)
                {
                    if (checkusercode.Status == "Active")
                    {
                        var checkpassword = await _context.Users.Where(x => x.Code.ToLower() == usercode.ToLower() && x.Password == password).FirstOrDefaultAsync();
                        if (checkpassword != null)
                        {
                            HttpContext.Session.SetString("username", checkpassword.Code);
                            HttpContext.Session.SetString("username", checkpassword.Code);
                            message = "Success";
                            user = checkpassword;
                        }
                        else
                        {
                            code = 400;
                            message = "password not correct";
                        }
                    }
                    else
                    {
                        code = 400;
                        message = "User is inactive";
                    }
                }
                else
                {
                    code = 400;
                    message = "usercode not valid";
                }
            }
            catch (Exception ex)
            {
                code = 400;
                message = ex.Message;
            }
            return Ok(new
            {
                code = code,
                message = message,
                data = user
            });
        }
        [HttpGet("invoice-list")]
        [AllowAnonymous]
        public async Task<IActionResult> InvList()
        {
            var currentuser =  UserHelper.GetUserId(HttpContext);
            var salesList = _context.tblSalesEmployees
                .FromSqlRaw("EXEC Get_SalesMan_by_User @p0,@p1", currentuser,"").ToList();
            ViewBag.saleslist = salesList;
            return View();
        }
        [HttpGet("non-order")]
        [AllowAnonymous]
        public async Task<IActionResult> NonOrder()
        {
            var currentuser =  UserHelper.GetUserId(HttpContext);
            var salesList = _context.tblSalesEmployees
                .FromSqlRaw("EXEC Get_SalesMan_by_User @p0,@p1", currentuser,"").ToList();
            ViewBag.saleslist = salesList;
            return View();
        }
        [HttpGet("user-list")]
        [AllowAnonymous]
        public IActionResult UserList()
        {
            ViewBag.sales = _context.tblSalesEmployees.ToList();
            ViewBag.user = _context.Users.Where(x => x.UserType == "User" && x.IsWebUser=="Web").ToList();
            return View();
        }
        [HttpGet("search-user-list")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchUserList(int start = 0,int limit = 20,string status = "All",string type = "All",string userType="All",string search = "")
        {
            try
            {
                var currentuser = UserHelper.GetUserId(HttpContext);
                // Call stored procedure with parameters
                var result = await _context.Set<UserListResponse>()
                    .FromSqlRaw(
                        "EXEC ICC_Get_User_List @searchtype={0}, @search={1}, @status={2}, @usertype={3}, @currentuser={4}, @start={6}, @limit={7}",
                        type, search, status, userType, currentuser, start, start, limit
                    )
                    .AsNoTracking()
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Success",
                    data = result,
                    totalrow = result.Count > 0 ? result[0].TotalRow : 0
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("check-user")]

        public async Task<IActionResult> checkuser(string usercode)
        {
            try
            {
                var users = await _context.Users.Where(x => x.Code == usercode).ToListAsync();
                return Ok(new { data = users });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        //[HttpGet("get-user-list")]
        //public async Task< IActionResult> GetUserList()
        //{
        //    try
        //    {
        //        var users =await _context.Users.Where(x=>x.UserType=="User").OrderBy(x => x.Code).ToListAsync();
        //        return Ok(new { data=users});
        //    }catch(Exception ex)
        //    {
        //        return StatusCode(500, new { success = false, message = ex.Message });
        //    }
        //}
        [HttpPost("save-user")]
        public async Task<IActionResult> SaveUser([FromBody] User u)
        {
            try
            {
                var user =await _context.Users.Where(x => x.Code == u.Code).FirstOrDefaultAsync();
                if (user == null)
                {
                    user = u;

                    //user.Password = new EncryptDecrypt().Encrypt(u.Password);
                    user.Password = DMS_Controller.PasswordHasher.HashPassword(u.Password);
                    user.CreatedDate = DateTime.Now;
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = "add new user success", data = user });
                }
                else
                {
                    user.Name = u.Name;
                    user.IsWebUser = u.IsWebUser;
                    user.IsEndofDay = u.IsEndofDay;
                    user.SlpCode = u.SlpCode;
                    user.UserType = u.UserType;
                    user.DeviceID = u.DeviceID;
                    user.Manager = u.Manager;
                    user.UpdatedBy = u.CreatedBy;
                    user.UpdatedDate = DateTime.Now;
                    user.PrinterMac = u.PrinterMac;
                    user.PrinterName = u.PrinterName;

                    user.Status = u.Status;
                    if (!string.IsNullOrEmpty(u.Password))
                    {
                        //user.Password=new EncryptDecrypt().Encrypt(u.Password);
                        user.Password = DMS_Controller.PasswordHasher.HashPassword(u.Password);
                    }
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = "update user success", data = user });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        // Get Districts by Province
        [HttpGet("get-district")]
        public IActionResult GetDistricts(string proCode)
        {
            var districts = _context.v_Districts
                .Where(d => d.ProCode == proCode)
                .ToList();
            return Ok(districts);
        }

        // Get Communes by District
        [HttpGet("get-commune")]
        public IActionResult GetCommunes(string disCode)
        {
            var communes = _context.v_Communes
                .Where(c => c.DisCode == disCode)
                .ToList();
            return Ok(communes);
        }

        // Get Addresses by Province/District/Commune
        [HttpGet("get-address")]
        public IActionResult GetAddresses(string proCode, string disCode, string comCode)
        {
            var addresses = _context.tblAddresses
                .Where(a =>
                    (string.IsNullOrEmpty(proCode) || a.ProCode == proCode) &&
                    (string.IsNullOrEmpty(disCode) || a.DisCode == disCode) &&
                    (string.IsNullOrEmpty(comCode) || a.ComCode == comCode)
                )
                .ToList();

            return Ok(addresses);
        }
        [HttpPost("save-customer-request")]
        public async Task<IActionResult> SaveBPRequest([FromBody] tblBPRequest model)
        {
            if (model == null)
            {
                return BadRequest("invalid model");
            }
            try
            {
                var bp = await _context.tblBPRequests.Where(x => x.DocEntry == model.DocEntry).FirstOrDefaultAsync();
                if (bp == null)
                {
                    bp = model;
                    bp.BPSource = "Web";
                    bp.CreatedBy =  UserHelper.GetUserId(HttpContext);
                    bp.CreatedDate = DateTime.Now;
                    bp.Status = "Draft";
                    bp.SalesCode =  UserHelper.GetUserId(HttpContext);
                    await _context.tblBPRequests.AddAsync(bp);

                }
                else
                {
                    bp.CardName = model.CardName;
                    bp.CardFName = model.CardFName;
                    bp.Channel = model.Channel;
                    bp.GroupCode = model.GroupCode;
                    bp.TermCode = model.TermCode;
                    bp.ProCode = model.ProCode;
                    bp.DisCode = model.DisCode;
                    bp.ComCode = model.ComCode;
                    bp.VilName = model.VilName;
                    bp.AddressCode = model.AddressCode;
                    bp.FullAddEN = model.FullAddEN;
                    bp.FullAddKH = model.FullAddKH;
                    bp.VATNo = model.VATNo;
                    bp.UpdatedBy =  UserHelper.GetUserId(HttpContext);
                    bp.UpdatedDate = DateTime.Now;
                    bp.StreetNo = model.StreetNo;
                    bp.Region = model.Region;
                    bp.Phone1 = model.Phone1;
                    bp.Phone2 = model.Phone2;
                    bp.Phone3 = model.Phone3;
                    bp.Email = model.Email;
                    bp.HouseNo = model.HouseNo;
                    bp.DefPriceListCode = model.DefPriceListCode;
                    bp.SubGroup = model.SubGroup;
                    bp.Grade = model.Grade;
                    bp.Region = model.Region;

                }
                await _context.SaveChangesAsync();
                return Ok(
                    new
                    {
                        success=true,
                        message = "Success"
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    sucess=false,
                    message = ex.Message
                });
            }

        }
        [HttpPost("customer-request-action")]
        public async Task<IActionResult> CustomerRequestAction(int docentry, string action)
        {
            try
            {
                var bp = await _context.tblBPRequests
                    .FirstOrDefaultAsync(x => x.DocEntry == docentry);

                if (bp == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "BP not found in the system"
                    });
                }

                // Normalize action
                action = action?.Trim();

                if (action == "Approved")
                {
                    bp.Status = "Approved";
                    bp.SAPSyncStatus = "Pending";
                    bp.ConfimedBy =  UserHelper.GetUserId(HttpContext);
                    bp.ConfirmedDate = DateTime.Now;
                }
                else if (action == "Rejected")
                {
                    bp.Status = "Rejected";
                    bp.SAPSyncStatus = "Cancelled";
                    bp.ConfimedBy =  UserHelper.GetUserId(HttpContext);
                    bp.ConfirmedDate = DateTime.Now;
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid action"
                    });
                }

                _context.tblBPRequests.Update(bp);
                await _context.SaveChangesAsync(); // ✅ REQUIRED

                return Ok(new
                {
                    success = true,
                    message = $"BP has been {action} successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileModel model)
        {
            try
            {
                if (model.File == null || model.File.Length == 0)
                {
                    return Ok(new { code = 200, message = "No file uploaded" });
                }

                // ✅ Create target folder path (e.g., wwwroot/Attachments/{folder})
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments", model.Folder);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // ✅ Generate new filename format: DMS_yyyy_MM_dd_HH_mm_ss_originalfilename
                string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                string originalName = Path.GetFileName(model.File.FileName); // e.g. IMG_9528.jpg
                string newFileName = $"DMS_{timestamp}_{originalName.Replace(" ",string.Empty)}";

                string filePath = Path.Combine(uploadPath, newFileName);

                // ✅ Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                }
                var user =await _context.Users.Where(x => x.Code.ToString() == model.UserCode).FirstOrDefaultAsync();
                if (user!=null)
                {
                    user.Profile = newFileName;
                    await _context.SaveChangesAsync();
                }

                // ✅ Return relative path or filename if needed
                return Ok(new
                {
                    code = 200,
                    message = "File uploaded successfully!",
                    filename = newFileName,
                    path = $"/Attachments/{model.Folder}/{newFileName}"
                });
            }
            catch (Exception ex)
            {
                return Ok(new { code = 500, message = ex.Message });
            }
        }
        [HttpGet("import-visit-plan")]
        [AllowAnonymous]
        public IActionResult ImportVisitPlan()
        {
            return View();
        }
        // Download template
        [HttpGet("download-template")]
        [AllowAnonymous]
        public IActionResult DownloadTemplate()
        {
            // Path to your template file in wwwroot/templates folder
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","Attachments", "Templates", "V_Template.xlsx");
            if (!System.IO.File.Exists(templatePath))
            {
                return NotFound("Template not found.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(templatePath);
            return File(fileBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "VisitPlanTemplate.xlsx");
        }
        [HttpPost("upload-template")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadTemplate(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Save to wwwroot/uploads
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments", "Uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            string originalName = Path.GetFileName(file.FileName);
            string newFileName = $"DMS_{timestamp}_{originalName}";
            string filePath = Path.Combine(uploadFolder, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { fileName = newFileName, message = "File uploaded to disk successfully." });
        }
        [HttpPost("read-template")]
        [AllowAnonymous]
        public async Task<IActionResult> ReadTemplate([FromBody] ReadTemplateRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(request.FileName))
                    return BadRequest(new { status = "Failed", message = "File name cannot be empty." });

                if (request.FileName.Contains("..") || !Path.GetExtension(request.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    return BadRequest(new { status = "Failed", message = "Invalid file name or extension. Only .xlsx files are allowed." });

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Attachments", "Uploads");
                var filePath = Path.Combine(uploadFolder, request.FileName);

                if (!System.IO.File.Exists(filePath))
                    return BadRequest(new { status = "Failed", message = "File not found on disk." });

                using (var workbook = new XLWorkbook(filePath))
                {
                    if (workbook.Worksheets.Count == 0)
                        return BadRequest(new { status = "Failed", message = "Excel file contains no worksheets." });

                    var worksheet = workbook.Worksheet(1);
                    int rowCount = worksheet.RowsUsed().Count();
                    if (rowCount < 2)
                    {
                        // Send 0% progress for empty data
                        //await hubContext.Clients.Client(request.ConnectionId).SendAsync("ReceiveProgress", 0);
                        return BadRequest(new { status = "Failed", message = "Excel file contains no data rows." });
                    }

                    // Validate header
                    if (worksheet.Cell(1, 1).GetString() != "SaleCode" ||
                        worksheet.Cell(1, 2).GetString() != "DocYear" ||
                        worksheet.Cell(1, 3).GetString() != "VisitDate" ||
                        worksheet.Cell(1, 4).GetString() != "CardCode" ||
                        worksheet.Cell(1, 5).GetString() != "CardName" ||
                        worksheet.Cell(1, 6).GetString() != "Action")
                    {
                        return BadRequest(new { status = "Failed", message = "Invalid Excel structure. Expected columns: SaleCode, DocYear, VisitDate, CardCode, CardName." });
                    }

                    int totalRows = rowCount - 1; // Exclude header
                    int processedRows = 0;
                    string saleCode="";
                    // Send initial 0% progress
                    //await hubContext.Clients.Client(request.ConnectionId).SendAsync("ReceiveProgress", 0);

                    using var transaction = await _context.Database.BeginTransactionAsync();
                    string unmapp = "";
                    try
                    {
                        for (int row = 2; row <= rowCount; row++)
                        {
                            saleCode = worksheet.Cell(row, 1).GetString();
                            var docYear = worksheet.Cell(row, 2).GetString();
                            var visitDateText = worksheet.Cell(row, 3).GetString();
                            var cardCode = worksheet.Cell(row, 4).GetString();
                            var cardName = worksheet.Cell(row, 5).GetString();
                            var action = worksheet.Cell(row, 6).GetString();
                            if (string.IsNullOrEmpty(saleCode) || string.IsNullOrEmpty(cardCode))
                            {
                                processedRows++;
                                int percentage0 = totalRows > 0 ? (int)((double)processedRows / totalRows * 100) : 100;
                                //await hubContext.Clients.Client(request.ConnectionId).SendAsync("ReceiveProgress", percentage0);
                                continue;
                            }

                            if (!DateTime.TryParseExact(visitDateText, "dd-MM-yyyy",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out var visitDate))
                            {
                                visitDate = DateTime.Now;
                            }

                            // Validate SaleCode
                            var checksale = await _context.tblSalesEmployees
                                .FirstOrDefaultAsync(x => x.U_SalesCode == saleCode);
                            if (checksale == null)
                                return NotFound(new { status = "Failed", message = $"SaleCode '{saleCode}' not found in database." });

                            // Check or create VisitH
                            var checkheader = await _context.VisitHs
                                .FirstOrDefaultAsync(x => x.DocYear.ToString() == docYear && x.SalesCode == checksale.SlpCode);

                            if (checkheader == null)
                            {
                                if (!int.TryParse(docYear, out var year))
                                    year = DateTime.Now.Year;

                                checkheader = new VisitH
                                {
                                    DocYear = year,
                                    SalesCode = checksale.SlpCode,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = request.CreatedBy ?? "SYSTEM",
                                    Status = "Active"
                                };

                                var docnumnumber = _context.DocumentNumbers
                                    .FromSqlRaw("EXEC [dbo].[ICC_Get_Last_Visitor_DocNum]")
                                    .AsEnumerable()
                                    .FirstOrDefault();

                                if (docnumnumber == null)
                                    return StatusCode(500, new { status = "Failed", message = "Failed to retrieve DocNum from stored procedure." });

                                //checkheader.DocNum = docnumnumber.docnum;
                                checkheader.DocNum = checksale.U_SalesCode + "-" + checkheader.DocYear.ToString();
                                _context.VisitHs.Add(checkheader);
                                await _context.SaveChangesAsync(); // Save header to generate DocEntry
                            }

                            // Check if detail exists
                            var existingDetail = await _context.VisitDs.FirstOrDefaultAsync(d =>
                                d.DocEntry == checkheader.DocEntry &&
                                d.VisitDate == DateOnly.FromDateTime(visitDate) &&
                                d.CardCode == cardCode);
                            if (existingDetail == null)
                            {
                                if (action.ToLower() == "a")
                                {
                                    var detail = new VisitD
                                    {
                                        DocEntry = checkheader.DocEntry,
                                        VisitDate = DateOnly.FromDateTime(visitDate),
                                        CardCode = cardCode
                                    };
                                    _context.VisitDs.Add(detail);
                                }
                            }
                            else
                            {
                                if (action.ToLower() == "u")
                                {
                                    existingDetail.VisitDate = DateOnly.FromDateTime(visitDate);
                                    _context.VisitDs.Update(existingDetail);
                                }
                                if (action.ToLower() == "d")
                                {
                                    _context.VisitDs.Remove(existingDetail);
                                }
                            }
                            processedRows++;
                            int percentage = totalRows > 0 ? (int)((double)processedRows / totalRows * 100) : 100;
                            //await hubContext.Clients.Client(request.ConnectionId).SendAsync("ReceiveProgress", percentage);
                        }
                        await _context.SaveChangesAsync();
                        if (unmapp == "")
                        {
                            await transaction.CommitAsync();
                        }
                        else
                        {
                            await transaction.RollbackAsync();
                        }

                        // Final progress update
                        //await hubContext.Clients.Client(request.ConnectionId).SendAsync("ReceiveProgress", 100);
                        if (unmapp != "")
                        {
                            return Ok(new { status="Failed", message = "Some customer codes do not match this sales employee (" + saleCode + ") " + unmapp });
                        }
                        else
                        {
                            return Ok(new { status = "OK", message = "File read and data inserted successfully." });
                        }
                    }
                    catch (DbUpdateException dbEx)
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(500, new
                        {
                            message = "Database error while saving changes.",
                            error = dbEx.InnerException?.Message ?? dbEx.Message
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while processing the file.",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }



    }

}
