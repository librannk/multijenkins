using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Formulary.API.Migrations
{
    public partial class _20190509 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteFlag",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "EndUTCTime",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "LastModifiedUTCDateTime",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "StartUTCDateTime",
                table: "Item");

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "ItemKey",
                keyValue: new Guid("9b9134d6-81d3-4460-92ad-cd1790aa4c60"),
                columns: new[] { "CreatedDateTime", "LastModifiedDateTime" },
                values: new object[] { new DateTimeOffset(new DateTime(2019, 5, 9, 6, 30, 31, 138, DateTimeKind.Unspecified).AddTicks(8779), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2019, 5, 9, 6, 30, 31, 146, DateTimeKind.Unspecified).AddTicks(6432), new TimeSpan(0, 5, 30, 0, 0)) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeleteFlag",
                table: "Item",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndUTCTime",
                table: "Item",
                type: "DateTimeOffset(7)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModifiedUTCDateTime",
                table: "Item",
                type: "DateTimeOffset(7)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartUTCDateTime",
                table: "Item",
                type: "DateTimeOffset(7)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Item",
                keyColumn: "ItemKey",
                keyValue: new Guid("9b9134d6-81d3-4460-92ad-cd1790aa4c60"),
                columns: new[] { "CreatedDateTime", "LastModifiedDateTime", "LastModifiedUTCDateTime", "StartUTCDateTime" },
                values: new object[] { new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2019, 5, 8, 20, 36, 49, 280, DateTimeKind.Unspecified).AddTicks(434), new TimeSpan(0, 5, 30, 0, 0)), new DateTimeOffset(new DateTime(2019, 5, 8, 20, 36, 49, 272, DateTimeKind.Unspecified).AddTicks(5045), new TimeSpan(0, 5, 30, 0, 0)) });
        }
    }
}
