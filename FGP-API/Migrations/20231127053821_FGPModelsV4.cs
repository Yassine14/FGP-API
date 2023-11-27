using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FGP_API.Migrations
{
    /// <inheritdoc />
    public partial class FGPModelsV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "FGPUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "FGPUsers");

            migrationBuilder.AlterColumn<string>(
                name: "TimeZone",
                table: "FGPUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TimeZone",
                table: "FGPUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FGPUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "FGPUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);
        }
    }
}
