using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonitoring.Migrations
{
    public partial class FlowOnOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Restart",
                table: "DeviceSettings");

            migrationBuilder.AddColumn<bool>(
                name: "On",
                table: "FlowSettings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "On",
                table: "FlowSettings");

            migrationBuilder.AddColumn<bool>(
                name: "Restart",
                table: "DeviceSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
