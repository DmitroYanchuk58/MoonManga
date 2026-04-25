using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddImageIntoReadItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImage",
                table: "ReadItems",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[] { 0 });

            migrationBuilder.AddCheckConstraint(
                name: "CK_ReadItem_CoverImage_NotEmpty",
                table: "ReadItems",
                sql: "CoverImage IS NOT NULL AND DATALENGTH(CoverImage) > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ReadItem_CoverImage_NotEmpty",
                table: "ReadItems");

            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "ReadItems");
        }
    }
}
