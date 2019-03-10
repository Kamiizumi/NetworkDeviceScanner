using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Migrations
{
    public partial class AddHostNameIpToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastSeenHostName",
                table: "Devices",
                maxLength: 63,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastSeenIp",
                table: "Devices",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSeenHostName",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "LastSeenIp",
                table: "Devices");
        }
    }
}
