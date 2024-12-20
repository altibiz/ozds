begin transaction;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count
from
  abb_b2x_aggregates
where
  abb_b2x_aggregates.meter_id =:'p28_meter_id'
  and abb_b2x_aggregates.measurement_location_id =:'p28_measurement_location_id';

insert into
  abb_b2x_aggregates (
    timestamp,
    interval,
    meter_id,
    measurement_location_id,
    count,
    quarter_hour_count,
    active_energy_l1_export_t0_max_wh,
    active_energy_l1_export_t0_min_wh,
    active_energy_l1_import_t0_max_wh,
    active_energy_l1_import_t0_min_wh,
    active_energy_l2_export_t0_max_wh,
    active_energy_l2_export_t0_min_wh,
    active_energy_l2_import_t0_max_wh,
    active_energy_l2_import_t0_min_wh,
    active_energy_l3_export_t0_max_wh,
    active_energy_l3_export_t0_min_wh,
    active_energy_l3_import_t0_max_wh,
    active_energy_l3_import_t0_min_wh,
    active_energy_total_export_t0_max_wh,
    active_energy_total_export_t0_min_wh,
    active_energy_total_import_t0_max_wh,
    active_energy_total_import_t0_min_wh,
    active_energy_total_import_t1_max_wh,
    active_energy_total_import_t1_min_wh,
    active_energy_total_import_t2_max_wh,
    active_energy_total_import_t2_min_wh,
    active_power_l1_net_t0_avg_w,
    active_power_l1_net_t0_max_w,
    active_power_l1_net_t0_max_timestamp,
    active_power_l1_net_t0_min_w,
    active_power_l1_net_t0_min_timestamp,
    active_power_l2_net_t0_avg_w,
    active_power_l2_net_t0_max_w,
    active_power_l2_net_t0_max_timestamp,
    active_power_l2_net_t0_min_w,
    active_power_l2_net_t0_min_timestamp,
    active_power_l3_net_t0_avg_w,
    active_power_l3_net_t0_max_w,
    active_power_l3_net_t0_max_timestamp,
    active_power_l3_net_t0_min_w,
    active_power_l3_net_t0_min_timestamp,
    current_l1_any_t0_avg_a,
    current_l1_any_t0_max_a,
    current_l1_any_t0_max_timestamp,
    current_l1_any_t0_min_a,
    current_l1_any_t0_min_timestamp,
    current_l2_any_t0_avg_a,
    current_l2_any_t0_max_a,
    current_l2_any_t0_max_timestamp,
    current_l2_any_t0_min_a,
    current_l2_any_t0_min_timestamp,
    current_l3_any_t0_avg_a,
    current_l3_any_t0_max_a,
    current_l3_any_t0_max_timestamp,
    current_l3_any_t0_min_a,
    current_l3_any_t0_min_timestamp,
    derived_active_power_l1_export_t0_avg_w,
    derived_active_power_l1_export_t0_max_w,
    derived_active_power_l1_export_t0_max_timestamp,
    derived_active_power_l1_export_t0_min_w,
    derived_active_power_l1_export_t0_min_timestamp,
    derived_active_power_l1_import_t0_avg_w,
    derived_active_power_l1_import_t0_max_w,
    derived_active_power_l1_import_t0_max_timestamp,
    derived_active_power_l1_import_t0_min_w,
    derived_active_power_l1_import_t0_min_timestamp,
    derived_active_power_l2_export_t0_avg_w,
    derived_active_power_l2_export_t0_max_w,
    derived_active_power_l2_export_t0_max_timestamp,
    derived_active_power_l2_export_t0_min_w,
    derived_active_power_l2_export_t0_min_timestamp,
    derived_active_power_l2_import_t0_avg_w,
    derived_active_power_l2_import_t0_max_w,
    derived_active_power_l2_import_t0_max_timestamp,
    derived_active_power_l2_import_t0_min_w,
    derived_active_power_l2_import_t0_min_timestamp,
    derived_active_power_l3_export_t0_avg_w,
    derived_active_power_l3_export_t0_max_w,
    derived_active_power_l3_export_t0_max_timestamp,
    derived_active_power_l3_export_t0_min_w,
    derived_active_power_l3_export_t0_min_timestamp,
    derived_active_power_l3_import_t0_avg_w,
    derived_active_power_l3_import_t0_max_w,
    derived_active_power_l3_import_t0_max_timestamp,
    derived_active_power_l3_import_t0_min_w,
    derived_active_power_l3_import_t0_min_timestamp,
    derived_active_power_total_export_t0_avg_w,
    derived_active_power_total_export_t0_max_w,
    derived_active_power_total_export_t0_max_timestamp,
    derived_active_power_total_export_t0_min_w,
    derived_active_power_total_export_t0_min_timestamp,
    derived_active_power_total_import_t0_avg_w,
    derived_active_power_total_import_t0_max_w,
    derived_active_power_total_import_t0_max_timestamp,
    derived_active_power_total_import_t0_min_w,
    derived_active_power_total_import_t0_min_timestamp,
    derived_active_power_total_import_t1_avg_w,
    derived_active_power_total_import_t1_max_w,
    derived_active_power_total_import_t1_max_timestamp,
    derived_active_power_total_import_t1_min_w,
    derived_active_power_total_import_t1_min_timestamp,
    derived_active_power_total_import_t2_avg_w,
    derived_active_power_total_import_t2_max_w,
    derived_active_power_total_import_t2_max_timestamp,
    derived_active_power_total_import_t2_min_w,
    derived_active_power_total_import_t2_min_timestamp,
    derived_reactive_power_l1_export_t0_avg_var,
    derived_reactive_power_l1_export_t0_max_var,
    derived_reactive_power_l1_export_t0_max_timestamp,
    derived_reactive_power_l1_export_t0_min_var,
    derived_reactive_power_l1_export_t0_min_timestamp,
    derived_reactive_power_l1_import_t0_avg_var,
    derived_reactive_power_l1_import_t0_max_var,
    derived_reactive_power_l1_import_t0_max_timestamp,
    derived_reactive_power_l1_import_t0_min_var,
    derived_reactive_power_l1_import_t0_min_timestamp,
    derived_reactive_power_l2_export_t0_avg_var,
    derived_reactive_power_l2_export_t0_max_var,
    derived_reactive_power_l2_export_t0_max_timestamp,
    derived_reactive_power_l2_export_t0_min_var,
    derived_reactive_power_l2_export_t0_min_timestamp,
    derived_reactive_power_l2_import_t0_avg_var,
    derived_reactive_power_l2_import_t0_max_var,
    derived_reactive_power_l2_import_t0_max_timestamp,
    derived_reactive_power_l2_import_t0_min_var,
    derived_reactive_power_l2_import_t0_min_timestamp,
    derived_reactive_power_l3_export_t0_avg_var,
    derived_reactive_power_l3_export_t0_max_var,
    derived_reactive_power_l3_export_t0_max_timestamp,
    derived_reactive_power_l3_export_t0_min_var,
    derived_reactive_power_l3_export_t0_min_timestamp,
    derived_reactive_power_l3_import_t0_avg_var,
    derived_reactive_power_l3_import_t0_max_var,
    derived_reactive_power_l3_import_t0_max_timestamp,
    derived_reactive_power_l3_import_t0_min_var,
    derived_reactive_power_l3_import_t0_min_timestamp,
    derived_reactive_power_total_export_t0_avg_var,
    derived_reactive_power_total_export_t0_max_var,
    derived_reactive_power_total_export_t0_max_timestamp,
    derived_reactive_power_total_export_t0_min_var,
    derived_reactive_power_total_export_t0_min_timestamp,
    derived_reactive_power_total_import_t0_avg_var,
    derived_reactive_power_total_import_t0_max_var,
    derived_reactive_power_total_import_t0_max_timestamp,
    derived_reactive_power_total_import_t0_min_var,
    derived_reactive_power_total_import_t0_min_timestamp,
    reactive_energy_l1_export_t0_max_varh,
    reactive_energy_l1_export_t0_min_varh,
    reactive_energy_l1_import_t0_max_varh,
    reactive_energy_l1_import_t0_min_varh,
    reactive_energy_l2_export_t0_max_varh,
    reactive_energy_l2_export_t0_min_varh,
    reactive_energy_l2_import_t0_max_varh,
    reactive_energy_l2_import_t0_min_varh,
    reactive_energy_l3_export_t0_max_varh,
    reactive_energy_l3_export_t0_min_varh,
    reactive_energy_l3_import_t0_max_varh,
    reactive_energy_l3_import_t0_min_varh,
    reactive_energy_total_export_t0_max_varh,
    reactive_energy_total_export_t0_min_varh,
    reactive_energy_total_import_t0_max_varh,
    reactive_energy_total_import_t0_min_varh,
    reactive_power_l1_net_t0_avg_var,
    reactive_power_l1_net_t0_max_var,
    reactive_power_l1_net_t0_max_timestamp,
    reactive_power_l1_net_t0_min_var,
    reactive_power_l1_net_t0_min_timestamp,
    reactive_power_l2_net_t0_avg_var,
    reactive_power_l2_net_t0_max_var,
    reactive_power_l2_net_t0_max_timestamp,
    reactive_power_l2_net_t0_min_var,
    reactive_power_l2_net_t0_min_timestamp,
    reactive_power_l3_net_t0_avg_var,
    reactive_power_l3_net_t0_max_var,
    reactive_power_l3_net_t0_max_timestamp,
    reactive_power_l3_net_t0_min_var,
    reactive_power_l3_net_t0_min_timestamp,
    voltage_l1_any_t0_avg_v,
    voltage_l1_any_t0_max_v,
    voltage_l1_any_t0_max_timestamp,
    voltage_l1_any_t0_min_v,
    voltage_l1_any_t0_min_timestamp,
    voltage_l2_any_t0_avg_v,
    voltage_l2_any_t0_max_v,
    voltage_l2_any_t0_max_timestamp,
    voltage_l2_any_t0_min_v,
    voltage_l2_any_t0_min_timestamp,
    voltage_l3_any_t0_avg_v,
    voltage_l3_any_t0_max_v,
    voltage_l3_any_t0_max_timestamp,
    voltage_l3_any_t0_min_v,
    voltage_l3_any_t0_min_timestamp
  )
values
  (
:'p30_timestamp'::timestamptz,
:'p30_interval'::interval_entity,
:'p30_meter_id',
:'p30_measurement_location_id',
:'p30_count',
:'p30_quarter_hour_count',
:'p30_active_energy_l1_export_t0_max_wh',
:'p30_active_energy_l1_export_t0_min_wh',
:'p30_active_energy_l1_import_t0_max_wh',
:'p30_active_energy_l1_import_t0_min_wh',
:'p30_active_energy_l2_export_t0_max_wh',
:'p30_active_energy_l2_export_t0_min_wh',
:'p30_active_energy_l2_import_t0_max_wh',
:'p30_active_energy_l2_import_t0_min_wh',
:'p30_active_energy_l3_export_t0_max_wh',
:'p30_active_energy_l3_export_t0_min_wh',
:'p30_active_energy_l3_import_t0_max_wh',
:'p30_active_energy_l3_import_t0_min_wh',
:'p30_active_energy_total_export_t0_max_wh',
:'p30_active_energy_total_export_t0_min_wh',
:'p30_active_energy_total_import_t0_max_wh',
:'p30_active_energy_total_import_t0_min_wh',
:'p30_active_energy_total_import_t1_max_wh',
:'p30_active_energy_total_import_t1_min_wh',
:'p30_active_energy_total_import_t2_max_wh',
:'p30_active_energy_total_import_t2_min_wh',
:'p30_active_power_l1_net_t0_avg_w',
:'p30_active_power_l1_net_t0_max_w',
:'p30_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p30_active_power_l1_net_t0_min_w',
:'p30_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p30_active_power_l2_net_t0_avg_w',
:'p30_active_power_l2_net_t0_max_w',
:'p30_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p30_active_power_l2_net_t0_min_w',
:'p30_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p30_active_power_l3_net_t0_avg_w',
:'p30_active_power_l3_net_t0_max_w',
:'p30_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p30_active_power_l3_net_t0_min_w',
:'p30_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p30_current_l1_any_t0_avg_a',
:'p30_current_l1_any_t0_max_a',
:'p30_current_l1_any_t0_max_timestamp'::timestamptz,
:'p30_current_l1_any_t0_min_a',
:'p30_current_l1_any_t0_min_timestamp'::timestamptz,
:'p30_current_l2_any_t0_avg_a',
:'p30_current_l2_any_t0_max_a',
:'p30_current_l2_any_t0_max_timestamp'::timestamptz,
:'p30_current_l2_any_t0_min_a',
:'p30_current_l2_any_t0_min_timestamp'::timestamptz,
:'p30_current_l3_any_t0_avg_a',
:'p30_current_l3_any_t0_max_a',
:'p30_current_l3_any_t0_max_timestamp'::timestamptz,
:'p30_current_l3_any_t0_min_a',
:'p30_current_l3_any_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_l1_export_t0_avg_w',
:'p30_derived_active_power_l1_export_t0_max_w',
:'p30_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_l1_export_t0_min_w',
:'p30_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_l1_import_t0_avg_w',
:'p30_derived_active_power_l1_import_t0_max_w',
:'p30_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_l1_import_t0_min_w',
:'p30_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_l2_export_t0_avg_w',
:'p30_derived_active_power_l2_export_t0_max_w',
:'p30_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_l2_export_t0_min_w',
:'p30_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_l2_import_t0_avg_w',
:'p30_derived_active_power_l2_import_t0_max_w',
:'p30_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_l2_import_t0_min_w',
:'p30_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_l3_export_t0_avg_w',
:'p30_derived_active_power_l3_export_t0_max_w',
:'p30_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_l3_export_t0_min_w',
:'p30_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_l3_import_t0_avg_w',
:'p30_derived_active_power_l3_import_t0_max_w',
:'p30_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_l3_import_t0_min_w',
:'p30_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_total_export_t0_avg_w',
:'p30_derived_active_power_total_export_t0_max_w',
:'p30_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_total_export_t0_min_w',
:'p30_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_total_import_t0_avg_w',
:'p30_derived_active_power_total_import_t0_max_w',
:'p30_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p30_derived_active_power_total_import_t0_min_w',
:'p30_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p30_derived_active_power_total_import_t1_avg_w',
:'p30_derived_active_power_total_import_t1_max_w',
:'p30_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p30_derived_active_power_total_import_t1_min_w',
:'p30_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p30_derived_active_power_total_import_t2_avg_w',
:'p30_derived_active_power_total_import_t2_max_w',
:'p30_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p30_derived_active_power_total_import_t2_min_w',
:'p30_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_l1_export_t0_avg_var',
:'p30_derived_reactive_power_l1_export_t0_max_var',
:'p30_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_l1_export_t0_min_var',
:'p30_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_l1_import_t0_avg_var',
:'p30_derived_reactive_power_l1_import_t0_max_var',
:'p30_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_l1_import_t0_min_var',
:'p30_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_l2_export_t0_avg_var',
:'p30_derived_reactive_power_l2_export_t0_max_var',
:'p30_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_l2_export_t0_min_var',
:'p30_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_l2_import_t0_avg_var',
:'p30_derived_reactive_power_l2_import_t0_max_var',
:'p30_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_l2_import_t0_min_var',
:'p30_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_l3_export_t0_avg_var',
:'p30_derived_reactive_power_l3_export_t0_max_var',
:'p30_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_l3_export_t0_min_var',
:'p30_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_l3_import_t0_avg_var',
:'p30_derived_reactive_power_l3_import_t0_max_var',
:'p30_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_l3_import_t0_min_var',
:'p30_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_total_export_t0_avg_var',
:'p30_derived_reactive_power_total_export_t0_max_var',
:'p30_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_total_export_t0_min_var',
:'p30_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p30_derived_reactive_power_total_import_t0_avg_var',
:'p30_derived_reactive_power_total_import_t0_max_var',
:'p30_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p30_derived_reactive_power_total_import_t0_min_var',
:'p30_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p30_reactive_energy_l1_export_t0_max_varh',
:'p30_reactive_energy_l1_export_t0_min_varh',
:'p30_reactive_energy_l1_import_t0_max_varh',
:'p30_reactive_energy_l1_import_t0_min_varh',
:'p30_reactive_energy_l2_export_t0_max_varh',
:'p30_reactive_energy_l2_export_t0_min_varh',
:'p30_reactive_energy_l2_import_t0_max_varh',
:'p30_reactive_energy_l2_import_t0_min_varh',
:'p30_reactive_energy_l3_export_t0_max_varh',
:'p30_reactive_energy_l3_export_t0_min_varh',
:'p30_reactive_energy_l3_import_t0_max_varh',
:'p30_reactive_energy_l3_import_t0_min_varh',
:'p30_reactive_energy_total_export_t0_max_varh',
:'p30_reactive_energy_total_export_t0_min_varh',
:'p30_reactive_energy_total_import_t0_max_varh',
:'p30_reactive_energy_total_import_t0_min_varh',
:'p30_reactive_power_l1_net_t0_avg_var',
:'p30_reactive_power_l1_net_t0_max_var',
:'p30_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p30_reactive_power_l1_net_t0_min_var',
:'p30_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p30_reactive_power_l2_net_t0_avg_var',
:'p30_reactive_power_l2_net_t0_max_var',
:'p30_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p30_reactive_power_l2_net_t0_min_var',
:'p30_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p30_reactive_power_l3_net_t0_avg_var',
:'p30_reactive_power_l3_net_t0_max_var',
:'p30_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p30_reactive_power_l3_net_t0_min_var',
:'p30_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p30_voltage_l1_any_t0_avg_v',
:'p30_voltage_l1_any_t0_max_v',
:'p30_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p30_voltage_l1_any_t0_min_v',
:'p30_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p30_voltage_l2_any_t0_avg_v',
:'p30_voltage_l2_any_t0_max_v',
:'p30_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p30_voltage_l2_any_t0_min_v',
:'p30_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p30_voltage_l3_any_t0_avg_v',
:'p30_voltage_l3_any_t0_max_v',
:'p30_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p30_voltage_l3_any_t0_min_v',
:'p30_voltage_l3_any_t0_min_timestamp'::timestamptz
  )
