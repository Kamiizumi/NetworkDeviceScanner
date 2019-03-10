using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Migrations
{
    public partial class RenameLastSeenToLastSeenAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastSeen",
                table: "Devices",
                newName: "LastSeenAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastSeenAt",
                table: "Devices",
                newName: "LastSeen");
        }
    }
}
