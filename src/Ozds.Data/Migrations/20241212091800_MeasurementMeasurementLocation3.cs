using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class MeasurementMeasurementLocation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp_meter_id",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval_meter_id",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements_timestamp_meter_id",
                table: "abb_b2x_measurements");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval_meter_id",
                table: "abb_b2x_aggregates");

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp_meter_id",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "timestamp", "meter_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval_meter_id",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_timestamp_meter_id",
                table: "abb_b2x_measurements",
                columns: new[] { "timestamp", "meter_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval_meter_id",
                table: "abb_b2x_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp_meter_id",
                table: "schneider_iem3xxx_measurements");

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval_meter_id",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements_timestamp_meter_id",
                table: "abb_b2x_measurements");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval_meter_id",
                table: "abb_b2x_aggregates");

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_timestamp_meter_id",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "timestamp", "meter_id" });

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_timestamp_interval_meter_id",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_timestamp_meter_id",
                table: "abb_b2x_measurements",
                columns: new[] { "timestamp", "meter_id" });

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_timestamp_interval_meter_id",
                table: "abb_b2x_aggregates",
                columns: new[] { "timestamp", "interval", "meter_id" });
        }
    }
}
