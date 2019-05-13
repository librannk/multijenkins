using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facility.API.Migrations
{
    public partial class _20190426_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Model");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    Rel = table.Column<string>(nullable: false),
                    ControlledSubstanceLicenseFacilityKey = table.Column<Guid>(nullable: true),
                    ControlledSubstanceLicenseKey = table.Column<Guid>(nullable: true),
                    FacilityEntityFacilityKey = table.Column<Guid>(nullable: true),
                    Href = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true)
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
        }
    }
}
