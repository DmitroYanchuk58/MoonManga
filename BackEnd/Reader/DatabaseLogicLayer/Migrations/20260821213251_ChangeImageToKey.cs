using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseLogicLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImageToKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Page_Image_NotEmpty",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Pages");

            migrationBuilder.AddColumn<string>(
                name: "StorageKey",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageKey",
                table: "Pages");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Pages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Page_Image_NotEmpty",
                table: "Pages",
                sql: "Image IS NOT NULL AND DATALENGTH(Image) > 0");
        }
    }
}
