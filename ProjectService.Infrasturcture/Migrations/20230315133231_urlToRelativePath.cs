using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class urlToRelativePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ProjectFiles",
                newName: "RelativePath");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ModelVersions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FirmwareVerisions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelativePath",
                table: "ProjectFiles",
                newName: "Url");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ModelVersions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FirmwareVerisions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
