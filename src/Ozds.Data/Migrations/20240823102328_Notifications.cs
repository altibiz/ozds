using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_events_auditable_entity_table_auditable_entity_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_events_auditable_entity_type_auditable_entity_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "description",
                table: "events");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:topic_entity", "general")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AddColumn<int[]>(
                name: "topics",
                table: "representatives",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<JsonDocument>(
                name: "content",
                table: "events",
                type: "jsonb",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    timestamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    event_id = table.Column<long>(type: "bigint", nullable: true),
                    title = table.Column<string>(type: "text", nullable: false),
                    summary = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    topic = table.Column<int>(type: "integer", nullable: false),
                    kind = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    resolved_by_id = table.Column<string>(type: "text", nullable: true),
                    resolved_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_events__event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_notifications_representatives_resolved_by_id",
                        column: x => x.resolved_by_id,
                        principalTable: "representatives",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "notification_representative_entity",
                columns: table => new
                {
                    representative_id = table.Column<string>(type: "text", nullable: false),
                    notification_id = table.Column<long>(type: "bigint", nullable: false),
                    seen_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_representative_entity", x => new { x.representative_id, x.notification_id });
                    table.ForeignKey(
                        name: "fk_notification_representative_entity_notifications_notificati",
                        column: x => x.notification_id,
                        principalTable: "notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notification_representative_entity_representatives_represen",
                        column: x => x.representative_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_events_audit_auditable_entity_table_auditable_entity_id",
                table: "events",
                columns: new[] { "audit", "auditable_entity_table", "auditable_entity_id" });

            migrationBuilder.CreateIndex(
                name: "ix_events_audit_auditable_entity_type_auditable_entity_id",
                table: "events",
                columns: new[] { "audit", "auditable_entity_type", "auditable_entity_id" });

            migrationBuilder.CreateIndex(
                name: "ix_notification_representative_entity__notification_id",
                table: "notification_representative_entity",
                column: "notification_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications__event_id",
                table: "notifications",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_resolved_by_id",
                table: "notifications",
                column: "resolved_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_representative_entity");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropIndex(
                name: "ix_events_audit_auditable_entity_table_auditable_entity_id",
                table: "events");

            migrationBuilder.DropIndex(
                name: "ix_events_audit_auditable_entity_type_auditable_entity_id",
                table: "events");

            migrationBuilder.DropColumn(
                name: "topics",
                table: "representatives");

            migrationBuilder.DropColumn(
                name: "content",
                table: "events");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "general")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_events_auditable_entity_table_auditable_entity_id",
                table: "events",
                columns: new[] { "auditable_entity_table", "auditable_entity_id" });

            migrationBuilder.CreateIndex(
                name: "ix_events_auditable_entity_type_auditable_entity_id",
                table: "events",
                columns: new[] { "auditable_entity_type", "auditable_entity_id" });
        }
    }
}
