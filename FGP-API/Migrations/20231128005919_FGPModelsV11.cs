using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGP_API.Migrations
{
    /// <inheritdoc />
    public partial class FGPModelsV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NonAppearances",
                table: "NonAppearances");

            migrationBuilder.DropIndex(
                name: "IX_NonAppearances_GameId",
                table: "NonAppearances");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "NonAppearances");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "NonAppearances");

            migrationBuilder.AddColumn<string>(
                name: "Motif",
                table: "NonAppearances",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NonAppearances",
                table: "NonAppearances",
                columns: new[] { "GameId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NonAppearances",
                table: "NonAppearances");

            migrationBuilder.DropColumn(
                name: "Motif",
                table: "NonAppearances");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "NonAppearances",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "NonAppearances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_NonAppearances",
                table: "NonAppearances",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NonAppearances_GameId",
                table: "NonAppearances",
                column: "GameId");
        }
    }
}
