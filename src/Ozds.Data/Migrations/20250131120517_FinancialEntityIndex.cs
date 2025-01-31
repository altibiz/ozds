using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinancialEntityIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_network_user_invoices_from_date",
                table: "network_user_invoices",
                column: "from_date");

            migrationBuilder.CreateIndex(
                name: "ix_network_user_calculations_from_date",
                table: "network_user_calculations",
                column: "from_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_network_user_invoices_from_date",
                table: "network_user_invoices");

            migrationBuilder.DropIndex(
                name: "ix_network_user_calculations_from_date",
                table: "network_user_calculations");
        }
    }
}
