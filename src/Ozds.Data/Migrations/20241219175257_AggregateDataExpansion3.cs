using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggregateDataExpansion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "quarter_hour_count",
                table: "schneider_iem3xxx_aggregates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "quarter_hour_count",
                table: "abb_b2x_aggregates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quarter_hour_count",
                table: "schneider_iem3xxx_aggregates");

            migrationBuilder.DropColumn(
                name: "quarter_hour_count",
                table: "abb_b2x_aggregates");
        }
    }
}
