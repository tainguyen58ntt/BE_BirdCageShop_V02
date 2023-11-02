using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class addfieldshoppingcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<decimal>(
                name: "PriceDesign",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PriceDesign",
                table: "ShoppingCarts");

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
        }
    }
}
