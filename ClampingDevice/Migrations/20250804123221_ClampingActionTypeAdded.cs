using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClampingDevice.Migrations
{
    /// <inheritdoc />
    public partial class ClampingActionTypeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionType",
                table: "ClampingsData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "ClampingsData");
        }
    }
}