on conflict (
  timestamp,
  interval,
  meter_id,
  measurement_location_id
) do
update
set
  count = abb_b2x_aggregates.count + EXCLUDED.count,
  active_energy_l1_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l1_export_t0_max_wh,
    EXCLUDED.active_energy_l1_export_t0_max_wh
  ),
  active_energy_l1_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l1_export_t0_min_wh,
    EXCLUDED.active_energy_l1_export_t0_min_wh
  ),
  active_energy_l1_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l1_import_t0_max_wh,
    EXCLUDED.active_energy_l1_import_t0_max_wh
  ),
  active_energy_l1_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l1_import_t0_min_wh,
    EXCLUDED.active_energy_l1_import_t0_min_wh
  ),
  active_energy_l2_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l2_export_t0_max_wh,
    EXCLUDED.active_energy_l2_export_t0_max_wh
  ),
  active_energy_l2_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l2_export_t0_min_wh,
    EXCLUDED.active_energy_l2_export_t0_min_wh
  ),
  active_energy_l2_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l2_import_t0_max_wh,
    EXCLUDED.active_energy_l2_import_t0_max_wh
  ),
  active_energy_l2_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l2_import_t0_min_wh,
    EXCLUDED.active_energy_l2_import_t0_min_wh
  ),
  active_energy_l3_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l3_export_t0_max_wh,
    EXCLUDED.active_energy_l3_export_t0_max_wh
  ),
  active_energy_l3_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l3_export_t0_min_wh,
    EXCLUDED.active_energy_l3_export_t0_min_wh
  ),
  active_energy_l3_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l3_import_t0_max_wh,
    EXCLUDED.active_energy_l3_import_t0_max_wh
  ),
  active_energy_l3_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l3_import_t0_min_wh,
    EXCLUDED.active_energy_l3_import_t0_min_wh
  ),
  active_energy_total_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_export_t0_max_wh,
    EXCLUDED.active_energy_total_export_t0_max_wh
  ),
  active_energy_total_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_total_export_t0_min_wh,
    EXCLUDED.active_energy_total_export_t0_min_wh
  ),
  active_energy_total_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_import_t0_max_wh,
    EXCLUDED.active_energy_total_import_t0_max_wh
  ),
  active_energy_total_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_total_import_t0_min_wh,
    EXCLUDED.active_energy_total_import_t0_min_wh
  ),
  active_energy_total_import_t1_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_import_t1_max_wh,
    EXCLUDED.active_energy_total_import_t1_max_wh
  ),
  active_energy_total_import_t1_min_wh = least(
    abb_b2x_aggregates.active_energy_total_import_t1_min_wh,
    EXCLUDED.active_energy_total_import_t1_min_wh
  ),
  active_energy_total_import_t2_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_import_t2_max_wh,
    EXCLUDED.active_energy_total_import_t2_max_wh
  ),
  active_energy_total_import_t2_min_wh = least(
    abb_b2x_aggregates.active_energy_total_import_t2_min_wh,
    EXCLUDED.active_energy_total_import_t2_min_wh
  ),
  active_power_l1_net_t0_avg_w = (
    abb_b2x_aggregates.active_power_l1_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l1_net_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  active_power_l1_net_t0_max_w = greatest(
    abb_b2x_aggregates.active_power_l1_net_t0_max_w,
    EXCLUDED.active_power_l1_net_t0_max_w
  ),
  active_power_l1_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_max_w > abb_b2x_aggregates.active_power_l1_net_t0_max_w then EXCLUDED.active_power_l1_net_t0_max_timestamp
    else abb_b2x_aggregates.active_power_l1_net_t0_max_timestamp
  end,
  active_power_l1_net_t0_min_w = least(
    abb_b2x_aggregates.active_power_l1_net_t0_min_w,
    EXCLUDED.active_power_l1_net_t0_min_w
  ),
  active_power_l1_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_min_w < abb_b2x_aggregates.active_power_l1_net_t0_min_w then EXCLUDED.active_power_l1_net_t0_min_timestamp
    else abb_b2x_aggregates.active_power_l1_net_t0_min_timestamp
  end,
  active_power_l2_net_t0_avg_w = (
    abb_b2x_aggregates.active_power_l2_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l2_net_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  active_power_l2_net_t0_max_w = greatest(
    abb_b2x_aggregates.active_power_l2_net_t0_max_w,
    EXCLUDED.active_power_l2_net_t0_max_w
  ),
  active_power_l2_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_max_w > abb_b2x_aggregates.active_power_l2_net_t0_max_w then EXCLUDED.active_power_l2_net_t0_max_timestamp
    else abb_b2x_aggregates.active_power_l2_net_t0_max_timestamp
  end,
  active_power_l2_net_t0_min_w = least(
    abb_b2x_aggregates.active_power_l2_net_t0_min_w,
    EXCLUDED.active_power_l2_net_t0_min_w
  ),
  active_power_l2_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_min_w < abb_b2x_aggregates.active_power_l2_net_t0_min_w then EXCLUDED.active_power_l2_net_t0_min_timestamp
    else abb_b2x_aggregates.active_power_l2_net_t0_min_timestamp
  end,
  active_power_l3_net_t0_avg_w = (
    abb_b2x_aggregates.active_power_l3_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l3_net_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  active_power_l3_net_t0_max_w = greatest(
    abb_b2x_aggregates.active_power_l3_net_t0_max_w,
    EXCLUDED.active_power_l3_net_t0_max_w
  ),
  active_power_l3_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_max_w > abb_b2x_aggregates.active_power_l3_net_t0_max_w then EXCLUDED.active_power_l3_net_t0_max_timestamp
    else abb_b2x_aggregates.active_power_l3_net_t0_max_timestamp
  end,
  active_power_l3_net_t0_min_w = least(
    abb_b2x_aggregates.active_power_l3_net_t0_min_w,
    EXCLUDED.active_power_l3_net_t0_min_w
  ),
  active_power_l3_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_min_w < abb_b2x_aggregates.active_power_l3_net_t0_min_w then EXCLUDED.active_power_l3_net_t0_min_timestamp
    else abb_b2x_aggregates.active_power_l3_net_t0_min_timestamp
  end,
  current_l1_any_t0_avg_a = (
    abb_b2x_aggregates.current_l1_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l1_any_t0_avg_a * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  current_l1_any_t0_max_a = greatest(
    abb_b2x_aggregates.current_l1_any_t0_max_a,
    EXCLUDED.current_l1_any_t0_max_a
  ),
  current_l1_any_t0_max_timestamp = case
    when EXCLUDED.current_l1_any_t0_max_a > abb_b2x_aggregates.current_l1_any_t0_max_a then EXCLUDED.current_l1_any_t0_max_timestamp
    else abb_b2x_aggregates.current_l1_any_t0_max_timestamp
  end,
  current_l1_any_t0_min_a = least(
    abb_b2x_aggregates.current_l1_any_t0_min_a,
    EXCLUDED.current_l1_any_t0_min_a
  ),
  current_l1_any_t0_min_timestamp = case
    when EXCLUDED.current_l1_any_t0_min_a < abb_b2x_aggregates.current_l1_any_t0_min_a then EXCLUDED.current_l1_any_t0_min_timestamp
    else abb_b2x_aggregates.current_l1_any_t0_min_timestamp
  end,
  current_l2_any_t0_avg_a = (
    abb_b2x_aggregates.current_l2_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l2_any_t0_avg_a * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  current_l2_any_t0_max_a = greatest(
    abb_b2x_aggregates.current_l2_any_t0_max_a,
    EXCLUDED.current_l2_any_t0_max_a
  ),
  current_l2_any_t0_max_timestamp = case
    when EXCLUDED.current_l2_any_t0_max_a > abb_b2x_aggregates.current_l2_any_t0_max_a then EXCLUDED.current_l2_any_t0_max_timestamp
    else abb_b2x_aggregates.current_l2_any_t0_max_timestamp
  end,
  current_l2_any_t0_min_a = least(
    abb_b2x_aggregates.current_l2_any_t0_min_a,
    EXCLUDED.current_l2_any_t0_min_a
  ),
  current_l2_any_t0_min_timestamp = case
    when EXCLUDED.current_l2_any_t0_min_a < abb_b2x_aggregates.current_l2_any_t0_min_a then EXCLUDED.current_l2_any_t0_min_timestamp
    else abb_b2x_aggregates.current_l2_any_t0_min_timestamp
  end,
  current_l3_any_t0_avg_a = (
    abb_b2x_aggregates.current_l3_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l3_any_t0_avg_a * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  current_l3_any_t0_max_a = greatest(
    abb_b2x_aggregates.current_l3_any_t0_max_a,
    EXCLUDED.current_l3_any_t0_max_a
  ),
  current_l3_any_t0_max_timestamp = case
    when EXCLUDED.current_l3_any_t0_max_a > abb_b2x_aggregates.current_l3_any_t0_max_a then EXCLUDED.current_l3_any_t0_max_timestamp
    else abb_b2x_aggregates.current_l3_any_t0_max_timestamp
  end,
  current_l3_any_t0_min_a = least(
    abb_b2x_aggregates.current_l3_any_t0_min_a,
    EXCLUDED.current_l3_any_t0_min_a
  ),
  current_l3_any_t0_min_timestamp = case
    when EXCLUDED.current_l3_any_t0_min_a < abb_b2x_aggregates.current_l3_any_t0_min_a then EXCLUDED.current_l3_any_t0_min_timestamp
    else abb_b2x_aggregates.current_l3_any_t0_min_timestamp
  end,
  derived_active_power_l1_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l1_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l1_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l1_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w,
    EXCLUDED.derived_active_power_l1_export_t0_max_w
  ),
  derived_active_power_l1_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l1_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w then EXCLUDED.derived_active_power_l1_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_export_t0_max_timestamp
  end,
  derived_active_power_l1_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w,
    EXCLUDED.derived_active_power_l1_export_t0_min_w
  ),
  derived_active_power_l1_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l1_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w then EXCLUDED.derived_active_power_l1_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_export_t0_min_timestamp
  end,
  derived_active_power_l1_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l1_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l1_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l1_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w,
    EXCLUDED.derived_active_power_l1_import_t0_max_w
  ),
  derived_active_power_l1_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w then EXCLUDED.derived_active_power_l1_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_import_t0_max_timestamp
  end,
  derived_active_power_l1_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w,
    EXCLUDED.derived_active_power_l1_import_t0_min_w
  ),
  derived_active_power_l1_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w then EXCLUDED.derived_active_power_l1_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_import_t0_min_timestamp
  end,
  derived_active_power_l2_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l2_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l2_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l2_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w,
    EXCLUDED.derived_active_power_l2_export_t0_max_w
  ),
  derived_active_power_l2_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l2_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w then EXCLUDED.derived_active_power_l2_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_export_t0_max_timestamp
  end,
  derived_active_power_l2_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w,
    EXCLUDED.derived_active_power_l2_export_t0_min_w
  ),
  derived_active_power_l2_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l2_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w then EXCLUDED.derived_active_power_l2_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_export_t0_min_timestamp
  end,
  derived_active_power_l2_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l2_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l2_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l2_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w,
    EXCLUDED.derived_active_power_l2_import_t0_max_w
  ),
  derived_active_power_l2_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w then EXCLUDED.derived_active_power_l2_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_import_t0_max_timestamp
  end,
  derived_active_power_l2_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w,
    EXCLUDED.derived_active_power_l2_import_t0_min_w
  ),
  derived_active_power_l2_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w then EXCLUDED.derived_active_power_l2_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_import_t0_min_timestamp
  end,
  derived_active_power_l3_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l3_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l3_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l3_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w,
    EXCLUDED.derived_active_power_l3_export_t0_max_w
  ),
  derived_active_power_l3_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l3_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w then EXCLUDED.derived_active_power_l3_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_export_t0_max_timestamp
  end,
  derived_active_power_l3_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w,
    EXCLUDED.derived_active_power_l3_export_t0_min_w
  ),
  derived_active_power_l3_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l3_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w then EXCLUDED.derived_active_power_l3_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_export_t0_min_timestamp
  end,
  derived_active_power_l3_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l3_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l3_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l3_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w,
    EXCLUDED.derived_active_power_l3_import_t0_max_w
  ),
  derived_active_power_l3_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w then EXCLUDED.derived_active_power_l3_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_import_t0_max_timestamp
  end,
  derived_active_power_l3_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w,
    EXCLUDED.derived_active_power_l3_import_t0_min_w
  ),
  derived_active_power_l3_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w then EXCLUDED.derived_active_power_l3_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_import_t0_min_timestamp
  end,
  derived_active_power_total_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_export_t0_max_w,
    EXCLUDED.derived_active_power_total_export_t0_max_w
  ),
  derived_active_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_max_w > abb_b2x_aggregates.derived_active_power_total_export_t0_max_w then EXCLUDED.derived_active_power_total_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_export_t0_max_timestamp
  end,
  derived_active_power_total_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_export_t0_min_w,
    EXCLUDED.derived_active_power_total_export_t0_min_w
  ),
  derived_active_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_min_w < abb_b2x_aggregates.derived_active_power_total_export_t0_min_w then EXCLUDED.derived_active_power_total_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_export_t0_min_timestamp
  end,
  derived_active_power_total_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_import_t0_max_w,
    EXCLUDED.derived_active_power_total_import_t0_max_w
  ),
  derived_active_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_max_w > abb_b2x_aggregates.derived_active_power_total_import_t0_max_w then EXCLUDED.derived_active_power_total_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t0_max_timestamp
  end,
  derived_active_power_total_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_import_t0_min_w,
    EXCLUDED.derived_active_power_total_import_t0_min_w
  ),
  derived_active_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_min_w < abb_b2x_aggregates.derived_active_power_total_import_t0_min_w then EXCLUDED.derived_active_power_total_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t0_min_timestamp
  end,
  derived_active_power_total_import_t1_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_import_t1_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_import_t1_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_import_t1_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_import_t1_max_w,
    EXCLUDED.derived_active_power_total_import_t1_max_w
  ),
  derived_active_power_total_import_t1_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_max_w > abb_b2x_aggregates.derived_active_power_total_import_t1_max_w then EXCLUDED.derived_active_power_total_import_t1_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t1_max_timestamp
  end,
  derived_active_power_total_import_t1_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_import_t1_min_w,
    EXCLUDED.derived_active_power_total_import_t1_min_w
  ),
  derived_active_power_total_import_t1_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_min_w < abb_b2x_aggregates.derived_active_power_total_import_t1_min_w then EXCLUDED.derived_active_power_total_import_t1_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t1_min_timestamp
  end,
  derived_active_power_total_import_t2_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_import_t2_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_import_t2_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_import_t2_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_import_t2_max_w,
    EXCLUDED.derived_active_power_total_import_t2_max_w
  ),
  derived_active_power_total_import_t2_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_max_w > abb_b2x_aggregates.derived_active_power_total_import_t2_max_w then EXCLUDED.derived_active_power_total_import_t2_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t2_max_timestamp
  end,
  derived_active_power_total_import_t2_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_import_t2_min_w,
    EXCLUDED.derived_active_power_total_import_t2_min_w
  ),
  derived_active_power_total_import_t2_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_min_w < abb_b2x_aggregates.derived_active_power_total_import_t2_min_w then EXCLUDED.derived_active_power_total_import_t2_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t2_min_timestamp
  end,
  derived_reactive_power_l1_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l1_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l1_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l1_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var,
    EXCLUDED.derived_reactive_power_l1_export_t0_max_var
  ),
  derived_reactive_power_l1_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var then EXCLUDED.derived_reactive_power_l1_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_timestamp
  end,
  derived_reactive_power_l1_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var,
    EXCLUDED.derived_reactive_power_l1_export_t0_min_var
  ),
  derived_reactive_power_l1_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var then EXCLUDED.derived_reactive_power_l1_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_timestamp
  end,
  derived_reactive_power_l1_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l1_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l1_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l1_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var,
    EXCLUDED.derived_reactive_power_l1_import_t0_max_var
  ),
  derived_reactive_power_l1_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var then EXCLUDED.derived_reactive_power_l1_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_timestamp
  end,
  derived_reactive_power_l1_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var,
    EXCLUDED.derived_reactive_power_l1_import_t0_min_var
  ),
  derived_reactive_power_l1_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var then EXCLUDED.derived_reactive_power_l1_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_timestamp
  end,
  derived_reactive_power_l2_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l2_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l2_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l2_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var,
    EXCLUDED.derived_reactive_power_l2_export_t0_max_var
  ),
  derived_reactive_power_l2_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var then EXCLUDED.derived_reactive_power_l2_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_timestamp
  end,
  derived_reactive_power_l2_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var,
    EXCLUDED.derived_reactive_power_l2_export_t0_min_var
  ),
  derived_reactive_power_l2_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var then EXCLUDED.derived_reactive_power_l2_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_timestamp
  end,
  derived_reactive_power_l2_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l2_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l2_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l2_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var,
    EXCLUDED.derived_reactive_power_l2_import_t0_max_var
  ),
  derived_reactive_power_l2_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var then EXCLUDED.derived_reactive_power_l2_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_timestamp
  end,
  derived_reactive_power_l2_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var,
    EXCLUDED.derived_reactive_power_l2_import_t0_min_var
  ),
  derived_reactive_power_l2_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var then EXCLUDED.derived_reactive_power_l2_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_timestamp
  end,
  derived_reactive_power_l3_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l3_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l3_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l3_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var,
    EXCLUDED.derived_reactive_power_l3_export_t0_max_var
  ),
  derived_reactive_power_l3_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var then EXCLUDED.derived_reactive_power_l3_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_timestamp
  end,
  derived_reactive_power_l3_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var,
    EXCLUDED.derived_reactive_power_l3_export_t0_min_var
  ),
  derived_reactive_power_l3_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var then EXCLUDED.derived_reactive_power_l3_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_timestamp
  end,
  derived_reactive_power_l3_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l3_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l3_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l3_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var,
    EXCLUDED.derived_reactive_power_l3_import_t0_max_var
  ),
  derived_reactive_power_l3_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var then EXCLUDED.derived_reactive_power_l3_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_timestamp
  end,
  derived_reactive_power_l3_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var,
    EXCLUDED.derived_reactive_power_l3_import_t0_min_var
  ),
  derived_reactive_power_l3_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var then EXCLUDED.derived_reactive_power_l3_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_timestamp
  end,
  derived_reactive_power_total_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_total_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_total_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_total_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var,
    EXCLUDED.derived_reactive_power_total_export_t0_max_var
  ),
  derived_reactive_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var then EXCLUDED.derived_reactive_power_total_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_timestamp
  end,
  derived_reactive_power_total_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var,
    EXCLUDED.derived_reactive_power_total_export_t0_min_var
  ),
  derived_reactive_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var then EXCLUDED.derived_reactive_power_total_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_timestamp
  end,
  derived_reactive_power_total_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_total_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_total_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_total_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var,
    EXCLUDED.derived_reactive_power_total_import_t0_max_var
  ),
  derived_reactive_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var then EXCLUDED.derived_reactive_power_total_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_timestamp
  end,
  derived_reactive_power_total_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var,
    EXCLUDED.derived_reactive_power_total_import_t0_min_var
  ),
  derived_reactive_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var then EXCLUDED.derived_reactive_power_total_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_timestamp
  end,
  reactive_energy_l1_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l1_export_t0_max_varh,
    EXCLUDED.reactive_energy_l1_export_t0_max_varh
  ),
  reactive_energy_l1_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l1_export_t0_min_varh,
    EXCLUDED.reactive_energy_l1_export_t0_min_varh
  ),
  reactive_energy_l1_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l1_import_t0_max_varh,
    EXCLUDED.reactive_energy_l1_import_t0_max_varh
  ),
  reactive_energy_l1_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l1_import_t0_min_varh,
    EXCLUDED.reactive_energy_l1_import_t0_min_varh
  ),
  reactive_energy_l2_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l2_export_t0_max_varh,
    EXCLUDED.reactive_energy_l2_export_t0_max_varh
  ),
  reactive_energy_l2_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l2_export_t0_min_varh,
    EXCLUDED.reactive_energy_l2_export_t0_min_varh
  ),
  reactive_energy_l2_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l2_import_t0_max_varh,
    EXCLUDED.reactive_energy_l2_import_t0_max_varh
  ),
  reactive_energy_l2_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l2_import_t0_min_varh,
    EXCLUDED.reactive_energy_l2_import_t0_min_varh
  ),
  reactive_energy_l3_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l3_export_t0_max_varh,
    EXCLUDED.reactive_energy_l3_export_t0_max_varh
  ),
  reactive_energy_l3_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l3_export_t0_min_varh,
    EXCLUDED.reactive_energy_l3_export_t0_min_varh
  ),
  reactive_energy_l3_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l3_import_t0_max_varh,
    EXCLUDED.reactive_energy_l3_import_t0_max_varh
  ),
  reactive_energy_l3_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l3_import_t0_min_varh,
    EXCLUDED.reactive_energy_l3_import_t0_min_varh
  ),
  reactive_energy_total_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_total_export_t0_max_varh,
    EXCLUDED.reactive_energy_total_export_t0_max_varh
  ),
  reactive_energy_total_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_total_export_t0_min_varh,
    EXCLUDED.reactive_energy_total_export_t0_min_varh
  ),
  reactive_energy_total_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_total_import_t0_max_varh,
    EXCLUDED.reactive_energy_total_import_t0_max_varh
  ),
  reactive_energy_total_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_total_import_t0_min_varh,
    EXCLUDED.reactive_energy_total_import_t0_min_varh
  ),
  reactive_power_l1_net_t0_avg_var = (
    abb_b2x_aggregates.reactive_power_l1_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l1_net_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  reactive_power_l1_net_t0_max_var = greatest(
    abb_b2x_aggregates.reactive_power_l1_net_t0_max_var,
    EXCLUDED.reactive_power_l1_net_t0_max_var
  ),
  reactive_power_l1_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_l1_net_t0_max_var > abb_b2x_aggregates.reactive_power_l1_net_t0_max_var then EXCLUDED.reactive_power_l1_net_t0_max_timestamp
    else abb_b2x_aggregates.reactive_power_l1_net_t0_max_timestamp
  end,
  reactive_power_l1_net_t0_min_var = least(
    abb_b2x_aggregates.reactive_power_l1_net_t0_min_var,
    EXCLUDED.reactive_power_l1_net_t0_min_var
  ),
  reactive_power_l1_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_l1_net_t0_min_var < abb_b2x_aggregates.reactive_power_l1_net_t0_min_var then EXCLUDED.reactive_power_l1_net_t0_min_timestamp
    else abb_b2x_aggregates.reactive_power_l1_net_t0_min_timestamp
  end,
  reactive_power_l2_net_t0_avg_var = (
    abb_b2x_aggregates.reactive_power_l2_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l2_net_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  reactive_power_l2_net_t0_max_var = greatest(
    abb_b2x_aggregates.reactive_power_l2_net_t0_max_var,
    EXCLUDED.reactive_power_l2_net_t0_max_var
  ),
  reactive_power_l2_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_l2_net_t0_max_var > abb_b2x_aggregates.reactive_power_l2_net_t0_max_var then EXCLUDED.reactive_power_l2_net_t0_max_timestamp
    else abb_b2x_aggregates.reactive_power_l2_net_t0_max_timestamp
  end,
  reactive_power_l2_net_t0_min_var = least(
    abb_b2x_aggregates.reactive_power_l2_net_t0_min_var,
    EXCLUDED.reactive_power_l2_net_t0_min_var
  ),
  reactive_power_l2_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_l2_net_t0_min_var < abb_b2x_aggregates.reactive_power_l2_net_t0_min_var then EXCLUDED.reactive_power_l2_net_t0_min_timestamp
    else abb_b2x_aggregates.reactive_power_l2_net_t0_min_timestamp
  end,
  reactive_power_l3_net_t0_avg_var = (
    abb_b2x_aggregates.reactive_power_l3_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l3_net_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  reactive_power_l3_net_t0_max_var = greatest(
    abb_b2x_aggregates.reactive_power_l3_net_t0_max_var,
    EXCLUDED.reactive_power_l3_net_t0_max_var
  ),
  reactive_power_l3_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_l3_net_t0_max_var > abb_b2x_aggregates.reactive_power_l3_net_t0_max_var then EXCLUDED.reactive_power_l3_net_t0_max_timestamp
    else abb_b2x_aggregates.reactive_power_l3_net_t0_max_timestamp
  end,
  reactive_power_l3_net_t0_min_var = least(
    abb_b2x_aggregates.reactive_power_l3_net_t0_min_var,
    EXCLUDED.reactive_power_l3_net_t0_min_var
  ),
  reactive_power_l3_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_l3_net_t0_min_var < abb_b2x_aggregates.reactive_power_l3_net_t0_min_var then EXCLUDED.reactive_power_l3_net_t0_min_timestamp
    else abb_b2x_aggregates.reactive_power_l3_net_t0_min_timestamp
  end,
  voltage_l1_any_t0_avg_v = (
    abb_b2x_aggregates.voltage_l1_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l1_any_t0_avg_v * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  voltage_l1_any_t0_max_v = greatest(
    abb_b2x_aggregates.voltage_l1_any_t0_max_v,
    EXCLUDED.voltage_l1_any_t0_max_v
  ),
  voltage_l1_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_max_v > abb_b2x_aggregates.voltage_l1_any_t0_max_v then EXCLUDED.voltage_l1_any_t0_max_timestamp
    else abb_b2x_aggregates.voltage_l1_any_t0_max_timestamp
  end,
  voltage_l1_any_t0_min_v = least(
    abb_b2x_aggregates.voltage_l1_any_t0_min_v,
    EXCLUDED.voltage_l1_any_t0_min_v
  ),
  voltage_l1_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_min_v < abb_b2x_aggregates.voltage_l1_any_t0_min_v then EXCLUDED.voltage_l1_any_t0_min_timestamp
    else abb_b2x_aggregates.voltage_l1_any_t0_min_timestamp
  end,
  voltage_l2_any_t0_avg_v = (
    abb_b2x_aggregates.voltage_l2_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l2_any_t0_avg_v * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  voltage_l2_any_t0_max_v = greatest(
    abb_b2x_aggregates.voltage_l2_any_t0_max_v,
    EXCLUDED.voltage_l2_any_t0_max_v
  ),
  voltage_l2_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_max_v > abb_b2x_aggregates.voltage_l2_any_t0_max_v then EXCLUDED.voltage_l2_any_t0_max_timestamp
    else abb_b2x_aggregates.voltage_l2_any_t0_max_timestamp
  end,
  voltage_l2_any_t0_min_v = least(
    abb_b2x_aggregates.voltage_l2_any_t0_min_v,
    EXCLUDED.voltage_l2_any_t0_min_v
  ),
  voltage_l2_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_min_v < abb_b2x_aggregates.voltage_l2_any_t0_min_v then EXCLUDED.voltage_l2_any_t0_min_timestamp
    else abb_b2x_aggregates.voltage_l2_any_t0_min_timestamp
  end,
  voltage_l3_any_t0_avg_v = (
    abb_b2x_aggregates.voltage_l3_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l3_any_t0_avg_v * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  voltage_l3_any_t0_max_v = greatest(
    abb_b2x_aggregates.voltage_l3_any_t0_max_v,
    EXCLUDED.voltage_l3_any_t0_max_v
  ),
  voltage_l3_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_max_v > abb_b2x_aggregates.voltage_l3_any_t0_max_v then EXCLUDED.voltage_l3_any_t0_max_timestamp
    else abb_b2x_aggregates.voltage_l3_any_t0_max_timestamp
  end,
  voltage_l3_any_t0_min_v = least(
    abb_b2x_aggregates.voltage_l3_any_t0_min_v,
    EXCLUDED.voltage_l3_any_t0_min_v
  ),
  voltage_l3_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_min_v < abb_b2x_aggregates.voltage_l3_any_t0_min_v then EXCLUDED.voltage_l3_any_t0_min_timestamp
    else abb_b2x_aggregates.voltage_l3_any_t0_min_timestamp
  end;

insert into
  abb_b2x_aggregates (
    timestamp,
    interval,
    meter_id,
    measurement_location_id,
    count,
    quarter_hour_count,
    active_energy_l1_export_t0_max_wh,
    active_energy_l1_export_t0_min_wh,
    active_energy_l1_import_t0_max_wh,
    active_energy_l1_import_t0_min_wh,
    active_energy_l2_export_t0_max_wh,
    active_energy_l2_export_t0_min_wh,
    active_energy_l2_import_t0_max_wh,
    active_energy_l2_import_t0_min_wh,
    active_energy_l3_export_t0_max_wh,
    active_energy_l3_export_t0_min_wh,
    active_energy_l3_import_t0_max_wh,
    active_energy_l3_import_t0_min_wh,
    active_energy_total_export_t0_max_wh,
    active_energy_total_export_t0_min_wh,
    active_energy_total_import_t0_max_wh,
    active_energy_total_import_t0_min_wh,
    active_energy_total_import_t1_max_wh,
    active_energy_total_import_t1_min_wh,
    active_energy_total_import_t2_max_wh,
    active_energy_total_import_t2_min_wh,
    active_power_l1_net_t0_avg_w,
    active_power_l1_net_t0_max_w,
    active_power_l1_net_t0_max_timestamp,
    active_power_l1_net_t0_min_w,
    active_power_l1_net_t0_min_timestamp,
    active_power_l2_net_t0_avg_w,
    active_power_l2_net_t0_max_w,
    active_power_l2_net_t0_max_timestamp,
    active_power_l2_net_t0_min_w,
    active_power_l2_net_t0_min_timestamp,
    active_power_l3_net_t0_avg_w,
    active_power_l3_net_t0_max_w,
    active_power_l3_net_t0_max_timestamp,
    active_power_l3_net_t0_min_w,
    active_power_l3_net_t0_min_timestamp,
    current_l1_any_t0_avg_a,
    current_l1_any_t0_max_a,
    current_l1_any_t0_max_timestamp,
    current_l1_any_t0_min_a,
    current_l1_any_t0_min_timestamp,
    current_l2_any_t0_avg_a,
    current_l2_any_t0_max_a,
    current_l2_any_t0_max_timestamp,
    current_l2_any_t0_min_a,
    current_l2_any_t0_min_timestamp,
    current_l3_any_t0_avg_a,
    current_l3_any_t0_max_a,
    current_l3_any_t0_max_timestamp,
    current_l3_any_t0_min_a,
    current_l3_any_t0_min_timestamp,
    derived_active_power_l1_export_t0_avg_w,
    derived_active_power_l1_export_t0_max_w,
    derived_active_power_l1_export_t0_max_timestamp,
    derived_active_power_l1_export_t0_min_w,
    derived_active_power_l1_export_t0_min_timestamp,
    derived_active_power_l1_import_t0_avg_w,
    derived_active_power_l1_import_t0_max_w,
    derived_active_power_l1_import_t0_max_timestamp,
    derived_active_power_l1_import_t0_min_w,
    derived_active_power_l1_import_t0_min_timestamp,
    derived_active_power_l2_export_t0_avg_w,
    derived_active_power_l2_export_t0_max_w,
    derived_active_power_l2_export_t0_max_timestamp,
    derived_active_power_l2_export_t0_min_w,
    derived_active_power_l2_export_t0_min_timestamp,
    derived_active_power_l2_import_t0_avg_w,
    derived_active_power_l2_import_t0_max_w,
    derived_active_power_l2_import_t0_max_timestamp,
    derived_active_power_l2_import_t0_min_w,
    derived_active_power_l2_import_t0_min_timestamp,
    derived_active_power_l3_export_t0_avg_w,
    derived_active_power_l3_export_t0_max_w,
    derived_active_power_l3_export_t0_max_timestamp,
    derived_active_power_l3_export_t0_min_w,
    derived_active_power_l3_export_t0_min_timestamp,
    derived_active_power_l3_import_t0_avg_w,
    derived_active_power_l3_import_t0_max_w,
    derived_active_power_l3_import_t0_max_timestamp,
    derived_active_power_l3_import_t0_min_w,
    derived_active_power_l3_import_t0_min_timestamp,
    derived_active_power_total_export_t0_avg_w,
    derived_active_power_total_export_t0_max_w,
    derived_active_power_total_export_t0_max_timestamp,
    derived_active_power_total_export_t0_min_w,
    derived_active_power_total_export_t0_min_timestamp,
    derived_active_power_total_import_t0_avg_w,
    derived_active_power_total_import_t0_max_w,
    derived_active_power_total_import_t0_max_timestamp,
    derived_active_power_total_import_t0_min_w,
    derived_active_power_total_import_t0_min_timestamp,
    derived_active_power_total_import_t1_avg_w,
    derived_active_power_total_import_t1_max_w,
    derived_active_power_total_import_t1_max_timestamp,
    derived_active_power_total_import_t1_min_w,
    derived_active_power_total_import_t1_min_timestamp,
    derived_active_power_total_import_t2_avg_w,
    derived_active_power_total_import_t2_max_w,
    derived_active_power_total_import_t2_max_timestamp,
    derived_active_power_total_import_t2_min_w,
    derived_active_power_total_import_t2_min_timestamp,
    derived_reactive_power_l1_export_t0_avg_var,
    derived_reactive_power_l1_export_t0_max_var,
    derived_reactive_power_l1_export_t0_max_timestamp,
    derived_reactive_power_l1_export_t0_min_var,
    derived_reactive_power_l1_export_t0_min_timestamp,
    derived_reactive_power_l1_import_t0_avg_var,
    derived_reactive_power_l1_import_t0_max_var,
    derived_reactive_power_l1_import_t0_max_timestamp,
    derived_reactive_power_l1_import_t0_min_var,
    derived_reactive_power_l1_import_t0_min_timestamp,
    derived_reactive_power_l2_export_t0_avg_var,
    derived_reactive_power_l2_export_t0_max_var,
    derived_reactive_power_l2_export_t0_max_timestamp,
    derived_reactive_power_l2_export_t0_min_var,
    derived_reactive_power_l2_export_t0_min_timestamp,
    derived_reactive_power_l2_import_t0_avg_var,
    derived_reactive_power_l2_import_t0_max_var,
    derived_reactive_power_l2_import_t0_max_timestamp,
    derived_reactive_power_l2_import_t0_min_var,
    derived_reactive_power_l2_import_t0_min_timestamp,
    derived_reactive_power_l3_export_t0_avg_var,
    derived_reactive_power_l3_export_t0_max_var,
    derived_reactive_power_l3_export_t0_max_timestamp,
    derived_reactive_power_l3_export_t0_min_var,
    derived_reactive_power_l3_export_t0_min_timestamp,
    derived_reactive_power_l3_import_t0_avg_var,
    derived_reactive_power_l3_import_t0_max_var,
    derived_reactive_power_l3_import_t0_max_timestamp,
    derived_reactive_power_l3_import_t0_min_var,
    derived_reactive_power_l3_import_t0_min_timestamp,
    derived_reactive_power_total_export_t0_avg_var,
    derived_reactive_power_total_export_t0_max_var,
    derived_reactive_power_total_export_t0_max_timestamp,
    derived_reactive_power_total_export_t0_min_var,
    derived_reactive_power_total_export_t0_min_timestamp,
    derived_reactive_power_total_import_t0_avg_var,
    derived_reactive_power_total_import_t0_max_var,
    derived_reactive_power_total_import_t0_max_timestamp,
    derived_reactive_power_total_import_t0_min_var,
    derived_reactive_power_total_import_t0_min_timestamp,
    reactive_energy_l1_export_t0_max_varh,
    reactive_energy_l1_export_t0_min_varh,
    reactive_energy_l1_import_t0_max_varh,
    reactive_energy_l1_import_t0_min_varh,
    reactive_energy_l2_export_t0_max_varh,
    reactive_energy_l2_export_t0_min_varh,
    reactive_energy_l2_import_t0_max_varh,
    reactive_energy_l2_import_t0_min_varh,
    reactive_energy_l3_export_t0_max_varh,
    reactive_energy_l3_export_t0_min_varh,
    reactive_energy_l3_import_t0_max_varh,
    reactive_energy_l3_import_t0_min_varh,
    reactive_energy_total_export_t0_max_varh,
    reactive_energy_total_export_t0_min_varh,
    reactive_energy_total_import_t0_max_varh,
    reactive_energy_total_import_t0_min_varh,
    reactive_power_l1_net_t0_avg_var,
    reactive_power_l1_net_t0_max_var,
    reactive_power_l1_net_t0_max_timestamp,
    reactive_power_l1_net_t0_min_var,
    reactive_power_l1_net_t0_min_timestamp,
    reactive_power_l2_net_t0_avg_var,
    reactive_power_l2_net_t0_max_var,
    reactive_power_l2_net_t0_max_timestamp,
    reactive_power_l2_net_t0_min_var,
    reactive_power_l2_net_t0_min_timestamp,
    reactive_power_l3_net_t0_avg_var,
    reactive_power_l3_net_t0_max_var,
    reactive_power_l3_net_t0_max_timestamp,
    reactive_power_l3_net_t0_min_var,
    reactive_power_l3_net_t0_min_timestamp,
    voltage_l1_any_t0_avg_v,
    voltage_l1_any_t0_max_v,
    voltage_l1_any_t0_max_timestamp,
    voltage_l1_any_t0_min_v,
    voltage_l1_any_t0_min_timestamp,
    voltage_l2_any_t0_avg_v,
    voltage_l2_any_t0_max_v,
    voltage_l2_any_t0_max_timestamp,
    voltage_l2_any_t0_min_v,
    voltage_l2_any_t0_min_timestamp,
    voltage_l3_any_t0_avg_v,
    voltage_l3_any_t0_max_v,
    voltage_l3_any_t0_max_timestamp,
    voltage_l3_any_t0_min_v,
    voltage_l3_any_t0_min_timestamp
  )
values
  (
:'p29_timestamp'::timestamptz,
:'p29_interval'::interval_entity,
:'p29_meter_id',
:'p29_measurement_location_id',
:'p29_count',
:'p29_quarter_hour_count',
:'p29_active_energy_l1_export_t0_max_wh',
:'p29_active_energy_l1_export_t0_min_wh',
:'p29_active_energy_l1_import_t0_max_wh',
:'p29_active_energy_l1_import_t0_min_wh',
:'p29_active_energy_l2_export_t0_max_wh',
:'p29_active_energy_l2_export_t0_min_wh',
:'p29_active_energy_l2_import_t0_max_wh',
:'p29_active_energy_l2_import_t0_min_wh',
:'p29_active_energy_l3_export_t0_max_wh',
:'p29_active_energy_l3_export_t0_min_wh',
:'p29_active_energy_l3_import_t0_max_wh',
:'p29_active_energy_l3_import_t0_min_wh',
:'p29_active_energy_total_export_t0_max_wh',
:'p29_active_energy_total_export_t0_min_wh',
:'p29_active_energy_total_import_t0_max_wh',
:'p29_active_energy_total_import_t0_min_wh',
:'p29_active_energy_total_import_t1_max_wh',
:'p29_active_energy_total_import_t1_min_wh',
:'p29_active_energy_total_import_t2_max_wh',
:'p29_active_energy_total_import_t2_min_wh',
:'p29_active_power_l1_net_t0_avg_w',
:'p29_active_power_l1_net_t0_max_w',
:'p29_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p29_active_power_l1_net_t0_min_w',
:'p29_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p29_active_power_l2_net_t0_avg_w',
:'p29_active_power_l2_net_t0_max_w',
:'p29_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p29_active_power_l2_net_t0_min_w',
:'p29_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p29_active_power_l3_net_t0_avg_w',
:'p29_active_power_l3_net_t0_max_w',
:'p29_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p29_active_power_l3_net_t0_min_w',
:'p29_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p29_current_l1_any_t0_avg_a',
:'p29_current_l1_any_t0_max_a',
:'p29_current_l1_any_t0_max_timestamp'::timestamptz,
:'p29_current_l1_any_t0_min_a',
:'p29_current_l1_any_t0_min_timestamp'::timestamptz,
:'p29_current_l2_any_t0_avg_a',
:'p29_current_l2_any_t0_max_a',
:'p29_current_l2_any_t0_max_timestamp'::timestamptz,
:'p29_current_l2_any_t0_min_a',
:'p29_current_l2_any_t0_min_timestamp'::timestamptz,
:'p29_current_l3_any_t0_avg_a',
:'p29_current_l3_any_t0_max_a',
:'p29_current_l3_any_t0_max_timestamp'::timestamptz,
:'p29_current_l3_any_t0_min_a',
:'p29_current_l3_any_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_l1_export_t0_avg_w',
:'p29_derived_active_power_l1_export_t0_max_w',
:'p29_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_l1_export_t0_min_w',
:'p29_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_l1_import_t0_avg_w',
:'p29_derived_active_power_l1_import_t0_max_w',
:'p29_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_l1_import_t0_min_w',
:'p29_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_l2_export_t0_avg_w',
:'p29_derived_active_power_l2_export_t0_max_w',
:'p29_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_l2_export_t0_min_w',
:'p29_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_l2_import_t0_avg_w',
:'p29_derived_active_power_l2_import_t0_max_w',
:'p29_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_l2_import_t0_min_w',
:'p29_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_l3_export_t0_avg_w',
:'p29_derived_active_power_l3_export_t0_max_w',
:'p29_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_l3_export_t0_min_w',
:'p29_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_l3_import_t0_avg_w',
:'p29_derived_active_power_l3_import_t0_max_w',
:'p29_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_l3_import_t0_min_w',
:'p29_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_total_export_t0_avg_w',
:'p29_derived_active_power_total_export_t0_max_w',
:'p29_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_total_export_t0_min_w',
:'p29_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_total_import_t0_avg_w',
:'p29_derived_active_power_total_import_t0_max_w',
:'p29_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p29_derived_active_power_total_import_t0_min_w',
:'p29_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p29_derived_active_power_total_import_t1_avg_w',
:'p29_derived_active_power_total_import_t1_max_w',
:'p29_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p29_derived_active_power_total_import_t1_min_w',
:'p29_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p29_derived_active_power_total_import_t2_avg_w',
:'p29_derived_active_power_total_import_t2_max_w',
:'p29_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p29_derived_active_power_total_import_t2_min_w',
:'p29_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_l1_export_t0_avg_var',
:'p29_derived_reactive_power_l1_export_t0_max_var',
:'p29_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_l1_export_t0_min_var',
:'p29_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_l1_import_t0_avg_var',
:'p29_derived_reactive_power_l1_import_t0_max_var',
:'p29_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_l1_import_t0_min_var',
:'p29_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_l2_export_t0_avg_var',
:'p29_derived_reactive_power_l2_export_t0_max_var',
:'p29_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_l2_export_t0_min_var',
:'p29_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_l2_import_t0_avg_var',
:'p29_derived_reactive_power_l2_import_t0_max_var',
:'p29_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_l2_import_t0_min_var',
:'p29_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_l3_export_t0_avg_var',
:'p29_derived_reactive_power_l3_export_t0_max_var',
:'p29_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_l3_export_t0_min_var',
:'p29_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_l3_import_t0_avg_var',
:'p29_derived_reactive_power_l3_import_t0_max_var',
:'p29_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_l3_import_t0_min_var',
:'p29_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_total_export_t0_avg_var',
:'p29_derived_reactive_power_total_export_t0_max_var',
:'p29_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_total_export_t0_min_var',
:'p29_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p29_derived_reactive_power_total_import_t0_avg_var',
:'p29_derived_reactive_power_total_import_t0_max_var',
:'p29_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p29_derived_reactive_power_total_import_t0_min_var',
:'p29_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p29_reactive_energy_l1_export_t0_max_varh',
:'p29_reactive_energy_l1_export_t0_min_varh',
:'p29_reactive_energy_l1_import_t0_max_varh',
:'p29_reactive_energy_l1_import_t0_min_varh',
:'p29_reactive_energy_l2_export_t0_max_varh',
:'p29_reactive_energy_l2_export_t0_min_varh',
:'p29_reactive_energy_l2_import_t0_max_varh',
:'p29_reactive_energy_l2_import_t0_min_varh',
:'p29_reactive_energy_l3_export_t0_max_varh',
:'p29_reactive_energy_l3_export_t0_min_varh',
:'p29_reactive_energy_l3_import_t0_max_varh',
:'p29_reactive_energy_l3_import_t0_min_varh',
:'p29_reactive_energy_total_export_t0_max_varh',
:'p29_reactive_energy_total_export_t0_min_varh',
:'p29_reactive_energy_total_import_t0_max_varh',
:'p29_reactive_energy_total_import_t0_min_varh',
:'p29_reactive_power_l1_net_t0_avg_var',
:'p29_reactive_power_l1_net_t0_max_var',
:'p29_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p29_reactive_power_l1_net_t0_min_var',
:'p29_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p29_reactive_power_l2_net_t0_avg_var',
:'p29_reactive_power_l2_net_t0_max_var',
:'p29_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p29_reactive_power_l2_net_t0_min_var',
:'p29_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p29_reactive_power_l3_net_t0_avg_var',
:'p29_reactive_power_l3_net_t0_max_var',
:'p29_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p29_reactive_power_l3_net_t0_min_var',
:'p29_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p29_voltage_l1_any_t0_avg_v',
:'p29_voltage_l1_any_t0_max_v',
:'p29_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p29_voltage_l1_any_t0_min_v',
:'p29_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p29_voltage_l2_any_t0_avg_v',
:'p29_voltage_l2_any_t0_max_v',
:'p29_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p29_voltage_l2_any_t0_min_v',
:'p29_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p29_voltage_l3_any_t0_avg_v',
:'p29_voltage_l3_any_t0_max_v',
:'p29_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p29_voltage_l3_any_t0_min_v',
:'p29_voltage_l3_any_t0_min_timestamp'::timestamptz
  )
on conflict (
  timestamp,
  interval,
  meter_id,
  measurement_location_id
) do
update
set
  count = abb_b2x_aggregates.count + EXCLUDED.count,
  active_energy_l1_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l1_export_t0_max_wh,
    EXCLUDED.active_energy_l1_export_t0_max_wh
  ),
  active_energy_l1_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l1_export_t0_min_wh,
    EXCLUDED.active_energy_l1_export_t0_min_wh
  ),
  active_energy_l1_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l1_import_t0_max_wh,
    EXCLUDED.active_energy_l1_import_t0_max_wh
  ),
  active_energy_l1_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l1_import_t0_min_wh,
    EXCLUDED.active_energy_l1_import_t0_min_wh
  ),
  active_energy_l2_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l2_export_t0_max_wh,
    EXCLUDED.active_energy_l2_export_t0_max_wh
  ),
  active_energy_l2_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l2_export_t0_min_wh,
    EXCLUDED.active_energy_l2_export_t0_min_wh
  ),
  active_energy_l2_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l2_import_t0_max_wh,
    EXCLUDED.active_energy_l2_import_t0_max_wh
  ),
  active_energy_l2_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l2_import_t0_min_wh,
    EXCLUDED.active_energy_l2_import_t0_min_wh
  ),
  active_energy_l3_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l3_export_t0_max_wh,
    EXCLUDED.active_energy_l3_export_t0_max_wh
  ),
  active_energy_l3_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l3_export_t0_min_wh,
    EXCLUDED.active_energy_l3_export_t0_min_wh
  ),
  active_energy_l3_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_l3_import_t0_max_wh,
    EXCLUDED.active_energy_l3_import_t0_max_wh
  ),
  active_energy_l3_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_l3_import_t0_min_wh,
    EXCLUDED.active_energy_l3_import_t0_min_wh
  ),
  active_energy_total_export_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_export_t0_max_wh,
    EXCLUDED.active_energy_total_export_t0_max_wh
  ),
  active_energy_total_export_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_total_export_t0_min_wh,
    EXCLUDED.active_energy_total_export_t0_min_wh
  ),
  active_energy_total_import_t0_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_import_t0_max_wh,
    EXCLUDED.active_energy_total_import_t0_max_wh
  ),
  active_energy_total_import_t0_min_wh = least(
    abb_b2x_aggregates.active_energy_total_import_t0_min_wh,
    EXCLUDED.active_energy_total_import_t0_min_wh
  ),
  active_energy_total_import_t1_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_import_t1_max_wh,
    EXCLUDED.active_energy_total_import_t1_max_wh
  ),
  active_energy_total_import_t1_min_wh = least(
    abb_b2x_aggregates.active_energy_total_import_t1_min_wh,
    EXCLUDED.active_energy_total_import_t1_min_wh
  ),
  active_energy_total_import_t2_max_wh = greatest(
    abb_b2x_aggregates.active_energy_total_import_t2_max_wh,
    EXCLUDED.active_energy_total_import_t2_max_wh
  ),
  active_energy_total_import_t2_min_wh = least(
    abb_b2x_aggregates.active_energy_total_import_t2_min_wh,
    EXCLUDED.active_energy_total_import_t2_min_wh
  ),
  active_power_l1_net_t0_avg_w = (
    abb_b2x_aggregates.active_power_l1_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l1_net_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  active_power_l1_net_t0_max_w = greatest(
    abb_b2x_aggregates.active_power_l1_net_t0_max_w,
    EXCLUDED.active_power_l1_net_t0_max_w
  ),
  active_power_l1_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_max_w > abb_b2x_aggregates.active_power_l1_net_t0_max_w then EXCLUDED.active_power_l1_net_t0_max_timestamp
    else abb_b2x_aggregates.active_power_l1_net_t0_max_timestamp
  end,
  active_power_l1_net_t0_min_w = least(
    abb_b2x_aggregates.active_power_l1_net_t0_min_w,
    EXCLUDED.active_power_l1_net_t0_min_w
  ),
  active_power_l1_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_min_w < abb_b2x_aggregates.active_power_l1_net_t0_min_w then EXCLUDED.active_power_l1_net_t0_min_timestamp
    else abb_b2x_aggregates.active_power_l1_net_t0_min_timestamp
  end,
  active_power_l2_net_t0_avg_w = (
    abb_b2x_aggregates.active_power_l2_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l2_net_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  active_power_l2_net_t0_max_w = greatest(
    abb_b2x_aggregates.active_power_l2_net_t0_max_w,
    EXCLUDED.active_power_l2_net_t0_max_w
  ),
  active_power_l2_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_max_w > abb_b2x_aggregates.active_power_l2_net_t0_max_w then EXCLUDED.active_power_l2_net_t0_max_timestamp
    else abb_b2x_aggregates.active_power_l2_net_t0_max_timestamp
  end,
  active_power_l2_net_t0_min_w = least(
    abb_b2x_aggregates.active_power_l2_net_t0_min_w,
    EXCLUDED.active_power_l2_net_t0_min_w
  ),
  active_power_l2_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_min_w < abb_b2x_aggregates.active_power_l2_net_t0_min_w then EXCLUDED.active_power_l2_net_t0_min_timestamp
    else abb_b2x_aggregates.active_power_l2_net_t0_min_timestamp
  end,
  active_power_l3_net_t0_avg_w = (
    abb_b2x_aggregates.active_power_l3_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l3_net_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  active_power_l3_net_t0_max_w = greatest(
    abb_b2x_aggregates.active_power_l3_net_t0_max_w,
    EXCLUDED.active_power_l3_net_t0_max_w
  ),
  active_power_l3_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_max_w > abb_b2x_aggregates.active_power_l3_net_t0_max_w then EXCLUDED.active_power_l3_net_t0_max_timestamp
    else abb_b2x_aggregates.active_power_l3_net_t0_max_timestamp
  end,
  active_power_l3_net_t0_min_w = least(
    abb_b2x_aggregates.active_power_l3_net_t0_min_w,
    EXCLUDED.active_power_l3_net_t0_min_w
  ),
  active_power_l3_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_min_w < abb_b2x_aggregates.active_power_l3_net_t0_min_w then EXCLUDED.active_power_l3_net_t0_min_timestamp
    else abb_b2x_aggregates.active_power_l3_net_t0_min_timestamp
  end,
  current_l1_any_t0_avg_a = (
    abb_b2x_aggregates.current_l1_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l1_any_t0_avg_a * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  current_l1_any_t0_max_a = greatest(
    abb_b2x_aggregates.current_l1_any_t0_max_a,
    EXCLUDED.current_l1_any_t0_max_a
  ),
  current_l1_any_t0_max_timestamp = case
    when EXCLUDED.current_l1_any_t0_max_a > abb_b2x_aggregates.current_l1_any_t0_max_a then EXCLUDED.current_l1_any_t0_max_timestamp
    else abb_b2x_aggregates.current_l1_any_t0_max_timestamp
  end,
  current_l1_any_t0_min_a = least(
    abb_b2x_aggregates.current_l1_any_t0_min_a,
    EXCLUDED.current_l1_any_t0_min_a
  ),
  current_l1_any_t0_min_timestamp = case
    when EXCLUDED.current_l1_any_t0_min_a < abb_b2x_aggregates.current_l1_any_t0_min_a then EXCLUDED.current_l1_any_t0_min_timestamp
    else abb_b2x_aggregates.current_l1_any_t0_min_timestamp
  end,
  current_l2_any_t0_avg_a = (
    abb_b2x_aggregates.current_l2_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l2_any_t0_avg_a * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  current_l2_any_t0_max_a = greatest(
    abb_b2x_aggregates.current_l2_any_t0_max_a,
    EXCLUDED.current_l2_any_t0_max_a
  ),
  current_l2_any_t0_max_timestamp = case
    when EXCLUDED.current_l2_any_t0_max_a > abb_b2x_aggregates.current_l2_any_t0_max_a then EXCLUDED.current_l2_any_t0_max_timestamp
    else abb_b2x_aggregates.current_l2_any_t0_max_timestamp
  end,
  current_l2_any_t0_min_a = least(
    abb_b2x_aggregates.current_l2_any_t0_min_a,
    EXCLUDED.current_l2_any_t0_min_a
  ),
  current_l2_any_t0_min_timestamp = case
    when EXCLUDED.current_l2_any_t0_min_a < abb_b2x_aggregates.current_l2_any_t0_min_a then EXCLUDED.current_l2_any_t0_min_timestamp
    else abb_b2x_aggregates.current_l2_any_t0_min_timestamp
  end,
  current_l3_any_t0_avg_a = (
    abb_b2x_aggregates.current_l3_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l3_any_t0_avg_a * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  current_l3_any_t0_max_a = greatest(
    abb_b2x_aggregates.current_l3_any_t0_max_a,
    EXCLUDED.current_l3_any_t0_max_a
  ),
  current_l3_any_t0_max_timestamp = case
    when EXCLUDED.current_l3_any_t0_max_a > abb_b2x_aggregates.current_l3_any_t0_max_a then EXCLUDED.current_l3_any_t0_max_timestamp
    else abb_b2x_aggregates.current_l3_any_t0_max_timestamp
  end,
  current_l3_any_t0_min_a = least(
    abb_b2x_aggregates.current_l3_any_t0_min_a,
    EXCLUDED.current_l3_any_t0_min_a
  ),
  current_l3_any_t0_min_timestamp = case
    when EXCLUDED.current_l3_any_t0_min_a < abb_b2x_aggregates.current_l3_any_t0_min_a then EXCLUDED.current_l3_any_t0_min_timestamp
    else abb_b2x_aggregates.current_l3_any_t0_min_timestamp
  end,
  derived_active_power_l1_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l1_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l1_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l1_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w,
    EXCLUDED.derived_active_power_l1_export_t0_max_w
  ),
  derived_active_power_l1_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l1_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w then EXCLUDED.derived_active_power_l1_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_export_t0_max_timestamp
  end,
  derived_active_power_l1_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w,
    EXCLUDED.derived_active_power_l1_export_t0_min_w
  ),
  derived_active_power_l1_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l1_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w then EXCLUDED.derived_active_power_l1_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_export_t0_min_timestamp
  end,
  derived_active_power_l1_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l1_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l1_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l1_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w,
    EXCLUDED.derived_active_power_l1_import_t0_max_w
  ),
  derived_active_power_l1_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w then EXCLUDED.derived_active_power_l1_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_import_t0_max_timestamp
  end,
  derived_active_power_l1_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w,
    EXCLUDED.derived_active_power_l1_import_t0_min_w
  ),
  derived_active_power_l1_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w then EXCLUDED.derived_active_power_l1_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l1_import_t0_min_timestamp
  end,
  derived_active_power_l2_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l2_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l2_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l2_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w,
    EXCLUDED.derived_active_power_l2_export_t0_max_w
  ),
  derived_active_power_l2_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l2_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w then EXCLUDED.derived_active_power_l2_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_export_t0_max_timestamp
  end,
  derived_active_power_l2_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w,
    EXCLUDED.derived_active_power_l2_export_t0_min_w
  ),
  derived_active_power_l2_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l2_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w then EXCLUDED.derived_active_power_l2_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_export_t0_min_timestamp
  end,
  derived_active_power_l2_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l2_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l2_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l2_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w,
    EXCLUDED.derived_active_power_l2_import_t0_max_w
  ),
  derived_active_power_l2_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w then EXCLUDED.derived_active_power_l2_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_import_t0_max_timestamp
  end,
  derived_active_power_l2_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w,
    EXCLUDED.derived_active_power_l2_import_t0_min_w
  ),
  derived_active_power_l2_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w then EXCLUDED.derived_active_power_l2_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l2_import_t0_min_timestamp
  end,
  derived_active_power_l3_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l3_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l3_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l3_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w,
    EXCLUDED.derived_active_power_l3_export_t0_max_w
  ),
  derived_active_power_l3_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l3_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w then EXCLUDED.derived_active_power_l3_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_export_t0_max_timestamp
  end,
  derived_active_power_l3_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w,
    EXCLUDED.derived_active_power_l3_export_t0_min_w
  ),
  derived_active_power_l3_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l3_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w then EXCLUDED.derived_active_power_l3_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_export_t0_min_timestamp
  end,
  derived_active_power_l3_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_l3_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_l3_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_l3_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w,
    EXCLUDED.derived_active_power_l3_import_t0_max_w
  ),
  derived_active_power_l3_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w then EXCLUDED.derived_active_power_l3_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_import_t0_max_timestamp
  end,
  derived_active_power_l3_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w,
    EXCLUDED.derived_active_power_l3_import_t0_min_w
  ),
  derived_active_power_l3_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w then EXCLUDED.derived_active_power_l3_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_l3_import_t0_min_timestamp
  end,
  derived_active_power_total_export_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_export_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_export_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_export_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_export_t0_max_w,
    EXCLUDED.derived_active_power_total_export_t0_max_w
  ),
  derived_active_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_max_w > abb_b2x_aggregates.derived_active_power_total_export_t0_max_w then EXCLUDED.derived_active_power_total_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_export_t0_max_timestamp
  end,
  derived_active_power_total_export_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_export_t0_min_w,
    EXCLUDED.derived_active_power_total_export_t0_min_w
  ),
  derived_active_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_min_w < abb_b2x_aggregates.derived_active_power_total_export_t0_min_w then EXCLUDED.derived_active_power_total_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_export_t0_min_timestamp
  end,
  derived_active_power_total_import_t0_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_import_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_import_t0_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_import_t0_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_import_t0_max_w,
    EXCLUDED.derived_active_power_total_import_t0_max_w
  ),
  derived_active_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_max_w > abb_b2x_aggregates.derived_active_power_total_import_t0_max_w then EXCLUDED.derived_active_power_total_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t0_max_timestamp
  end,
  derived_active_power_total_import_t0_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_import_t0_min_w,
    EXCLUDED.derived_active_power_total_import_t0_min_w
  ),
  derived_active_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_min_w < abb_b2x_aggregates.derived_active_power_total_import_t0_min_w then EXCLUDED.derived_active_power_total_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t0_min_timestamp
  end,
  derived_active_power_total_import_t1_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_import_t1_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_import_t1_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_import_t1_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_import_t1_max_w,
    EXCLUDED.derived_active_power_total_import_t1_max_w
  ),
  derived_active_power_total_import_t1_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_max_w > abb_b2x_aggregates.derived_active_power_total_import_t1_max_w then EXCLUDED.derived_active_power_total_import_t1_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t1_max_timestamp
  end,
  derived_active_power_total_import_t1_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_import_t1_min_w,
    EXCLUDED.derived_active_power_total_import_t1_min_w
  ),
  derived_active_power_total_import_t1_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_min_w < abb_b2x_aggregates.derived_active_power_total_import_t1_min_w then EXCLUDED.derived_active_power_total_import_t1_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t1_min_timestamp
  end,
  derived_active_power_total_import_t2_avg_w = (
    abb_b2x_aggregates.derived_active_power_total_import_t2_avg_w * abb_b2x_aggregates.count + EXCLUDED.derived_active_power_total_import_t2_avg_w * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_active_power_total_import_t2_max_w = greatest(
    abb_b2x_aggregates.derived_active_power_total_import_t2_max_w,
    EXCLUDED.derived_active_power_total_import_t2_max_w
  ),
  derived_active_power_total_import_t2_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_max_w > abb_b2x_aggregates.derived_active_power_total_import_t2_max_w then EXCLUDED.derived_active_power_total_import_t2_max_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t2_max_timestamp
  end,
  derived_active_power_total_import_t2_min_w = least(
    abb_b2x_aggregates.derived_active_power_total_import_t2_min_w,
    EXCLUDED.derived_active_power_total_import_t2_min_w
  ),
  derived_active_power_total_import_t2_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_min_w < abb_b2x_aggregates.derived_active_power_total_import_t2_min_w then EXCLUDED.derived_active_power_total_import_t2_min_timestamp
    else abb_b2x_aggregates.derived_active_power_total_import_t2_min_timestamp
  end,
  derived_reactive_power_l1_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l1_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l1_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l1_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var,
    EXCLUDED.derived_reactive_power_l1_export_t0_max_var
  ),
  derived_reactive_power_l1_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var then EXCLUDED.derived_reactive_power_l1_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_timestamp
  end,
  derived_reactive_power_l1_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var,
    EXCLUDED.derived_reactive_power_l1_export_t0_min_var
  ),
  derived_reactive_power_l1_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var then EXCLUDED.derived_reactive_power_l1_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_timestamp
  end,
  derived_reactive_power_l1_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l1_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l1_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l1_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var,
    EXCLUDED.derived_reactive_power_l1_import_t0_max_var
  ),
  derived_reactive_power_l1_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var then EXCLUDED.derived_reactive_power_l1_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_timestamp
  end,
  derived_reactive_power_l1_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var,
    EXCLUDED.derived_reactive_power_l1_import_t0_min_var
  ),
  derived_reactive_power_l1_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l1_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var then EXCLUDED.derived_reactive_power_l1_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_timestamp
  end,
  derived_reactive_power_l2_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l2_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l2_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l2_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var,
    EXCLUDED.derived_reactive_power_l2_export_t0_max_var
  ),
  derived_reactive_power_l2_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var then EXCLUDED.derived_reactive_power_l2_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_timestamp
  end,
  derived_reactive_power_l2_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var,
    EXCLUDED.derived_reactive_power_l2_export_t0_min_var
  ),
  derived_reactive_power_l2_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var then EXCLUDED.derived_reactive_power_l2_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_timestamp
  end,
  derived_reactive_power_l2_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l2_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l2_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l2_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var,
    EXCLUDED.derived_reactive_power_l2_import_t0_max_var
  ),
  derived_reactive_power_l2_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var then EXCLUDED.derived_reactive_power_l2_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_timestamp
  end,
  derived_reactive_power_l2_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var,
    EXCLUDED.derived_reactive_power_l2_import_t0_min_var
  ),
  derived_reactive_power_l2_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l2_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var then EXCLUDED.derived_reactive_power_l2_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_timestamp
  end,
  derived_reactive_power_l3_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l3_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l3_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l3_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var,
    EXCLUDED.derived_reactive_power_l3_export_t0_max_var
  ),
  derived_reactive_power_l3_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var then EXCLUDED.derived_reactive_power_l3_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_timestamp
  end,
  derived_reactive_power_l3_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var,
    EXCLUDED.derived_reactive_power_l3_export_t0_min_var
  ),
  derived_reactive_power_l3_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var then EXCLUDED.derived_reactive_power_l3_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_timestamp
  end,
  derived_reactive_power_l3_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_l3_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_l3_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_l3_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var,
    EXCLUDED.derived_reactive_power_l3_import_t0_max_var
  ),
  derived_reactive_power_l3_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var then EXCLUDED.derived_reactive_power_l3_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_timestamp
  end,
  derived_reactive_power_l3_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var,
    EXCLUDED.derived_reactive_power_l3_import_t0_min_var
  ),
  derived_reactive_power_l3_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_l3_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var then EXCLUDED.derived_reactive_power_l3_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_timestamp
  end,
  derived_reactive_power_total_export_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_total_export_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_total_export_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_total_export_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var,
    EXCLUDED.derived_reactive_power_total_export_t0_max_var
  ),
  derived_reactive_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var then EXCLUDED.derived_reactive_power_total_export_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_timestamp
  end,
  derived_reactive_power_total_export_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var,
    EXCLUDED.derived_reactive_power_total_export_t0_min_var
  ),
  derived_reactive_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var then EXCLUDED.derived_reactive_power_total_export_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_timestamp
  end,
  derived_reactive_power_total_import_t0_avg_var = (
    abb_b2x_aggregates.derived_reactive_power_total_import_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.derived_reactive_power_total_import_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  derived_reactive_power_total_import_t0_max_var = greatest(
    abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var,
    EXCLUDED.derived_reactive_power_total_import_t0_max_var
  ),
  derived_reactive_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var then EXCLUDED.derived_reactive_power_total_import_t0_max_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_timestamp
  end,
  derived_reactive_power_total_import_t0_min_var = least(
    abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var,
    EXCLUDED.derived_reactive_power_total_import_t0_min_var
  ),
  derived_reactive_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var then EXCLUDED.derived_reactive_power_total_import_t0_min_timestamp
    else abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_timestamp
  end,
  reactive_energy_l1_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l1_export_t0_max_varh,
    EXCLUDED.reactive_energy_l1_export_t0_max_varh
  ),
  reactive_energy_l1_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l1_export_t0_min_varh,
    EXCLUDED.reactive_energy_l1_export_t0_min_varh
  ),
  reactive_energy_l1_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l1_import_t0_max_varh,
    EXCLUDED.reactive_energy_l1_import_t0_max_varh
  ),
  reactive_energy_l1_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l1_import_t0_min_varh,
    EXCLUDED.reactive_energy_l1_import_t0_min_varh
  ),
  reactive_energy_l2_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l2_export_t0_max_varh,
    EXCLUDED.reactive_energy_l2_export_t0_max_varh
  ),
  reactive_energy_l2_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l2_export_t0_min_varh,
    EXCLUDED.reactive_energy_l2_export_t0_min_varh
  ),
  reactive_energy_l2_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l2_import_t0_max_varh,
    EXCLUDED.reactive_energy_l2_import_t0_max_varh
  ),
  reactive_energy_l2_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l2_import_t0_min_varh,
    EXCLUDED.reactive_energy_l2_import_t0_min_varh
  ),
  reactive_energy_l3_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l3_export_t0_max_varh,
    EXCLUDED.reactive_energy_l3_export_t0_max_varh
  ),
  reactive_energy_l3_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l3_export_t0_min_varh,
    EXCLUDED.reactive_energy_l3_export_t0_min_varh
  ),
  reactive_energy_l3_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_l3_import_t0_max_varh,
    EXCLUDED.reactive_energy_l3_import_t0_max_varh
  ),
  reactive_energy_l3_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_l3_import_t0_min_varh,
    EXCLUDED.reactive_energy_l3_import_t0_min_varh
  ),
  reactive_energy_total_export_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_total_export_t0_max_varh,
    EXCLUDED.reactive_energy_total_export_t0_max_varh
  ),
  reactive_energy_total_export_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_total_export_t0_min_varh,
    EXCLUDED.reactive_energy_total_export_t0_min_varh
  ),
  reactive_energy_total_import_t0_max_varh = greatest(
    abb_b2x_aggregates.reactive_energy_total_import_t0_max_varh,
    EXCLUDED.reactive_energy_total_import_t0_max_varh
  ),
  reactive_energy_total_import_t0_min_varh = least(
    abb_b2x_aggregates.reactive_energy_total_import_t0_min_varh,
    EXCLUDED.reactive_energy_total_import_t0_min_varh
  ),
  reactive_power_l1_net_t0_avg_var = (
    abb_b2x_aggregates.reactive_power_l1_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l1_net_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  reactive_power_l1_net_t0_max_var = greatest(
    abb_b2x_aggregates.reactive_power_l1_net_t0_max_var,
    EXCLUDED.reactive_power_l1_net_t0_max_var
  ),
  reactive_power_l1_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_l1_net_t0_max_var > abb_b2x_aggregates.reactive_power_l1_net_t0_max_var then EXCLUDED.reactive_power_l1_net_t0_max_timestamp
    else abb_b2x_aggregates.reactive_power_l1_net_t0_max_timestamp
  end,
  reactive_power_l1_net_t0_min_var = least(
    abb_b2x_aggregates.reactive_power_l1_net_t0_min_var,
    EXCLUDED.reactive_power_l1_net_t0_min_var
  ),
  reactive_power_l1_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_l1_net_t0_min_var < abb_b2x_aggregates.reactive_power_l1_net_t0_min_var then EXCLUDED.reactive_power_l1_net_t0_min_timestamp
    else abb_b2x_aggregates.reactive_power_l1_net_t0_min_timestamp
  end,
  reactive_power_l2_net_t0_avg_var = (
    abb_b2x_aggregates.reactive_power_l2_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l2_net_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  reactive_power_l2_net_t0_max_var = greatest(
    abb_b2x_aggregates.reactive_power_l2_net_t0_max_var,
    EXCLUDED.reactive_power_l2_net_t0_max_var
  ),
  reactive_power_l2_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_l2_net_t0_max_var > abb_b2x_aggregates.reactive_power_l2_net_t0_max_var then EXCLUDED.reactive_power_l2_net_t0_max_timestamp
    else abb_b2x_aggregates.reactive_power_l2_net_t0_max_timestamp
  end,
  reactive_power_l2_net_t0_min_var = least(
    abb_b2x_aggregates.reactive_power_l2_net_t0_min_var,
    EXCLUDED.reactive_power_l2_net_t0_min_var
  ),
  reactive_power_l2_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_l2_net_t0_min_var < abb_b2x_aggregates.reactive_power_l2_net_t0_min_var then EXCLUDED.reactive_power_l2_net_t0_min_timestamp
    else abb_b2x_aggregates.reactive_power_l2_net_t0_min_timestamp
  end,
  reactive_power_l3_net_t0_avg_var = (
    abb_b2x_aggregates.reactive_power_l3_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l3_net_t0_avg_var * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  reactive_power_l3_net_t0_max_var = greatest(
    abb_b2x_aggregates.reactive_power_l3_net_t0_max_var,
    EXCLUDED.reactive_power_l3_net_t0_max_var
  ),
  reactive_power_l3_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_l3_net_t0_max_var > abb_b2x_aggregates.reactive_power_l3_net_t0_max_var then EXCLUDED.reactive_power_l3_net_t0_max_timestamp
    else abb_b2x_aggregates.reactive_power_l3_net_t0_max_timestamp
  end,
  reactive_power_l3_net_t0_min_var = least(
    abb_b2x_aggregates.reactive_power_l3_net_t0_min_var,
    EXCLUDED.reactive_power_l3_net_t0_min_var
  ),
  reactive_power_l3_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_l3_net_t0_min_var < abb_b2x_aggregates.reactive_power_l3_net_t0_min_var then EXCLUDED.reactive_power_l3_net_t0_min_timestamp
    else abb_b2x_aggregates.reactive_power_l3_net_t0_min_timestamp
  end,
  voltage_l1_any_t0_avg_v = (
    abb_b2x_aggregates.voltage_l1_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l1_any_t0_avg_v * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  voltage_l1_any_t0_max_v = greatest(
    abb_b2x_aggregates.voltage_l1_any_t0_max_v,
    EXCLUDED.voltage_l1_any_t0_max_v
  ),
  voltage_l1_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_max_v > abb_b2x_aggregates.voltage_l1_any_t0_max_v then EXCLUDED.voltage_l1_any_t0_max_timestamp
    else abb_b2x_aggregates.voltage_l1_any_t0_max_timestamp
  end,
  voltage_l1_any_t0_min_v = least(
    abb_b2x_aggregates.voltage_l1_any_t0_min_v,
    EXCLUDED.voltage_l1_any_t0_min_v
  ),
  voltage_l1_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_min_v < abb_b2x_aggregates.voltage_l1_any_t0_min_v then EXCLUDED.voltage_l1_any_t0_min_timestamp
    else abb_b2x_aggregates.voltage_l1_any_t0_min_timestamp
  end,
  voltage_l2_any_t0_avg_v = (
    abb_b2x_aggregates.voltage_l2_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l2_any_t0_avg_v * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  voltage_l2_any_t0_max_v = greatest(
    abb_b2x_aggregates.voltage_l2_any_t0_max_v,
    EXCLUDED.voltage_l2_any_t0_max_v
  ),
  voltage_l2_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_max_v > abb_b2x_aggregates.voltage_l2_any_t0_max_v then EXCLUDED.voltage_l2_any_t0_max_timestamp
    else abb_b2x_aggregates.voltage_l2_any_t0_max_timestamp
  end,
  voltage_l2_any_t0_min_v = least(
    abb_b2x_aggregates.voltage_l2_any_t0_min_v,
    EXCLUDED.voltage_l2_any_t0_min_v
  ),
  voltage_l2_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_min_v < abb_b2x_aggregates.voltage_l2_any_t0_min_v then EXCLUDED.voltage_l2_any_t0_min_timestamp
    else abb_b2x_aggregates.voltage_l2_any_t0_min_timestamp
  end,
  voltage_l3_any_t0_avg_v = (
    abb_b2x_aggregates.voltage_l3_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l3_any_t0_avg_v * EXCLUDED.count
  ) / (abb_b2x_aggregates.count + EXCLUDED.count),
  voltage_l3_any_t0_max_v = greatest(
    abb_b2x_aggregates.voltage_l3_any_t0_max_v,
    EXCLUDED.voltage_l3_any_t0_max_v
  ),
  voltage_l3_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_max_v > abb_b2x_aggregates.voltage_l3_any_t0_max_v then EXCLUDED.voltage_l3_any_t0_max_timestamp
    else abb_b2x_aggregates.voltage_l3_any_t0_max_timestamp
  end,
  voltage_l3_any_t0_min_v = least(
    abb_b2x_aggregates.voltage_l3_any_t0_min_v,
    EXCLUDED.voltage_l3_any_t0_min_v
  ),
  voltage_l3_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_min_v < abb_b2x_aggregates.voltage_l3_any_t0_min_v then EXCLUDED.voltage_l3_any_t0_min_timestamp
    else abb_b2x_aggregates.voltage_l3_any_t0_min_timestamp
  end;

with
  old as (
    select
      abb_b2x_aggregates.*
    from
      abb_b2x_aggregates
    where
      abb_b2x_aggregates.timestamp =:'p28_timestamp'::timestamptz
      and abb_b2x_aggregates.interval =:'p28_interval'::interval_entity
      and abb_b2x_aggregates.meter_id =:'p28_meter_id'
      and abb_b2x_aggregates.measurement_location_id =:'p28_measurement_location_id'
  ),
  new as (
    insert into
      abb_b2x_aggregates (
        timestamp,
        interval,
        meter_id,
        measurement_location_id,
        count,
        quarter_hour_count,
        active_energy_l1_export_t0_max_wh,
        active_energy_l1_export_t0_min_wh,
        active_energy_l1_import_t0_max_wh,
        active_energy_l1_import_t0_min_wh,
        active_energy_l2_export_t0_max_wh,
        active_energy_l2_export_t0_min_wh,
        active_energy_l2_import_t0_max_wh,
        active_energy_l2_import_t0_min_wh,
        active_energy_l3_export_t0_max_wh,
        active_energy_l3_export_t0_min_wh,
        active_energy_l3_import_t0_max_wh,
        active_energy_l3_import_t0_min_wh,
        active_energy_total_export_t0_max_wh,
        active_energy_total_export_t0_min_wh,
        active_energy_total_import_t0_max_wh,
        active_energy_total_import_t0_min_wh,
        active_energy_total_import_t1_max_wh,
        active_energy_total_import_t1_min_wh,
        active_energy_total_import_t2_max_wh,
        active_energy_total_import_t2_min_wh,
        active_power_l1_net_t0_avg_w,
        active_power_l1_net_t0_max_w,
        active_power_l1_net_t0_max_timestamp,
        active_power_l1_net_t0_min_w,
        active_power_l1_net_t0_min_timestamp,
        active_power_l2_net_t0_avg_w,
        active_power_l2_net_t0_max_w,
        active_power_l2_net_t0_max_timestamp,
        active_power_l2_net_t0_min_w,
        active_power_l2_net_t0_min_timestamp,
        active_power_l3_net_t0_avg_w,
        active_power_l3_net_t0_max_w,
        active_power_l3_net_t0_max_timestamp,
        active_power_l3_net_t0_min_w,
        active_power_l3_net_t0_min_timestamp,
        current_l1_any_t0_avg_a,
        current_l1_any_t0_max_a,
        current_l1_any_t0_max_timestamp,
        current_l1_any_t0_min_a,
        current_l1_any_t0_min_timestamp,
        current_l2_any_t0_avg_a,
        current_l2_any_t0_max_a,
        current_l2_any_t0_max_timestamp,
        current_l2_any_t0_min_a,
        current_l2_any_t0_min_timestamp,
        current_l3_any_t0_avg_a,
        current_l3_any_t0_max_a,
        current_l3_any_t0_max_timestamp,
        current_l3_any_t0_min_a,
        current_l3_any_t0_min_timestamp,
        derived_active_power_l1_export_t0_avg_w,
        derived_active_power_l1_export_t0_max_w,
        derived_active_power_l1_export_t0_max_timestamp,
        derived_active_power_l1_export_t0_min_w,
        derived_active_power_l1_export_t0_min_timestamp,
        derived_active_power_l1_import_t0_avg_w,
        derived_active_power_l1_import_t0_max_w,
        derived_active_power_l1_import_t0_max_timestamp,
        derived_active_power_l1_import_t0_min_w,
        derived_active_power_l1_import_t0_min_timestamp,
        derived_active_power_l2_export_t0_avg_w,
        derived_active_power_l2_export_t0_max_w,
        derived_active_power_l2_export_t0_max_timestamp,
        derived_active_power_l2_export_t0_min_w,
        derived_active_power_l2_export_t0_min_timestamp,
        derived_active_power_l2_import_t0_avg_w,
        derived_active_power_l2_import_t0_max_w,
        derived_active_power_l2_import_t0_max_timestamp,
        derived_active_power_l2_import_t0_min_w,
        derived_active_power_l2_import_t0_min_timestamp,
        derived_active_power_l3_export_t0_avg_w,
        derived_active_power_l3_export_t0_max_w,
        derived_active_power_l3_export_t0_max_timestamp,
        derived_active_power_l3_export_t0_min_w,
        derived_active_power_l3_export_t0_min_timestamp,
        derived_active_power_l3_import_t0_avg_w,
        derived_active_power_l3_import_t0_max_w,
        derived_active_power_l3_import_t0_max_timestamp,
        derived_active_power_l3_import_t0_min_w,
        derived_active_power_l3_import_t0_min_timestamp,
        derived_active_power_total_export_t0_avg_w,
        derived_active_power_total_export_t0_max_w,
        derived_active_power_total_export_t0_max_timestamp,
        derived_active_power_total_export_t0_min_w,
        derived_active_power_total_export_t0_min_timestamp,
        derived_active_power_total_import_t0_avg_w,
        derived_active_power_total_import_t0_max_w,
        derived_active_power_total_import_t0_max_timestamp,
        derived_active_power_total_import_t0_min_w,
        derived_active_power_total_import_t0_min_timestamp,
        derived_active_power_total_import_t1_avg_w,
        derived_active_power_total_import_t1_max_w,
        derived_active_power_total_import_t1_max_timestamp,
        derived_active_power_total_import_t1_min_w,
        derived_active_power_total_import_t1_min_timestamp,
        derived_active_power_total_import_t2_avg_w,
        derived_active_power_total_import_t2_max_w,
        derived_active_power_total_import_t2_max_timestamp,
        derived_active_power_total_import_t2_min_w,
        derived_active_power_total_import_t2_min_timestamp,
        derived_reactive_power_l1_export_t0_avg_var,
        derived_reactive_power_l1_export_t0_max_var,
        derived_reactive_power_l1_export_t0_max_timestamp,
        derived_reactive_power_l1_export_t0_min_var,
        derived_reactive_power_l1_export_t0_min_timestamp,
        derived_reactive_power_l1_import_t0_avg_var,
        derived_reactive_power_l1_import_t0_max_var,
        derived_reactive_power_l1_import_t0_max_timestamp,
        derived_reactive_power_l1_import_t0_min_var,
        derived_reactive_power_l1_import_t0_min_timestamp,
        derived_reactive_power_l2_export_t0_avg_var,
        derived_reactive_power_l2_export_t0_max_var,
        derived_reactive_power_l2_export_t0_max_timestamp,
        derived_reactive_power_l2_export_t0_min_var,
        derived_reactive_power_l2_export_t0_min_timestamp,
        derived_reactive_power_l2_import_t0_avg_var,
        derived_reactive_power_l2_import_t0_max_var,
        derived_reactive_power_l2_import_t0_max_timestamp,
        derived_reactive_power_l2_import_t0_min_var,
        derived_reactive_power_l2_import_t0_min_timestamp,
        derived_reactive_power_l3_export_t0_avg_var,
        derived_reactive_power_l3_export_t0_max_var,
        derived_reactive_power_l3_export_t0_max_timestamp,
        derived_reactive_power_l3_export_t0_min_var,
        derived_reactive_power_l3_export_t0_min_timestamp,
        derived_reactive_power_l3_import_t0_avg_var,
        derived_reactive_power_l3_import_t0_max_var,
        derived_reactive_power_l3_import_t0_max_timestamp,
        derived_reactive_power_l3_import_t0_min_var,
        derived_reactive_power_l3_import_t0_min_timestamp,
        derived_reactive_power_total_export_t0_avg_var,
        derived_reactive_power_total_export_t0_max_var,
        derived_reactive_power_total_export_t0_max_timestamp,
        derived_reactive_power_total_export_t0_min_var,
        derived_reactive_power_total_export_t0_min_timestamp,
        derived_reactive_power_total_import_t0_avg_var,
        derived_reactive_power_total_import_t0_max_var,
        derived_reactive_power_total_import_t0_max_timestamp,
        derived_reactive_power_total_import_t0_min_var,
        derived_reactive_power_total_import_t0_min_timestamp,
        reactive_energy_l1_export_t0_max_varh,
        reactive_energy_l1_export_t0_min_varh,
        reactive_energy_l1_import_t0_max_varh,
        reactive_energy_l1_import_t0_min_varh,
        reactive_energy_l2_export_t0_max_varh,
        reactive_energy_l2_export_t0_min_varh,
        reactive_energy_l2_import_t0_max_varh,
        reactive_energy_l2_import_t0_min_varh,
        reactive_energy_l3_export_t0_max_varh,
        reactive_energy_l3_export_t0_min_varh,
        reactive_energy_l3_import_t0_max_varh,
        reactive_energy_l3_import_t0_min_varh,
        reactive_energy_total_export_t0_max_varh,
        reactive_energy_total_export_t0_min_varh,
        reactive_energy_total_import_t0_max_varh,
        reactive_energy_total_import_t0_min_varh,
        reactive_power_l1_net_t0_avg_var,
        reactive_power_l1_net_t0_max_var,
        reactive_power_l1_net_t0_max_timestamp,
        reactive_power_l1_net_t0_min_var,
        reactive_power_l1_net_t0_min_timestamp,
        reactive_power_l2_net_t0_avg_var,
        reactive_power_l2_net_t0_max_var,
        reactive_power_l2_net_t0_max_timestamp,
        reactive_power_l2_net_t0_min_var,
        reactive_power_l2_net_t0_min_timestamp,
        reactive_power_l3_net_t0_avg_var,
        reactive_power_l3_net_t0_max_var,
        reactive_power_l3_net_t0_max_timestamp,
        reactive_power_l3_net_t0_min_var,
        reactive_power_l3_net_t0_min_timestamp,
        voltage_l1_any_t0_avg_v,
        voltage_l1_any_t0_max_v,
        voltage_l1_any_t0_max_timestamp,
        voltage_l1_any_t0_min_v,
        voltage_l1_any_t0_min_timestamp,
        voltage_l2_any_t0_avg_v,
        voltage_l2_any_t0_max_v,
        voltage_l2_any_t0_max_timestamp,
        voltage_l2_any_t0_min_v,
        voltage_l2_any_t0_min_timestamp,
        voltage_l3_any_t0_avg_v,
        voltage_l3_any_t0_max_v,
        voltage_l3_any_t0_max_timestamp,
        voltage_l3_any_t0_min_v,
        voltage_l3_any_t0_min_timestamp
      )
    values
      (
:'p28_timestamp'::timestamptz,
:'p28_interval'::interval_entity,
:'p28_meter_id',
:'p28_measurement_location_id',
:'p28_count',
:'p28_quarter_hour_count',
:'p28_active_energy_l1_export_t0_max_wh',
:'p28_active_energy_l1_export_t0_min_wh',
:'p28_active_energy_l1_import_t0_max_wh',
:'p28_active_energy_l1_import_t0_min_wh',
:'p28_active_energy_l2_export_t0_max_wh',
:'p28_active_energy_l2_export_t0_min_wh',
:'p28_active_energy_l2_import_t0_max_wh',
:'p28_active_energy_l2_import_t0_min_wh',
:'p28_active_energy_l3_export_t0_max_wh',
:'p28_active_energy_l3_export_t0_min_wh',
:'p28_active_energy_l3_import_t0_max_wh',
:'p28_active_energy_l3_import_t0_min_wh',
:'p28_active_energy_total_export_t0_max_wh',
:'p28_active_energy_total_export_t0_min_wh',
:'p28_active_energy_total_import_t0_max_wh',
:'p28_active_energy_total_import_t0_min_wh',
:'p28_active_energy_total_import_t1_max_wh',
:'p28_active_energy_total_import_t1_min_wh',
:'p28_active_energy_total_import_t2_max_wh',
:'p28_active_energy_total_import_t2_min_wh',
:'p28_active_power_l1_net_t0_avg_w',
:'p28_active_power_l1_net_t0_max_w',
:'p28_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p28_active_power_l1_net_t0_min_w',
:'p28_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p28_active_power_l2_net_t0_avg_w',
:'p28_active_power_l2_net_t0_max_w',
:'p28_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p28_active_power_l2_net_t0_min_w',
:'p28_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p28_active_power_l3_net_t0_avg_w',
:'p28_active_power_l3_net_t0_max_w',
:'p28_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p28_active_power_l3_net_t0_min_w',
:'p28_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p28_current_l1_any_t0_avg_a',
:'p28_current_l1_any_t0_max_a',
:'p28_current_l1_any_t0_max_timestamp'::timestamptz,
:'p28_current_l1_any_t0_min_a',
:'p28_current_l1_any_t0_min_timestamp'::timestamptz,
:'p28_current_l2_any_t0_avg_a',
:'p28_current_l2_any_t0_max_a',
:'p28_current_l2_any_t0_max_timestamp'::timestamptz,
:'p28_current_l2_any_t0_min_a',
:'p28_current_l2_any_t0_min_timestamp'::timestamptz,
:'p28_current_l3_any_t0_avg_a',
:'p28_current_l3_any_t0_max_a',
:'p28_current_l3_any_t0_max_timestamp'::timestamptz,
:'p28_current_l3_any_t0_min_a',
:'p28_current_l3_any_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_l1_export_t0_avg_w',
:'p28_derived_active_power_l1_export_t0_max_w',
:'p28_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_l1_export_t0_min_w',
:'p28_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_l1_import_t0_avg_w',
:'p28_derived_active_power_l1_import_t0_max_w',
:'p28_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_l1_import_t0_min_w',
:'p28_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_l2_export_t0_avg_w',
:'p28_derived_active_power_l2_export_t0_max_w',
:'p28_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_l2_export_t0_min_w',
:'p28_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_l2_import_t0_avg_w',
:'p28_derived_active_power_l2_import_t0_max_w',
:'p28_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_l2_import_t0_min_w',
:'p28_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_l3_export_t0_avg_w',
:'p28_derived_active_power_l3_export_t0_max_w',
:'p28_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_l3_export_t0_min_w',
:'p28_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_l3_import_t0_avg_w',
:'p28_derived_active_power_l3_import_t0_max_w',
:'p28_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_l3_import_t0_min_w',
:'p28_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_total_export_t0_avg_w',
:'p28_derived_active_power_total_export_t0_max_w',
:'p28_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_total_export_t0_min_w',
:'p28_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_total_import_t0_avg_w',
:'p28_derived_active_power_total_import_t0_max_w',
:'p28_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p28_derived_active_power_total_import_t0_min_w',
:'p28_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p28_derived_active_power_total_import_t1_avg_w',
:'p28_derived_active_power_total_import_t1_max_w',
:'p28_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p28_derived_active_power_total_import_t1_min_w',
:'p28_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p28_derived_active_power_total_import_t2_avg_w',
:'p28_derived_active_power_total_import_t2_max_w',
:'p28_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p28_derived_active_power_total_import_t2_min_w',
:'p28_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_l1_export_t0_avg_var',
:'p28_derived_reactive_power_l1_export_t0_max_var',
:'p28_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_l1_export_t0_min_var',
:'p28_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_l1_import_t0_avg_var',
:'p28_derived_reactive_power_l1_import_t0_max_var',
:'p28_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_l1_import_t0_min_var',
:'p28_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_l2_export_t0_avg_var',
:'p28_derived_reactive_power_l2_export_t0_max_var',
:'p28_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_l2_export_t0_min_var',
:'p28_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_l2_import_t0_avg_var',
:'p28_derived_reactive_power_l2_import_t0_max_var',
:'p28_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_l2_import_t0_min_var',
:'p28_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_l3_export_t0_avg_var',
:'p28_derived_reactive_power_l3_export_t0_max_var',
:'p28_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_l3_export_t0_min_var',
:'p28_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_l3_import_t0_avg_var',
:'p28_derived_reactive_power_l3_import_t0_max_var',
:'p28_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_l3_import_t0_min_var',
:'p28_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_total_export_t0_avg_var',
:'p28_derived_reactive_power_total_export_t0_max_var',
:'p28_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_total_export_t0_min_var',
:'p28_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p28_derived_reactive_power_total_import_t0_avg_var',
:'p28_derived_reactive_power_total_import_t0_max_var',
:'p28_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p28_derived_reactive_power_total_import_t0_min_var',
:'p28_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p28_reactive_energy_l1_export_t0_max_varh',
:'p28_reactive_energy_l1_export_t0_min_varh',
:'p28_reactive_energy_l1_import_t0_max_varh',
:'p28_reactive_energy_l1_import_t0_min_varh',
:'p28_reactive_energy_l2_export_t0_max_varh',
:'p28_reactive_energy_l2_export_t0_min_varh',
:'p28_reactive_energy_l2_import_t0_max_varh',
:'p28_reactive_energy_l2_import_t0_min_varh',
:'p28_reactive_energy_l3_export_t0_max_varh',
:'p28_reactive_energy_l3_export_t0_min_varh',
:'p28_reactive_energy_l3_import_t0_max_varh',
:'p28_reactive_energy_l3_import_t0_min_varh',
:'p28_reactive_energy_total_export_t0_max_varh',
:'p28_reactive_energy_total_export_t0_min_varh',
:'p28_reactive_energy_total_import_t0_max_varh',
:'p28_reactive_energy_total_import_t0_min_varh',
:'p28_reactive_power_l1_net_t0_avg_var',
:'p28_reactive_power_l1_net_t0_max_var',
:'p28_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p28_reactive_power_l1_net_t0_min_var',
:'p28_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p28_reactive_power_l2_net_t0_avg_var',
:'p28_reactive_power_l2_net_t0_max_var',
:'p28_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p28_reactive_power_l2_net_t0_min_var',
:'p28_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p28_reactive_power_l3_net_t0_avg_var',
:'p28_reactive_power_l3_net_t0_max_var',
:'p28_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p28_reactive_power_l3_net_t0_min_var',
:'p28_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p28_voltage_l1_any_t0_avg_v',
:'p28_voltage_l1_any_t0_max_v',
:'p28_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p28_voltage_l1_any_t0_min_v',
:'p28_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p28_voltage_l2_any_t0_avg_v',
:'p28_voltage_l2_any_t0_max_v',
:'p28_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p28_voltage_l2_any_t0_min_v',
:'p28_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p28_voltage_l3_any_t0_avg_v',
:'p28_voltage_l3_any_t0_max_v',
:'p28_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p28_voltage_l3_any_t0_min_v',
:'p28_voltage_l3_any_t0_min_timestamp'::timestamptz
      )
    on conflict (
      timestamp,
      interval,
      meter_id,
      measurement_location_id
    ) do
    update
    set
      count = abb_b2x_aggregates.count + EXCLUDED.count,
      active_energy_l1_export_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_l1_export_t0_max_wh,
        EXCLUDED.active_energy_l1_export_t0_max_wh
      ),
      active_energy_l1_export_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_l1_export_t0_min_wh,
        EXCLUDED.active_energy_l1_export_t0_min_wh
      ),
      active_energy_l1_import_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_l1_import_t0_max_wh,
        EXCLUDED.active_energy_l1_import_t0_max_wh
      ),
      active_energy_l1_import_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_l1_import_t0_min_wh,
        EXCLUDED.active_energy_l1_import_t0_min_wh
      ),
      active_energy_l2_export_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_l2_export_t0_max_wh,
        EXCLUDED.active_energy_l2_export_t0_max_wh
      ),
      active_energy_l2_export_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_l2_export_t0_min_wh,
        EXCLUDED.active_energy_l2_export_t0_min_wh
      ),
      active_energy_l2_import_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_l2_import_t0_max_wh,
        EXCLUDED.active_energy_l2_import_t0_max_wh
      ),
      active_energy_l2_import_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_l2_import_t0_min_wh,
        EXCLUDED.active_energy_l2_import_t0_min_wh
      ),
      active_energy_l3_export_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_l3_export_t0_max_wh,
        EXCLUDED.active_energy_l3_export_t0_max_wh
      ),
      active_energy_l3_export_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_l3_export_t0_min_wh,
        EXCLUDED.active_energy_l3_export_t0_min_wh
      ),
      active_energy_l3_import_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_l3_import_t0_max_wh,
        EXCLUDED.active_energy_l3_import_t0_max_wh
      ),
      active_energy_l3_import_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_l3_import_t0_min_wh,
        EXCLUDED.active_energy_l3_import_t0_min_wh
      ),
      active_energy_total_export_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_total_export_t0_max_wh,
        EXCLUDED.active_energy_total_export_t0_max_wh
      ),
      active_energy_total_export_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_total_export_t0_min_wh,
        EXCLUDED.active_energy_total_export_t0_min_wh
      ),
      active_energy_total_import_t0_max_wh = greatest(
        abb_b2x_aggregates.active_energy_total_import_t0_max_wh,
        EXCLUDED.active_energy_total_import_t0_max_wh
      ),
      active_energy_total_import_t0_min_wh = least(
        abb_b2x_aggregates.active_energy_total_import_t0_min_wh,
        EXCLUDED.active_energy_total_import_t0_min_wh
      ),
      active_energy_total_import_t1_max_wh = greatest(
        abb_b2x_aggregates.active_energy_total_import_t1_max_wh,
        EXCLUDED.active_energy_total_import_t1_max_wh
      ),
      active_energy_total_import_t1_min_wh = least(
        abb_b2x_aggregates.active_energy_total_import_t1_min_wh,
        EXCLUDED.active_energy_total_import_t1_min_wh
      ),
      active_energy_total_import_t2_max_wh = greatest(
        abb_b2x_aggregates.active_energy_total_import_t2_max_wh,
        EXCLUDED.active_energy_total_import_t2_max_wh
      ),
      active_energy_total_import_t2_min_wh = least(
        abb_b2x_aggregates.active_energy_total_import_t2_min_wh,
        EXCLUDED.active_energy_total_import_t2_min_wh
      ),
      active_power_l1_net_t0_avg_w = (
        abb_b2x_aggregates.active_power_l1_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l1_net_t0_avg_w * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      active_power_l1_net_t0_max_w = greatest(
        abb_b2x_aggregates.active_power_l1_net_t0_max_w,
        EXCLUDED.active_power_l1_net_t0_max_w
      ),
      active_power_l1_net_t0_max_timestamp = case
        when EXCLUDED.active_power_l1_net_t0_max_w > abb_b2x_aggregates.active_power_l1_net_t0_max_w then EXCLUDED.active_power_l1_net_t0_max_timestamp
        else abb_b2x_aggregates.active_power_l1_net_t0_max_timestamp
      end,
      active_power_l1_net_t0_min_w = least(
        abb_b2x_aggregates.active_power_l1_net_t0_min_w,
        EXCLUDED.active_power_l1_net_t0_min_w
      ),
      active_power_l1_net_t0_min_timestamp = case
        when EXCLUDED.active_power_l1_net_t0_min_w < abb_b2x_aggregates.active_power_l1_net_t0_min_w then EXCLUDED.active_power_l1_net_t0_min_timestamp
        else abb_b2x_aggregates.active_power_l1_net_t0_min_timestamp
      end,
      active_power_l2_net_t0_avg_w = (
        abb_b2x_aggregates.active_power_l2_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l2_net_t0_avg_w * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      active_power_l2_net_t0_max_w = greatest(
        abb_b2x_aggregates.active_power_l2_net_t0_max_w,
        EXCLUDED.active_power_l2_net_t0_max_w
      ),
      active_power_l2_net_t0_max_timestamp = case
        when EXCLUDED.active_power_l2_net_t0_max_w > abb_b2x_aggregates.active_power_l2_net_t0_max_w then EXCLUDED.active_power_l2_net_t0_max_timestamp
        else abb_b2x_aggregates.active_power_l2_net_t0_max_timestamp
      end,
      active_power_l2_net_t0_min_w = least(
        abb_b2x_aggregates.active_power_l2_net_t0_min_w,
        EXCLUDED.active_power_l2_net_t0_min_w
      ),
      active_power_l2_net_t0_min_timestamp = case
        when EXCLUDED.active_power_l2_net_t0_min_w < abb_b2x_aggregates.active_power_l2_net_t0_min_w then EXCLUDED.active_power_l2_net_t0_min_timestamp
        else abb_b2x_aggregates.active_power_l2_net_t0_min_timestamp
      end,
      active_power_l3_net_t0_avg_w = (
        abb_b2x_aggregates.active_power_l3_net_t0_avg_w * abb_b2x_aggregates.count + EXCLUDED.active_power_l3_net_t0_avg_w * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      active_power_l3_net_t0_max_w = greatest(
        abb_b2x_aggregates.active_power_l3_net_t0_max_w,
        EXCLUDED.active_power_l3_net_t0_max_w
      ),
      active_power_l3_net_t0_max_timestamp = case
        when EXCLUDED.active_power_l3_net_t0_max_w > abb_b2x_aggregates.active_power_l3_net_t0_max_w then EXCLUDED.active_power_l3_net_t0_max_timestamp
        else abb_b2x_aggregates.active_power_l3_net_t0_max_timestamp
      end,
      active_power_l3_net_t0_min_w = least(
        abb_b2x_aggregates.active_power_l3_net_t0_min_w,
        EXCLUDED.active_power_l3_net_t0_min_w
      ),
      active_power_l3_net_t0_min_timestamp = case
        when EXCLUDED.active_power_l3_net_t0_min_w < abb_b2x_aggregates.active_power_l3_net_t0_min_w then EXCLUDED.active_power_l3_net_t0_min_timestamp
        else abb_b2x_aggregates.active_power_l3_net_t0_min_timestamp
      end,
      current_l1_any_t0_avg_a = (
        abb_b2x_aggregates.current_l1_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l1_any_t0_avg_a * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      current_l1_any_t0_max_a = greatest(
        abb_b2x_aggregates.current_l1_any_t0_max_a,
        EXCLUDED.current_l1_any_t0_max_a
      ),
      current_l1_any_t0_max_timestamp = case
        when EXCLUDED.current_l1_any_t0_max_a > abb_b2x_aggregates.current_l1_any_t0_max_a then EXCLUDED.current_l1_any_t0_max_timestamp
        else abb_b2x_aggregates.current_l1_any_t0_max_timestamp
      end,
      current_l1_any_t0_min_a = least(
        abb_b2x_aggregates.current_l1_any_t0_min_a,
        EXCLUDED.current_l1_any_t0_min_a
      ),
      current_l1_any_t0_min_timestamp = case
        when EXCLUDED.current_l1_any_t0_min_a < abb_b2x_aggregates.current_l1_any_t0_min_a then EXCLUDED.current_l1_any_t0_min_timestamp
        else abb_b2x_aggregates.current_l1_any_t0_min_timestamp
      end,
      current_l2_any_t0_avg_a = (
        abb_b2x_aggregates.current_l2_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l2_any_t0_avg_a * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      current_l2_any_t0_max_a = greatest(
        abb_b2x_aggregates.current_l2_any_t0_max_a,
        EXCLUDED.current_l2_any_t0_max_a
      ),
      current_l2_any_t0_max_timestamp = case
        when EXCLUDED.current_l2_any_t0_max_a > abb_b2x_aggregates.current_l2_any_t0_max_a then EXCLUDED.current_l2_any_t0_max_timestamp
        else abb_b2x_aggregates.current_l2_any_t0_max_timestamp
      end,
      current_l2_any_t0_min_a = least(
        abb_b2x_aggregates.current_l2_any_t0_min_a,
        EXCLUDED.current_l2_any_t0_min_a
      ),
      current_l2_any_t0_min_timestamp = case
        when EXCLUDED.current_l2_any_t0_min_a < abb_b2x_aggregates.current_l2_any_t0_min_a then EXCLUDED.current_l2_any_t0_min_timestamp
        else abb_b2x_aggregates.current_l2_any_t0_min_timestamp
      end,
      current_l3_any_t0_avg_a = (
        abb_b2x_aggregates.current_l3_any_t0_avg_a * abb_b2x_aggregates.count + EXCLUDED.current_l3_any_t0_avg_a * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      current_l3_any_t0_max_a = greatest(
        abb_b2x_aggregates.current_l3_any_t0_max_a,
        EXCLUDED.current_l3_any_t0_max_a
      ),
      current_l3_any_t0_max_timestamp = case
        when EXCLUDED.current_l3_any_t0_max_a > abb_b2x_aggregates.current_l3_any_t0_max_a then EXCLUDED.current_l3_any_t0_max_timestamp
        else abb_b2x_aggregates.current_l3_any_t0_max_timestamp
      end,
      current_l3_any_t0_min_a = least(
        abb_b2x_aggregates.current_l3_any_t0_min_a,
        EXCLUDED.current_l3_any_t0_min_a
      ),
      current_l3_any_t0_min_timestamp = case
        when EXCLUDED.current_l3_any_t0_min_a < abb_b2x_aggregates.current_l3_any_t0_min_a then EXCLUDED.current_l3_any_t0_min_timestamp
        else abb_b2x_aggregates.current_l3_any_t0_min_timestamp
      end,
      derived_active_power_l1_export_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l1_export_t0_max_wh,
          EXCLUDED.active_energy_l1_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l1_export_t0_min_wh,
          EXCLUDED.active_energy_l1_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_export_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l1_export_t0_max_wh,
          EXCLUDED.active_energy_l1_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l1_export_t0_min_wh,
          EXCLUDED.active_energy_l1_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_export_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l1_export_t0_max_wh,
          EXCLUDED.active_energy_l1_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l1_export_t0_min_wh,
          EXCLUDED.active_energy_l1_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_import_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l1_import_t0_max_wh,
          EXCLUDED.active_energy_l1_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l1_import_t0_min_wh,
          EXCLUDED.active_energy_l1_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_import_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l1_import_t0_max_wh,
          EXCLUDED.active_energy_l1_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l1_import_t0_min_wh,
          EXCLUDED.active_energy_l1_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_import_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l1_import_t0_max_wh,
          EXCLUDED.active_energy_l1_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l1_import_t0_min_wh,
          EXCLUDED.active_energy_l1_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_export_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l2_export_t0_max_wh,
          EXCLUDED.active_energy_l2_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l2_export_t0_min_wh,
          EXCLUDED.active_energy_l2_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_export_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l2_export_t0_max_wh,
          EXCLUDED.active_energy_l2_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l2_export_t0_min_wh,
          EXCLUDED.active_energy_l2_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_export_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l2_export_t0_max_wh,
          EXCLUDED.active_energy_l2_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l2_export_t0_min_wh,
          EXCLUDED.active_energy_l2_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_import_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l2_import_t0_max_wh,
          EXCLUDED.active_energy_l2_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l2_import_t0_min_wh,
          EXCLUDED.active_energy_l2_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_import_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l2_import_t0_max_wh,
          EXCLUDED.active_energy_l2_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l2_import_t0_min_wh,
          EXCLUDED.active_energy_l2_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_import_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l2_import_t0_max_wh,
          EXCLUDED.active_energy_l2_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l2_import_t0_min_wh,
          EXCLUDED.active_energy_l2_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_export_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l3_export_t0_max_wh,
          EXCLUDED.active_energy_l3_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l3_export_t0_min_wh,
          EXCLUDED.active_energy_l3_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_export_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l3_export_t0_max_wh,
          EXCLUDED.active_energy_l3_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l3_export_t0_min_wh,
          EXCLUDED.active_energy_l3_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_export_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l3_export_t0_max_wh,
          EXCLUDED.active_energy_l3_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l3_export_t0_min_wh,
          EXCLUDED.active_energy_l3_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_import_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l3_import_t0_max_wh,
          EXCLUDED.active_energy_l3_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l3_import_t0_min_wh,
          EXCLUDED.active_energy_l3_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_import_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l3_import_t0_max_wh,
          EXCLUDED.active_energy_l3_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l3_import_t0_min_wh,
          EXCLUDED.active_energy_l3_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_import_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_l3_import_t0_max_wh,
          EXCLUDED.active_energy_l3_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_l3_import_t0_min_wh,
          EXCLUDED.active_energy_l3_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_export_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_export_t0_max_wh,
          EXCLUDED.active_energy_total_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_export_t0_min_wh,
          EXCLUDED.active_energy_total_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_export_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_export_t0_max_wh,
          EXCLUDED.active_energy_total_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_export_t0_min_wh,
          EXCLUDED.active_energy_total_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_export_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_export_t0_max_wh,
          EXCLUDED.active_energy_total_export_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_export_t0_min_wh,
          EXCLUDED.active_energy_total_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t0_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t0_max_wh,
          EXCLUDED.active_energy_total_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t0_min_wh,
          EXCLUDED.active_energy_total_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t0_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t0_max_wh,
          EXCLUDED.active_energy_total_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t0_min_wh,
          EXCLUDED.active_energy_total_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t0_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t0_max_wh,
          EXCLUDED.active_energy_total_import_t0_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t0_min_wh,
          EXCLUDED.active_energy_total_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t1_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t1_max_wh,
          EXCLUDED.active_energy_total_import_t1_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t1_min_wh,
          EXCLUDED.active_energy_total_import_t1_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t1_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t1_max_wh,
          EXCLUDED.active_energy_total_import_t1_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t1_min_wh,
          EXCLUDED.active_energy_total_import_t1_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t1_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t1_max_wh,
          EXCLUDED.active_energy_total_import_t1_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t1_min_wh,
          EXCLUDED.active_energy_total_import_t1_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t2_avg_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t2_max_wh,
          EXCLUDED.active_energy_total_import_t2_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t2_min_wh,
          EXCLUDED.active_energy_total_import_t2_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t2_max_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t2_max_wh,
          EXCLUDED.active_energy_total_import_t2_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t2_min_wh,
          EXCLUDED.active_energy_total_import_t2_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t2_min_w = (
        greatest(
          abb_b2x_aggregates.active_energy_total_import_t2_max_wh,
          EXCLUDED.active_energy_total_import_t2_max_wh
        ) - least(
          abb_b2x_aggregates.active_energy_total_import_t2_min_wh,
          EXCLUDED.active_energy_total_import_t2_min_wh
        )
      ) * 4,
      derived_reactive_power_l1_export_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l1_export_t0_max_varh,
          EXCLUDED.reactive_energy_l1_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l1_export_t0_min_varh,
          EXCLUDED.reactive_energy_l1_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l1_export_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l1_export_t0_max_varh,
          EXCLUDED.reactive_energy_l1_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l1_export_t0_min_varh,
          EXCLUDED.reactive_energy_l1_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l1_export_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l1_export_t0_max_varh,
          EXCLUDED.reactive_energy_l1_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l1_export_t0_min_varh,
          EXCLUDED.reactive_energy_l1_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l1_import_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l1_import_t0_max_varh,
          EXCLUDED.reactive_energy_l1_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l1_import_t0_min_varh,
          EXCLUDED.reactive_energy_l1_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l1_import_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l1_import_t0_max_varh,
          EXCLUDED.reactive_energy_l1_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l1_import_t0_min_varh,
          EXCLUDED.reactive_energy_l1_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l1_import_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l1_import_t0_max_varh,
          EXCLUDED.reactive_energy_l1_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l1_import_t0_min_varh,
          EXCLUDED.reactive_energy_l1_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l2_export_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l2_export_t0_max_varh,
          EXCLUDED.reactive_energy_l2_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l2_export_t0_min_varh,
          EXCLUDED.reactive_energy_l2_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l2_export_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l2_export_t0_max_varh,
          EXCLUDED.reactive_energy_l2_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l2_export_t0_min_varh,
          EXCLUDED.reactive_energy_l2_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l2_export_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l2_export_t0_max_varh,
          EXCLUDED.reactive_energy_l2_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l2_export_t0_min_varh,
          EXCLUDED.reactive_energy_l2_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l2_import_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l2_import_t0_max_varh,
          EXCLUDED.reactive_energy_l2_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l2_import_t0_min_varh,
          EXCLUDED.reactive_energy_l2_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l2_import_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l2_import_t0_max_varh,
          EXCLUDED.reactive_energy_l2_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l2_import_t0_min_varh,
          EXCLUDED.reactive_energy_l2_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l2_import_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l2_import_t0_max_varh,
          EXCLUDED.reactive_energy_l2_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l2_import_t0_min_varh,
          EXCLUDED.reactive_energy_l2_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l3_export_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l3_export_t0_max_varh,
          EXCLUDED.reactive_energy_l3_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l3_export_t0_min_varh,
          EXCLUDED.reactive_energy_l3_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l3_export_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l3_export_t0_max_varh,
          EXCLUDED.reactive_energy_l3_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l3_export_t0_min_varh,
          EXCLUDED.reactive_energy_l3_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l3_export_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l3_export_t0_max_varh,
          EXCLUDED.reactive_energy_l3_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l3_export_t0_min_varh,
          EXCLUDED.reactive_energy_l3_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l3_import_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l3_import_t0_max_varh,
          EXCLUDED.reactive_energy_l3_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l3_import_t0_min_varh,
          EXCLUDED.reactive_energy_l3_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l3_import_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l3_import_t0_max_varh,
          EXCLUDED.reactive_energy_l3_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l3_import_t0_min_varh,
          EXCLUDED.reactive_energy_l3_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_l3_import_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_l3_import_t0_max_varh,
          EXCLUDED.reactive_energy_l3_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_l3_import_t0_min_varh,
          EXCLUDED.reactive_energy_l3_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_export_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_total_export_t0_max_varh,
          EXCLUDED.reactive_energy_total_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_total_export_t0_min_varh,
          EXCLUDED.reactive_energy_total_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_export_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_total_export_t0_max_varh,
          EXCLUDED.reactive_energy_total_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_total_export_t0_min_varh,
          EXCLUDED.reactive_energy_total_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_export_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_total_export_t0_max_varh,
          EXCLUDED.reactive_energy_total_export_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_total_export_t0_min_varh,
          EXCLUDED.reactive_energy_total_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_import_t0_avg_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_total_import_t0_max_varh,
          EXCLUDED.reactive_energy_total_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_total_import_t0_min_varh,
          EXCLUDED.reactive_energy_total_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_import_t0_max_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_total_import_t0_max_varh,
          EXCLUDED.reactive_energy_total_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_total_import_t0_min_varh,
          EXCLUDED.reactive_energy_total_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_import_t0_min_var = (
        greatest(
          abb_b2x_aggregates.reactive_energy_total_import_t0_max_varh,
          EXCLUDED.reactive_energy_total_import_t0_max_varh
        ) - least(
          abb_b2x_aggregates.reactive_energy_total_import_t0_min_varh,
          EXCLUDED.reactive_energy_total_import_t0_min_varh
        )
      ) * 4,
      reactive_energy_l1_export_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_l1_export_t0_max_varh,
        EXCLUDED.reactive_energy_l1_export_t0_max_varh
      ),
      reactive_energy_l1_export_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_l1_export_t0_min_varh,
        EXCLUDED.reactive_energy_l1_export_t0_min_varh
      ),
      reactive_energy_l1_import_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_l1_import_t0_max_varh,
        EXCLUDED.reactive_energy_l1_import_t0_max_varh
      ),
      reactive_energy_l1_import_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_l1_import_t0_min_varh,
        EXCLUDED.reactive_energy_l1_import_t0_min_varh
      ),
      reactive_energy_l2_export_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_l2_export_t0_max_varh,
        EXCLUDED.reactive_energy_l2_export_t0_max_varh
      ),
      reactive_energy_l2_export_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_l2_export_t0_min_varh,
        EXCLUDED.reactive_energy_l2_export_t0_min_varh
      ),
      reactive_energy_l2_import_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_l2_import_t0_max_varh,
        EXCLUDED.reactive_energy_l2_import_t0_max_varh
      ),
      reactive_energy_l2_import_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_l2_import_t0_min_varh,
        EXCLUDED.reactive_energy_l2_import_t0_min_varh
      ),
      reactive_energy_l3_export_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_l3_export_t0_max_varh,
        EXCLUDED.reactive_energy_l3_export_t0_max_varh
      ),
      reactive_energy_l3_export_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_l3_export_t0_min_varh,
        EXCLUDED.reactive_energy_l3_export_t0_min_varh
      ),
      reactive_energy_l3_import_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_l3_import_t0_max_varh,
        EXCLUDED.reactive_energy_l3_import_t0_max_varh
      ),
      reactive_energy_l3_import_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_l3_import_t0_min_varh,
        EXCLUDED.reactive_energy_l3_import_t0_min_varh
      ),
      reactive_energy_total_export_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_total_export_t0_max_varh,
        EXCLUDED.reactive_energy_total_export_t0_max_varh
      ),
      reactive_energy_total_export_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_total_export_t0_min_varh,
        EXCLUDED.reactive_energy_total_export_t0_min_varh
      ),
      reactive_energy_total_import_t0_max_varh = greatest(
        abb_b2x_aggregates.reactive_energy_total_import_t0_max_varh,
        EXCLUDED.reactive_energy_total_import_t0_max_varh
      ),
      reactive_energy_total_import_t0_min_varh = least(
        abb_b2x_aggregates.reactive_energy_total_import_t0_min_varh,
        EXCLUDED.reactive_energy_total_import_t0_min_varh
      ),
      reactive_power_l1_net_t0_avg_var = (
        abb_b2x_aggregates.reactive_power_l1_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l1_net_t0_avg_var * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      reactive_power_l1_net_t0_max_var = greatest(
        abb_b2x_aggregates.reactive_power_l1_net_t0_max_var,
        EXCLUDED.reactive_power_l1_net_t0_max_var
      ),
      reactive_power_l1_net_t0_max_timestamp = case
        when EXCLUDED.reactive_power_l1_net_t0_max_var > abb_b2x_aggregates.reactive_power_l1_net_t0_max_var then EXCLUDED.reactive_power_l1_net_t0_max_timestamp
        else abb_b2x_aggregates.reactive_power_l1_net_t0_max_timestamp
      end,
      reactive_power_l1_net_t0_min_var = least(
        abb_b2x_aggregates.reactive_power_l1_net_t0_min_var,
        EXCLUDED.reactive_power_l1_net_t0_min_var
      ),
      reactive_power_l1_net_t0_min_timestamp = case
        when EXCLUDED.reactive_power_l1_net_t0_min_var < abb_b2x_aggregates.reactive_power_l1_net_t0_min_var then EXCLUDED.reactive_power_l1_net_t0_min_timestamp
        else abb_b2x_aggregates.reactive_power_l1_net_t0_min_timestamp
      end,
      reactive_power_l2_net_t0_avg_var = (
        abb_b2x_aggregates.reactive_power_l2_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l2_net_t0_avg_var * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      reactive_power_l2_net_t0_max_var = greatest(
        abb_b2x_aggregates.reactive_power_l2_net_t0_max_var,
        EXCLUDED.reactive_power_l2_net_t0_max_var
      ),
      reactive_power_l2_net_t0_max_timestamp = case
        when EXCLUDED.reactive_power_l2_net_t0_max_var > abb_b2x_aggregates.reactive_power_l2_net_t0_max_var then EXCLUDED.reactive_power_l2_net_t0_max_timestamp
        else abb_b2x_aggregates.reactive_power_l2_net_t0_max_timestamp
      end,
      reactive_power_l2_net_t0_min_var = least(
        abb_b2x_aggregates.reactive_power_l2_net_t0_min_var,
        EXCLUDED.reactive_power_l2_net_t0_min_var
      ),
      reactive_power_l2_net_t0_min_timestamp = case
        when EXCLUDED.reactive_power_l2_net_t0_min_var < abb_b2x_aggregates.reactive_power_l2_net_t0_min_var then EXCLUDED.reactive_power_l2_net_t0_min_timestamp
        else abb_b2x_aggregates.reactive_power_l2_net_t0_min_timestamp
      end,
      reactive_power_l3_net_t0_avg_var = (
        abb_b2x_aggregates.reactive_power_l3_net_t0_avg_var * abb_b2x_aggregates.count + EXCLUDED.reactive_power_l3_net_t0_avg_var * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      reactive_power_l3_net_t0_max_var = greatest(
        abb_b2x_aggregates.reactive_power_l3_net_t0_max_var,
        EXCLUDED.reactive_power_l3_net_t0_max_var
      ),
      reactive_power_l3_net_t0_max_timestamp = case
        when EXCLUDED.reactive_power_l3_net_t0_max_var > abb_b2x_aggregates.reactive_power_l3_net_t0_max_var then EXCLUDED.reactive_power_l3_net_t0_max_timestamp
        else abb_b2x_aggregates.reactive_power_l3_net_t0_max_timestamp
      end,
      reactive_power_l3_net_t0_min_var = least(
        abb_b2x_aggregates.reactive_power_l3_net_t0_min_var,
        EXCLUDED.reactive_power_l3_net_t0_min_var
      ),
      reactive_power_l3_net_t0_min_timestamp = case
        when EXCLUDED.reactive_power_l3_net_t0_min_var < abb_b2x_aggregates.reactive_power_l3_net_t0_min_var then EXCLUDED.reactive_power_l3_net_t0_min_timestamp
        else abb_b2x_aggregates.reactive_power_l3_net_t0_min_timestamp
      end,
      voltage_l1_any_t0_avg_v = (
        abb_b2x_aggregates.voltage_l1_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l1_any_t0_avg_v * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      voltage_l1_any_t0_max_v = greatest(
        abb_b2x_aggregates.voltage_l1_any_t0_max_v,
        EXCLUDED.voltage_l1_any_t0_max_v
      ),
      voltage_l1_any_t0_max_timestamp = case
        when EXCLUDED.voltage_l1_any_t0_max_v > abb_b2x_aggregates.voltage_l1_any_t0_max_v then EXCLUDED.voltage_l1_any_t0_max_timestamp
        else abb_b2x_aggregates.voltage_l1_any_t0_max_timestamp
      end,
      voltage_l1_any_t0_min_v = least(
        abb_b2x_aggregates.voltage_l1_any_t0_min_v,
        EXCLUDED.voltage_l1_any_t0_min_v
      ),
      voltage_l1_any_t0_min_timestamp = case
        when EXCLUDED.voltage_l1_any_t0_min_v < abb_b2x_aggregates.voltage_l1_any_t0_min_v then EXCLUDED.voltage_l1_any_t0_min_timestamp
        else abb_b2x_aggregates.voltage_l1_any_t0_min_timestamp
      end,
      voltage_l2_any_t0_avg_v = (
        abb_b2x_aggregates.voltage_l2_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l2_any_t0_avg_v * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      voltage_l2_any_t0_max_v = greatest(
        abb_b2x_aggregates.voltage_l2_any_t0_max_v,
        EXCLUDED.voltage_l2_any_t0_max_v
      ),
      voltage_l2_any_t0_max_timestamp = case
        when EXCLUDED.voltage_l2_any_t0_max_v > abb_b2x_aggregates.voltage_l2_any_t0_max_v then EXCLUDED.voltage_l2_any_t0_max_timestamp
        else abb_b2x_aggregates.voltage_l2_any_t0_max_timestamp
      end,
      voltage_l2_any_t0_min_v = least(
        abb_b2x_aggregates.voltage_l2_any_t0_min_v,
        EXCLUDED.voltage_l2_any_t0_min_v
      ),
      voltage_l2_any_t0_min_timestamp = case
        when EXCLUDED.voltage_l2_any_t0_min_v < abb_b2x_aggregates.voltage_l2_any_t0_min_v then EXCLUDED.voltage_l2_any_t0_min_timestamp
        else abb_b2x_aggregates.voltage_l2_any_t0_min_timestamp
      end,
      voltage_l3_any_t0_avg_v = (
        abb_b2x_aggregates.voltage_l3_any_t0_avg_v * abb_b2x_aggregates.count + EXCLUDED.voltage_l3_any_t0_avg_v * EXCLUDED.count
      ) / (abb_b2x_aggregates.count + EXCLUDED.count),
      voltage_l3_any_t0_max_v = greatest(
        abb_b2x_aggregates.voltage_l3_any_t0_max_v,
        EXCLUDED.voltage_l3_any_t0_max_v
      ),
      voltage_l3_any_t0_max_timestamp = case
        when EXCLUDED.voltage_l3_any_t0_max_v > abb_b2x_aggregates.voltage_l3_any_t0_max_v then EXCLUDED.voltage_l3_any_t0_max_timestamp
        else abb_b2x_aggregates.voltage_l3_any_t0_max_timestamp
      end,
      voltage_l3_any_t0_min_v = least(
        abb_b2x_aggregates.voltage_l3_any_t0_min_v,
        EXCLUDED.voltage_l3_any_t0_min_v
      ),
      voltage_l3_any_t0_min_timestamp = case
        when EXCLUDED.voltage_l3_any_t0_min_v < abb_b2x_aggregates.voltage_l3_any_t0_min_v then EXCLUDED.voltage_l3_any_t0_min_timestamp
        else abb_b2x_aggregates.voltage_l3_any_t0_min_timestamp
      end
    returning
      abb_b2x_aggregates.*
  ),
  delta as (
    select
      date_trunc('day', new.timestamp AT time zone 'Europe/Zagreb') AT time zone 'Europe/Zagreb' daily_timestamp,
      date_trunc(
        'month',
        new.timestamp AT time zone 'Europe/Zagreb'
      ) AT time zone 'Europe/Zagreb' monthly_timestamp,
      new.timestamp,
      new.interval,
      new.meter_id,
      new.measurement_location_id,
      case
        when old.timestamp is null then 1
        else 0
      end new_count,
      new.derived_active_power_l1_export_t0_avg_w - coalesce(old.derived_active_power_l1_export_t0_avg_w, 0) derived_active_power_l1_export_t0_avg_w,
      greatest(
        new.derived_active_power_l1_export_t0_max_w,
        coalesce(
          old.derived_active_power_l1_export_t0_max_w,
          new.derived_active_power_l1_export_t0_max_w
        )
      ) derived_active_power_l1_export_t0_max_w,
      case
        when new.derived_active_power_l1_export_t0_max_w > coalesce(
          old.derived_active_power_l1_export_t0_max_w,
          new.derived_active_power_l1_export_t0_max_w
        ) then new.derived_active_power_l1_export_t0_max_timestamp
        else coalesce(
          old.derived_active_power_l1_export_t0_max_timestamp,
          new.derived_active_power_l1_export_t0_max_timestamp
        )
      end derived_active_power_l1_export_t0_max_timestamp,
      least(
        new.derived_active_power_l1_export_t0_min_w,
        coalesce(
          old.derived_active_power_l1_export_t0_min_w,
          new.derived_active_power_l1_export_t0_min_w
        )
      ) derived_active_power_l1_export_t0_min_w,
      case
        when new.derived_active_power_l1_export_t0_min_w < coalesce(
          old.derived_active_power_l1_export_t0_min_w,
          new.derived_active_power_l1_export_t0_min_w
        ) then new.derived_active_power_l1_export_t0_min_timestamp
        else coalesce(
          old.derived_active_power_l1_export_t0_min_timestamp,
          new.derived_active_power_l1_export_t0_min_timestamp
        )
      end derived_active_power_l1_export_t0_min_timestamp,
      new.derived_active_power_l1_import_t0_avg_w - coalesce(old.derived_active_power_l1_import_t0_avg_w, 0) derived_active_power_l1_import_t0_avg_w,
      greatest(
        new.derived_active_power_l1_import_t0_max_w,
        coalesce(
          old.derived_active_power_l1_import_t0_max_w,
          new.derived_active_power_l1_import_t0_max_w
        )
      ) derived_active_power_l1_import_t0_max_w,
      case
        when new.derived_active_power_l1_import_t0_max_w > coalesce(
          old.derived_active_power_l1_import_t0_max_w,
          new.derived_active_power_l1_import_t0_max_w
        ) then new.derived_active_power_l1_import_t0_max_timestamp
        else coalesce(
          old.derived_active_power_l1_import_t0_max_timestamp,
          new.derived_active_power_l1_import_t0_max_timestamp
        )
      end derived_active_power_l1_import_t0_max_timestamp,
      least(
        new.derived_active_power_l1_import_t0_min_w,
        coalesce(
          old.derived_active_power_l1_import_t0_min_w,
          new.derived_active_power_l1_import_t0_min_w
        )
      ) derived_active_power_l1_import_t0_min_w,
      case
        when new.derived_active_power_l1_import_t0_min_w < coalesce(
          old.derived_active_power_l1_import_t0_min_w,
          new.derived_active_power_l1_import_t0_min_w
        ) then new.derived_active_power_l1_import_t0_min_timestamp
        else coalesce(
          old.derived_active_power_l1_import_t0_min_timestamp,
          new.derived_active_power_l1_import_t0_min_timestamp
        )
      end derived_active_power_l1_import_t0_min_timestamp,
      new.derived_active_power_l2_export_t0_avg_w - coalesce(old.derived_active_power_l2_export_t0_avg_w, 0) derived_active_power_l2_export_t0_avg_w,
      greatest(
        new.derived_active_power_l2_export_t0_max_w,
        coalesce(
          old.derived_active_power_l2_export_t0_max_w,
          new.derived_active_power_l2_export_t0_max_w
        )
      ) derived_active_power_l2_export_t0_max_w,
      case
        when new.derived_active_power_l2_export_t0_max_w > coalesce(
          old.derived_active_power_l2_export_t0_max_w,
          new.derived_active_power_l2_export_t0_max_w
        ) then new.derived_active_power_l2_export_t0_max_timestamp
        else coalesce(
          old.derived_active_power_l2_export_t0_max_timestamp,
          new.derived_active_power_l2_export_t0_max_timestamp
        )
      end derived_active_power_l2_export_t0_max_timestamp,
      least(
        new.derived_active_power_l2_export_t0_min_w,
        coalesce(
          old.derived_active_power_l2_export_t0_min_w,
          new.derived_active_power_l2_export_t0_min_w
        )
      ) derived_active_power_l2_export_t0_min_w,
      case
        when new.derived_active_power_l2_export_t0_min_w < coalesce(
          old.derived_active_power_l2_export_t0_min_w,
          new.derived_active_power_l2_export_t0_min_w
        ) then new.derived_active_power_l2_export_t0_min_timestamp
        else coalesce(
          old.derived_active_power_l2_export_t0_min_timestamp,
          new.derived_active_power_l2_export_t0_min_timestamp
        )
      end derived_active_power_l2_export_t0_min_timestamp,
      new.derived_active_power_l2_import_t0_avg_w - coalesce(old.derived_active_power_l2_import_t0_avg_w, 0) derived_active_power_l2_import_t0_avg_w,
      greatest(
        new.derived_active_power_l2_import_t0_max_w,
        coalesce(
          old.derived_active_power_l2_import_t0_max_w,
          new.derived_active_power_l2_import_t0_max_w
        )
      ) derived_active_power_l2_import_t0_max_w,
      case
        when new.derived_active_power_l2_import_t0_max_w > coalesce(
          old.derived_active_power_l2_import_t0_max_w,
          new.derived_active_power_l2_import_t0_max_w
        ) then new.derived_active_power_l2_import_t0_max_timestamp
        else coalesce(
          old.derived_active_power_l2_import_t0_max_timestamp,
          new.derived_active_power_l2_import_t0_max_timestamp
        )
      end derived_active_power_l2_import_t0_max_timestamp,
      least(
        new.derived_active_power_l2_import_t0_min_w,
        coalesce(
          old.derived_active_power_l2_import_t0_min_w,
          new.derived_active_power_l2_import_t0_min_w
        )
      ) derived_active_power_l2_import_t0_min_w,
      case
        when new.derived_active_power_l2_import_t0_min_w < coalesce(
          old.derived_active_power_l2_import_t0_min_w,
          new.derived_active_power_l2_import_t0_min_w
        ) then new.derived_active_power_l2_import_t0_min_timestamp
        else coalesce(
          old.derived_active_power_l2_import_t0_min_timestamp,
          new.derived_active_power_l2_import_t0_min_timestamp
        )
      end derived_active_power_l2_import_t0_min_timestamp,
      new.derived_active_power_l3_export_t0_avg_w - coalesce(old.derived_active_power_l3_export_t0_avg_w, 0) derived_active_power_l3_export_t0_avg_w,
      greatest(
        new.derived_active_power_l3_export_t0_max_w,
        coalesce(
          old.derived_active_power_l3_export_t0_max_w,
          new.derived_active_power_l3_export_t0_max_w
        )
      ) derived_active_power_l3_export_t0_max_w,
      case
        when new.derived_active_power_l3_export_t0_max_w > coalesce(
          old.derived_active_power_l3_export_t0_max_w,
          new.derived_active_power_l3_export_t0_max_w
        ) then new.derived_active_power_l3_export_t0_max_timestamp
        else coalesce(
          old.derived_active_power_l3_export_t0_max_timestamp,
          new.derived_active_power_l3_export_t0_max_timestamp
        )
      end derived_active_power_l3_export_t0_max_timestamp,
      least(
        new.derived_active_power_l3_export_t0_min_w,
        coalesce(
          old.derived_active_power_l3_export_t0_min_w,
          new.derived_active_power_l3_export_t0_min_w
        )
      ) derived_active_power_l3_export_t0_min_w,
      case
        when new.derived_active_power_l3_export_t0_min_w < coalesce(
          old.derived_active_power_l3_export_t0_min_w,
          new.derived_active_power_l3_export_t0_min_w
        ) then new.derived_active_power_l3_export_t0_min_timestamp
        else coalesce(
          old.derived_active_power_l3_export_t0_min_timestamp,
          new.derived_active_power_l3_export_t0_min_timestamp
        )
      end derived_active_power_l3_export_t0_min_timestamp,
      new.derived_active_power_l3_import_t0_avg_w - coalesce(old.derived_active_power_l3_import_t0_avg_w, 0) derived_active_power_l3_import_t0_avg_w,
      greatest(
        new.derived_active_power_l3_import_t0_max_w,
        coalesce(
          old.derived_active_power_l3_import_t0_max_w,
          new.derived_active_power_l3_import_t0_max_w
        )
      ) derived_active_power_l3_import_t0_max_w,
      case
        when new.derived_active_power_l3_import_t0_max_w > coalesce(
          old.derived_active_power_l3_import_t0_max_w,
          new.derived_active_power_l3_import_t0_max_w
        ) then new.derived_active_power_l3_import_t0_max_timestamp
        else coalesce(
          old.derived_active_power_l3_import_t0_max_timestamp,
          new.derived_active_power_l3_import_t0_max_timestamp
        )
      end derived_active_power_l3_import_t0_max_timestamp,
      least(
        new.derived_active_power_l3_import_t0_min_w,
        coalesce(
          old.derived_active_power_l3_import_t0_min_w,
          new.derived_active_power_l3_import_t0_min_w
        )
      ) derived_active_power_l3_import_t0_min_w,
      case
        when new.derived_active_power_l3_import_t0_min_w < coalesce(
          old.derived_active_power_l3_import_t0_min_w,
          new.derived_active_power_l3_import_t0_min_w
        ) then new.derived_active_power_l3_import_t0_min_timestamp
        else coalesce(
          old.derived_active_power_l3_import_t0_min_timestamp,
          new.derived_active_power_l3_import_t0_min_timestamp
        )
      end derived_active_power_l3_import_t0_min_timestamp,
      new.derived_active_power_total_export_t0_avg_w - coalesce(old.derived_active_power_total_export_t0_avg_w, 0) derived_active_power_total_export_t0_avg_w,
      greatest(
        new.derived_active_power_total_export_t0_max_w,
        coalesce(
          old.derived_active_power_total_export_t0_max_w,
          new.derived_active_power_total_export_t0_max_w
        )
      ) derived_active_power_total_export_t0_max_w,
      case
        when new.derived_active_power_total_export_t0_max_w > coalesce(
          old.derived_active_power_total_export_t0_max_w,
          new.derived_active_power_total_export_t0_max_w
        ) then new.derived_active_power_total_export_t0_max_timestamp
        else coalesce(
          old.derived_active_power_total_export_t0_max_timestamp,
          new.derived_active_power_total_export_t0_max_timestamp
        )
      end derived_active_power_total_export_t0_max_timestamp,
      least(
        new.derived_active_power_total_export_t0_min_w,
        coalesce(
          old.derived_active_power_total_export_t0_min_w,
          new.derived_active_power_total_export_t0_min_w
        )
      ) derived_active_power_total_export_t0_min_w,
      case
        when new.derived_active_power_total_export_t0_min_w < coalesce(
          old.derived_active_power_total_export_t0_min_w,
          new.derived_active_power_total_export_t0_min_w
        ) then new.derived_active_power_total_export_t0_min_timestamp
        else coalesce(
          old.derived_active_power_total_export_t0_min_timestamp,
          new.derived_active_power_total_export_t0_min_timestamp
        )
      end derived_active_power_total_export_t0_min_timestamp,
      new.derived_active_power_total_import_t0_avg_w - coalesce(old.derived_active_power_total_import_t0_avg_w, 0) derived_active_power_total_import_t0_avg_w,
      greatest(
        new.derived_active_power_total_import_t0_max_w,
        coalesce(
          old.derived_active_power_total_import_t0_max_w,
          new.derived_active_power_total_import_t0_max_w
        )
      ) derived_active_power_total_import_t0_max_w,
      case
        when new.derived_active_power_total_import_t0_max_w > coalesce(
          old.derived_active_power_total_import_t0_max_w,
          new.derived_active_power_total_import_t0_max_w
        ) then new.derived_active_power_total_import_t0_max_timestamp
        else coalesce(
          old.derived_active_power_total_import_t0_max_timestamp,
          new.derived_active_power_total_import_t0_max_timestamp
        )
      end derived_active_power_total_import_t0_max_timestamp,
      least(
        new.derived_active_power_total_import_t0_min_w,
        coalesce(
          old.derived_active_power_total_import_t0_min_w,
          new.derived_active_power_total_import_t0_min_w
        )
      ) derived_active_power_total_import_t0_min_w,
      case
        when new.derived_active_power_total_import_t0_min_w < coalesce(
          old.derived_active_power_total_import_t0_min_w,
          new.derived_active_power_total_import_t0_min_w
        ) then new.derived_active_power_total_import_t0_min_timestamp
        else coalesce(
          old.derived_active_power_total_import_t0_min_timestamp,
          new.derived_active_power_total_import_t0_min_timestamp
        )
      end derived_active_power_total_import_t0_min_timestamp,
      new.derived_active_power_total_import_t1_avg_w - coalesce(old.derived_active_power_total_import_t1_avg_w, 0) derived_active_power_total_import_t1_avg_w,
      greatest(
        new.derived_active_power_total_import_t1_max_w,
        coalesce(
          old.derived_active_power_total_import_t1_max_w,
          new.derived_active_power_total_import_t1_max_w
        )
      ) derived_active_power_total_import_t1_max_w,
      case
        when new.derived_active_power_total_import_t1_max_w > coalesce(
          old.derived_active_power_total_import_t1_max_w,
          new.derived_active_power_total_import_t1_max_w
        ) then new.derived_active_power_total_import_t1_max_timestamp
        else coalesce(
          old.derived_active_power_total_import_t1_max_timestamp,
          new.derived_active_power_total_import_t1_max_timestamp
        )
      end derived_active_power_total_import_t1_max_timestamp,
      least(
        new.derived_active_power_total_import_t1_min_w,
        coalesce(
          old.derived_active_power_total_import_t1_min_w,
          new.derived_active_power_total_import_t1_min_w
        )
      ) derived_active_power_total_import_t1_min_w,
      case
        when new.derived_active_power_total_import_t1_min_w < coalesce(
          old.derived_active_power_total_import_t1_min_w,
          new.derived_active_power_total_import_t1_min_w
        ) then new.derived_active_power_total_import_t1_min_timestamp
        else coalesce(
          old.derived_active_power_total_import_t1_min_timestamp,
          new.derived_active_power_total_import_t1_min_timestamp
        )
      end derived_active_power_total_import_t1_min_timestamp,
      new.derived_active_power_total_import_t2_avg_w - coalesce(old.derived_active_power_total_import_t2_avg_w, 0) derived_active_power_total_import_t2_avg_w,
      greatest(
        new.derived_active_power_total_import_t2_max_w,
        coalesce(
          old.derived_active_power_total_import_t2_max_w,
          new.derived_active_power_total_import_t2_max_w
        )
      ) derived_active_power_total_import_t2_max_w,
      case
        when new.derived_active_power_total_import_t2_max_w > coalesce(
          old.derived_active_power_total_import_t2_max_w,
          new.derived_active_power_total_import_t2_max_w
        ) then new.derived_active_power_total_import_t2_max_timestamp
        else coalesce(
          old.derived_active_power_total_import_t2_max_timestamp,
          new.derived_active_power_total_import_t2_max_timestamp
        )
      end derived_active_power_total_import_t2_max_timestamp,
      least(
        new.derived_active_power_total_import_t2_min_w,
        coalesce(
          old.derived_active_power_total_import_t2_min_w,
          new.derived_active_power_total_import_t2_min_w
        )
      ) derived_active_power_total_import_t2_min_w,
      case
        when new.derived_active_power_total_import_t2_min_w < coalesce(
          old.derived_active_power_total_import_t2_min_w,
          new.derived_active_power_total_import_t2_min_w
        ) then new.derived_active_power_total_import_t2_min_timestamp
        else coalesce(
          old.derived_active_power_total_import_t2_min_timestamp,
          new.derived_active_power_total_import_t2_min_timestamp
        )
      end derived_active_power_total_import_t2_min_timestamp,
      new.derived_reactive_power_l1_export_t0_avg_var - coalesce(
        old.derived_reactive_power_l1_export_t0_avg_var,
        0
      ) derived_reactive_power_l1_export_t0_avg_var,
      greatest(
        new.derived_reactive_power_l1_export_t0_max_var,
        coalesce(
          old.derived_reactive_power_l1_export_t0_max_var,
          new.derived_reactive_power_l1_export_t0_max_var
        )
      ) derived_reactive_power_l1_export_t0_max_var,
      case
        when new.derived_reactive_power_l1_export_t0_max_var > coalesce(
          old.derived_reactive_power_l1_export_t0_max_var,
          new.derived_reactive_power_l1_export_t0_max_var
        ) then new.derived_reactive_power_l1_export_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_l1_export_t0_max_timestamp,
          new.derived_reactive_power_l1_export_t0_max_timestamp
        )
      end derived_reactive_power_l1_export_t0_max_timestamp,
      least(
        new.derived_reactive_power_l1_export_t0_min_var,
        coalesce(
          old.derived_reactive_power_l1_export_t0_min_var,
          new.derived_reactive_power_l1_export_t0_min_var
        )
      ) derived_reactive_power_l1_export_t0_min_var,
      case
        when new.derived_reactive_power_l1_export_t0_min_var < coalesce(
          old.derived_reactive_power_l1_export_t0_min_var,
          new.derived_reactive_power_l1_export_t0_min_var
        ) then new.derived_reactive_power_l1_export_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_l1_export_t0_min_timestamp,
          new.derived_reactive_power_l1_export_t0_min_timestamp
        )
      end derived_reactive_power_l1_export_t0_min_timestamp,
      new.derived_reactive_power_l1_import_t0_avg_var - coalesce(
        old.derived_reactive_power_l1_import_t0_avg_var,
        0
      ) derived_reactive_power_l1_import_t0_avg_var,
      greatest(
        new.derived_reactive_power_l1_import_t0_max_var,
        coalesce(
          old.derived_reactive_power_l1_import_t0_max_var,
          new.derived_reactive_power_l1_import_t0_max_var
        )
      ) derived_reactive_power_l1_import_t0_max_var,
      case
        when new.derived_reactive_power_l1_import_t0_max_var > coalesce(
          old.derived_reactive_power_l1_import_t0_max_var,
          new.derived_reactive_power_l1_import_t0_max_var
        ) then new.derived_reactive_power_l1_import_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_l1_import_t0_max_timestamp,
          new.derived_reactive_power_l1_import_t0_max_timestamp
        )
      end derived_reactive_power_l1_import_t0_max_timestamp,
      least(
        new.derived_reactive_power_l1_import_t0_min_var,
        coalesce(
          old.derived_reactive_power_l1_import_t0_min_var,
          new.derived_reactive_power_l1_import_t0_min_var
        )
      ) derived_reactive_power_l1_import_t0_min_var,
      case
        when new.derived_reactive_power_l1_import_t0_min_var < coalesce(
          old.derived_reactive_power_l1_import_t0_min_var,
          new.derived_reactive_power_l1_import_t0_min_var
        ) then new.derived_reactive_power_l1_import_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_l1_import_t0_min_timestamp,
          new.derived_reactive_power_l1_import_t0_min_timestamp
        )
      end derived_reactive_power_l1_import_t0_min_timestamp,
      new.derived_reactive_power_l2_export_t0_avg_var - coalesce(
        old.derived_reactive_power_l2_export_t0_avg_var,
        0
      ) derived_reactive_power_l2_export_t0_avg_var,
      greatest(
        new.derived_reactive_power_l2_export_t0_max_var,
        coalesce(
          old.derived_reactive_power_l2_export_t0_max_var,
          new.derived_reactive_power_l2_export_t0_max_var
        )
      ) derived_reactive_power_l2_export_t0_max_var,
      case
        when new.derived_reactive_power_l2_export_t0_max_var > coalesce(
          old.derived_reactive_power_l2_export_t0_max_var,
          new.derived_reactive_power_l2_export_t0_max_var
        ) then new.derived_reactive_power_l2_export_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_l2_export_t0_max_timestamp,
          new.derived_reactive_power_l2_export_t0_max_timestamp
        )
      end derived_reactive_power_l2_export_t0_max_timestamp,
      least(
        new.derived_reactive_power_l2_export_t0_min_var,
        coalesce(
          old.derived_reactive_power_l2_export_t0_min_var,
          new.derived_reactive_power_l2_export_t0_min_var
        )
      ) derived_reactive_power_l2_export_t0_min_var,
      case
        when new.derived_reactive_power_l2_export_t0_min_var < coalesce(
          old.derived_reactive_power_l2_export_t0_min_var,
          new.derived_reactive_power_l2_export_t0_min_var
        ) then new.derived_reactive_power_l2_export_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_l2_export_t0_min_timestamp,
          new.derived_reactive_power_l2_export_t0_min_timestamp
        )
      end derived_reactive_power_l2_export_t0_min_timestamp,
      new.derived_reactive_power_l2_import_t0_avg_var - coalesce(
        old.derived_reactive_power_l2_import_t0_avg_var,
        0
      ) derived_reactive_power_l2_import_t0_avg_var,
      greatest(
        new.derived_reactive_power_l2_import_t0_max_var,
        coalesce(
          old.derived_reactive_power_l2_import_t0_max_var,
          new.derived_reactive_power_l2_import_t0_max_var
        )
      ) derived_reactive_power_l2_import_t0_max_var,
      case
        when new.derived_reactive_power_l2_import_t0_max_var > coalesce(
          old.derived_reactive_power_l2_import_t0_max_var,
          new.derived_reactive_power_l2_import_t0_max_var
        ) then new.derived_reactive_power_l2_import_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_l2_import_t0_max_timestamp,
          new.derived_reactive_power_l2_import_t0_max_timestamp
        )
      end derived_reactive_power_l2_import_t0_max_timestamp,
      least(
        new.derived_reactive_power_l2_import_t0_min_var,
        coalesce(
          old.derived_reactive_power_l2_import_t0_min_var,
          new.derived_reactive_power_l2_import_t0_min_var
        )
      ) derived_reactive_power_l2_import_t0_min_var,
      case
        when new.derived_reactive_power_l2_import_t0_min_var < coalesce(
          old.derived_reactive_power_l2_import_t0_min_var,
          new.derived_reactive_power_l2_import_t0_min_var
        ) then new.derived_reactive_power_l2_import_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_l2_import_t0_min_timestamp,
          new.derived_reactive_power_l2_import_t0_min_timestamp
        )
      end derived_reactive_power_l2_import_t0_min_timestamp,
      new.derived_reactive_power_l3_export_t0_avg_var - coalesce(
        old.derived_reactive_power_l3_export_t0_avg_var,
        0
      ) derived_reactive_power_l3_export_t0_avg_var,
      greatest(
        new.derived_reactive_power_l3_export_t0_max_var,
        coalesce(
          old.derived_reactive_power_l3_export_t0_max_var,
          new.derived_reactive_power_l3_export_t0_max_var
        )
      ) derived_reactive_power_l3_export_t0_max_var,
      case
        when new.derived_reactive_power_l3_export_t0_max_var > coalesce(
          old.derived_reactive_power_l3_export_t0_max_var,
          new.derived_reactive_power_l3_export_t0_max_var
        ) then new.derived_reactive_power_l3_export_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_l3_export_t0_max_timestamp,
          new.derived_reactive_power_l3_export_t0_max_timestamp
        )
      end derived_reactive_power_l3_export_t0_max_timestamp,
      least(
        new.derived_reactive_power_l3_export_t0_min_var,
        coalesce(
          old.derived_reactive_power_l3_export_t0_min_var,
          new.derived_reactive_power_l3_export_t0_min_var
        )
      ) derived_reactive_power_l3_export_t0_min_var,
      case
        when new.derived_reactive_power_l3_export_t0_min_var < coalesce(
          old.derived_reactive_power_l3_export_t0_min_var,
          new.derived_reactive_power_l3_export_t0_min_var
        ) then new.derived_reactive_power_l3_export_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_l3_export_t0_min_timestamp,
          new.derived_reactive_power_l3_export_t0_min_timestamp
        )
      end derived_reactive_power_l3_export_t0_min_timestamp,
      new.derived_reactive_power_l3_import_t0_avg_var - coalesce(
        old.derived_reactive_power_l3_import_t0_avg_var,
        0
      ) derived_reactive_power_l3_import_t0_avg_var,
      greatest(
        new.derived_reactive_power_l3_import_t0_max_var,
        coalesce(
          old.derived_reactive_power_l3_import_t0_max_var,
          new.derived_reactive_power_l3_import_t0_max_var
        )
      ) derived_reactive_power_l3_import_t0_max_var,
      case
        when new.derived_reactive_power_l3_import_t0_max_var > coalesce(
          old.derived_reactive_power_l3_import_t0_max_var,
          new.derived_reactive_power_l3_import_t0_max_var
        ) then new.derived_reactive_power_l3_import_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_l3_import_t0_max_timestamp,
          new.derived_reactive_power_l3_import_t0_max_timestamp
        )
      end derived_reactive_power_l3_import_t0_max_timestamp,
      least(
        new.derived_reactive_power_l3_import_t0_min_var,
        coalesce(
          old.derived_reactive_power_l3_import_t0_min_var,
          new.derived_reactive_power_l3_import_t0_min_var
        )
      ) derived_reactive_power_l3_import_t0_min_var,
      case
        when new.derived_reactive_power_l3_import_t0_min_var < coalesce(
          old.derived_reactive_power_l3_import_t0_min_var,
          new.derived_reactive_power_l3_import_t0_min_var
        ) then new.derived_reactive_power_l3_import_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_l3_import_t0_min_timestamp,
          new.derived_reactive_power_l3_import_t0_min_timestamp
        )
      end derived_reactive_power_l3_import_t0_min_timestamp,
      new.derived_reactive_power_total_export_t0_avg_var - coalesce(
        old.derived_reactive_power_total_export_t0_avg_var,
        0
      ) derived_reactive_power_total_export_t0_avg_var,
      greatest(
        new.derived_reactive_power_total_export_t0_max_var,
        coalesce(
          old.derived_reactive_power_total_export_t0_max_var,
          new.derived_reactive_power_total_export_t0_max_var
        )
      ) derived_reactive_power_total_export_t0_max_var,
      case
        when new.derived_reactive_power_total_export_t0_max_var > coalesce(
          old.derived_reactive_power_total_export_t0_max_var,
          new.derived_reactive_power_total_export_t0_max_var
        ) then new.derived_reactive_power_total_export_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_total_export_t0_max_timestamp,
          new.derived_reactive_power_total_export_t0_max_timestamp
        )
      end derived_reactive_power_total_export_t0_max_timestamp,
      least(
        new.derived_reactive_power_total_export_t0_min_var,
        coalesce(
          old.derived_reactive_power_total_export_t0_min_var,
          new.derived_reactive_power_total_export_t0_min_var
        )
      ) derived_reactive_power_total_export_t0_min_var,
      case
        when new.derived_reactive_power_total_export_t0_min_var < coalesce(
          old.derived_reactive_power_total_export_t0_min_var,
          new.derived_reactive_power_total_export_t0_min_var
        ) then new.derived_reactive_power_total_export_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_total_export_t0_min_timestamp,
          new.derived_reactive_power_total_export_t0_min_timestamp
        )
      end derived_reactive_power_total_export_t0_min_timestamp,
      new.derived_reactive_power_total_import_t0_avg_var - coalesce(
        old.derived_reactive_power_total_import_t0_avg_var,
        0
      ) derived_reactive_power_total_import_t0_avg_var,
      greatest(
        new.derived_reactive_power_total_import_t0_max_var,
        coalesce(
          old.derived_reactive_power_total_import_t0_max_var,
          new.derived_reactive_power_total_import_t0_max_var
        )
      ) derived_reactive_power_total_import_t0_max_var,
      case
        when new.derived_reactive_power_total_import_t0_max_var > coalesce(
          old.derived_reactive_power_total_import_t0_max_var,
          new.derived_reactive_power_total_import_t0_max_var
        ) then new.derived_reactive_power_total_import_t0_max_timestamp
        else coalesce(
          old.derived_reactive_power_total_import_t0_max_timestamp,
          new.derived_reactive_power_total_import_t0_max_timestamp
        )
      end derived_reactive_power_total_import_t0_max_timestamp,
      least(
        new.derived_reactive_power_total_import_t0_min_var,
        coalesce(
          old.derived_reactive_power_total_import_t0_min_var,
          new.derived_reactive_power_total_import_t0_min_var
        )
      ) derived_reactive_power_total_import_t0_min_var,
      case
        when new.derived_reactive_power_total_import_t0_min_var < coalesce(
          old.derived_reactive_power_total_import_t0_min_var,
          new.derived_reactive_power_total_import_t0_min_var
        ) then new.derived_reactive_power_total_import_t0_min_timestamp
        else coalesce(
          old.derived_reactive_power_total_import_t0_min_timestamp,
          new.derived_reactive_power_total_import_t0_min_timestamp
        )
      end derived_reactive_power_total_import_t0_min_timestamp
    from
      new
      left join old on old.timestamp = new.timestamp
      and old.interval = new.interval
      and old.meter_id = new.meter_id
      and old.measurement_location_id = new.measurement_location_id
  ),
  daily as (
    update abb_b2x_aggregates
    set
      quarter_hour_count = abb_b2x_aggregates.quarter_hour_count + delta.new_count,
      derived_active_power_l1_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l1_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l1_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l1_export_t0_max_w = greatest(
        delta.derived_active_power_l1_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w
      ),
      derived_active_power_l1_export_t0_max_timestamp = case
        when delta.derived_active_power_l1_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w then delta.derived_active_power_l1_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_export_t0_max_timestamp
      end,
      derived_active_power_l1_export_t0_min_w = least(
        delta.derived_active_power_l1_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w
      ),
      derived_active_power_l1_export_t0_min_timestamp = case
        when delta.derived_active_power_l1_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w then delta.derived_active_power_l1_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_export_t0_min_timestamp
      end,
      derived_active_power_l1_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l1_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l1_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l1_import_t0_max_w = greatest(
        delta.derived_active_power_l1_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w
      ),
      derived_active_power_l1_import_t0_max_timestamp = case
        when delta.derived_active_power_l1_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w then delta.derived_active_power_l1_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_import_t0_max_timestamp
      end,
      derived_active_power_l1_import_t0_min_w = least(
        delta.derived_active_power_l1_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w
      ),
      derived_active_power_l1_import_t0_min_timestamp = case
        when delta.derived_active_power_l1_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w then delta.derived_active_power_l1_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_import_t0_min_timestamp
      end,
      derived_active_power_l2_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l2_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l2_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l2_export_t0_max_w = greatest(
        delta.derived_active_power_l2_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w
      ),
      derived_active_power_l2_export_t0_max_timestamp = case
        when delta.derived_active_power_l2_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w then delta.derived_active_power_l2_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_export_t0_max_timestamp
      end,
      derived_active_power_l2_export_t0_min_w = least(
        delta.derived_active_power_l2_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w
      ),
      derived_active_power_l2_export_t0_min_timestamp = case
        when delta.derived_active_power_l2_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w then delta.derived_active_power_l2_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_export_t0_min_timestamp
      end,
      derived_active_power_l2_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l2_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l2_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l2_import_t0_max_w = greatest(
        delta.derived_active_power_l2_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w
      ),
      derived_active_power_l2_import_t0_max_timestamp = case
        when delta.derived_active_power_l2_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w then delta.derived_active_power_l2_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_import_t0_max_timestamp
      end,
      derived_active_power_l2_import_t0_min_w = least(
        delta.derived_active_power_l2_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w
      ),
      derived_active_power_l2_import_t0_min_timestamp = case
        when delta.derived_active_power_l2_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w then delta.derived_active_power_l2_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_import_t0_min_timestamp
      end,
      derived_active_power_l3_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l3_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l3_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l3_export_t0_max_w = greatest(
        delta.derived_active_power_l3_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w
      ),
      derived_active_power_l3_export_t0_max_timestamp = case
        when delta.derived_active_power_l3_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w then delta.derived_active_power_l3_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_export_t0_max_timestamp
      end,
      derived_active_power_l3_export_t0_min_w = least(
        delta.derived_active_power_l3_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w
      ),
      derived_active_power_l3_export_t0_min_timestamp = case
        when delta.derived_active_power_l3_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w then delta.derived_active_power_l3_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_export_t0_min_timestamp
      end,
      derived_active_power_l3_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l3_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l3_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l3_import_t0_max_w = greatest(
        delta.derived_active_power_l3_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w
      ),
      derived_active_power_l3_import_t0_max_timestamp = case
        when delta.derived_active_power_l3_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w then delta.derived_active_power_l3_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_import_t0_max_timestamp
      end,
      derived_active_power_l3_import_t0_min_w = least(
        delta.derived_active_power_l3_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w
      ),
      derived_active_power_l3_import_t0_min_timestamp = case
        when delta.derived_active_power_l3_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w then delta.derived_active_power_l3_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_import_t0_min_timestamp
      end,
      derived_active_power_total_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_export_t0_max_w = greatest(
        delta.derived_active_power_total_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_total_export_t0_max_w
      ),
      derived_active_power_total_export_t0_max_timestamp = case
        when delta.derived_active_power_total_export_t0_max_w > abb_b2x_aggregates.derived_active_power_total_export_t0_max_w then delta.derived_active_power_total_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_export_t0_max_timestamp
      end,
      derived_active_power_total_export_t0_min_w = least(
        delta.derived_active_power_total_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_total_export_t0_min_w
      ),
      derived_active_power_total_export_t0_min_timestamp = case
        when delta.derived_active_power_total_export_t0_min_w < abb_b2x_aggregates.derived_active_power_total_export_t0_min_w then delta.derived_active_power_total_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_export_t0_min_timestamp
      end,
      derived_active_power_total_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t0_max_w = greatest(
        delta.derived_active_power_total_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_total_import_t0_max_w
      ),
      derived_active_power_total_import_t0_max_timestamp = case
        when delta.derived_active_power_total_import_t0_max_w > abb_b2x_aggregates.derived_active_power_total_import_t0_max_w then delta.derived_active_power_total_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t0_max_timestamp
      end,
      derived_active_power_total_import_t0_min_w = least(
        delta.derived_active_power_total_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_total_import_t0_min_w
      ),
      derived_active_power_total_import_t0_min_timestamp = case
        when delta.derived_active_power_total_import_t0_min_w < abb_b2x_aggregates.derived_active_power_total_import_t0_min_w then delta.derived_active_power_total_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t0_min_timestamp
      end,
      derived_active_power_total_import_t1_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_import_t1_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t1_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t1_max_w = greatest(
        delta.derived_active_power_total_import_t1_max_w,
        abb_b2x_aggregates.derived_active_power_total_import_t1_max_w
      ),
      derived_active_power_total_import_t1_max_timestamp = case
        when delta.derived_active_power_total_import_t1_max_w > abb_b2x_aggregates.derived_active_power_total_import_t1_max_w then delta.derived_active_power_total_import_t1_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t1_max_timestamp
      end,
      derived_active_power_total_import_t1_min_w = least(
        delta.derived_active_power_total_import_t1_min_w,
        abb_b2x_aggregates.derived_active_power_total_import_t1_min_w
      ),
      derived_active_power_total_import_t1_min_timestamp = case
        when delta.derived_active_power_total_import_t1_min_w < abb_b2x_aggregates.derived_active_power_total_import_t1_min_w then delta.derived_active_power_total_import_t1_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t1_min_timestamp
      end,
      derived_active_power_total_import_t2_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_import_t2_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t2_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t2_max_w = greatest(
        delta.derived_active_power_total_import_t2_max_w,
        abb_b2x_aggregates.derived_active_power_total_import_t2_max_w
      ),
      derived_active_power_total_import_t2_max_timestamp = case
        when delta.derived_active_power_total_import_t2_max_w > abb_b2x_aggregates.derived_active_power_total_import_t2_max_w then delta.derived_active_power_total_import_t2_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t2_max_timestamp
      end,
      derived_active_power_total_import_t2_min_w = least(
        delta.derived_active_power_total_import_t2_min_w,
        abb_b2x_aggregates.derived_active_power_total_import_t2_min_w
      ),
      derived_active_power_total_import_t2_min_timestamp = case
        when delta.derived_active_power_total_import_t2_min_w < abb_b2x_aggregates.derived_active_power_total_import_t2_min_w then delta.derived_active_power_total_import_t2_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t2_min_timestamp
      end,
      derived_reactive_power_l1_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l1_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l1_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l1_export_t0_max_var = greatest(
        delta.derived_reactive_power_l1_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var
      ),
      derived_reactive_power_l1_export_t0_max_timestamp = case
        when delta.derived_reactive_power_l1_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var then delta.derived_reactive_power_l1_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_timestamp
      end,
      derived_reactive_power_l1_export_t0_min_var = least(
        delta.derived_reactive_power_l1_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var
      ),
      derived_reactive_power_l1_export_t0_min_timestamp = case
        when delta.derived_reactive_power_l1_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var then delta.derived_reactive_power_l1_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_timestamp
      end,
      derived_reactive_power_l1_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l1_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l1_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l1_import_t0_max_var = greatest(
        delta.derived_reactive_power_l1_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var
      ),
      derived_reactive_power_l1_import_t0_max_timestamp = case
        when delta.derived_reactive_power_l1_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var then delta.derived_reactive_power_l1_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_timestamp
      end,
      derived_reactive_power_l1_import_t0_min_var = least(
        delta.derived_reactive_power_l1_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var
      ),
      derived_reactive_power_l1_import_t0_min_timestamp = case
        when delta.derived_reactive_power_l1_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var then delta.derived_reactive_power_l1_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_timestamp
      end,
      derived_reactive_power_l2_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l2_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l2_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l2_export_t0_max_var = greatest(
        delta.derived_reactive_power_l2_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var
      ),
      derived_reactive_power_l2_export_t0_max_timestamp = case
        when delta.derived_reactive_power_l2_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var then delta.derived_reactive_power_l2_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_timestamp
      end,
      derived_reactive_power_l2_export_t0_min_var = least(
        delta.derived_reactive_power_l2_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var
      ),
      derived_reactive_power_l2_export_t0_min_timestamp = case
        when delta.derived_reactive_power_l2_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var then delta.derived_reactive_power_l2_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_timestamp
      end,
      derived_reactive_power_l2_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l2_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l2_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l2_import_t0_max_var = greatest(
        delta.derived_reactive_power_l2_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var
      ),
      derived_reactive_power_l2_import_t0_max_timestamp = case
        when delta.derived_reactive_power_l2_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var then delta.derived_reactive_power_l2_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_timestamp
      end,
      derived_reactive_power_l2_import_t0_min_var = least(
        delta.derived_reactive_power_l2_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var
      ),
      derived_reactive_power_l2_import_t0_min_timestamp = case
        when delta.derived_reactive_power_l2_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var then delta.derived_reactive_power_l2_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_timestamp
      end,
      derived_reactive_power_l3_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l3_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l3_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l3_export_t0_max_var = greatest(
        delta.derived_reactive_power_l3_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var
      ),
      derived_reactive_power_l3_export_t0_max_timestamp = case
        when delta.derived_reactive_power_l3_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var then delta.derived_reactive_power_l3_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_timestamp
      end,
      derived_reactive_power_l3_export_t0_min_var = least(
        delta.derived_reactive_power_l3_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var
      ),
      derived_reactive_power_l3_export_t0_min_timestamp = case
        when delta.derived_reactive_power_l3_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var then delta.derived_reactive_power_l3_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_timestamp
      end,
      derived_reactive_power_l3_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l3_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l3_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l3_import_t0_max_var = greatest(
        delta.derived_reactive_power_l3_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var
      ),
      derived_reactive_power_l3_import_t0_max_timestamp = case
        when delta.derived_reactive_power_l3_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var then delta.derived_reactive_power_l3_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_timestamp
      end,
      derived_reactive_power_l3_import_t0_min_var = least(
        delta.derived_reactive_power_l3_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var
      ),
      derived_reactive_power_l3_import_t0_min_timestamp = case
        when delta.derived_reactive_power_l3_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var then delta.derived_reactive_power_l3_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_timestamp
      end,
      derived_reactive_power_total_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_total_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_total_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_export_t0_max_var = greatest(
        delta.derived_reactive_power_total_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var
      ),
      derived_reactive_power_total_export_t0_max_timestamp = case
        when delta.derived_reactive_power_total_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var then delta.derived_reactive_power_total_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_timestamp
      end,
      derived_reactive_power_total_export_t0_min_var = least(
        delta.derived_reactive_power_total_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var
      ),
      derived_reactive_power_total_export_t0_min_timestamp = case
        when delta.derived_reactive_power_total_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var then delta.derived_reactive_power_total_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_timestamp
      end,
      derived_reactive_power_total_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_total_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_total_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_import_t0_max_var = greatest(
        delta.derived_reactive_power_total_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var
      ),
      derived_reactive_power_total_import_t0_max_timestamp = case
        when delta.derived_reactive_power_total_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var then delta.derived_reactive_power_total_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_timestamp
      end,
      derived_reactive_power_total_import_t0_min_var = least(
        delta.derived_reactive_power_total_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var
      ),
      derived_reactive_power_total_import_t0_min_timestamp = case
        when delta.derived_reactive_power_total_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var then delta.derived_reactive_power_total_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_timestamp
      end
    from
      delta
    where
      abb_b2x_aggregates.timestamp = delta.daily_timestamp
      and abb_b2x_aggregates.interval = 'day'::interval_entity
      and abb_b2x_aggregates.meter_id = delta.meter_id
      and abb_b2x_aggregates.measurement_location_id = delta.measurement_location_id
    returning
      abb_b2x_aggregates.*
  ),
  monthly as (
    update abb_b2x_aggregates
    set
      quarter_hour_count = abb_b2x_aggregates.quarter_hour_count + delta.new_count,
      derived_active_power_l1_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l1_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l1_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l1_export_t0_max_w = greatest(
        delta.derived_active_power_l1_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w
      ),
      derived_active_power_l1_export_t0_max_timestamp = case
        when delta.derived_active_power_l1_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_export_t0_max_w then delta.derived_active_power_l1_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_export_t0_max_timestamp
      end,
      derived_active_power_l1_export_t0_min_w = least(
        delta.derived_active_power_l1_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w
      ),
      derived_active_power_l1_export_t0_min_timestamp = case
        when delta.derived_active_power_l1_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_export_t0_min_w then delta.derived_active_power_l1_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_export_t0_min_timestamp
      end,
      derived_active_power_l1_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l1_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l1_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l1_import_t0_max_w = greatest(
        delta.derived_active_power_l1_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w
      ),
      derived_active_power_l1_import_t0_max_timestamp = case
        when delta.derived_active_power_l1_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l1_import_t0_max_w then delta.derived_active_power_l1_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_import_t0_max_timestamp
      end,
      derived_active_power_l1_import_t0_min_w = least(
        delta.derived_active_power_l1_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w
      ),
      derived_active_power_l1_import_t0_min_timestamp = case
        when delta.derived_active_power_l1_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l1_import_t0_min_w then delta.derived_active_power_l1_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l1_import_t0_min_timestamp
      end,
      derived_active_power_l2_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l2_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l2_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l2_export_t0_max_w = greatest(
        delta.derived_active_power_l2_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w
      ),
      derived_active_power_l2_export_t0_max_timestamp = case
        when delta.derived_active_power_l2_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_export_t0_max_w then delta.derived_active_power_l2_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_export_t0_max_timestamp
      end,
      derived_active_power_l2_export_t0_min_w = least(
        delta.derived_active_power_l2_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w
      ),
      derived_active_power_l2_export_t0_min_timestamp = case
        when delta.derived_active_power_l2_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_export_t0_min_w then delta.derived_active_power_l2_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_export_t0_min_timestamp
      end,
      derived_active_power_l2_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l2_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l2_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l2_import_t0_max_w = greatest(
        delta.derived_active_power_l2_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w
      ),
      derived_active_power_l2_import_t0_max_timestamp = case
        when delta.derived_active_power_l2_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l2_import_t0_max_w then delta.derived_active_power_l2_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_import_t0_max_timestamp
      end,
      derived_active_power_l2_import_t0_min_w = least(
        delta.derived_active_power_l2_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w
      ),
      derived_active_power_l2_import_t0_min_timestamp = case
        when delta.derived_active_power_l2_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l2_import_t0_min_w then delta.derived_active_power_l2_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l2_import_t0_min_timestamp
      end,
      derived_active_power_l3_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l3_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l3_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l3_export_t0_max_w = greatest(
        delta.derived_active_power_l3_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w
      ),
      derived_active_power_l3_export_t0_max_timestamp = case
        when delta.derived_active_power_l3_export_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_export_t0_max_w then delta.derived_active_power_l3_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_export_t0_max_timestamp
      end,
      derived_active_power_l3_export_t0_min_w = least(
        delta.derived_active_power_l3_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w
      ),
      derived_active_power_l3_export_t0_min_timestamp = case
        when delta.derived_active_power_l3_export_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_export_t0_min_w then delta.derived_active_power_l3_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_export_t0_min_timestamp
      end,
      derived_active_power_l3_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_l3_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_l3_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l3_import_t0_max_w = greatest(
        delta.derived_active_power_l3_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w
      ),
      derived_active_power_l3_import_t0_max_timestamp = case
        when delta.derived_active_power_l3_import_t0_max_w > abb_b2x_aggregates.derived_active_power_l3_import_t0_max_w then delta.derived_active_power_l3_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_import_t0_max_timestamp
      end,
      derived_active_power_l3_import_t0_min_w = least(
        delta.derived_active_power_l3_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w
      ),
      derived_active_power_l3_import_t0_min_timestamp = case
        when delta.derived_active_power_l3_import_t0_min_w < abb_b2x_aggregates.derived_active_power_l3_import_t0_min_w then delta.derived_active_power_l3_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_l3_import_t0_min_timestamp
      end,
      derived_active_power_total_export_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_export_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_export_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_export_t0_max_w = greatest(
        delta.derived_active_power_total_export_t0_max_w,
        abb_b2x_aggregates.derived_active_power_total_export_t0_max_w
      ),
      derived_active_power_total_export_t0_max_timestamp = case
        when delta.derived_active_power_total_export_t0_max_w > abb_b2x_aggregates.derived_active_power_total_export_t0_max_w then delta.derived_active_power_total_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_export_t0_max_timestamp
      end,
      derived_active_power_total_export_t0_min_w = least(
        delta.derived_active_power_total_export_t0_min_w,
        abb_b2x_aggregates.derived_active_power_total_export_t0_min_w
      ),
      derived_active_power_total_export_t0_min_timestamp = case
        when delta.derived_active_power_total_export_t0_min_w < abb_b2x_aggregates.derived_active_power_total_export_t0_min_w then delta.derived_active_power_total_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_export_t0_min_timestamp
      end,
      derived_active_power_total_import_t0_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_import_t0_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t0_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t0_max_w = greatest(
        delta.derived_active_power_total_import_t0_max_w,
        abb_b2x_aggregates.derived_active_power_total_import_t0_max_w
      ),
      derived_active_power_total_import_t0_max_timestamp = case
        when delta.derived_active_power_total_import_t0_max_w > abb_b2x_aggregates.derived_active_power_total_import_t0_max_w then delta.derived_active_power_total_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t0_max_timestamp
      end,
      derived_active_power_total_import_t0_min_w = least(
        delta.derived_active_power_total_import_t0_min_w,
        abb_b2x_aggregates.derived_active_power_total_import_t0_min_w
      ),
      derived_active_power_total_import_t0_min_timestamp = case
        when delta.derived_active_power_total_import_t0_min_w < abb_b2x_aggregates.derived_active_power_total_import_t0_min_w then delta.derived_active_power_total_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t0_min_timestamp
      end,
      derived_active_power_total_import_t1_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_import_t1_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t1_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t1_max_w = greatest(
        delta.derived_active_power_total_import_t1_max_w,
        abb_b2x_aggregates.derived_active_power_total_import_t1_max_w
      ),
      derived_active_power_total_import_t1_max_timestamp = case
        when delta.derived_active_power_total_import_t1_max_w > abb_b2x_aggregates.derived_active_power_total_import_t1_max_w then delta.derived_active_power_total_import_t1_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t1_max_timestamp
      end,
      derived_active_power_total_import_t1_min_w = least(
        delta.derived_active_power_total_import_t1_min_w,
        abb_b2x_aggregates.derived_active_power_total_import_t1_min_w
      ),
      derived_active_power_total_import_t1_min_timestamp = case
        when delta.derived_active_power_total_import_t1_min_w < abb_b2x_aggregates.derived_active_power_total_import_t1_min_w then delta.derived_active_power_total_import_t1_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t1_min_timestamp
      end,
      derived_active_power_total_import_t2_avg_w = (
        abb_b2x_aggregates.derived_active_power_total_import_t2_avg_w * abb_b2x_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t2_avg_w
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t2_max_w = greatest(
        delta.derived_active_power_total_import_t2_max_w,
        abb_b2x_aggregates.derived_active_power_total_import_t2_max_w
      ),
      derived_active_power_total_import_t2_max_timestamp = case
        when delta.derived_active_power_total_import_t2_max_w > abb_b2x_aggregates.derived_active_power_total_import_t2_max_w then delta.derived_active_power_total_import_t2_max_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t2_max_timestamp
      end,
      derived_active_power_total_import_t2_min_w = least(
        delta.derived_active_power_total_import_t2_min_w,
        abb_b2x_aggregates.derived_active_power_total_import_t2_min_w
      ),
      derived_active_power_total_import_t2_min_timestamp = case
        when delta.derived_active_power_total_import_t2_min_w < abb_b2x_aggregates.derived_active_power_total_import_t2_min_w then delta.derived_active_power_total_import_t2_min_timestamp
        else abb_b2x_aggregates.derived_active_power_total_import_t2_min_timestamp
      end,
      derived_reactive_power_l1_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l1_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l1_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l1_export_t0_max_var = greatest(
        delta.derived_reactive_power_l1_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var
      ),
      derived_reactive_power_l1_export_t0_max_timestamp = case
        when delta.derived_reactive_power_l1_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_var then delta.derived_reactive_power_l1_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_max_timestamp
      end,
      derived_reactive_power_l1_export_t0_min_var = least(
        delta.derived_reactive_power_l1_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var
      ),
      derived_reactive_power_l1_export_t0_min_timestamp = case
        when delta.derived_reactive_power_l1_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_var then delta.derived_reactive_power_l1_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_export_t0_min_timestamp
      end,
      derived_reactive_power_l1_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l1_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l1_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l1_import_t0_max_var = greatest(
        delta.derived_reactive_power_l1_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var
      ),
      derived_reactive_power_l1_import_t0_max_timestamp = case
        when delta.derived_reactive_power_l1_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_var then delta.derived_reactive_power_l1_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_max_timestamp
      end,
      derived_reactive_power_l1_import_t0_min_var = least(
        delta.derived_reactive_power_l1_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var
      ),
      derived_reactive_power_l1_import_t0_min_timestamp = case
        when delta.derived_reactive_power_l1_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_var then delta.derived_reactive_power_l1_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l1_import_t0_min_timestamp
      end,
      derived_reactive_power_l2_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l2_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l2_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l2_export_t0_max_var = greatest(
        delta.derived_reactive_power_l2_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var
      ),
      derived_reactive_power_l2_export_t0_max_timestamp = case
        when delta.derived_reactive_power_l2_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_var then delta.derived_reactive_power_l2_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_max_timestamp
      end,
      derived_reactive_power_l2_export_t0_min_var = least(
        delta.derived_reactive_power_l2_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var
      ),
      derived_reactive_power_l2_export_t0_min_timestamp = case
        when delta.derived_reactive_power_l2_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_var then delta.derived_reactive_power_l2_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_export_t0_min_timestamp
      end,
      derived_reactive_power_l2_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l2_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l2_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l2_import_t0_max_var = greatest(
        delta.derived_reactive_power_l2_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var
      ),
      derived_reactive_power_l2_import_t0_max_timestamp = case
        when delta.derived_reactive_power_l2_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_var then delta.derived_reactive_power_l2_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_max_timestamp
      end,
      derived_reactive_power_l2_import_t0_min_var = least(
        delta.derived_reactive_power_l2_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var
      ),
      derived_reactive_power_l2_import_t0_min_timestamp = case
        when delta.derived_reactive_power_l2_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_var then delta.derived_reactive_power_l2_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l2_import_t0_min_timestamp
      end,
      derived_reactive_power_l3_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l3_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l3_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l3_export_t0_max_var = greatest(
        delta.derived_reactive_power_l3_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var
      ),
      derived_reactive_power_l3_export_t0_max_timestamp = case
        when delta.derived_reactive_power_l3_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_var then delta.derived_reactive_power_l3_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_max_timestamp
      end,
      derived_reactive_power_l3_export_t0_min_var = least(
        delta.derived_reactive_power_l3_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var
      ),
      derived_reactive_power_l3_export_t0_min_timestamp = case
        when delta.derived_reactive_power_l3_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_var then delta.derived_reactive_power_l3_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_export_t0_min_timestamp
      end,
      derived_reactive_power_l3_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_l3_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_l3_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_l3_import_t0_max_var = greatest(
        delta.derived_reactive_power_l3_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var
      ),
      derived_reactive_power_l3_import_t0_max_timestamp = case
        when delta.derived_reactive_power_l3_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_var then delta.derived_reactive_power_l3_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_max_timestamp
      end,
      derived_reactive_power_l3_import_t0_min_var = least(
        delta.derived_reactive_power_l3_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var
      ),
      derived_reactive_power_l3_import_t0_min_timestamp = case
        when delta.derived_reactive_power_l3_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_var then delta.derived_reactive_power_l3_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_l3_import_t0_min_timestamp
      end,
      derived_reactive_power_total_export_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_total_export_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_total_export_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_export_t0_max_var = greatest(
        delta.derived_reactive_power_total_export_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var
      ),
      derived_reactive_power_total_export_t0_max_timestamp = case
        when delta.derived_reactive_power_total_export_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_var then delta.derived_reactive_power_total_export_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_export_t0_max_timestamp
      end,
      derived_reactive_power_total_export_t0_min_var = least(
        delta.derived_reactive_power_total_export_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var
      ),
      derived_reactive_power_total_export_t0_min_timestamp = case
        when delta.derived_reactive_power_total_export_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_var then delta.derived_reactive_power_total_export_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_export_t0_min_timestamp
      end,
      derived_reactive_power_total_import_t0_avg_var = (
        abb_b2x_aggregates.derived_reactive_power_total_import_t0_avg_var * abb_b2x_aggregates.quarter_hour_count + delta.derived_reactive_power_total_import_t0_avg_var
      ) / (
        abb_b2x_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_import_t0_max_var = greatest(
        delta.derived_reactive_power_total_import_t0_max_var,
        abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var
      ),
      derived_reactive_power_total_import_t0_max_timestamp = case
        when delta.derived_reactive_power_total_import_t0_max_var > abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_var then delta.derived_reactive_power_total_import_t0_max_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_import_t0_max_timestamp
      end,
      derived_reactive_power_total_import_t0_min_var = least(
        delta.derived_reactive_power_total_import_t0_min_var,
        abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var
      ),
      derived_reactive_power_total_import_t0_min_timestamp = case
        when delta.derived_reactive_power_total_import_t0_min_var < abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_var then delta.derived_reactive_power_total_import_t0_min_timestamp
        else abb_b2x_aggregates.derived_reactive_power_total_import_t0_min_timestamp
      end
    from
      delta
    where
      abb_b2x_aggregates.timestamp = delta.monthly_timestamp
      and abb_b2x_aggregates.interval = 'month'::interval_entity
      and abb_b2x_aggregates.meter_id = delta.meter_id
      and abb_b2x_aggregates.measurement_location_id = delta.measurement_location_id
    returning
      abb_b2x_aggregates.*
  )
  -- select
  --   timestamp,
  --   daily_timestamp,
  --   monthly_timestamp
  -- from
  --   delta;
select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count,
  derived_active_power_total_import_t0_min_w,
  derived_active_power_total_import_t0_max_w,
  derived_active_power_total_import_t0_avg_w
from
  new
union all
select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count,
  derived_active_power_total_import_t0_min_w,
  derived_active_power_total_import_t0_max_w,
  derived_active_power_total_import_t0_avg_w
from
  daily
union all
select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count,
  derived_active_power_total_import_t0_min_w,
  derived_active_power_total_import_t0_max_w,
  derived_active_power_total_import_t0_avg_w
from
  monthly;

rollback;
