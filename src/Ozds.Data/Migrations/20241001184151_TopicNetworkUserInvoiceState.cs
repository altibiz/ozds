using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class TopicNetworkUserInvoiceState : Migration
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
                .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state")
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                .OldAnnotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit,error,lifecycle")
                .OldAnnotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
                .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
                .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
                .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
                .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
                .OldAnnotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity,invalid_push,error,network_user_invoice_state")
                .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");
        }
    }
}
