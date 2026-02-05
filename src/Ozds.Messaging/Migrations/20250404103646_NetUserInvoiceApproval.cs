using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Messaging.Migrations
{
  /// <inheritdoc />
  public partial class NetUserInvoiceApproval : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<bool>(
        name: "approved",
        table: "network_user_invoice_states",
        type: "boolean",
        nullable: false,
        defaultValue: false
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
        name: "approved",
        table: "network_user_invoice_states"
      );
    }
  }
}
