using Microsoft.EntityFrameworkCore.Migrations;

namespace RideOnBulgaria.Data.Migrations
{
    public partial class add_rating_properties_to_road : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AveragePosterRating",
                table: "Roads",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "PleasureRating",
                table: "Roads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SurfaceRating",
                table: "Roads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewRating",
                table: "Roads",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AveragePosterRating",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "PleasureRating",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "SurfaceRating",
                table: "Roads");

            migrationBuilder.DropColumn(
                name: "ViewRating",
                table: "Roads");
        }
    }
}
