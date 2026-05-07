using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class ExchangeRate
{
    public int Code { get; set; }

    public string? CurCode { get; set; }

    public decimal? Amount { get; set; }

    public DateOnly? RateDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
