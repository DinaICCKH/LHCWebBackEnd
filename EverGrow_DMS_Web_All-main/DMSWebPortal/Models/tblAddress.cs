using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblAddress
{
    public string AddressCode { get; set; } = null!;

    public string? AddressName { get; set; }

    public string? ComCode { get; set; }

    public string? ComENName { get; set; }

    public string? ComKHName { get; set; }

    public string? DisCode { get; set; }

    public string? DisENName { get; set; }

    public string? DisKHName { get; set; }

    public string? ProCode { get; set; }

    public string? ProENName { get; set; }

    public string? ProKHName { get; set; }

    public string? AddressEN { get; set; }

    public string? AddressKH { get; set; }

    public string? VillageCode { get; set; }

    public string? VillageNameEN { get; set; }

    public string? VillageNameKH { get; set; }

    public string? Region { get; set; }

    public string? Status { get; set; }

    public string? Zone { get; set; }

    public string? SubZone { get; set; }
}
