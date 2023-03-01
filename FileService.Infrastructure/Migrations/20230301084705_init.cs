using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(32)", unicode: false, maxLength: 32, nullable: false),
                    ProjectName = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    FileType = table.Column<string>(type: "character varying(64)", unicode: false, maxLength: 64, nullable: false),
                    VersionName = table.Column<string>(type: "character varying(256)", unicode: false, maxLength: 256, nullable: false),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileSHA256Hash = table.Column<string>(type: "character varying(64)", unicode: false, maxLength: 64, nullable: false),
                    BackupUrl = table.Column<string>(type: "text", nullable: false),
                    RemoteUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileItems", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileItems_FileSHA256Hash_FileSizeInBytes",
                table: "FileItems",
                columns: new[] { "FileSHA256Hash", "FileSizeInBytes" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileItems");
        }
    }
}
