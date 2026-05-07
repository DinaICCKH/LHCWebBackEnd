using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMSWebPortal.Models;

public partial class User
{
    [NotMapped]
    // Optional: Mode for API (Add/Update) 
    public string? Mode { get; set; }

    //---------------------------
    public string Code { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Profile { get; set; }

    public string? CompanyName { get; set; }

    public string? UserType { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? Status { get; set; }
    public int? SlpCode { get; set; }
    public string? IsWebUser { get; set; }
    public string? IsEndofDay { get; set; }
    public string? Manager { get; set; }
    public string? DeviceID { get; set; }
    public string? PrinterName { get; set; }
    public string? PrinterMac { get; set; }
}
