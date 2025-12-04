using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class MeteredNetworkUserCalculationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_network_user_catalogues__usage_ne",
                table: "network_user_calculations");

            migrationBuilder.RenameIndex(
                name: "ix_network_user_calculations__usage_network_user_catalogue_id",
                table: "network_user_calculations",
                newName: "ix_network_user_calculations_usage_network_user_catalogue_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_amount",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "metered_from_date",
                table: "network_user_calculations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "metered_to_date",
                table: "network_user_calculations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "requested_from_date",
                table: "network_user_calculations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "requested_to_date",
                table: "network_user_calculations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_network_user_catalogues_usage_net",
                table: "network_user_calculations",
                column: "usage_network_user_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"
                UPDATE network_user_calculations
                SET
                    metered_from_date = from_date,
                    metered_to_date = to_date,
                    requested_from_date = from_date,
                    requested_to_date = to_date
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_network_user_catalogues_usage_net",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "metered_from_date",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "metered_to_date",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "requested_from_date",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "requested_to_date",
                table: "network_user_calculations");

            migrationBuilder.RenameIndex(
                name: "ix_network_user_calculations_usage_network_user_catalogue_id",
                table: "network_user_calculations",
                newName: "ix_network_user_calculations__usage_network_user_catalogue_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_meter_fee_amount",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "usage_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "trp_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "supply_fee_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rvt_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "rnt_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_total_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_price_eur",
                table: "network_user_calculations",
                type: "numeric(19,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(19,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_min_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_max_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "oie_amount_kwh",
                table: "network_user_calculations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_network_user_catalogues__usage_ne",
                table: "network_user_calculations",
                column: "usage_network_user_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
