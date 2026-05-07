
public class BPRequestDto
{
    public int? AppCode { get; set; }
    public int? BPEntry { set; get; }
    public string? SalesCode { get; set; }
    public string? CardName { get; set; }
    public string? CardFName { get; set; }
    public string? VATNo { get; set; }
    public string? VATImage { get; set; }
    public string? ContactPer { get; set; }
    public short? GroupCode { get; set; }
    public string? Channel { get; set; }
    public short? TermCode { get; set; }
    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Phone3 { get; set; }
    public string? Email { get; set; }
    public string? ProCode { get; set; }
    public string? DisCode { get; set; }
    public string? ComCode { get; set; }
    public string? VilName { get; set; }
    public string? HouseNo { get; set; }
    public string? StreetNo { get; set; }
    public string? FullAddKH { get; set; }
    public string? FullAddEN { get; set; }
    public string? GPSLateLong { get; set; }
    public string? ImageUrlServer { get; set; }
    public string? AddressCode { set; get; }
    // 🔹 FILE
    public IFormFile? Image { get; set; }
}
