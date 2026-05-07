namespace DMSWebPortal.Models
{
    public class tblCheck
    {
        public int? DocEntry { set; get; }
        public int? AppEntry { set; get; }//docentry in app
        public string? CardCode {  set; get; }
        public string? SalesCode { set; get; }
        public string? CheckInImage {  set; get; }
        public string? CheckOutImage {  set; get; }
        public DateTime? CheckInDate { set; get; }
        public DateTime? CheckOutDate {  set; get; }
        public string? CheckInGPS { set; get; }
        public string? CheckOutGPS { set; get; }
        public string? CheckInRemark { set; get; }
        public string? CheckOutRemark { set; get; }
        public string? CheckStatus { set; get; }
       
        public int? AppOrderEntry { set; get; }
        public int? DMSOrderEntry { set; get; }

    }
}
