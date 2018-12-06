using Microsoft.EntityFrameworkCore.Migrations;

namespace RideOnBulgaria.Data.Migrations
{
    public partial class add_CoverPhoto_to_RoadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverPhotoId",
                table: "Roads",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoverPhotoRoads",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageId = table.Column<string>(nullable: true),
                    RoadId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverPhotoRoads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoverPhotoRoads_Roads_Id",
                        column: x => x.Id,
                        principalTable: "Roads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoverPhotoRoads_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoverPhotoRoads_ImageId",
                table: "CoverPhotoRoads",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoverPhotoRoads");

            migrationBuilder.DropColumn(
                name: "CoverPhotoId",
                table: "Roads");
        }
    }
}
