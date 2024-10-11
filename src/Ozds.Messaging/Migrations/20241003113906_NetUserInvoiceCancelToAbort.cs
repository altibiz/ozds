using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Messaging.Migrations
{
    /// <inheritdoc />
    public partial class NetUserInvoiceCancelToAbort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cancel_reason",
                table: "network_user_invoice_states",
                newName: "abort_reason");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "abort_reason",
                table: "network_user_invoice_states",
                newName: "cancel_reason");
        }
    }
}
