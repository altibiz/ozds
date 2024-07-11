using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Messaging.Migrations
{
    /// <inheritdoc />
    public partial class BillId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registration_id",
                table: "network_user_invoice_state",
                newName: "bill_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bill_id",
                table: "network_user_invoice_state",
                newName: "registration_id");
        }
    }
}
