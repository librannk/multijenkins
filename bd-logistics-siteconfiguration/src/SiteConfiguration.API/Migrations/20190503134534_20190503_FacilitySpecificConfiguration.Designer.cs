﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiteConfiguration.API.Infrastructure.DataAccess.SQL.DBContextEntities;

namespace SiteConfiguration.API.Migrations
{    
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190503134534_20190503_changes")]
    partial class _20190503_changes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SiteConfiguration.API.FacilityConfiguration.Models.FacilityLogisticsConfig", b =>
                {
                    b.Property<Guid>("FacilityKey")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ADMDupeTimeDelay");

                    b.Property<bool>("ADMIgnoreCritLowFlag");

                    b.Property<bool>("ADMIgnoreStockOutFlag");

                    b.Property<bool>("ADMQuantityRoundingFlag");

                    b.Property<bool>("AutoCalcSlowPrepackFlag");

                    b.Property<bool>("AutoGenCycleCountFlag");

                    b.Property<bool>("AutoGenCycleCountScheduleFlag");

                    b.Property<Guid?>("BinLabelPrinterKey");

                    b.Property<bool>("CalcPrepackWithPOFlag");

                    b.Property<bool>("CaptureLotExpirationFlag");

                    b.Property<string>("CostCenterCode");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<bool>("EnableOldestExpirationDateFlag");

                    b.Property<Guid?>("ExceptionPrinterKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<Guid?>("ManualRestockPrinterKey");

                    b.Property<bool>("NotifyOrderPickedFlag");

                    b.Property<bool>("PackageShareQuantityRoundingFlag");

                    b.Property<bool>("PrepackBulkSuffixFlag");

                    b.Property<bool>("PrepositionISAFlag");

                    b.Property<bool>("PrintCompositLabelFlag");

                    b.Property<bool>("PrintLabelOnQtyChangeFlag");

                    b.Property<bool>("ProcessInactiveAsExceptionFlag");

                    b.Property<bool>("ReceiveAndSendRemoteOrdersFlag");

                    b.Property<Guid?>("ReceivingPrinterKey");

                    b.Property<bool>("ReqRestockDestFlag");

                    b.Property<bool>("RequestRestockLotInfoFlag");

                    b.Property<bool>("RequireHoldReasonFlag");

                    b.Property<bool>("ReturnsOnHoldDefFlag");

                    b.Property<bool>("ScanTranLabelFlag");

                    b.Property<bool>("ScanVerifyFlag");

                    b.Property<bool>("SendCheckStationPickInfoFlag");

                    b.Property<bool>("SeparateOrderByISAFlag");

                    b.Property<bool>("SmartOrderRoutingFlag");

                    b.Property<bool>("SubmitGeneralLedgerInfoFlag");

                    b.Property<bool>("SubmitMedListInfoFlag");

                    b.Property<bool>("SubmitReturnCreditFlag");

                    b.Property<Guid>("TenantKey");

                    b.Property<bool>("UsePreferredOrderingFlag");

                    b.Property<bool>("VerifyMultiDispenseFlag");

                    b.Property<bool>("VerifyQuantityFlag");

                    b.HasKey("FacilityKey");

                    b.ToTable("FacilitySpecificConfigurations");
                });

