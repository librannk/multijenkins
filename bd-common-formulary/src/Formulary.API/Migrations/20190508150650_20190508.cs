using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Formulary.API.Migrations
{
    public partial class _20190508 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    ExternalSystemKey = table.Column<Guid>(nullable: false),
                    FacilityKey = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<string>(type: "varchar(100)", nullable: true),
                    AlternateItemID = table.Column<string>(type: "varchar(100)", nullable: true),
                    ExternalSystemDeleteUTCDatetime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: true),
                    CustomField1Text = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomField2Text = table.Column<string>(type: "varchar(100)", nullable: true),
                    CustomField3Text = table.Column<string>(type: "varchar(100)", nullable: true),
                    EnterpriseItemID = table.Column<string>(type: "varchar(50)", nullable: true),
                    MedicationItemFlag = table.Column<bool>(nullable: false),
                    DeleteFlag = table.Column<bool>(nullable: false),
                    LastModifiedUTCDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    StartUTCDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    EndUTCTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemKey);
                });

            migrationBuilder.CreateTable(
                name: "NDCEntities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FormularyId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    TradeName = table.Column<string>(nullable: true),
                    GenericName = table.Column<string>(nullable: true),
                    NDC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NDCEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedItemResult",
                columns: table => new
                {
                    RecommendedItemResultInternalCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    DescriptionText = table.Column<string>(type: "varchar(50)", nullable: true),
                    SortValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedItemResult", x => x.RecommendedItemResultInternalCode);
                });

            migrationBuilder.CreateTable(
                name: "UOM",
                columns: table => new
                {
                    UomKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    BaseUomKey = table.Column<Guid>(nullable: true),
                    TenantKey = table.Column<Guid>(nullable: false),
                    DisplayCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    InternalCode = table.Column<string>(type: "varchar(10)", nullable: true),
                    DescriptionText = table.Column<string>(type: "varchar(250)", nullable: false),
                    ConversionAmount = table.Column<decimal>(type: "decimal(28,14)", nullable: true),
                    StrengthFlag = table.Column<bool>(nullable: false),
                    VolumeFlag = table.Column<bool>(nullable: false),
                    PredefinedFlag = table.Column<bool>(nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOM", x => x.UomKey);
                    table.ForeignKey(
                        name: "FK_UOM_UOM_BaseUomKey",
                        column: x => x.BaseUomKey,
                        principalTable: "UOM",
                        principalColumn: "UomKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CS_Formulary_FacililtyNDC",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FacilityId = table.Column<int>(nullable: false),
                    NDCId = table.Column<int>(nullable: false),
                    IsPreferred = table.Column<bool>(nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(8, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_Formulary_FacililtyNDC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_Formulary_FacilityFormulary",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FormularyId = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    ADUQtyRounding = table.Column<bool>(nullable: false),
                    ADUIgnoreStockOut = table.Column<bool>(nullable: false),
                    ADUIgnoreCritLow = table.Column<bool>(nullable: false),
                    FacilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_Formulary_FacilityFormulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CS_Formulary_Formulary",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ItemId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    ItemName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CS_Formulary_Formulary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DispenseForm",
                schema: "dbo",
                columns: table => new
                {
                    DispenseFormLookupKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    DispenseForm = table.Column<string>(type: "varchar(50)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispenseForm", x => x.DispenseFormLookupKey);
                });

            migrationBuilder.CreateTable(
                name: "DispenseUnit",
                schema: "dbo",
                columns: table => new
                {
                    DispenseUnitLookupKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    DispenseUnit = table.Column<string>(type: "varchar(50)", nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispenseUnit", x => x.DispenseUnitLookupKey);
                });

            migrationBuilder.CreateTable(
                name: "FormularyStandard",
                schema: "dbo",
                columns: table => new
                {
                    FormularyStandardKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: true),
                    FormularyStandardName = table.Column<string>(maxLength: 50, nullable: false),
                    FormularyStandardDescription = table.Column<string>(maxLength: 250, nullable: true),
                    ADMQuantityRoundingFlag = table.Column<bool>(nullable: false),
                    ADMIgnoreStockoutFlag = table.Column<bool>(nullable: false),
                    ADMIgnoreCriticalLowFlag = table.Column<bool>(nullable: false),
                    RequireLotExpirationDuringRestockFlag = table.Column<bool>(nullable: false),
                    ExcludeFromInventoryReportFlag = table.Column<bool>(nullable: false),
                    EnableEarliestExpirationDateFlag = table.Column<bool>(nullable: false),
                    SendToPackagerFlag = table.Column<bool>(nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    CycleCountIntervalDay = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormularyStandard", x => x.FormularyStandardKey);
                });

            migrationBuilder.CreateTable(
                name: "GeneralLedgerAccount",
                schema: "dbo",
                columns: table => new
                {
                    GLAccountKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    AccountCode = table.Column<string>(type: "varchar(25)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    ActiveFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralLedgerAccount", x => x.GLAccountKey);
                });

            migrationBuilder.CreateTable(
                name: "PreferredOrdering",
                columns: table => new
                {
                    PreferredOrderingKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    VendorKey = table.Column<Guid>(nullable: false),
                    ProductIDKey = table.Column<Guid>(nullable: false),
                    ItemKey = table.Column<Guid>(nullable: false),
                    VendorItemCode = table.Column<string>(maxLength: 25, nullable: false),
                    IsPreferred = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreferredOrdering", x => x.PreferredOrderingKey);
                    table.ForeignKey(
                        name: "FK_PreferredOrdering_Item_ItemKey",
                        column: x => x.ItemKey,
                        principalTable: "Item",
                        principalColumn: "ItemKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalUOM",
                columns: table => new
                {
                    ExternalUOMKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    ExternalSystemKey = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    UOMKey = table.Column<Guid>(nullable: true),
                    UOMCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SortValue = table.Column<int>(nullable: true),
                    UseOnOutboundFlag = table.Column<bool>(nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalUOM", x => x.ExternalUOMKey);
                    table.ForeignKey(
                        name: "FK_ExternalUOM_UOM_UOMKey",
                        column: x => x.UOMKey,
                        principalTable: "UOM",
                        principalColumn: "UomKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityItem",
                columns: table => new
                {
                    FacilityItemKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    FacilityKey = table.Column<Guid>(nullable: false),
                    ItemKey = table.Column<Guid>(nullable: false),
                    UOMKey = table.Column<Guid>(nullable: false),
                    RestockRoundingFactor = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    PrepackFlag = table.Column<bool>(nullable: false),
                    FastmoverFlag = table.Column<bool>(nullable: false),
                    ForPackagerFlag = table.Column<bool>(nullable: false),
                    ADMQuantityRoundingFlag = table.Column<bool>(nullable: false),
                    ADMIgnoreStockOutFlag = table.Column<bool>(nullable: false),
                    ADMIgnoreCritLowFlag = table.Column<bool>(nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    EnableOldestExpirationDateFlag = table.Column<bool>(nullable: false),
                    PackageShareQuantityRoundingFlag = table.Column<bool>(nullable: false),
                    RequestRestockLotInfoFlag = table.Column<bool>(nullable: false),
                    RequiresSetupFlag = table.Column<bool>(nullable: false),
                    NeedReviewFlag = table.Column<bool>(nullable: false),
                    ConsignmentFlag = table.Column<bool>(nullable: false),
                    ExcludeFromInventoryReportFlag = table.Column<bool>(nullable: false),
                    CostAmount = table.Column<int>(nullable: true),
                    AverageWholesalePriceAmount = table.Column<decimal>(type: "decimal(14,4)", nullable: true),
                    AverageSalesPriceAmount = table.Column<decimal>(type: "decimal(14,4)", nullable: true),
                    CycleCountIntervalDay = table.Column<decimal>(type: "decimal(14,4)", nullable: true),
                    BulkItemKey = table.Column<Guid>(nullable: true),
                    OldestExpirationDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: true),
                    ConversionBarCodeValue = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    PrepackConversionFactor = table.Column<decimal>(type: "decimal(14,4)", nullable: true),
                    SpecialInstruction = table.Column<string>(type: "nvarchar(300)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityItem", x => x.FacilityItemKey);
                    table.ForeignKey(
                        name: "FK_FacilityItem_Item_ItemKey",
                        column: x => x.ItemKey,
                        principalTable: "Item",
                        principalColumn: "ItemKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacilityItem_UOM_UOMKey",
                        column: x => x.UOMKey,
                        principalTable: "UOM",
                        principalColumn: "UomKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedItem",
                columns: table => new
                {
                    MedicationItemKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    MedicationItemTypeInternalCode = table.Column<string>(type: "varchar(100)", nullable: true),
                    MedicationClassKey = table.Column<Guid>(nullable: true),
                    TotalVolumeUnitOfMeasureKey = table.Column<Guid>(nullable: true),
                    ConcentrationVolumeUnitOfMeasureKey = table.Column<Guid>(nullable: true),
                    StrengthUnitOfMeasureKey = table.Column<Guid>(nullable: true),
                    DispenseFormLookupKey = table.Column<Guid>(nullable: true),
                    DispenseUnitLookupKey = table.Column<Guid>(nullable: true),
                    TotalVolumeExternalUnitOfMeasureKey = table.Column<Guid>(nullable: true),
                    StrengthExternalUnitOfMeasureKey = table.Column<Guid>(nullable: true),
                    ConcentrationVolumeExternalUnitOfMeasureKey = table.Column<Guid>(nullable: true),
                    GLAccountKey = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(type: "varchar(225)", nullable: true),
                    GenericName = table.Column<string>(type: "varchar(225)", nullable: true),
                    ChargeCode = table.Column<string>(type: "varchar(25)", nullable: true),
                    HighRiskFlag = table.Column<bool>(nullable: false),
                    ChemotherapyFlag = table.Column<bool>(nullable: false),
                    NonFormularyFlag = table.Column<bool>(nullable: false),
                    LASAFlag = table.Column<bool>(nullable: false),
                    ActiveFlag = table.Column<bool>(nullable: false),
                    RefrigeratedFlag = table.Column<bool>(nullable: false),
                    DrugFlag = table.Column<bool>(nullable: false),
                    ChemoAgentFlag = table.Column<bool>(nullable: false),
                    BioHazFlag = table.Column<bool>(nullable: false),
                    HazAerosolFlag = table.Column<bool>(nullable: false),
                    HazBaseFlag = table.Column<bool>(nullable: false),
                    HazAcidFlag = table.Column<bool>(nullable: false),
                    HazChemicalFlag = table.Column<bool>(nullable: false),
                    HazOxidizerFlag = table.Column<bool>(nullable: false),
                    HazToxicFlag = table.Column<bool>(nullable: false),
                    FreezerFlag = table.Column<bool>(nullable: false),
                    RequiresSetupFlag = table.Column<bool>(nullable: false),
                    DeletedFlag = table.Column<bool>(nullable: false),
                    PrepHazFlag = table.Column<bool>(nullable: false),
                    StrengthAmount = table.Column<double>(nullable: true),
                    StrengthText = table.Column<string>(nullable: true),
                    ConcentrationVolumeAmount = table.Column<double>(nullable: true),
                    TotalVolumeAmount = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedItem", x => x.MedicationItemKey);
                    table.ForeignKey(
                        name: "FK_MedItem_DispenseForm_DispenseFormLookupKey",
                        column: x => x.DispenseFormLookupKey,
                        principalSchema: "dbo",
                        principalTable: "DispenseForm",
                        principalColumn: "DispenseFormLookupKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedItem_DispenseUnit_DispenseUnitLookupKey",
                        column: x => x.DispenseUnitLookupKey,
                        principalSchema: "dbo",
                        principalTable: "DispenseUnit",
                        principalColumn: "DispenseUnitLookupKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedItem_GeneralLedgerAccount_GLAccountKey",
                        column: x => x.GLAccountKey,
                        principalSchema: "dbo",
                        principalTable: "GeneralLedgerAccount",
                        principalColumn: "GLAccountKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MedItem_Item_MedicationItemKey",
                        column: x => x.MedicationItemKey,
                        principalTable: "Item",
                        principalColumn: "ItemKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductID",
                schema: "dbo",
                columns: table => new
                {
                    ProductIDKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    ItemKey = table.Column<Guid>(nullable: false),
                    ManufacturerID = table.Column<Guid>(nullable: true),
                    MedClassKey = table.Column<Guid>(nullable: true),
                    ProductID = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    AltCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    GenericName = table.Column<string>(type: "nvarchar(225)", nullable: true),
                    TradeName = table.Column<string>(type: "nvarchar(225)", nullable: true),
                    PackageSize = table.Column<int>(nullable: false),
                    DrugFlag = table.Column<bool>(type: "bit", nullable: false),
                    ActiveFlag = table.Column<bool>(type: "bit", nullable: false),
                    FromExternalSystemFlag = table.Column<bool>(type: "bit", nullable: false),
                    LinkedByUserAccountKey = table.Column<Guid>(nullable: true),
                    VerifiedByUserAccountKey = table.Column<Guid>(nullable: true),
                    LinkedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    VerifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    CreatedByExternalSystemName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    DeletedByExternalSystemName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Strength = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Volume = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    TotalVolume = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductID", x => x.ProductIDKey);
                    table.ForeignKey(
                        name: "FK_ProductID_Item_ItemKey",
                        column: x => x.ItemKey,
                        principalTable: "Item",
                        principalColumn: "ItemKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductID_MedItem_ItemKey",
                        column: x => x.ItemKey,
                        principalTable: "MedItem",
                        principalColumn: "MedicationItemKey",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductIDRecommendedItem",
                columns: table => new
                {
                    ProductIDRecommendedItemKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<Guid>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    LastModifiedByActorKey = table.Column<Guid>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(type: "DateTimeOffset(7)", nullable: false),
                    ResultingProductIdentificationKey = table.Column<Guid>(nullable: false),
                    RecommendedItemKey = table.Column<Guid>(nullable: false),
                    RecommendedMedicationItemKey = table.Column<Guid>(nullable: false),
                    RecommendedItemResultInternalCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    UserAccountKey = table.Column<Guid>(nullable: false),
                    SequenceNumber = table.Column<int>(nullable: false),
                    ProductID = table.Column<string>(type: "varchar(20)", nullable: false),
                    RecommandationFollowedFlag = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIDRecommendedItem", x => x.ProductIDRecommendedItemKey);
                    table.ForeignKey(
                        name: "FK_ProductIDRecommendedItem_Item_RecommendedItemKey",
                        column: x => x.RecommendedItemKey,
                        principalTable: "Item",
                        principalColumn: "ItemKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIDRecommendedItem_RecommendedItemResult_RecommendedItemResultInternalCode",
                        column: x => x.RecommendedItemResultInternalCode,
                        principalTable: "RecommendedItemResult",
                        principalColumn: "RecommendedItemResultInternalCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIDRecommendedItem_MedItem_RecommendedMedicationItemKey",
                        column: x => x.RecommendedMedicationItemKey,
                        principalTable: "MedItem",
                        principalColumn: "MedicationItemKey",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductIDRecommendedItem_ProductID_ResultingProductIdentificationKey",
                        column: x => x.ResultingProductIdentificationKey,
                        principalSchema: "dbo",
                        principalTable: "ProductID",
                        principalColumn: "ProductIDKey",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemKey", "AlternateItemID", "CreatedByActorKey", "CreatedDateTime", "CustomField1Text", "CustomField2Text", "CustomField3Text", "DeleteFlag", "EndUTCTime", "EnterpriseItemID", "ExternalSystemDeleteUTCDatetime", "ExternalSystemKey", "FacilityKey", "ItemID", "LastModifiedByActorKey", "LastModifiedDateTime", "LastModifiedUTCDateTime", "MedicationItemFlag", "StartUTCDateTime", "TenantKey" },
                values: new object[] { new Guid("9b9134d6-81d3-4460-92ad-cd1790aa4c60"), "AA1", new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, null, false, null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "A1", new Guid("00000000-0000-0000-0000-000000000000"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 5, 8, 20, 36, 49, 280, DateTimeKind.Unspecified).AddTicks(434), new TimeSpan(0, 5, 30, 0, 0)), true, new DateTimeOffset(new DateTime(2019, 5, 8, 20, 36, 49, 272, DateTimeKind.Unspecified).AddTicks(5045), new TimeSpan(0, 5, 30, 0, 0)), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalUOM_UOMKey",
                table: "ExternalUOM",
                column: "UOMKey");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityItem_ItemKey",
                table: "FacilityItem",
                column: "ItemKey");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityItem_UOMKey",
                table: "FacilityItem",
                column: "UOMKey");

            migrationBuilder.CreateIndex(
                name: "IX_MedItem_DispenseFormLookupKey",
                table: "MedItem",
                column: "DispenseFormLookupKey");

            migrationBuilder.CreateIndex(
                name: "IX_MedItem_DispenseUnitLookupKey",
                table: "MedItem",
                column: "DispenseUnitLookupKey");

            migrationBuilder.CreateIndex(
                name: "IX_MedItem_GLAccountKey",
                table: "MedItem",
                column: "GLAccountKey");

            migrationBuilder.CreateIndex(
                name: "IX_PreferredOrdering_ItemKey",
                table: "PreferredOrdering",
                column: "ItemKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductIDRecommendedItem_RecommendedItemKey",
                table: "ProductIDRecommendedItem",
                column: "RecommendedItemKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIDRecommendedItem_RecommendedItemResultInternalCode",
                table: "ProductIDRecommendedItem",
                column: "RecommendedItemResultInternalCode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIDRecommendedItem_RecommendedMedicationItemKey",
                table: "ProductIDRecommendedItem",
                column: "RecommendedMedicationItemKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIDRecommendedItem_ResultingProductIdentificationKey",
                table: "ProductIDRecommendedItem",
                column: "ResultingProductIdentificationKey");

            migrationBuilder.CreateIndex(
                name: "IX_UOM_BaseUomKey",
                table: "UOM",
                column: "BaseUomKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProductID_ItemKey",
                schema: "dbo",
                table: "ProductID",
                column: "ItemKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalUOM");

            migrationBuilder.DropTable(
                name: "FacilityItem");

            migrationBuilder.DropTable(
                name: "NDCEntities");

            migrationBuilder.DropTable(
                name: "PreferredOrdering");

            migrationBuilder.DropTable(
                name: "ProductIDRecommendedItem");

            migrationBuilder.DropTable(
                name: "CS_Formulary_FacililtyNDC",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CS_Formulary_FacilityFormulary",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CS_Formulary_Formulary",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FormularyStandard",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UOM");

            migrationBuilder.DropTable(
                name: "RecommendedItemResult");

            migrationBuilder.DropTable(
                name: "ProductID",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MedItem");

            migrationBuilder.DropTable(
                name: "DispenseForm",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "DispenseUnit",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GeneralLedgerAccount",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
