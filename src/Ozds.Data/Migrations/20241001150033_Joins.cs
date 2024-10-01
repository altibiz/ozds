using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    public partial class Joins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_recipient_entity",
                table: "notification_recipient_entity");

            migrationBuilder.RenameTable(
                name: "notification_recipient_entity",
                newName: "notification_recipients");

            migrationBuilder.RenameIndex(
                name: "ix_notification_recipient_entity__notification_id",
                table: "notification_recipients",
                newName: "ix_notification_recipients__notification_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_recipients",
                table: "notification_recipients",
                columns: new[] { "representative_id", "notification_id" });

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipient_entity_notifications_notification_id",
                table: "notification_recipients");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipient_entity_representatives_representativ",
                table: "notification_recipients");

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipients_notifications_notification_id",
                table: "notification_recipients",
                column: "notification_id",
                principalTable: "notifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipients_representatives_representative_id",
                table: "notification_recipients",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropPrimaryKey(
                name: "pk_location_entity_representative_entity",
                table: "location_entity_representative_entity");

            migrationBuilder.RenameTable(
                name: "location_entity_representative_entity",
                newName: "location_representatives");

            migrationBuilder.RenameColumn(
                name: "locations_id",
                table: "location_representatives",
                newName: "location_id");

            migrationBuilder.RenameColumn(
                name: "representatives_string_id",
                table: "location_representatives",
                newName: "representative_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_location_representatives",
                table: "location_representatives",
                columns: new[] { "representative_id", "location_id" });

            migrationBuilder.DropForeignKey(
                name: "fk_location_entity_representative_entity_locations_locations_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_location_entity_representative_entity_representatives_repre",
                table: "location_representatives");

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropPrimaryKey(
                name: "pk_network_user_entity_representative_entity",
                table: "network_user_entity_representative_entity");

            migrationBuilder.RenameTable(
                name: "network_user_entity_representative_entity",
                newName: "network_user_representatives");

            migrationBuilder.RenameColumn(
                name: "network_users_id",
                table: "network_user_representatives",
                newName: "network_user_id");

            migrationBuilder.RenameColumn(
                name: "representatives_string_id",
                table: "network_user_representatives",
                newName: "representative_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_network_user_representatives",
                table: "network_user_representatives",
                columns: new[] { "representative_id", "network_user_id" });

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_entity_representative_entity_network_users_net",
                table: "network_user_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_entity_representative_entity_representatives_r",
                table: "network_user_representatives");

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_network_users_network_user_id",
                table: "network_user_representatives",
                column: "network_user_id",
                principalTable: "network_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_representatives_representatives_representative",
                table: "network_user_representatives",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipients_notifications_notification_id",
                table: "notification_recipients");

            migrationBuilder.DropForeignKey(
                name: "fk_notification_recipients_representatives_representative_id",
                table: "notification_recipients");

            migrationBuilder.DropPrimaryKey(
                name: "pk_notification_recipients",
                table: "notification_recipients");

            migrationBuilder.RenameTable(
                name: "notification_recipients",
                newName: "notification_recipient_entity");

            migrationBuilder.RenameIndex(
                name: "ix_notification_recipients__notification_id",
                table: "notification_recipient_entity",
                newName: "ix_notification_recipient_entity__notification_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_notification_recipient_entity",
                table: "notification_recipient_entity",
                columns: new[] { "representative_id", "notification_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipient_entity_notifications_notification_id",
                table: "notification_recipient_entity",
                column: "notification_id",
                principalTable: "notifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notification_recipient_entity_representatives_representativ",
                table: "notification_recipient_entity",
                column: "representative_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_locations_location_id",
                table: "location_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_location_representatives_representatives_representative_id",
                table: "location_representatives");

            migrationBuilder.DropPrimaryKey(
                name: "pk_location_representatives",
                table: "location_representatives");

            migrationBuilder.RenameTable(
                name: "location_representatives",
                newName: "location_entity_representative_entity");

            migrationBuilder.RenameColumn(
                name: "location_id",
                table: "location_entity_representative_entity",
                newName: "locations_id");

            migrationBuilder.RenameColumn(
                name: "representative_id",
                table: "location_entity_representative_entity",
                newName: "representatives_string_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_location_entity_representative_entity",
                table: "location_entity_representative_entity",
                columns: new[] { "locations_id", "representatives_string_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_location_entity_representative_entity_locations_locations_id",
                table: "location_entity_representative_entity",
                column: "locations_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_location_entity_representative_entity_representatives_repre",
                table: "location_entity_representative_entity",
                column: "representatives_string_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_network_users_network_user_id",
                table: "network_user_representatives");

            migrationBuilder.DropForeignKey(
                name: "fk_network_user_representatives_representatives_representative",
                table: "network_user_representatives");

            migrationBuilder.DropPrimaryKey(
                name: "pk_network_user_representatives",
                table: "network_user_representatives");

            migrationBuilder.RenameTable(
                name: "network_user_representatives",
                newName: "network_user_entity_representative_entity");

            migrationBuilder.RenameColumn(
                name: "network_user_id",
                table: "network_user_entity_representative_entity",
                newName: "network_users_id");

            migrationBuilder.RenameColumn(
                name: "representative_id",
                table: "network_user_entity_representative_entity",
                newName: "representatives_string_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_network_user_entity_representative_entity",
                table: "network_user_entity_representative_entity",
                columns: new[] { "network_users_id", "representatives_string_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_entity_representative_entity_network_users_net",
                table: "network_user_entity_representative_entity",
                column: "network_users_id",
                principalTable: "network_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_network_user_entity_representative_entity_representatives_r",
                table: "network_user_entity_representative_entity",
                column: "representatives_string_id",
                principalTable: "representatives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
