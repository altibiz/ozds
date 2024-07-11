using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class KiloCalculations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_import_min_varh",
                table: "network_user_calculations",
                newName: "jen_reactive_import_min_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_import_max_varh",
                table: "network_user_calculations",
                newName: "jen_reactive_import_max_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_import_amount_varh",
                table: "network_user_calculations",
                newName: "jen_reactive_import_amount_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_export_min_varh",
                table: "network_user_calculations",
                newName: "jen_reactive_export_min_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_export_max_varh",
                table: "network_user_calculations",
                newName: "jen_reactive_export_max_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_export_amount_varh",
                table: "network_user_calculations",
                newName: "jen_reactive_export_amount_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_ramped_total_eur",
                table: "network_user_calculations",
                newName: "jen_ramped_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_ramped_price_eur",
                table: "network_user_calculations",
                newName: "jen_ramped_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_ramped_amount_varh",
                table: "network_user_calculations",
                newName: "jen_ramped_amount_kvarh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_active_import_min_wh",
                table: "network_user_calculations",
                newName: "jen_active_import_min_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_active_import_max_wh",
                table: "network_user_calculations",
                newName: "jen_active_import_max_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_active_import_amount_wh",
                table: "network_user_calculations",
                newName: "jen_active_import_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "svt_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "svt_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_peak_w",
                table: "network_user_calculations",
                newName: "svt_peak_kw");

            migrationBuilder.RenameColumn(
                name: "usage_active_power_total_import_t1_amount_w",
                table: "network_user_calculations",
                newName: "svt_amount_kw");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_total_eur",
                table: "network_user_calculations",
                newName: "mnt_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_price_eur",
                table: "network_user_calculations",
                newName: "mnt_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_min_wh",
                table: "network_user_calculations",
                newName: "mnt_min_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_max_wh",
                table: "network_user_calculations",
                newName: "mnt_max_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t2_amount_wh",
                table: "network_user_calculations",
                newName: "mnt_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "mvt_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "mvt_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_min_wh",
                table: "network_user_calculations",
                newName: "mvt_min_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_max_wh",
                table: "network_user_calculations",
                newName: "mvt_max_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t1_amount_wh",
                table: "network_user_calculations",
                newName: "mvt_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_total_eur",
                table: "network_user_calculations",
                newName: "mjt_total_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_price_eur",
                table: "network_user_calculations",
                newName: "mjt_price_eur");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_min_wh",
                table: "network_user_calculations",
                newName: "mjt_min_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_max_wh",
                table: "network_user_calculations",
                newName: "mjt_max_kwh");

            migrationBuilder.RenameColumn(
                name: "usage_active_energy_total_import_t0_amount_wh",
                table: "network_user_calculations",
                newName: "mjt_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_renewable_energy_fee_active_energy_total_import_t0_total_eur",
                table: "network_user_calculations",
                newName: "oie_total_eur");

            migrationBuilder.RenameColumn(
                name: "supply_renewable_energy_fee_active_energy_total_import_t0_price_eur",
                table: "network_user_calculations",
                newName: "oie_price_eur");

            migrationBuilder.RenameColumn(
                name: "supply_renewable_energy_fee_active_energy_total_import_t0_min_wh",
                table: "network_user_calculations",
                newName: "oie_min_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_renewable_energy_fee_active_energy_total_import_t0_max_wh",
                table: "network_user_calculations",
                newName: "oie_max_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_renewable_energy_fee_active_energy_total_import_t0_amount_wh",
                table: "network_user_calculations",
                newName: "oie_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_business_usage_fee_active_energy_total_import_t0_total_eur",
                table: "network_user_calculations",
                newName: "trp_total_eur");

            migrationBuilder.RenameColumn(
                name: "supply_business_usage_fee_active_energy_total_import_t0_price_eur",
                table: "network_user_calculations",
                newName: "trp_price_eur");

            migrationBuilder.RenameColumn(
                name: "supply_business_usage_fee_active_energy_total_import_t0_min_wh",
                table: "network_user_calculations",
                newName: "trp_min_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_business_usage_fee_active_energy_total_import_t0_max_wh",
                table: "network_user_calculations",
                newName: "trp_max_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_business_usage_fee_active_energy_total_import_t0_amount_wh",
                table: "network_user_calculations",
                newName: "trp_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2_total_eur",
                table: "network_user_calculations",
                newName: "rnt_total_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2_price_eur",
                table: "network_user_calculations",
                newName: "rnt_price_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2_min_wh",
                table: "network_user_calculations",
                newName: "rnt_min_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2_max_wh",
                table: "network_user_calculations",
                newName: "rnt_max_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t2_amount_wh",
                table: "network_user_calculations",
                newName: "rnt_amount_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1_total_eur",
                table: "network_user_calculations",
                newName: "rvt_total_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1_price_eur",
                table: "network_user_calculations",
                newName: "rvt_price_eur");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1_min_wh",
                table: "network_user_calculations",
                newName: "rvt_min_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1_max_wh",
                table: "network_user_calculations",
                newName: "rvt_max_kwh");

            migrationBuilder.RenameColumn(
                name: "supply_active_energy_total_import_t1_amount_wh",
                table: "network_user_calculations",
                newName: "rvt_amount_kwh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "trp_total_eur",
                table: "network_user_calculations",
                newName: "supply_business_usage_fee_active_energy_total_import_t0_total_eur");

            migrationBuilder.RenameColumn(
                name: "trp_price_eur",
                table: "network_user_calculations",
                newName: "supply_business_usage_fee_active_energy_total_import_t0_price_eur");

            migrationBuilder.RenameColumn(
                name: "trp_min_kwh",
                table: "network_user_calculations",
                newName: "supply_business_usage_fee_active_energy_total_import_t0_min_wh");

            migrationBuilder.RenameColumn(
                name: "trp_max_kwh",
                table: "network_user_calculations",
                newName: "supply_business_usage_fee_active_energy_total_import_t0_max_wh");

            migrationBuilder.RenameColumn(
                name: "trp_amount_kwh",
                table: "network_user_calculations",
                newName: "supply_business_usage_fee_active_energy_total_import_t0_amount_wh");

            migrationBuilder.RenameColumn(
                name: "svt_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "svt_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "svt_peak_kw",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_peak_w");

            migrationBuilder.RenameColumn(
                name: "svt_amount_kw",
                table: "network_user_calculations",
                newName: "usage_active_power_total_import_t1_amount_w");

            migrationBuilder.RenameColumn(
                name: "rvt_total_eur",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "rvt_price_eur",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "rvt_min_kwh",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t1_min_wh");

            migrationBuilder.RenameColumn(
                name: "rvt_max_kwh",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t1_max_wh");

            migrationBuilder.RenameColumn(
                name: "rvt_amount_kwh",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t1_amount_wh");

            migrationBuilder.RenameColumn(
                name: "rnt_total_eur",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t2_total_eur");

            migrationBuilder.RenameColumn(
                name: "rnt_price_eur",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t2_price_eur");

            migrationBuilder.RenameColumn(
                name: "rnt_min_kwh",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t2_min_wh");

            migrationBuilder.RenameColumn(
                name: "rnt_max_kwh",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t2_max_wh");

            migrationBuilder.RenameColumn(
                name: "rnt_amount_kwh",
                table: "network_user_calculations",
                newName: "supply_active_energy_total_import_t2_amount_wh");

            migrationBuilder.RenameColumn(
                name: "oie_total_eur",
                table: "network_user_calculations",
                newName: "supply_renewable_energy_fee_active_energy_total_import_t0_total_eur");

            migrationBuilder.RenameColumn(
                name: "oie_price_eur",
                table: "network_user_calculations",
                newName: "supply_renewable_energy_fee_active_energy_total_import_t0_price_eur");

            migrationBuilder.RenameColumn(
                name: "oie_min_kwh",
                table: "network_user_calculations",
                newName: "supply_renewable_energy_fee_active_energy_total_import_t0_min_wh");

            migrationBuilder.RenameColumn(
                name: "oie_max_kwh",
                table: "network_user_calculations",
                newName: "supply_renewable_energy_fee_active_energy_total_import_t0_max_wh");

            migrationBuilder.RenameColumn(
                name: "oie_amount_kwh",
                table: "network_user_calculations",
                newName: "supply_renewable_energy_fee_active_energy_total_import_t0_amount_wh");

            migrationBuilder.RenameColumn(
                name: "mvt_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_total_eur");

            migrationBuilder.RenameColumn(
                name: "mvt_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_price_eur");

            migrationBuilder.RenameColumn(
                name: "mvt_min_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_min_wh");

            migrationBuilder.RenameColumn(
                name: "mvt_max_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_max_wh");

            migrationBuilder.RenameColumn(
                name: "mvt_amount_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t1_amount_wh");

            migrationBuilder.RenameColumn(
                name: "mnt_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_total_eur");

            migrationBuilder.RenameColumn(
                name: "mnt_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_price_eur");

            migrationBuilder.RenameColumn(
                name: "mnt_min_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_min_wh");

            migrationBuilder.RenameColumn(
                name: "mnt_max_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_max_wh");

            migrationBuilder.RenameColumn(
                name: "mnt_amount_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t2_amount_wh");

            migrationBuilder.RenameColumn(
                name: "mjt_total_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_total_eur");

            migrationBuilder.RenameColumn(
                name: "mjt_price_eur",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_price_eur");

            migrationBuilder.RenameColumn(
                name: "mjt_min_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_min_wh");

            migrationBuilder.RenameColumn(
                name: "mjt_max_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_max_wh");

            migrationBuilder.RenameColumn(
                name: "mjt_amount_kwh",
                table: "network_user_calculations",
                newName: "usage_active_energy_total_import_t0_amount_wh");

            migrationBuilder.RenameColumn(
                name: "jen_reactive_import_min_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_import_min_varh");

            migrationBuilder.RenameColumn(
                name: "jen_reactive_import_max_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_import_max_varh");

            migrationBuilder.RenameColumn(
                name: "jen_reactive_import_amount_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_import_amount_varh");

            migrationBuilder.RenameColumn(
                name: "jen_reactive_export_min_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_export_min_varh");

            migrationBuilder.RenameColumn(
                name: "jen_reactive_export_max_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_export_max_varh");

            migrationBuilder.RenameColumn(
                name: "jen_reactive_export_amount_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_export_amount_varh");

            migrationBuilder.RenameColumn(
                name: "jen_ramped_total_eur",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_ramped_total_eur");

            migrationBuilder.RenameColumn(
                name: "jen_ramped_price_eur",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_ramped_price_eur");

            migrationBuilder.RenameColumn(
                name: "jen_ramped_amount_kvarh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_ramped_amount_varh");

            migrationBuilder.RenameColumn(
                name: "jen_active_import_min_kwh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_active_import_min_wh");

            migrationBuilder.RenameColumn(
                name: "jen_active_import_max_kwh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_active_import_max_wh");

            migrationBuilder.RenameColumn(
                name: "jen_active_import_amount_kwh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_active_import_amount_wh");
        }
    }
}
