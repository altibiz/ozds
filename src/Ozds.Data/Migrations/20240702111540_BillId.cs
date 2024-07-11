using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class BillId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "registration_id",
                table: "network_user_invoices",
                newName: "bill_id");

            migrationBuilder.AddColumn<string>(
                name: "alti_biz_sub_project_code",
                table: "network_users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_alti_biz_sub_project_code",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anu_alti_biz_sub_project_code",
                table: "network_user_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "alti_biz_sub_project_code",
                table: "locations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "al_alti_biz_sub_project_code",
                table: "location_invoices",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "alti_biz_sub_project_code",
                table: "network_users");

            migrationBuilder.DropColumn(
                name: "al_alti_biz_sub_project_code",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "anu_alti_biz_sub_project_code",
                table: "network_user_invoices");

            migrationBuilder.DropColumn(
                name: "alti_biz_sub_project_code",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "al_alti_biz_sub_project_code",
                table: "location_invoices");

            migrationBuilder.RenameColumn(
                name: "bill_id",
                table: "network_user_invoices",
                newName: "registration_id");
        }
    }
}
