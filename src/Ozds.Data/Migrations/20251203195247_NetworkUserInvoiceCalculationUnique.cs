using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class NetworkUserInvoiceCalculationUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_network_user_invoices__network_user_id",
                table: "network_user_invoices"
            );

            migrationBuilder.DropIndex(
                name: "ix_network_user_calculations__network_user_measurement_locatio",
                table: "network_user_calculations"
            );

            migrationBuilder.Sql(
                @"
                DELETE FROM network_user_invoices
                WHERE id IN (
                    SELECT id FROM (
                        SELECT id,
                        ROW_NUMBER() OVER (
                            PARTITION BY network_user_id, from_date, to_date
                            ORDER BY issued_on DESC, id DESC
                        ) as rn
                        FROM network_user_invoices
                    ) t
                    WHERE rn > 1
                );
            "
            );

            migrationBuilder.Sql(
                @"
                DELETE FROM network_user_calculations
                WHERE id IN (
                    SELECT id FROM (
                        SELECT c.id,
                        ROW_NUMBER() OVER (
                            PARTITION BY c.network_user_measurement_location_id, c.from_date, c.to_date
                            ORDER BY i.issued_on DESC, c.id DESC
                        ) as rn
                        FROM network_user_calculations c
                        LEFT JOIN network_user_invoices i ON c.network_user_invoice_id = i.id
                    ) t
                    WHERE rn > 1
                );
            "
            );

            migrationBuilder.CreateIndex(
                name: "ix_network_user_invoices__network_user_id_from_date_to_date",
                table: "network_user_invoices",
                columns: new[] { "network_user_id", "from_date", "to_date" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_network_user_calculations__network_user_measurement_locatio",
                table: "network_user_calculations",
                columns: new[] { "network_user_measurement_location_id", "from_date", "to_date" },
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_network_user_invoices__network_user_id_from_date_to_date",
                table: "network_user_invoices"
            );

            migrationBuilder.DropIndex(
                name: "ix_network_user_calculations__network_user_measurement_locatio",
                table: "network_user_calculations"
            );

            migrationBuilder.CreateIndex(
                name: "ix_network_user_invoices__network_user_id",
                table: "network_user_invoices",
                column: "network_user_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_network_user_calculations__network_user_measurement_locatio",
                table: "network_user_calculations",
                column: "network_user_measurement_location_id"
            );
        }
    }
}
