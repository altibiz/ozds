using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
  /// <inheritdoc />
  public partial class InvoiceRegistration2 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
          name: "registration_id",
          table: "network_user_invoices",
          type: "text",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "text");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
          name: "registration_id",
          table: "network_user_invoices",
          type: "text",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "text",
          oldNullable: true);
    }
  }
}
