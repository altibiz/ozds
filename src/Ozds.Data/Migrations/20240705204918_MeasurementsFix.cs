using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class MeasurementsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reactive_energy_import_total_t0_varh",
                table: "schneider_iem3xxx_measurements",
                newName: "reactive_energy_total_import_t0_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_export_total_t0_varh",
                table: "schneider_iem3xxx_measurements",
                newName: "reactive_energy_total_export_t0_varh");

            migrationBuilder.RenameColumn(
                name: "active_energy_import_total_t2_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_total_import_t2_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_import_total_t1_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_total_import_t1_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_import_total_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_total_import_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_import_l3_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_l3_import_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_import_l2_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_l2_import_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_import_l1_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_l1_import_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_export_total_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_total_export_t0_wh");

            migrationBuilder.RenameColumn(
                name: "reactive_power_l3_any_t0_var",
                table: "abb_b2x_measurements",
                newName: "reactive_power_l3_net_t0_var");

            migrationBuilder.RenameColumn(
                name: "reactive_power_l2_any_t0_var",
                table: "abb_b2x_measurements",
                newName: "reactive_power_l2_net_t0_var");

            migrationBuilder.RenameColumn(
                name: "reactive_power_l1_any_t0_var",
                table: "abb_b2x_measurements",
                newName: "reactive_power_l1_net_t0_var");

            migrationBuilder.RenameColumn(
                name: "active_power_l3_any_t0_w",
                table: "abb_b2x_measurements",
                newName: "active_power_l3_net_t0_w");

            migrationBuilder.RenameColumn(
                name: "active_power_l2_any_t0_w",
                table: "abb_b2x_measurements",
                newName: "active_power_l2_net_t0_w");

            migrationBuilder.RenameColumn(
                name: "active_power_l1_any_t0_w",
                table: "abb_b2x_measurements",
                newName: "active_power_l1_net_t0_w");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_import_t0_varh",
                table: "schneider_iem3xxx_measurements",
                newName: "reactive_energy_import_total_t0_varh");

            migrationBuilder.RenameColumn(
                name: "reactive_energy_total_export_t0_varh",
                table: "schneider_iem3xxx_measurements",
                newName: "reactive_energy_export_total_t0_varh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t2_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_import_total_t2_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t1_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_import_total_t1_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_import_total_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_total_export_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_export_total_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_l3_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_import_l3_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_l2_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_import_l2_t0_wh");

            migrationBuilder.RenameColumn(
                name: "active_energy_l1_import_t0_wh",
                table: "schneider_iem3xxx_measurements",
                newName: "active_energy_import_l1_t0_wh");

            migrationBuilder.RenameColumn(
                name: "reactive_power_l3_net_t0_var",
                table: "abb_b2x_measurements",
                newName: "reactive_power_l3_any_t0_var");

            migrationBuilder.RenameColumn(
                name: "reactive_power_l2_net_t0_var",
                table: "abb_b2x_measurements",
                newName: "reactive_power_l2_any_t0_var");

            migrationBuilder.RenameColumn(
                name: "reactive_power_l1_net_t0_var",
                table: "abb_b2x_measurements",
                newName: "reactive_power_l1_any_t0_var");

            migrationBuilder.RenameColumn(
                name: "active_power_l3_net_t0_w",
                table: "abb_b2x_measurements",
                newName: "active_power_l3_any_t0_w");

            migrationBuilder.RenameColumn(
                name: "active_power_l2_net_t0_w",
                table: "abb_b2x_measurements",
                newName: "active_power_l2_any_t0_w");

            migrationBuilder.RenameColumn(
                name: "active_power_l1_net_t0_w",
                table: "abb_b2x_measurements",
                newName: "active_power_l1_any_t0_w");
        }
    }
}
