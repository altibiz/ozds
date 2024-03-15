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
                    VoltageL1_V = table.Column<float>(type: "real", nullable: false),
                    VoltageL2_V = table.Column<float>(type: "real", nullable: false),
                    VoltageL3_V = table.Column<float>(type: "real", nullable: false),
                    CurrentL1_A = table.Column<float>(type: "real", nullable: false),
                    CurrentL2_A = table.Column<float>(type: "real", nullable: false),
                    CurrentL3_A = table.Column<float>(type: "real", nullable: false),
                    ActivePowerL1_W = table.Column<float>(type: "real", nullable: false),
                    ActivePowerL2_W = table.Column<float>(type: "real", nullable: false),
                    ActivePowerL3_W = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerL1_VAR = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerL2_VAR = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerL3_VAR = table.Column<float>(type: "real", nullable: false),
                    ActivePowerImportL1_Wh = table.Column<float>(type: "real", nullable: false),
                    ActivePowerImportL2_Wh = table.Column<float>(type: "real", nullable: false),
                    ActivePowerImportL3_Wh = table.Column<float>(type: "real", nullable: false),
                    ActivePowerExportL1_Wh = table.Column<float>(type: "real", nullable: false),
                    ActivePowerExportL2_Wh = table.Column<float>(type: "real", nullable: false),
                    ActivePowerExportL3_Wh = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerImportL1_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerImportL2_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerImportL3_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerExportL1_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerExportL2_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerExportL3_VARh = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyImportTotal_Wh = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyExportTotal_Wh = table.Column<float>(type: "real", nullable: false),
                    ReactiveEnergyImportTotal_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactiveEnergyExportTotal_VARh = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbbMeasurements", x => new { x.Source, x.Timestamp });
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NetworkUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Representatives",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsOperatorRepresentative = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SocialSecurityNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostalCode = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representatives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchneiderMeasurements",
                columns: table => new
                {
                    Source = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    VoltageL1_V = table.Column<float>(type: "real", nullable: false),
                    VoltageL2_V = table.Column<float>(type: "real", nullable: false),
                    VoltageL3_V = table.Column<float>(type: "real", nullable: false),
                    CurrentL1_A = table.Column<float>(type: "real", nullable: false),
                    CurrentL2_A = table.Column<float>(type: "real", nullable: false),
                    CurrentL3_A = table.Column<float>(type: "real", nullable: false),
                    ActivePowerL1_W = table.Column<float>(type: "real", nullable: false),
                    ActivePowerL2_W = table.Column<float>(type: "real", nullable: false),
                    ActivePowerL3_W = table.Column<float>(type: "real", nullable: false),
                    ReactivePowerTotal_VAR = table.Column<float>(type: "real", nullable: false),
                    ApparentPowerTotal_VA = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyImportL1_Wh = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyImportL2_Wh = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyImportL3_Wh = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyImportTotal_Wh = table.Column<float>(type: "real", nullable: false),
                    ActiveEnergyExportTotal_Wh = table.Column<float>(type: "real", nullable: false),
                    ReactiveEnergyImportTotal_VARh = table.Column<float>(type: "real", nullable: false),
                    ReactiveEnergyExportTotal_VARh = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchneiderMeasurements", x => new { x.Source, x.Timestamp });
                });

            migrationBuilder.CreateTable(
                name: "LocationEntityRepresentativeEntity",
                columns: table => new
                {
                    LocationsId = table.Column<string>(type: "text", nullable: false),
                    RepresentativesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEntityRepresentativeEntity", x => new { x.LocationsId, x.RepresentativesId });
                    table.ForeignKey(
                        name: "FK_LocationEntityRepresentativeEntity_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationEntityRepresentativeEntity_Representatives_Represen~",
                        column: x => x.RepresentativesId,
                        principalTable: "Representatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NetworkUserEntityRepresentativeEntity",
                columns: table => new
                {
                    NetworkUsersId = table.Column<string>(type: "text", nullable: false),
                    RepresentativesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkUserEntityRepresentativeEntity", x => new { x.NetworkUsersId, x.RepresentativesId });
                    table.ForeignKey(
                        name: "FK_NetworkUserEntityRepresentativeEntity_NetworkUsers_NetworkU~",
                        column: x => x.NetworkUsersId,
                        principalTable: "NetworkUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NetworkUserEntityRepresentativeEntity_Representatives_Repre~",
                        column: x => x.RepresentativesId,
                        principalTable: "Representatives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationEntityRepresentativeEntity_RepresentativesId",
                table: "LocationEntityRepresentativeEntity",
                column: "RepresentativesId");

            migrationBuilder.CreateIndex(
                name: "IX_NetworkUserEntityRepresentativeEntity_RepresentativesId",
                table: "NetworkUserEntityRepresentativeEntity",
                column: "RepresentativesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbbMeasurements");

            migrationBuilder.DropTable(
                name: "LocationEntityRepresentativeEntity");

            migrationBuilder.DropTable(
                name: "NetworkUserEntityRepresentativeEntity");

            migrationBuilder.DropTable(
                name: "SchneiderMeasurements");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "NetworkUsers");

            migrationBuilder.DropTable(
                name: "Representatives");
        }
    }
}
