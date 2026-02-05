using Microsoft.EntityFrameworkCore.Migrations;
using Ozds.Data.Entities.Enums;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class MeterInactivityDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation(
                    "Npgsql:Enum:role_entity",
                    "operator_representative,location_representative,network_user_representative"
                )
                .Annotation(
                    "Npgsql:Enum:topic_entity",
                    "all,messenger,messenger_inactivity,meter,meter_inactivity,invalid_push,error,network_user_invoice_state"
                )
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation(
                    "Npgsql:Enum:audit_entity",
                    "query,creation,modification,deletion,restoration,forgetting"
                )
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation(
                    "Npgsql:Enum:role_entity",
                    "operator_representative,location_representative,network_user_representative"
                )
                .OldAnnotation(
                    "Npgsql:Enum:topic_entity",
                    "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state"
                )
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AddColumn<string>(name: "meter_id", table: "notifications", type: "text", nullable: true);

            migrationBuilder.AddColumn<DurationEntity>(
                name: "am_mip_duration",
                table: "network_user_calculations",
                type: "duration_entity",
                nullable: false,
                defaultValue: DurationEntity.Second
            );

            migrationBuilder.AddColumn<long>(
                name: "am_mip_multiplier",
                table: "network_user_calculations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L
            );

            migrationBuilder.AddColumn<DurationEntity>(
                name: "max_inactivity_period_duration",
                table: "meters",
                type: "duration_entity",
                nullable: false,
                defaultValue: DurationEntity.Second
            );

            migrationBuilder.AddColumn<long>(
                name: "max_inactivity_period_multiplier",
                table: "meters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L
            );

            migrationBuilder.CreateIndex(name: "ix_notifications_meter_id", table: "notifications", column: "meter_id");

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_meters_meter_id",
                table: "notifications",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.Sql(
                @"
                UPDATE meters
                SET
                    max_inactivity_period_duration = messengers.max_inactivity_period_duration,
                    max_inactivity_period_multiplier = messengers.max_inactivity_period_multiplier
                FROM messengers
                WHERE meters.messenger_id = messengers.id;
            "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "fk_notifications_meters_meter_id", table: "notifications");

            migrationBuilder.DropIndex(name: "ix_notifications_meter_id", table: "notifications");

            migrationBuilder.DropColumn(name: "meter_id", table: "notifications");

            migrationBuilder.DropColumn(name: "am_mip_duration", table: "network_user_calculations");

            migrationBuilder.DropColumn(name: "am_mip_multiplier", table: "network_user_calculations");

            migrationBuilder.DropColumn(name: "max_inactivity_period_duration", table: "meters");

            migrationBuilder.DropColumn(name: "max_inactivity_period_multiplier", table: "meters");

            migrationBuilder
                .AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation(
                    "Npgsql:Enum:role_entity",
                    "operator_representative,location_representative,network_user_representative"
                )
                .Annotation(
                    "Npgsql:Enum:topic_entity",
                    "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state"
                )
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation(
                    "Npgsql:Enum:audit_entity",
                    "query,creation,modification,deletion,restoration,forgetting"
                )
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation(
                    "Npgsql:Enum:role_entity",
                    "operator_representative,location_representative,network_user_representative"
                )
                .OldAnnotation(
                    "Npgsql:Enum:topic_entity",
                    "all,messenger,messenger_inactivity,meter,meter_inactivity,invalid_push,error,network_user_invoice_state"
                )
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");
        }
    }
}
