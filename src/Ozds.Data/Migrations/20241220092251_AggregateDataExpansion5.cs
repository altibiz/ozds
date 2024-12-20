using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggregateDataExpansion5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "active_energy_l1_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l1_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l2_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l2_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l3_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l3_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l1_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l1_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l2_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l2_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l3_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l3_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l1_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l1_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l1_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l1_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l2_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l2_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l2_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l2_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l3_export_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l3_export_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l3_import_t0_max_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "active_energy_l3_import_t0_min_wh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l1_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l1_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l1_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l1_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l2_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l2_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l2_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l2_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l3_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l3_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l3_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_l3_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l1_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l1_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l1_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l1_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l1_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l1_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l1_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l1_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l1_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l1_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l2_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l2_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l2_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l2_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l2_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l2_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l2_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l2_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l2_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l2_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l3_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l3_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l3_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l3_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l3_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l3_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l3_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l3_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_l3_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_l3_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l1_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l1_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l1_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l1_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l2_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l2_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l2_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l2_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l3_export_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l3_export_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l3_import_t0_max_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "reactive_energy_l3_import_t0_min_varh",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active_energy_l1_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l1_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l2_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l2_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l3_import_t0_max_wh",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l3_import_t0_min_wh",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l1_export_t0_max_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l1_export_t0_min_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l1_import_t0_max_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l1_import_t0_min_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l2_export_t0_max_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l2_export_t0_min_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l2_import_t0_max_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l2_import_t0_min_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l3_export_t0_max_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l3_export_t0_min_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l3_import_t0_max_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_energy_l3_import_t0_min_wh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_export_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_export_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_export_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l1_import_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_export_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_export_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_export_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l2_import_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_export_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_export_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_export_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_l3_import_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_export_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_export_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_export_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_import_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_import_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l1_import_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_export_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_export_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_export_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_import_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_import_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l2_import_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_export_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_export_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_export_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_import_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_import_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_l3_import_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l1_export_t0_max_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l1_export_t0_min_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l1_import_t0_max_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l1_import_t0_min_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l2_export_t0_max_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l2_export_t0_min_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l2_import_t0_max_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l2_import_t0_min_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l3_export_t0_max_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l3_export_t0_min_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l3_import_t0_max_varh",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_energy_l3_import_t0_min_varh",
                table: "abb_b2x_aggregates");
        }
    }
}
