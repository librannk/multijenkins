using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Facility.API.Migrations
{
    public partial class _20190428_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TenantKey",
                schema: "dbo",
                table: "Facilities",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "TenantKey",
                schema: "dbo",
                table: "Facilities",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
