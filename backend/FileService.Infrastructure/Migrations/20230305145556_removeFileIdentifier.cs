using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeFileIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "VersionName",
                table: "FileItems");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "FileItems",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "FileItems",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "FileItems",
                type: "character varying(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "FileItems",
                type: "character varying(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "FileItems",
                type: "character varying(32)",
                unicode: false,
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VersionName",
                table: "FileItems",
                type: "character varying(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
