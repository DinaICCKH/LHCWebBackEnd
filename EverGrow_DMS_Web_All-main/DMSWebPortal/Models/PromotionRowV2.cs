namespace DMSWebPortal.Models
{
    public class PromotionRowV2
    {
        public string Mode { get; set; } = "Add";       // Add / Update
        public string Code { get; set; }                // FK to PromotionHeaderV2
        public int LineId { get; set; }                 // PK with Code
        public string? Object { get; set; }
        public int? LogInst { get; set; }
        public string U_LevelType { get; set; }         // NOT NULL
        public string? U_UOMType { get; set; }
        public string? U_Code { get; set; }
        public string? U_Description { get; set; }
        public decimal? U_StartQty { get; set; }
        public decimal? U_FreeQty { get; set; }
        public decimal? U_PromotionPercent { get; set; }
        public decimal? U_PromotionAmount { get; set; }
        public decimal? U_TransportationPercent { get; set; }
        public decimal? U_TransportionAmount { get; set; }       // spelling as in SQL
        public decimal? U_TransportationAmountKH { get; set; }
    }
}
