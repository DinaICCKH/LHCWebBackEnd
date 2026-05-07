namespace DMSWebPortal.Models
{
    public class tblCheckOutReason
    {
        public int DocEntry { get; set; }          // IDENTITY PRIMARY KEY

        public int? CheckInEntry { get; set; }     // FK to Check-In

        public string? ReasonImage { get; set; }   // Image path or filename

        public int? ReasonCode { get; set; }       // Reason master code

        public string? ReasonRemark { get; set; }  // Remark / description
    }
}
