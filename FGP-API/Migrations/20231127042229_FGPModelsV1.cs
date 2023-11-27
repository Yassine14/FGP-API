using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGP_API.Migrations
{
    /// <inheritdoc />
    public partial class FGPModelsV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VenueId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Games_VenueId",
                table: "Games",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Venues_VenueId",
                table: "Games",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Venues_VenueId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_VenueId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "VenueId",
                table: "Games");
        }
    }
}
