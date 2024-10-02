using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Messaging.Migrations
{
    /// <inheritdoc />
    public partial class NetUserInvoiceStateRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_network_user_invoice_state",
                table: "network_user_invoice_state");

            migrationBuilder.RenameTable(
                name: "network_user_invoice_state",
                newName: "network_user_invoice_states");

            migrationBuilder.RenameIndex(
                name: "ix_network_user_invoice_state_network_user_invoice_id",
                table: "network_user_invoice_states",
                newName: "ix_network_user_invoice_states_network_user_invoice_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_network_user_invoice_states",
                table: "network_user_invoice_states",
                column: "correlation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_network_user_invoice_states",
                table: "network_user_invoice_states");

            migrationBuilder.RenameTable(
                name: "network_user_invoice_states",
                newName: "network_user_invoice_state");

            migrationBuilder.RenameIndex(
                name: "ix_network_user_invoice_states_network_user_invoice_id",
                table: "network_user_invoice_state",
                newName: "ix_network_user_invoice_state_network_user_invoice_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_network_user_invoice_state",
                table: "network_user_invoice_state",
                column: "correlation_id");
        }
    }
}
