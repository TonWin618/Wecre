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
                    FileIdentifier_UserName = table.Column<string>(type: "text", nullable: false),
                    FileIdentifier_ProjectName = table.Column<string>(type: "text", nullable: false),
                    FileIdentifier_FileType = table.Column<string>(type: "text", nullable: false),
                    FileIdentifier_VersionName = table.Column<string>(type: "text", nullable: false),
                    FileIdentifier_FileName = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FileSHA256Hash = table.Column<string>(type: "text", nullable: false),
                    BackupUrl = table.Column<string>(type: "text", nullable: false),
                    RemoteUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileItems");
        }
    }
}
