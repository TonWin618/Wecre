using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    /// <inheritdoc />
    public partial class setProjectFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_FirmwareVerisions_FirmwareVersionId",
                table: "ProjectFile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_ModelVersions_ModelVersionId",
                table: "ProjectFile");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectFile_Projects_ProjectId",
                table: "ProjectFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectFile",
                table: "ProjectFile");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ProjectFile");

            migrationBuilder.RenameTable(
                name: "ProjectFile",
                newName: "ProjectFiles");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFile_ProjectId",
                table: "ProjectFiles",
                newName: "IX_ProjectFiles_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFile_ModelVersionId",
                table: "ProjectFiles",
                newName: "IX_ProjectFiles_ModelVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFile_FirmwareVersionId",
                table: "ProjectFiles",
                newName: "IX_ProjectFiles_FirmwareVersionId");

            migrationBuilder.AddColumn<long>(
                name: "Downloads",
                table: "ProjectFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SizeInBytes",
                table: "ProjectFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ProjectFiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectFiles",
                table: "ProjectFiles",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectFiles",
                table: "ProjectFiles");

            migrationBuilder.DropColumn(
                name: "Downloads",
                table: "ProjectFiles");

            migrationBuilder.DropColumn(
                name: "SizeInBytes",
                table: "ProjectFiles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ProjectFiles");

            migrationBuilder.RenameTable(
                name: "ProjectFiles",
                newName: "ProjectFile");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFiles_ProjectId",
                table: "ProjectFile",
                newName: "IX_ProjectFile_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFiles_ModelVersionId",
                table: "ProjectFile",
                newName: "IX_ProjectFile_ModelVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectFiles_FirmwareVersionId",
                table: "ProjectFile",
                newName: "IX_ProjectFile_FirmwareVersionId");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                table: "ProjectFile",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectFile",
                table: "ProjectFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_FirmwareVerisions_FirmwareVersionId",
                table: "ProjectFile",
                column: "FirmwareVersionId",
                principalTable: "FirmwareVerisions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_ModelVersions_ModelVersionId",
                table: "ProjectFile",
                column: "ModelVersionId",
                principalTable: "ModelVersions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectFile_Projects_ProjectId",
                table: "ProjectFile",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
