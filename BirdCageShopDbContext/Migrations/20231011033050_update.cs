using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BirdCageTypes_BirdCageTypeId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "BirdCageTypeId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BirdCageTypes_BirdCageTypeId",
                table: "Products",
                column: "BirdCageTypeId",
                principalTable: "BirdCageTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_BirdCageTypes_BirdCageTypeId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "BirdCageTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_BirdCageTypes_BirdCageTypeId",
                table: "Products",
                column: "BirdCageTypeId",
                principalTable: "BirdCageTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
