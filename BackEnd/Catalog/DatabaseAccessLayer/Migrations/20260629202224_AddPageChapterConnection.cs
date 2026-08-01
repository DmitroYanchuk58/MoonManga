using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPageChapterConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdChapter",
                table: "Pages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pages_IdChapter",
                table: "Pages",
                column: "IdChapter");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Chapters_IdChapter",
                table: "Pages",
                column: "IdChapter",
                principalTable: "Chapters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Chapters_IdChapter",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_IdChapter",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "IdChapter",
                table: "Pages");
        }
    }
}
