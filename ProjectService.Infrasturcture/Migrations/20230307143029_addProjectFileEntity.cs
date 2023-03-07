using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class addProjectFileEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadmeFiles",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "ModelVersions");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "FirmwareVerisions");

            migrationBuilder.CreateTable(
                name: "ProjectFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FirmwareVersionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModelVersionId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectFile_FirmwareVerisions_FirmwareVersionId",
                        column: x => x.FirmwareVersionId,
                        principalTable: "FirmwareVerisions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectFile_ModelVersions_ModelVersionId",
                        column: x => x.ModelVersionId,
                        principalTable: "ModelVersions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectFile_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_FirmwareVersionId",
                table: "ProjectFile",
                column: "FirmwareVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_ModelVersionId",
                table: "ProjectFile",
                column: "ModelVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFile_ProjectId",
                table: "ProjectFile",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectFile");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "ReadmeFiles",
                table: "Projects",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Files",
                table: "ModelVersions",
                type: "uuid[]",
                nullable: false);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "Files",
                table: "FirmwareVerisions",
                type: "uuid[]",
                nullable: false);
        }
    }
}
