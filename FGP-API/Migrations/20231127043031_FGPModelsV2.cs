using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGP_API.Migrations
{
    /// <inheritdoc />
    public partial class FGPModelsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserApplicationId",
                table: "FGPUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AspNetUsersId",
                table: "FGPUsers",
                type: "nvarchar(450)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserApplicationId",
                table: "FGPUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
