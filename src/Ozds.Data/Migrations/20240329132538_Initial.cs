using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.CreateTable(
                name: "representatives",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    social_security_number = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_representatives", x => x.id);
                    table.ForeignKey(
                        name: "fk_representatives_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_representatives_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_representatives_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "measurement_validators",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    kind = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    min_voltage_v = table.Column<float>(type: "real", nullable: true),
                    max_voltage_v = table.Column<float>(type: "real", nullable: true),
                    min_current_a = table.Column<float>(type: "real", nullable: true),
                    max_current_a = table.Column<float>(type: "real", nullable: true),
                    min_active_power_w = table.Column<float>(type: "real", nullable: true),
                    max_active_power_w = table.Column<float>(type: "real", nullable: true),
                    min_reactive_power_var = table.Column<float>(type: "real", nullable: true),
                    max_reactive_power_var = table.Column<float>(type: "real", nullable: true),
                    meter_id = table.Column<string>(type: "text", nullable: true),
                    min_apparent_power_va = table.Column<float>(type: "real", nullable: true),
                    max_apparent_power_va = table.Column<float>(type: "real", nullable: true),
                    schneideri_em3xxx_measurement_validator_entity_meter_id = table.Column<string>(type: "text", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measurement_validators", x => x.id);
                    table.ForeignKey(
                        name: "fk_measurement_validators_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_measurement_validators_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_measurement_validators_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "abb_b2x_aggregates",
                columns: table => new
                {
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    interval = table.Column<int>(type: "integer", nullable: false),
                    meter_id = table.Column<string>(type: "text", nullable: false),
                    voltage_l1_any_t0_avg_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l2_any_t0_avg_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l3_any_t0_avg_v = table.Column<float>(type: "real", nullable: false),
                    current_l1_any_t0_avg_a = table.Column<float>(type: "real", nullable: false),
                    current_l2_any_t0_avg_a = table.Column<float>(type: "real", nullable: false),
                    current_l3_any_t0_avg_a = table.Column<float>(type: "real", nullable: false),
                    active_power_l1_net_t0_avg_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l2_net_t0_avg_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l3_net_t0_avg_w = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l1_net_t0_avg_var = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l2_net_t0_avg_var = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l3_net_t0_avg_var = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t0_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t0_max_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_export_t0_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_export_t0_max_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_import_t0_min_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_import_t0_max_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_export_t0_min_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_export_t0_max_varh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t1_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t1_max_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t2_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t2_max_wh = table.Column<float>(type: "real", nullable: false),
                    count = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abb_b2x_aggregates", x => new { x.timestamp, x.interval, x.meter_id });
                })
                .Annotation("TimescaleHypertable", "timestamp,meter_id:number_partitions => 2");

            migrationBuilder.CreateTable(
                name: "abb_b2x_measurements",
                columns: table => new
                {
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    meter_id = table.Column<string>(type: "text", nullable: false),
                    voltage_l1_any_t0_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l2_any_t0_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l3_any_t0_v = table.Column<float>(type: "real", nullable: false),
                    current_l1_any_t0_a = table.Column<float>(type: "real", nullable: false),
                    current_l2_any_t0_a = table.Column<float>(type: "real", nullable: false),
                    current_l3_any_t0_a = table.Column<float>(type: "real", nullable: false),
                    active_power_l1_any_t0_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l2_any_t0_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l3_any_t0_w = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l1_any_t0_var = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l2_any_t0_var = table.Column<float>(type: "real", nullable: false),
                    reactive_power_l3_any_t0_var = table.Column<float>(type: "real", nullable: false),
                    active_energy_l1_import_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_l2_import_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_l3_import_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_l1_export_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_l2_export_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_l3_export_t0_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_l1_import_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_l2_import_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_l3_import_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_l1_export_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_l2_export_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_l3_export_t0_varh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_export_t0_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_import_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_export_t0_varh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t1_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t2_wh = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_abb_b2x_measurements", x => new { x.timestamp, x.meter_id });
                })
                .Annotation("TimescaleHypertable", "timestamp,meter_id:number_partitions => 2");

            migrationBuilder.CreateTable(
                name: "catalogues",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    _location_id = table.Column<long>(type: "bigint", nullable: false),
                    kind = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    active_energy_total_import_t0_price_eur = table.Column<float>(type: "real", nullable: true),
                    active_energy_total_import_t1_price_eur = table.Column<float>(type: "real", nullable: true),
                    meter_fee_price_eur = table.Column<float>(type: "real", nullable: true),
                    active_energy_total_import_t2_price_eur = table.Column<float>(type: "real", nullable: true),
                    max_active_power_total_import_t1_price_eur = table.Column<float>(type: "real", nullable: true),
                    renewable_energy_fee_price_eur = table.Column<float>(type: "real", nullable: true),
                    business_usage_fee_price_eur = table.Column<float>(type: "real", nullable: true),
                    tax_rate_percent = table.Column<float>(type: "real", nullable: true),
                    reactive_energy_total_import_t0_price_eur = table.Column<float>(type: "real", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_catalogues", x => x.id);
                    table.ForeignKey(
                        name: "fk_catalogues_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_catalogues_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_catalogues_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    measurement_location_id = table.Column<long>(type: "bigint", nullable: false),
                    white_medium_catalogue_id = table.Column<long>(type: "bigint", nullable: false),
                    white_medium_catalogue_id1 = table.Column<long>(type: "bigint", nullable: false),
                    blue_low_catalogue_id = table.Column<long>(type: "bigint", nullable: false),
                    blue_low_catalogue_id1 = table.Column<long>(type: "bigint", nullable: false),
                    white_low_catalogue_id = table.Column<long>(type: "bigint", nullable: false),
                    white_low_catalogue_id1 = table.Column<long>(type: "bigint", nullable: false),
                    red_low_catalogue_id = table.Column<long>(type: "bigint", nullable: false),
                    red_low_catalogue_id1 = table.Column<long>(type: "bigint", nullable: false),
                    regulatory_catalogue_id = table.Column<long>(type: "bigint", nullable: false),
                    regulatory_catalogue_id1 = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_locations_catalogues_blue_low_catalogue_id",
                        column: x => x.blue_low_catalogue_id1,
                        principalTable: "catalogues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_locations_catalogues_red_low_catalogue_id",
                        column: x => x.red_low_catalogue_id1,
                        principalTable: "catalogues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_locations_catalogues_regulatory_catalogue_id",
                        column: x => x.regulatory_catalogue_id1,
                        principalTable: "catalogues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_locations_catalogues_white_low_catalogue_id",
                        column: x => x.white_low_catalogue_id1,
                        principalTable: "catalogues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_locations_catalogues_white_medium_catalogue_id",
                        column: x => x.white_medium_catalogue_id1,
                        principalTable: "catalogues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_locations_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_locations_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_locations_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "location_entity_representative_entity",
                columns: table => new
                {
                    locations_id = table.Column<long>(type: "bigint", nullable: false),
                    representatives_string_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_entity_representative_entity", x => new { x.locations_id, x.representatives_string_id });
                    table.ForeignKey(
                        name: "fk_location_entity_representative_entity_locations_locations_id",
                        column: x => x.locations_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_location_entity_representative_entity_representatives_repre",
                        column: x => x.representatives_string_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "location_invoices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    _location_id = table.Column<long>(type: "bigint", nullable: false),
                    issued_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    issued_by_id = table.Column<string>(type: "text", nullable: true),
                    from_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    to_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_invoices", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_invoices_locations__location_id",
                        column: x => x._location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_location_invoices_representatives_issued_by_id",
                        column: x => x.issued_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "messengers",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    _location_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messengers", x => x.id);
                    table.ForeignKey(
                        name: "fk_messengers_locations__location_id",
                        column: x => x._location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_messengers_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messengers_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messengers_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "network_users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    _location_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_network_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_network_users_locations__location_id",
                        column: x => x._location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_network_users_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_network_users_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_network_users_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "measurement_locations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    meter_id = table.Column<string>(type: "text", nullable: false),
                    kind = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    _location_id = table.Column<long>(type: "bigint", nullable: true),
                    _network_user_id = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_measurement_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_measurement_locations_locations__location_id",
                        column: x => x._location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_measurement_locations_network_users__network_user_id",
                        column: x => x._network_user_id,
                        principalTable: "network_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_measurement_locations_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_measurement_locations_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_measurement_locations_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "network_user_entity_representative_entity",
                columns: table => new
                {
                    network_users_id = table.Column<long>(type: "bigint", nullable: false),
                    representatives_string_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_network_user_entity_representative_entity", x => new { x.network_users_id, x.representatives_string_id });
                    table.ForeignKey(
                        name: "fk_network_user_entity_representative_entity_network_users_net",
                        column: x => x.network_users_id,
                        principalTable: "network_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_network_user_entity_representative_entity_representatives_r",
                        column: x => x.representatives_string_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "network_user_invoices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    _network_user_id = table.Column<long>(type: "bigint", nullable: false),
                    issued_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    issued_by_id = table.Column<string>(type: "text", nullable: true),
                    from_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    to_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_network_user_invoices", x => x.id);
                    table.ForeignKey(
                        name: "fk_network_user_invoices_network_users__network_user_id",
                        column: x => x._network_user_id,
                        principalTable: "network_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_network_user_invoices_representatives_issued_by_id",
                        column: x => x.issued_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "meters",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    id1 = table.Column<string>(type: "text", nullable: false),
                    messenger_id = table.Column<string>(type: "text", nullable: false),
                    _measurement_location_id = table.Column<long>(type: "bigint", nullable: false),
                    _catalogue_id = table.Column<long>(type: "bigint", nullable: false),
                    connection_power_w = table.Column<float>(type: "real", nullable: false),
                    phases = table.Column<int[]>(type: "integer[]", nullable: false),
                    kind = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    _measurement_validator_id = table.Column<long>(type: "bigint", nullable: true),
                    schneideri_em3xxx_meter_entity__measurement_validator_id = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meters", x => x.id);
                    table.ForeignKey(
                        name: "fk_meters_catalogues__catalogue_id",
                        column: x => x._catalogue_id,
                        principalTable: "catalogues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meters_measurement_locations__measurement_location_id",
                        column: x => x._measurement_location_id,
                        principalTable: "measurement_locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meters_measurement_validators__measurement_validator_id",
                        column: x => x._measurement_validator_id,
                        principalTable: "measurement_validators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meters_measurement_validators__measurement_validator_id1",
                        column: x => x.schneideri_em3xxx_meter_entity__measurement_validator_id,
                        principalTable: "measurement_validators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meters_messengers_messenger_id",
                        column: x => x.messenger_id,
                        principalTable: "messengers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meters_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_meters_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_meters_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    kind = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    auditable_entity_id = table.Column<string>(type: "text", nullable: true),
                    auditable_entity_type = table.Column<string>(type: "text", nullable: true),
                    auditable_entity_table = table.Column<string>(type: "text", nullable: true),
                    audit = table.Column<int>(type: "integer", nullable: true),
                    catalogue_entity_id = table.Column<long>(type: "bigint", nullable: true),
                    location_entity_id = table.Column<long>(type: "bigint", nullable: true),
                    measurement_location_entity_id = table.Column<long>(type: "bigint", nullable: true),
                    measurement_validator_entity_id = table.Column<long>(type: "bigint", nullable: true),
                    messenger_entity_string_id = table.Column<string>(type: "text", nullable: true),
                    meter_entity_string_id = table.Column<string>(type: "text", nullable: true),
                    network_user_entity_id = table.Column<long>(type: "bigint", nullable: true),
                    representative_entity_string_id = table.Column<string>(type: "text", nullable: true),
                    representative_id = table.Column<string>(type: "text", nullable: true),
                    messenger_id = table.Column<string>(type: "text", nullable: true),
                    representative_event_entity_representative_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_events_catalogues_catalogue_entity_id",
                        column: x => x.catalogue_entity_id,
                        principalTable: "catalogues",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_locations_location_entity_id",
                        column: x => x.location_entity_id,
                        principalTable: "locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_measurement_locations_measurement_location_entity_id",
                        column: x => x.measurement_location_entity_id,
                        principalTable: "measurement_locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_measurement_validators_measurement_validator_entity_",
                        column: x => x.measurement_validator_entity_id,
                        principalTable: "measurement_validators",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_messengers_messenger_entity_string_id",
                        column: x => x.messenger_entity_string_id,
                        principalTable: "messengers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_messengers_messenger_id",
                        column: x => x.messenger_id,
                        principalTable: "messengers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_events_meters_meter_entity_string_id",
                        column: x => x.meter_entity_string_id,
                        principalTable: "meters",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_network_users_network_user_entity_id",
                        column: x => x.network_user_entity_id,
                        principalTable: "network_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_representatives_representative_entity_string_id",
                        column: x => x.representative_entity_string_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_events_representatives_representative_id",
                        column: x => x.representative_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_events_representatives_representative_id1",
                        column: x => x.representative_event_entity_representative_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schneider_iem3xxx_aggregates",
                columns: table => new
                {
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    interval = table.Column<int>(type: "integer", nullable: false),
                    meter_id = table.Column<string>(type: "text", nullable: false),
                    voltage_l1_any_t0_avg_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l2_any_t0_avg_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l3_any_t0_avg_v = table.Column<float>(type: "real", nullable: false),
                    current_l1_any_t0_avg_a = table.Column<float>(type: "real", nullable: false),
                    current_l2_any_t0_avg_a = table.Column<float>(type: "real", nullable: false),
                    current_l3_any_t0_avg_a = table.Column<float>(type: "real", nullable: false),
                    active_power_l1_net_t0_avg_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l2_net_t0_avg_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l3_net_t0_avg_w = table.Column<float>(type: "real", nullable: false),
                    reactive_power_total_net_t0_avg_var = table.Column<float>(type: "real", nullable: false),
                    apparent_power_total_net_t0_avg_va = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t0_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t0_max_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_export_t0_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_export_t0_max_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_import_t0_min_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_import_t0_max_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_export_t0_min_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_total_export_t0_max_varh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t1_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t1_max_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t2_min_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_total_import_t2_max_wh = table.Column<float>(type: "real", nullable: false),
                    count = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schneider_iem3xxx_aggregates", x => new { x.timestamp, x.interval, x.meter_id });
                    table.ForeignKey(
                        name: "fk_schneider_iem3xxx_aggregates_meters_meter_id",
                        column: x => x.meter_id,
                        principalTable: "meters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("TimescaleHypertable", "timestamp,meter_id:number_partitions => 2");

            migrationBuilder.CreateTable(
                name: "schneider_iem3xxx_measurements",
                columns: table => new
                {
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    meter_id = table.Column<string>(type: "text", nullable: false),
                    voltage_l1_any_t0_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l2_any_t0_v = table.Column<float>(type: "real", nullable: false),
                    voltage_l3_any_t0_v = table.Column<float>(type: "real", nullable: false),
                    current_l1_any_t0_a = table.Column<float>(type: "real", nullable: false),
                    current_l2_any_t0_a = table.Column<float>(type: "real", nullable: false),
                    current_l3_any_t0_a = table.Column<float>(type: "real", nullable: false),
                    active_power_l1_net_t0_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l2_net_t0_w = table.Column<float>(type: "real", nullable: false),
                    active_power_l3_net_t0_w = table.Column<float>(type: "real", nullable: false),
                    reactive_power_total_net_t0_var = table.Column<float>(type: "real", nullable: false),
                    apparent_power_total_net_t0_va = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l1_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l2_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_l3_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_total_t0_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_export_total_t0_wh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_import_total_t0_varh = table.Column<float>(type: "real", nullable: false),
                    reactive_energy_export_total_t0_varh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_total_t1_wh = table.Column<float>(type: "real", nullable: false),
                    active_energy_import_total_t2_wh = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schneider_iem3xxx_measurements", x => new { x.timestamp, x.meter_id });
                    table.ForeignKey(
                        name: "fk_schneider_iem3xxx_measurements_meters_meter_id",
                        column: x => x.meter_id,
                        principalTable: "meters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("TimescaleHypertable", "timestamp,meter_id:number_partitions => 2");

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_meter_id",
                table: "abb_b2x_aggregates",
                column: "meter_id");

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_meter_id",
                table: "abb_b2x_measurements",
                column: "meter_id");

            migrationBuilder.CreateIndex(
                name: "ix_catalogues__location_id",
                table: "catalogues",
                column: "_location_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_catalogues_created_by_id",
                table: "catalogues",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_catalogues_deleted_by_id",
                table: "catalogues",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_catalogues_last_updated_by_id",
                table: "catalogues",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_catalogue_entity_id",
                table: "events",
                column: "catalogue_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_location_entity_id",
                table: "events",
                column: "location_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_measurement_location_entity_id",
                table: "events",
                column: "measurement_location_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_measurement_validator_entity_id",
                table: "events",
                column: "measurement_validator_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_messenger_entity_string_id",
                table: "events",
                column: "messenger_entity_string_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_messenger_id",
                table: "events",
                column: "messenger_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_meter_entity_string_id",
                table: "events",
                column: "meter_entity_string_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_network_user_entity_id",
                table: "events",
                column: "network_user_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_representative_entity_string_id",
                table: "events",
                column: "representative_entity_string_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_representative_id",
                table: "events",
                column: "representative_id");

            migrationBuilder.CreateIndex(
                name: "ix_events_representative_id1",
                table: "events",
                column: "representative_event_entity_representative_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_entity_representative_entity_representatives_strin",
                table: "location_entity_representative_entity",
                column: "representatives_string_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_invoices__location_id",
                table: "location_invoices",
                column: "_location_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_invoices_issued_by_id",
                table: "location_invoices",
                column: "issued_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_blue_low_catalogue_id",
                table: "locations",
                column: "blue_low_catalogue_id1");

            migrationBuilder.CreateIndex(
                name: "ix_locations_created_by_id",
                table: "locations",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_deleted_by_id",
                table: "locations",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_last_updated_by_id",
                table: "locations",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_red_low_catalogue_id",
                table: "locations",
                column: "red_low_catalogue_id1");

            migrationBuilder.CreateIndex(
                name: "ix_locations_regulatory_catalogue_id",
                table: "locations",
                column: "regulatory_catalogue_id1");

            migrationBuilder.CreateIndex(
                name: "ix_locations_white_low_catalogue_id",
                table: "locations",
                column: "white_low_catalogue_id1");

            migrationBuilder.CreateIndex(
                name: "ix_locations_white_medium_catalogue_id",
                table: "locations",
                column: "white_medium_catalogue_id1");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations__location_id",
                table: "measurement_locations",
                column: "_location_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations__network_user_id",
                table: "measurement_locations",
                column: "_network_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations_created_by_id",
                table: "measurement_locations",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations_deleted_by_id",
                table: "measurement_locations",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations_last_updated_by_id",
                table: "measurement_locations",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_validators_created_by_id",
                table: "measurement_validators",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_validators_deleted_by_id",
                table: "measurement_validators",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_validators_last_updated_by_id",
                table: "measurement_validators",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_messengers__location_id",
                table: "messengers",
                column: "_location_id");

            migrationBuilder.CreateIndex(
                name: "ix_messengers_created_by_id",
                table: "messengers",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_messengers_deleted_by_id",
                table: "messengers",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_messengers_last_updated_by_id",
                table: "messengers",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_meters__catalogue_id",
                table: "meters",
                column: "_catalogue_id");

            migrationBuilder.CreateIndex(
                name: "ix_meters__measurement_location_id",
                table: "meters",
                column: "_measurement_location_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_meters__measurement_validator_id",
                table: "meters",
                column: "_measurement_validator_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_meters__measurement_validator_id1",
                table: "meters",
                column: "schneideri_em3xxx_meter_entity__measurement_validator_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_meters_created_by_id",
                table: "meters",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_meters_deleted_by_id",
                table: "meters",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_meters_last_updated_by_id",
                table: "meters",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_meters_messenger_id",
                table: "meters",
                column: "messenger_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_user_entity_representative_entity_representatives_s",
                table: "network_user_entity_representative_entity",
                column: "representatives_string_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_user_invoices__network_user_id",
                table: "network_user_invoices",
                column: "_network_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_user_invoices_issued_by_id",
                table: "network_user_invoices",
                column: "issued_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_users__location_id",
                table: "network_users",
                column: "_location_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_users_created_by_id",
                table: "network_users",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_users_deleted_by_id",
                table: "network_users",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_network_users_last_updated_by_id",
                table: "network_users",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_representatives_created_by_id",
                table: "representatives",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_representatives_deleted_by_id",
                table: "representatives",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_representatives_last_updated_by_id",
                table: "representatives",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_meter_id",
                table: "schneider_iem3xxx_aggregates",
                column: "meter_id");

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_meter_id",
                table: "schneider_iem3xxx_measurements",
                column: "meter_id");

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_aggregates_meters_meter_id",
                table: "abb_b2x_aggregates",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_measurements_meters_meter_id",
                table: "abb_b2x_measurements",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_catalogues_locations__location_id",
                table: "catalogues",
                column: "_location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_catalogues_locations__location_id",
                table: "catalogues");

            migrationBuilder.DropTable(
                name: "abb_b2x_aggregates");

            migrationBuilder.DropTable(
                name: "abb_b2x_measurements");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "location_entity_representative_entity");

            migrationBuilder.DropTable(
                name: "location_invoices");

            migrationBuilder.DropTable(
                name: "network_user_entity_representative_entity");

            migrationBuilder.DropTable(
                name: "network_user_invoices");

            migrationBuilder.DropTable(
                name: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropTable(
                name: "schneider_iem3xxx_measurements");

            migrationBuilder.DropTable(
                name: "meters");

            migrationBuilder.DropTable(
                name: "measurement_locations");

            migrationBuilder.DropTable(
                name: "measurement_validators");

            migrationBuilder.DropTable(
                name: "messengers");

            migrationBuilder.DropTable(
                name: "network_users");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "catalogues");

            migrationBuilder.DropTable(
                name: "representatives");
        }
    }
}
