using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class Calculation6 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_total_eur",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_fee_active_energy_total_import_t0_total_eur");

      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_fee_price_eur",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_fee_active_energy_total_import_t0_price_eur");

      migrationBuilder.RenameColumn(
          name: "supply_business_usage_total_eur",
          table: "network_user_calculations",
          newName: "supply_business_usage_fee_active_energy_total_import_t0_total_eur");

      migrationBuilder.RenameColumn(
          name: "supply_business_usage_fee_price_eur",
          table: "network_user_calculations",
          newName: "supply_business_usage_fee_active_energy_total_import_t0_price_eur");

      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_fee_amount",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_fee_active_energy_total_import_t0_min_wh");

      migrationBuilder.RenameColumn(
          name: "supply_business_usage_fee_amount",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_fee_active_energy_total_import_t0_max_wh");

      migrationBuilder.AddColumn<decimal>(
          name: "supply_business_usage_fee_active_energy_total_import_t0_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "supply_business_usage_fee_active_energy_total_import_t0_max_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "supply_business_usage_fee_active_energy_total_import_t0_min_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);

      migrationBuilder.AddColumn<decimal>(
          name: "supply_renewable_energy_fee_active_energy_total_import_t0_amount_wh",
          table: "network_user_calculations",
          type: "numeric",
          nullable: false,
          defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "supply_business_usage_fee_active_energy_total_import_t0_amount_wh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "supply_business_usage_fee_active_energy_total_import_t0_max_wh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "supply_business_usage_fee_active_energy_total_import_t0_min_wh",
          table: "network_user_calculations");

      migrationBuilder.DropColumn(
          name: "supply_renewable_energy_fee_active_energy_total_import_t0_amount_wh",
          table: "network_user_calculations");

      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_fee_active_energy_total_import_t0_total_eur",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_total_eur");

      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_fee_active_energy_total_import_t0_price_eur",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_fee_price_eur");

      migrationBuilder.RenameColumn(
          name: "supply_business_usage_fee_active_energy_total_import_t0_total_eur",
          table: "network_user_calculations",
          newName: "supply_business_usage_total_eur");

      migrationBuilder.RenameColumn(
          name: "supply_business_usage_fee_active_energy_total_import_t0_price_eur",
          table: "network_user_calculations",
          newName: "supply_business_usage_fee_price_eur");

      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_fee_active_energy_total_import_t0_min_wh",
          table: "network_user_calculations",
          newName: "supply_renewable_energy_fee_amount");

      migrationBuilder.RenameColumn(
          name: "supply_renewable_energy_fee_active_energy_total_import_t0_max_wh",
          table: "network_user_calculations",
          newName: "supply_business_usage_fee_amount");
    }
  }
}
