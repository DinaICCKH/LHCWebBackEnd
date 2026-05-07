namespace DMSWebPortal.Models
{
    public class PO
    {
        public string Mode { get; set; }
        public string DocEntry { get; set; }
        public string Branch { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime PostingDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string VATStatus { get; set; }
        public string PaymentTerm { get; set; }
        public string SaleCode { get; set; }
        public string Remark { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DPMAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DocAmount { get; set; }
        public string DocStatus { get; set; }
        public string SAPCode { get; set; }
        public string SAPStatus { get; set; }
        public string APIStatus { get; set; }
        public string APIErrorMessage { get; set; }
        public string DoCur { get; set; }

        // Optional: list of PO1 items
        public List<PO1> PODetails { get; set; } = new List<PO1>();
    }

    public class PO1
    {
        public string DocEntry { get; set; }
        public int LineNum { get; set; }
        public string BaseType { get; set; }
        public string BaseEntry { get; set; }
        public int? BaseLineNum { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UOM { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public string VAT { get; set; }
        public decimal LineAmount { get; set; }
        public string Warehouse { get; set; }
        public string OcrCode { get; set; }
        public string OcrCode2 { get; set; }
        public string OcrCode3 { get; set; }
        public string OcrCode4 { get; set; }
        public string Project { get; set; }
        public string LineStatus { get; set; }
        public string Remark { get; set; }
        public string IsFather { get; set; }
        public string FatherCode { get; set; }

        // Removed navigation property to avoid EF Core errors
        // public PO PO { get; set; }
    }
}
