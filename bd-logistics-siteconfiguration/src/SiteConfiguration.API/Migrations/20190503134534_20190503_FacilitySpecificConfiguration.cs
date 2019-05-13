using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteConfiguration.API.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class _20190503_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityLogisticsConfig",
                columns: table => new
                {
                    FacilityKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateUTCDateTime = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedUTCDateTime = table.Column<DateTimeOffset>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    ManualRestockPrinterKey = table.Column<Guid>(nullable: true),
                    ReceivingPrinterKey = table.Column<Guid>(nullable: true),
                    ExceptionPrinterKey = table.Column<Guid>(nullable: true),
                    BinLabelPrinterKey = table.Column<Guid>(nullable: true),
                    CostCenterCode = table.Column<string>(nullable: true),
                    ReqRestockDestFlag = table.Column<bool>(nullable: false),
                    SubmitMedListInfoFlag = table.Column<bool>(nullable: false),
                    SubmitReturnCreditFlag = table.Column<bool>(nullable: false),
                    PrintLabelOnQtyChangeFlag = table.Column<bool>(nullable: false),
                    RequestRestockLotInfoFlag = table.Column<bool>(nullable: false),
                    ProcessInactiveAsExceptionFlag = table.Column<bool>(nullable: false),
                    ReceiveAndSendRemoteOrdersFlag = table.Column<bool>(nullable: false),
                    RequireHoldReasonFlag = table.Column<bool>(nullable: false),
                    NotifyOrderPickedFlag = table.Column<bool>(nullable: false),
                    SendCheckStationPickInfoFlag = table.Column<bool>(nullable: false),
                    SubmitGeneralLedgerInfoFlag = table.Column<bool>(nullable: false),
                    PrintCompositLabelFlag = table.Column<bool>(nullable: false),
                    SmartOrderRoutingFlag = table.Column<bool>(nullable: false),
                    PrepositionISAFlag = table.Column<bool>(nullable: false),
                    VerifyMultiDispenseFlag = table.Column<bool>(nullable: false),
                    ScanTranLabelFlag = table.Column<bool>(nullable: false),
                    VerifyQuantityFlag = table.Column<bool>(nullable: false),
                    ScanVerifyFlag = table.Column<bool>(nullable: false),
                    ADMQuantityRoundingFlag = table.Column<bool>(nullable: false),
                    ADMDupeTimeDelay = table.Column<int>(nullable: true),
                    ADMIgnoreStockOutFlag = table.Column<bool>(nullable: false),
                    ADMIgnoreCritLowFlag = table.Column<bool>(nullable: false),
                    PrepackBulkSuffixFlag = table.Column<bool>(nullable: false),
                    CalcPrepackWithPOFlag = table.Column<bool>(nullable: false),
                    AutoCalcSlowPrepackFlag = table.Column<bool>(nullable: false),
                    ReturnsOnHoldDefFlag = table.Column<bool>(nullable: false),
                    PackageShareQuantityRoundingFlag = table.Column<bool>(nullable: false),
                    UsePreferredOrderingFlag = table.Column<bool>(nullable: false),
                    SeparateOrderByISAFlag = table.Column<bool>(nullable: false),
                    AutoGenCycleCountScheduleFlag = table.Column<bool>(nullable: false),
                    EnableOldestExpirationDateFlag = table.Column<bool>(nullable: false),
                    AutoGenCycleCountFlag = table.Column<bool>(nullable: false),
                    CaptureLotExpirationFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilitySpecificConfigurations", x => x.FacilityKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilitySpecificConfigurations");
        }
    }
}
