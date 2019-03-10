using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetworkDeviceScanner.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscoveredDevices",
                columns: table => new
                {
                    MacAddress = table.Column<string>(maxLength: 12, nullable: false),
                    CustomName = table.Column<string>(maxLength: 255, nullable: true),
                    LastSeen = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscoveredDevices", x => x.MacAddress);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscoveredDevices");
        }
    }
}
