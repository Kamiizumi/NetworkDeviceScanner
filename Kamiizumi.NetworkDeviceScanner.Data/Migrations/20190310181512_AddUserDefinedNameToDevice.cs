using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Migrations
{
    public partial class AddUserDefinedNameToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserDefinedName",
                table: "Devices",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDefinedName",
                table: "Devices");
        }
    }
}
