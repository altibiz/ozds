using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggregateDataExpansion6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var (columnPrefix, unit, derivedColumnPrefix, derivedUnit) in new[]
            {
                ("active_energy_l1_import_t0", "wh", "derived_active_power_l1_import_t0", "w"),
                ("active_energy_l2_import_t0", "wh", "derived_active_power_l2_import_t0", "w"),
                ("active_energy_l3_import_t0", "wh", "derived_active_power_l3_import_t0", "w"),
                ("active_energy_l1_export_t0", "wh", "derived_active_power_l1_export_t0", "w"),
                ("active_energy_l2_export_t0", "wh", "derived_active_power_l2_export_t0", "w"),
                ("active_energy_l3_export_t0", "wh", "derived_active_power_l3_export_t0", "w"),
                ("reactive_energy_l1_import_t0", "varh", "derived_reactive_power_l1_import_t0", "var"),
                ("reactive_energy_l2_import_t0", "varh", "derived_reactive_power_l2_import_t0", "var"),
                ("reactive_energy_l3_import_t0", "varh", "derived_reactive_power_l3_import_t0", "var"),
                ("reactive_energy_l1_export_t0", "varh", "derived_reactive_power_l1_export_t0", "var"),
                ("reactive_energy_l2_export_t0", "varh", "derived_reactive_power_l2_export_t0", "var"),
                ("reactive_energy_l3_export_t0", "varh", "derived_reactive_power_l3_export_t0", "var"),
            })
            {
                var tablePrefix = "abb_b2x";
                CalculateDerivedValues(
                    migrationBuilder,
                    tablePrefix,
                    columnPrefix,
                    unit,
                    derivedColumnPrefix,
                    derivedUnit
                );
            }

            foreach (var (columnPrefix, unit, derivedColumnPrefix, derivedUnit) in new[]
            {
                ("active_energy_l1_import_t0", "wh", "derived_active_power_l1_import_t0", "w"),
                ("active_energy_l2_import_t0", "wh", "derived_active_power_l2_import_t0", "w"),
                ("active_energy_l3_import_t0", "wh", "derived_active_power_l3_import_t0", "w"),
            })
            {
                var tablePrefix = "schneider_iem3xxx";
                CalculateDerivedValues(
                    migrationBuilder,
                    tablePrefix,
                    columnPrefix,
                    unit,
                    derivedColumnPrefix,
                    derivedUnit
                );
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

        private static void CalculateDerivedValues(
            MigrationBuilder migrationBuilder,
            string tablePrefix,
            string columnPrefix,
            string unit,
            string derivedColumnPrefix,
            string derivedUnit
        )
        {
            migrationBuilder.Sql($@"
                update {tablePrefix}_aggregates aggregates
                set
                    {derivedColumnPrefix}_min_{derivedUnit} = quarter_hours.min_value,
                    {derivedColumnPrefix}_max_{derivedUnit} = quarter_hours.max_value,
                    {derivedColumnPrefix}_avg_{derivedUnit} = quarter_hours.avg_value
                from
                    (
                        select
                            quarter_hours.meter_id,
                            quarter_hours.measurement_location_id,
                            min(
                                quarter_hours.{derivedColumnPrefix}_{derivedUnit}
                            ) min_value,
                            max(
                                quarter_hours.{derivedColumnPrefix}_{derivedUnit}
                            ) max_value,
                            avg(
                                quarter_hours.{derivedColumnPrefix}_{derivedUnit}
                            ) avg_value
                        from
                            (
                                select
                                    quarter_hours.meter_id,
                                    quarter_hours.measurement_location_id,
                                    (
                                        (
                                        quarter_hours.{columnPrefix}_max_{unit} - quarter_hours.{columnPrefix}_min_{unit}
                                        ) * 4
                                    ) {derivedColumnPrefix}_{derivedUnit}
                                from
                                    {tablePrefix}_aggregates quarter_hours
                                    join {tablePrefix}_aggregates aggregates on quarter_hours.meter_id = aggregates.meter_id
                                    and quarter_hours.measurement_location_id = aggregates.measurement_location_id
                                where
                                    quarter_hours.interval = 'quarter_hour'
                                    and quarter_hours.timestamp >= aggregates.timestamp
                                    and quarter_hours.timestamp < (
                                        aggregates.timestamp + (
                                            case
                                                when aggregates.interval = 'quarter_hour' then (interval '15 minutes')
                                                when aggregates.interval = 'day' then (interval '1 day')
                                                when aggregates.interval = 'month' then (interval '1 month')
                                            end
                                        )
                                    )
                            ) quarter_hours
                        group by
                            quarter_hours.meter_id,
                            quarter_hours.measurement_location_id
                    ) quarter_hours
                where
                    quarter_hours.meter_id = aggregates.meter_id
                    and quarter_hours.measurement_location_id = aggregates.measurement_location_id;

                update {tablePrefix}_aggregates aggregates
                set
                    {derivedColumnPrefix}_min_timestamp = quarter_hours.timestamp
                from
                    (
                        select
                            quarter_hours.meter_id,
                            quarter_hours.measurement_location_id,
                            quarter_hours.timestamp,
                            (
                                (
                                quarter_hours.{columnPrefix}_max_{unit} - quarter_hours.{columnPrefix}_min_{unit}
                                ) * 4
                            ) {derivedColumnPrefix}_{derivedUnit}
                        from
                            {tablePrefix}_aggregates quarter_hours
                            join {tablePrefix}_aggregates aggregates on quarter_hours.meter_id = aggregates.meter_id
                            and quarter_hours.measurement_location_id = aggregates.measurement_location_id
                        where
                            quarter_hours.interval = 'quarter_hour'
                            and quarter_hours.timestamp >= aggregates.timestamp
                            and quarter_hours.timestamp < (
                                aggregates.timestamp + (
                                    case
                                        when aggregates.interval = 'quarter_hour' then (interval '15 minutes')
                                        when aggregates.interval = 'day' then (interval '1 day')
                                        when aggregates.interval = 'month' then (interval '1 month')
                                    end
                                )
                            )
                    ) quarter_hours
                where
                    quarter_hours.meter_id = aggregates.meter_id
                    and quarter_hours.measurement_location_id = aggregates.measurement_location_id
                    and quarter_hours.{derivedColumnPrefix}_{derivedUnit} = aggregates.{derivedColumnPrefix}_min_{derivedUnit};

                update {tablePrefix}_aggregates aggregates
                set
                    {derivedColumnPrefix}_max_timestamp = quarter_hours.timestamp
                from
                    (
                        select
                            quarter_hours.meter_id,
                            quarter_hours.measurement_location_id,
                            quarter_hours.timestamp,
                            (
                                (
                                quarter_hours.{columnPrefix}_max_{unit} - quarter_hours.{columnPrefix}_min_{unit}
                                ) * 4
                            ) {derivedColumnPrefix}_{derivedUnit}
                        from
                            {tablePrefix}_aggregates quarter_hours
                            join {tablePrefix}_aggregates aggregates on quarter_hours.meter_id = aggregates.meter_id
                            and quarter_hours.measurement_location_id = aggregates.measurement_location_id
                        where
                            quarter_hours.interval = 'quarter_hour'
                            and quarter_hours.timestamp >= aggregates.timestamp
                            and quarter_hours.timestamp < (
                                aggregates.timestamp + (
                                    case
                                        when aggregates.interval = 'quarter_hour' then (interval '15 minutes')
                                        when aggregates.interval = 'day' then (interval '1 day')
                                        when aggregates.interval = 'month' then (interval '1 month')
                                    end
                                )
                            )
                    ) quarter_hours
                where
                    quarter_hours.meter_id = aggregates.meter_id
                    and quarter_hours.measurement_location_id = aggregates.measurement_location_id
                    and quarter_hours.{derivedColumnPrefix}_{derivedUnit} = aggregates.{derivedColumnPrefix}_max_{derivedUnit};
            ");
        }
    }
}
