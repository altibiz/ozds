using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ozds.Data.Entities.Enums;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApiKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:action_entity", "read,list,create,update,delete,restore,forget")
                .Annotation("Npgsql:Enum:aggregation_entity", "min,max,avg")
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duplex_entity", "any,net,import,export")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:measure_entity", "current,voltage,active_power,reactive_power,apparent_power,active_energy,reactive_energy,apparent_energy")
                .Annotation("Npgsql:Enum:order_of_magnitude_entity", "giga,mega,kilo,hecto,deca,deci,centi,milli,micro,nano")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:tariff_entity", "t0,t1,t2")
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,meter,meter_inactivity,invalid_push,error,network_user_invoice_state")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,meter,meter_inactivity,invalid_push,error,network_user_invoice_state")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AddColumn<string>(
                name: "created_by_id",
                table: "network_user_representatives",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_on",
                table: "network_user_representatives",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "created_by_id",
                table: "location_representatives",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "created_on",
                table: "location_representatives",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "api_keys",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    principal_entity_type = table.Column<string>(type: "text", nullable: false),
                    principal_entity_table = table.Column<string>(type: "text", nullable: false),
                    principal_entity_id = table.Column<string>(type: "text", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: false),
                    expires_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("pk_api_keys", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_keys_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_api_keys_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_api_keys_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "scopes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    scope_entity_id = table.Column<string>(type: "text", nullable: true),
                    scope_entity_type = table.Column<string>(type: "text", nullable: true),
                    scope_entity_table = table.Column<string>(type: "text", nullable: true),
                    scope_action = table.Column<ActionEntity>(type: "action_entity", nullable: false),
                    kind = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    interval = table.Column<IntervalEntity>(type: "interval_entity", nullable: true),
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
                    table.PrimaryKey("pk_scopes", x => x.id);
                    table.ForeignKey(
                        name: "fk_scopes_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_scopes_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_scopes_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "api_key_scopes",
                columns: table => new
                {
                    api_key_id = table.Column<Guid>(type: "uuid", nullable: false),
                    scope_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_by_id = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_key_scopes", x => new { x.api_key_id, x.scope_id });
                    table.ForeignKey(
                        name: "fk_api_key_scopes_api_keys_api_key_id",
                        column: x => x.api_key_id,
                        principalTable: "api_keys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_api_key_scopes_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_api_key_scopes_scopes_scope_id",
                        column: x => x.scope_id,
                        principalTable: "scopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    scope_id = table.Column<Guid>(type: "uuid", nullable: false),
                    measure = table.Column<MeasureEntity>(type: "measure_entity", nullable: false),
                    order_of_magnitude = table.Column<OrderOfMagnitudeEntity>(type: "order_of_magnitude_entity", nullable: true),
                    tariff = table.Column<TariffEntity>(type: "tariff_entity", nullable: true),
                    duplex = table.Column<DuplexEntity>(type: "duplex_entity", nullable: true),
                    phase = table.Column<PhaseEntity>(type: "phase_entity", nullable: true),
                    aggregation = table.Column<AggregationEntity>(type: "aggregation_entity", nullable: true),
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
                    table.PrimaryKey("pk_registers", x => x.id);
                    table.ForeignKey(
                        name: "fk_registers_representatives_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_registers_representatives_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_registers_representatives_last_updated_by_id",
                        column: x => x.last_updated_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_registers_scopes_scope_id",
                        column: x => x.scope_id,
                        principalTable: "scopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_network_user_representatives_created_by_id",
                table: "network_user_representatives",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_representatives_created_by_id",
                table: "location_representatives",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_key_scopes__scope_id",
                table: "api_key_scopes",
                column: "scope_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_key_scopes_created_by_id",
                table: "api_key_scopes",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_created_by_id",
                table: "api_keys",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_deleted_by_id",
                table: "api_keys",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_expires_on",
                table: "api_keys",
                column: "expires_on");

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_last_updated_by_id",
                table: "api_keys",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_principal_entity_table_principal_entity_id",
                table: "api_keys",
                columns: new[] { "principal_entity_table", "principal_entity_id" });

            migrationBuilder.CreateIndex(
                name: "ix_api_keys_principal_entity_type_principal_entity_id",
                table: "api_keys",
                columns: new[] { "principal_entity_type", "principal_entity_id" });

            migrationBuilder.CreateIndex(
                name: "ix_registers_created_by_id",
                table: "registers",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_registers_deleted_by_id",
                table: "registers",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_registers_last_updated_by_id",
                table: "registers",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_registers_scope_id",
                table: "registers",
                column: "scope_id");

            migrationBuilder.CreateIndex(
                name: "ix_scopes_created_by_id",
                table: "scopes",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_scopes_deleted_by_id",
                table: "scopes",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_scopes_last_updated_by_id",
                table: "scopes",
                column: "last_updated_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_scopes_scope_entity_table_scope_entity_id",
                table: "scopes",
                columns: new[] { "scope_entity_table", "scope_entity_id" });

            migrationBuilder.CreateIndex(
                name: "ix_scopes_scope_entity_type_scope_entity_id",
                table: "scopes",
                columns: new[] { "scope_entity_type", "scope_entity_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_representatives_created_by_id",
                table: "location_representatives",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_representatives_created_by_id",
                table: "network_user_representatives",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_representatives_created_by_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_representatives_created_by_id",
                table: "network_user_representatives");

            migrationBuilder.DropTable(
                name: "api_key_scopes");

            migrationBuilder.DropTable(
                name: "registers");

            migrationBuilder.DropTable(
                name: "api_keys");

            migrationBuilder.DropTable(
                name: "scopes");

            migrationBuilder.DropIndex(
                name: "ix_network_user_representatives_created_by_id",
                table: "network_user_representatives");

            migrationBuilder.DropIndex(
                name: "ix_location_representatives_created_by_id",
                table: "location_representatives");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "network_user_representatives");

            migrationBuilder.DropColumn(
                name: "created_on",
                table: "network_user_representatives");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "location_representatives");

            migrationBuilder.DropColumn(
                name: "created_on",
                table: "location_representatives");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,meter,meter_inactivity,invalid_push,error,network_user_invoice_state")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:action_entity", "read,list,create,update,delete,restore,forget")
                .OldAnnotation("Npgsql:Enum:aggregation_entity", "min,max,avg")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duplex_entity", "any,net,import,export")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:measure_entity", "current,voltage,active_power,reactive_power,apparent_power,active_energy,reactive_energy,apparent_energy")
                .OldAnnotation("Npgsql:Enum:order_of_magnitude_entity", "giga,mega,kilo,hecto,deca,deci,centi,milli,micro,nano")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:tariff_entity", "t0,t1,t2")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,meter,meter_inactivity,invalid_push,error,network_user_invoice_state")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");
        }
    }
}
