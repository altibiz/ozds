using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class InvoiceRegistration : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "registration_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "registration_id",
          table: "network_user_invoices");
    }
  }
}
