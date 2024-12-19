begin transaction;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  quarter_hour_count
from
  abb_b2x_aggregates;

update abb_b2x_aggregates
set
  quarter_hour_count = 1
where
  interval = 'quarter_hour'::interval_entity;

update abb_b2x_aggregates
set
  quarter_hour_count = quarter_hour_count.count,
  timestamp = date_trunc(
    'day',
    abb_b2x_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
from
  (
    select
      meter_id,
      measurement_location_id,
      time_bucket ('1 day', timestamp at time zone 'Europe/Zagreb') as timestamp,
      sum(abb_b2x_aggregates.quarter_hour_count) as count
    from
      abb_b2x_aggregates
    where
      interval = 'quarter_hour'::interval_entity
    group by
      timestamp,
      meter_id,
      measurement_location_id
  ) quarter_hour_count
where
  interval = 'day'::interval_entity
  and quarter_hour_count.timestamp = date_trunc(
    'day',
    abb_b2x_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
  and quarter_hour_count.meter_id = abb_b2x_aggregates.meter_id
  and quarter_hour_count.measurement_location_id = abb_b2x_aggregates.measurement_location_id;

update abb_b2x_aggregates
set
  quarter_hour_count = quarter_hour_count.count,
  timestamp = date_trunc(
    'month',
    abb_b2x_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
from
  (
    select
      meter_id,
      measurement_location_id,
      time_bucket ('1 month', timestamp at time zone 'Europe/Zagreb') as timestamp,
      sum(abb_b2x_aggregates.quarter_hour_count) as count
    from
      abb_b2x_aggregates
    where
      interval = 'quarter_hour'::interval_entity
    group by
      timestamp,
      meter_id,
      measurement_location_id
  ) quarter_hour_count
where
  interval = 'month'::interval_entity
  and quarter_hour_count.timestamp = date_trunc(
    'month',
    abb_b2x_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
  and quarter_hour_count.meter_id = abb_b2x_aggregates.meter_id
  and quarter_hour_count.measurement_location_id = abb_b2x_aggregates.measurement_location_id;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  quarter_hour_count
from
  abb_b2x_aggregates;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  quarter_hour_count
from
  schneider_iem3xxx_aggregates;

update schneider_iem3xxx_aggregates
set
  quarter_hour_count = 1
where
  interval = 'quarter_hour'::interval_entity;

update schneider_iem3xxx_aggregates
set
  quarter_hour_count = quarter_hour_count.count,
  timestamp = date_trunc(
    'day',
    schneider_iem3xxx_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
from
  (
    select
      meter_id,
      measurement_location_id,
      time_bucket ('1 day', timestamp at time zone 'Europe/Zagreb') as timestamp,
      sum(schneider_iem3xxx_aggregates.quarter_hour_count) as count
    from
      schneider_iem3xxx_aggregates
    where
      interval = 'quarter_hour'::interval_entity
    group by
      timestamp,
      meter_id,
      measurement_location_id
  ) quarter_hour_count
where
  interval = 'day'::interval_entity
  and quarter_hour_count.timestamp = date_trunc(
    'day',
    schneider_iem3xxx_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
  and quarter_hour_count.meter_id = schneider_iem3xxx_aggregates.meter_id
  and quarter_hour_count.measurement_location_id = schneider_iem3xxx_aggregates.measurement_location_id;

update schneider_iem3xxx_aggregates
set
  quarter_hour_count = quarter_hour_count.count,
  timestamp = date_trunc(
    'month',
    schneider_iem3xxx_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
from
  (
    select
      meter_id,
      measurement_location_id,
      time_bucket ('1 month', timestamp at time zone 'Europe/Zagreb') as timestamp,
      sum(schneider_iem3xxx_aggregates.quarter_hour_count) as count
    from
      schneider_iem3xxx_aggregates
    where
      interval = 'quarter_hour'::interval_entity
    group by
      timestamp,
      meter_id,
      measurement_location_id
  ) quarter_hour_count
where
  interval = 'month'::interval_entity
  and quarter_hour_count.timestamp = date_trunc(
    'month',
    schneider_iem3xxx_aggregates.timestamp at time zone 'Europe/Zagreb'
  )
  and quarter_hour_count.meter_id = schneider_iem3xxx_aggregates.meter_id
  and quarter_hour_count.measurement_location_id = schneider_iem3xxx_aggregates.measurement_location_id;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  quarter_hour_count
from
  schneider_iem3xxx_aggregates;

rollback;
