using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class Calculation5 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<decimal>(
          name: "aunuc_active_energy_total_import_t0_price__eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "aunuc_active_energy_total_import_t1_price__eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "aunuc_active_energy_total_import_t2_price__eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "aunuc_active_power_total_import_t1_price__eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "aunuc_meter_fee_price__eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "aunuc_reactive_energy_total_ramped_t0_price__eur",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "aunuc_active_energy_total_import_t0_price__eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "aunuc_active_energy_total_import_t1_price__eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "aunuc_active_energy_total_import_t2_price__eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "aunuc_active_power_total_import_t1_price__eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "aunuc_meter_fee_price__eur",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "aunuc_reactive_energy_total_ramped_t0_price__eur",
          table: "network_user_calculations");
    }
  }
}
