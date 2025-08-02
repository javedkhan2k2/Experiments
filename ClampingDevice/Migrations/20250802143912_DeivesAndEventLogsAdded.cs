using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClampingDevice.Migrations
{
    /// <inheritdoc />
    public partial class DeivesAndEventLogsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "ClampingsData",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "ClampingsData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WarningMessage",
                table: "ClampingsData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventType = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClampingsData_DeviceId",
                table: "ClampingsData",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClampingsData_Devices_DeviceId",
                table: "ClampingsData",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClampingsData_Devices_DeviceId",
                table: "ClampingsData");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropIndex(
                name: "IX_ClampingsData_DeviceId",
                table: "ClampingsData");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "ClampingsData");

            migrationBuilder.DropColumn(
                name: "WarningMessage",
                table: "ClampingsData");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "ClampingsData",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
