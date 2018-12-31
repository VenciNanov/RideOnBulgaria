using Microsoft.EntityFrameworkCore.Migrations;

namespace RideOnBulgaria.Data.Migrations
{
    public partial class road_delete_fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Roads_RoadId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Roads_RoadId",
                table: "Images",
                column: "RoadId",
                principalTable: "Roads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Roads_RoadId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Roads_RoadId",
                table: "Images",
                column: "RoadId",
                principalTable: "Roads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
