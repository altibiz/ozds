using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReverseMeasurementAggregateIndices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_measurement_location_id",
                table: "schneider_iem3xxx_measurements"
            );

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_meter_id",
                table: "schneider_iem3xxx_measurements"
            );

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_measurement_location_id",
                table: "schneider_iem3xxx_aggregates"
            );

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_meter_id",
                table: "schneider_iem3xxx_aggregates"
            );

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements_measurement_location_id",
                table: "abb_b2x_measurements"
            );

            migrationBuilder.DropIndex(name: "ix_abb_b2x_measurements_meter_id", table: "abb_b2x_measurements");

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates_measurement_location_id",
                table: "abb_b2x_aggregates"
            );

            migrationBuilder.DropIndex(name: "ix_abb_b2x_aggregates_meter_id", table: "abb_b2x_aggregates");

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements__measurement_location_id_tim",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "measurement_location_id", "timestamp" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_meter_id_timestamp",
                table: "schneider_iem3xxx_measurements",
                columns: new[] { "meter_id", "timestamp" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates__measurement_location_id_inter",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "measurement_location_id", "interval", "timestamp" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_meter_id_interval_timestamp",
                table: "schneider_iem3xxx_aggregates",
                columns: new[] { "meter_id", "interval", "timestamp" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements__measurement_location_id_timestamp",
                table: "abb_b2x_measurements",
                columns: new[] { "measurement_location_id", "timestamp" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_meter_id_timestamp",
                table: "abb_b2x_measurements",
                columns: new[] { "meter_id", "timestamp" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates__measurement_location_id_interval_timest",
                table: "abb_b2x_aggregates",
                columns: new[] { "measurement_location_id", "interval", "timestamp" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_meter_id_interval_timestamp",
                table: "abb_b2x_aggregates",
                columns: new[] { "meter_id", "interval", "timestamp" },
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements__measurement_location_id_tim",
                table: "schneider_iem3xxx_measurements"
            );

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_measurements_meter_id_timestamp",
                table: "schneider_iem3xxx_measurements"
            );

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates__measurement_location_id_inter",
                table: "schneider_iem3xxx_aggregates"
            );

            migrationBuilder.DropIndex(
                name: "ix_schneider_iem3xxx_aggregates_meter_id_interval_timestamp",
                table: "schneider_iem3xxx_aggregates"
            );

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements__measurement_location_id_timestamp",
                table: "abb_b2x_measurements"
            );

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_measurements_meter_id_timestamp",
                table: "abb_b2x_measurements"
            );

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates__measurement_location_id_interval_timest",
                table: "abb_b2x_aggregates"
            );

            migrationBuilder.DropIndex(
                name: "ix_abb_b2x_aggregates_meter_id_interval_timestamp",
                table: "abb_b2x_aggregates"
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_measurement_location_id",
                table: "schneider_iem3xxx_measurements",
                column: "measurement_location_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_measurements_meter_id",
                table: "schneider_iem3xxx_measurements",
                column: "meter_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_measurement_location_id",
                table: "schneider_iem3xxx_aggregates",
                column: "measurement_location_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_schneider_iem3xxx_aggregates_meter_id",
                table: "schneider_iem3xxx_aggregates",
                column: "meter_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_measurement_location_id",
                table: "abb_b2x_measurements",
                column: "measurement_location_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_measurements_meter_id",
                table: "abb_b2x_measurements",
                column: "meter_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_measurement_location_id",
                table: "abb_b2x_aggregates",
                column: "measurement_location_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_abb_b2x_aggregates_meter_id",
                table: "abb_b2x_aggregates",
                column: "meter_id"
            );
        }
    }
}
