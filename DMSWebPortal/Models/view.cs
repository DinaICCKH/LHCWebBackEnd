namespace DMSWebPortal.Models
{
    public class ICC_Get_BP_Request_Data
    {
        // 🔢 Paging
        public long? RowNumber { get; set; }
        public int? TotalRow { get; set; }
        public int? DocEntry { set; get; }

        // 🧾 BP Information
        public string? AppCode { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? CardFName { get; set; }

        // 📞 Contact
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Phone3 { get; set; }
        public string? Email { get; set; }

        // 📍 Address
        public string? ProCode { get; set; }
        public string? DisCode { get; set; }
        public string? ComCode { get; set; }
        public string? VilName { get; set; }
        public string? FullAddEN { get; set; }
        public string? FullAddKH { get; set; }

        // 🌍 Location
        public string? GPSLateLong { get; set; }
        public string? Zone { get; set; }

        // 👤 Sales
        public string? SalesCode { get; set; }
        public string? SlpName { get; set; } = string.Empty;

        // 🖼 Image
        public string? ImagePath { get; set; }        // Local path (optional)
        public string? ImageUrlServer { get; set; }   // Public URL

        // 🗓 Dates
        public DateTime? CreatedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }

        // 🔄 Status
        public string? Status { get; set; }            // Unsync / Synced / Failed
        public string? LastError { get; set; }

        // 📝 Remark
        public string? Remark { get; set; }
    }


    public class ICC_Get_BP_Master_Data
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
        public string? FullAddEN { get; set; }
        public string? RegionalName { get; set; }
    }
    public class LastEndDay
    {
        public string SalesCode { get; set; }
        public DateTime? EndDay { get; set; }
        public string Bank { get; set; }
        public double? CashUSD { get; set; }
        public double? CashKHR { get; set; }
        public string? Remark { get; set; }
    }
    public class v_salmapping
    {
        public int slpcode { get; set; }
        public List<v_salitem> listsalitems { get; set; }
        public List<v_salregion> listsalregions { get; set; }
    }
    public class v_salitem
    {
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public int SlpCode { get; set; }
        public string ItemCode { get; set; }
        public string DocStatus { get; set; }
    }
    public class v_salregion
    {
        public int DocEntry { get; set; }
        public int LineNum { get; set; }
        public int SlpCode { get; set; }
        public string Region { get; set; }
        public string DocStatus { get; set; }
    }
    public class v_promocal
    {
        public string? Item { get; set; }
        public string? UoM { get; set; }
        public string? GetProType { get; set; }
        public double? LineDisPer { get; set; }
        public double? LineDisAmt { get; set; }
        public double? DocDisPer { get; set; }
        public double? DocDisAmt { get; set; }
        public int UoMEntry { get; set; }
        public string? WhsCode { get; set; }
        public string? WhsName { get; set; }
    }
    public class v_OrderSAP
    {
        public int? AppId { get; set; }
        public string? SalesCode { get; set; }
        public string? SAPDocNo { get; set; }

    }
    public class v_Income
    {
        public int? DocEntry { get; set; }
        public int? SODocEntry { get; set; }
        public string? BankCode { get; set; }
        public decimal? BankAmount { get; set; }
        public decimal? CashAmount { get; set; }
        public string? CurCode { get; set; }
        public string? CardCode { get; set; }
        public DateOnly? DocDate { get; set; }
        public decimal? Rate { get; set; }
        public int? AREntry { get; set; }
        public string? BankGL { get; set; }
        public string? CashGL { get; set; }
        public int? SAPDocEntry { get; set; }
        public string? LastError { get; set; }
    }
    public class v_Order_Status
    {
        public int? DocEntry { get; set; }
        public string? BaseType { get; set; }
        public string? DocStatus { get; set; }
        public string? DocNo { get; set; }
    }
    public class v_Order
    {
        public int? DocEntry { get; set; }
        public string? DocNo { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public int? ContactPer { get; set; }
        public string? DelAddress { get; set; }
        public DateOnly? DocDate { get; set; }
        public DateOnly? DueDate { get; set; }
        public DateOnly? TaxDate { get; set; }
        public string? DocCur { get; set; }
        public decimal? DocRate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? AfterDis { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? Total { get; set; }
        public string? SalesCode { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AppId { get; set; }
        public string? AppDocNo { get; set; }
        public string? APIStatus { get; set; }
        public string? APIErrMessage { get; set; }
        public string? VATStatus { get; set; }
        public string? DocStatus { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string? CheckInLateLong { get; set; }
        public string? CheckInRemark { get; set; }
        public string? ImageURL { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string? CheckOutLateLong { get; set; }
        public string? CheckOutRemark { get; set; }
        public int? Branch { get; set; }
        public int? SlpCode { get; set; }
        public string? SalType { get; set; }
        public string? VATType { get; set; }
        public List<v_Order1>? order1 { get; set; }
    }
    public class v_Order1
    {
        public int? DocEntry { get; set; }
        public int? LineNum { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public int? UoMEntry { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? LineTotal { get; set; }
        public string? WhsCode { get; set; }
        public int? RefLineNum { get; set; }
        public string? SalType { get; set; }
        public string? OcrCode { get; set; }
        public string? OcrCode2 { get; set; }
        public string? OcrCode3 { get; set; }
        public string? OcrCode4 { get; set; }
        public string? OcrCode5 { get; set; }
        public string? VATGroup { get; set; }
        public string? ProCode { get; set; }
        public int? ProLineNo { get; set; }
        public string? PromotionType { get; set; }
        public string? UoMCode { get; set; }
    }
    public class v_RunPromotion
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string ItemName { get; set; }
        public string UoM { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalAmt { get; set; }
        public string GetProType { get; set; }
        public decimal LineDisPer { get; set; }
        public decimal LineDisAmt { get; set; }
        public decimal DocDisPer { get; set; }
        public decimal DocDisAmt { get; set; }
        public string PromotionGroup { get; set; }
        public string PromotionType { get; set; }
        public int UoMEntry { get; set; }
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public int ProEntry { get; set; }
        public int ProLineNum { get; set; }

    }
    public class v_Item
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public int UgpEntry { get; set; }
        public decimal Onhand { get; set; }
        public decimal OnOrder { get; set; }
        public decimal IsCommited { get; set; }
        public decimal Available { get; set; }
        public decimal MinLevel { get; set; }
        public decimal MaxLevel { get; set; }
        public string Status { get; set; }
        public string ImageUrlServer { get; set; }
        public string ImageUrlLocal { get; set; }
        public string FrgnName { get; set; }
        public string InvUoMcode { get; set; }
        public int InvUoMentry { get; set; }
        public string UProGroup { get; set; }
        public string HasPromotion { get; set; }
        public string OcrCode { get; set; }
        public string OcrCode2 { get; set; }
        public string OcrCode3 { get; set; }
        public string OcrCode4 { get; set; }
        public string PrincipleCode { get; set; }
        public string MainCat { get; set; }
        public string SubCat { get; set; }
        public string PackageType { get; set; }
        public string BarCode { get; set; }
        public int DefEntry { get; set; }
        public decimal AltQty { get; set; }
        public List<v_ItemPricing> Pricing { get; set; }
        public List<v_ItemStock> stocks { get; set; }
        public List<v_UoMgroup> UoMgroups { get; set; }
    }
    public class v_ItemPricing
    {
        /// <summary>
        /// ItemCode+PriceListCode+UoMEntry=Primary Key
        /// </summary>
        public string PricingKey { get; set; }
        public string ItemCode { get; set; }
        public int PriceListCode { get; set; }
        public decimal Amount { get; set; }
        public int UoMentry { get; set; }
    }
    public class v_ItemStock
    {
        /// <summary>
        /// ItemCode+WhsCode=Primary Key
        /// </summary>
        public string ItemStockKey { get; set; }
        public string ItemCode { get; set; }
        public string WhsCode { get; set; }
        public decimal Onhand { get; set; }
        public decimal OnOrder { get; set; }
        public decimal IsCommited { get; set; }
        public decimal Available { get; set; }
        public decimal MinStock { get; set; }
        public decimal MaxStock { get; set; }
        public decimal AltQty { get; set; }
    }
    public class v_UoMgroup
    {
        /// <summary>
        /// UgpEntry+UoMEntry=Primary Key
        /// </summary>
        public string UgpKey { get; set; }
        public int UgpEntry { get; set; }
        public string UgpName { get; set; }
        public int UoMentry { get; set; }
        public string UoMcode { get; set; }
        public decimal BaseQty { get; set; }
        public decimal AltQty { get; set; }
    }
    public class v_Bp
    {
        public string CardCode { get; set; }

        public string CardName { get; set; }

        public string CardFname { get; set; }

        public string ContactPer { get; set; }

        public short GroupCode { get; set; }

        public string Phone { get; set; }

        public string Vatno { get; set; }

        public short DefPriceListCode { get; set; }

        public decimal CreditLimited { get; set; }

        public short TermCode { get; set; }

        public string TermName { get; set; }

        public string Status { get; set; }

        public string GpslateLong { get; set; }

        public decimal Balance { get; set; }

        public string ImagePath { get; set; }

        public string ImageUrlServer { get; set; }

        public string Channel { get; set; }

        public string Region { get; set; }

        public string ProCode { get; set; }

        public string DisCode { get; set; }

        public string ComCode { get; set; }

        public string VilName { get; set; }

        public string AddressCode { get; set; }

        public string FullAddKh { get; set; }

        public string FullAddEn { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string ConfimedBy { get; set; }

        public DateTime ConfirmedDate { get; set; }

        public string Bpsource { get; set; }

        public string Bprkey { get; set; }

        public string Sync { get; set; }

        public string Territory { get; set; }

        public string AppCode { get; set; }

        public string Vatimage { get; set; }

        public string SubZone { get; set; }

        public string AllowDown { get; set; }

        public string Zone { get; set; }
        public List<v_Bpcontact> ContactList { get; set; }
    }
    public class v_Bpcontact
    {
        public int ContactCode { get; set; }

        public string CardCode { get; set; } = null!;

        public string? Tel1 { get; set; }

        public string Status { get; set; } = null!;

        public string ContactName { get; set; } = null!;
    }
    public class v_Promotion
    {
        public int ProEntry { get; set; }
        public string PrincipleCode { get; set; }
        public string PrincipleDesc { get; set; }
        public string DocStatus { get; set; }
        public DateOnly Fdate { get; set; }
        public DateOnly Tdate { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<v_Promotion1> Pro1_List { get; set; }
    }
    public class v_Promotion1
    {
        public int ProEntry { get; set; }
        public int LineNum { get; set; }
        public int? ItemGroupCode { get; set; }
        public string? ItemGroupName { get; set; }
        public string? PackType { get; set; }
        public string? PromotionGroup { get; set; }
        public int? Bpgroup { get; set; }
        public string? BpgroupName { get; set; }
        public string? BpchannelCode { get; set; }
        public string? BpchannelName { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? Combine { get; set; }
        public string? PromotionType { get; set; }
        public string? Foctype { get; set; }
        public string? Condition { get; set; }
        public decimal? BuyAmt { get; set; }
        public decimal? BuyQty { get; set; }
        public string? BuyUoM { get; set; }
        public decimal? Focqty { get; set; }
        public string? FocuoM { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmt { get; set; }
        public string? Remark { get; set; }
        public DateOnly? ValidFrom { get; set; }
        public DateOnly? ValidTo { get; set; }
        public string? LineStatus { get; set; }
    }
}
