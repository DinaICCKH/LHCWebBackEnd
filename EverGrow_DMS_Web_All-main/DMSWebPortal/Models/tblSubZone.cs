using System;
using System.Collections.Generic;
namespace DMSWebPortal.Models
{
    public partial class tblSubZone
    {
        public string Code { get; set; } = null!;
        public string? SubZoneName { get; set; }
        public string? DocStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
