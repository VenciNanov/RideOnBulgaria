using Microsoft.EntityFrameworkCore.Migrations;

namespace RideOnBulgaria.Data.Migrations
{
    public partial class add_publicId_to_ProductImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "ProductImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "ProductImages");
        }
    }
}
