using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Calculation4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations");

            migrationBuilder.DropIndex(
                name: "ix_network_user_calculations__network_user_id",
                table: "network_user_calculations");

            migrationBuilder.DropColumn(
                name: "network_user_id",
                table: "network_user_calculations");

            migrationBuilder.CreateIndex(
                name: "ix_network_user_calculations__network_user_invoice_id",
                table: "network_user_calculations",
                column: "network_user_invoice_id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations",
                column: "network_user_invoice_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations");

            migrationBuilder.DropIndex(
                name: "ix_network_user_calculations__network_user_invoice_id",
                table: "network_user_calculations");

            migrationBuilder.AddColumn<long>(
                name: "network_user_id",
                table: "network_user_calculations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_network_user_calculations__network_user_id",
                table: "network_user_calculations",
                column: "network_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_network_user_invoices__network_us",
                table: "network_user_calculations",
                column: "network_user_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
