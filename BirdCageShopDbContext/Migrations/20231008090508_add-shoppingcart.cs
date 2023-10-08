using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class addshoppingcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpDate",
                table: "ShoppingCarts",
                newName: "ModifiedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedAt",
                table: "ShoppingCarts",
                newName: "ExpDate");
        }
    }
}
