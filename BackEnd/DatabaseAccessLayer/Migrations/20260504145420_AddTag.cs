using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.CheckConstraint("CK_Tag_Name_NotEmpty", "LEN(TRIM(Name)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "ReadItemTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdReadItem = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTag = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadItemTags", x => new { x.Id, x.IdReadItem, x.IdTag });
                    table.ForeignKey(
                        name: "FK_ReadItemTags_ReadItems_IdReadItem",
                        column: x => x.IdReadItem,
                        principalTable: "ReadItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReadItemTags_Tags_IdTag",
                        column: x => x.IdTag,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReadItemTags_IdReadItem",
                table: "ReadItemTags",
                column: "IdReadItem");

            migrationBuilder.CreateIndex(
                name: "IX_ReadItemTags_IdTag",
                table: "ReadItemTags",
                column: "IdTag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadItemTags");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
