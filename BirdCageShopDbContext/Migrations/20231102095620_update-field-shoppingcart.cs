using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class updatefieldshoppingcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2076edc9-0072-4aa0-bc11-aa3fae84cdd5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22ff69e7-251b-4024-af51-7cb5c8ee0286");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "390ee178-e97c-46cd-a6a3-1906eb7cf268");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1dce125-cfa1-4be8-a247-26e32383f720");

            migrationBuilder.AddColumn<int>(
                name: "Bars",
                table: "ShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "ShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "ShoppingCarts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Bars",
                table: "OrderDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18339d0b-2484-40e6-a54d-60153daeb871", "1", "Admin", "Admin" },
                    { "563d3d47-bd94-4d41-b30c-e853e75ebc87", "2", "Manager", "Manager" },
                    { "5baf032d-2037-4809-9661-6e3d4e5bb7a6", "2", "Customer", "Customer" },
                    { "efe29813-25e4-4d87-8130-780f07dcb9c4", "3", "Staff", "Staff" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18339d0b-2484-40e6-a54d-60153daeb871");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "563d3d47-bd94-4d41-b30c-e853e75ebc87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5baf032d-2037-4809-9661-6e3d4e5bb7a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efe29813-25e4-4d87-8130-780f07dcb9c4");

            migrationBuilder.DropColumn(
                name: "Bars",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "ShoppingCarts");

            migrationBuilder.AlterColumn<string>(
                name: "Bars",
                table: "OrderDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2076edc9-0072-4aa0-bc11-aa3fae84cdd5", "3", "Staff", "Staff" },
                    { "22ff69e7-251b-4024-af51-7cb5c8ee0286", "1", "Admin", "Admin" },
                    { "390ee178-e97c-46cd-a6a3-1906eb7cf268", "2", "Manager", "Manager" },
                    { "b1dce125-cfa1-4be8-a247-26e32383f720", "2", "Customer", "Customer" }
                });
        }
    }
}
