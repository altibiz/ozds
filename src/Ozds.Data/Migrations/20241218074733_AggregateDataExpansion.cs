using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggregateDataExpansion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l1_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l1_net_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l1_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l1_net_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l2_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l2_net_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l2_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l2_net_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l3_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l3_net_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l3_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l3_net_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "apparent_power_total_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "apparent_power_total_net_t0_max_va",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "apparent_power_total_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "apparent_power_total_net_t0_min_va",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "current_l1_any_t0_max_a",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l1_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l1_any_t0_min_a",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l1_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l2_any_t0_max_a",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l2_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l2_any_t0_min_a",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l2_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l3_any_t0_max_a",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l3_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l3_any_t0_min_a",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l3_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_export_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_export_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_export_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_export_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t1_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t1_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t1_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t1_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t2_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t2_max_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t2_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t2_min_w",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_export_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_export_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_total_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_total_net_t0_max_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_total_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_total_net_t0_min_var",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l1_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l1_any_t0_max_v",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l1_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l1_any_t0_min_v",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l2_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l2_any_t0_max_v",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l2_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l2_any_t0_min_v",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l3_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l3_any_t0_max_v",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l3_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l3_any_t0_min_v",
                table: "schneider_iem3xxx_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l1_net_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l1_net_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l1_net_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l1_net_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l2_net_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l2_net_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l2_net_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l2_net_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l3_net_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l3_net_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "active_power_l3_net_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "active_power_l3_net_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "current_l1_any_t0_max_a",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l1_any_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l1_any_t0_min_a",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l1_any_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l2_any_t0_max_a",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l2_any_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l2_any_t0_min_a",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l2_any_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l3_any_t0_max_a",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l3_any_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "current_l3_any_t0_min_a",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "current_l3_any_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_export_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_export_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t0_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t0_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t1_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t1_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t1_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t1_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t2_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t2_max_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_active_power_total_import_t2_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_active_power_total_import_t2_min_w",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_export_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_export_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_import_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "derived_reactive_power_total_import_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_l1_net_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_l1_net_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_l1_net_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_l1_net_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_l2_net_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_l2_net_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_l2_net_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_l2_net_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_l3_net_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_l3_net_t0_max_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "reactive_power_l3_net_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "reactive_power_l3_net_t0_min_var",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l1_any_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l1_any_t0_max_v",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l1_any_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l1_any_t0_min_v",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l2_any_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l2_any_t0_max_v",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l2_any_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l2_any_t0_min_v",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l3_any_t0_max_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l3_any_t0_max_v",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "voltage_l3_any_t0_min_timestamp",
                table: "abb_b2x_aggregates",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<float>(
                name: "voltage_l3_any_t0_min_v",
                table: "abb_b2x_aggregates",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "apparent_power_total_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "apparent_power_total_net_t0_max_va",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "apparent_power_total_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "apparent_power_total_net_t0_min_va",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_max_a",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_min_a",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_max_a",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_min_a",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_max_a",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_min_a",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_max_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_min_w",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_total_net_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_total_net_t0_max_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_total_net_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_total_net_t0_min_var",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_max_v",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_min_v",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_max_v",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_min_v",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_max_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_max_v",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_min_timestamp",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_min_v",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l1_net_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l2_net_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "active_power_l3_net_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_max_a",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_min_a",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l1_any_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_max_a",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_min_a",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l2_any_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_max_a",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_min_a",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "current_l3_any_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_export_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t0_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t1_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_avg_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_max_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_active_power_total_import_t2_min_w",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_export_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_avg_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "derived_reactive_power_total_import_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l1_net_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l1_net_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l1_net_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l1_net_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l2_net_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l2_net_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l2_net_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l2_net_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l3_net_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l3_net_t0_max_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l3_net_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "reactive_power_l3_net_t0_min_var",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_max_v",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l1_any_t0_min_v",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_max_v",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l2_any_t0_min_v",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_max_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_max_v",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_min_timestamp",
                table: "abb_b2x_aggregates");

            migrationBuilder.DropColumn(
                name: "voltage_l3_any_t0_min_v",
                table: "abb_b2x_aggregates");
        }
    }
}
