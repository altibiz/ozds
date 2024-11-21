using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnalysisBasisQueryTestingFixes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "am_kind",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anuml_kind",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "aunuc_kind",
                table: "network_user_calculations",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "am_kind",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "anuml_kind",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "aunuc_kind",
                table: "network_user_calculations");
        }
    }
}
