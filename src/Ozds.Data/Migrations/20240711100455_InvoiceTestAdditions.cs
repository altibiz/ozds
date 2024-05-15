using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceTestAdditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_import_min_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_import_min_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_import_max_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_import_max_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_import_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_import_amount_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_export_min_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_export_min_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_export_max_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_export_max_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_export_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_reactive_export_amount_varh");

            migrationBuilder.AddColumn<decimal>(
                name: "tax_rate_percent",
                table: "network_user_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_reactive_energy_total_ramped_t0_active_import_amount_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_reactive_energy_total_ramped_t0_active_import_max_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "usage_reactive_energy_total_ramped_t0_active_import_min_wh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "tax_rate_percent",
                table: "location_invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tax_rate_percent",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "usage_reactive_energy_total_ramped_t0_active_import_amount_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "usage_reactive_energy_total_ramped_t0_active_import_max_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "usage_reactive_energy_total_ramped_t0_active_import_min_wh",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "tax_rate_percent",
                table: "location_invoices");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_import_min_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_import_min_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_import_max_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_import_max_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_import_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_import_amount_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_export_min_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_export_min_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_export_max_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_export_max_varh");

            migrationBuilder.RenameColumn(
                name: "usage_reactive_energy_total_ramped_t0_reactive_export_amount_varh",
                table: "network_user_calculations",
                newName: "usage_reactive_energy_total_ramped_t0_export_amount_varh");
        }
    }
}
