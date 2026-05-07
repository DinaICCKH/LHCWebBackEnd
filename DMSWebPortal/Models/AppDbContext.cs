using DMSWebPortal.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ReportingServices.Interfaces;
using System;
using System.Collections.Generic;

namespace DMSWebPortal.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<LoginResult> LoginResults { get; set; }
    public DbSet<ItemLoginResult> ItemLoginResults { get; set; }
    public DbSet<CustomerResult> CustomerResults { get; set; }
    public DbSet<WhsResult> WhsResults { get; set; }
    public DbSet<UoMResult> UoMResults { get; set; }
    public DbSet<UoMGroupResult> UoMGroupResults { get; set; }
    public DbSet<PriceListResult> PriceListResults { get; set; }
    public DbSet<ItemPricingResult> ItemPricingResults { get; set; }
    public DbSet<VisitPlanResult> VisitPlanResults { get; set; }



    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    public virtual DbSet<Reason> Reasons { get; set; }

    public virtual DbSet<SO> SOs { get; set; }

    public virtual DbSet<SO1> SO1s { get; set; }

    public virtual DbSet<SO2> SO2s { get; set; }

    public virtual DbSet<SO2_Backup> SO2_Backups { get; set; }

    public virtual DbSet<SOLog> SOLogs { get; set; }

    public virtual DbSet<Table_1> Table_1s { get; set; }

    public virtual DbSet<Table_2> Table_2s { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<EndOfDay> EndofDays { get; set; }

    public virtual DbSet<VisitD> VisitDs { get; set; }

    public virtual DbSet<VisitH> VisitHs { get; set; }

    public virtual DbSet<tblAddress> tblAddresses { get; set; }

    public virtual DbSet<tblAlterAppTable> tblAlterAppTables { get; set; }

    public virtual DbSet<tblAlterAppTable_SAL> tblAlterAppTable_SALs { get; set; }

    public virtual DbSet<tblBP> tblBPs { get; set; }

    public virtual DbSet<tblBPAddress> tblBPAddresses { get; set; }

    public virtual DbSet<tblBPCatelog> tblBPCatelogs { get; set; }

    public virtual DbSet<tblBPChannel> tblBPChannels { get; set; }

    public virtual DbSet<tblBPContact> tblBPContacts { get; set; }

    public virtual DbSet<tblBPGroup> tblBPGroups { get; set; }

    public virtual DbSet<tblBPPricing> tblBPPricings { get; set; }

    public virtual DbSet<tblBPRequest> tblBPRequests { get; set; }

    public virtual DbSet<tblBPSalMapping> tblBPSalMappings { get; set; }

    public virtual DbSet<tblBank> tblBanks { get; set; }

    public virtual DbSet<tblIncome> tblIncomes { get; set; }

    public virtual DbSet<tblItem> tblItems { get; set; }
    public virtual DbSet<TblItemBatch> tblItemBatchs { get; set; }

    public virtual DbSet<tblItemPricing> tblItemPricings { get; set; }

    public virtual DbSet<tblItemSalesMan> tblItemSalesMen { get; set; }

    public virtual DbSet<tblItemStock> tblItemStocks { get; set; }
    public virtual DbSet<ItemStockResponse> ItemStockResponses { get; set; }

    public virtual DbSet<tblMainCat> tblMainCats { get; set; }

    public virtual DbSet<tblNewFeed> tblNewFeeds { get; set; }

    public virtual DbSet<tblPayment> tblPayments { get; set; }

    public virtual DbSet<tblPrinciple> tblPrinciples { get; set; }

    public virtual DbSet<tblProCondition> tblProConditions { get; set; }

    public virtual DbSet<tblProFOCSetup> tblProFOCSetups { get; set; }

    public virtual DbSet<tblProFOCSetup1> tblProFOCSetup1s { get; set; }

    public virtual DbSet<tblProFOCType> tblProFOCTypes { get; set; }

    public virtual DbSet<tblProMonthly> tblProMonthlies { get; set; }

    public virtual DbSet<tblProType> tblProTypes { get; set; }

    public virtual DbSet<tblPromotion> tblPromotions { get; set; }

    public virtual DbSet<tblPromotion1> tblPromotion1s { get; set; }

    public virtual DbSet<tblPromotionGroup> tblPromotionGroups { get; set; }

    public virtual DbSet<tblRegional> tblRegionals { get; set; }
    public DbSet<TblAR> TblARs { get; set; }

    public virtual DbSet<tblSalesEmployee> tblSalesEmployees { get; set; }

    public virtual DbSet<tblSalesEmployee1> tblSalesEmployee1s { get; set; }

    public virtual DbSet<tblSubCat> tblSubCats { get; set; }

    public virtual DbSet<tblUoMGroup> tblUoMGroups { get; set; }
    public virtual DbSet<tblUoM> tbluomresults { get; set; }

    public virtual DbSet<tblWh> tblWhs { get; set; }

    public virtual DbSet<v_ADDRESS> v_ADDRESSEs { get; set; }

    public virtual DbSet<v_BP> v_BPs { get; set; }

    public virtual DbSet<v_BPChannel> v_BPChannels { get; set; }

    public virtual DbSet<v_Commune> v_Communes { get; set; }

    public virtual DbSet<v_District> v_Districts { get; set; }

    public virtual DbSet<v_OCPR> v_OCPRs { get; set; }

    public virtual DbSet<v_OCRD> v_OCRDs { get; set; }

    public virtual DbSet<v_OCRG> v_OCRGs { get; set; }

    public virtual DbSet<v_OITM> v_OITMs { get; set; }

    public virtual DbSet<v_OSLP> v_OSLPs { get; set; }

    public virtual DbSet<v_OWH> v_OWHs { get; set; }

    public virtual DbSet<v_Province> v_Provinces { get; set; }

    public virtual DbSet<v_Reason> v_Reasons { get; set; }

    public virtual DbSet<v_SO> v_SOs { get; set; }

    public virtual DbSet<v_SO_VAN> v_SO_VANs { get; set; }

    public virtual DbSet<v_Village> v_Villages { get; set; }

    public virtual DbSet<v_tblBP> v_tblBPs { get; set; }
    public virtual DbSet<VNotification> VNotifications { get; set; }
    public virtual DbSet<ItemMasterDataResult> ItemMasterDataResults { get; set; }
    public virtual DbSet<BPMasterDataResult> BPMasterDataResults { get; set; }
    public virtual DbSet<SaleEmployeeMasterDataResult> SaleEmployeeMasterDataResults { get; set; }
    public virtual DbSet<DailyplanreportResult> DailyplanreportResults { get; set; }
    public virtual DbSet<PlanTrackingResult> PlanTrackingResults { get; set; }
    public virtual DbSet<DocumentNumber> DocumentNumbers { get; set; }

    public virtual DbSet<UomListByItemCodeResult> UomListByItemCodeResults { get; set; }
    public virtual DbSet<SaleOrderListResult> SaleOrderListResults { get; set; }
    public virtual DbSet<NoneSaleOrderListResult> NonSaleOrderListResults { get; set; }
    public virtual DbSet<vOnecolumm> OneColumnResults { get; set; }

    public virtual DbSet<PromotionListResult> PromotionListResults { get; set; }
    public virtual DbSet<v_Order> Get_Order_Available_Results { get; set; }
    public virtual DbSet<v_Order1> Get_Order_Available1_Results { get; set; }
    public virtual DbSet<v_OrderSAP> Get_SAP_OrderResult { get; set; }
    public virtual DbSet<v_promocal> Get_PromCal_Results { get; set; }

    public virtual DbSet<tblZone> tblZones { get; set; }
    public virtual DbSet<tblSubZone> tblSubZones { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<tblItemWhsPricing> tblItemWhsPricings { get; set; }
    public virtual DbSet<v_RunPromotion> v_RunPromotionResults { get; set; }
    public virtual DbSet<v_Income> v_IncomeResults { get; set; }
    public virtual DbSet<v_Order_Status> v_Order_StatusResult { get; set; }
    public virtual DbSet<ICC_Get_BP_Master_Data> ICC_Get_BP_Master_Data_Result { get; set; }
    public virtual DbSet<ICC_Get_BP_Request_Data> ICC_Get_BP_Request_Data_Result { get; set; }
    public virtual DbSet<PriceApprovalResult> PriceApprovalResults { get; set; }
    // DbSet for tblPriceList
    public virtual DbSet<tblPriceList> tblPriceLists { get; set; }
    public virtual DbSet<tblCheck> tblChecks { get; set; }
    public virtual DbSet<tblCheckOutReason> tblCheckOutReasons { get; set; }

    public virtual DbSet<DocEntryMapping> DocEntryMappings { get; set; }
    public virtual DbSet<tblSubGroup> tblSubGroups { get; set; }
    public virtual DbSet<tblGrade> tblGrades { get; set; }
    public virtual DbSet<ICC_Get_Order_App_Status_Result> ICC_Get_Order_App_Status_Results { set;get;}
    public virtual DbSet<Api_SAP_Get_Available_BPRequest_Result> Api_SAP_Get_Available_BPRequest_Results { set; get; }
    public virtual DbSet<ICC_API_Get_AlertsResult> ICC_API_Get_AlertsResults { set; get; }
    public virtual DbSet<LoginResponse> LoginResponses { get; set; }
    public virtual DbSet<ExchangeRateResponse> ExchangeRateResponses { get; set; }
    public virtual DbSet<UserListResponse> UserListResponses { get; set; }
    public DbSet<Tbl_SO> Tbl_SO { get; set; }
    public DbSet<Tbl_SO1> Tbl_SO1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //---------------------------------------------------------------------------------------------------


        modelBuilder.Entity<Tbl_SO>().HasNoKey();
        modelBuilder.Entity<Tbl_SO1>().HasNoKey();



        //------------------------------------------------------------------------------------------------------

        modelBuilder.Entity<LoginResponse>().HasNoKey();
        modelBuilder.Entity<LoginResult>().HasNoKey();
        modelBuilder.Entity<ItemLoginResult>().HasNoKey();
        modelBuilder.Entity<CustomerResult>().HasNoKey();
        modelBuilder.Entity<WhsResult>().HasNoKey();
        modelBuilder.Entity<UoMResult>().HasNoKey();
        modelBuilder.Entity<UoMGroupResult>().HasNoKey();
        modelBuilder.Entity<PriceListResult>().HasNoKey();
        modelBuilder.Entity<ItemPricingResult>().HasNoKey();
        modelBuilder.Entity<VisitPlanResult>().HasNoKey();


        //---------------------------------------------------------------------------------------------------------------

        ///No Result
        modelBuilder.Entity<ICC_Get_BP_Master_Data>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<ICC_Get_BP_Request_Data>().HasNoKey(); // Important for SP result

        modelBuilder.Entity<DailyplanreportResult>(entity =>
        {
            entity.HasNoKey(); // Because your class has no primary key

            entity.ToView("DailyplanreportResults"); // Or .ToTable() if it's a table

            entity.Property(e => e.CardCode)
                .HasColumnName("CardCode")
                .HasMaxLength(50);

            entity.Property(e => e.CardName)
                .HasColumnName("CardName")
                .HasMaxLength(200);

            entity.Property(e => e.Phone)
                .HasColumnName("Phone")
                .HasMaxLength(50);

            entity.Property(e => e.FullAddEn)
                .HasColumnName("FullAddEn")
                .HasMaxLength(300);

            entity.Property(e => e.VisitDate)
                .HasColumnName("VisitDate")
                .HasColumnType("datetime");

            entity.Property(e => e.CheckInDate)
                .HasColumnName("CheckInDate")
                .HasColumnType("datetime");

            entity.Property(e => e.CheckOutDate)
                .HasColumnName("CheckOutDate")
                .HasColumnType("datetime");

            entity.Property(e => e.DocStatus)
                .HasColumnName("DocStatus")
                .HasMaxLength(50);

            entity.Property(e => e.Duration)
                .HasColumnName("Duration")
                .HasMaxLength(50);

            entity.Property(e => e.FromDate)
                .HasColumnName("FromDate")
                .HasMaxLength(20);

            entity.Property(e => e.ToDate)
                .HasColumnName("ToDate")
                .HasMaxLength(20);
        });

        


        modelBuilder.Entity<ExchangeRate>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Exchange__A25C5AA6897684E9");

            entity.ToTable("ExchangeRate");

            entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CurCode).HasMaxLength(3);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__Reason__A25C5AA6F90B5B4A");

            entity.ToTable("Reason");

            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason1)
                .HasMaxLength(100)
                .HasColumnName("Reason");
            entity.Property(e => e.ReasonEN).HasMaxLength(100);
            entity.Property(e => e.ReasonKH).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<SO>(entity =>
        {
            entity.HasKey(e => e.DocEntry).HasName("PK__SO__F4D96FAE87E4351F");
            entity.ToTable("SO");


            entity.Property(e => e.APIStatus).HasMaxLength(1);
            entity.Property(e => e.AfterDis)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AppDocNo).HasMaxLength(30);
            entity.Property(e => e.CardCode).HasMaxLength(20);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckInLateLong).HasMaxLength(50);
            entity.Property(e => e.CheckInRemark).HasMaxLength(200);
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutLateLong).HasMaxLength(50);
            entity.Property(e => e.CheckOutRemark).HasMaxLength(200);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DelAddress).HasMaxLength(100);
            entity.Property(e => e.DisAmount)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DisPer)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocCur).HasMaxLength(5);
            entity.Property(e => e.DocNo).HasMaxLength(20);
            entity.Property(e => e.DocRate)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocStatus).HasMaxLength(20);
            entity.Property(e => e.FromLoc).HasMaxLength(50);
            entity.Property(e => e.ImageURL).HasMaxLength(200);
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.SAPLastError).HasMaxLength(200);
            entity.Property(e => e.SAPSyncStatus).HasMaxLength(15);
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.SubTotal)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VATAmount)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VATStatus).HasMaxLength(50);
            entity.Property(e => e.VATType).HasMaxLength(20);
            entity.Property(e => e.NextApprover).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<SO1>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.LineNum });
            entity.ToTable("SO1");

            entity.Property(e => e.DisAmount)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DisPer)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ItemCode).HasMaxLength(100);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.LineTotal)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ProCode).HasMaxLength(15);
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SaleType).HasMaxLength(10);
            entity.Property(e => e.UnitPrice)
                .HasDefaultValue(0m)
                .HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WhsCode).HasMaxLength(10);
        });

        modelBuilder.Entity<SO2>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.LineNum });

            entity.ToTable("SO2");

            entity.Property(e => e.ImageUrl).HasMaxLength(200);
            entity.Property(e => e.Remark).HasMaxLength(200);
        });

        modelBuilder.Entity<SO2_Backup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SO2_Backup");

            entity.Property(e => e.ImageUrl).HasMaxLength(200);
            entity.Property(e => e.Remark).HasMaxLength(200);
        });

        modelBuilder.Entity<SOLog>(entity =>
        {
            entity.HasKey(e => e.SysNo).HasName("PK__SOLog__EB33D9B13592B42C");

            entity.ToTable("SOLog");

            entity.Property(e => e.AppDocNum).HasMaxLength(20);
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Table_1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_1");

            entity.Property(e => e.SalType).HasMaxLength(50);
            entity.Property(e => e.SalesCode).HasMaxLength(50);
            entity.Property(e => e.WhsCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Table_2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_2");

            entity.Property(e => e.CardCode).HasMaxLength(50);
            entity.Property(e => e.SubZone).HasMaxLength(50);
            entity.Property(e => e.Zone).HasMaxLength(50);
        });
        modelBuilder.Entity<EndOfDay>(entity =>
        {
            entity.HasKey(e => e.DocEntry);
            entity.ToTable("EndOfDay");
            entity.Property(e => e.SalesCode)
                .HasMaxLength(20);
            entity.Property(e => e.EndDay)
                .HasColumnType("datetime");
            entity.Property(e => e.Bank)
                .HasMaxLength(100);
            entity.Property(e => e.CashUSD)
                .HasColumnType("numeric(18,2)");
            entity.Property(e => e.CashKHR)
                .HasColumnType("numeric(18,2)");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime");
            entity.Property(e => e.Remark)
                .HasMaxLength(200);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.CompanyName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Profile).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.UpdatedBy).HasMaxLength(100);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserType).HasMaxLength(30);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.SlpCode);
            entity.Property(e => e.IsWebUser).HasMaxLength(30);
            entity.Property(e => e.IsEndofDay).HasMaxLength(30);
            entity.Property(e => e.Manager).HasMaxLength(30);
            entity.Property(e => e.DeviceID).HasMaxLength(30);
            entity.Property(e => e.PrinterName).HasMaxLength(30);
            entity.Property(e => e.PrinterMac).HasMaxLength(30);
        });

        modelBuilder.Entity<VisitD>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.VisitDate, e.CardCode });

            entity.ToTable("VisitD");

            entity.Property(e => e.CardCode).HasMaxLength(50);
            entity.Property(e => e.ImageURL).HasMaxLength(100);
            entity.Property(e => e.ReasonType).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(100);
        });

        modelBuilder.Entity<VisitH>(entity =>
        {
            entity.HasKey(e => e.DocEntry).HasName("PK__VisitH__F4D96FAE8E099AC2");

            entity.ToTable("VisitH");

            entity.Property(e => e.CreatedBy).HasMaxLength(30);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocNum).HasMaxLength(30);
            entity.Property(e => e.Remark).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(30);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblAddress>(entity =>
        {
            entity.HasKey(e => e.AddressCode);

            entity.ToTable("tblAddress");

            entity.Property(e => e.AddressCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddressEN)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddressKH)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddressName).HasMaxLength(50);
            entity.Property(e => e.Region).HasMaxLength(20);
            entity.Property(e => e.ComCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ComENName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ComKHName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisENName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisKHName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProENName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProKHName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SubZone).HasMaxLength(20);
            entity.Property(e => e.VillageCode).HasMaxLength(50);
            entity.Property(e => e.VillageNameEN).HasMaxLength(200);
            entity.Property(e => e.VillageNameKH).HasMaxLength(200);
            entity.Property(e => e.Zone).HasMaxLength(20);
        });

        modelBuilder.Entity<tblAlterAppTable>(entity =>
        {
            entity.HasKey(e => e.DocEntry).HasName("PK__tblAlter__F4D96FAE7CB05363");

            entity.ToTable("tblAlterAppTable");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NewColumn).HasMaxLength(50);
            entity.Property(e => e.TableName).HasMaxLength(50);
        });

        modelBuilder.Entity<tblAlterAppTable_SAL>(entity =>
        {
            entity.HasKey(e => e.AlterDetail).HasName("PK__tblAlter__76EFC087A601DA06");

            entity.ToTable("tblAlterAppTable_SAL");

            entity.Property(e => e.DocStatus).HasMaxLength(15);
        });

        modelBuilder.Entity<tblBP>(entity =>
        {
            entity.HasKey(e => e.CardCode);

            entity.ToTable("tblBP");

            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddressCode).HasMaxLength(20);
            entity.Property(e => e.AllowDown).HasMaxLength(20);
            entity.Property(e => e.AppCode).HasMaxLength(50);
            entity.Property(e => e.BPRKey).HasMaxLength(50);
            entity.Property(e => e.BPSource).HasMaxLength(10);
            entity.Property(e => e.Balance).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CardFName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Channel).HasMaxLength(50);
            entity.Property(e => e.ComCode).HasMaxLength(20);
            entity.Property(e => e.ConfimedBy).HasMaxLength(50);
            entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");
            entity.Property(e => e.ContactPer).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreditLimited).HasColumnType("numeric(2, 2)");
            entity.Property(e => e.DisCode).HasMaxLength(20);
            entity.Property(e => e.FullAddEN).HasMaxLength(200);
            entity.Property(e => e.FullAddKH).HasMaxLength(200);
            entity.Property(e => e.GPSLateLong)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ImagePath).HasMaxLength(200);
            entity.Property(e => e.ImageUrlServer)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode).HasMaxLength(20);
            entity.Property(e => e.Region).HasMaxLength(30);
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.SubZone).HasMaxLength(20);
            entity.Property(e => e.Sync).HasMaxLength(5);
            entity.Property(e => e.TermName).HasMaxLength(100);
            entity.Property(e => e.Territory).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VATImage).HasMaxLength(200);
            entity.Property(e => e.VATNo)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VilName).HasMaxLength(100);
            entity.Property(e => e.Zone).HasMaxLength(20);
        });

        modelBuilder.Entity<tblBPAddress>(entity =>
        {
            entity.HasKey(e => new { e.CardCode, e.AddCode }).HasName("PK_tblBPAddress_1");

            entity.ToTable("tblBPAddress");

            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<tblBPCatelog>(entity =>
        {
            entity.HasKey(e => e.CatCode).HasName("PK__tblBPCat__5E593E4FDFE7332C");

            entity.ToTable("tblBPCatelog");

            entity.Property(e => e.CatCode).HasMaxLength(50);
            entity.Property(e => e.BarCode).HasMaxLength(50);
            entity.Property(e => e.CardCode).HasMaxLength(50);
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(200);
            entity.Property(e => e.Substitute).HasMaxLength(50);
        });

        modelBuilder.Entity<tblBPChannel>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK_BPChannel");

            entity.ToTable("tblBPChannel");

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<tblBPContact>(entity =>
        {
            entity.HasKey(e => e.ContactCode);

            entity.ToTable("tblBPContact");

            entity.Property(e => e.ContactCode).ValueGeneratedNever();
            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ContactName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Status)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Tel1)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<tblBPGroup>(entity =>
        {
            entity.HasKey(e => e.GroupCode);

            entity.ToTable("tblBPGroup");

            entity.Property(e => e.GroupCode).ValueGeneratedNever();
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<tblBPPricing>(entity =>
        {
            entity.HasKey(e => e.BPPriceKey);

            entity.ToTable("tblBPPricing");

            entity.Property(e => e.BPPriceKey)
                .HasMaxLength(125)
                .HasComment("CardCode+ItemCode+cast(PriceList as nvarchar)+cast(UomEntry as nvarchar) as 'BPPriceKey'	")
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Amount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });
        modelBuilder.Entity<tblBPRequest>(entity =>
        {
            entity.ToTable("tblBPRequest", "dbo");
            entity.HasKey(e => e.DocEntry)
                   .HasName("PK_tblBPRequest_1");
            entity.Property(e => e.DocEntry)
                   .ValueGeneratedOnAdd();
            entity.Property(e => e.BPKey).HasMaxLength(50);
            entity.Property(e => e.CardCode).HasMaxLength(20);
            entity.Property(e => e.AppCode);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.CardFName).HasMaxLength(100);
            entity.Property(e => e.ContactPer).HasMaxLength(100);
            entity.Property(e => e.GroupCode);
            entity.Property(e => e.Phone1).HasMaxLength(20);
            entity.Property(e => e.Phone2).HasMaxLength(20);
            entity.Property(e => e.Phone3).HasMaxLength(20);
            entity.Property(e => e.VATNo).HasMaxLength(32);
            entity.Property(e => e.DefPriceListCode);
            entity.Property(e => e.CreditLimited).HasColumnType("numeric(2,2)");
            entity.Property(e => e.TermCode);
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.GPSLateLong).HasMaxLength(100);
            entity.Property(e => e.ImagePath).HasMaxLength(200);
            entity.Property(e => e.Channel).HasMaxLength(50);
            entity.Property(e => e.SubGroup).HasMaxLength(20);
            entity.Property(e => e.Region).HasMaxLength(30);
            entity.Property(e => e.ProCode).HasMaxLength(20);
            entity.Property(e => e.DisCode).HasMaxLength(20);
            entity.Property(e => e.ComCode).HasMaxLength(20);
            entity.Property(e => e.VilName).HasMaxLength(100);
            entity.Property(e => e.AddressCode).HasMaxLength(20);
            entity.Property(e => e.FullAddKH).HasMaxLength(200);
            entity.Property(e => e.FullAddEN).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.ConfimedBy).HasMaxLength(50);
            entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");
            entity.Property(e => e.BPSource).HasMaxLength(10);
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.IsVAT).HasMaxLength(5);
            entity.Property(e => e.HouseNo).HasMaxLength(50);
            entity.Property(e => e.StreetNo).HasMaxLength(50);
            entity.Property(e => e.LastError).HasColumnType("nvarchar(max)");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.VATImage).HasMaxLength(200);
            entity.Property(e => e.JsonRemark);
            entity.Property(e => e.SAPSyncStatus);
        });


        modelBuilder.Entity<tblBPSalMapping>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblBPSal__A25C5AA634875105");

            entity.ToTable("tblBPSalMapping");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CardCode).HasMaxLength(20);
        });

        modelBuilder.Entity<tblBank>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblBank__93AE04F64E121B70");

            entity.ToTable("tblBank");

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.BankCode).HasMaxLength(30);
            entity.Property(e => e.BankName).HasMaxLength(100);
            entity.Property(e => e.CurCode).HasMaxLength(3);
            entity.Property(e => e.DocStatus).HasMaxLength(15);
            entity.Property(e => e.GLAccount).HasMaxLength(50);
        });

        modelBuilder.Entity<tblIncome>(entity =>
        {
            entity.HasKey(e => e.DocEntry).HasName("PK__tblIncom__F4D96FAEE0A71BB3");
            entity.ToTable("tblIncome");
            entity.Property(e => e.BankAmount) .HasDefaultValue(0.00m) .HasColumnType("numeric(18, 2)");
            entity.Property(e => e.BankCode).HasMaxLength(20);
            entity.Property(e => e.CashAmount) .HasDefaultValue(0.00m) .HasColumnType("numeric(18, 2)");
            entity.Property(e => e.CurCode) .HasMaxLength(3);
            entity.Property(e => e.IntegrationStatus) .HasMaxLength(10) .HasDefaultValue("Pending");
            entity.Property(e => e.LastError).HasMaxLength(300);
            entity.Property(e => e.SOBalance) .HasDefaultValue(0.00m) .HasColumnType("numeric(18, 2)");
            entity.Property(e => e.SODocEntry).HasColumnType("int");
            entity.Property(e => e.SAPIncomeDocEntry).HasColumnType("int");
        });

        modelBuilder.Entity<tblItem>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("PK_Item");

            entity.ToTable("tblItem");

            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Available).HasColumnType("numeric(21, 6)");
            entity.Property(e => e.BarCode).HasMaxLength(50);
            entity.Property(e => e.FrgnName)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HasPromotion)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrlLocal)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrlServer)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.InvUoMCode)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IsCommited).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ItemGroupName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ItemName)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MainCat).HasMaxLength(30);
            entity.Property(e => e.MaxLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OcrCode).HasMaxLength(20);
            entity.Property(e => e.OcrCode2).HasMaxLength(20);
            entity.Property(e => e.OcrCode3).HasMaxLength(20);
            entity.Property(e => e.OcrCode4).HasMaxLength(20);
            entity.Property(e => e.OnOrder).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Onhand).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PackageType).HasMaxLength(30);
            entity.Property(e => e.PrincipleCode).HasMaxLength(30);
            entity.Property(e => e.Status)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.SubCat).HasMaxLength(30);
            entity.Property(e => e.U_ProGroup)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });
        modelBuilder.Entity<TblItemBatch>(entity =>
        {
            entity.ToTable("tblItemBatch");
            entity.HasKey(e => e.AbsEntry);
            entity.Property(e => e.AbsEntry).ValueGeneratedNever();
            entity.Property(e => e.ItemCode).IsRequired().HasMaxLength(50);
            entity.Property(e => e.BatchNum).IsRequired().HasMaxLength(50);
            entity.Property(e => e.InDate);
            entity.Property(e => e.ExpDate);
            entity.Property(e => e.MnfDate);
            entity.Property(e => e.WhsCode).IsRequired().HasMaxLength(8);
            entity.Property(e => e.BatchQty).HasColumnType("numeric(19,6)");
            entity.Property(e => e.UpdatedDate);
        });


        modelBuilder.Entity<tblItemPricing>(entity =>
        {
            entity.HasKey(e => e.PricingKey).HasName("PK__ItemPric__AFC24ADE2FBC91D1");

            entity.ToTable("tblItemPricing");

            entity.Property(e => e.PricingKey)
                .HasMaxLength(100)
                .HasComment("ItemCode+PriceListCode+UoMEntry=Primary Key");
            entity.Property(e => e.Amount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ItemCode).HasMaxLength(50);
        });

        modelBuilder.Entity<tblItemSalesMan>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.LineNum }).HasName("PK_ItemSalesMan");

            entity.ToTable("tblItemSalesMan");
            entity.Property(e => e.DocEntry);
            entity.Property(e => e.LineNum);
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.DocStatus).HasMaxLength(20);
            entity.Property(e => e.Sync).HasMaxLength(10);
        });

        modelBuilder.Entity<tblItemStock>(entity =>
        {
            entity.HasKey(e => e.ItemStockKey).HasName("PK_ItemStock");

            entity.ToTable("tblItemStock");

            entity.Property(e => e.ItemStockKey)
                .HasMaxLength(70)
                .HasComment("ItemCode+WhsCode=Primary Key")
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AltQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Available).HasColumnType("numeric(21, 6)");
            entity.Property(e => e.IsCommited).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MaxStock).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinStock).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OnOrder).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Onhand).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WhsCode)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdateDate);
        });

        modelBuilder.Entity<tblMainCat>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblMainC__A25C5AA6DE514A61");

            entity.ToTable("tblMainCat");

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.Dscription).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblNewFeed>(entity =>
        {
            entity.HasKey(e => e.FeedID).HasName("PK__tblNewFe__1586DF7531A1AC23");

            entity.ToTable("tblNewFeed");

            entity.Property(e => e.ObjAction)
                .HasMaxLength(20)
                .HasComment("Add,Update,Delete");
            entity.Property(e => e.ObjName)
                .HasMaxLength(50)
                .HasComment("If New table, Table Name");
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .HasComment("if new Table, Table If add new column, Column");
        });

        modelBuilder.Entity<tblPayment>(entity =>
        {
            entity.HasKey(e => e.TermCode).HasName("PK__tblPayme__675CC10CFBB7DB40");

            entity.ToTable("tblPayment");

            entity.Property(e => e.TermCode).ValueGeneratedNever();
            entity.Property(e => e.AddDay).HasDefaultValue(0);
            entity.Property(e => e.AddMonth).HasDefaultValue(0);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TermName).HasMaxLength(100);
        });

        modelBuilder.Entity<tblPrinciple>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblPrinc__A25C5AA606E6E54E");

            entity.ToTable("tblPrinciple");

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.Dscription).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblProCondition>(entity =>
        {
            entity.HasKey(e => e.ConditionCode).HasName("PK__tblProCo__A25C5AA6A7F94DBA");

            entity.ToTable("tblProCondition");

            entity.Property(e => e.ConditionCode).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.Dscription).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblProFOCSetup>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblProFO__A25C5AA69E2EDE8A");

            entity.ToTable("tblProFOCSetup");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.ProFOCTypeCode).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblProFOCSetup1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tblProFOCSetup1");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.LineStatus).HasMaxLength(15);
        });

        modelBuilder.Entity<tblProFOCType>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblProFO__A25C5AA60C0F8B48");

            entity.ToTable("tblProFOCType");

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.Dscription).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblProMonthly>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblProMo__A25C5AA675D1183D");

            entity.ToTable("tblProMonthly");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.Dscription).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblProType>(entity =>
        {
            entity.HasKey(e => e.ProTypeCode);

            entity.ToTable("tblProType");

            entity.Property(e => e.ProTypeCode).HasMaxLength(20);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.ProTypeDesc).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblPromotion>(entity =>
        {
            entity.HasKey(e => e.ProEntry).HasName("PK__tblPromo__57C20CBF2C576067");

            entity.ToTable("tblPromotion");

            entity.Property(e => e.ProEntry).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.PrincipleCode).HasMaxLength(30);
            entity.Property(e => e.PrincipleDesc).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblPromotion1>(entity =>
        {
            entity.HasKey(e => new { e.ProEntry, e.LineNum });

            entity.ToTable("tblPromotion1");

            entity.Property(e => e.BPChannelCode).HasMaxLength(50);
            entity.Property(e => e.BPChannelName).HasMaxLength(100);
            entity.Property(e => e.BPGroupName).HasMaxLength(1000);
            entity.Property(e => e.BuyAmt).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.BuyQty).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.BuyUoM).HasMaxLength(20);
            entity.Property(e => e.CardCode).HasMaxLength(50);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.Combine).HasMaxLength(10);
            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.DisAmt).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.DisPer).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.FOCQty).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.FOCType).HasMaxLength(50);
            entity.Property(e => e.FOCUoM).HasMaxLength(20);
            entity.Property(e => e.ItemGroupName).HasMaxLength(100);
            entity.Property(e => e.LineStatus).HasMaxLength(20);
            entity.Property(e => e.PackType).HasMaxLength(50);
            entity.Property(e => e.PromotionGroup).HasMaxLength(50);
            entity.Property(e => e.PromotionType).HasMaxLength(50);
            entity.Property(e => e.Remark).HasMaxLength(100);
        });

        modelBuilder.Entity<tblPromotionGroup>(entity =>
        {
            entity.HasKey(e => e.ProGroupCode).HasName("PK__tblPromo__2DFAA5E2342F2B25");

            entity.ToTable("tblPromotionGroup");

            entity.Property(e => e.ProGroupCode).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProDesc).HasMaxLength(200);
            entity.Property(e => e.ProStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<tblRegional>(entity =>
        {
            entity.HasKey(e => e.RegionalCode).HasName("PK__tblRegio__455CD279C4F3AACB");

            entity.ToTable("tblRegional");

            entity.Property(e => e.RegionalCode).HasMaxLength(10);
            entity.Property(e => e.CC3Loc).HasMaxLength(20);
            entity.Property(e => e.RegionalName).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<tblSalesEmployee>(entity =>
        {
            entity.HasKey(e => e.SlpCode);
            entity.ToTable("tblSalesEmployee");
            entity.Property(e => e.SlpCode).ValueGeneratedNever();
            entity.Property(e => e.SalesName).HasMaxLength(155).UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Whs).HasMaxLength(8).UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.SALType).HasMaxLength(3);
            entity.Property(e => e.IsAllPrinciple).HasMaxLength(10); 
            entity.Property(e => e.IsTax).HasMaxLength(10);
            entity.Property(e => e.U_SalesCode).HasMaxLength(10);
            entity.Property(e => e.PrincipleAssign).HasMaxLength(50);
            entity.Property(e => e.IsDepo).HasMaxLength(3);
            entity.Property(e => e.DepoID).HasMaxLength(50);
            entity.Property(e => e.MainWhs).HasMaxLength(50);
        });

        modelBuilder.Entity<tblSalesEmployee1>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.LineNum });

            entity.ToTable("tblSalesEmployee1");
            entity.Property(e => e.DocEntry);
            entity.Property(e => e.LineNum);
            entity.Property(e => e.SlpCode);
            entity.Property(e => e.DocStatus).HasMaxLength(15);
            entity.Property(e => e.Region).HasMaxLength(10);
        });

        modelBuilder.Entity<tblSubCat>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("PK__tblSubCa__A25C5AA6CFF79FD6");

            entity.ToTable("tblSubCat");

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .HasDefaultValue("Active");
            entity.Property(e => e.Dscription).HasMaxLength(100);
            entity.Property(e => e.MainCat).HasMaxLength(20);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<tblUoMGroup>(entity =>
        {
            entity.HasKey(e => e.UgpKey).HasName("PK_UoMGroup");

            entity.ToTable("tblUoMGroup");

            entity.Property(e => e.UgpKey)
                .HasMaxLength(61)
                .HasComment("UgpEntry+UoMEntry=Primary Key");
            entity.Property(e => e.AltQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.UoMCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<tblWh>(entity =>
        {
            entity.HasKey(e => e.WhsCode).HasName("PK__tblWhs__9AE7288B4C56ED4A");

            entity.Property(e => e.WhsCode).HasMaxLength(20);
            entity.Property(e => e.WhsName).HasMaxLength(100);
            entity.Property(e => e.WhsStatus).HasMaxLength(20);
        });

        modelBuilder.Entity<v_ADDRESS>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_ADDRESSES");

            entity.Property(e => e.Canceled)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Object)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_AddressEn)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_AddressKh)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Khan)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_KhanCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_KhanKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Province)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_ProvinceCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_ProvinceKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_SangkatKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<v_BP>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_BP");

            entity.Property(e => e.AddCode).HasMaxLength(20);
            entity.Property(e => e.AppCode).HasMaxLength(20);
            entity.Property(e => e.BPChannel).HasMaxLength(20);
            entity.Property(e => e.BPName).HasMaxLength(100);
            entity.Property(e => e.ChannelName).HasMaxLength(200);
            entity.Property(e => e.ComCode).HasMaxLength(20);
            entity.Property(e => e.ConfimedBy).HasMaxLength(50);
            entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");
            entity.Property(e => e.ConfirmerName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatorName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisCode).HasMaxLength(20);
            entity.Property(e => e.DocStatus).HasMaxLength(20);
            entity.Property(e => e.FullAddEN).HasMaxLength(200);
            entity.Property(e => e.FullAddKH).HasMaxLength(200);
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode).HasMaxLength(20);
            entity.Property(e => e.SAPCode).HasMaxLength(50);
            entity.Property(e => e.U_Khan)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_KhanKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Province)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_ProvinceKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Sangkat)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_SangkatKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatorName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VilCode).HasMaxLength(20);
        });

        modelBuilder.Entity<v_BPChannel>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_BPChannel");

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<v_Commune>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_Commune");

            entity.Property(e => e.ComCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ComENName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ComKHName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<v_District>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_District");

            entity.Property(e => e.DisCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisENName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisKHName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<v_OCPR>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_OCPR");

            entity.Property(e => e.Address)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CardCode)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Cellolar)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate_)
                .HasColumnType("datetime")
                .HasColumnName("CreateDate ");
            entity.Property(e => e.E_MailL)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Tel1)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Tel2)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<v_OCRD>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_OCRD");

            entity.Property(e => e.AccCritria)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddID)
                .HasMaxLength(64)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AddrType)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Affiliate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AgentCode)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AggregDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AliasName)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.Attachment)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.AutoCalBCG)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AutoPost)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BCACode)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BackOrder)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BalTrnsfrd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Balance).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BalanceFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BalanceSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BankCode)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BankCountr)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BankCtlKey)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BillToDef)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Block)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BlockComm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BlockDunn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BoEDiscnt)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BoEOnClct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BoEPrsnt)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Box1099)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Building)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.Business)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardFName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardValid).HasColumnType("datetime");
            entity.Property(e => e.Cellular)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CertBKeep)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CertDetail)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CertWHT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ChannlBP)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ChecksBal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ChecksBalL).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ChecksBalS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CmpPrivate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CntctPrsn)
                .HasMaxLength(90)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CollecAuth)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Commission).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ConCerti)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ConnBP)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Country)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.County)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CrCardNum)
                .HasMaxLength(64)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreditLine).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CrtfcateNO)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DME)
                .HasMaxLength(5)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DNoteBalFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DNoteBalSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DNotesBal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DPPStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTill).HasColumnType("datetime");
            entity.Property(e => e.DatevAcct)
                .HasMaxLength(9)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DatevFirst)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DdctFileNo)
                .HasMaxLength(9)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DdctOffice)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DdctPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DdctStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DebPayAcct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DebtLine).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DefCommDDt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DefaultCur)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DeferrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DflAccount)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DflBranch)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DflCustomr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DflIBAN)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DflSwift)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DiscInRet)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DiscRel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Discount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmClear)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DpmIntAct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DscntRel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DunTerm)
                .HasMaxLength(25)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DunnDate).HasColumnType("datetime");
            entity.Property(e => e.ECVatGroup)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EDocGenTyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EORINumber)
                .HasMaxLength(17)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.E_Mail)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EdrsFromBP)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EdrsToBP)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EffcAllSrc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EffecPrice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EmplymntCt)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EnAddID)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.EnDflAccnt)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.EnDflIBAN)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.EnERD4In)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EnERD4Out)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EnIBAN)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.EncryptIV)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Equ)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExLettDate).HasColumnType("datetime");
            entity.Property(e => e.ExcptnlEvt)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExemptNo)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.ExpnPrfFnd).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExportCode)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FCEPmnMean)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FCERelevnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FCEVldte)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FatherCard)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FatherType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FeeAcc)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Free_Text)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.FrozenComm)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GTSBankAct)
                .HasMaxLength(80)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GTSBilAddr)
                .HasMaxLength(80)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GTSRegNum)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GlblLocNum)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HierchDdct)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HldCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HousBnkAct)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HousBnkBrn)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HousBnkCry)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HousCtlKey)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HouseBank)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HsBnkIBAN)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.HsBnkSwift)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IBAN)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IPACodePA)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ISRBillId)
                .HasMaxLength(9)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ITWTCode)
                .HasMaxLength(4)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Indicator)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Industry)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.InstrucKey)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.InsurOp347)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IntrAcc)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IntrntSite)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IntrstRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.IsDomestic)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IsResident)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.KBKCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LegalText)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LetterNum)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LicTradNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LocMth)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailAddrTy)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailAddres)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailBlock)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailBuildi)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.MailCity)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailCountr)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailCounty)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailStrNo)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MailZipCod)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MandateID)
                .HasMaxLength(35)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MaxAmount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MerchantID)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MinIntrst).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MivzExpSts)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NINum)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NaturalPer)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NoDiscount)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NotRel4MI)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Notes)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OKATO)
                .HasMaxLength(11)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OKTMO)
                .HasMaxLength(12)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OpCode347)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OrderBalFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrderBalSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrdersBal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OtrCtlAcct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OwnerIdNum)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PECAddr)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Pager)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PartDelivr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PaymBlock)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Phone1)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Phone2)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Picture)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PlngGroup)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PrevYearAc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PriceMode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Profession)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProjectCod)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Protected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PymCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup10)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup11)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup12)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup13)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup14)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup15)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup16)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup17)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup18)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup19)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup20)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup21)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup22)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup23)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup24)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup25)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup26)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup27)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup28)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup29)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup3)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup30)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup31)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup32)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup33)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup34)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup35)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup36)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup37)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup38)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup39)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup4)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup40)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup41)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup42)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup43)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup44)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup45)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup46)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup47)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup48)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup49)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup5)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup50)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup51)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup52)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup53)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup54)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup55)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup56)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup57)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup58)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup59)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup6)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup60)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup61)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup62)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup63)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup64)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup7)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup8)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup9)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RateDifAct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RcpntID)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RefDetails)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RegNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RelCode)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RepAddID)
                .HasMaxLength(28)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RepCmpName)
                .HasMaxLength(36)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RepFName)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RepFisCode)
                .HasMaxLength(16)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RepName)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RepSName)
                .HasMaxLength(36)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ResidenNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RoleTypCod)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SCAdjust)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SefazCheck)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SelfInvoic)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SenderID)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ShipToDef)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SignDate).HasColumnType("datetime");
            entity.Property(e => e.SinglePaym)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.State1)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.State2)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.StreetNo)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SurOver)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxIdIdent)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxRndRule)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ThreshOver)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TxExMxVdTp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TypWTReprt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TypeOfOp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UnpaidBoE)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UseBilAddr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UseShpdGd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VATRegNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ValidComm)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ValidUntil).HasColumnType("datetime");
            entity.Property(e => e.VatGroup)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatIDNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatIdUnCmp)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatResAddr)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatResDate).HasColumnType("datetime");
            entity.Property(e => e.VatResName)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VendorOcup)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VerifNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WHShaamGrp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WTCode)
                .HasMaxLength(4)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WTLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WTTaxCat)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.chainStore)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.eCityTown)
                .HasMaxLength(48)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.eCountry)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.eDistrict)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.eStreet)
                .HasMaxLength(38)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.eStreetNum)
                .HasMaxLength(4)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.eZipCode)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.frozenFor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.frozenFrom).HasColumnType("datetime");
            entity.Property(e => e.frozenTo).HasColumnType("datetime");
            entity.Property(e => e.sEmployed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.validFor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.validFrom).HasColumnType("datetime");
            entity.Property(e => e.validTo).HasColumnType("datetime");
        });

        modelBuilder.Entity<v_OCRG>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_OCRG");

            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DiscRel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EffecPrice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GroupType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Locked)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<v_OITM>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_OITM");

            entity.Property(e => e.AcqDate).HasColumnType("datetime");
            entity.Property(e => e.AssVal4WTR).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssblValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetAmnt1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetAmnt2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetClass)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AssetGroup)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AssetItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AssetRmk1)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AssetRmk2)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AssetSerNo)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AsstStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Attachment)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.AutoBatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.AvgPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BHeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BHeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BLength1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BVolume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWidth1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWidth2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseUnit)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BeverGrpC)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BeverTblC)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Blength2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BlncTrnsfr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BlockOut)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BuyUnitMsr)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ByWh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Canceled)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CapDate).HasColumnType("datetime");
            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Cession)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CntUnitMsr)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CodeBars)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CommisPcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CommisSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CompoWH)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Consig).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Counted).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CountryOrg)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CstmActing)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CtrSealQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CustomPer).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DeacAftUL)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DeprGroup)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DfltWH)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ECExpAcc)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ECInAcct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EnAstSeri)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EvalSystem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExcFixAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExcRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Excisable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExitCur)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExitPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExitWH)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExmptIncom)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExpensAcct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ExportCode)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FREE)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FREE1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FixCurrCms)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FrgnExpAcc)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FrgnInAcct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FrgnName)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.FrozenComm)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GLMethod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GLPickMeth)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GSTRelevnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GstTaxCtg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.IWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.IfrsPsRev)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Imported)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.InCostRoll)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IncomeAcct)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IndirctTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.InventryNo)
                .HasMaxLength(12)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.InvntItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.InvntryUom)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.IsCommited).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.IssueMthd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ItemClass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ItemCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ItemName)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ItemType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LastPurCur)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LastPurDat).HasColumnType("datetime");
            entity.Property(e => e.LastPurPrc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LegalText)
                .HasMaxLength(250)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LinkRsc)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.LstEvlDate).HasColumnType("datetime");
            entity.Property(e => e.LstEvlPric).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstSalDate).HasColumnType("datetime");
            entity.Property(e => e.ManBtchNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ManOutOnly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ManSerNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MatType)
                .HasMaxLength(3)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MaxLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MgrByQty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.MinLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinOrdrQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MngMethod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NVECode)
                .HasMaxLength(6)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NoDiscount)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NotifyASN)
                .HasMaxLength(40)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.NumInBuy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NumInCnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NumInSale).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OnHand).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OnHldPert).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OnOrder).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OneBOneRec)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.OpenBlnc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrdrMulti).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Phantom)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PicturName)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PlaningSys)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PrchseItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PrcrmntMtd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PrdStdCst).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PricingPrc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ProAssNum)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProductSrc)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PurFactor1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFactor2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFactor3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFactor4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFormula)
                .HasMaxLength(40)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PurPackMsr)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.PurPackUn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.QRCodeSrc)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.QryGroup1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup10)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup11)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup12)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup13)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup14)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup15)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup16)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup17)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup18)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup19)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup20)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup21)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup22)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup23)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup24)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup25)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup26)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup27)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup28)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup29)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup3)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup30)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup31)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup32)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup33)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup34)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup35)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup36)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup37)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup38)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup39)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup4)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup40)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup41)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup42)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup43)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup44)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup45)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup46)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup47)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup48)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup49)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup5)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup50)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup51)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup52)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup53)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup54)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup55)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup56)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup57)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup58)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup59)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup6)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup60)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup61)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup62)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup63)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup64)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup7)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup8)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.QryGroup9)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ReorderPnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReorderQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetDate).HasColumnType("datetime");
            entity.Property(e => e.RetilrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.RuleCode)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SHeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SHeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SLength1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SOIExc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SVolume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWW)
                .HasMaxLength(16)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWidth1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWidth2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFormula)
                .HasMaxLength(40)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SalPackMsr)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SalPackUn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalUnitMsr)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ScsCode)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SellItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SerialNum)
                .HasMaxLength(17)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Slength2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SouVirAsst)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SpProdType)
                .HasMaxLength(2)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SpcialDisc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Spec)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.StatAsset)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.StockValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SuppCatNum)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TNVED)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxCatCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxCodeAP)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxCodeAR)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxCtg)
                .HasMaxLength(4)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TaxType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Traceable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TrackSales)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.TreeQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TreeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserText)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS")
                .HasColumnType("ntext");
            entity.Property(e => e.VATLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ValidComm)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatGourpSa)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VatGroupPu)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VirtAstItm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WTLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WarrntTmpl)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WasCounted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WholSlsTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.frozenFor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.frozenFrom).HasColumnType("datetime");
            entity.Property(e => e.frozenTo).HasColumnType("datetime");
            entity.Property(e => e.onHldLimt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.validFor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.validFrom).HasColumnType("datetime");
            entity.Property(e => e.validTo).HasColumnType("datetime");
        });

        modelBuilder.Entity<v_OSLP>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_OSLP");

            entity.Property(e => e.Active)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Commission).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DPPStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.EncryptIV)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Locked)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Memo)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Mobil)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SlpName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_AppLocked)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_DeviceID)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Profile)
                .HasMaxLength(254)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_SalesCode)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Secret)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_ShowStock)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_UserType)
                .HasMaxLength(10)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Whs)
                .HasMaxLength(8)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.WhsName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<v_OWH>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_OWHS");

            entity.Property(e => e.City)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.WhsCode).HasMaxLength(20);
            entity.Property(e => e.WhsName).HasMaxLength(100);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.createDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<v_Province>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_Province");

            entity.Property(e => e.ProCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProENName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProKHName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
        });

        modelBuilder.Entity<v_Reason>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_Reason");

            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedByName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(100);
            entity.Property(e => e.ReasonKH).HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(20);
            entity.Property(e => e.UpdatedByName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<v_SO>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_SO");

            entity.Property(e => e.APIStatus).HasMaxLength(1);
            entity.Property(e => e.AfterDis).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AppDocNo).HasMaxLength(30);
            entity.Property(e => e.CardCode).HasMaxLength(20);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.DefPriceListCode);
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckInLateLong).HasMaxLength(50);
            entity.Property(e => e.CheckInRemark).HasMaxLength(200);
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutLateLong).HasMaxLength(50);
            entity.Property(e => e.CheckOutRemark).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DelAddress).HasMaxLength(100);
            entity.Property(e => e.DisAmount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DisPer).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocCur).HasMaxLength(5);
            entity.Property(e => e.DocNo).HasMaxLength(20);
            entity.Property(e => e.DocRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocStatus).HasMaxLength(20);
            entity.Property(e => e.FromLoc).HasMaxLength(50);
            entity.Property(e => e.ImageURL).HasMaxLength(200);
            entity.Property(e => e.LicTradNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.SAPLastError).HasMaxLength(200);
            entity.Property(e => e.SAPSyncStatus).HasMaxLength(15);
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.SalesName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SubTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Total).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VATAmount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VATStatus).HasMaxLength(50);
            entity.Property(e => e.VATType).HasMaxLength(20);
            entity.Property(e => e.SAPDocNum).HasMaxLength(20);
        });

        modelBuilder.Entity<v_SO_VAN>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_SO_VAN");

            entity.Property(e => e.APIStatus).HasMaxLength(1);
            entity.Property(e => e.AfterDis).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AppDocNo).HasMaxLength(30);
            entity.Property(e => e.CardCode).HasMaxLength(20);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckInLateLong).HasMaxLength(50);
            entity.Property(e => e.CheckInRemark).HasMaxLength(200);
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutLateLong).HasMaxLength(50);
            entity.Property(e => e.CheckOutRemark).HasMaxLength(200);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DelAddress).HasMaxLength(100);
            entity.Property(e => e.DisAmount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DisPer).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocCur).HasMaxLength(5);
            entity.Property(e => e.DocNo).HasMaxLength(20);
            entity.Property(e => e.DocRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocStatus).HasMaxLength(20);
            entity.Property(e => e.FromLoc).HasMaxLength(50);
            entity.Property(e => e.ImageURL).HasMaxLength(200);
            entity.Property(e => e.LicTradNum)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Remark).HasMaxLength(200);
            entity.Property(e => e.SAPLastError).HasMaxLength(200);
            entity.Property(e => e.SAPSyncStatus).HasMaxLength(15);
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.SalesName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.SubTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Total).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VATAmount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VATStatus).HasMaxLength(50);
            entity.Property(e => e.VATType).HasMaxLength(20);
            entity.Property(e => e.SAPDocNum).HasMaxLength(20);
        });

        modelBuilder.Entity<v_Village>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_Village");

            entity.Property(e => e.AddressName).HasMaxLength(50);
            entity.Property(e => e.CC3Loc).HasMaxLength(20);
            entity.Property(e => e.ComCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.DisCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VillageCode).HasMaxLength(50);
            entity.Property(e => e.VillageNameEN).HasMaxLength(200);
            entity.Property(e => e.VillageNameKH).HasMaxLength(200);
        });

        modelBuilder.Entity<v_tblBP>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_tblBP");

            entity.Property(e => e.AddressCode).HasMaxLength(20);
            entity.Property(e => e.AppCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.BPSource).HasMaxLength(10);
            entity.Property(e => e.Balance).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CardCode)
                .HasMaxLength(15)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardFName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CardName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Channel).HasMaxLength(50);
            entity.Property(e => e.ChannelName).HasMaxLength(200);
            entity.Property(e => e.ComCode).HasMaxLength(20);
            entity.Property(e => e.ConfimedBy).HasMaxLength(50);
            entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");
            entity.Property(e => e.ConfirmerName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ContactPer).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatorName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.CreditLimited).HasColumnType("numeric(2, 2)");
            entity.Property(e => e.DeviceID).HasMaxLength(100);
            entity.Property(e => e.DisCode).HasMaxLength(20);
            entity.Property(e => e.FullAddEN).HasMaxLength(200);
            entity.Property(e => e.FullAddKH).HasMaxLength(200);
            entity.Property(e => e.GPSLateLong)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ImagePath).HasMaxLength(200);
            entity.Property(e => e.ImageUrlServer)
                .HasMaxLength(200)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.ProCode).HasMaxLength(20);
            entity.Property(e => e.SalesCode).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(15);
            entity.Property(e => e.TermName).HasMaxLength(100);
            entity.Property(e => e.U_Khan)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_KhanKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Province)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_ProvinceKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_Sangkat)
                .HasMaxLength(100)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.U_SangkatKh)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatorName)
                .HasMaxLength(155)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VATNo)
                .HasMaxLength(32)
                .UseCollation("SQL_Latin1_General_CP850_CI_AS");
            entity.Property(e => e.VilName).HasMaxLength(100);
        });
        // Notifications table configuration
        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notifications");

            entity.HasKey(e => e.NotificationId);

            entity.Property(e => e.RecipientType)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.SenderType)
                .HasMaxLength(20);

            entity.Property(e => e.Message);

            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.RelatedEntityType)
                .HasMaxLength(50);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime");

            entity.Property(e => e.IsViewed)
                .HasDefaultValue(false);

            entity.Property(e => e.IsGlobal)
                .HasDefaultValue(false);
        });
        modelBuilder.Entity<VNotification>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("v_Notifications");

            entity.Property(e => e.NotificationId);
            entity.Property(e => e.RecipientId);
            entity.Property(e => e.RecipientType);
            entity.Property(e => e.SenderId);
            entity.Property(e => e.SenderType);
            entity.Property(e => e.Message);
            entity.Property(e => e.Type);
            entity.Property(e => e.RelatedEntityId);
            entity.Property(e => e.RelatedEntityType);
            entity.Property(e => e.CreatedDate);
            entity.Property(e => e.IsViewed);
            entity.Property(e => e.IsGlobal);
            entity.Property(e => e.TimeAgo);
            entity.Property(e => e.DocNo);
            entity.Property(e => e.Remark);
            entity.Property(e => e.CheckInRemark);
            entity.Property(e => e.CheckOutRemark);
        });
        // Map PriceList to tblPriceList table
        modelBuilder.Entity<tblPriceList>(entity =>
        {
            entity.ToTable("tblPriceList");

            entity.HasKey(e => e.ListNum); // Primary key

            entity.Property(e => e.ListName)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(false); // nullable

            entity.Property(e => e.DocStatus)
                .HasMaxLength(15)
                .IsUnicode(true)
                .IsRequired(false); // nullable

            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .IsRequired(false); // nullable
        });
        // Map PriceList to tblCheck table
        modelBuilder.Entity<tblCheck>(entity =>
        {
            entity.ToTable("tblCheck");
            entity.HasKey(e => e.DocEntry); // Primary key
        });
        // Map PriceList to tblCheckOutReason table
        modelBuilder.Entity<tblCheckOutReason>(entity =>
        {
            entity.ToTable("tblCheckOutReason");
            entity.HasKey(e => e.DocEntry); // Primary key
        });
        // Map DocEntryMapping to DocEntryMapping table
        modelBuilder.Entity<DocEntryMapping>(entity =>
        {
            entity.ToTable("DocEntryMapping");
            entity.HasKey(e => e.ID); // Primary key
        });
        // Map DocEntryMapping to DocEntryMapping table
        modelBuilder.Entity<tblSubGroup>(entity =>
        {
            entity.ToTable("tblSubGroup");
            entity.HasKey(e => e.Code); // Primary key
        });
        // Map DocEntryMapping to DocEntryMapping table
        modelBuilder.Entity<tblGrade>(entity =>
        {
            entity.ToTable("tblGrade");
            entity.HasKey(e => e.Code); // Primary key
        });
        modelBuilder.Entity<ItemMasterDataResult>().HasNoKey(); // Important for SP result
        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<BPMasterDataResult>().HasNoKey(); // Important for SP result
        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<SaleEmployeeMasterDataResult>().HasNoKey(); // Important for SP result
        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<PlanTrackingResult>().HasNoKey(); // Important for SP result
        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<DocumentNumber>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<tblSubZone>(entity =>
        {
            entity.HasKey(e => new { e.Code });
            entity.ToTable("tblSubZone");
            entity.Property(e => e.SubZoneName).HasMaxLength(100);
            entity.Property(e => e.DocStatus).HasMaxLength(15);
        });
        modelBuilder.Entity<tblZone>(entity =>
        {
            entity.HasKey(e => new { e.Code });
            entity.ToTable("tblZone");
            entity.Property(e => e.ZoneName).HasMaxLength(100);
            entity.Property(e => e.DocStatus).HasMaxLength(15);
        });
        modelBuilder.Entity<tblItemWhsPricing>(entity =>
        {
            entity.HasKey(e => new { e.Code });
            entity.ToTable("tblItemPriceWhs");
            entity.Property(e => e.ItemCode).HasMaxLength(100);
            entity.Property(e => e.WhsCode).HasMaxLength(10);
            entity.Property(e => e.UoMEntry);
            entity.Property(e => e.Amount) .HasDefaultValue(0m) .HasColumnType("numeric(19, 6)");
        });

        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<UomListByItemCodeResult>().HasNoKey(); // Important for SP result
        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<SaleOrderListResult>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<vOnecolumm>().HasNoKey(); // Important for SP result

        modelBuilder.Entity<NoneSaleOrderListResult>().HasNoKey(); // Important for SP result
        //OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<PromotionListResult>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<v_RunPromotion>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<v_Order>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<v_Order1>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<v_Income>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<tblUoM>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<v_OrderSAP>().HasNoKey(); // Important for SP result
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<v_promocal>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<v_Order_Status>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<PriceApprovalResult>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<ICC_Get_Order_App_Status_Result>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<Api_SAP_Get_Available_BPRequest_Result>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<ICC_API_Get_AlertsResult>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<LoginResponse>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<ItemStockResponse>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<ExchangeRateResponse>().HasNoKey(); // Important for SP result
        modelBuilder.Entity<UserListResponse>().HasNoKey(); // Important for SP result
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
