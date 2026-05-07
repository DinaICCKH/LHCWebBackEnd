using System.ComponentModel.DataAnnotations;

namespace DMSWebPortal.Models
{
    public partial class EndOfDay
    {
        public int DocEntry { get; set; }
        public string? SalesCode { get; set; }
        public DateTime? EndDay { get; set; }
        public string? Bank { get; set; }
        public decimal? CashUSD { get; set; }
        public decimal? CashKHR { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Remark { get; set; }
    }
}
