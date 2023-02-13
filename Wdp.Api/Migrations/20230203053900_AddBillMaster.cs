using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wdp.Api.Migrations
{
    public partial class AddBillMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_bill_detail",
                table: "bill_detail");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "bill_detail");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "bill_detail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "bill_date_time",
                table: "bill_detail",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<Guid>(
                name: "detail_id",
                table: "bill_detail",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "BillMasterBillId",
                table: "bill_detail",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bill_detail",
                table: "bill_detail",
                column: "detail_id");

            migrationBuilder.CreateTable(
                name: "bill_master",
                columns: table => new
                {
                    bill_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    bill_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bill_date_time = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    bill_money = table.Column<double>(type: "double", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bill_master", x => x.bill_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_bill_detail_BillMasterBillId",
                table: "bill_detail",
                column: "BillMasterBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_bill_detail_bill_master_BillMasterBillId",
                table: "bill_detail",
                column: "BillMasterBillId",
                principalTable: "bill_master",
                principalColumn: "bill_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bill_detail_bill_master_BillMasterBillId",
                table: "bill_detail");

            migrationBuilder.DropTable(
                name: "bill_master");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bill_detail",
                table: "bill_detail");

            migrationBuilder.DropIndex(
                name: "IX_bill_detail_BillMasterBillId",
                table: "bill_detail");

            migrationBuilder.DropColumn(
                name: "detail_id",
                table: "bill_detail");

            migrationBuilder.DropColumn(
                name: "BillMasterBillId",
                table: "bill_detail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "bill_date_time",
                table: "bill_detail",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "bill_detail",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "bill_detail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_bill_detail",
                table: "bill_detail",
                column: "Id");
        }
    }
}
