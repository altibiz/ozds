using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class MeasurementMeasurementLocation : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<long>(
          name: "measurement_location_id",
          table: "schneider_iem3xxx_measurements",
          type: "bigint",
          nullable: true);

      migrationBuilder.AddColumn<long>(
          name: "measurement_location_id",
          table: "schneider_iem3xxx_aggregates",
          type: "bigint",
          nullable: true);

      migrationBuilder.AddColumn<long>(
          name: "measurement_location_id",
          table: "abb_b2x_measurements",
          type: "bigint",
          nullable: true);

      migrationBuilder.AddColumn<long>(
          name: "measurement_location_id",
          table: "abb_b2x_aggregates",
          type: "bigint",
          nullable: true);

      migrationBuilder.Sql(@"
        UPDATE schneider_iem3xxx_measurements m
        SET measurement_location_id = ml.id
        FROM measurement_locations ml
        WHERE m.meter_id = ml.meter_id;

        UPDATE schneider_iem3xxx_aggregates a
        SET measurement_location_id = ml.id
        FROM measurement_locations ml
        WHERE a.meter_id = ml.meter_id;

        UPDATE abb_b2x_measurements m
        SET measurement_location_id = ml.id
        FROM measurement_locations ml
        WHERE m.meter_id = ml.meter_id;

        UPDATE abb_b2x_aggregates a
        SET measurement_location_id = ml.id
        FROM measurement_locations ml
        WHERE a.meter_id = ml.meter_id;
    ");

      migrationBuilder.AlterColumn<long>(
          name: "measurement_location_id",
          table: "schneider_iem3xxx_measurements",
          type: "bigint",
          nullable: false,
          oldClrType: typeof(long),
          oldType: "bigint",
          oldNullable: true);

      migrationBuilder.AlterColumn<long>(
          name: "measurement_location_id",
          table: "schneider_iem3xxx_aggregates",
          type: "bigint",
          nullable: false,
          oldClrType: typeof(long),
          oldType: "bigint",
          oldNullable: true);

      migrationBuilder.AlterColumn<long>(
          name: "measurement_location_id",
          table: "abb_b2x_measurements",
          type: "bigint",
          nullable: false,
          oldClrType: typeof(long),
          oldType: "bigint",
          oldNullable: true);

      migrationBuilder.AlterColumn<long>(
          name: "measurement_location_id",
          table: "abb_b2x_aggregates",
          type: "bigint",
          nullable: false,
          oldClrType: typeof(long),
          oldType: "bigint",
          oldNullable: true);

      migrationBuilder.CreateIndex(
          name: "ix_schneider_iem3xxx_measurements_measurement_location_id",
          table: "schneider_iem3xxx_measurements",
          column: "measurement_location_id");

      migrationBuilder.CreateIndex(
          name: "ix_schneider_iem3xxx_aggregates_measurement_location_id",
          table: "schneider_iem3xxx_aggregates",
          column: "measurement_location_id");

      migrationBuilder.CreateIndex(
          name: "ix_abb_b2x_measurements_measurement_location_id",
          table: "abb_b2x_measurements",
          column: "measurement_location_id");

      migrationBuilder.CreateIndex(
          name: "ix_abb_b2x_aggregates_measurement_location_id",
          table: "abb_b2x_aggregates",
          column: "measurement_location_id");

      migrationBuilder.AddForeignKey(
          name: "fk_abb_b2x_aggregates_measurement_locations_measurement_locati",
          table: "abb_b2x_aggregates",
          column: "measurement_location_id",
          principalTable: "measurement_locations",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_abb_b2x_measurements_measurement_locations_measurement_loca",
          table: "abb_b2x_measurements",
          column: "measurement_location_id",
          principalTable: "measurement_locations",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_schneider_iem3xxx_aggregates_measurement_locations_measurem",
          table: "schneider_iem3xxx_aggregates",
          column: "measurement_location_id",
          principalTable: "measurement_locations",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);

      migrationBuilder.AddForeignKey(
          name: "fk_schneider_iem3xxx_measurements_measurement_locations_measur",
          table: "schneider_iem3xxx_measurements",
          column: "measurement_location_id",
          principalTable: "measurement_locations",
          principalColumn: "id",
          onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "fk_abb_b2x_aggregates_measurement_locations_measurement_locati",
          table: "abb_b2x_aggregates");

      migrationBuilder.DropForeignKey(
          name: "fk_abb_b2x_measurements_measurement_locations_measurement_loca",
          table: "abb_b2x_measurements");

      migrationBuilder.DropForeignKey(
          name: "fk_schneider_iem3xxx_aggregates_measurement_locations_measurem",
          table: "schneider_iem3xxx_aggregates");

      migrationBuilder.DropForeignKey(
          name: "fk_schneider_iem3xxx_measurements_measurement_locations_measur",
          table: "schneider_iem3xxx_measurements");

      migrationBuilder.DropIndex(
          name: "ix_schneider_iem3xxx_measurements_measurement_location_id",
          table: "schneider_iem3xxx_measurements");

      migrationBuilder.DropIndex(
          name: "ix_schneider_iem3xxx_aggregates_measurement_location_id",
          table: "schneider_iem3xxx_aggregates");

      migrationBuilder.DropIndex(
          name: "ix_abb_b2x_measurements_measurement_location_id",
          table: "abb_b2x_measurements");

      migrationBuilder.DropIndex(
          name: "ix_abb_b2x_aggregates_measurement_location_id",
          table: "abb_b2x_aggregates");

      migrationBuilder.DropColumn(
          name: "measurement_location_id",
          table: "schneider_iem3xxx_measurements");

      migrationBuilder.DropColumn(
          name: "measurement_location_id",
          table: "schneider_iem3xxx_aggregates");

      migrationBuilder.DropColumn(
          name: "measurement_location_id",
          table: "abb_b2x_measurements");

      migrationBuilder.DropColumn(
          name: "measurement_location_id",
          table: "abb_b2x_aggregates");
    }
  }
}
