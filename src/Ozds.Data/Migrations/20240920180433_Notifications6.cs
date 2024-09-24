using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notifications6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "kind",
                table: "notifications",
                type: "character varying(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(34)",
                oldMaxLength: 34);

            migrationBuilder.AddColumn<long>(
                name: "invoice_id",
                table: "notifications",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_notifications_invoice_id",
                table: "notifications",
                column: "invoice_id");

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_network_user_invoices_invoice_id",
                table: "notifications",
                column: "invoice_id",
                principalTable: "network_user_invoices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_notifications_network_user_invoices_invoice_id",
                table: "notifications");

            migrationBuilder.DropIndex(
                name: "ix_notifications_invoice_id",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "invoice_id",
                table: "notifications");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit")
                .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity")
                .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
                .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            migrationBuilder.AlterColumn<string>(
                name: "kind",
                table: "notifications",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55);
        }
    }
}
