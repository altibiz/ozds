using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class NumericValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0fee_eur",
                table: "network_user_invoices",
                newName: "usage_reactive_energy_total_ramped_t0_fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1peak_fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_power_total_import_t1_peak_fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_energy_total_import_t2_fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_energy_total_import_t1_fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_energy_total_import_t0_fee_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices",
                newName: "supply_active_energy_total_import_t2_fee_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices",
                newName: "supply_active_energy_total_import_t1_fee_eur");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_import_t0_varh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_export_t0_varh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t2_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t1_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_export_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_import_t0_min_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_import_t0_max_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_export_t0_min_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_export_t0_max_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t2_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t2_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t1_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t1_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_export_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_export_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t2_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t2_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t1_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t1_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_export_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_export_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_with_tax_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_rate_percent",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_renewable_energy_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_business_usage_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_reactive_energy_total_ramped_t0_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_power_total_import_t1_peak_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_energy_total_import_t2_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_energy_total_import_t1_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_energy_total_import_t0_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_active_energy_total_import_t2_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_active_energy_total_import_t1_fee_eur",
                table: "network_user_invoices",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "reactive_energy_total_ramped_t0_price_eur",
                table: "network_user_catalogues",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "meter_fee_price_eur",
                table: "network_user_catalogues",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "active_power_total_import_t1_price_eur",
                table: "network_user_catalogues",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "active_energy_total_import_t2_price_eur",
                table: "network_user_catalogues",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "active_energy_total_import_t1_price_eur",
                table: "network_user_catalogues",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "active_energy_total_import_t0_price_eur",
                table: "network_user_catalogues",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "svt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "svt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "mvt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mvt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mnt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mnt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mjt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mjt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "jen_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "jen_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l3_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l3_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l2_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l2_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l1_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l1_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t2_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t1_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_total_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l3_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l3_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l3_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l3_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l2_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l2_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l2_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l2_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l1_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l1_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l1_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "reactive_energy_l1_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l3_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l3_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_l3_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l3_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l3_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_l3_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l2_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l2_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_l2_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l2_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l2_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_l2_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l1_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l1_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_l1_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l1_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_reactive_power_l1_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_reactive_power_l1_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t2_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t2_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t1_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t1_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_total_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l3_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l3_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l3_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l2_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l2_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l2_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l1_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "derived_active_power_l1_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "derived_active_power_l1_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t2_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t2_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t1_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t1_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_total_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l3_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l2_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<long>(
                name: "active_energy_l1_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_fee_eur",
                table: "network_user_invoices",
                newName: "usage_reactive_energy_total_ramped_t0fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_peak_fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_power_total_import_t1peak_fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_energy_total_import_t2fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_energy_total_import_t1fee_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_fee_eur",
                table: "network_user_invoices",
                newName: "usage_active_energy_total_import_t0fee_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2_fee_eur",
                table: "network_user_invoices",
                newName: "supply_active_energy_total_import_t2fee_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1_fee_eur",
                table: "network_user_invoices",
                newName: "supply_active_energy_total_import_t1fee_eur");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_import_t0_varh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_export_t0_varh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t2_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t1_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_export_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_import_t0_min_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_import_t0_max_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_export_t0_min_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_export_t0_max_varh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t2_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t2_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t1_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t1_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_export_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_export_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t2_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t2_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t1_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t1_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_export_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_export_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_with_tax_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_rate_percent",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "tax_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_renewable_energy_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_business_usage_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_reactive_energy_total_ramped_t0fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_power_total_import_t1peak_fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_active_energy_total_import_t0fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_active_energy_total_import_t2fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_active_energy_total_import_t1fee_eur",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "reactive_energy_total_ramped_t0_price_eur",
                table: "network_user_catalogues",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "meter_fee_price_eur",
                table: "network_user_catalogues",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "active_power_total_import_t1_price_eur",
                table: "network_user_catalogues",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "active_energy_total_import_t2_price_eur",
                table: "network_user_catalogues",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "active_energy_total_import_t1_price_eur",
                table: "network_user_catalogues",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "active_energy_total_import_t0_price_eur",
                table: "network_user_catalogues",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "svt_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "svt_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "mvt_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mvt_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mnt_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mnt_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mjt_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mjt_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "jen_total_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "jen_price_eur",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l3_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l3_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l2_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l2_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l1_import_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l1_export_t0_varh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t2_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t1_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_import_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_export_t0_wh",
                table: "abb_b2x_measurements",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_total_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l3_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l3_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l3_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l3_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l2_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l2_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l2_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l2_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l1_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l1_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l1_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "reactive_energy_l1_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l3_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l3_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l3_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l3_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l3_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l3_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l2_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l2_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l2_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l2_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l2_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l2_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l1_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l1_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l1_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l1_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l1_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_reactive_power_l1_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t2_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t2_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t1_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t1_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l3_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l2_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "derived_active_power_l1_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t2_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t2_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t1_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t1_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_total_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l3_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l2_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<float>(
                name: "active_energy_l1_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
