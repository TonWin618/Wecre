using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class redesignFileService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileItems_FileSHA256Hash_FileSizeInBytes",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "FileItems");

            migrationBuilder.DropColumn(
                name: "FileSHA256Hash",
                table: "FileItems");

            migrationBuilder.AddColumn<string>(
                name: "RelativePath",
                table: "FileItems",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelativePath",
                table: "FileItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "FileItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FileSHA256Hash",
                table: "FileItems",
                type: "character varying(64)",
                unicode: false,
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_FileSHA256Hash_FileSizeInBytes",
                table: "FileItems",
                columns: new[] { "FileSHA256Hash", "FileSizeInBytes" });
        }
    }
}
