using System.Collections.Generic;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable S3887
#pragma warning disable S2386

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class Notifications5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Handle enum label changes using raw SQL
            // Rename 'general' to 'all' in 'topic_entity' enum
            migrationBuilder.Sql(@"
                DO $$
                BEGIN
                    IF EXISTS (
                        SELECT 1 FROM pg_enum
                        WHERE enumlabel = 'general' AND enumtypid = 'topic_entity'::regtype
                    ) THEN
                        ALTER TYPE topic_entity RENAME VALUE 'general' TO 'all';
                    END IF;
                END
                $$;", suppressTransaction: true);
            // Add new enum labels to 'topic_entity' enum
            migrationBuilder.Sql("ALTER TYPE topic_entity ADD VALUE IF NOT EXISTS 'messenger';", suppressTransaction: true);
            migrationBuilder.Sql("ALTER TYPE topic_entity ADD VALUE IF NOT EXISTS 'messenger_inactivity';", suppressTransaction: true);

            // Create new enum types if they don't exist
            migrationBuilder.Sql("DO $$ BEGIN IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'category_entity') THEN CREATE TYPE category_entity AS ENUM ('all', 'messenger', 'messenger_push', 'audit'); END IF; END $$;", suppressTransaction: true);
            migrationBuilder.Sql("DO $$ BEGIN IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'duration_entity') THEN CREATE TYPE duration_entity AS ENUM ('second', 'minute', 'hour', 'day', 'week', 'month', 'year'); END IF; END $$;", suppressTransaction: true);

            // Drop existing foreign key
            migrationBuilder.DropForeignKey(
                name: "fk_messengers_locations_location_id",
                table: "messengers");

            // Drop existing column
            migrationBuilder.DropColumn(
                name: "topic",
                table: "notifications");

            // Update database annotations
            // migrationBuilder.AlterDatabase()
            //     .Annotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
            //     .Annotation("Npgsql:Enum:category_entity", "all,messenger,messenger_push,audit")
            //     .Annotation("Npgsql:Enum:duration_entity", "second,minute,hour,day,week,month,year")
            //     .Annotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
            //     .Annotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
            //     .Annotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
            //     .Annotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
            //     .Annotation("Npgsql:Enum:topic_entity", "all,messenger,messenger_inactivity")
            //     .Annotation("Npgsql:PostgresExtension:timescaledb", ",,")
            //     .OldAnnotation("Npgsql:Enum:audit_entity", "query,creation,modification,deletion")
            //     .OldAnnotation("Npgsql:Enum:interval_entity", "quarter_hour,day,month")
            //     .OldAnnotation("Npgsql:Enum:level_entity", "trace,debug,info,warning,error,critical")
            //     .OldAnnotation("Npgsql:Enum:phase_entity", "l1,l2,l3")
            //     .OldAnnotation("Npgsql:Enum:role_entity", "operator_representative,location_representative,network_user_representative")
            //     .OldAnnotation("Npgsql:Enum:topic_entity", "general")
            //     .OldAnnotation("Npgsql:PostgresExtension:timescaledb", ",,");

            // Apply data conversions from int to enum types
            EnumMappings.ApplyUpMigrations(migrationBuilder);

            // Recreate primary keys on 'aggregates' tables
            migrationBuilder.Sql(@"
                ALTER TABLE schneider_iem3xxx_aggregates
                ADD PRIMARY KEY (interval, timestamp, meter_id);
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE abb_b2x_aggregates
                ADD PRIMARY KEY (interval, timestamp, meter_id);
            ");

            // Add new columns
            migrationBuilder.AddColumn<string>(
                name: "messenger_id",
                table: "notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<HashSet<TopicEntity>>(
                name: "topics",
                table: "notifications",
                type: "topic_entity[]",
                nullable: false);

            migrationBuilder.AddColumn<HashSet<CategoryEntity>>(
                name: "categories",
                table: "events",
                type: "category_entity[]",
                nullable: false,
                defaultValue: new HashSet<CategoryEntity>() { CategoryEntity.All, CategoryEntity.Audit });

            migrationBuilder.AddColumn<string>(
                name: "kind",
                table: "messengers",
                type: "character varying(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "PidgeonMessengerEntity");

            migrationBuilder.AddColumn<DurationEntity>(
                name: "max_inactivity_period_duration",
                table: "messengers",
                type: "duration_entity",
                nullable: false,
                defaultValue: DurationEntity.Second);

            migrationBuilder.AddColumn<long>(
                name: "max_inactivity_period_multiplier",
                table: "messengers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DurationEntity>(
                name: "push_delay_period_duration",
                table: "messengers",
                type: "duration_entity",
                nullable: false,
                defaultValue: DurationEntity.Second);

            migrationBuilder.AddColumn<long>(
                name: "push_delay_period_multiplier",
                table: "messengers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            // Create index
            migrationBuilder.CreateIndex(
                name: "ix_notifications_messenger_id",
                table: "notifications",
                column: "messenger_id");

            // Add foreign keys
            migrationBuilder.AddForeignKey(
                name: "fk_messengers_locations__location_id",
                table: "messengers",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_notifications_messengers_messenger_id",
                table: "notifications",
                column: "messenger_id",
                principalTable: "messengers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys
            migrationBuilder.DropForeignKey(
                name: "fk_messengers_locations__location_id",
                table: "messengers");

            migrationBuilder.DropForeignKey(
                name: "fk_notifications_messengers_messenger_id",
                table: "notifications");

            // Drop index
            migrationBuilder.DropIndex(
                name: "ix_notifications_messenger_id",
                table: "notifications");

            // Drop new columns
            migrationBuilder.DropColumn(
                name: "messenger_id",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "topics",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "categories",
                table: "events");

            migrationBuilder.DropColumn(
                name: "kind",
                table: "messengers");

            migrationBuilder.DropColumn(
                name: "max_inactivity_period_duration",
                table: "messengers");

            migrationBuilder.DropColumn(
                name: "max_inactivity_period_multiplier",
                table: "messengers");

            migrationBuilder.DropColumn(
                name: "push_delay_period_duration",
                table: "messengers");

            migrationBuilder.DropColumn(
                name: "push_delay_period_multiplier",
                table: "messengers");

            // Revert data conversions from enum back to int types
            EnumMappings.ApplyDownMigrations(migrationBuilder);

            // Drop and recreate primary keys on 'aggregates' tables
            migrationBuilder.Sql(@"
                ALTER TABLE schneider_iem3xxx_aggregates
                DROP CONSTRAINT IF EXISTS schneider_iem3xxx_aggregates_pkey;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE schneider_iem3xxx_aggregates
                ADD PRIMARY KEY (interval, timestamp, meter_id);
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE abb_b2x_aggregates
                DROP CONSTRAINT IF EXISTS abb_b2x_aggregates_pkey;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE abb_b2x_aggregates
                ADD PRIMARY KEY (interval, timestamp, meter_id);
            ");

            // Revert enum label changes
            migrationBuilder.Sql("ALTER TYPE topic_entity RENAME VALUE 'all' TO 'general';");
            // Note: Dropping enum values is not directly supported in PostgreSQL; consider leaving them if not causing issues.

            // Re-add dropped column
            migrationBuilder.AddColumn<int>(
                name: "topic",
                table: "notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Re-add foreign key
            migrationBuilder.AddForeignKey(
                name: "fk_messengers_locations_location_id",
                table: "messengers",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        public static class EnumMappings
        {
            // Enum mappings between integer values and enum labels
            public static readonly Dictionary<int, string> AuditEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "query" },
                { 1, "creation" },
                { 2, "modification" },
                { 3, "deletion" }
            };

            public static readonly Dictionary<string, int> AuditEntityEnumToInt = new Dictionary<string, int>
            {
                { "query", 0 },
                { "creation", 1 },
                { "modification", 2 },
                { "deletion", 3 }
            };

            public static readonly Dictionary<int, string> CategoryEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "all" },
                { 1, "messenger" },
                { 2, "messenger_push" },
                { 3, "audit" }
            };

            public static readonly Dictionary<string, int> CategoryEntityEnumToInt = new Dictionary<string, int>
            {
                { "all", 0 },
                { "messenger", 1 },
                { "messenger_push", 2 },
                { "audit", 3 }
            };

            public static readonly Dictionary<int, string> DurationEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "second" },
                { 1, "minute" },
                { 2, "hour" },
                { 3, "day" },
                { 4, "week" },
                { 5, "month" },
                { 6, "year" }
            };

            public static readonly Dictionary<string, int> DurationEntityEnumToInt = new Dictionary<string, int>
            {
                { "second", 0 },
                { "minute", 1 },
                { "hour", 2 },
                { "day", 3 },
                { "week", 4 },
                { "month", 5 },
                { "year", 6 }
            };

            public static readonly Dictionary<int, string> IntervalEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "quarter_hour" },
                { 1, "day" },
                { 2, "month" }
            };

            public static readonly Dictionary<string, int> IntervalEntityEnumToInt = new Dictionary<string, int>
            {
                { "quarter_hour", 0 },
                { "day", 1 },
                { "month", 2 }
            };

            public static readonly Dictionary<int, string> LevelEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "trace" },
                { 1, "debug" },
                { 2, "info" },
                { 3, "warning" },
                { 4, "error" },
                { 5, "critical" }
            };

            public static readonly Dictionary<string, int> LevelEntityEnumToInt = new Dictionary<string, int>
            {
                { "trace", 0 },
                { "debug", 1 },
                { "info", 2 },
                { "warning", 3 },
                { "error", 4 },
                { "critical", 5 }
            };

            public static readonly Dictionary<int, string> PhaseEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "l1" },
                { 1, "l2" },
                { 2, "l3" }
            };

            public static readonly Dictionary<string, int> PhaseEntityEnumToInt = new Dictionary<string, int>
            {
                { "l1", 0 },
                { "l2", 1 },
                { "l3", 2 }
            };

            public static readonly Dictionary<int, string> RoleEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "operator_representative" },
                { 1, "location_representative" },
                { 2, "network_user_representative" }
            };

            public static readonly Dictionary<string, int> RoleEntityEnumToInt = new Dictionary<string, int>
            {
                { "operator_representative", 0 },
                { "location_representative", 1 },
                { "network_user_representative", 2 }
            };

            public static readonly Dictionary<int, string> TopicEntityIntToEnum = new Dictionary<int, string>
            {
                { 0, "all" },
                { 1, "messenger" },
                { 2, "messenger_inactivity" }
            };

            public static readonly Dictionary<string, int> TopicEntityEnumToInt = new Dictionary<string, int>
            {
                { "all", 0 },
                { "messenger", 1 },
                { "messenger_inactivity", 2 }
            };

            public static void ApplyUpMigrations(MigrationBuilder migrationBuilder)
            {
                // Convert 'interval' in 'schneider_iem3xxx_aggregates' from int to enum
                migrationBuilder.ConvertIntToEnum(
                    "schneider_iem3xxx_aggregates",
                    "interval",
                    IntervalEntityIntToEnum,
                    "interval_entity"
                );

                // Convert 'interval' in 'abb_b2x_aggregates' from int to enum
                migrationBuilder.ConvertIntToEnum(
                    "abb_b2x_aggregates",
                    "interval",
                    IntervalEntityIntToEnum,
                    "interval_entity"
                );

                // Convert int[] to enum[] for 'topics' in 'representatives'
                migrationBuilder.ConvertIntArrayToEnumArray(
                    "representatives",
                    "topics",
                    TopicEntityIntToEnum,
                    "topic_entity"
                );

                // Convert int to enum for 'role' in 'representatives'
                migrationBuilder.ConvertIntToEnum(
                    "representatives",
                    "role",
                    RoleEntityIntToEnum,
                    "role_entity"
                );

                // Convert int[] to enum[] for 'am_phases' in 'network_user_calculations'
                migrationBuilder.ConvertIntArrayToEnumArray(
                    "network_user_calculations",
                    "am_phases",
                    PhaseEntityIntToEnum,
                    "phase_entity"
                );

                // Convert int[] to enum[] for 'phases' in 'meters'
                migrationBuilder.ConvertIntArrayToEnumArray(
                    "meters",
                    "phases",
                    PhaseEntityIntToEnum,
                    "phase_entity"
                );

                // Convert int to enum for 'level' in 'events'
                migrationBuilder.ConvertIntToEnum(
                    "events",
                    "level",
                    LevelEntityIntToEnum,
                    "level_entity"
                );

                // Convert nullable int to enum for 'audit' in 'events'
                migrationBuilder.ConvertNullableIntToEnum(
                    "events",
                    "audit",
                    AuditEntityIntToEnum,
                    "audit_entity"
                );
            }

            public static void ApplyDownMigrations(MigrationBuilder migrationBuilder)
            {
                // Revert 'interval' in 'schneider_iem3xxx_aggregates' from enum to int
                migrationBuilder.ConvertEnumToInt(
                    "schneider_iem3xxx_aggregates",
                    "interval",
                    IntervalEntityEnumToInt
                );

                // Revert 'interval' in 'abb_b2x_aggregates' from enum to int
                migrationBuilder.ConvertEnumToInt(
                    "abb_b2x_aggregates",
                    "interval",
                    IntervalEntityEnumToInt
                );

                // Revert enum[] to int[] for 'topics' in 'representatives'
                migrationBuilder.ConvertEnumArrayToIntArray(
                    "representatives",
                    "topics",
                    TopicEntityEnumToInt
                );

                // Revert enum to int for 'role' in 'representatives'
                migrationBuilder.ConvertEnumToInt(
                    "representatives",
                    "role",
                    RoleEntityEnumToInt
                );

                // Revert enum[] to int[] for 'am_phases' in 'network_user_calculations'
                migrationBuilder.ConvertEnumArrayToIntArray(
                    "network_user_calculations",
                    "am_phases",
                    PhaseEntityEnumToInt
                );

                // Revert enum[] to int[] for 'phases' in 'meters'
                migrationBuilder.ConvertEnumArrayToIntArray(
                    "meters",
                    "phases",
                    PhaseEntityEnumToInt
                );

                // Revert enum to int for 'level' in 'events'
                migrationBuilder.ConvertEnumToInt(
                    "events",
                    "level",
                    LevelEntityEnumToInt
                );

                // Revert nullable enum to int for 'audit' in 'events'
                migrationBuilder.ConvertEnumToNullableInt(
                    "events",
                    "audit",
                    AuditEntityEnumToInt
                );
            }
        }
    }
}
