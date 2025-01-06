using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadingDelete : Migration
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
                name: "fk_location_invoices_representatives_issued_by_id",
                table: "location_invoices");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_blue_low_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_red_low_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_white_low_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_white_medium_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_regulatory_catalogues_regulatory_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_locations_location_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_meters_meter_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_network_user_catalogues_network_user_",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_network_users_network_user_id",
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
                name: "fk_messengers_locations__location_id",
                table: "messengers");

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
                name: "fk_meters_measurement_validators__measurement_validator_id",
                table: "meters");

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
                name: "fk_network_users_locations_location_id",
                table: "network_users");

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
                name: "fk_location_invoices_representatives_issued_by_id",
                table: "location_invoices",
                column: "issued_by_id",
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
                name: "fk_locations_network_user_catalogues_blue_low_catalogue_id",
                table: "locations",
                column: "blue_low_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_network_user_catalogues_red_low_catalogue_id",
                table: "locations",
                column: "red_low_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_network_user_catalogues_white_low_catalogue_id",
                table: "locations",
                column: "white_low_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_network_user_catalogues_white_medium_catalogue_id",
                table: "locations",
                column: "white_medium_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_regulatory_catalogues_regulatory_catalogue_id",
                table: "locations",
                column: "regulatory_catalogue_id",
                principalTable: "regulatory_catalogues",
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
                name: "fk_measurement_locations_meters_meter_id",
                table: "measurement_locations",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_network_user_catalogues_network_user_",
                table: "measurement_locations",
                column: "network_user_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_network_users_network_user_id",
                table: "measurement_locations",
                column: "network_user_id",
                principalTable: "network_users",
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
                name: "fk_messengers_locations__location_id",
                table: "messengers",
                column: "location_id",
                principalTable: "locations",
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
                name: "fk_meters_measurement_validators__measurement_validator_id",
                table: "meters",
                column: "measurement_validator_id",
                principalTable: "measurement_validators",
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
                name: "fk_network_users_locations_location_id",
                table: "network_users",
                column: "location_id",
                principalTable: "locations",
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
                name: "fk_location_invoices_representatives_issued_by_id",
                table: "location_invoices");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_blue_low_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_red_low_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_white_low_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_network_user_catalogues_white_medium_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_locations_regulatory_catalogues_regulatory_catalogue_id",
                table: "locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_locations_location_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_meters_meter_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_network_user_catalogues_network_user_",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_network_users_network_user_id",
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
                name: "fk_messengers_locations__location_id",
                table: "messengers");

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
                name: "fk_meters_measurement_validators__measurement_validator_id",
                table: "meters");

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
                name: "fk_network_users_locations_location_id",
                table: "network_users");

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
                name: "fk_location_invoices_representatives_issued_by_id",
                table: "location_invoices",
                column: "issued_by_id",
                principalTable: "representatives",
                principalColumn: "id");

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
                name: "fk_locations_network_user_catalogues_blue_low_catalogue_id",
                table: "locations",
                column: "blue_low_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_network_user_catalogues_red_low_catalogue_id",
                table: "locations",
                column: "red_low_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_network_user_catalogues_white_low_catalogue_id",
                table: "locations",
                column: "white_low_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_network_user_catalogues_white_medium_catalogue_id",
                table: "locations",
                column: "white_medium_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_locations_regulatory_catalogues_regulatory_catalogue_id",
                table: "locations",
                column: "regulatory_catalogue_id",
                principalTable: "regulatory_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_locations_location_id",
                table: "measurement_locations",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_meters_meter_id",
                table: "measurement_locations",
                column: "meter_id",
                principalTable: "meters",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_network_user_catalogues_network_user_",
                table: "measurement_locations",
                column: "network_user_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_network_users_network_user_id",
                table: "measurement_locations",
                column: "network_user_id",
                principalTable: "network_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_created_by_id",
                table: "measurement_locations",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_deleted_by_id",
                table: "measurement_locations",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_representatives_last_updated_by_id",
                table: "measurement_locations",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_created_by_id",
                table: "measurement_validators",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_deleted_by_id",
                table: "measurement_validators",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_validators_representatives_last_updated_by_id",
                table: "measurement_validators",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_locations__location_id",
                table: "messengers",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_created_by_id",
                table: "messengers",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_deleted_by_id",
                table: "messengers",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messengers_representatives_last_updated_by_id",
                table: "messengers",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_meters_measurement_validators__measurement_validator_id",
                table: "meters",
                column: "measurement_validator_id",
                principalTable: "measurement_validators",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_meters_messengers_messenger_id",
                table: "meters",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_created_by_id",
                table: "meters",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_deleted_by_id",
                table: "meters",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_meters_representatives_last_updated_by_id",
                table: "meters",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id");

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
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_created_by_id",
                table: "network_user_catalogues",
                column: "created_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_deleted_by_id",
                table: "network_user_catalogues",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_catalogues_representatives_last_updated_by_id",
                table: "network_user_catalogues",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_invoices_representatives_issued_by_id",
                table: "network_user_invoices",
                column: "issued_by_id",
                principalTable: "representatives",
                principalColumn: "id");

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
                name: "fk_network_users_locations_location_id",
                table: "network_users",
                column: "location_id",
                principalTable: "locations",
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
                principalColumn: "id");

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
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_deleted_by_id",
                table: "regulatory_catalogues",
                column: "deleted_by_id",
                principalTable: "representatives",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_regulatory_catalogues_representatives_last_updated_by_id",
                table: "regulatory_catalogues",
                column: "last_updated_by_id",
                principalTable: "representatives",
                principalColumn: "id");

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
    }
}
