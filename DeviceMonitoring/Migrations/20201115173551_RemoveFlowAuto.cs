using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonitoring.Migrations
{
    public partial class RemoveFlowAuto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlowAuto = table.Column<double>(type: "float", nullable: false),
                    On = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowSettings", x => x.Id);
                });
        }
    }
}
