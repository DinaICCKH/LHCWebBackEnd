using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSWebPortal.Models
{

    [Table("TBLSO")]
    public class TBLSO
    {
        [Key]
        public int DocEntry { get; set; }

        public string DocNo { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public int? ContactPer { get; set; }
        public string DelAddress { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? TaxDate { get; set; }
        public string DocCur { get; set; }
        public decimal? DocRate { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? DisPer { get; set; }
        public decimal? DisAmount { get; set; }
        public decimal? AfterDis { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? Total { get; set; }
        public string SalesCode { get; set; }
        public string Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AppId { get; set; }
        public string AppDocNo { get; set; }
        public string APIStatus { get; set; }
        public string APIErrMessage { get; set; }
        public string VATStatus { get; set; }
        public string DocStatus { get; set; }
        public int? SAPDocEntry { get; set; }
        public string SAPDocNum { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public string CheckInLateLong { get; set; }
        public string CheckInRemark { get; set; }
        public string ImageURL { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string CheckOutLateLong { get; set; }
        public string CheckOutRemark { get; set; }
        public string SAPSyncStatus { get; set; }
        public string SAPLastError { get; set; }
        public string VATType { get; set; }
        public string FromLoc { get; set; }
        public string NextApprover { get; set; }
        public string CreatedBy { get; set; }
        public string AllowHistory { get; set; }
        public string AppStatus { get; set; }
        public string JsonRemark { get; set; }
        public string JsonWeb { get; set; }
        public string Source { get; set; }
        public int? TermCode { get; set; }
        public string CheckInID { get; set; }
        public string CheckOutID { get; set; }
        public string PONo { get; set; }
        public string AppJson { get; set; }
        public string Alert { get; set; }
        public string SaleType { get; set; }
    }
}
