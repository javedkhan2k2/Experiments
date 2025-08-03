using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClampingDevice.Migrations
{
    /// <inheritdoc />
    public partial class ClampingDataUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ClampingsData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ClampingsData");
        }
    }
}
