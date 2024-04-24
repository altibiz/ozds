using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Calculation7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_network_user_catalogues_catalogue_id",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_location_invoices_location_invoic",
                table: "network_user_calculations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_measurement_locations__measuremen",
                table: "network_user_calculations");

            migrationBuilder.DropIndex(
                name: "ix_network_user_calculations_location_invoice_entity_id",
                table: "network_user_calculations");

            migrationBuilder.DropIndex(
                name: "ix_measurement_locations_catalogue_id",
                table: "measurement_locations");

            migrationBuilder.DropColumn(
                name: "location_invoice_entity_id",
                table: "network_user_calculations");

            migrationBuilder.RenameColumn(
                name: "aml_meter_id",
                table: "network_user_calculations",
                newName: "anuml_meter_id");

            migrationBuilder.RenameColumn(
                name: "aml_last_updated_on",
                table: "network_user_calculations",
                newName: "anuml_last_updated_on");

            migrationBuilder.RenameColumn(
                name: "aml_last_updated_by_id",
                table: "network_user_calculations",
                newName: "anuml_last_updated_by_id");

            migrationBuilder.RenameColumn(
                name: "aml_is_deleted",
                table: "network_user_calculations",
                newName: "anuml_is_deleted");

            migrationBuilder.RenameColumn(
                name: "aml_deleted_on",
                table: "network_user_calculations",
                newName: "anuml_deleted_on");

            migrationBuilder.RenameColumn(
                name: "aml_deleted_by_id",
                table: "network_user_calculations",
                newName: "anuml_deleted_by_id");

            migrationBuilder.RenameColumn(
                name: "aml_created_on",
                table: "network_user_calculations",
                newName: "anuml_created_on");

            migrationBuilder.RenameColumn(
                name: "aml_created_by_id",
                table: "network_user_calculations",
                newName: "anuml_created_by_id");

            migrationBuilder.RenameColumn(
                name: "measurement_location_id",
                table: "network_user_calculations",
                newName: "network_user_measurement_location_id");

            migrationBuilder.RenameIndex(
                name: "ix_network_user_calculations__measurement_location_id",
                table: "network_user_calculations",
                newName: "ix_network_user_calculations__network_user_measurement_locatio");

            migrationBuilder.AddColumn<long>(
                name: "_network_user_catalogue_id",
                table: "measurement_locations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations__network_user_catalogue_id",
                table: "measurement_locations",
                column: "_network_user_catalogue_id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_network_user_catalogues__network_user",
                table: "measurement_locations",
                column: "_network_user_catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_measurement_locations__network_us",
                table: "network_user_calculations",
                column: "network_user_measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_measurement_locations_network_user_catalogues__network_user",
                table: "measurement_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_calculations_measurement_locations__network_us",
                table: "network_user_calculations");

            migrationBuilder.DropIndex(
                name: "ix_measurement_locations__network_user_catalogue_id",
                table: "measurement_locations");

            migrationBuilder.DropColumn(
                name: "_network_user_catalogue_id",
                table: "measurement_locations");

            migrationBuilder.RenameColumn(
                name: "anuml_meter_id",
                table: "network_user_calculations",
                newName: "aml_meter_id");

            migrationBuilder.RenameColumn(
                name: "anuml_last_updated_on",
                table: "network_user_calculations",
                newName: "aml_last_updated_on");

            migrationBuilder.RenameColumn(
                name: "anuml_last_updated_by_id",
                table: "network_user_calculations",
                newName: "aml_last_updated_by_id");

            migrationBuilder.RenameColumn(
                name: "anuml_is_deleted",
                table: "network_user_calculations",
                newName: "aml_is_deleted");

            migrationBuilder.RenameColumn(
                name: "anuml_deleted_on",
                table: "network_user_calculations",
                newName: "aml_deleted_on");

            migrationBuilder.RenameColumn(
                name: "anuml_deleted_by_id",
                table: "network_user_calculations",
                newName: "aml_deleted_by_id");

            migrationBuilder.RenameColumn(
                name: "anuml_created_on",
                table: "network_user_calculations",
                newName: "aml_created_on");

            migrationBuilder.RenameColumn(
                name: "anuml_created_by_id",
                table: "network_user_calculations",
                newName: "aml_created_by_id");

            migrationBuilder.RenameColumn(
                name: "network_user_measurement_location_id",
                table: "network_user_calculations",
                newName: "measurement_location_id");

            migrationBuilder.RenameIndex(
                name: "ix_network_user_calculations__network_user_measurement_locatio",
                table: "network_user_calculations",
                newName: "ix_network_user_calculations__measurement_location_id");

            migrationBuilder.AddColumn<long>(
                name: "location_invoice_entity_id",
                table: "network_user_calculations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_network_user_calculations_location_invoice_entity_id",
                table: "network_user_calculations",
                column: "location_invoice_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_measurement_locations_catalogue_id",
                table: "measurement_locations",
                column: "catalogue_id");

            migrationBuilder.AddForeignKey(
                name: "fk_measurement_locations_network_user_catalogues_catalogue_id",
                table: "measurement_locations",
                column: "catalogue_id",
                principalTable: "network_user_catalogues",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_location_invoices_location_invoic",
                table: "network_user_calculations",
                column: "location_invoice_entity_id",
                principalTable: "location_invoices",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_calculations_measurement_locations__measuremen",
                table: "network_user_calculations",
                column: "measurement_location_id",
                principalTable: "measurement_locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
