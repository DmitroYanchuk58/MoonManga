using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddRelaptionshipReadItemChapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdReadItem",
                table: "Chapters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_IdReadItem",
                table: "Chapters",
                column: "IdReadItem");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_ReadItems_IdReadItem",
                table: "Chapters",
                column: "IdReadItem",
                principalTable: "ReadItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_ReadItems_IdReadItem",
                table: "Chapters");

            migrationBuilder.DropIndex(
                name: "IX_Chapters_IdReadItem",
                table: "Chapters");

            migrationBuilder.DropColumn(
                name: "IdReadItem",
                table: "Chapters");
        }
    }
}
