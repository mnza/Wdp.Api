using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wdp.Api.Migrations
{
    public partial class addconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "fav_websites",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "fav_websites",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "fav_websites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "fav_websites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "fav_websites",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "fav_websites");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "fav_websites");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "fav_websites");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "fav_websites");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "fav_websites");
        }
    }
}
