using DMSWebPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;

namespace DMSWebPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MarketingController(AppDbContext db)
        {
            _db = db;
        }

        // POST: Add / Update PO
        [HttpPost]
        public async Task<IActionResult> AddOrUpdatePO([FromBody] PO model)
        {
            try
            {
                var token = new { UserId = 1, UserName = "Admin", CompanyId = 1001 }; // later replace with real JWT user
                string mode = model.Mode ?? "Add";

                // ✅ Build JSON for SP
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    Mode = mode,
                    model.DocEntry,
                    model.Branch,
                    model.CardCode,
                    model.CardName,
                    model.PostingDate,
                    model.DueDate,
                    model.ExchangeRate,
                    model.VATStatus,
                    model.PaymentTerm,
                    model.SaleCode,
                    model.Remark,
                    model.SubTotal,
                    model.DPMAmount,
                    model.VATAmount,
                    model.DiscountPercent,
                    model.DiscountAmount,
                    model.PaidAmount,
                    model.DocAmount,
                    model.DocStatus,
                    model.SAPCode,
                    model.SAPStatus,
                    model.APIStatus,
                    model.APIErrorMessage,
                    model.DoCur,
                    CreateBy = token.UserId,
                    UpdateBy = token.UserId,

                    PO1 = model.PODetails?.Select(d => new
                    {
                        d.DocEntry,
                        d.LineNum,
                        d.BaseType,
                        d.BaseEntry,
                        d.BaseLineNum,
                        d.ItemCode,
                        d.ItemName,
                        d.UOM,
                        d.Quantity,
                        d.Price,
                        d.DiscountPercent,
                        d.DiscountAmount,
                        d.VAT,
                        d.LineAmount,
                        d.Warehouse,
                        d.OcrCode,
                        d.OcrCode2,
                        d.OcrCode3,
                        d.OcrCode4,
                        d.Project,
                        d.LineStatus,
                        d.Remark,
                        d.IsFather,
                        d.FatherCode
                    })
                });

                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw("EXEC dbo.ControllerPO @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "PO"),
                        new SqlParameter("@TranType", mode),
                        new SqlParameter("@EntryPrimary", model.DocEntry ?? ""),
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
                        primaryKey = result.PrimaryKey // ✅ Return generated DocEntry
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing PO"
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

        // POST: Add / Update SO
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateSO([FromBody] Tbl_SO model)
        {
            try
            {
                var token = new { UserId = 1, UserName = "Admin", CompanyId = 1001 }; // replace with real JWT info


                // ✅ Build JSON for SP
                var jsonBody = JsonConvert.SerializeObject(new
                {
                    model.Mode,
                    model.DocEntry,
                    model.BPLId,
                    model.BPLName,
                    model.CANCELED,
                    model.DocStatus,
                    model.DocDate,
                    model.DocDueDate,
                    model.U_DeliveryTime,
                    model.CardCode,
                    model.CardName,
                    model.Address,
                    model.NumAtCard,
                    model.VatSum,
                    model.DiscPrcnt,
                    model.DiscSum,
                    model.SubTotal,
                    model.DocTotal,
                    model.PaidToDate,
                    model.Ref1,
                    model.Ref2,
                    model.Comments,
                    model.U_PaymentMethod,
                    model.U_Owner,
                    model.CreateDate,
                    model.UserSign,
                    model.UserSign2,
                    CreateBy = token.UserId,
                    UpdateBy = token.UserId,

                    SO1 = model.SO1Lines?.Select(d => new
                    {
                        d.DocEntry,
                        d.LineNum,
                        d.ItemCode,
                        d.Dscription,
                        d.Quantity,
                        d.UomCode,
                        d.UnitMsr,
                        d.Price,
                        d.DiscPrcnt,
                        d.DisAmt,
                        d.TaxCode,
                        d.LineTotal,
                        d.WhsCode,
                        d.OcrCode,
                        d.OcrCode2,
                        d.OcrCode3,
                        d.OcrCode4,
                        d.U_InvPaymentAmt,
                        d.U_PaymentPer,
                        d.U_PaymentAmt,
                        d.U_InvDiscountPer,
                        d.U_InvDicountAmt,
                        d.U_DiscPer,
                        d.U_DiscAmt,
                        d.U_InvVoucherAmt,
                        d.U_Voucher,
                        d.U_VoucherNo,
                        d.U_InvTransportAmt,
                        d.U_TransportationPercent,
                        d.U_TransportationAmt,
                        d.U_InvSpecialAmt,
                        d.U_specialPricePercent,
                        d.U_specialPriceAmt,
                        d.U_PolicyDisc,
                        d.U_InvTransportPer,
                        d.U_InvSpecialPer,
                        d.U_InvSpecialFreeAmt,
                        d.U_InvPaymentPer,
                        d.U_AddOnStatus,
                        d.U_InvTransprtFAmt,
                        d.U_InvCurrency,
                        d.U_MnCurrency,
                        d.U_RemarkCurrency,
                        d.U_InvFactory,
                        d.U_MnFactory,
                        d.U_RemarkFactory,
                        d.U_InvTransportB7,
                        d.U_MnTransportB7,
                        d.U_RemarkTransportB7,
                        d.U_InvTransportB8,
                        d.U_MnTransportB8,
                        d.U_RemarkTransportB8,
                        d.U_InvEmployeeCom,
                        d.U_MnEmployeeCom,
                        d.U_RemarkEmployeeCom,
                        d.U_InvDepotCom,
                        d.U_MnDepotCom,
                        d.U_RemarkDepotCom,
                        d.U_InvQuarterCom,
                        d.U_MnQuarterCom,
                        d.U_RemarkQuarterCom,
                        d.U_InvMarketing,
                        d.U_MnMarketing,
                        d.U_RemarkMarketing,
                        d.U_InOther9,
                        d.U_MnOther9,
                        d.U_RemarkOther9,
                        d.U_InOther10,
                        d.U_MnOther10,
                        d.U_RemarkOther10,
                        d.U_InOther11,
                        d.U_MnOther11,
                        d.U_RemarkOther11,
                        d.U_InOther12,
                        d.U_MnOther12,
                        d.U_RemarkOther12,
                        d.U_SpecialTrAmt,
                        d.U_SpecialTrnPer,
                        d.U_QtyFactory
                    })
                });

                // Execute stored procedure
                var resultList = await _db.Set<SpResult>()
                    .FromSqlRaw(
                        "EXEC dbo.ControllerSO @MasterType, @TranType, @EntryPrimary, @JsonBody",
                        new SqlParameter("@MasterType", "SO"),
                        new SqlParameter("@TranType", model.Mode),
                        new SqlParameter("@EntryPrimary", model.DocEntry.ToString()),  
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
                        primaryKey = result.PrimaryKey // ✅ Return generated DocEntry
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result?.Message ?? "Error processing SO"
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
