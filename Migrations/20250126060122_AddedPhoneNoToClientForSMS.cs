using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutsourcingSystemWepApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhoneNoToClientForSMS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Client",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Client");
        }
    }
}
