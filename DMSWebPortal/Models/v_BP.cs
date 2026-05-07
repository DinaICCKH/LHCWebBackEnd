using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class v_BP
{
    public string AppCode { get; set; } = null!;

    public string? SAPCode { get; set; }

    public string? BPName { get; set; }

    public int? BPGroup { get; set; }

    public string? BPChannel { get; set; }

    public string? ProCode { get; set; }

    public string? DisCode { get; set; }

    public string? ComCode { get; set; }

    public string? VilCode { get; set; }

    public string? AddCode { get; set; }

    public string? FullAddKH { get; set; }

    public string? FullAddEN { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? ConfimedBy { get; set; }

    public DateTime? ConfirmedDate { get; set; }

    public string? DocStatus { get; set; }

    public string CreatorName { get; set; } = null!;

    public string UpdatorName { get; set; } = null!;

    public string? ChannelName { get; set; }

    public string? GroupName { get; set; }

    public string? U_Province { get; set; }

    public string? U_ProvinceKh { get; set; }

    public string? U_Khan { get; set; }

    public string? U_KhanKh { get; set; }

    public string? U_Sangkat { get; set; }

    public string? U_SangkatKh { get; set; }

    public string ConfirmerName { get; set; } = null!;
}
