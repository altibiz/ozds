using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class Calculations2 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_catalogues_representatives_created_by_id",
          table: "catalogues");

      migrationBuilder.DropForeignKey(
          name: "fk_catalogues_representatives_deleted_by_id",
          table: "catalogues");

      migrationBuilder.DropForeignKey(
          name: "fk_catalogues_representatives_last_updated_by_id",
          table: "catalogues");

      migrationBuilder.DropForeignKey(
          name: "fk_locations_catalogues_blue_low_catalogue_id",
          table: "locations");

      migrationBuilder.DropForeignKey(
          name: "fk_locations_catalogues_red_low_catalogue_id",
          table: "locations");

      migrationBuilder.DropForeignKey(
          name: "fk_locations_catalogues_regulatory_catalogue_id",
          table: "locations");

      migrationBuilder.DropForeignKey(
          name: "fk_locations_catalogues_white_low_catalogue_id",
          table: "locations");

      migrationBuilder.DropForeignKey(
          name: "fk_locations_catalogues_white_medium_catalogue_id",
          table: "locations");

      migrationBuilder.DropForeignKey(
          name: "fk_measurement_locations_catalogues_catalogue_id",
          table: "measurement_locations");

      migrationBuilder.DropForeignKey(
          name: "fk_network_user_calculations_catalogues__catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.DropForeignKey(
          name: "fk_network_user_calculations_catalogues_regulatory_catalogue_e",
          table: "network_user_calculations");

      migrationBuilder.DropIndex(
          name: "ix_network_user_calculations__catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.DropPrimaryKey(
          name: "pk_catalogues",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "archived_location_blue_low_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_red_low_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_title",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_white_low_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_catalogue_title",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_measurement_location_catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_measurement_location_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_measurement_location_network_user_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_measurement_location_title",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_meter_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "archived_meter_title",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "max_active_power_total_import_t1_amount_w",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "max_active_power_total_import_t1_peak_w",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "max_active_power_total_import_t1_price_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "max_active_power_total_import_t1_total_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_export_t0_amount_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_export_t0_max_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_export_t0_min_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_import_t0_amount_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_import_t0_max_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_import_t0_min_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_price_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_total_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0amount_va_rh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "red_low_network_user_calculation_entity_reactive_energy_total_rampe~",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "white_low_network_user_calculation_entity_reactive_energy_total_ram~",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "white_medium_network_user_calculation_entity_reactive_energy_total_~",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "active_energy_total_import_t0_price_eur",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "active_power_total_import_t1_price_eur",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "kind",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "meter_fee_price_eur",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_price_eur",
          table: "catalogues");

      migrationBuilder.RenameTable(
          name: "catalogues",
          newName: "regulatory_catalogues");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_last_updated_on",
          table: "network_user_invoices",
          newName: "anu_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_last_updated_by_id",
          table: "network_user_invoices",
          newName: "anu_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_is_deleted",
          table: "network_user_invoices",
          newName: "anu_is_deleted");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_deleted_on",
          table: "network_user_invoices",
          newName: "anu_deleted_on");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_deleted_by_id",
          table: "network_user_invoices",
          newName: "anu_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_created_on",
          table: "network_user_invoices",
          newName: "anu_created_on");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_created_by_id",
          table: "network_user_invoices",
          newName: "anu_created_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_regulatory_catalogue_id",
          table: "network_user_invoices",
          newName: "al_regulatory_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_last_updated_on",
          table: "network_user_invoices",
          newName: "al_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "archived_location_last_updated_by_id",
          table: "network_user_invoices",
          newName: "al_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_is_deleted",
          table: "network_user_invoices",
          newName: "al_is_deleted");

      migrationBuilder.RenameColumn(
          name: "archived_location_deleted_on",
          table: "network_user_invoices",
          newName: "al_deleted_on");

      migrationBuilder.RenameColumn(
          name: "archived_location_deleted_by_id",
          table: "network_user_invoices",
          newName: "al_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_created_on",
          table: "network_user_invoices",
          newName: "al_created_on");

      migrationBuilder.RenameColumn(
          name: "archived_location_created_by_id",
          table: "network_user_invoices",
          newName: "al_created_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_title",
          table: "network_user_invoices",
          newName: "al_white_medium_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_location_id",
          table: "network_user_invoices",
          newName: "al_white_low_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_network_user_id",
          table: "network_user_invoices",
          newName: "al_red_low_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_white_medium_catalogue_id",
          table: "network_user_invoices",
          newName: "al_blue_low_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_meter_phases",
          table: "network_user_calculations",
          newName: "am_phases");

      migrationBuilder.RenameColumn(
          name: "archived_meter_messenger_id",
          table: "network_user_calculations",
          newName: "am_messenger_id");

      migrationBuilder.RenameColumn(
          name: "archived_meter_last_updated_on",
          table: "network_user_calculations",
          newName: "am_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "archived_meter_last_updated_by_id",
          table: "network_user_calculations",
          newName: "am_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_meter_is_deleted",
          table: "network_user_calculations",
          newName: "am_is_deleted");

      migrationBuilder.RenameColumn(
          name: "archived_meter_deleted_on",
          table: "network_user_calculations",
          newName: "am_deleted_on");

      migrationBuilder.RenameColumn(
          name: "archived_meter_deleted_by_id",
          table: "network_user_calculations",
          newName: "am_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_meter_created_on",
          table: "network_user_calculations",
          newName: "am_created_on");

      migrationBuilder.RenameColumn(
          name: "archived_meter_created_by_id",
          table: "network_user_calculations",
          newName: "am_created_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_meter_connection_power_w",
          table: "network_user_calculations",
          newName: "am_connection_power__w");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_meter_id",
          table: "network_user_calculations",
          newName: "aml_meter_id");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_last_updated_on",
          table: "network_user_calculations",
          newName: "aml_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_last_updated_by_id",
          table: "network_user_calculations",
          newName: "aml_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_is_deleted",
          table: "network_user_calculations",
          newName: "aml_is_deleted");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_deleted_on",
          table: "network_user_calculations",
          newName: "aml_deleted_on");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_deleted_by_id",
          table: "network_user_calculations",
          newName: "aml_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_created_on",
          table: "network_user_calculations",
          newName: "aml_created_on");

      migrationBuilder.RenameColumn(
          name: "archived_measurement_location_created_by_id",
          table: "network_user_calculations",
          newName: "aml_created_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_last_updated_on",
          table: "network_user_calculations",
          newName: "aunuc_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_last_updated_by_id",
          table: "network_user_calculations",
          newName: "aunuc_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_is_deleted",
          table: "network_user_calculations",
          newName: "aunuc_is_deleted");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_deleted_on",
          table: "network_user_calculations",
          newName: "aunuc_deleted_on");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_deleted_by_id",
          table: "network_user_calculations",
          newName: "aunuc_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_created_on",
          table: "network_user_calculations",
          newName: "aunuc_created_on");

      migrationBuilder.RenameColumn(
          name: "archived_catalogue_created_by_id",
          table: "network_user_calculations",
          newName: "aunuc_created_by_id");

      migrationBuilder.RenameColumn(
          name: "regulatory_catalogue_entity_id",
          table: "network_user_calculations",
          newName: "usage_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_network_user_calculations_regulatory_catalogue_entity_id",
          table: "network_user_calculations",
          newName: "ix_network_user_calculations__usage_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__white_medium_catalogue_id",
          table: "locations",
          newName: "ix_locations__white_medium_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__white_low_catalogue_id",
          table: "locations",
          newName: "ix_locations__white_low_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__regulatory_catalogue_id",
          table: "locations",
          newName: "ix_locations__regulatory_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__red_low_catalogue_id",
          table: "locations",
          newName: "ix_locations__red_low_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__blue_low_catalogue_id",
          table: "locations",
          newName: "ix_locations__blue_low_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_white_medium_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_white_medium_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_white_low_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_white_low_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_red_low_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_red_low_network_user_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_blue_low_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_blue_low_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_catalogues_last_updated_by_id",
          table: "regulatory_catalogues",
          newName: "ix_regulatory_catalogues_last_updated_by_id");

      migrationBuilder.RenameIndex(
          name: "ix_catalogues_deleted_by_id",
          table: "regulatory_catalogues",
          newName: "ix_regulatory_catalogues_deleted_by_id");

      migrationBuilder.RenameIndex(
          name: "ix_catalogues_created_by_id",
          table: "regulatory_catalogues",
          newName: "ix_regulatory_catalogues_created_by_id");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_peak_w",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_amount_w",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_min_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_max_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_min_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_max_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "active_energy_total_import_t0_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "active_energy_total_import_t0_max_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "active_energy_total_import_t0_min_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "active_energy_total_import_t0_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "active_energy_total_import_t0_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "asrc_active_energy_total_import_t1_price__e_u_r",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "asrc_active_energy_total_import_t2_price__e_u_r",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "asrc_business_usage_fee_price__e_u_r",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<string>(
          name: "asrc_created_by_id",
          table: "network_user_calculations",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "asrc_created_on",
          table: "network_user_calculations",
          type: "timestamp with time zone",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<string>(
          name: "asrc_deleted_by_id",
          table: "network_user_calculations",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "asrc_deleted_on",
          table: "network_user_calculations",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<bool>(
          name: "asrc_is_deleted",
          table: "network_user_calculations",
          type: "boolean",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "asrc_last_updated_by_id",
          table: "network_user_calculations",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "asrc_last_updated_on",
          table: "network_user_calculations",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "asrc_renewable_energy_fee_price__e_u_r",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "asrc_tax_rate__percent",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_export_amount_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_export_max_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_export_min_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_import_amount_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_import_max_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_import_min_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_ramped_amount_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_ramped_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_ramped_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<long>(
          name: "supply_regulatory_catalogue_id",
          table: "network_user_calculations",
          type: "bigint",
          nullable: false,
          defaultValue: 0L);

      migrationBuilder.AlterColumn<decimal>(
          name: "tax_rate_percent",
          table: "regulatory_catalogues",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "renewable_energy_fee_price_eur",
          table: "regulatory_catalogues",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "business_usage_fee_price_eur",
          table: "regulatory_catalogues",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_price_eur",
          table: "regulatory_catalogues",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_price_eur",
          table: "regulatory_catalogues",
          type: "numeric",
          nullable: false,
          defaultValue: 0m,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AddPrimaryKey(
          name: "pk_regulatory_catalogues",
          table: "regulatory_catalogues",
          column: "id");

      migrationBuilder.CreateTable(
          name: "network_user_catalogues",
          columns: table => new
          {
            id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
            kind = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
            active_energy_total_import_t0_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_ramped_t0_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            meter_fee_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t1_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t2_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            active_power_total_import_t1_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            created_by_id = table.Column<string>(type: "text", nullable: true),
            last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            last_updated_by_id = table.Column<string>(type: "text", nullable: true),
            is_deleted = table.Column<bool>(type: "boolean", nullable: false),
            deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            deleted_by_id = table.Column<string>(type: "text", nullable: true),
            title = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_network_user_catalogues", x => x.id);
            table.ForeignKey(
                      name: "fk_network_user_catalogues_representatives_created_by_id",
                      column: x => x.created_by_id,
                      principalTable: "representatives",
                      principalColumn: "id");
            table.ForeignKey(
                      name: "fk_network_user_catalogues_representatives_deleted_by_id",
                      column: x => x.deleted_by_id,
                      principalTable: "representatives",
                      principalColumn: "id");
            table.ForeignKey(
                      name: "fk_network_user_catalogues_representatives_last_updated_by_id",
                      column: x => x.last_updated_by_id,
                      principalTable: "representatives",
                      principalColumn: "id");
          });

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations__supply_regulatory_catalogue_id",
          table: "network_user_calculations",
          column: "supply_regulatory_catalogue_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_catalogues_created_by_id",
          table: "network_user_catalogues",
          column: "created_by_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_catalogues_deleted_by_id",
          table: "network_user_catalogues",
          column: "deleted_by_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_catalogues_last_updated_by_id",
          table: "network_user_catalogues",
          column: "last_updated_by_id");

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
          name: "fk_measurement_locations_network_user_catalogues_catalogue_id",
          table: "measurement_locations",
          column: "catalogue_id",
          principalTable: "network_user_catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_network_user_calculations_network_user_catalogues__usage_ne",
          table: "network_user_calculations",
          column: "usage_network_user_catalogue_id",
          principalTable: "network_user_catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_network_user_calculations_regulatory_catalogues__supply_reg",
          table: "network_user_calculations",
          column: "supply_regulatory_catalogue_id",
          principalTable: "regulatory_catalogues",
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
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
          name: "fk_measurement_locations_network_user_catalogues_catalogue_id",
          table: "measurement_locations");

      migrationBuilder.DropForeignKey(
          name: "fk_network_user_calculations_network_user_catalogues__usage_ne",
          table: "network_user_calculations");

      migrationBuilder.DropForeignKey(
          name: "fk_network_user_calculations_regulatory_catalogues__supply_reg",
          table: "network_user_calculations");

      migrationBuilder.DropForeignKey(
          name: "fk_regulatory_catalogues_representatives_created_by_id",
          table: "regulatory_catalogues");

      migrationBuilder.DropForeignKey(
          name: "fk_regulatory_catalogues_representatives_deleted_by_id",
          table: "regulatory_catalogues");

      migrationBuilder.DropForeignKey(
          name: "fk_regulatory_catalogues_representatives_last_updated_by_id",
          table: "regulatory_catalogues");

      migrationBuilder.DropTable(
          name: "network_user_catalogues");

      migrationBuilder.DropIndex(
          name: "ix_network_user_calculations__supply_regulatory_catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.DropPrimaryKey(
          name: "pk_regulatory_catalogues",
          table: "regulatory_catalogues");

      migrationBuilder.DropColumn(
          name: "active_energy_total_import_t0_amount_wh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "active_energy_total_import_t0_max_wh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "active_energy_total_import_t0_min_wh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "active_energy_total_import_t0_price_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "active_energy_total_import_t0_total_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_active_energy_total_import_t1_price__e_u_r",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_active_energy_total_import_t2_price__e_u_r",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_business_usage_fee_price__e_u_r",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_created_by_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_created_on",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_deleted_by_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_deleted_on",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_is_deleted",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_last_updated_by_id",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_last_updated_on",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_renewable_energy_fee_price__e_u_r",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "asrc_tax_rate__percent",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_export_amount_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_export_max_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_export_min_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_import_amount_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_import_max_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_import_min_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_ramped_amount_varh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_ramped_price_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_ramped_total_eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "supply_regulatory_catalogue_id",
          table: "network_user_calculations");

      migrationBuilder.RenameTable(
          name: "regulatory_catalogues",
          newName: "catalogues");

      migrationBuilder.RenameColumn(
          name: "anu_last_updated_on",
          table: "network_user_invoices",
          newName: "archived_network_user_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "anu_last_updated_by_id",
          table: "network_user_invoices",
          newName: "archived_network_user_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "anu_is_deleted",
          table: "network_user_invoices",
          newName: "archived_network_user_is_deleted");

      migrationBuilder.RenameColumn(
          name: "anu_deleted_on",
          table: "network_user_invoices",
          newName: "archived_network_user_deleted_on");

      migrationBuilder.RenameColumn(
          name: "anu_deleted_by_id",
          table: "network_user_invoices",
          newName: "archived_network_user_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "anu_created_on",
          table: "network_user_invoices",
          newName: "archived_network_user_created_on");

      migrationBuilder.RenameColumn(
          name: "anu_created_by_id",
          table: "network_user_invoices",
          newName: "archived_network_user_created_by_id");

      migrationBuilder.RenameColumn(
          name: "al_regulatory_catalogue_id",
          table: "network_user_invoices",
          newName: "archived_location_regulatory_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "al_last_updated_on",
          table: "network_user_invoices",
          newName: "archived_location_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "al_last_updated_by_id",
          table: "network_user_invoices",
          newName: "archived_location_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "al_is_deleted",
          table: "network_user_invoices",
          newName: "archived_location_is_deleted");

      migrationBuilder.RenameColumn(
          name: "al_deleted_on",
          table: "network_user_invoices",
          newName: "archived_location_deleted_on");

      migrationBuilder.RenameColumn(
          name: "al_deleted_by_id",
          table: "network_user_invoices",
          newName: "archived_location_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "al_created_on",
          table: "network_user_invoices",
          newName: "archived_location_created_on");

      migrationBuilder.RenameColumn(
          name: "al_created_by_id",
          table: "network_user_invoices",
          newName: "archived_location_created_by_id");

      migrationBuilder.RenameColumn(
          name: "al_white_medium_network_user_catalogue_id",
          table: "network_user_invoices",
          newName: "archived_network_user_title");

      migrationBuilder.RenameColumn(
          name: "al_white_low_network_user_catalogue_id",
          table: "network_user_invoices",
          newName: "archived_network_user_location_id");

      migrationBuilder.RenameColumn(
          name: "al_red_low_network_user_catalogue_id",
          table: "network_user_invoices",
          newName: "archived_network_user_id");

      migrationBuilder.RenameColumn(
          name: "al_blue_low_network_user_catalogue_id",
          table: "network_user_invoices",
          newName: "archived_location_white_medium_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "aml_meter_id",
          table: "network_user_calculations",
          newName: "archived_measurement_location_meter_id");

      migrationBuilder.RenameColumn(
          name: "aml_last_updated_on",
          table: "network_user_calculations",
          newName: "archived_measurement_location_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "aml_last_updated_by_id",
          table: "network_user_calculations",
          newName: "archived_measurement_location_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "aml_is_deleted",
          table: "network_user_calculations",
          newName: "archived_measurement_location_is_deleted");

      migrationBuilder.RenameColumn(
          name: "aml_deleted_on",
          table: "network_user_calculations",
          newName: "archived_measurement_location_deleted_on");

      migrationBuilder.RenameColumn(
          name: "aml_deleted_by_id",
          table: "network_user_calculations",
          newName: "archived_measurement_location_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "aml_created_on",
          table: "network_user_calculations",
          newName: "archived_measurement_location_created_on");

      migrationBuilder.RenameColumn(
          name: "aml_created_by_id",
          table: "network_user_calculations",
          newName: "archived_measurement_location_created_by_id");

      migrationBuilder.RenameColumn(
          name: "am_phases",
          table: "network_user_calculations",
          newName: "archived_meter_phases");

      migrationBuilder.RenameColumn(
          name: "am_messenger_id",
          table: "network_user_calculations",
          newName: "archived_meter_messenger_id");

      migrationBuilder.RenameColumn(
          name: "am_last_updated_on",
          table: "network_user_calculations",
          newName: "archived_meter_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "am_last_updated_by_id",
          table: "network_user_calculations",
          newName: "archived_meter_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "am_is_deleted",
          table: "network_user_calculations",
          newName: "archived_meter_is_deleted");

      migrationBuilder.RenameColumn(
          name: "am_deleted_on",
          table: "network_user_calculations",
          newName: "archived_meter_deleted_on");

      migrationBuilder.RenameColumn(
          name: "am_deleted_by_id",
          table: "network_user_calculations",
          newName: "archived_meter_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "am_created_on",
          table: "network_user_calculations",
          newName: "archived_meter_created_on");

      migrationBuilder.RenameColumn(
          name: "am_created_by_id",
          table: "network_user_calculations",
          newName: "archived_meter_created_by_id");

      migrationBuilder.RenameColumn(
          name: "am_connection_power__w",
          table: "network_user_calculations",
          newName: "archived_meter_connection_power_w");

      migrationBuilder.RenameColumn(
          name: "aunuc_last_updated_on",
          table: "network_user_calculations",
          newName: "archived_catalogue_last_updated_on");

      migrationBuilder.RenameColumn(
          name: "aunuc_last_updated_by_id",
          table: "network_user_calculations",
          newName: "archived_catalogue_last_updated_by_id");

      migrationBuilder.RenameColumn(
          name: "aunuc_is_deleted",
          table: "network_user_calculations",
          newName: "archived_catalogue_is_deleted");

      migrationBuilder.RenameColumn(
          name: "aunuc_deleted_on",
          table: "network_user_calculations",
          newName: "archived_catalogue_deleted_on");

      migrationBuilder.RenameColumn(
          name: "aunuc_deleted_by_id",
          table: "network_user_calculations",
          newName: "archived_catalogue_deleted_by_id");

      migrationBuilder.RenameColumn(
          name: "aunuc_created_on",
          table: "network_user_calculations",
          newName: "archived_catalogue_created_on");

      migrationBuilder.RenameColumn(
          name: "aunuc_created_by_id",
          table: "network_user_calculations",
          newName: "archived_catalogue_created_by_id");

      migrationBuilder.RenameColumn(
          name: "usage_network_user_catalogue_id",
          table: "network_user_calculations",
          newName: "regulatory_catalogue_entity_id");

      migrationBuilder.RenameIndex(
          name: "ix_network_user_calculations__usage_network_user_catalogue_id",
          table: "network_user_calculations",
          newName: "ix_network_user_calculations_regulatory_catalogue_entity_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__white_medium_network_user_catalogue_id",
          table: "locations",
          newName: "ix_locations__white_medium_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__white_low_network_user_catalogue_id",
          table: "locations",
          newName: "ix_locations__white_low_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__regulatory_network_user_catalogue_id",
          table: "locations",
          newName: "ix_locations__regulatory_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__red_low_network_user_catalogue_id",
          table: "locations",
          newName: "ix_locations__red_low_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_locations__blue_low_network_user_catalogue_id",
          table: "locations",
          newName: "ix_locations__blue_low_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_white_medium_network_user_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_white_medium_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_white_low_network_user_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_white_low_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_red_low_network_user_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_red_low_catalogue_id");

      migrationBuilder.RenameColumn(
          name: "archived_location_blue_low_network_user_catalogue_id",
          table: "location_invoices",
          newName: "archived_location_blue_low_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_regulatory_catalogues_last_updated_by_id",
          table: "catalogues",
          newName: "ix_catalogues_last_updated_by_id");

      migrationBuilder.RenameIndex(
          name: "ix_regulatory_catalogues_deleted_by_id",
          table: "catalogues",
          newName: "ix_catalogues_deleted_by_id");

      migrationBuilder.RenameIndex(
          name: "ix_regulatory_catalogues_created_by_id",
          table: "catalogues",
          newName: "ix_catalogues_created_by_id");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_blue_low_catalogue_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_red_low_catalogue_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_title",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_white_low_catalogue_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_peak_w",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_power_total_import_t1_amount_w",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_min_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_max_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_min_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_max_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AddColumn<string>(
          name: "archived_catalogue_id",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_catalogue_title",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_measurement_location_catalogue_id",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_measurement_location_id",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_measurement_location_network_user_id",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_measurement_location_title",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_meter_id",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_meter_title",
          table: "network_user_calculations",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<long>(
          name: "catalogue_id",
          table: "network_user_calculations",
          type: "bigint",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "max_active_power_total_import_t1_amount_w",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "max_active_power_total_import_t1_peak_w",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "max_active_power_total_import_t1_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "max_active_power_total_import_t1_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_export_t0_amount_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_export_t0_max_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_export_t0_min_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_import_t0_amount_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_import_t0_max_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_import_t0_min_varh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_price_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_total_eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0amount_va_rh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "red_low_network_user_calculation_entity_reactive_energy_total_rampe~",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "white_low_network_user_calculation_entity_reactive_energy_total_ram~",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "white_medium_network_user_calculation_entity_reactive_energy_total_~",
          table: "network_user_calculations",
          type: "numeric",
          nullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "tax_rate_percent",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "renewable_energy_fee_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "business_usage_fee_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric");

      migrationBuilder.AddColumn<decimal>(
          name: "active_energy_total_import_t0_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "active_power_total_import_t1_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "kind",
          table: "catalogues",
          type: "character varying(34)",
          maxLength: 34,
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<decimal>(
          name: "meter_fee_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddPrimaryKey(
          name: "pk_catalogues",
          table: "catalogues",
          column: "id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations__catalogue_id",
          table: "network_user_calculations",
          column: "catalogue_id");

      migrationBuilder.AddForeignKey(
          name: "fk_catalogues_representatives_created_by_id",
          table: "catalogues",
          column: "created_by_id",
          principalTable: "representatives",
          principalColumn: "id");

      migrationBuilder.AddForeignKey(
          name: "fk_catalogues_representatives_deleted_by_id",
          table: "catalogues",
          column: "deleted_by_id",
          principalTable: "representatives",
          principalColumn: "id");

      migrationBuilder.AddForeignKey(
          name: "fk_catalogues_representatives_last_updated_by_id",
          table: "catalogues",
          column: "last_updated_by_id",
          principalTable: "representatives",
          principalColumn: "id");

      migrationBuilder.AddForeignKey(
          name: "fk_locations_catalogues_blue_low_catalogue_id",
          table: "locations",
          column: "blue_low_catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_locations_catalogues_red_low_catalogue_id",
          table: "locations",
          column: "red_low_catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_locations_catalogues_regulatory_catalogue_id",
          table: "locations",
          column: "regulatory_catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_locations_catalogues_white_low_catalogue_id",
          table: "locations",
          column: "white_low_catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_locations_catalogues_white_medium_catalogue_id",
          table: "locations",
          column: "white_medium_catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_measurement_locations_catalogues_catalogue_id",
          table: "measurement_locations",
          column: "catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_network_user_calculations_catalogues__catalogue_id",
          table: "network_user_calculations",
          column: "catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_network_user_calculations_catalogues_regulatory_catalogue_e",
          table: "network_user_calculations",
          column: "regulatory_catalogue_entity_id",
          principalTable: "catalogues",
          principalColumn: "id");
    }
  }
}
