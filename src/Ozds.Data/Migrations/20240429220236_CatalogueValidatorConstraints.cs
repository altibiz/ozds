using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class CatalogueValidatorConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_meters__measurement_validator_id",
                table: "meters");

            migrationBuilder.DropIndex(
                name: "ix_locations__blue_low_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__red_low_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__regulatory_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__white_low_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__white_medium_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.CreateIndex(
                name: "ix_meters__measurement_validator_id",
                table: "meters",
                column: "measurement_validator_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations__blue_low_network_user_catalogue_id",
                table: "locations",
                column: "blue_low_catalogue_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations__red_low_network_user_catalogue_id",
                table: "locations",
                column: "red_low_catalogue_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations__regulatory_network_user_catalogue_id",
                table: "locations",
                column: "regulatory_catalogue_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations__white_low_network_user_catalogue_id",
                table: "locations",
                column: "white_low_catalogue_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations__white_medium_network_user_catalogue_id",
                table: "locations",
                column: "white_medium_catalogue_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_meters__measurement_validator_id",
                table: "meters");

            migrationBuilder.DropIndex(
                name: "ix_locations__blue_low_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__red_low_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__regulatory_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__white_low_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "ix_locations__white_medium_network_user_catalogue_id",
                table: "locations");

            migrationBuilder.CreateIndex(
                name: "ix_meters__measurement_validator_id",
                table: "meters",
                column: "measurement_validator_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations__blue_low_network_user_catalogue_id",
                table: "locations",
                column: "blue_low_catalogue_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations__red_low_network_user_catalogue_id",
                table: "locations",
                column: "red_low_catalogue_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations__regulatory_network_user_catalogue_id",
                table: "locations",
                column: "regulatory_catalogue_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations__white_low_network_user_catalogue_id",
                table: "locations",
                column: "white_low_catalogue_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations__white_medium_network_user_catalogue_id",
                table: "locations",
                column: "white_medium_catalogue_id",
                unique: true);
        }
    }
}
