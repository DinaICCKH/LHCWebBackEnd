using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_OSLP
{
    public int SlpCode { get; set; }

    public string SlpName { get; set; } = null!;

    public string? Memo { get; set; }

    public decimal? Commission { get; set; }

    public short? GroupCode { get; set; }

    public string? Locked { get; set; }

    public string? DataSource { get; set; }

    public short? UserSign { get; set; }

    public int? EmpID { get; set; }

    public string? Active { get; set; }

    public string? Telephone { get; set; }

    public string? Mobil { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? DPPStatus { get; set; }

    public string? EncryptIV { get; set; }

    public string? U_SalesCode { get; set; }

    public string? U_Whs { get; set; }

    public int? U_Area { get; set; }

    public string? U_DeviceID { get; set; }

    public string? U_ShowStock { get; set; }

    public string? U_AppLocked { get; set; }

    public string? U_Secret { get; set; }

    public string? U_Profile { get; set; }

    public string U_UserType { get; set; } = null!;

    public string? WhsName { get; set; }
}
