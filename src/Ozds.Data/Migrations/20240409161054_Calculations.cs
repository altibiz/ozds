using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class Calculations : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_meters_catalogues__catalogue_id",
          table: "meters");

      migrationBuilder.DropForeignKey(
          name: "fk_meters_messengers_messenger_id",
          table: "meters");

      migrationBuilder.DropIndex(
          name: "ix_meters__catalogue_id",
          table: "meters");

      migrationBuilder.DropColumn(
          name: "catalogue_id",
          table: "meters");

      migrationBuilder.DropColumn(
          name: "max_active_power_total_import_t1_price_eur",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_import_t0_price_eur",
          table: "catalogues");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_blue_low_catalogue_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_created_by_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_location_created_on",
          table: "network_user_invoices",
          type: "timestamp with time zone",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<string>(
          name: "archived_location_deleted_by_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_location_deleted_on",
          table: "network_user_invoices",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<bool>(
          name: "archived_location_is_deleted",
          table: "network_user_invoices",
          type: "boolean",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_last_updated_by_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_location_last_updated_on",
          table: "network_user_invoices",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_red_low_catalogue_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_regulatory_catalogue_id",
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

      migrationBuilder.AddColumn<string>(
          name: "archived_location_white_medium_catalogue_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_network_user_created_by_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_network_user_created_on",
          table: "network_user_invoices",
          type: "timestamp with time zone",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<string>(
          name: "archived_network_user_deleted_by_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_network_user_deleted_on",
          table: "network_user_invoices",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_network_user_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<bool>(
          name: "archived_network_user_is_deleted",
          table: "network_user_invoices",
          type: "boolean",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "archived_network_user_last_updated_by_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_network_user_last_updated_on",
          table: "network_user_invoices",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_network_user_location_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_network_user_title",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<decimal>(
          name: "tax_eur",
          table: "network_user_invoices",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "total_eur",
          table: "network_user_invoices",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "total_with_tax_eur",
          table: "network_user_invoices",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AlterColumn<string>(
          name: "messenger_id",
          table: "meters",
          type: "text",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "text");

      migrationBuilder.AddColumn<long>(
          name: "catalogue_id",
          table: "measurement_locations",
          type: "bigint",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_blue_low_catalogue_id",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_created_by_id",
          table: "location_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_location_created_on",
          table: "location_invoices",
          type: "timestamp with time zone",
          nullable: false,
          defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

      migrationBuilder.AddColumn<string>(
          name: "archived_location_deleted_by_id",
          table: "location_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_location_deleted_on",
          table: "location_invoices",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_id",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<bool>(
          name: "archived_location_is_deleted",
          table: "location_invoices",
          type: "boolean",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_last_updated_by_id",
          table: "location_invoices",
          type: "text",
          nullable: true);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "archived_location_last_updated_on",
          table: "location_invoices",
          type: "timestamp with time zone",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "archived_location_red_low_catalogue_id",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_regulatory_catalogue_id",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_title",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_white_low_catalogue_id",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<string>(
          name: "archived_location_white_medium_catalogue_id",
          table: "location_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");

      migrationBuilder.AddColumn<decimal>(
          name: "tax_eur",
          table: "location_invoices",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "total_eur",
          table: "location_invoices",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "total_with_tax_eur",
          table: "location_invoices",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AlterColumn<decimal>(
          name: "tax_rate_percent",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "renewable_energy_fee_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "meter_fee_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "business_usage_fee_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t2_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t1_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AlterColumn<decimal>(
          name: "active_energy_total_import_t0_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true,
          oldClrType: typeof(float),
          oldType: "real",
          oldNullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "active_power_total_import_t1_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true);

      migrationBuilder.AddColumn<decimal>(
          name: "reactive_energy_total_ramped_t0_price_eur",
          table: "catalogues",
          type: "numeric",
          nullable: true);

      migrationBuilder.CreateTable(
          name: "network_user_calculations",
          columns: table => new
          {
            id = table.Column<long>(type: "bigint", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
            issued_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            issued_by_id = table.Column<string>(type: "text", nullable: true),
            from_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            to_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            meter_id = table.Column<string>(type: "text", nullable: false),
            measurement_location_id = table.Column<long>(type: "bigint", nullable: false),
            network_user_id = table.Column<long>(type: "bigint", nullable: false),
            total_eur = table.Column<decimal>(type: "numeric", nullable: false),
            title = table.Column<string>(type: "text", nullable: false),
            location_invoice_entity_id = table.Column<long>(type: "bigint", nullable: true),
            regulatory_catalogue_entity_id = table.Column<long>(type: "bigint", nullable: true),
            network_user_invoice_id = table.Column<long>(type: "bigint", nullable: false),
            kind = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
            archived_catalogue_created_by_id = table.Column<string>(type: "text", nullable: true),
            archived_catalogue_created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            archived_catalogue_deleted_by_id = table.Column<string>(type: "text", nullable: true),
            archived_catalogue_deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            archived_catalogue_id = table.Column<string>(type: "text", nullable: false),
            archived_catalogue_is_deleted = table.Column<bool>(type: "boolean", nullable: false),
            archived_catalogue_last_updated_by_id = table.Column<string>(type: "text", nullable: true),
            archived_catalogue_last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            archived_catalogue_title = table.Column<string>(type: "text", nullable: false),
            archived_measurement_location_catalogue_id = table.Column<string>(type: "text", nullable: false),
            archived_measurement_location_created_by_id = table.Column<string>(type: "text", nullable: true),
            archived_measurement_location_created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            archived_measurement_location_deleted_by_id = table.Column<string>(type: "text", nullable: true),
            archived_measurement_location_deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            archived_measurement_location_id = table.Column<string>(type: "text", nullable: false),
            archived_measurement_location_is_deleted = table.Column<bool>(type: "boolean", nullable: false),
            archived_measurement_location_last_updated_by_id = table.Column<string>(type: "text", nullable: true),
            archived_measurement_location_last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            archived_measurement_location_meter_id = table.Column<string>(type: "text", nullable: false),
            archived_measurement_location_network_user_id = table.Column<string>(type: "text", nullable: false),
            archived_measurement_location_title = table.Column<string>(type: "text", nullable: false),
            archived_meter_connection_power_w = table.Column<float>(type: "real", nullable: false),
            archived_meter_created_by_id = table.Column<string>(type: "text", nullable: true),
            archived_meter_created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
            archived_meter_deleted_by_id = table.Column<string>(type: "text", nullable: true),
            archived_meter_deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            archived_meter_id = table.Column<string>(type: "text", nullable: false),
            archived_meter_is_deleted = table.Column<bool>(type: "boolean", nullable: false),
            archived_meter_last_updated_by_id = table.Column<string>(type: "text", nullable: true),
            archived_meter_last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
            archived_meter_messenger_id = table.Column<string>(type: "text", nullable: true),
            archived_meter_phases = table.Column<int[]>(type: "integer[]", nullable: false),
            archived_meter_title = table.Column<string>(type: "text", nullable: false),
            active_energy_total_import_t2_min_wh = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t2_max_wh = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t2_amount_wh = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t2_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t2_total_eur = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_import_t0_min_varh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_import_t0_max_varh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_import_t0_amount_varh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_export_t0_min_varh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_export_t0_max_varh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_export_t0_amount_varh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_ramped_t0amount_va_rh = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_ramped_t0_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            reactive_energy_total_ramped_t0_total_eur = table.Column<decimal>(type: "numeric", nullable: true),
            meter_fee_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            catalogue_id = table.Column<long>(type: "bigint", nullable: true),
            active_energy_total_import_t1_min_wh = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t1_max_wh = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t1_amount_wh = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t1_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            active_energy_total_import_t1_total_eur = table.Column<decimal>(type: "numeric", nullable: true),
            max_active_power_total_import_t1_peak_w = table.Column<decimal>(type: "numeric", nullable: true),
            max_active_power_total_import_t1_amount_w = table.Column<decimal>(type: "numeric", nullable: true),
            max_active_power_total_import_t1_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            max_active_power_total_import_t1_total_eur = table.Column<decimal>(type: "numeric", nullable: true),
            red_low_network_user_calculation_entity_reactive_energy_total_rampe = table.Column<decimal>(name: "red_low_network_user_calculation_entity_reactive_energy_total_rampe~", type: "numeric", nullable: true),
            white_low_network_user_calculation_entity_reactive_energy_total_ram = table.Column<decimal>(name: "white_low_network_user_calculation_entity_reactive_energy_total_ram~", type: "numeric", nullable: true),
            active_power_total_import_t1_peak_w = table.Column<decimal>(type: "numeric", nullable: true),
            active_power_total_import_t1_amount_w = table.Column<decimal>(type: "numeric", nullable: true),
            active_power_total_import_t1_price_eur = table.Column<decimal>(type: "numeric", nullable: true),
            active_power_total_import_t1_total_eur = table.Column<decimal>(type: "numeric", nullable: true),
            white_medium_network_user_calculation_entity_reactive_energy_total_ = table.Column<decimal>(name: "white_medium_network_user_calculation_entity_reactive_energy_total_~", type: "numeric", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_network_user_calculations", x => x.id);
            table.ForeignKey(
                      name: "fk_network_user_calculations_catalogues__catalogue_id",
                      column: x => x.catalogue_id,
                      principalTable: "catalogues",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "fk_network_user_calculations_catalogues_regulatory_catalogue_e",
                      column: x => x.regulatory_catalogue_entity_id,
                      principalTable: "catalogues",
                      principalColumn: "id");
            table.ForeignKey(
                      name: "fk_network_user_calculations_location_invoices_location_invoic",
                      column: x => x.location_invoice_entity_id,
                      principalTable: "location_invoices",
                      principalColumn: "id");
            table.ForeignKey(
                      name: "fk_network_user_calculations_measurement_locations__measuremen",
                      column: x => x.measurement_location_id,
                      principalTable: "measurement_locations",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "fk_network_user_calculations_meters_meter_id",
                      column: x => x.meter_id,
                      principalTable: "meters",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "fk_network_user_calculations_network_user_invoices__network_us",
                      column: x => x.network_user_id,
                      principalTable: "network_user_invoices",
                      principalColumn: "id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "fk_network_user_calculations_representatives_issued_by_id",
                      column: x => x.issued_by_id,
                      principalTable: "representatives",
                      principalColumn: "id");
          });

      migrationBuilder.CreateIndex(
          name: "ix_measurement_locations_catalogue_id",
          table: "measurement_locations",
          column: "catalogue_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations__catalogue_id",
          table: "network_user_calculations",
          column: "catalogue_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations__measurement_location_id",
          table: "network_user_calculations",
          column: "measurement_location_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations__network_user_id",
          table: "network_user_calculations",
          column: "network_user_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations_issued_by_id",
          table: "network_user_calculations",
          column: "issued_by_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations_location_invoice_entity_id",
          table: "network_user_calculations",
          column: "location_invoice_entity_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations_meter_id",
          table: "network_user_calculations",
          column: "meter_id");

      migrationBuilder.CreateIndex(
          name: "ix_network_user_calculations_regulatory_catalogue_entity_id",
          table: "network_user_calculations",
          column: "regulatory_catalogue_entity_id");

      migrationBuilder.AddForeignKey(
          name: "fk_measurement_locations_catalogues_catalogue_id",
          table: "measurement_locations",
          column: "catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_meters_messengers_messenger_id",
          table: "meters",
          column: "messenger_id",
          principalTable: "messengers",
          principalColumn: "id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_measurement_locations_catalogues_catalogue_id",
          table: "measurement_locations");

      migrationBuilder.DropForeignKey(
          name: "fk_meters_messengers_messenger_id",
          table: "meters");

      migrationBuilder.DropTable(
          name: "network_user_calculations");

      migrationBuilder.DropIndex(
          name: "ix_measurement_locations_catalogue_id",
          table: "measurement_locations");

      migrationBuilder.DropColumn(
          name: "archived_location_blue_low_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_created_by_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_created_on",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_deleted_by_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_deleted_on",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_is_deleted",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_last_updated_by_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_last_updated_on",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_red_low_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_regulatory_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_title",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_white_low_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_white_medium_catalogue_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_created_by_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_created_on",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_deleted_by_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_deleted_on",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_is_deleted",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_last_updated_by_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_last_updated_on",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_location_id",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "archived_network_user_title",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "tax_eur",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "total_eur",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "total_with_tax_eur",
          table: "network_user_invoices");

      migrationBuilder.DropColumn(
          name: "catalogue_id",
          table: "measurement_locations");

      migrationBuilder.DropColumn(
          name: "archived_location_blue_low_catalogue_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_created_by_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_created_on",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_deleted_by_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_deleted_on",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_is_deleted",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_last_updated_by_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_last_updated_on",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_red_low_catalogue_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_regulatory_catalogue_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_title",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_white_low_catalogue_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "archived_location_white_medium_catalogue_id",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "tax_eur",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "total_eur",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "total_with_tax_eur",
          table: "location_invoices");

      migrationBuilder.DropColumn(
          name: "active_power_total_import_t1_price_eur",
          table: "catalogues");

      migrationBuilder.DropColumn(
          name: "reactive_energy_total_ramped_t0_price_eur",
          table: "catalogues");

      migrationBuilder.AlterColumn<string>(
          name: "messenger_id",
          table: "meters",
          type: "text",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "text",
          oldNullable: true);

      migrationBuilder.AddColumn<long>(
          name: "catalogue_id",
          table: "meters",
          type: "bigint",
          nullable: false,
          defaultValue: 0L);

      migrationBuilder.AlterColumn<float>(
          name: "tax_rate_percent",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<float>(
          name: "renewable_energy_fee_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<float>(
          name: "meter_fee_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<float>(
          name: "business_usage_fee_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<float>(
          name: "active_energy_total_import_t2_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<float>(
          name: "active_energy_total_import_t1_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AlterColumn<float>(
          name: "active_energy_total_import_t0_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true,
          oldClrType: typeof(decimal),
          oldType: "numeric",
          oldNullable: true);

      migrationBuilder.AddColumn<float>(
          name: "max_active_power_total_import_t1_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true);

      migrationBuilder.AddColumn<float>(
          name: "reactive_energy_total_import_t0_price_eur",
          table: "catalogues",
          type: "real",
          nullable: true);

      migrationBuilder.CreateIndex(
          name: "ix_meters__catalogue_id",
          table: "meters",
          column: "catalogue_id");

      migrationBuilder.AddForeignKey(
          name: "fk_meters_catalogues__catalogue_id",
          table: "meters",
          column: "catalogue_id",
          principalTable: "catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_meters_messengers_messenger_id",
          table: "meters",
          column: "messenger_id",
          principalTable: "messengers",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
