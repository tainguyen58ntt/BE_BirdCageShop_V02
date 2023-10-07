using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdCageShopDbContext.Migrations
{
    public partial class addcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<bool>(
  name: "IsDelete",
  table: "Products",
  type: "bit",
  nullable: false,
  defaultValue: false);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
