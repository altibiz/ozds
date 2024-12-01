using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class MeasurementMeasurementLocation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_schneider_iem3xxx_measurements",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropPrimaryKey(
                name: "schneider_iem3xxx_aggregates_pkey",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropPrimaryKey(
                name: "pk_abb_b2x_measurements",
                table: "abb_b2x_measurements");

            migrationBuilder.DropPrimaryKey(
                name: "abb_b2x_aggregates_pkey",
                table: "abb_b2x_aggregates");

            migrationBuilder.AddPrimaryKey(
                name: "pk_schneider_iem3xxx_measurements",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "timestamp", "meter_id", "measurement_location_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_schneider_iem3xxx_aggregates",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id", "measurement_location_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_abb_b2x_measurements",
                table: "abb_b2x_measurements",
                columns: new[] { "timestamp", "meter_id", "measurement_location_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_abb_b2x_aggregates",
                table: "abb_b2x_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id", "measurement_location_id" });

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp__measurement_locat",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "timestamp", "measurement_location_id" });

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp_meter_id",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "timestamp", "meter_id" });

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval__measuremen",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "timestamp", "interval", "measurement_location_id" });

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval_meter_id",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_timestamp__measurement_location_id",
                table: "abb_b2x_measurements",
                columns: new[] { "timestamp", "measurement_location_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_timestamp_meter_id",
                table: "abb_b2x_measurements",
                columns: new[] { "timestamp", "meter_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval__measurement_location",
                table: "abb_b2x_aggregates",
                columns: new[] { "timestamp", "interval", "measurement_location_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval_meter_id",
                table: "abb_b2x_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_schneider_iem3xxx_measurements",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp__measurement_locat",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp_meter_id",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_schneider_iem3xxx_aggregates",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval__measuremen",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval_meter_id",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropPrimaryKey(
                name: "pk_abb_b2x_measurements",
                table: "abb_b2x_measurements");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements_timestamp__measurement_location_id",
                table: "abb_b2x_measurements");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements_timestamp_meter_id",
                table: "abb_b2x_measurements");

            migrationBuilder.DropPrimaryKey(
                name: "pk_abb_b2x_aggregates",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval__measurement_location",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval_meter_id",
                table: "abb_b2x_aggregates");

            migrationBuilder.AddPrimaryKey(
                name: "pk_schneider_iem3xxx_measurements",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "timestamp", "meter_id" });

            migrationBuilder.AddPrimaryKey(
                name: "schneider_iem3xxx_aggregates_pkey",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_abb_b2x_measurements",
                table: "abb_b2x_measurements",
                columns: new[] { "timestamp", "meter_id" });

            migrationBuilder.AddPrimaryKey(
                name: "abb_b2x_aggregates_pkey",
                table: "abb_b2x_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" });
        }
    }
}
