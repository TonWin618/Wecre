using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class mangToMang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFiles_FirmwareVerisions_FirmwareVersionId",
                table: "ProjectFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFiles_ModelVersions_ModelVersionId",
                table: "ProjectFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFiles_Projects_ProjectId",
                table: "ProjectFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFiles_FirmwareVersionId",
                table: "ProjectFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFiles_ModelVersionId",
                table: "ProjectFiles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectFiles_ProjectId",
                table: "ProjectFiles");

            migrationBuilder.DropColumn(
                name: "FirmwareVersionId",
                table: "ProjectFiles");

            migrationBuilder.DropColumn(
                name: "ModelVersionId",
                table: "ProjectFiles");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectFiles");

            migrationBuilder.CreateTable(
                name: "FirmwareVersionProjectFile",
                columns: table => new
                {
                    FilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirmwareVersionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmwareVersionProjectFile", x => new { x.FilesId, x.FirmwareVersionId });
                    table.ForeignKey(
                        name: "FK_FirmwareVersionProjectFile_FirmwareVerisions_FirmwareVersio~",
                        column: x => x.FirmwareVersionId,
                        principalTable: "FirmwareVerisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirmwareVersionProjectFile_ProjectFiles_FilesId",
                        column: x => x.FilesId,
                        principalTable: "ProjectFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelVersionProjectFile",
                columns: table => new
                {
                    FilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModelVersionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelVersionProjectFile", x => new { x.FilesId, x.ModelVersionId });
                    table.ForeignKey(
                        name: "FK_ModelVersionProjectFile_ModelVersions_ModelVersionId",
                        column: x => x.ModelVersionId,
                        principalTable: "ModelVersions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModelVersionProjectFile_ProjectFiles_FilesId",
                        column: x => x.FilesId,
                        principalTable: "ProjectFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProjectFile",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReadmeFilesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProjectFile", x => new { x.ProjectId, x.ReadmeFilesId });
                    table.ForeignKey(
                        name: "FK_ProjectProjectFile_ProjectFiles_ReadmeFilesId",
                        column: x => x.ReadmeFilesId,
                        principalTable: "ProjectFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProjectFile_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirmwareVersionProjectFile_FirmwareVersionId",
                table: "FirmwareVersionProjectFile",
                column: "FirmwareVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelVersionProjectFile_ModelVersionId",
                table: "ModelVersionProjectFile",
                column: "ModelVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProjectFile_ReadmeFilesId",
                table: "ProjectProjectFile",
                column: "ReadmeFilesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirmwareVersionProjectFile");

            migrationBuilder.DropTable(
                name: "ModelVersionProjectFile");

            migrationBuilder.DropTable(
                name: "ProjectProjectFile");

            migrationBuilder.AddColumn<Guid>(
                name: "FirmwareVersionId",
                table: "ProjectFiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModelVersionId",
                table: "ProjectFiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "ProjectFiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_FirmwareVersionId",
                table: "ProjectFiles",
                column: "FirmwareVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ModelVersionId",
                table: "ProjectFiles",
                column: "ModelVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFiles_ProjectId",
                table: "ProjectFiles",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFiles_FirmwareVerisions_FirmwareVersionId",
                table: "ProjectFiles",
                column: "FirmwareVersionId",
                principalTable: "FirmwareVerisions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFiles_ModelVersions_ModelVersionId",
                table: "ProjectFiles",
                column: "ModelVersionId",
                principalTable: "ModelVersions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFiles_Projects_ProjectId",
                table: "ProjectFiles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
