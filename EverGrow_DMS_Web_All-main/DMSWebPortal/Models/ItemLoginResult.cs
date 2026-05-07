namespace DMSWebPortal.Models
{
    public class ItemLoginResult
    {
        public int Code { get; set; }
        public string? Message { get; set; }

        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public short? ItemGroupCode { get; set; }
        public string? ItemGroupName { get; set; }
        public int? UgpEntry { get; set; }

        public decimal? Onhand { get; set; }
        public decimal? OnOrder { get; set; }
        public decimal? IsCommited { get; set; }
        public decimal? Available { get; set; }

        public decimal? MinLevel { get; set; }
        public decimal? MaxLevel { get; set; }

        public string? Status { get; set; }

        public string? ImageUrlServer { get; set; }
        public string? ImageUrlLocal { get; set; }

        public string? FrgnName { get; set; }

        public string? InvUoMCode { get; set; }
        public int? InvUoMEntry { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? OcrCode { get; set; }
        public string? OcrCode2 { get; set; }
        public string? OcrCode3 { get; set; }
        public string? OcrCode4 { get; set; }

        public string? Manufacturer { get; set; }
        public string? ManufacturerDes { get; set; }

        public string? SubGroup { get; set; }
        public string? SubGroupDes { get; set; }

        public string? ItemBrand { get; set; }
        public string? ItemBrandDes { get; set; }

        public string? ItemType { get; set; }
        public string? ItemTypeDes { get; set; }

        public string? ProteinType { get; set; }
        public string? ProteinTypeDes { get; set; }

        public string? SubGroup2 { get; set; }
        public string? SubGroup2Des { get; set; }

        public string? Factory { get; set; }
        public string? FactoryDes { get; set; }

        public string? BarCode { get; set; }

        public int? DefEntry { get; set; }

        public decimal? AltQty { get; set; }
        public decimal? SellingPrice { get; set; }
    }
}
