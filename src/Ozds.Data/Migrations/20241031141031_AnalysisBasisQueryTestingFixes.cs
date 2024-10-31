using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnalysisBasisQueryTestingFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "al_title",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_title",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "arc_title",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "am_title",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anuml_title",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "asrc_title",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "aunuc_title",
                table: "network_user_calculations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "al_title",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "al_title",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_title",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "arc_title",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "am_title",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "anuml_title",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "asrc_title",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "aunuc_title",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "al_title",
                table: "location_invoices");
        }
    }
}
