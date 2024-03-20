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
                name: "abb_b2x_measurements",
                columns: table => new
                {
                    source = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    voltage_l1_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l2_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l3_v = table.Column<float>(type: "real", nullable: false),
                    current_l1_a = table.Column<float>(type: "real", nullable: false),
                    current_l2_a = table.Column<float>(type: "real", nullable: false),
                    current_l3_a = table.Column<float>(type: "real", nullable: false),
                    active_power_l1_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l2_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l3_w = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l1_var = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l2_var = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l3_var = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l1_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l2_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l3_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_export_l1_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_export_l2_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_export_l3_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_import_l1_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_import_l2_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_import_l3_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_export_l1_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_export_l2_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_export_l3_va_rh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_total_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_export_total_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_import_total_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_export_total_va_rh = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abb_b2x_measurements", x => new { x.timestamp, x.source });
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "network_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_network_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "representatives",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    is_operator_representative = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    social_security_number = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_representatives", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "schneideri_em3xxx_measurements",
                columns: table => new
                {
                    source = table.Column<string>(type: "text", nullable: false),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    voltage_l1_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l2_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l3_v = table.Column<float>(type: "real", nullable: false),
                    current_l1_a = table.Column<float>(type: "real", nullable: false),
                    current_l2_a = table.Column<float>(type: "real", nullable: false),
                    current_l3_a = table.Column<float>(type: "real", nullable: false),
                    active_power_l1_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l2_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l3_w = table.Column<float>(type: "real", nullable: false),
                    reactive_power_total_var = table.Column<float>(type: "real", nullable: false),
                    apparent_power_total_va = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l1_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l2_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l3_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_total_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_export_total_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_import_total_va_rh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_export_total_va_rh = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schneideri_em3xxx_measurements", x => new { x.timestamp, x.source });
                });

            migrationBuilder.CreateTable(
                name: "location_entity_representative_entity",
                columns: table => new
                {
                    locations_id = table.Column<string>(type: "text", nullable: false),
                    representatives_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_entity_representative_entity", x => new { x.locations_id, x.representatives_id });
                    table.ForeignKey(
                        name: "fk_location_entity_representative_entity_locations_locations_id",
                        column: x => x.locations_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_location_entity_representative_entity_representatives_repre",
                        column: x => x.representatives_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "network_user_entity_representative_entity",
                columns: table => new
                {
                    network_users_id = table.Column<string>(type: "text", nullable: false),
                    representatives_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_network_user_entity_representative_entity", x => new { x.network_users_id, x.representatives_id });
                    table.ForeignKey(
                        name: "fk_network_user_entity_representative_entity_network_users_net",
                        column: x => x.network_users_id,
                        principalTable: "network_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_network_user_entity_representative_entity_representatives_r",
                        column: x => x.representatives_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_location_entity_representative_entity_representatives_id",
                table: "location_entity_representative_entity",
                column: "representatives_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_user_entity_representative_entity_representatives_id",
                table: "network_user_entity_representative_entity",
                column: "representatives_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abb_b2x_measurements");

            migrationBuilder.DropTable(
                name: "location_entity_representative_entity");

            migrationBuilder.DropTable(
                name: "network_user_entity_representative_entity");

            migrationBuilder.DropTable(
                name: "schneideri_em3xxx_measurements");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "network_users");

            migrationBuilder.DropTable(
                name: "representatives");
        }
    }
}
