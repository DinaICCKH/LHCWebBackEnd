using DocumentFormat.OpenXml.Drawing;

namespace DMSWebPortal.Models
{
    public class PlanTrackingResult
    {
        public long RowNumber { get; set; }              // always returned by ROW_NUMBER()
        public int TotalRow { get; set; }               // always returned by COUNT()

        public int? DocYear { get; set; }            // nvarchar (nullable)
        public int DocEntry { get; set; }               // int (PK, not nullable)
        public string? DocNum { get; set; }             // nvarchar (nullable)

        public int? SalesCode { get; set; }             // int (can be null from join)
        public string? U_SalesCode { get; set; }        // nvarchar (nullable)
        public string? SlpName { get; set; }            // nvarchar (nullable)

        public string? Remark { get; set; }             // nvarchar (nullable, default is '')
        public DateTime? CreatedDate { get; set; }       // datetime (not nullable in VisitH)
        public string? CreatedBy { get; set; }             // int (nullable join to Users)
        public string? Creator { get; set; }            // nvarchar (nullable)

        public DateTime? UpdatedDate { get; set; }      // datetime (nullable)
        public string? UpdatedBy { get; set; }             // int (nullable)
        public string? UpdateName { get; set; }         // nvarchar (nullable)

        public string? Status { get; set; }             // nvarchar (nullable, default 'Active')
        public string? WhsCode { get; set; }            // nvarchar (nullable)
        public string? Area { get; set; }               // nvarchar (nullable)
        public string? WhsName { get; set; }            // nvarchar (nullable)
    }
    public class BPMasterDataResult
    {
        public long RowNumber { get; set; }
        public int TotalRow { get; set; }

        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public short? GroupCode { get; set; }
        public string? GroupName { get; set; }
        public string? Phone1 { get; set; }
        public string? LicTradNum { get; set; }
        public short? ListNum { get; set; }
        public decimal? CreditLimite { get; set; }
        public short? TermCode { get; set; }
        public string? TermName { get; set; }
        public string? Status { get; set; }
        public string? LateLong { get; set; }
        public decimal? Balance { get; set; }
        public string? ImageUrlServer { get; set; }
        public string? CardFName { get; set; }
        public int? SlpCode { get; set; }
        public string? SlpName { get; set; }
        //public string? FullAddEN { get; set; }
    }
    public class ItemMasterDataResult
    {
        public long? RowNumber { get; set; }
        public int? TotalRow { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public short? ItemGroupCode { get; set; }       // int matches SQL int
        public string? ItemGroupName { get; set; }
        public int? UgpEntry { get; set; }            // int matches SQL int
        public decimal? Onhand { get; set; }
        public decimal? OnOrder { get; set; }
        public decimal? IsCommited { get; set; }
        public decimal? Available { get; set; }
        public decimal? MinLevel { get; set; }
        public decimal? MaxLevel { get; set; }
        public string? Status { get; set; }
        public string? ImageUrlServer { get; set; }
        public string? ImageUrlLocal { get; set; }
        public string? UgpEntryList { get; set; }
        public string? FrgnName { get; set; }
        public string? InvUoMCode { get; set; }
        public int? InvUoMEntry { get; set; }         // int matches SQL int
        public string? U_ProGroup { get; set; }
        public string? HasPromotion { get; set; }
        public int? DefEntry { get; set; }
        public string? PrincipleCode { get; set; }
        public string? PrincipleName { get; set; }
        public string? MainCatCode { get; set; }
        public string? MainCatName { get; set; }
        public string? SubCatCode { get; set; }
        public string? SubCatName { get; set; }
    }

    public class DailyplanreportResult
    {

        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? Phone { get; set; }
        public string? FullAddEn { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string? DocStatus { get; set; }
        public string? Duration { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
    }

    public class SaleEmployeeMasterDataResult
    {
        public long? RowNumber { get; set; }     // BIGINT from SQL
        public int? TotalRow { get; set; }      // BIGINT from SQL
        public string? SlpCode { get; set; }
        public string? SalesName { get; set; }
        public string? SalesCode { get; set; }
        public string? Whs { get; set; }
        public string? WhsName { get; set; }
        public string? Status { get; set; }
        public string? SALType { get; set; }
    }
    public class DocumentNumber
    {
        public string? docnum { get; set; }
    }
    public class UomListByItemCodeResult
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public int? UomEntry { get; set; }
        public string? UomCode { get; set; }
        public string? UomName { get; set; }
    }
    public class vOnecolumm
    {
        public string? Approver { get; set; }
    }
    public class NoneSaleOrderListResult
    {
        public long? RowNumber { get; set; }     // BIGINT from SQL
        public int? TotalRow { get; set; }      // BIGINT from SQL

        public int? DocEntry { get; set; }
        public string? DocNo { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? Remark { get; set; }
        public DateOnly? DocDate { get; set; }
        public string? SalesCode { get; set; }
        public string? SalesName { get; set; }
        public string? DocStatus { set; get; }
        public string? Reasons { set; get; }
        public string? ReasonRemark { set; get; }
        public string? NextApprover { set; get; }

    }
    public class SaleOrderListResult
    {
        public long? RowNumber { get; set; }     // BIGINT from SQL
        public int? TotalRow { get; set; }      // BIGINT from SQL

