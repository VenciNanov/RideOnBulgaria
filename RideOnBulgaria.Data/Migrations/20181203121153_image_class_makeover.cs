using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RideOnBulgaria.Data.Migrations
{
    public partial class image_class_makeover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "TripName",
                table: "Roads",
                newName: "RoadName");

            migrationBuilder.RenameColumn(
                name: "TripLength",
                table: "Roads",
                newName: "RoadLength");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Images",
                newName: "PublicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoadName",
                table: "Roads",
                newName: "TripName");

            migrationBuilder.RenameColumn(
                name: "RoadLength",
                table: "Roads",
                newName: "TripLength");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Images",
                newName: "Description");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Images",
                nullable: true);
        }
    }
}
