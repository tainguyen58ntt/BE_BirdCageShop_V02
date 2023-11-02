using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class updatefordesign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0decf35d-b2b5-4458-b76a-9e14e5a3de2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "441ae6c0-332d-4f99-b7b2-470742f39dc0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4567cf1a-3748-4f0a-8d48-79229d40bc02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81292ec6-c258-462d-966a-868f39c35070");

            migrationBuilder.AddColumn<string>(
                name: "Bars",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Formulas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinWidth = table.Column<int>(type: "int", nullable: false),
                    MaxWidth = table.Column<int>(type: "int", nullable: false),
                    MinHeight = table.Column<int>(type: "int", nullable: false),
                    MaxHeight = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinBars = table.Column<int>(type: "int", nullable: false),
                    MaxBars = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BirdCageTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formulas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formulas_BirdCageTypes_BirdCageTypeId",
                        column: x => x.BirdCageTypeId,
                        principalTable: "BirdCageTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "731511d8-14c6-4216-bc0d-5a5cb682ffd8", "2", "Manager", "Manager" },
                    { "a7e83e6c-728e-4e3f-a9cf-6958f925414d", "2", "Customer", "Customer" },
                    { "d72e36f6-c00d-49d3-92d0-11e676117893", "3", "Staff", "Staff" },
                    { "f707d0ce-9ace-4a76-a642-aff9c6d79c67", "1", "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Formulas_BirdCageTypeId",
                table: "Formulas",
                column: "BirdCageTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Formulas");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "731511d8-14c6-4216-bc0d-5a5cb682ffd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a7e83e6c-728e-4e3f-a9cf-6958f925414d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d72e36f6-c00d-49d3-92d0-11e676117893");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f707d0ce-9ace-4a76-a642-aff9c6d79c67");

            migrationBuilder.DropColumn(
                name: "Bars",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "OrderDetail");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0decf35d-b2b5-4458-b76a-9e14e5a3de2d", "1", "Admin", "Admin" },
                    { "441ae6c0-332d-4f99-b7b2-470742f39dc0", "2", "Customer", "Customer" },
                    { "4567cf1a-3748-4f0a-8d48-79229d40bc02", "3", "Staff", "Staff" },
                    { "81292ec6-c258-462d-966a-868f39c35070", "2", "Manager", "Manager" }
                });
        }
    }
}