        public int? DocEntry { get; set; }
        public string? DocNo { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? Remark { get; set; }
        public DateOnly? DocDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public string? SalesCode { get; set; }
        public string? SalesName { get; set; }
        public int? SAPDocEntry { get; set; }
        public string? SAPDocNum { get; set; }
        public int? AppId { set; get; }
        public string? AppDocNo { set; get; }
        public string? SAPSyncStatus { set; get; }
        public string? SAPLastError { set; get; }
        public decimal? Total { set; get; }
        public string? DocStatus { set; get; }
        public string? APIErrMessage { set; get; }
        public string? NextApprover { get; set; }
        public string? CreatedBy { get; set; }
        public string? Source { get; set; }

    }
    public class PromotionListResult
    {
        public long? RowNumber { get; set; }         // ROW_NUMBER()
        public int? TotalRow { get; set; }           // COUNT() OVER()

        public int ProEntry { get; set; }            // t0.ProEntry
        public string? Code { get; set; }            // t0.PrincipleCode
        public string? U_PromoType { get; set; }     // t0.PrincipleCode
        public string? U_PromoStatus { get; set; }   // t0.DocStatus
        public string? U_PromoFrmDate { get; set; }  // formatted FDate
        public string? U_PromoToDate { get; set; }   // formatted TDate
        public string? PrincipleDesc { get; set; }   // t0.PrincipleDesc
        public string? U_ProDesc { get; set; }       // t0.PrincipleDesc
    }
    public partial class VisitPlanDto
    {
        public VisitH Header { set; get; }
        public List<VisitD> Detail { set; get; }
    }
    public class SaleOrder
    {
        public SO Header { set; get; }
        public List<SO1> Detail { set; get; }

    }
    public class UploadFileModel
    {
        public string UserCode { get; set; }
        public string Folder { get; set; }
        public string CreatedBy { get; set; }
        public IFormFile File { get; set; }
    }
    public static class UrlHelper
    {
        public static string GetBaseUrl(HttpContext context)
        {
            var request = context.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}";
        }
    }
    public class ReadTemplateRequest
    {
        public string FileName { get; set; }
        public string CreatedBy { get; set; }
        public string ConnectionId { get; set; } // Added for SignalR
    }
    public class VNotification
    {
        public int NotificationId { get; set; }
        public int? RecipientId { get; set; }
        public string? RecipientType { get; set; }
        public int? SenderId { get; set; }
        public string? SenderType { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
        public int? RelatedEntityId { get; set; }
        public string? RelatedEntityType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsViewed { get; set; }
        public bool? IsGlobal { get; set; }
        public string? TimeAgo { get; set; }  // <-- from CASE expression
        public string? DocNo { get; set; }
        public string? Remark { get; set; }
        public string? CheckInRemark { get; set; }
        public string? CheckOutRemark { get; set; }
    }
    public class PriceApprovalResult
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? UoMCode { get; set; }
        public decimal? CostPerUOM { get; set; }
        public decimal? OrderQtyUOM { get; set; }
        public decimal? SalePrice { get; set; }
        public string? ApprovalStatus { get; set; }
    }
    public class ICC_Get_Order_App_Status_Result
    {
        public int? DocEntry { get; set; }
        public string? DocStatus { get; set; }

    }
    public class Api_SAP_Get_Available_BPRequest_Result{
        public int DocEntry { get; set; }
        public string? BPKey { get; set; }
        public string? CardCode { get; set; }
        public int? AppCode { get; set; }
        public string? CardName { get; set; }
        public string? CardFName { get; set; }
        public string? ContactPer { get; set; }
        public short? GroupCode { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? VATNo { get; set; }
        public short? DefPriceListCode { get; set; }
        public decimal? CreditLimited { get; set; }   // numeric(2,2) → decimal
        public short? TermCode { get; set; }
        public string? Status { get; set; }
        public string? GPSLateLong { get; set; }
        public string? ImagePath { get; set; }
        public string? Channel { get; set; }
        public string? SubGroup { get; set; }
        public string? Region { get; set; }
        public string? ProCode { get; set; }
        public string? DisCode { get; set; }
        public string? ComCode { get; set; }
        public string? VilName { get; set; }
        public string? AddressCode { get; set; }
        public string? FullAddKH { get; set; }
        public string? FullAddEN { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? ConfimedBy { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string? BPSource { get; set; }
        public string? SalesCode { get; set; }
        public string? IsVAT { get; set; }
        public string? HouseNo { get; set; }
        public string? StreetNo { get; set; }
        public string? LastError { get; set; }        // nvarchar(max) → string
        public string? Email { get; set; }
        public string? VATImage { get; set; }
        public string? JsonRemark { get; set; }
        public string? SAPSyncStatus { set; get; }
        public string? Grade { set; get; }
    }
    public class ICC_API_Get_AlertsResult
    {
        public int DocEntry { get; set; }
        public string? DocNum { get; set; }
        public string? DocType { get; set; }
        public string? DocStatus { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LinkRef { get; set; }
    }
    public class SetAlertRequest
    {
        public int DocEntry { get; set; }
        public string? DocType { get; set; } = "";
    }
}
