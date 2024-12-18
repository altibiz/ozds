begin transaction;

select
  interval,
  measurement_location_id,
  meter_id,
  timestamp,
  active_power_l1_net_t0_min_w min,
  active_power_l1_net_t0_max_w max,
  active_power_l1_net_t0_avg_w avg,
  active_power_l1_net_t0_min_timestamp mints,
  active_power_l1_net_t0_max_timestamp maxts
from
  abb_b2x_aggregates;

update abb_b2x_aggregates aggregates
set
  active_power_l1_net_t0_min_w = measurements.min_value,
  active_power_l1_net_t0_max_w = measurements.max_value,
  active_power_l1_net_t0_avg_w = measurements.avg_value
from
  (
    select
      measurements.meter_id,
      measurements.measurement_location_id,
      min(measurements.active_power_l1_net_t0_w) min_value,
      max(measurements.active_power_l1_net_t0_w) max_value,
      avg(measurements.active_power_l1_net_t0_w) avg_value
    from
      abb_b2x_measurements measurements
      join abb_b2x_aggregates aggregates on measurements.meter_id = aggregates.meter_id
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

update abb_b2x_aggregates aggregates
set
  active_power_l1_net_t0_min_timestamp = measurements.timestamp
from
  (
    select
      measurements.meter_id,
      measurements.measurement_location_id,
      measurements.timestamp
    from
      abb_b2x_measurements measurements
      join abb_b2x_aggregates aggregates on measurements.meter_id = aggregates.meter_id
      and measurements.measurement_location_id = aggregates.measurement_location_id
    where
      measurements.active_power_l1_net_t0_w = aggregates.active_power_l1_net_t0_min_w
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

update abb_b2x_aggregates aggregates
set
  active_power_l1_net_t0_max_timestamp = measurements.timestamp
from
  (
    select
      measurements.meter_id,
      measurements.measurement_location_id,
      measurements.timestamp
    from
      abb_b2x_measurements measurements
      join abb_b2x_aggregates aggregates on measurements.meter_id = aggregates.meter_id
      and measurements.measurement_location_id = aggregates.measurement_location_id
    where
      measurements.active_power_l1_net_t0_w = aggregates.active_power_l1_net_t0_max_w
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

select
  interval,
  measurement_location_id,
  meter_id,
  timestamp,
  active_power_l1_net_t0_min_w min,
  active_power_l1_net_t0_max_w max,
  active_power_l1_net_t0_avg_w avg,
  active_power_l1_net_t0_min_timestamp mints,
  active_power_l1_net_t0_max_timestamp maxts
from
  abb_b2x_aggregates;

rollback;
