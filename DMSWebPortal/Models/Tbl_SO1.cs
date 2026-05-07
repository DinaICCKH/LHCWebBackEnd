namespace DMSWebPortal.Models
{
    public class Tbl_SO1
    {
        public string? DocEntry { get; set; } // allow null for new lines
        public int LineNum { get; set; }
        public string? ItemCode { get; set; }
        public string? Dscription { get; set; }
        public decimal? Quantity { get; set; }
        public string? UomCode { get; set; }
        public string? UnitMsr { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscPrcnt { get; set; }
        public decimal? DisAmt { get; set; }
        public string? TaxCode { get; set; }
        public decimal? LineTotal { get; set; }
        public string? WhsCode { get; set; }
        public string? OcrCode { get; set; }
        public string? OcrCode2 { get; set; }
        public string? OcrCode3 { get; set; }
        public string? OcrCode4 { get; set; }

        // Updated U_ fields as strings
        public string? U_InvPaymentAmt { get; set; }
        public string? U_PaymentPer { get; set; }
        public string? U_PaymentAmt { get; set; }
        public string? U_InvDiscountPer { get; set; }
        public string? U_InvDicountAmt { get; set; }
        public string? U_DiscPer { get; set; }
        public string? U_DiscAmt { get; set; }
        public string? U_InvVoucherAmt { get; set; }
        public string? U_Voucher { get; set; }
        public string? U_VoucherNo { get; set; }
        public string? U_InvTransportAmt { get; set; }
        public string? U_TransportationPercent { get; set; }
        public string? U_TransportationAmt { get; set; }
        public string? U_InvSpecialAmt { get; set; }
        public string? U_specialPricePercent { get; set; }
        public string? U_specialPriceAmt { get; set; }
        public string? U_PolicyDisc { get; set; }
        public string? U_InvTransportPer { get; set; }
        public string? U_InvSpecialPer { get; set; }
        public string? U_InvSpecialFreeAmt { get; set; }
        public string? U_InvPaymentPer { get; set; }
        public string? U_AddOnStatus { get; set; }
        public string? U_InvTransprtFAmt { get; set; }
        public string? U_InvCurrency { get; set; }
        public string? U_MnCurrency { get; set; }
        public string? U_RemarkCurrency { get; set; }
        public string? U_InvFactory { get; set; }
        public string? U_MnFactory { get; set; }
        public string? U_RemarkFactory { get; set; }
        public string? U_InvTransportB7 { get; set; }
        public string? U_MnTransportB7 { get; set; }
        public string? U_RemarkTransportB7 { get; set; }
        public string? U_InvTransportB8 { get; set; }
        public string? U_MnTransportB8 { get; set; }
        public string? U_RemarkTransportB8 { get; set; }
        public string? U_InvEmployeeCom { get; set; }
        public string? U_MnEmployeeCom { get; set; }
        public string? U_RemarkEmployeeCom { get; set; }
        public string? U_InvDepotCom { get; set; }
        public string? U_MnDepotCom { get; set; }
        public string? U_RemarkDepotCom { get; set; }
        public string? U_InvQuarterCom { get; set; }
        public string? U_MnQuarterCom { get; set; }
        public string? U_RemarkQuarterCom { get; set; }
        public string? U_InvMarketing { get; set; }
        public string? U_MnMarketing { get; set; }
        public string? U_RemarkMarketing { get; set; }
        public string? U_InOther9 { get; set; }
        public string? U_MnOther9 { get; set; }
        public string? U_RemarkOther9 { get; set; }
        public string? U_InOther10 { get; set; }
        public string? U_MnOther10 { get; set; }
        public string? U_RemarkOther10 { get; set; }
        public string? U_InOther11 { get; set; }
        public string? U_MnOther11 { get; set; }
        public string? U_RemarkOther11 { get; set; }
        public string? U_InOther12 { get; set; }
        public string? U_MnOther12 { get; set; }
        public string? U_RemarkOther12 { get; set; }
        public string? U_SpecialTrAmt { get; set; }
        public string? U_SpecialTrnPer { get; set; }
        public string? U_QtyFactory { get; set; }
    }
}
