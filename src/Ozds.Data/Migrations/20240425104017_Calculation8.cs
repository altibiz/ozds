using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class Calculation8 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_measurement_locations_network_user_catalogues__network_user",
          table: "measurement_locations");

      migrationBuilder.DropColumn(
          name: "catalogue_id",
          table: "measurement_locations");

      migrationBuilder.RenameColumn(
          name: "_network_user_catalogue_id",
          table: "measurement_locations",
          newName: "network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_measurement_locations__network_user_catalogue_id",
          table: "measurement_locations",
          newName: "ix_measurement_locations_network_user_catalogue_id");

      migrationBuilder.AddForeignKey(
          name: "fk_measurement_locations_network_user_catalogues_network_user_",
          table: "measurement_locations",
          column: "network_user_catalogue_id",
          principalTable: "network_user_catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_measurement_locations_network_user_catalogues_network_user_",
          table: "measurement_locations");

      migrationBuilder.RenameColumn(
          name: "network_user_catalogue_id",
          table: "measurement_locations",
          newName: "_network_user_catalogue_id");

      migrationBuilder.RenameIndex(
          name: "ix_measurement_locations_network_user_catalogue_id",
          table: "measurement_locations",
          newName: "ix_measurement_locations__network_user_catalogue_id");

      migrationBuilder.AddColumn<long>(
          name: "catalogue_id",
          table: "measurement_locations",
          type: "bigint",
          nullable: true);

      migrationBuilder.AddForeignKey(
          name: "fk_measurement_locations_network_user_catalogues__network_user",
          table: "measurement_locations",
          column: "_network_user_catalogue_id",
          principalTable: "network_user_catalogues",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
    }
  }
}
