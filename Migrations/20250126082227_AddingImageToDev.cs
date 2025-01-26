using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutsourcingSystemWepApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingImageToDev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "Developer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "Developer");
        }
    }
}
