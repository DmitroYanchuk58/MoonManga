using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckDataRulesForReadItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_ReadItem_Title_NotEmpty",
                table: "ReadItems",
                sql: "LEN(TRIM(Title)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ReadItem_Type_NotEmpty",
                table: "ReadItems",
                sql: "LEN(TRIM(Type)) > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ReadItem_Title_NotEmpty",
                table: "ReadItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ReadItem_Type_NotEmpty",
                table: "ReadItems");
        }
    }
}
