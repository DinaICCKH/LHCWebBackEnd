using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_ADDRESS
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public int DocEntry { get; set; }

    public string? Canceled { get; set; }

    public string? Object { get; set; }

    public int? LogInst { get; set; }

    public int? UserSign { get; set; }

    public string? Transfered { get; set; }

    public DateTime? CreateDate { get; set; }

    public short? CreateTime { get; set; }

    public DateTime? UpdateDate { get; set; }

    public short? UpdateTime { get; set; }

    public string? DataSource { get; set; }

    public string? U_SangkatKh { get; set; }

    public string? U_ProvinceCode { get; set; }

    public string? U_Province { get; set; }

    public string? U_ProvinceKh { get; set; }

    public string? U_KhanCode { get; set; }

    public string? U_Khan { get; set; }

    public string? U_KhanKh { get; set; }

    public string? U_AddressEn { get; set; }

    public string? U_AddressKh { get; set; }
}
