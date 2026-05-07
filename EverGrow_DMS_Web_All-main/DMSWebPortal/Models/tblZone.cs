using System;
using System.Collections.Generic;
namespace DMSWebPortal.Models
{
    public partial class tblZone
    {
        public string Code { get; set; } = null!;
        public string? ZoneName { get; set; }
        public string? DocStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
