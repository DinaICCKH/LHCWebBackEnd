namespace DMSWebPortal.Models
{
    public class PromotionHeaderV2
    {
        public string Mode { get; set; } = "Add";       // Add / Update
        public string Code { get; set; }                // PK
        public string? Name { get; set; }
        public int DocEntry { get; set; }
        public string? Canceled { get; set; }           // 'Y'/'N'
        public string? Object { get; set; }
        public int? LogInst { get; set; }
        public int? UserSign { get; set; }
        public string? Transfered { get; set; }         // 'Y'/'N'
        public DateTime? CreateDate { get; set; }
        public short? CreateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public short? UpdateTime { get; set; }
        public string? DataSource { get; set; }         // 'C'?
        public string U_RefNo { get; set; }             // NOT NULL
        public DateTime? U_FromDate { get; set; }
        public DateTime? U_ToDate { get; set; }
        public string U_CardCode { get; set; }          // NOT NULL
        public string? U_CardName { get; set; }
        public string U_PromotionType { get; set; }     // NOT NULL
        public string U_ApplyType { get; set; }         // NOT NULL
        public string? U_RemarkCode { get; set; }
        public string? U_Remark { get; set; }
        public string U_ItemGroup { get; set; }         // NOT NULL
        public string U_Status { get; set; }            // NOT NULL
        public string? U_Attactment { get; set; }

        // ✅ Include child rows for API serialization
        public List<PromotionRowV2>? PromotionRows { get; set; } = new();
    }
}
