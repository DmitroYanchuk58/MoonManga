using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DeleteChapterRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Chapter_Order_Min",
                table: "Chapters");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Chapter_Volume_Min",
                table: "Chapters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Chapter_Order_Min",
                table: "Chapters",
                sql: "[Order] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Chapter_Volume_Min",
                table: "Chapters",
                sql: "[Volume] > 0");
        }
    }
}
