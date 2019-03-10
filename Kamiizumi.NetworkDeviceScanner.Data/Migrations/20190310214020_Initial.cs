using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kamiizumi.NetworkDeviceScanner.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.UniqueConstraint("AK_Profiles_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    MacAddress = table.Column<string>(maxLength: 17, nullable: false),
                    UserDefinedName = table.Column<string>(maxLength: 255, nullable: true),
                    LastSeenIp = table.Column<string>(maxLength: 15, nullable: false),
                    LastSeenHostName = table.Column<string>(maxLength: 63, nullable: true),
                    LastSeenAt = table.Column<DateTimeOffset>(nullable: false),
                    ProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.MacAddress);
                    table.ForeignKey(
                        name: "FK_Devices_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ProfileId",
                table: "Devices",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
