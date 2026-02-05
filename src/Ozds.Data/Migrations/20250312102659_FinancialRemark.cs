using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinancialRemark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invoice_remark",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "anu_invoice_remark",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "anuml_calculation_remark",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "remark",
                table: "network_user_calculations",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "calculation_remark",
                table: "measurement_locations",
                type: "text",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "invoice_remark", table: "network_users");

            migrationBuilder.DropColumn(name: "anu_invoice_remark", table: "network_user_invoices");

            migrationBuilder.DropColumn(name: "remark", table: "network_user_invoices");

            migrationBuilder.DropColumn(name: "anuml_calculation_remark", table: "network_user_calculations");

            migrationBuilder.DropColumn(name: "remark", table: "network_user_calculations");

            migrationBuilder.DropColumn(name: "calculation_remark", table: "measurement_locations");
        }
    }
}
