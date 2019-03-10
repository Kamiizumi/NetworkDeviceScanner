using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Migrations
{
    public partial class AddDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    MacAddress = table.Column<string>(maxLength: 17, nullable: false),
                    LastSeen = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.MacAddress);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
