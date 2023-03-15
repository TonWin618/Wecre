using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class addDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ModelVersions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FirmwareVerisions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ModelVersions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FirmwareVerisions");
        }
    }
}
