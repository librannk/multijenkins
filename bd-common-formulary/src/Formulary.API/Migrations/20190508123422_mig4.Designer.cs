﻿// <auto-generated />
using System;
using Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Formulary.API.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20190508123422_mig4")]
    partial class mig4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.DispensingFormLookupEntity", b =>
                {
                    b.Property<Guid>("DispenseFormLookupKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("DispenseForm")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("DispenseFormLookupKey");

                    b.ToTable("DispenseForm","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.DispensingUnitLookupEntity", b =>
                {
                    b.Property<Guid>("DispenseUnitLookupKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("DispenseUnit")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("DispenseUnitLookupKey");

                    b.ToTable("DispenseUnit","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ExternalUomEntity", b =>
                {
                    b.Property<Guid>("ExternalUOMKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("ExternalSystemKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<int?>("SortValue");

                    b.Property<Guid>("TenantKey");

                    b.Property<string>("UOMCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("UOMKey");

                    b.Property<bool>("UseOnOutboundFlag");

                    b.HasKey("ExternalUOMKey");

                    b.HasIndex("UOMKey");

                    b.ToTable("ExternalUOM");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.FacilityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ADUIgnoreCritLow");

                    b.Property<bool>("ADUIgnoreStockOut");

                    b.Property<bool>("ADUQtyRounding");

                    b.Property<bool>("Active");

                    b.Property<bool>("Approved");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("FacilityId");

                    b.Property<int>("FormularyId");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("CS_Formulary_FacilityFormulary","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.FacilityItemEntity", b =>
                {
                    b.Property<Guid>("FacilityItemKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ADMIgnoreCritLowFlag");

                    b.Property<bool>("ADMIgnoreStockOutFlag");

                    b.Property<bool>("ADMQuantityRoundingFlag");

                    b.Property<bool>("ActiveFlag");

                    b.Property<decimal?>("AverageSalesPriceAmount")
                        .HasColumnType("decimal(14,4)");

                    b.Property<decimal?>("AverageWholesalePriceAmount")
                        .HasColumnType("decimal(14,4)");

                    b.Property<Guid?>("BulkItemKey");

                    b.Property<bool>("ConsignmentFlag");

                    b.Property<string>("ConversionBarCodeValue")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("CostAmount");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<decimal?>("CycleCountIntervalDay")
                        .HasColumnType("decimal(14,4)");

                    b.Property<bool>("EnableOldestExpirationDateFlag");

                    b.Property<bool>("ExcludeFromInventoryReportFlag");

                    b.Property<Guid>("FacilityKey");

                    b.Property<bool>("FastmoverFlag");

                    b.Property<bool>("ForPackagerFlag");

                    b.Property<Guid>("ItemKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("NeedReviewFlag");

                    b.Property<DateTimeOffset?>("OldestExpirationDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("PackageShareQuantityRoundingFlag");

                    b.Property<decimal?>("PrepackConversionFactor")
                        .HasColumnType("decimal(14,4)");

                    b.Property<bool>("PrepackFlag");

                    b.Property<bool>("RequestRestockLotInfoFlag");

                    b.Property<bool>("RequiresSetupFlag");

                    b.Property<decimal?>("RestockRoundingFactor")
                        .HasColumnType("decimal(3,1)");

                    b.Property<string>("SpecialInstruction")
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("TenantKey");

                    b.Property<Guid>("UOMKey");

                    b.HasKey("FacilityItemKey");

                    b.HasIndex("ItemKey");

                    b.HasIndex("UOMKey");

                    b.ToTable("FacilityItem");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.FacilityNDCAssocEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("FacilityId");

                    b.Property<bool>("IsPreferred");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("NDCId");

                    b.HasKey("Id");

                    b.ToTable("CS_Formulary_FacililtyNDC","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.FormularyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description");

                    b.Property<int>("ItemId");

                    b.Property<string>("ItemName");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("CS_Formulary_Formulary","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.FormularyStandard", b =>
                {
                    b.Property<Guid>("FormularyStandardKey");

                    b.Property<bool>("ADMIgnoreCriticalLowFlag");

                    b.Property<bool>("ADMIgnoreStockoutFlag");

                    b.Property<bool>("ADMQuantityRoundingFlag");

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<int?>("CycleCountIntervalDay");

                    b.Property<bool>("EnableEarliestExpirationDateFlag");

                    b.Property<bool>("ExcludeFromInventoryReportFlag");

                    b.Property<string>("FormularyStandardDescription")
                        .HasMaxLength(250);

                    b.Property<string>("FormularyStandardName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("RequireLotExpirationDuringRestockFlag");

                    b.Property<bool>("SendToPackagerFlag");

                    b.Property<Guid?>("TenantKey");

                    b.HasKey("FormularyStandardKey");

                    b.ToTable("FormularyStandard","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.GLAccountEntity", b =>
                {
                    b.Property<Guid>("GLAccountKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountCode")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("GLAccountKey");

                    b.ToTable("GeneralLedgerAccount","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ItemEntity", b =>
                {
                    b.Property<Guid>("ItemKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlternateItemID")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("CustomField1Text")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CustomField2Text")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CustomField3Text")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("DeleteFlag");

                    b.Property<string>("EnterpriseItemID")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTimeOffset?>("ExternalSystemDeleteUTCDatetime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("ExternalSystemKey");

                    b.Property<Guid>("FacilityKey");

                    b.Property<string>("ItemID")
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("MedicationItemFlag");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("ItemKey");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.MedicationItemEntity", b =>
                {
                    b.Property<Guid>("MedicationItemKey");

                    b.Property<bool>("ActiveFlag");

                    b.Property<bool>("BioHazFlag");

                    b.Property<string>("ChargeCode")
                        .HasColumnType("varchar(25)");

                    b.Property<bool>("ChemoAgentFlag");

                    b.Property<bool>("ChemotherapyFlag");

                    b.Property<double?>("ConcentrationVolumeAmount");

                    b.Property<Guid?>("ConcentrationVolumeExternalUnitOfMeasureKey");

                    b.Property<Guid?>("ConcentrationVolumeUnitOfMeasureKey");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("DeletedFlag");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(225)");

                    b.Property<Guid?>("DispenseFormLookupKey");

                    b.Property<Guid?>("DispenseUnitLookupKey");

                    b.Property<bool>("DrugFlag");

                    b.Property<bool>("FreezerFlag");

                    b.Property<Guid?>("GLAccountKey");

                    b.Property<string>("GenericName")
                        .HasColumnType("varchar(225)");

                    b.Property<bool>("HazAcidFlag");

                    b.Property<bool>("HazAerosolFlag");

                    b.Property<bool>("HazBaseFlag");

                    b.Property<bool>("HazChemicalFlag");

                    b.Property<bool>("HazOxidizer");

                    b.Property<bool>("HazToxicFlag");

                    b.Property<bool>("HighRiskFlag");

                    b.Property<bool>("LASAFlag");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid?>("MedicationClassKey");

                    b.Property<string>("MedicationItemTypeInternalCode")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("NonFormularyFlag");

                    b.Property<bool>("PrepHazFlag");

                    b.Property<bool>("RefrigeratedFlag");

                    b.Property<bool>("RequiresSetupFlag");

                    b.Property<double?>("StrengthAmount");

                    b.Property<Guid?>("StrengthExternalUnitOfMeasureKey");

                    b.Property<string>("StrengthText");

                    b.Property<Guid?>("StrengthUnitOfMeasureKey");

                    b.Property<Guid>("TenantKey");

                    b.Property<double?>("TotalVolumeAmount");

                    b.Property<Guid?>("TotalVolumeExternalUnitOfMeasureKey");

                    b.Property<Guid?>("TotalVolumeUnitOfMeasureKey");

                    b.HasKey("MedicationItemKey");

                    b.HasIndex("DispenseFormLookupKey");

                    b.HasIndex("DispenseUnitLookupKey");

                    b.HasIndex("GLAccountKey");

                    b.ToTable("MedItem");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.NDCEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int>("FormularyId");

                    b.Property<string>("GenericName");

                    b.Property<int?>("LastModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("NDC");

                    b.Property<string>("TradeName");

                    b.HasKey("Id");

                    b.ToTable("NDCEntities");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.PreferredOrderingEntity", b =>
                {
                    b.Property<Guid>("PreferredOrderingKey")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("IsPreferred");

                    b.Property<Guid>("ItemKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("ProductIDKey");

                    b.Property<string>("VendorItemCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<Guid>("VendorKey");

                    b.HasKey("PreferredOrderingKey");

                    b.HasIndex("ItemKey")
                        .IsUnique();

                    b.ToTable("PreferredOrderingEntity");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ProductIDRecommendedItemEntity", b =>
                {
                    b.Property<Guid>("ProductIDRecommendedItemKey")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("ProductID")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("RecommandationFollowedFlag")
                        .HasColumnType("bit");

                    b.Property<Guid>("RecommendedItemKey");

                    b.Property<string>("RecommendedItemResultInternalCode")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("RecommendedMedicationItemKey");

                    b.Property<Guid>("ResultingProductIdentificationKey");

                    b.Property<int>("SequenceNumber");

                    b.Property<Guid>("UserAccountKey");

                    b.HasKey("ProductIDRecommendedItemKey");

                    b.HasIndex("RecommendedItemKey");

                    b.HasIndex("RecommendedItemResultInternalCode");

                    b.HasIndex("RecommendedMedicationItemKey");

                    b.HasIndex("ResultingProductIdentificationKey");

                    b.ToTable("ProductIDRecommendedItem");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ProductIdentificationEntity", b =>
                {
                    b.Property<Guid>("ProductIDKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag")
                        .HasColumnType("bit");

                    b.Property<string>("AltCode")
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<string>("CreatedByExternalSystemName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("DeletedByExternalSystemName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("DrugFlag")
                        .HasColumnType("bit");

                    b.Property<bool>("FromExternalSystemFlag")
                        .HasColumnType("bit");

                    b.Property<string>("GenericName")
                        .HasColumnType("nvarchar(225)");

                    b.Property<Guid>("ItemKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid?>("LinkedByUserAccountKey");

                    b.Property<DateTimeOffset>("LinkedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<Guid?>("ManufacturerID");

                    b.Property<Guid?>("MedClassKey");

                    b.Property<int>("PackageSize");

                    b.Property<string>("ProductID")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Strength")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TotalVolume")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TradeName")
                        .HasColumnType("nvarchar(225)");

                    b.Property<Guid?>("VerifiedByUserAccountKey");

                    b.Property<DateTimeOffset>("VerifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("Volume")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ProductIDKey");

                    b.HasIndex("ItemKey");

                    b.ToTable("ProductID","dbo");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.RecommendedItemResultEntity", b =>
                {
                    b.Property<string>("RecommendedItemResultInternalCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("DescriptionText")
                        .HasColumnType("varchar(50)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<int>("SortValue");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("RecommendedItemResultInternalCode");

                    b.ToTable("RecommendedItemResult");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity", b =>
                {
                    b.Property<Guid>("UomKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid?>("BaseUomKey");

                    b.Property<decimal?>("ConversionAmount")
                        .HasColumnType("decimal(28,14)");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<string>("DescriptionText")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<string>("DisplayCode")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("InternalCode")
                        .HasColumnType("varchar(10)");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedDateTime")
                        .HasColumnType("DateTimeOffset(7)");

                    b.Property<bool>("PredefinedFlag");

                    b.Property<bool>("StrengthFlag");

                    b.Property<Guid>("TenantKey");

                    b.Property<bool>("VolumeFlag");

                    b.HasKey("UomKey");

                    b.HasIndex("BaseUomKey");

                    b.ToTable("UOM");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ExternalUomEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity", "UomEntity")
                        .WithMany("ExternalUoms")
                        .HasForeignKey("UOMKey");
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.FacilityItemEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ItemEntity", "ItemEntity")
                        .WithMany("FacilityItems")
                        .HasForeignKey("ItemKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity", "UomEntity")
                        .WithMany("FacilityItems")
                        .HasForeignKey("UOMKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.MedicationItemEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.DispensingFormLookupEntity", "dispensingFormLookup")
                        .WithMany("MedicationItems")
                        .HasForeignKey("DispenseFormLookupKey");

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.DispensingUnitLookupEntity", "dispensingUnitLookup")
                        .WithMany("MedicationItems")
                        .HasForeignKey("DispenseUnitLookupKey");

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.GLAccountEntity", "GLAccount")
                        .WithMany("MedicationItems")
                        .HasForeignKey("GLAccountKey");

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ItemEntity", "Item")
                        .WithOne("MedicationItem")
                        .HasForeignKey("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.MedicationItemEntity", "MedicationItemKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.PreferredOrderingEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ItemEntity")
                        .WithOne("PreferredOrderings")
                        .HasForeignKey("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.PreferredOrderingEntity", "ItemKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ProductIDRecommendedItemEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ItemEntity", "Item")
                        .WithMany("ProductIDRecommendedItems")
                        .HasForeignKey("RecommendedItemKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.RecommendedItemResultEntity", "RecommendedItemResult")
                        .WithMany("ProductIDRecommendedItems")
                        .HasForeignKey("RecommendedItemResultInternalCode")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.MedicationItemEntity", "MedicationItem")
                        .WithMany("ProductIDRecommendedItems")
                        .HasForeignKey("RecommendedMedicationItemKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ProductIdentificationEntity", "ProductIdentification")
                        .WithMany("ProductIDRecommendedItems")
                        .HasForeignKey("ResultingProductIdentificationKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ProductIdentificationEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.ItemEntity", "Item")
                        .WithMany("ProductIdentifications")
                        .HasForeignKey("ItemKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.MedicationItemEntity", "MedicationItem")
                        .WithMany("ProductIdentifications")
                        .HasForeignKey("ItemKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity", b =>
                {
                    b.HasOne("Formulary.API.Infrastructure.DataAccess.SQL.DBContextEntities.Entities.UomEntity", "Uom")
                        .WithMany("Uoms")
                        .HasForeignKey("BaseUomKey");
                });
#pragma warning restore 612, 618
        }
    }
}
