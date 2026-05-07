namespace DMSWebPortal.Models
{
    public class Tbl_SO
    {
        public string? Mode { get; set; } // used for Add / Update
        public string? DocEntry { get; set; } // allow null for Add
        public string? BPLId { get; set; }
        public string? BPLName { get; set; }
        public string? CANCELED { get; set; }
        public string? DocStatus { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public string? U_DeliveryTime { get; set; }
        public string? CardCode { get; set; }
        public string? CardName { get; set; }
        public string? Address { get; set; }
        public string? NumAtCard { get; set; }
        public decimal? VatSum { get; set; }
        public decimal? DiscPrcnt { get; set; }
        public decimal? DiscSum { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DocTotal { get; set; }
        public decimal? PaidToDate { get; set; }
        public string? Ref1 { get; set; }
        public string? Ref2 { get; set; }
        public string? Comments { get; set; }
        public string? U_PaymentMethod { get; set; }
        public string? U_Owner { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UserSign { get; set; }
        public string? UserSign2 { get; set; }

        // Navigation property
        public List<Tbl_SO1> SO1Lines { get; set; } = new List<Tbl_SO1>();
    }
}
