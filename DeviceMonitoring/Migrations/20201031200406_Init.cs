using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceMonitoring.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceData",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDt = table.Column<DateTime>(nullable: false),
                    UpdatedDt = table.Column<DateTime>(nullable: false),
                    DeviceId = table.Column<string>(nullable: true),
                    Flowpast = table.Column<double>(nullable: false),
                    Flowsarqac = table.Column<double>(nullable: false),
                    Flowhanac = table.Column<double>(nullable: false),
                    Flowmax = table.Column<double>(nullable: false),
                    Flowproc = table.Column<double>(nullable: false),
                    Dppastaci = table.Column<double>(nullable: false),
                    Dpdrac = table.Column<double>(nullable: false),
                    Dpgorcakic = table.Column<double>(nullable: false),
                    Kgorcakic = table.Column<double>(nullable: false),
                    Onoff = table.Column<double>(nullable: false),
                    Selfonoff = table.Column<double>(nullable: false),
                    Presspastaci = table.Column<double>(nullable: false),
                    Pressgorcakic = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDt = table.Column<DateTime>(nullable: false),
                    UpdatedDt = table.Column<DateTime>(nullable: false),
                    DeviceId = table.Column<string>(nullable: false),
                    Flowhanac = table.Column<double>(nullable: false),
                    Flowmax = table.Column<double>(nullable: false),
                    Flowproc = table.Column<double>(nullable: false),
                    Dpgorcakic = table.Column<double>(nullable: false),
                    Kgorcakic = table.Column<double>(nullable: false),
                    Pressgorcakic = table.Column<double>(nullable: false),
                    Restart = table.Column<bool>(nullable: false),
                    Onoff = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDt = table.Column<DateTime>(nullable: false),
                    UpdatedDt = table.Column<DateTime>(nullable: false),
                    FlowAuto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceData");

            migrationBuilder.DropTable(
                name: "DeviceSettings");

            migrationBuilder.DropTable(
                name: "FlowSettings");
        }
    }
}
