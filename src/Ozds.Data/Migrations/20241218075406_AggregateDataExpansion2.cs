using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ozds.Data.Migrations
{
    /// <inheritdoc />
    public partial class AggregateDataExpansion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var (columnPrefix, unit) in new[]
            {
                ("voltage_l1_any_t0", "v"),
                ("voltage_l2_any_t0", "v"),
                ("voltage_l3_any_t0", "v"),
                ("current_l1_any_t0", "a"),
                ("current_l2_any_t0", "a"),
                ("current_l3_any_t0", "a"),
                ("active_power_l1_net_t0", "w"),
                ("active_power_l2_net_t0", "w"),
                ("active_power_l3_net_t0", "w"),
                ("reactive_power_l1_net_t0", "var"),
                ("reactive_power_l2_net_t0", "var"),
                ("reactive_power_l3_net_t0", "var"),
            })
            {
                var tablePrefix = "abb_b2x";
                CalculateMinMaxAvg(
                    migrationBuilder,
                    tablePrefix,
                    columnPrefix,
                    unit
                );
            }

            foreach (var (columnPrefix, unit) in new[]
            {
                ("voltage_l1_any_t0", "v"),
                ("voltage_l2_any_t0", "v"),
                ("voltage_l3_any_t0", "v"),
                ("current_l1_any_t0", "a"),
                ("current_l2_any_t0", "a"),
                ("current_l3_any_t0", "a"),
                ("active_power_l1_net_t0", "w"),
                ("active_power_l2_net_t0", "w"),
                ("active_power_l3_net_t0", "w"),
                ("reactive_power_total_net_t0", "var"),
                ("apparent_power_total_net_t0", "va"),
            })
            {
                var tablePrefix = "schneider_iem3xxx";
                CalculateMinMaxAvg(
                    migrationBuilder,
                    tablePrefix,
                    columnPrefix,
                    unit
                );
            }

            foreach (var (columnPrefix, unit, derivedColumnPrefix, derivedUnit) in new[]
            {
                ("active_energy_total_import_t0", "wh", "derived_active_power_total_import_t0", "w"),
                ("active_energy_total_export_t0", "wh", "derived_active_power_total_export_t0", "w"),
                ("reactive_energy_total_import_t0", "varh", "derived_reactive_power_total_import_t0", "var"),
                ("reactive_energy_total_export_t0", "varh", "derived_reactive_power_total_export_t0", "var"),
                ("active_energy_total_import_t1", "wh", "derived_active_power_total_import_t1", "w"),
                ("active_energy_total_import_t2", "wh", "derived_active_power_total_import_t2", "w"),
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
                ("active_energy_total_import_t0", "wh", "derived_active_power_total_import_t0", "w"),
                ("active_energy_total_export_t0", "wh", "derived_active_power_total_export_t0", "w"),
                ("reactive_energy_total_import_t0", "varh", "derived_reactive_power_total_import_t0", "var"),
                ("reactive_energy_total_export_t0", "varh", "derived_reactive_power_total_export_t0", "var"),
                ("active_energy_total_import_t1", "wh", "derived_active_power_total_import_t1", "w"),
                ("active_energy_total_import_t2", "wh", "derived_active_power_total_import_t2", "w"),
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

        private static void CalculateMinMaxAvg(
            MigrationBuilder migrationBuilder,
            string tablePrefix,
            string columnPrefix,
            string unit
        )
        {
            migrationBuilder.Sql($@"
                update {tablePrefix}_aggregates aggregates
                set
                    {columnPrefix}_min_{unit} = measurements.min_value,
                    {columnPrefix}_max_{unit} = measurements.max_value,
                    {columnPrefix}_avg_{unit} = measurements.avg_value
                from
                    (
                        select
                            measurements.meter_id,
                            measurements.measurement_location_id,
                            min(measurements.{columnPrefix}_{unit}) min_value,
                            max(measurements.{columnPrefix}_{unit}) max_value,
                            avg(measurements.{columnPrefix}_{unit}) avg_value
                        from
                            {tablePrefix}_measurements measurements
                            join {tablePrefix}_aggregates aggregates on measurements.meter_id = aggregates.meter_id
                            and measurements.measurement_location_id = aggregates.measurement_location_id
                        where
                            measurements.timestamp >= aggregates.timestamp
                            and measurements.timestamp < (
                                aggregates.timestamp + (
                                    case
                                        when aggregates.interval = 'quarter_hour' then (interval '15 minutes')
                                        when aggregates.interval = 'day' then (interval '1 day')
                                        when aggregates.interval = 'month' then (interval '1 month')
                                    end
                                )
                            )
                        group by
                            measurements.meter_id,
                            measurements.measurement_location_id
                    ) measurements
                where
                    measurements.meter_id = aggregates.meter_id
                    and measurements.measurement_location_id = aggregates.measurement_location_id;

                update {tablePrefix}_aggregates aggregates
                set
                    {columnPrefix}_min_timestamp = measurements.timestamp
                from
                    (
                        select
                            measurements.meter_id,
                            measurements.measurement_location_id,
                            measurements.timestamp
                        from
                            {tablePrefix}_measurements measurements
                            join {tablePrefix}_aggregates aggregates on measurements.meter_id = aggregates.meter_id
                            and measurements.measurement_location_id = aggregates.measurement_location_id
                        where
                            measurements.{columnPrefix}_{unit} = aggregates.{columnPrefix}_min_{unit}
                            and measurements.timestamp >= aggregates.timestamp
                            and measurements.timestamp < (
                                aggregates.timestamp + (
                                    case
                                        when aggregates.interval = 'quarter_hour' then (interval '15 minutes')
                                        when aggregates.interval = 'day' then (interval '1 day')
                                        when aggregates.interval = 'month' then (interval '1 month')
                                    end
                                )
                            )
                    ) measurements
                where
                    measurements.meter_id = aggregates.meter_id
                    and measurements.measurement_location_id = aggregates.measurement_location_id;

                update {tablePrefix}_aggregates aggregates
                set
                    {columnPrefix}_max_timestamp = measurements.timestamp
                from
                    (
                        select
                            measurements.meter_id,
                            measurements.measurement_location_id,
                            measurements.timestamp
                        from
                            {tablePrefix}_measurements measurements
                            join {tablePrefix}_aggregates aggregates on measurements.meter_id = aggregates.meter_id
                            and measurements.measurement_location_id = aggregates.measurement_location_id
                        where
                            measurements.{columnPrefix}_{unit} = aggregates.{columnPrefix}_max_{unit}
                            and measurements.timestamp >= aggregates.timestamp
                            and measurements.timestamp < (
                                aggregates.timestamp + (
                                    case
                                        when aggregates.interval = 'quarter_hour' then (interval '15 minutes')
                                        when aggregates.interval = 'day' then (interval '1 day')
                                        when aggregates.interval = 'month' then (interval '1 month')
                                    end
                                    )
                            )
                    ) measurements
                where
                    measurements.meter_id = aggregates.meter_id
                    and measurements.measurement_location_id = aggregates.measurement_location_id;
            ");
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
