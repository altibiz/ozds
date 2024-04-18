using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Calculations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "representatives");

            migrationBuilder.DropColumn(
                name: "city",
                table: "representatives");

            migrationBuilder.DropColumn(
                name: "postal_code",
                table: "representatives");

            migrationBuilder.DropColumn(
                name: "social_security_number",
                table: "representatives");

            migrationBuilder.DropColumn(
                name: "meter_fee_price_eur",
                table: "network_user_calculations");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "representatives",
                newName: "physical_person_phone_number");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "representatives",
                newName: "physical_person_name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "representatives",
                newName: "physical_person_email");

            migrationBuilder.RenameColumn(
                name: "asrc_renewable_energy_fee_price__e_u_r",
                table: "network_user_calculations",
                newName: "asrc_renewable_energy_fee_price__eur");

            migrationBuilder.RenameColumn(
                name: "asrc_business_usage_fee_price__e_u_r",
                table: "network_user_calculations",
                newName: "asrc_business_usage_fee_price__eur");

            migrationBuilder.RenameColumn(
                name: "asrc_active_energy_total_import_t2_price__e_u_r",
                table: "network_user_calculations",
                newName: "asrc_active_energy_total_import_t2_price__eur");

            migrationBuilder.RenameColumn(
                name: "asrc_active_energy_total_import_t1_price__e_u_r",
                table: "network_user_calculations",
                newName: "asrc_active_energy_total_import_t1_price__eur");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_ramped_total_eur",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_ramped_total_eur");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_ramped_price_eur",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_ramped_price_eur");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_ramped_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_ramped_amount_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_import_min_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_import_min_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_import_max_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_import_max_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_import_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_import_amount_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_export_min_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_export_min_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_export_max_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_export_max_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_ramped_t0_export_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_export_amount_varh");

            migrationBuilder.RenameColumn(
                name: "active_power_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "usage_meter_total_eur");

            migrationBuilder.RenameColumn(
                name: "active_power_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "usage_meter_fee_price_eur");

            migrationBuilder.RenameColumn(
                name: "active_power_total_import_t1_peak_w",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_peak_w");

            migrationBuilder.RenameColumn(
                name: "active_power_total_import_t1_amount_w",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_amount_w");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t2_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t2_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t2_min_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_min_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t2_max_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_max_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t2_amount_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_amount_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_total_eur");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_price_eur");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t1_min_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_min_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t1_max_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_max_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t1_amount_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_amount_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t0_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t0_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t0_min_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_min_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t0_max_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_max_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t0_amount_wh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_amount_wh");

            migrationBuilder.RenameColumn(
                name: "archived_location_white_medium_network_user_catalogue_id",
                table: "location_invoices",
                newName: "al_white_medium_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_white_low_network_user_catalogue_id",
                table: "location_invoices",
                newName: "al_white_low_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_regulatory_catalogue_id",
                table: "location_invoices",
                newName: "al_regulatory_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_red_low_network_user_catalogue_id",
                table: "location_invoices",
                newName: "al_red_low_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_last_updated_on",
                table: "location_invoices",
                newName: "al_last_updated_on");

            migrationBuilder.RenameColumn(
                name: "archived_location_last_updated_by_id",
                table: "location_invoices",
                newName: "al_last_updated_by_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_is_deleted",
                table: "location_invoices",
                newName: "al_is_deleted");

            migrationBuilder.RenameColumn(
                name: "archived_location_deleted_on",
                table: "location_invoices",
                newName: "al_deleted_on");

            migrationBuilder.RenameColumn(
                name: "archived_location_deleted_by_id",
                table: "location_invoices",
                newName: "al_deleted_by_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_created_on",
                table: "location_invoices",
                newName: "al_created_on");

            migrationBuilder.RenameColumn(
                name: "archived_location_created_by_id",
                table: "location_invoices",
                newName: "al_created_by_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_blue_low_network_user_catalogue_id",
                table: "location_invoices",
                newName: "al_blue_low_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "archived_location_title",
                table: "location_invoices",
                newName: "al_lp_social_security_number");

            migrationBuilder.RenameColumn(
                name: "archived_location_id",
                table: "location_invoices",
                newName: "al_lp_postal_code");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_address",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_city",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_email",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_name",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_phone_number",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_postal_code",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_social_security_number",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_address",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_city",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_email",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_name",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_phone_number",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_postal_code",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_social_security_number",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_address",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_city",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_email",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_name",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_phone_number",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_postal_code",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_lp_social_security_number",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "arc_active_energy_total_import_t1_price__eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "arc_active_energy_total_import_t2_price__eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "arc_business_usage_fee_price__eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "arc_created_by_id",
                table: "network_user_invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "arc_created_on",
                table: "network_user_invoices",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "arc_deleted_by_id",
                table: "network_user_invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "arc_deleted_on",
                table: "network_user_invoices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "arc_is_deleted",
                table: "network_user_invoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "arc_last_updated_by_id",
                table: "network_user_invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "arc_last_updated_on",
                table: "network_user_invoices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "arc_renewable_energy_fee_price__eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "arc_tax_rate__percent",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_business_usage_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_renewable_energy_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_active_energy_total_import_t0fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_active_power_total_import_t1peak_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_meter_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_reactive_energy_total_ramped_t0fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t1_amount_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t1_max_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t1_min_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t1_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t1_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t2_amount_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t2_max_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t2_min_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t2_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_active_energy_total_import_t2_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_business_usage_fee_amount",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_business_usage_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_business_usage_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_renewable_energy_fee_amount",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_renewable_energy_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "supply_renewable_energy_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_active_energy_total_import_t0_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_active_energy_total_import_t0_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_meter_fee_amount",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "legal_person_address",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_city",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_email",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_name",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_phone_number",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_postal_code",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "legal_person_social_security_number",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_address",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_city",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_email",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_name",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_lp_phone_number",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "legal_person_address",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "legal_person_city",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "legal_person_email",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "legal_person_name",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "legal_person_phone_number",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "legal_person_postal_code",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "legal_person_social_security_number",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "al_lp_address",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_city",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_email",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_name",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_phone_number",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_postal_code",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_social_security_number",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_address",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_city",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_email",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_name",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_phone_number",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_postal_code",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_lp_social_security_number",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_active_energy_total_import_t1_price__eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_active_energy_total_import_t2_price__eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_business_usage_fee_price__eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_created_by_id",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_created_on",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_deleted_by_id",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_deleted_on",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_is_deleted",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_last_updated_by_id",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_last_updated_on",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_renewable_energy_fee_price__eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_tax_rate__percent",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "supply_business_usage_fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "supply_fee_total_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "supply_renewable_energy_fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_active_energy_total_import_t0fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_active_power_total_import_t1peak_fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_fee_total_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_meter_fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_reactive_energy_total_ramped_t0fee_eur",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t1_amount_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t1_max_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t1_min_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t1_price_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t1_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t2_amount_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t2_max_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t2_min_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t2_price_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_active_energy_total_import_t2_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_business_usage_fee_amount",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_business_usage_fee_price_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_business_usage_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_fee_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_renewable_energy_fee_amount",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_renewable_energy_fee_price_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "supply_renewable_energy_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "usage_active_energy_total_import_t0_price_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "usage_active_energy_total_import_t0_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "usage_fee_total_eur",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "usage_meter_fee_amount",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "legal_person_address",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "legal_person_city",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "legal_person_email",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "legal_person_name",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "legal_person_phone_number",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "legal_person_postal_code",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "legal_person_social_security_number",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "al_lp_address",
                table: "location_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_city",
                table: "location_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_email",
                table: "location_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_name",
                table: "location_invoices");

            migrationBuilder.DropColumn(
                name: "al_lp_phone_number",
                table: "location_invoices");

            migrationBuilder.RenameColumn(
                name: "physical_person_phone_number",
                table: "representatives",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "physical_person_name",
                table: "representatives",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "physical_person_email",
                table: "representatives",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "asrc_renewable_energy_fee_price__eur",
                table: "network_user_calculations",
                newName: "asrc_renewable_energy_fee_price__e_u_r");

            migrationBuilder.RenameColumn(
                name: "asrc_business_usage_fee_price__eur",
                table: "network_user_calculations",
                newName: "asrc_business_usage_fee_price__e_u_r");

            migrationBuilder.RenameColumn(
                name: "asrc_active_energy_total_import_t2_price__eur",
                table: "network_user_calculations",
                newName: "asrc_active_energy_total_import_t2_price__e_u_r");

            migrationBuilder.RenameColumn(
                name: "asrc_active_energy_total_import_t1_price__eur",
                table: "network_user_calculations",
                newName: "asrc_active_energy_total_import_t1_price__e_u_r");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_ramped_total_eur",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_ramped_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_ramped_price_eur",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_ramped_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_ramped_amount_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_ramped_amount_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_import_min_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_import_min_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_import_max_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_import_max_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_import_amount_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_import_amount_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_export_min_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_export_min_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_export_max_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_export_max_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_export_amount_varh",
                table: "network_user_calculations",
                newName: "reactive_energy_total_ramped_t0_export_amount_varh");

            migrationBuilder.RenameColumn(
                name: "usage_meter_total_eur",
                table: "network_user_calculations",
                newName: "active_power_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_meter_fee_price_eur",
                table: "network_user_calculations",
                newName: "active_power_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t2_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t2_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_peak_w",
                table: "network_user_calculations",
                newName: "active_power_total_import_t1_peak_w");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_amount_w",
                table: "network_user_calculations",
                newName: "active_power_total_import_t1_amount_w");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_total_eur",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_price_eur",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_min_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t2_min_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_max_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t2_max_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_amount_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t2_amount_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t0_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t0_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_min_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t1_min_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_max_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t1_max_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_amount_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t1_amount_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_min_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t0_min_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_max_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t0_max_wh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_amount_wh",
                table: "network_user_calculations",
                newName: "active_energy_total_import_t0_amount_wh");

            migrationBuilder.RenameColumn(
                name: "al_white_medium_network_user_catalogue_id",
                table: "location_invoices",
                newName: "archived_location_white_medium_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "al_white_low_network_user_catalogue_id",
                table: "location_invoices",
                newName: "archived_location_white_low_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "al_regulatory_catalogue_id",
                table: "location_invoices",
                newName: "archived_location_regulatory_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "al_red_low_network_user_catalogue_id",
                table: "location_invoices",
                newName: "archived_location_red_low_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "al_last_updated_on",
                table: "location_invoices",
                newName: "archived_location_last_updated_on");

            migrationBuilder.RenameColumn(
                name: "al_last_updated_by_id",
                table: "location_invoices",
                newName: "archived_location_last_updated_by_id");

            migrationBuilder.RenameColumn(
                name: "al_is_deleted",
                table: "location_invoices",
                newName: "archived_location_is_deleted");

            migrationBuilder.RenameColumn(
                name: "al_deleted_on",
                table: "location_invoices",
                newName: "archived_location_deleted_on");

            migrationBuilder.RenameColumn(
                name: "al_deleted_by_id",
                table: "location_invoices",
                newName: "archived_location_deleted_by_id");

            migrationBuilder.RenameColumn(
                name: "al_created_on",
                table: "location_invoices",
                newName: "archived_location_created_on");

            migrationBuilder.RenameColumn(
                name: "al_created_by_id",
                table: "location_invoices",
                newName: "archived_location_created_by_id");

            migrationBuilder.RenameColumn(
                name: "al_blue_low_network_user_catalogue_id",
                table: "location_invoices",
                newName: "archived_location_blue_low_network_user_catalogue_id");

            migrationBuilder.RenameColumn(
                name: "al_lp_social_security_number",
                table: "location_invoices",
                newName: "archived_location_title");

            migrationBuilder.RenameColumn(
                name: "al_lp_postal_code",
                table: "location_invoices",
                newName: "archived_location_id");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "representatives",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "representatives",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "postal_code",
                table: "representatives",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "social_security_number",
                table: "representatives",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "meter_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true);
        }
    }
}
