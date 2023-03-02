using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    ReadmeUrl = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FirmwareVerisions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Files = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Downloads = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmwareVerisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmwareVerisions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ModelVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Files = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Downloads = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelVersions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TotalDownloads = table.Column<long>(type: "bigint", nullable: false),
                    FirmwareVersionsId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModelVersionsId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVersions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectVersions_FirmwareVerisions_FirmwareVersionsId",
                        column: x => x.FirmwareVersionsId,
                        principalTable: "FirmwareVerisions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectVersions_ModelVersions_ModelVersionsId",
                        column: x => x.ModelVersionsId,
                        principalTable: "ModelVersions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectVersions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FirmwareVerisions_ProjectId",
                table: "FirmwareVerisions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelVersions_ProjectId",
                table: "ModelVersions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserName_Name",
                table: "Projects",
                columns: new[] { "UserName", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVersions_FirmwareVersionsId",
                table: "ProjectVersions",
                column: "FirmwareVersionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVersions_ModelVersionsId",
                table: "ProjectVersions",
                column: "ModelVersionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectVersions_ProjectId",
                table: "ProjectVersions",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectVersions");

            migrationBuilder.DropTable(
                name: "FirmwareVerisions");

            migrationBuilder.DropTable(
                name: "ModelVersions");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
