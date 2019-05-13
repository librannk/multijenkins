using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facility.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ControlledSubstanceLicenses",
                schema: "dbo",
                columns: table => new
                {
                    ControlledSubstanceLicenseKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedByActorKey = table.Column<string>(nullable: true),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    LicenseId = table.Column<string>(maxLength: 100, nullable: true),
                    ExternalFlag = table.Column<bool>(nullable: false),
                    StreetAddressText = table.Column<string>(maxLength: 120, nullable: true),
                    CityName = table.Column<string>(maxLength: 50, nullable: true),
                    SubdivisionName = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 20, nullable: true),
                    CountryName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlledSubstanceLicenses", x => x.ControlledSubstanceLicenseKey);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                schema: "dbo",
                columns: table => new
                {
                    FacilityKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedByActorKey = table.Column<string>(nullable: true),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    PharmacyInformationSystemKey = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(maxLength: 50, nullable: true),
                    FacilityCode = table.Column<string>(maxLength: 20, nullable: true),
                    TimeZoneId = table.Column<string>(type: "VARCHAR(64)", nullable: true),
                    SiteId = table.Column<string>(maxLength: 50, nullable: true),
                    DescriptionText = table.Column<string>(maxLength: 100, nullable: true),
                    CustomerContactName = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerContactPhoneNumberText = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerContactFaxNumberText = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerContactEmailAddressValue = table.Column<string>(maxLength: 50, nullable: true),
                    StreetAddressText = table.Column<string>(maxLength: 120, nullable: true),
                    StreetAddress2Text = table.Column<string>(maxLength: 50, nullable: true),
                    CityName = table.Column<string>(maxLength: 50, nullable: true),
                    SubDivisionName = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 20, nullable: true),
                    CountryName = table.Column<string>(maxLength: 50, nullable: true),
                    RxLicenseId = table.Column<string>(maxLength: 20, nullable: true),
                    ActiveFlag = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityKey);
                });

            migrationBuilder.CreateTable(
                name: "ControlledSubstanceLicenseFacilities",
                schema: "dbo",
                columns: table => new
                {
                    ControlledSubstanceLicenseFacilityKey = table.Column<Guid>(nullable: false),
                    CreatedByActorKey = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    LastModifiedByActorKey = table.Column<string>(nullable: true),
                    LastModifiedDateTime = table.Column<DateTimeOffset>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    FacilityKey = table.Column<Guid>(nullable: false),
                    ControlledSubstanceLicenseKey = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlledSubstanceLicenseFacilities", x => x.ControlledSubstanceLicenseFacilityKey);
                    table.ForeignKey(
                        name: "FK_ControlledSubstanceLicenseFacilities_ControlledSubstanceLicenses_ControlledSubstanceLicenseKey",
                        column: x => x.ControlledSubstanceLicenseKey,
                        principalSchema: "dbo",
                        principalTable: "ControlledSubstanceLicenses",
                        principalColumn: "ControlledSubstanceLicenseKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlledSubstanceLicenseFacilities_Facilities_FacilityKey",
                        column: x => x.FacilityKey,
                        principalSchema: "dbo",
                        principalTable: "Facilities",
                        principalColumn: "FacilityKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    Rel = table.Column<string>(nullable: false),
                    Href = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    ControlledSubstanceLicenseFacilityKey = table.Column<Guid>(nullable: true),
                    ControlledSubstanceLicenseKey = table.Column<Guid>(nullable: true),
                    FacilityEntityFacilityKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.Rel);
                    table.ForeignKey(
                        name: "FK_Model_ControlledSubstanceLicenseFacilities_ControlledSubstanceLicenseFacilityKey",
                        column: x => x.ControlledSubstanceLicenseFacilityKey,
                        principalSchema: "dbo",
                        principalTable: "ControlledSubstanceLicenseFacilities",
                        principalColumn: "ControlledSubstanceLicenseFacilityKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Model_ControlledSubstanceLicenses_ControlledSubstanceLicenseKey",
                        column: x => x.ControlledSubstanceLicenseKey,
                        principalSchema: "dbo",
                        principalTable: "ControlledSubstanceLicenses",
                        principalColumn: "ControlledSubstanceLicenseKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Model_Facilities_FacilityEntityFacilityKey",
                        column: x => x.FacilityEntityFacilityKey,
                        principalSchema: "dbo",
                        principalTable: "Facilities",
                        principalColumn: "FacilityKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model_ControlledSubstanceLicenseFacilityKey",
                table: "Model",
                column: "ControlledSubstanceLicenseFacilityKey");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ControlledSubstanceLicenseKey",
                table: "Model",
                column: "ControlledSubstanceLicenseKey");

            migrationBuilder.CreateIndex(
                name: "IX_Model_FacilityEntityFacilityKey",
                table: "Model",
                column: "FacilityEntityFacilityKey");

            migrationBuilder.CreateIndex(
                name: "IX_ControlledSubstanceLicenseFacilities_ControlledSubstanceLicenseKey",
                schema: "dbo",
                table: "ControlledSubstanceLicenseFacilities",
                column: "ControlledSubstanceLicenseKey");

            migrationBuilder.CreateIndex(
                name: "IX_ControlledSubstanceLicenseFacilities_FacilityKey",
                schema: "dbo",
                table: "ControlledSubstanceLicenseFacilities",
                column: "FacilityKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model");

            migrationBuilder.DropTable(
                name: "ControlledSubstanceLicenseFacilities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ControlledSubstanceLicenses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Facilities",
                schema: "dbo");
        }
    }
}
