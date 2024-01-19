using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.CreateTable(
                name: "AbbMeasurements",
                columns: table => new
                {
                    Source = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    VoltageL1_V = table.Column<double>(type: "double precision", nullable: false),
                    VoltageL2_V = table.Column<double>(type: "double precision", nullable: false),
                    VoltageL3_V = table.Column<double>(type: "double precision", nullable: false),
                    CurrentL1_A = table.Column<double>(type: "double precision", nullable: false),
                    CurrentL2_A = table.Column<double>(type: "double precision", nullable: false),
                    CurrentL3_A = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerL1_W = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerL2_W = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerL3_W = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerL1_VAR = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerL2_VAR = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerL3_VAR = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerImportL1_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerImportL2_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerImportL3_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerExportL1_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerExportL2_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerExportL3_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerImportL1_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerImportL2_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerImportL3_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerExportL1_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerExportL2_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerExportL3_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyImportTotal_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyExportTotal_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ReactiveEnergyImportTotal_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactiveEnergyExportTotal_VARh = table.Column<double>(type: "double precision", nullable: false),
                    Milliseconds = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbbMeasurements", x => new { x.Source, x.Timestamp });
                });

            migrationBuilder.CreateTable(
                name: "SchneiderMeasurements",
                columns: table => new
                {
                    Source = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    VoltageL1_V = table.Column<double>(type: "double precision", nullable: false),
                    VoltageL2_V = table.Column<double>(type: "double precision", nullable: false),
                    VoltageL3_V = table.Column<double>(type: "double precision", nullable: false),
                    CurrentL1_A = table.Column<double>(type: "double precision", nullable: false),
                    CurrentL2_A = table.Column<double>(type: "double precision", nullable: false),
                    CurrentL3_A = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerL1_W = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerL2_W = table.Column<double>(type: "double precision", nullable: false),
                    ActivePowerL3_W = table.Column<double>(type: "double precision", nullable: false),
                    ReactivePowerTotal_VAR = table.Column<double>(type: "double precision", nullable: false),
                    ApparentPowerTotal_VA = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyImportL1_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyImportL2_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyImportL3_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyImportTotal_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ActiveEnergyExportTotal_Wh = table.Column<double>(type: "double precision", nullable: false),
                    ReactiveEnergyImportTotal_VARh = table.Column<double>(type: "double precision", nullable: false),
                    ReactiveEnergyExportTotal_VARh = table.Column<double>(type: "double precision", nullable: false),
                    Milliseconds = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchneiderMeasurements", x => new { x.Source, x.Timestamp });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbbMeasurements_Source_Milliseconds",
                table: "AbbMeasurements",
                columns: new[] { "Source", "Milliseconds" });

            migrationBuilder.CreateIndex(
                name: "IX_AbbMeasurements_Source_Timestamp",
                table: "AbbMeasurements",
                columns: new[] { "Source", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_SchneiderMeasurements_Source_Milliseconds",
                table: "SchneiderMeasurements",
                columns: new[] { "Source", "Milliseconds" });

            migrationBuilder.CreateIndex(
                name: "IX_SchneiderMeasurements_Source_Timestamp",
                table: "SchneiderMeasurements",
                columns: new[] { "Source", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbbMeasurements");

            migrationBuilder.DropTable(
                name: "SchneiderMeasurements");
        }
    }
}
