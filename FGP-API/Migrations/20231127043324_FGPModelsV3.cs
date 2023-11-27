using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGP_API.Migrations
{
    /// <inheritdoc />
    public partial class FGPModelsV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FGPUsers_AspNetUsers_AspNetUsersId",
                table: "FGPUsers");

            migrationBuilder.DropIndex(
                name: "IX_FGPUsers_AspNetUsersId",
                table: "FGPUsers");

            migrationBuilder.DropColumn(
                name: "AspNetUsersId",
                table: "FGPUsers");

            migrationBuilder.DropColumn(
                name: "UserApplicationId",
                table: "FGPUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "FGPUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FGPUsers_ApplicationUserId",
                table: "FGPUsers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FGPUsers_AspNetUsers_ApplicationUserId",
                table: "FGPUsers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FGPUsers_AspNetUsers_ApplicationUserId",
                table: "FGPUsers");

            migrationBuilder.DropIndex(
                name: "IX_FGPUsers_ApplicationUserId",
                table: "FGPUsers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "FGPUsers");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUsersId",
                table: "FGPUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserApplicationId",
                table: "FGPUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FGPUsers_AspNetUsersId",
                table: "FGPUsers",
                column: "AspNetUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_FGPUsers_AspNetUsers_AspNetUsersId",
                table: "FGPUsers",
                column: "AspNetUsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
