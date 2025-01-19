using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteBehaviour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_aggregates_measurement_locations_measurement_locati",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_aggregates_meters_meter_id",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_measurements_measurement_locations_measurement_loca",
                table: "abb_b2x_measurements");

            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_measurements_meters_meter_id",
                table: "abb_b2x_measurements");

            migrationBuilder.DropForeignKey(
                name: "fk_events_messengers_messenger_id",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "fk_events_representatives_representative_id",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_locations_location_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_representatives_created_by_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_representatives_deleted_by_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_representatives_last_updated_by_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_validators_representatives_created_by_id",
                table: "measurement_validators");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_validators_representatives_deleted_by_id",
                table: "measurement_validators");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_validators_representatives_last_updated_by_id",
                table: "measurement_validators");

            migrationBuilder.DropForeignKey(
                name: "fk_messengers_representatives_created_by_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_messengers_representatives_deleted_by_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_messengers_representatives_last_updated_by_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_messengers_messenger_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_representatives_created_by_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_representatives_deleted_by_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_representatives_last_updated_by_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_representatives_issued_by_id",
                table: "network_user_calculations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_catalogues_representatives_created_by_id",
                table: "network_user_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_catalogues_representatives_deleted_by_id",
                table: "network_user_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_catalogues_representatives_last_updated_by_id",
                table: "network_user_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_invoices_representatives_issued_by_id",
                table: "network_user_invoices");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_network_users_network_user_id",
                table: "network_user_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_representatives_representative",
                table: "network_user_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipients_notifications_notification_id",
                table: "notification_recipients");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipients_representatives_representative_id",
                table: "notification_recipients");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_events__event_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_messengers_messenger_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_network_user_invoices_invoice_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_regulatory_catalogues_representatives_created_by_id",
                table: "regulatory_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_regulatory_catalogues_representatives_deleted_by_id",
                table: "regulatory_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_regulatory_catalogues_representatives_last_updated_by_id",
                table: "regulatory_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_measurement_locations_measurem",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_meters_meter_id",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_measurements_measurement_locations_measur",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_measurements_meters_meter_id",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropTable(
                name: "location_invoices");

            migrationBuilder.DropIndex(
                name: "ix_measurement_locations_location_id",
                table: "measurement_locations");

            migrationBuilder.DropColumn(
                name: "location_id",
                table: "measurement_locations");

            migrationBuilder.RenameColumn(
                name: "usage_meter_total_eur",
                table: "network_user_calculations",
                newName: "usage_meter_fee_total_eur");

            migrationBuilder.RenameColumn(
                name: "jen_ramped_total_eur",
                table: "network_user_calculations",
                newName: "jen_total_eur");

            migrationBuilder.RenameColumn(
                name: "jen_ramped_price_eur",
                table: "network_user_calculations",
                newName: "jen_price_eur");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_aggregates_measurement_locations_measurement_locati",
                table: "abb_b2x_aggregates",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_aggregates_meters_meter_id",
                table: "abb_b2x_aggregates",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_measurements_measurement_locations_measurement_loca",
                table: "abb_b2x_measurements",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
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
                name: "fk_events_messengers_messenger_id",
                table: "events",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_events_representatives_representative_id",
                table: "events",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_created_by_id",
                table: "measurement_locations",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_deleted_by_id",
                table: "measurement_locations",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_last_updated_by_id",
                table: "measurement_locations",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_created_by_id",
                table: "measurement_validators",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_deleted_by_id",
                table: "measurement_validators",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_last_updated_by_id",
                table: "measurement_validators",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_created_by_id",
                table: "messengers",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_deleted_by_id",
                table: "messengers",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_last_updated_by_id",
                table: "messengers",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_messengers_messenger_id",
                table: "meters",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_created_by_id",
                table: "meters",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_deleted_by_id",
                table: "meters",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_last_updated_by_id",
                table: "meters",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations",
                column: "network_user_invoice_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_representatives_issued_by_id",
                table: "network_user_calculations",
                column: "issued_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_created_by_id",
                table: "network_user_catalogues",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_deleted_by_id",
                table: "network_user_catalogues",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_last_updated_by_id",
                table: "network_user_catalogues",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_invoices_representatives_issued_by_id",
                table: "network_user_invoices",
                column: "issued_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_network_users_network_user_id",
                table: "network_user_representatives",
                column: "network_user_id",
                principalTable: "network_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_representatives_representative",
                table: "network_user_representatives",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipients_notifications_notification_id",
                table: "notification_recipients",
                column: "notification_id",
                principalTable: "notifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipients_representatives_representative_id",
                table: "notification_recipients",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_events__event_id",
                table: "notifications",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_messengers_messenger_id",
                table: "notifications",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_network_user_invoices_invoice_id",
                table: "notifications",
                column: "invoice_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_created_by_id",
                table: "regulatory_catalogues",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_deleted_by_id",
                table: "regulatory_catalogues",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_last_updated_by_id",
                table: "regulatory_catalogues",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_measurement_locations_measurem",
                table: "schneider_iem3xxx_aggregates",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_meters_meter_id",
                table: "schneider_iem3xxx_aggregates",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_measurements_measurement_locations_measur",
                table: "schneider_iem3xxx_measurements",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_measurements_meters_meter_id",
                table: "schneider_iem3xxx_measurements",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_aggregates_measurement_locations_measurement_locati",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_aggregates_meters_meter_id",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_measurements_measurement_locations_measurement_loca",
                table: "abb_b2x_measurements");

            migrationBuilder.DropForeignKey(
                name: "fk_abb_b2x_measurements_meters_meter_id",
                table: "abb_b2x_measurements");

            migrationBuilder.DropForeignKey(
                name: "fk_events_messengers_messenger_id",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "fk_events_representatives_representative_id",
                table: "events");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_representatives_created_by_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_representatives_deleted_by_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_representatives_last_updated_by_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_validators_representatives_created_by_id",
                table: "measurement_validators");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_validators_representatives_deleted_by_id",
                table: "measurement_validators");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_validators_representatives_last_updated_by_id",
                table: "measurement_validators");

            migrationBuilder.DropForeignKey(
                name: "fk_messengers_representatives_created_by_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_messengers_representatives_deleted_by_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_messengers_representatives_last_updated_by_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_messengers_messenger_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_representatives_created_by_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_representatives_deleted_by_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_meters_representatives_last_updated_by_id",
                table: "meters");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_representatives_issued_by_id",
                table: "network_user_calculations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_catalogues_representatives_created_by_id",
                table: "network_user_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_catalogues_representatives_deleted_by_id",
                table: "network_user_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_catalogues_representatives_last_updated_by_id",
                table: "network_user_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_invoices_representatives_issued_by_id",
                table: "network_user_invoices");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_network_users_network_user_id",
                table: "network_user_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_representatives_representative",
                table: "network_user_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipients_notifications_notification_id",
                table: "notification_recipients");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipients_representatives_representative_id",
                table: "notification_recipients");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_events__event_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_messengers_messenger_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_network_user_invoices_invoice_id",
                table: "notifications");

            migrationBuilder.DropForeignKey(
                name: "fk_regulatory_catalogues_representatives_created_by_id",
                table: "regulatory_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_regulatory_catalogues_representatives_deleted_by_id",
                table: "regulatory_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_regulatory_catalogues_representatives_last_updated_by_id",
                table: "regulatory_catalogues");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_measurement_locations_measurem",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_meters_meter_id",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_measurements_measurement_locations_measur",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropForeignKey(
                name: "fk_schneider_iem3xxx_measurements_meters_meter_id",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.RenameColumn(
                name: "usage_meter_fee_total_eur",
                table: "network_user_calculations",
                newName: "usage_meter_total_eur");

            migrationBuilder.RenameColumn(
                name: "jen_total_eur",
                table: "network_user_calculations",
                newName: "jen_ramped_total_eur");

            migrationBuilder.RenameColumn(
                name: "jen_price_eur",
                table: "network_user_calculations",
                newName: "jen_ramped_price_eur");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion,restoration,forgetting")
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AddColumn<long>(
                name: "location_id",
                table: "measurement_locations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "location_invoices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    location_id = table.Column<long>(type: "bigint", nullable: false),
                    issued_by_id = table.Column<string>(type: "text", nullable: true),
                    from_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    issued_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    tax_rate_percent = table.Column<decimal>(type: "numeric", nullable: false),
                    tax_eur = table.Column<decimal>(type: "numeric", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    to_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    total_with_tax_eur = table.Column<decimal>(type: "numeric", nullable: false),
                    total_eur = table.Column<decimal>(type: "numeric", nullable: false),
                    al_alti_biz_sub_project_code = table.Column<string>(type: "text", nullable: false),
                    al_blue_low_network_user_catalogue_id = table.Column<string>(type: "text", nullable: false),
                    al_created_by_id = table.Column<string>(type: "text", nullable: true),
                    al_created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    al_deleted_by_id = table.Column<string>(type: "text", nullable: true),
                    al_deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    al_is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    al_last_updated_by_id = table.Column<string>(type: "text", nullable: true),
                    al_last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    al_red_low_network_user_catalogue_id = table.Column<string>(type: "text", nullable: false),
                    al_regulatory_catalogue_id = table.Column<string>(type: "text", nullable: false),
                    al_title = table.Column<string>(type: "text", nullable: false),
                    al_white_low_network_user_catalogue_id = table.Column<string>(type: "text", nullable: false),
                    al_white_medium_network_user_catalogue_id = table.Column<string>(type: "text", nullable: false),
                    al_lp_address = table.Column<string>(type: "text", nullable: false),
                    al_lp_city = table.Column<string>(type: "text", nullable: false),
                    al_lp_email = table.Column<string>(type: "text", nullable: false),
                    al_lp_name = table.Column<string>(type: "text", nullable: false),
                    al_lp_phone_number = table.Column<string>(type: "text", nullable: false),
                    al_lp_postal_code = table.Column<string>(type: "text", nullable: false),
                    al_lp_social_security_number = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_location_invoices", x => x.id);
                    table.ForeignKey(
                        name: "fk_location_invoices_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_location_invoices_representatives_issued_by_id",
                        column: x => x.issued_by_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations_location_id",
                table: "measurement_locations",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_invoices__location_id",
                table: "location_invoices",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_location_invoices_issued_by_id",
                table: "location_invoices",
                column: "issued_by_id");

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_aggregates_measurement_locations_measurement_locati",
                table: "abb_b2x_aggregates",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_aggregates_meters_meter_id",
                table: "abb_b2x_aggregates",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_measurements_measurement_locations_measurement_loca",
                table: "abb_b2x_measurements",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_abb_b2x_measurements_meters_meter_id",
                table: "abb_b2x_measurements",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_events_messengers_messenger_id",
                table: "events",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_events_representatives_representative_id",
                table: "events",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_locations_location_id",
                table: "measurement_locations",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_created_by_id",
                table: "measurement_locations",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_deleted_by_id",
                table: "measurement_locations",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_last_updated_by_id",
                table: "measurement_locations",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_created_by_id",
                table: "measurement_validators",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_deleted_by_id",
                table: "measurement_validators",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_last_updated_by_id",
                table: "measurement_validators",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_created_by_id",
                table: "messengers",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_deleted_by_id",
                table: "messengers",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_last_updated_by_id",
                table: "messengers",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_messengers_messenger_id",
                table: "meters",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_created_by_id",
                table: "meters",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_deleted_by_id",
                table: "meters",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_last_updated_by_id",
                table: "meters",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations",
                column: "network_user_invoice_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_representatives_issued_by_id",
                table: "network_user_calculations",
                column: "issued_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_created_by_id",
                table: "network_user_catalogues",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_deleted_by_id",
                table: "network_user_catalogues",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_last_updated_by_id",
                table: "network_user_catalogues",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_invoices_representatives_issued_by_id",
                table: "network_user_invoices",
                column: "issued_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_network_users_network_user_id",
                table: "network_user_representatives",
                column: "network_user_id",
                principalTable: "network_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_representatives_representative",
                table: "network_user_representatives",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipients_notifications_notification_id",
                table: "notification_recipients",
                column: "notification_id",
                principalTable: "notifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipients_representatives_representative_id",
                table: "notification_recipients",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_events__event_id",
                table: "notifications",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_messengers_messenger_id",
                table: "notifications",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_network_user_invoices_invoice_id",
                table: "notifications",
                column: "invoice_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_created_by_id",
                table: "regulatory_catalogues",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_deleted_by_id",
                table: "regulatory_catalogues",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_last_updated_by_id",
                table: "regulatory_catalogues",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_measurement_locations_measurem",
                table: "schneider_iem3xxx_aggregates",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_aggregates_meters_meter_id",
                table: "schneider_iem3xxx_aggregates",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_measurements_measurement_locations_measur",
                table: "schneider_iem3xxx_measurements",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_schneider_iem3xxx_measurements_meters_meter_id",
                table: "schneider_iem3xxx_measurements",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
