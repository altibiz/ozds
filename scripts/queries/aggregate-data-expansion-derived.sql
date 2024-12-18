begin transaction;

select
  interval,
  measurement_location_id,
  meter_id,
  timestamp,
  derived_active_power_total_import_t0_min_w min,
  derived_active_power_total_import_t0_max_w max,
  derived_active_power_total_import_t0_avg_w avg,
  derived_active_power_total_import_t0_min_timestamp mints,
  derived_active_power_total_import_t0_max_timestamp maxts
from
  abb_b2x_aggregates;

update abb_b2x_aggregates aggregates
set
  derived_active_power_total_import_t0_min_w = quarter_hours.min_value,
  derived_active_power_total_import_t0_max_w = quarter_hours.max_value,
  derived_active_power_total_import_t0_avg_w = quarter_hours.avg_value
from
  (
    select
      quarter_hours.meter_id,
      quarter_hours.measurement_location_id,
      min(
        quarter_hours.derived_active_power_total_import_t0_w
      ) min_value,
      max(
        quarter_hours.derived_active_power_total_import_t0_w
      ) max_value,
      avg(
        quarter_hours.derived_active_power_total_import_t0_w
      ) avg_value
    from
      (
        select
          quarter_hours.meter_id,
          quarter_hours.measurement_location_id,
          (
            (
              quarter_hours.active_energy_total_import_t0_max_wh - quarter_hours.active_energy_total_import_t0_min_wh
            ) * 4
          ) derived_active_power_total_import_t0_w
        from
          abb_b2x_aggregates quarter_hours
          join abb_b2x_aggregates aggregates on quarter_hours.meter_id = aggregates.meter_id
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

update abb_b2x_aggregates aggregates
set
  derived_active_power_total_import_t0_min_timestamp = quarter_hours.timestamp
from
  (
    select
      quarter_hours.meter_id,
      quarter_hours.measurement_location_id,
      quarter_hours.timestamp,
      (
        (
          quarter_hours.active_energy_total_import_t0_max_wh - quarter_hours.active_energy_total_import_t0_min_wh
        ) * 4
      ) derived_active_power_total_import_t0_w
    from
      abb_b2x_aggregates quarter_hours
      join abb_b2x_aggregates aggregates on quarter_hours.meter_id = aggregates.meter_id
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
  and quarter_hours.derived_active_power_total_import_t0_w = aggregates.derived_active_power_total_import_t0_min_w;

update abb_b2x_aggregates aggregates
set
  derived_active_power_total_import_t0_max_timestamp = quarter_hours.timestamp
from
  (
    select
      quarter_hours.meter_id,
      quarter_hours.measurement_location_id,
      quarter_hours.timestamp,
      (
        (
          quarter_hours.active_energy_total_import_t0_max_wh - quarter_hours.active_energy_total_import_t0_min_wh
        ) * 4
      ) derived_active_power_total_import_t0_w
    from
      abb_b2x_aggregates quarter_hours
      join abb_b2x_aggregates aggregates on quarter_hours.meter_id = aggregates.meter_id
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
  and quarter_hours.derived_active_power_total_import_t0_w = aggregates.derived_active_power_total_import_t0_max_w;

select
  interval,
  measurement_location_id,
  meter_id,
  timestamp,
  derived_active_power_total_import_t0_min_w min,
  derived_active_power_total_import_t0_max_w max,
  derived_active_power_total_import_t0_avg_w avg,
  derived_active_power_total_import_t0_min_timestamp mints,
  derived_active_power_total_import_t0_max_timestamp maxts
from
  abb_b2x_aggregates;

rollback;
