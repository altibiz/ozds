using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Messaging.Migrations
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "network_user_invoice_state",
          columns: table => new
          {
            correlation_id = table.Column<Guid>(type: "uuid", nullable: false),
            current_state = table.Column<string>(type: "text", nullable: false),
            xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
            network_user_invoice_id = table.Column<string>(type: "text", nullable: false),
            registration_id = table.Column<string>(type: "text", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("pk_network_user_invoice_state", x => x.correlation_id);
          });

      migrationBuilder.CreateIndex(
          name: "ix_network_user_invoice_state_network_user_invoice_id",
          table: "network_user_invoice_state",
          column: "network_user_invoice_id",
          unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "network_user_invoice_state");
    }
  }
}
