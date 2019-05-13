using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Formulary.API.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedItem_DispenseForm_DispenseFormLookupKey",
                table: "MedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MedItem_DispenseUnit_DispenseUnitLookupKey",
                table: "MedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MedItem_GeneralLedgerAccount_GLAccountKey",
                table: "MedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductID_MedItem_MedicationItemKey",
                table: "ProductID");

            migrationBuilder.DropIndex(
                name: "IX_ProductID_MedicationItemKey",
                table: "ProductID");

            migrationBuilder.DropColumn(
                name: "AHFSClassName",
                table: "ProductID");

            migrationBuilder.DropColumn(
                name: "DeleteFlag",
                table: "ProductID");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "ProductID");

            migrationBuilder.DropColumn(
                name: "StarDateTime",
                table: "ProductID");

            migrationBuilder.DropColumn(
                name: "TenantKey",
                table: "ProductID");

            migrationBuilder.RenameTable(
                name: "ProductID",
                newName: "ProductID",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "MedicationItemKey",
                schema: "dbo",
                table: "ProductID",
                newName: "MedClassKey");

            migrationBuilder.RenameColumn(
                name: "AlternateProductID",
                schema: "dbo",
                table: "ProductID",
                newName: "AltCode");

            migrationBuilder.RenameColumn(
                name: "ProductIdentificationKey",
                schema: "dbo",
                table: "ProductID",
                newName: "ProductIDKey");

            migrationBuilder.AlterColumn<Guid>(
                name: "TotalVolumeUnitOfMeasureKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "TotalVolumeExternalUnitOfMeasureKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "StrengthUnitOfMeasureKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "StrengthExternalUnitOfMeasureKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicationClassKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "GLAccountKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "DispenseUnitLookupKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "DispenseFormLookupKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ConcentrationVolumeUnitOfMeasureKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ConcentrationVolumeExternalUnitOfMeasureKey",
                table: "MedItem",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "ItemID",
                table: "Item",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<Guid>(
                name: "VerifiedByUserAccountKey",
                schema: "dbo",
                table: "ProductID",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkedByUserAccountKey",
                schema: "dbo",
                table: "ProductID",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ManufacturerID",
                schema: "dbo",
                table: "ProductID",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PreferredOrderingEntity",
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
                    table.PrimaryKey("PK_PreferredOrderingEntity", x => x.PreferredOrderingKey);
                    table.ForeignKey(
                        name: "FK_PreferredOrderingEntity_Item_ItemKey",
                        column: x => x.ItemKey,
                        principalTable: "Item",
                        principalColumn: "ItemKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreferredOrderingEntity_ItemKey",
                table: "PreferredOrderingEntity",
                column: "ItemKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedItem_DispenseForm_DispenseFormLookupKey",
                table: "MedItem",
                column: "DispenseFormLookupKey",
                principalSchema: "dbo",
                principalTable: "DispenseForm",
                principalColumn: "DispenseFormLookupKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedItem_DispenseUnit_DispenseUnitLookupKey",
                table: "MedItem",
                column: "DispenseUnitLookupKey",
                principalSchema: "dbo",
                principalTable: "DispenseUnit",
                principalColumn: "DispenseUnitLookupKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedItem_GeneralLedgerAccount_GLAccountKey",
                table: "MedItem",
                column: "GLAccountKey",
                principalSchema: "dbo",
                principalTable: "GeneralLedgerAccount",
                principalColumn: "GLAccountKey",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductID_MedItem_ItemKey",
                schema: "dbo",
                table: "ProductID",
                column: "ItemKey",
                principalTable: "MedItem",
                principalColumn: "MedicationItemKey",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedItem_DispenseForm_DispenseFormLookupKey",
                table: "MedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MedItem_DispenseUnit_DispenseUnitLookupKey",
                table: "MedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MedItem_GeneralLedgerAccount_GLAccountKey",
                table: "MedItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductID_MedItem_ItemKey",
                schema: "dbo",
                table: "ProductID");

            migrationBuilder.DropTable(
                name: "PreferredOrderingEntity");

            migrationBuilder.DropColumn(
                name: "ManufacturerID",
                schema: "dbo",
                table: "ProductID");

            migrationBuilder.RenameTable(
                name: "ProductID",
                schema: "dbo",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "MedClassKey",
                table: "ProductID",
                newName: "MedicationItemKey");

            migrationBuilder.RenameColumn(
                name: "AltCode",
                table: "ProductID",
                newName: "AlternateProductID");

            migrationBuilder.RenameColumn(
                name: "ProductIDKey",
                table: "ProductID",
                newName: "ProductIdentificationKey");

            migrationBuilder.AlterColumn<Guid>(
                name: "TotalVolumeUnitOfMeasureKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TotalVolumeExternalUnitOfMeasureKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StrengthUnitOfMeasureKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StrengthExternalUnitOfMeasureKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MedicationClassKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "GLAccountKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DispenseUnitLookupKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DispenseFormLookupKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConcentrationVolumeUnitOfMeasureKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConcentrationVolumeExternalUnitOfMeasureKey",
                table: "MedItem",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemID",
                table: "Item",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "VerifiedByUserAccountKey",
                table: "ProductID",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LinkedByUserAccountKey",
                table: "ProductID",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AHFSClassName",
                table: "ProductID",
                type: "nvarchar(15)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "DeleteFlag",
                table: "ProductID",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndDateTime",
                table: "ProductID",
                type: "DateTimeOffset(7)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StarDateTime",
                table: "ProductID",
                type: "DateTimeOffset(7)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKey",
                table: "ProductID",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductID_MedicationItemKey",
                table: "ProductID",
                column: "MedicationItemKey");

            migrationBuilder.AddForeignKey(
                name: "FK_MedItem_DispenseForm_DispenseFormLookupKey",
                table: "MedItem",
                column: "DispenseFormLookupKey",
                principalSchema: "dbo",
                principalTable: "DispenseForm",
                principalColumn: "DispenseFormLookupKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedItem_DispenseUnit_DispenseUnitLookupKey",
                table: "MedItem",
                column: "DispenseUnitLookupKey",
                principalSchema: "dbo",
                principalTable: "DispenseUnit",
                principalColumn: "DispenseUnitLookupKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedItem_GeneralLedgerAccount_GLAccountKey",
                table: "MedItem",
                column: "GLAccountKey",
                principalSchema: "dbo",
                principalTable: "GeneralLedgerAccount",
                principalColumn: "GLAccountKey",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductID_MedItem_MedicationItemKey",
                table: "ProductID",
                column: "MedicationItemKey",
                principalTable: "MedItem",
                principalColumn: "MedicationItemKey",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