            modelBuilder.Entity("SiteConfiguration.API.Printers.Models.Data.Printer", b =>
                {
                    b.Property<Guid>("PrinterKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<string>("Description");

                    b.Property<Guid>("FacilityKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<string>("PrinterName");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("PrinterKey");

                    b.ToTable("Printer");
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRule", b =>
                {
                    b.Property<Guid>("RoutingRuleKey");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<Guid>("FacilityKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<string>("RoutingRuleName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<short>("SearchCriteriaGranularityLevel");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("RoutingRuleKey")
                        .HasName("PK__RoutingR__0FB1C17FA55401AA");

                    b.ToTable("RoutingRule");
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRuleDestination", b =>
                {
                    b.Property<Guid>("RoutingRuleDestinationKey");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<Guid>("DestinationKey");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<Guid>("RoutingRuleKey");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("RoutingRuleDestinationKey")
                        .HasName("PK__RoutingR__E19F25F328C0BF07");

                    b.HasIndex("RoutingRuleKey");

                    b.ToTable("RoutingRuleDestination");
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRuleScheduleTiming", b =>
                {
                    b.Property<Guid>("RoutingRuleScheduleTimingKey");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<Guid>("RoutingRuleKey");

                    b.Property<Guid>("ScheduleTimingKey");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("RoutingRuleScheduleTimingKey")
                        .HasName("PK__RoutingR__52CA6676390D6362");

                    b.HasIndex("RoutingRuleKey");

                    b.HasIndex("ScheduleTimingKey");

                    b.ToTable("RoutingRuleScheduleTiming");
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRuleTranPriority", b =>
                {
                    b.Property<Guid>("RoutingRuleTranPriorityKey");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<Guid>("RoutingRuleKey");

                    b.Property<Guid>("TenantKey");

                    b.Property<Guid>("TranPriorityKey");

                    b.HasKey("RoutingRuleTranPriorityKey")
                        .HasName("PK__RoutingR__4DA1E626D911327A");

                    b.HasIndex("RoutingRuleKey");

                    b.HasIndex("TranPriorityKey");

                    b.ToTable("RoutingRuleTranPriority");
                });

            modelBuilder.Entity("SiteConfiguration.API.Schedule.Models.ScheduleTiming", b =>
                {
                    b.Property<Guid>("ScheduleTimingKey")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<int>("EndMinutes");

                    b.Property<Guid>("FacilityKey");

                    b.Property<bool>("FridayFlag");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<bool>("MondayFlag");

                    b.Property<bool>("SaturdayFlag");

                    b.Property<string>("ScheduleTimingName");

                    b.Property<int>("StartMinutes");

                    b.Property<bool>("SundayFlag");

                    b.Property<Guid>("TenantKey");

                    b.Property<bool>("ThursdayFlag");

                    b.Property<bool>("TuesdayFlag");

                    b.Property<bool>("WednesdayFlag");

                    b.HasKey("ScheduleTimingKey");

                    b.ToTable("ScheduleTiming");
                });

            modelBuilder.Entity("SiteConfiguration.API.TransactionPriority.Models.SmartSort", b =>
                {
                    b.Property<Guid>("TransPriorityKey");

                    b.Property<Guid>("SmartSortColumnKey");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<int>("SmartSortOrder");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("TransPriorityKey", "SmartSortColumnKey")
                        .HasName("pk__smartsor__3ee1053ec47e2d08");

                    b.HasIndex("SmartSortColumnKey");

                    b.ToTable("SmartSort");
                });

            modelBuilder.Entity("SiteConfiguration.API.TransactionPriority.Models.SmartSortColumn", b =>
                {
                    b.Property<Guid>("SmartSortColumnKey")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveFlag");

                    b.Property<string>("ColumnNameText");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<string>("FriendlyColumnNameText");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<Guid>("TenantKey");

                    b.HasKey("SmartSortColumnKey");

                    b.ToTable("SmartSortColumn");
                });

            modelBuilder.Entity("SiteConfiguration.API.TransactionPriority.Models.TransactionPriority", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("TranPriorityKey");

                    b.Property<bool>("ADUFlag")
                        .HasColumnName("aduflag");

                    b.Property<bool>("ActiveFlag");

                    b.Property<bool>("AutoReceiveFlag");

                    b.Property<Guid>("CreatedByActorKey");

                    b.Property<DateTimeOffset>("CreatedDateUTCDateTime");

                    b.Property<Guid>("FacilityKey");

                    b.Property<bool>("ForManualPickFlag");

                    b.Property<bool>("ForManualRestockFlag");

                    b.Property<Guid>("LastModifiedByActorKey");

                    b.Property<DateTimeOffset>("LastModifiedUTCDateTime");

                    b.Property<string>("LegendBackColor")
                        .HasMaxLength(25);

                    b.Property<string>("LegendForeColor")
                        .HasMaxLength(25);

                    b.Property<int>("MaxOnHoldLength");

                    b.Property<Guid>("PrintLabelKey");

                    b.Property<string>("PriorityCode")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("PriorityName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("PriorityOrder");

                    b.Property<bool>("SystemFlag");

                    b.Property<Guid>("TenantKey");

                    b.Property<bool>("UseInterfaceMedNameFlag");

                    b.HasKey("Id")
                        .HasName("pk__transact__3fc0e8949c65ee1d");

                    b.ToTable("TransactionPriority","dbo");
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRuleDestination", b =>
                {
                    b.HasOne("SiteConfiguration.API.RoutingRules.Models.RoutingRule", "RoutingRuleKeyNavigation")
                        .WithMany("RoutingRuleDestination")
                        .HasForeignKey("RoutingRuleKey")
                        .HasConstraintName("FK__RoutingRu__Routi__123EB7A3")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRuleScheduleTiming", b =>
                {
                    b.HasOne("SiteConfiguration.API.RoutingRules.Models.RoutingRule", "RoutingRuleKeyNavigation")
                        .WithMany("RoutingRuleScheduleTiming")
                        .HasForeignKey("RoutingRuleKey")
                        .HasConstraintName("FK__RoutingRu__Routi__0E6E26BF")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteConfiguration.API.Schedule.Models.ScheduleTiming", "ScheduleTimingKeyNavigation")
                        .WithMany("RoutingRuleScheduleTiming")
                        .HasForeignKey("ScheduleTimingKey")
                        .HasConstraintName("FK__RoutingRu__Sched__0D7A0286")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteConfiguration.API.RoutingRules.Models.RoutingRuleTranPriority", b =>
                {
                    b.HasOne("SiteConfiguration.API.RoutingRules.Models.RoutingRule", "RoutingRuleKeyNavigation")
                        .WithMany("RoutingRuleTranPriority")
                        .HasForeignKey("RoutingRuleKey")
                        .HasConstraintName("FK__RoutingRu__Routi__55F4C372")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteConfiguration.API.TransactionPriority.Models.TransactionPriority", "TranPriorityKeyNavigation")
                        .WithMany("RoutingRuleTranPriority")
                        .HasForeignKey("TranPriorityKey")
                        .HasConstraintName("FK__RoutingRu__TranP__55009F39")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteConfiguration.API.TransactionPriority.Models.SmartSort", b =>
                {
                    b.HasOne("SiteConfiguration.API.TransactionPriority.Models.SmartSortColumn", "SmartSortColumnKeyNavigation")
                        .WithMany("SmartSort")
                        .HasForeignKey("SmartSortColumnKey")
                        .HasConstraintName("fk__smartsort__smart__625a9a57")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteConfiguration.API.TransactionPriority.Models.TransactionPriority", "TransPriorityKeyNavigation")
                        .WithMany("SmartSort")
                        .HasForeignKey("TransPriorityKey")
                        .HasConstraintName("fk__smartsort__trans__6166761e")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
