using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class tblIncome
{
    public int DocEntry { get; set; }
    public int? SODocEntry { get; set; }

    public decimal? SOBalance { get; set; }

    public string? BankCode { get; set; }

    public decimal? BankAmount { get; set; }

    public decimal? CashAmount { get; set; }

    public string? CurCode { get; set; }

    public decimal? SAPIncomeDocEntry { get; set; }

    public string? IntegrationStatus { get; set; }
    public string? LastError { get; set; }

}
