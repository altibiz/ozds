using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notifications4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_representative_entity");

            migrationBuilder.CreateTable(
                name: "notification_recipient_entity",
                columns: table => new
                {
                    representative_id = table.Column<string>(type: "text", nullable: false),
                    notification_id = table.Column<long>(type: "bigint", nullable: false),
                    seen_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_recipient_entity", x => new { x.representative_id, x.notification_id });
                    table.ForeignKey(
                        name: "fk_notification_recipient_entity_notifications_notification_id",
                        column: x => x.notification_id,
                        principalTable: "notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notification_recipient_entity_representatives_representativ",
                        column: x => x.representative_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_notification_recipient_entity__notification_id",
                table: "notification_recipient_entity",
                column: "notification_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_recipient_entity");

            migrationBuilder.CreateTable(
                name: "notification_representative_entity",
                columns: table => new
                {
                    representative_id = table.Column<string>(type: "text", nullable: false),
                    notification_id = table.Column<long>(type: "bigint", nullable: false),
                    seen_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification_representative_entity", x => new { x.representative_id, x.notification_id });
                    table.ForeignKey(
                        name: "fk_notification_representative_entity_notifications_notificati",
                        column: x => x.notification_id,
                        principalTable: "notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notification_representative_entity_representatives_represen",
                        column: x => x.representative_id,
                        principalTable: "representatives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_notification_representative_entity__notification_id",
                table: "notification_representative_entity",
                column: "notification_id");
        }
    }
}
