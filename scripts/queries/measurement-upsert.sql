begin transaction;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count
from
  abb_b2x_aggregates;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count
from
  schneider_iem3xxx_aggregates;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p0_timestamp'::timestamptz,
:'p0_meter_id',
:'p0_measurement_location_id',
:'p0_active_energy_l1_export_t0_wh',
:'p0_active_energy_l1_import_t0_wh',
:'p0_active_energy_l2_export_t0_wh',
:'p0_active_energy_l2_import_t0_wh',
:'p0_active_energy_l3_export_t0_wh',
:'p0_active_energy_l3_import_t0_wh',
:'p0_active_energy_total_export_t0_wh',
:'p0_active_energy_total_import_t0_wh',
:'p0_active_energy_total_import_t1_wh',
:'p0_active_energy_total_import_t2_wh',
:'p0_active_power_l1_net_t0_w',
:'p0_active_power_l2_net_t0_w',
:'p0_active_power_l3_net_t0_w',
:'p0_current_l1_any_t0_a',
:'p0_current_l2_any_t0_a',
:'p0_current_l3_any_t0_a',
:'p0_reactive_energy_l1_export_t0_varh',
:'p0_reactive_energy_l1_import_t0_varh',
:'p0_reactive_energy_l2_export_t0_varh',
:'p0_reactive_energy_l2_import_t0_varh',
:'p0_reactive_energy_l3_export_t0_varh',
:'p0_reactive_energy_l3_import_t0_varh',
:'p0_reactive_energy_total_export_t0_varh',
:'p0_reactive_energy_total_import_t0_varh',
:'p0_reactive_power_l1_net_t0_var',
:'p0_reactive_power_l2_net_t0_var',
:'p0_reactive_power_l3_net_t0_var',
:'p0_voltage_l1_any_t0_v',
:'p0_voltage_l2_any_t0_v',
:'p0_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p1_timestamp'::timestamptz,
:'p1_meter_id',
:'p1_measurement_location_id',
:'p1_active_energy_l1_export_t0_wh',
:'p1_active_energy_l1_import_t0_wh',
:'p1_active_energy_l2_export_t0_wh',
:'p1_active_energy_l2_import_t0_wh',
:'p1_active_energy_l3_export_t0_wh',
:'p1_active_energy_l3_import_t0_wh',
:'p1_active_energy_total_export_t0_wh',
:'p1_active_energy_total_import_t0_wh',
:'p1_active_energy_total_import_t1_wh',
:'p1_active_energy_total_import_t2_wh',
:'p1_active_power_l1_net_t0_w',
:'p1_active_power_l2_net_t0_w',
:'p1_active_power_l3_net_t0_w',
:'p1_current_l1_any_t0_a',
:'p1_current_l2_any_t0_a',
:'p1_current_l3_any_t0_a',
:'p1_reactive_energy_l1_export_t0_varh',
:'p1_reactive_energy_l1_import_t0_varh',
:'p1_reactive_energy_l2_export_t0_varh',
:'p1_reactive_energy_l2_import_t0_varh',
:'p1_reactive_energy_l3_export_t0_varh',
:'p1_reactive_energy_l3_import_t0_varh',
:'p1_reactive_energy_total_export_t0_varh',
:'p1_reactive_energy_total_import_t0_varh',
:'p1_reactive_power_l1_net_t0_var',
:'p1_reactive_power_l2_net_t0_var',
:'p1_reactive_power_l3_net_t0_var',
:'p1_voltage_l1_any_t0_v',
:'p1_voltage_l2_any_t0_v',
:'p1_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p2_timestamp'::timestamptz,
:'p2_meter_id',
:'p2_measurement_location_id',
:'p2_active_energy_l1_export_t0_wh',
:'p2_active_energy_l1_import_t0_wh',
:'p2_active_energy_l2_export_t0_wh',
:'p2_active_energy_l2_import_t0_wh',
:'p2_active_energy_l3_export_t0_wh',
:'p2_active_energy_l3_import_t0_wh',
:'p2_active_energy_total_export_t0_wh',
:'p2_active_energy_total_import_t0_wh',
:'p2_active_energy_total_import_t1_wh',
:'p2_active_energy_total_import_t2_wh',
:'p2_active_power_l1_net_t0_w',
:'p2_active_power_l2_net_t0_w',
:'p2_active_power_l3_net_t0_w',
:'p2_current_l1_any_t0_a',
:'p2_current_l2_any_t0_a',
:'p2_current_l3_any_t0_a',
:'p2_reactive_energy_l1_export_t0_varh',
:'p2_reactive_energy_l1_import_t0_varh',
:'p2_reactive_energy_l2_export_t0_varh',
:'p2_reactive_energy_l2_import_t0_varh',
:'p2_reactive_energy_l3_export_t0_varh',
:'p2_reactive_energy_l3_import_t0_varh',
:'p2_reactive_energy_total_export_t0_varh',
:'p2_reactive_energy_total_import_t0_varh',
:'p2_reactive_power_l1_net_t0_var',
:'p2_reactive_power_l2_net_t0_var',
:'p2_reactive_power_l3_net_t0_var',
:'p2_voltage_l1_any_t0_v',
:'p2_voltage_l2_any_t0_v',
:'p2_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p3_timestamp'::timestamptz,
:'p3_meter_id',
:'p3_measurement_location_id',
:'p3_active_energy_l1_export_t0_wh',
:'p3_active_energy_l1_import_t0_wh',
:'p3_active_energy_l2_export_t0_wh',
:'p3_active_energy_l2_import_t0_wh',
:'p3_active_energy_l3_export_t0_wh',
:'p3_active_energy_l3_import_t0_wh',
:'p3_active_energy_total_export_t0_wh',
:'p3_active_energy_total_import_t0_wh',
:'p3_active_energy_total_import_t1_wh',
:'p3_active_energy_total_import_t2_wh',
:'p3_active_power_l1_net_t0_w',
:'p3_active_power_l2_net_t0_w',
:'p3_active_power_l3_net_t0_w',
:'p3_current_l1_any_t0_a',
:'p3_current_l2_any_t0_a',
:'p3_current_l3_any_t0_a',
:'p3_reactive_energy_l1_export_t0_varh',
:'p3_reactive_energy_l1_import_t0_varh',
:'p3_reactive_energy_l2_export_t0_varh',
:'p3_reactive_energy_l2_import_t0_varh',
:'p3_reactive_energy_l3_export_t0_varh',
:'p3_reactive_energy_l3_import_t0_varh',
:'p3_reactive_energy_total_export_t0_varh',
:'p3_reactive_energy_total_import_t0_varh',
:'p3_reactive_power_l1_net_t0_var',
:'p3_reactive_power_l2_net_t0_var',
:'p3_reactive_power_l3_net_t0_var',
:'p3_voltage_l1_any_t0_v',
:'p3_voltage_l2_any_t0_v',
:'p3_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p4_timestamp'::timestamptz,
:'p4_meter_id',
:'p4_measurement_location_id',
:'p4_active_energy_l1_export_t0_wh',
:'p4_active_energy_l1_import_t0_wh',
:'p4_active_energy_l2_export_t0_wh',
:'p4_active_energy_l2_import_t0_wh',
:'p4_active_energy_l3_export_t0_wh',
:'p4_active_energy_l3_import_t0_wh',
:'p4_active_energy_total_export_t0_wh',
:'p4_active_energy_total_import_t0_wh',
:'p4_active_energy_total_import_t1_wh',
:'p4_active_energy_total_import_t2_wh',
:'p4_active_power_l1_net_t0_w',
:'p4_active_power_l2_net_t0_w',
:'p4_active_power_l3_net_t0_w',
:'p4_current_l1_any_t0_a',
:'p4_current_l2_any_t0_a',
:'p4_current_l3_any_t0_a',
:'p4_reactive_energy_l1_export_t0_varh',
:'p4_reactive_energy_l1_import_t0_varh',
:'p4_reactive_energy_l2_export_t0_varh',
:'p4_reactive_energy_l2_import_t0_varh',
:'p4_reactive_energy_l3_export_t0_varh',
:'p4_reactive_energy_l3_import_t0_varh',
:'p4_reactive_energy_total_export_t0_varh',
:'p4_reactive_energy_total_import_t0_varh',
:'p4_reactive_power_l1_net_t0_var',
:'p4_reactive_power_l2_net_t0_var',
:'p4_reactive_power_l3_net_t0_var',
:'p4_voltage_l1_any_t0_v',
:'p4_voltage_l2_any_t0_v',
:'p4_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p5_timestamp'::timestamptz,
:'p5_meter_id',
:'p5_measurement_location_id',
:'p5_active_energy_l1_export_t0_wh',
:'p5_active_energy_l1_import_t0_wh',
:'p5_active_energy_l2_export_t0_wh',
:'p5_active_energy_l2_import_t0_wh',
:'p5_active_energy_l3_export_t0_wh',
:'p5_active_energy_l3_import_t0_wh',
:'p5_active_energy_total_export_t0_wh',
:'p5_active_energy_total_import_t0_wh',
:'p5_active_energy_total_import_t1_wh',
:'p5_active_energy_total_import_t2_wh',
:'p5_active_power_l1_net_t0_w',
:'p5_active_power_l2_net_t0_w',
:'p5_active_power_l3_net_t0_w',
:'p5_current_l1_any_t0_a',
:'p5_current_l2_any_t0_a',
:'p5_current_l3_any_t0_a',
:'p5_reactive_energy_l1_export_t0_varh',
:'p5_reactive_energy_l1_import_t0_varh',
:'p5_reactive_energy_l2_export_t0_varh',
:'p5_reactive_energy_l2_import_t0_varh',
:'p5_reactive_energy_l3_export_t0_varh',
:'p5_reactive_energy_l3_import_t0_varh',
:'p5_reactive_energy_total_export_t0_varh',
:'p5_reactive_energy_total_import_t0_varh',
:'p5_reactive_power_l1_net_t0_var',
:'p5_reactive_power_l2_net_t0_var',
:'p5_reactive_power_l3_net_t0_var',
:'p5_voltage_l1_any_t0_v',
:'p5_voltage_l2_any_t0_v',
:'p5_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p6_timestamp'::timestamptz,
:'p6_meter_id',
:'p6_measurement_location_id',
:'p6_active_energy_l1_export_t0_wh',
:'p6_active_energy_l1_import_t0_wh',
:'p6_active_energy_l2_export_t0_wh',
:'p6_active_energy_l2_import_t0_wh',
:'p6_active_energy_l3_export_t0_wh',
:'p6_active_energy_l3_import_t0_wh',
:'p6_active_energy_total_export_t0_wh',
:'p6_active_energy_total_import_t0_wh',
:'p6_active_energy_total_import_t1_wh',
:'p6_active_energy_total_import_t2_wh',
:'p6_active_power_l1_net_t0_w',
:'p6_active_power_l2_net_t0_w',
:'p6_active_power_l3_net_t0_w',
:'p6_current_l1_any_t0_a',
:'p6_current_l2_any_t0_a',
:'p6_current_l3_any_t0_a',
:'p6_reactive_energy_l1_export_t0_varh',
:'p6_reactive_energy_l1_import_t0_varh',
:'p6_reactive_energy_l2_export_t0_varh',
:'p6_reactive_energy_l2_import_t0_varh',
:'p6_reactive_energy_l3_export_t0_varh',
:'p6_reactive_energy_l3_import_t0_varh',
:'p6_reactive_energy_total_export_t0_varh',
:'p6_reactive_energy_total_import_t0_varh',
:'p6_reactive_power_l1_net_t0_var',
:'p6_reactive_power_l2_net_t0_var',
:'p6_reactive_power_l3_net_t0_var',
:'p6_voltage_l1_any_t0_v',
:'p6_voltage_l2_any_t0_v',
:'p6_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p7_timestamp'::timestamptz,
:'p7_meter_id',
:'p7_measurement_location_id',
:'p7_active_energy_l1_export_t0_wh',
:'p7_active_energy_l1_import_t0_wh',
:'p7_active_energy_l2_export_t0_wh',
:'p7_active_energy_l2_import_t0_wh',
:'p7_active_energy_l3_export_t0_wh',
:'p7_active_energy_l3_import_t0_wh',
:'p7_active_energy_total_export_t0_wh',
:'p7_active_energy_total_import_t0_wh',
:'p7_active_energy_total_import_t1_wh',
:'p7_active_energy_total_import_t2_wh',
:'p7_active_power_l1_net_t0_w',
:'p7_active_power_l2_net_t0_w',
:'p7_active_power_l3_net_t0_w',
:'p7_current_l1_any_t0_a',
:'p7_current_l2_any_t0_a',
:'p7_current_l3_any_t0_a',
:'p7_reactive_energy_l1_export_t0_varh',
:'p7_reactive_energy_l1_import_t0_varh',
:'p7_reactive_energy_l2_export_t0_varh',
:'p7_reactive_energy_l2_import_t0_varh',
:'p7_reactive_energy_l3_export_t0_varh',
:'p7_reactive_energy_l3_import_t0_varh',
:'p7_reactive_energy_total_export_t0_varh',
:'p7_reactive_energy_total_import_t0_varh',
:'p7_reactive_power_l1_net_t0_var',
:'p7_reactive_power_l2_net_t0_var',
:'p7_reactive_power_l3_net_t0_var',
:'p7_voltage_l1_any_t0_v',
:'p7_voltage_l2_any_t0_v',
:'p7_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p8_timestamp'::timestamptz,
:'p8_meter_id',
:'p8_measurement_location_id',
:'p8_active_energy_l1_export_t0_wh',
:'p8_active_energy_l1_import_t0_wh',
:'p8_active_energy_l2_export_t0_wh',
:'p8_active_energy_l2_import_t0_wh',
:'p8_active_energy_l3_export_t0_wh',
:'p8_active_energy_l3_import_t0_wh',
:'p8_active_energy_total_export_t0_wh',
:'p8_active_energy_total_import_t0_wh',
:'p8_active_energy_total_import_t1_wh',
:'p8_active_energy_total_import_t2_wh',
:'p8_active_power_l1_net_t0_w',
:'p8_active_power_l2_net_t0_w',
:'p8_active_power_l3_net_t0_w',
:'p8_current_l1_any_t0_a',
:'p8_current_l2_any_t0_a',
:'p8_current_l3_any_t0_a',
:'p8_reactive_energy_l1_export_t0_varh',
:'p8_reactive_energy_l1_import_t0_varh',
:'p8_reactive_energy_l2_export_t0_varh',
:'p8_reactive_energy_l2_import_t0_varh',
:'p8_reactive_energy_l3_export_t0_varh',
:'p8_reactive_energy_l3_import_t0_varh',
:'p8_reactive_energy_total_export_t0_varh',
:'p8_reactive_energy_total_import_t0_varh',
:'p8_reactive_power_l1_net_t0_var',
:'p8_reactive_power_l2_net_t0_var',
:'p8_reactive_power_l3_net_t0_var',
:'p8_voltage_l1_any_t0_v',
:'p8_voltage_l2_any_t0_v',
:'p8_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p9_timestamp'::timestamptz,
:'p9_meter_id',
:'p9_measurement_location_id',
:'p9_active_energy_l1_export_t0_wh',
:'p9_active_energy_l1_import_t0_wh',
:'p9_active_energy_l2_export_t0_wh',
:'p9_active_energy_l2_import_t0_wh',
:'p9_active_energy_l3_export_t0_wh',
:'p9_active_energy_l3_import_t0_wh',
:'p9_active_energy_total_export_t0_wh',
:'p9_active_energy_total_import_t0_wh',
:'p9_active_energy_total_import_t1_wh',
:'p9_active_energy_total_import_t2_wh',
:'p9_active_power_l1_net_t0_w',
:'p9_active_power_l2_net_t0_w',
:'p9_active_power_l3_net_t0_w',
:'p9_current_l1_any_t0_a',
:'p9_current_l2_any_t0_a',
:'p9_current_l3_any_t0_a',
:'p9_reactive_energy_l1_export_t0_varh',
:'p9_reactive_energy_l1_import_t0_varh',
:'p9_reactive_energy_l2_export_t0_varh',
:'p9_reactive_energy_l2_import_t0_varh',
:'p9_reactive_energy_l3_export_t0_varh',
:'p9_reactive_energy_l3_import_t0_varh',
:'p9_reactive_energy_total_export_t0_varh',
:'p9_reactive_energy_total_import_t0_varh',
:'p9_reactive_power_l1_net_t0_var',
:'p9_reactive_power_l2_net_t0_var',
:'p9_reactive_power_l3_net_t0_var',
:'p9_voltage_l1_any_t0_v',
:'p9_voltage_l2_any_t0_v',
:'p9_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p10_timestamp'::timestamptz,
:'p10_meter_id',
:'p10_measurement_location_id',
:'p10_active_energy_l1_export_t0_wh',
:'p10_active_energy_l1_import_t0_wh',
:'p10_active_energy_l2_export_t0_wh',
:'p10_active_energy_l2_import_t0_wh',
:'p10_active_energy_l3_export_t0_wh',
:'p10_active_energy_l3_import_t0_wh',
:'p10_active_energy_total_export_t0_wh',
:'p10_active_energy_total_import_t0_wh',
:'p10_active_energy_total_import_t1_wh',
:'p10_active_energy_total_import_t2_wh',
:'p10_active_power_l1_net_t0_w',
:'p10_active_power_l2_net_t0_w',
:'p10_active_power_l3_net_t0_w',
:'p10_current_l1_any_t0_a',
:'p10_current_l2_any_t0_a',
:'p10_current_l3_any_t0_a',
:'p10_reactive_energy_l1_export_t0_varh',
:'p10_reactive_energy_l1_import_t0_varh',
:'p10_reactive_energy_l2_export_t0_varh',
:'p10_reactive_energy_l2_import_t0_varh',
:'p10_reactive_energy_l3_export_t0_varh',
:'p10_reactive_energy_l3_import_t0_varh',
:'p10_reactive_energy_total_export_t0_varh',
:'p10_reactive_energy_total_import_t0_varh',
:'p10_reactive_power_l1_net_t0_var',
:'p10_reactive_power_l2_net_t0_var',
:'p10_reactive_power_l3_net_t0_var',
:'p10_voltage_l1_any_t0_v',
:'p10_voltage_l2_any_t0_v',
:'p10_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p11_timestamp'::timestamptz,
:'p11_meter_id',
:'p11_measurement_location_id',
:'p11_active_energy_l1_export_t0_wh',
:'p11_active_energy_l1_import_t0_wh',
:'p11_active_energy_l2_export_t0_wh',
:'p11_active_energy_l2_import_t0_wh',
:'p11_active_energy_l3_export_t0_wh',
:'p11_active_energy_l3_import_t0_wh',
:'p11_active_energy_total_export_t0_wh',
:'p11_active_energy_total_import_t0_wh',
:'p11_active_energy_total_import_t1_wh',
:'p11_active_energy_total_import_t2_wh',
:'p11_active_power_l1_net_t0_w',
:'p11_active_power_l2_net_t0_w',
:'p11_active_power_l3_net_t0_w',
:'p11_current_l1_any_t0_a',
:'p11_current_l2_any_t0_a',
:'p11_current_l3_any_t0_a',
:'p11_reactive_energy_l1_export_t0_varh',
:'p11_reactive_energy_l1_import_t0_varh',
:'p11_reactive_energy_l2_export_t0_varh',
:'p11_reactive_energy_l2_import_t0_varh',
:'p11_reactive_energy_l3_export_t0_varh',
:'p11_reactive_energy_l3_import_t0_varh',
:'p11_reactive_energy_total_export_t0_varh',
:'p11_reactive_energy_total_import_t0_varh',
:'p11_reactive_power_l1_net_t0_var',
:'p11_reactive_power_l2_net_t0_var',
:'p11_reactive_power_l3_net_t0_var',
:'p11_voltage_l1_any_t0_v',
:'p11_voltage_l2_any_t0_v',
:'p11_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p12_timestamp'::timestamptz,
:'p12_meter_id',
:'p12_measurement_location_id',
:'p12_active_energy_l1_export_t0_wh',
:'p12_active_energy_l1_import_t0_wh',
:'p12_active_energy_l2_export_t0_wh',
:'p12_active_energy_l2_import_t0_wh',
:'p12_active_energy_l3_export_t0_wh',
:'p12_active_energy_l3_import_t0_wh',
:'p12_active_energy_total_export_t0_wh',
:'p12_active_energy_total_import_t0_wh',
:'p12_active_energy_total_import_t1_wh',
:'p12_active_energy_total_import_t2_wh',
:'p12_active_power_l1_net_t0_w',
:'p12_active_power_l2_net_t0_w',
:'p12_active_power_l3_net_t0_w',
:'p12_current_l1_any_t0_a',
:'p12_current_l2_any_t0_a',
:'p12_current_l3_any_t0_a',
:'p12_reactive_energy_l1_export_t0_varh',
:'p12_reactive_energy_l1_import_t0_varh',
:'p12_reactive_energy_l2_export_t0_varh',
:'p12_reactive_energy_l2_import_t0_varh',
:'p12_reactive_energy_l3_export_t0_varh',
:'p12_reactive_energy_l3_import_t0_varh',
:'p12_reactive_energy_total_export_t0_varh',
:'p12_reactive_energy_total_import_t0_varh',
:'p12_reactive_power_l1_net_t0_var',
:'p12_reactive_power_l2_net_t0_var',
:'p12_reactive_power_l3_net_t0_var',
:'p12_voltage_l1_any_t0_v',
:'p12_voltage_l2_any_t0_v',
:'p12_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p13_timestamp'::timestamptz,
:'p13_meter_id',
:'p13_measurement_location_id',
:'p13_active_energy_l1_export_t0_wh',
:'p13_active_energy_l1_import_t0_wh',
:'p13_active_energy_l2_export_t0_wh',
:'p13_active_energy_l2_import_t0_wh',
:'p13_active_energy_l3_export_t0_wh',
:'p13_active_energy_l3_import_t0_wh',
:'p13_active_energy_total_export_t0_wh',
:'p13_active_energy_total_import_t0_wh',
:'p13_active_energy_total_import_t1_wh',
:'p13_active_energy_total_import_t2_wh',
:'p13_active_power_l1_net_t0_w',
:'p13_active_power_l2_net_t0_w',
:'p13_active_power_l3_net_t0_w',
:'p13_current_l1_any_t0_a',
:'p13_current_l2_any_t0_a',
:'p13_current_l3_any_t0_a',
:'p13_reactive_energy_l1_export_t0_varh',
:'p13_reactive_energy_l1_import_t0_varh',
:'p13_reactive_energy_l2_export_t0_varh',
:'p13_reactive_energy_l2_import_t0_varh',
:'p13_reactive_energy_l3_export_t0_varh',
:'p13_reactive_energy_l3_import_t0_varh',
:'p13_reactive_energy_total_export_t0_varh',
:'p13_reactive_energy_total_import_t0_varh',
:'p13_reactive_power_l1_net_t0_var',
:'p13_reactive_power_l2_net_t0_var',
:'p13_reactive_power_l3_net_t0_var',
:'p13_voltage_l1_any_t0_v',
:'p13_voltage_l2_any_t0_v',
:'p13_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p14_timestamp'::timestamptz,
:'p14_meter_id',
:'p14_measurement_location_id',
:'p14_active_energy_l1_export_t0_wh',
:'p14_active_energy_l1_import_t0_wh',
:'p14_active_energy_l2_export_t0_wh',
:'p14_active_energy_l2_import_t0_wh',
:'p14_active_energy_l3_export_t0_wh',
:'p14_active_energy_l3_import_t0_wh',
:'p14_active_energy_total_export_t0_wh',
:'p14_active_energy_total_import_t0_wh',
:'p14_active_energy_total_import_t1_wh',
:'p14_active_energy_total_import_t2_wh',
:'p14_active_power_l1_net_t0_w',
:'p14_active_power_l2_net_t0_w',
:'p14_active_power_l3_net_t0_w',
:'p14_current_l1_any_t0_a',
:'p14_current_l2_any_t0_a',
:'p14_current_l3_any_t0_a',
:'p14_reactive_energy_l1_export_t0_varh',
:'p14_reactive_energy_l1_import_t0_varh',
:'p14_reactive_energy_l2_export_t0_varh',
:'p14_reactive_energy_l2_import_t0_varh',
:'p14_reactive_energy_l3_export_t0_varh',
:'p14_reactive_energy_l3_import_t0_varh',
:'p14_reactive_energy_total_export_t0_varh',
:'p14_reactive_energy_total_import_t0_varh',
:'p14_reactive_power_l1_net_t0_var',
:'p14_reactive_power_l2_net_t0_var',
:'p14_reactive_power_l3_net_t0_var',
:'p14_voltage_l1_any_t0_v',
:'p14_voltage_l2_any_t0_v',
:'p14_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p15_timestamp'::timestamptz,
:'p15_meter_id',
:'p15_measurement_location_id',
:'p15_active_energy_l1_export_t0_wh',
:'p15_active_energy_l1_import_t0_wh',
:'p15_active_energy_l2_export_t0_wh',
:'p15_active_energy_l2_import_t0_wh',
:'p15_active_energy_l3_export_t0_wh',
:'p15_active_energy_l3_import_t0_wh',
:'p15_active_energy_total_export_t0_wh',
:'p15_active_energy_total_import_t0_wh',
:'p15_active_energy_total_import_t1_wh',
:'p15_active_energy_total_import_t2_wh',
:'p15_active_power_l1_net_t0_w',
:'p15_active_power_l2_net_t0_w',
:'p15_active_power_l3_net_t0_w',
:'p15_current_l1_any_t0_a',
:'p15_current_l2_any_t0_a',
:'p15_current_l3_any_t0_a',
:'p15_reactive_energy_l1_export_t0_varh',
:'p15_reactive_energy_l1_import_t0_varh',
:'p15_reactive_energy_l2_export_t0_varh',
:'p15_reactive_energy_l2_import_t0_varh',
:'p15_reactive_energy_l3_export_t0_varh',
:'p15_reactive_energy_l3_import_t0_varh',
:'p15_reactive_energy_total_export_t0_varh',
:'p15_reactive_energy_total_import_t0_varh',
:'p15_reactive_power_l1_net_t0_var',
:'p15_reactive_power_l2_net_t0_var',
:'p15_reactive_power_l3_net_t0_var',
:'p15_voltage_l1_any_t0_v',
:'p15_voltage_l2_any_t0_v',
:'p15_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p16_timestamp'::timestamptz,
:'p16_meter_id',
:'p16_measurement_location_id',
:'p16_active_energy_l1_export_t0_wh',
:'p16_active_energy_l1_import_t0_wh',
:'p16_active_energy_l2_export_t0_wh',
:'p16_active_energy_l2_import_t0_wh',
:'p16_active_energy_l3_export_t0_wh',
:'p16_active_energy_l3_import_t0_wh',
:'p16_active_energy_total_export_t0_wh',
:'p16_active_energy_total_import_t0_wh',
:'p16_active_energy_total_import_t1_wh',
:'p16_active_energy_total_import_t2_wh',
:'p16_active_power_l1_net_t0_w',
:'p16_active_power_l2_net_t0_w',
:'p16_active_power_l3_net_t0_w',
:'p16_current_l1_any_t0_a',
:'p16_current_l2_any_t0_a',
:'p16_current_l3_any_t0_a',
:'p16_reactive_energy_l1_export_t0_varh',
:'p16_reactive_energy_l1_import_t0_varh',
:'p16_reactive_energy_l2_export_t0_varh',
:'p16_reactive_energy_l2_import_t0_varh',
:'p16_reactive_energy_l3_export_t0_varh',
:'p16_reactive_energy_l3_import_t0_varh',
:'p16_reactive_energy_total_export_t0_varh',
:'p16_reactive_energy_total_import_t0_varh',
:'p16_reactive_power_l1_net_t0_var',
:'p16_reactive_power_l2_net_t0_var',
:'p16_reactive_power_l3_net_t0_var',
:'p16_voltage_l1_any_t0_v',
:'p16_voltage_l2_any_t0_v',
:'p16_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p17_timestamp'::timestamptz,
:'p17_meter_id',
:'p17_measurement_location_id',
:'p17_active_energy_l1_export_t0_wh',
:'p17_active_energy_l1_import_t0_wh',
:'p17_active_energy_l2_export_t0_wh',
:'p17_active_energy_l2_import_t0_wh',
:'p17_active_energy_l3_export_t0_wh',
:'p17_active_energy_l3_import_t0_wh',
:'p17_active_energy_total_export_t0_wh',
:'p17_active_energy_total_import_t0_wh',
:'p17_active_energy_total_import_t1_wh',
:'p17_active_energy_total_import_t2_wh',
:'p17_active_power_l1_net_t0_w',
:'p17_active_power_l2_net_t0_w',
:'p17_active_power_l3_net_t0_w',
:'p17_current_l1_any_t0_a',
:'p17_current_l2_any_t0_a',
:'p17_current_l3_any_t0_a',
:'p17_reactive_energy_l1_export_t0_varh',
:'p17_reactive_energy_l1_import_t0_varh',
:'p17_reactive_energy_l2_export_t0_varh',
:'p17_reactive_energy_l2_import_t0_varh',
:'p17_reactive_energy_l3_export_t0_varh',
:'p17_reactive_energy_l3_import_t0_varh',
:'p17_reactive_energy_total_export_t0_varh',
:'p17_reactive_energy_total_import_t0_varh',
:'p17_reactive_power_l1_net_t0_var',
:'p17_reactive_power_l2_net_t0_var',
:'p17_reactive_power_l3_net_t0_var',
:'p17_voltage_l1_any_t0_v',
:'p17_voltage_l2_any_t0_v',
:'p17_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p18_timestamp'::timestamptz,
:'p18_meter_id',
:'p18_measurement_location_id',
:'p18_active_energy_l1_export_t0_wh',
:'p18_active_energy_l1_import_t0_wh',
:'p18_active_energy_l2_export_t0_wh',
:'p18_active_energy_l2_import_t0_wh',
:'p18_active_energy_l3_export_t0_wh',
:'p18_active_energy_l3_import_t0_wh',
:'p18_active_energy_total_export_t0_wh',
:'p18_active_energy_total_import_t0_wh',
:'p18_active_energy_total_import_t1_wh',
:'p18_active_energy_total_import_t2_wh',
:'p18_active_power_l1_net_t0_w',
:'p18_active_power_l2_net_t0_w',
:'p18_active_power_l3_net_t0_w',
:'p18_current_l1_any_t0_a',
:'p18_current_l2_any_t0_a',
:'p18_current_l3_any_t0_a',
:'p18_reactive_energy_l1_export_t0_varh',
:'p18_reactive_energy_l1_import_t0_varh',
:'p18_reactive_energy_l2_export_t0_varh',
:'p18_reactive_energy_l2_import_t0_varh',
:'p18_reactive_energy_l3_export_t0_varh',
:'p18_reactive_energy_l3_import_t0_varh',
:'p18_reactive_energy_total_export_t0_varh',
:'p18_reactive_energy_total_import_t0_varh',
:'p18_reactive_power_l1_net_t0_var',
:'p18_reactive_power_l2_net_t0_var',
:'p18_reactive_power_l3_net_t0_var',
:'p18_voltage_l1_any_t0_v',
:'p18_voltage_l2_any_t0_v',
:'p18_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p19_timestamp'::timestamptz,
:'p19_meter_id',
:'p19_measurement_location_id',
:'p19_active_energy_l1_export_t0_wh',
:'p19_active_energy_l1_import_t0_wh',
:'p19_active_energy_l2_export_t0_wh',
:'p19_active_energy_l2_import_t0_wh',
:'p19_active_energy_l3_export_t0_wh',
:'p19_active_energy_l3_import_t0_wh',
:'p19_active_energy_total_export_t0_wh',
:'p19_active_energy_total_import_t0_wh',
:'p19_active_energy_total_import_t1_wh',
:'p19_active_energy_total_import_t2_wh',
:'p19_active_power_l1_net_t0_w',
:'p19_active_power_l2_net_t0_w',
:'p19_active_power_l3_net_t0_w',
:'p19_current_l1_any_t0_a',
:'p19_current_l2_any_t0_a',
:'p19_current_l3_any_t0_a',
:'p19_reactive_energy_l1_export_t0_varh',
:'p19_reactive_energy_l1_import_t0_varh',
:'p19_reactive_energy_l2_export_t0_varh',
:'p19_reactive_energy_l2_import_t0_varh',
:'p19_reactive_energy_l3_export_t0_varh',
:'p19_reactive_energy_l3_import_t0_varh',
:'p19_reactive_energy_total_export_t0_varh',
:'p19_reactive_energy_total_import_t0_varh',
:'p19_reactive_power_l1_net_t0_var',
:'p19_reactive_power_l2_net_t0_var',
:'p19_reactive_power_l3_net_t0_var',
:'p19_voltage_l1_any_t0_v',
:'p19_voltage_l2_any_t0_v',
:'p19_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  abb_b2x_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_export_t0_wh,
    active_energy_l1_import_t0_wh,
    active_energy_l2_export_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_export_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_l1_export_t0_varh,
    reactive_energy_l1_import_t0_varh,
    reactive_energy_l2_export_t0_varh,
    reactive_energy_l2_import_t0_varh,
    reactive_energy_l3_export_t0_varh,
    reactive_energy_l3_import_t0_varh,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_l1_net_t0_var,
    reactive_power_l2_net_t0_var,
    reactive_power_l3_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p20_timestamp'::timestamptz,
:'p20_meter_id',
:'p20_measurement_location_id',
:'p20_active_energy_l1_export_t0_wh',
:'p20_active_energy_l1_import_t0_wh',
:'p20_active_energy_l2_export_t0_wh',
:'p20_active_energy_l2_import_t0_wh',
:'p20_active_energy_l3_export_t0_wh',
:'p20_active_energy_l3_import_t0_wh',
:'p20_active_energy_total_export_t0_wh',
:'p20_active_energy_total_import_t0_wh',
:'p20_active_energy_total_import_t1_wh',
:'p20_active_energy_total_import_t2_wh',
:'p20_active_power_l1_net_t0_w',
:'p20_active_power_l2_net_t0_w',
:'p20_active_power_l3_net_t0_w',
:'p20_current_l1_any_t0_a',
:'p20_current_l2_any_t0_a',
:'p20_current_l3_any_t0_a',
:'p20_reactive_energy_l1_export_t0_varh',
:'p20_reactive_energy_l1_import_t0_varh',
:'p20_reactive_energy_l2_export_t0_varh',
:'p20_reactive_energy_l2_import_t0_varh',
:'p20_reactive_energy_l3_export_t0_varh',
:'p20_reactive_energy_l3_import_t0_varh',
:'p20_reactive_energy_total_export_t0_varh',
:'p20_reactive_energy_total_import_t0_varh',
:'p20_reactive_power_l1_net_t0_var',
:'p20_reactive_power_l2_net_t0_var',
:'p20_reactive_power_l3_net_t0_var',
:'p20_voltage_l1_any_t0_v',
:'p20_voltage_l2_any_t0_v',
:'p20_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p21_timestamp'::timestamptz,
:'p21_meter_id',
:'p21_measurement_location_id',
:'p21_active_energy_l1_import_t0_wh',
:'p21_active_energy_l2_import_t0_wh',
:'p21_active_energy_l3_import_t0_wh',
:'p21_active_energy_total_export_t0_wh',
:'p21_active_energy_total_import_t0_wh',
:'p21_active_energy_total_import_t1_wh',
:'p21_active_energy_total_import_t2_wh',
:'p21_active_power_l1_net_t0_w',
:'p21_active_power_l2_net_t0_w',
:'p21_active_power_l3_net_t0_w',
:'p21_apparent_power_total_net_t0_va',
:'p21_current_l1_any_t0_a',
:'p21_current_l2_any_t0_a',
:'p21_current_l3_any_t0_a',
:'p21_reactive_energy_total_export_t0_varh',
:'p21_reactive_energy_total_import_t0_varh',
:'p21_reactive_power_total_net_t0_var',
:'p21_voltage_l1_any_t0_v',
:'p21_voltage_l2_any_t0_v',
:'p21_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p22_timestamp'::timestamptz,
:'p22_meter_id',
:'p22_measurement_location_id',
:'p22_active_energy_l1_import_t0_wh',
:'p22_active_energy_l2_import_t0_wh',
:'p22_active_energy_l3_import_t0_wh',
:'p22_active_energy_total_export_t0_wh',
:'p22_active_energy_total_import_t0_wh',
:'p22_active_energy_total_import_t1_wh',
:'p22_active_energy_total_import_t2_wh',
:'p22_active_power_l1_net_t0_w',
:'p22_active_power_l2_net_t0_w',
:'p22_active_power_l3_net_t0_w',
:'p22_apparent_power_total_net_t0_va',
:'p22_current_l1_any_t0_a',
:'p22_current_l2_any_t0_a',
:'p22_current_l3_any_t0_a',
:'p22_reactive_energy_total_export_t0_varh',
:'p22_reactive_energy_total_import_t0_varh',
:'p22_reactive_power_total_net_t0_var',
:'p22_voltage_l1_any_t0_v',
:'p22_voltage_l2_any_t0_v',
:'p22_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p23_timestamp'::timestamptz,
:'p23_meter_id',
:'p23_measurement_location_id',
:'p23_active_energy_l1_import_t0_wh',
:'p23_active_energy_l2_import_t0_wh',
:'p23_active_energy_l3_import_t0_wh',
:'p23_active_energy_total_export_t0_wh',
:'p23_active_energy_total_import_t0_wh',
:'p23_active_energy_total_import_t1_wh',
:'p23_active_energy_total_import_t2_wh',
:'p23_active_power_l1_net_t0_w',
:'p23_active_power_l2_net_t0_w',
:'p23_active_power_l3_net_t0_w',
:'p23_apparent_power_total_net_t0_va',
:'p23_current_l1_any_t0_a',
:'p23_current_l2_any_t0_a',
:'p23_current_l3_any_t0_a',
:'p23_reactive_energy_total_export_t0_varh',
:'p23_reactive_energy_total_import_t0_varh',
:'p23_reactive_power_total_net_t0_var',
:'p23_voltage_l1_any_t0_v',
:'p23_voltage_l2_any_t0_v',
:'p23_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p24_timestamp'::timestamptz,
:'p24_meter_id',
:'p24_measurement_location_id',
:'p24_active_energy_l1_import_t0_wh',
:'p24_active_energy_l2_import_t0_wh',
:'p24_active_energy_l3_import_t0_wh',
:'p24_active_energy_total_export_t0_wh',
:'p24_active_energy_total_import_t0_wh',
:'p24_active_energy_total_import_t1_wh',
:'p24_active_energy_total_import_t2_wh',
:'p24_active_power_l1_net_t0_w',
:'p24_active_power_l2_net_t0_w',
:'p24_active_power_l3_net_t0_w',
:'p24_apparent_power_total_net_t0_va',
:'p24_current_l1_any_t0_a',
:'p24_current_l2_any_t0_a',
:'p24_current_l3_any_t0_a',
:'p24_reactive_energy_total_export_t0_varh',
:'p24_reactive_energy_total_import_t0_varh',
:'p24_reactive_power_total_net_t0_var',
:'p24_voltage_l1_any_t0_v',
:'p24_voltage_l2_any_t0_v',
:'p24_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p25_timestamp'::timestamptz,
:'p25_meter_id',
:'p25_measurement_location_id',
:'p25_active_energy_l1_import_t0_wh',
:'p25_active_energy_l2_import_t0_wh',
:'p25_active_energy_l3_import_t0_wh',
:'p25_active_energy_total_export_t0_wh',
:'p25_active_energy_total_import_t0_wh',
:'p25_active_energy_total_import_t1_wh',
:'p25_active_energy_total_import_t2_wh',
:'p25_active_power_l1_net_t0_w',
:'p25_active_power_l2_net_t0_w',
:'p25_active_power_l3_net_t0_w',
:'p25_apparent_power_total_net_t0_va',
:'p25_current_l1_any_t0_a',
:'p25_current_l2_any_t0_a',
:'p25_current_l3_any_t0_a',
:'p25_reactive_energy_total_export_t0_varh',
:'p25_reactive_energy_total_import_t0_varh',
:'p25_reactive_power_total_net_t0_var',
:'p25_voltage_l1_any_t0_v',
:'p25_voltage_l2_any_t0_v',
:'p25_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p26_timestamp'::timestamptz,
:'p26_meter_id',
:'p26_measurement_location_id',
:'p26_active_energy_l1_import_t0_wh',
:'p26_active_energy_l2_import_t0_wh',
:'p26_active_energy_l3_import_t0_wh',
:'p26_active_energy_total_export_t0_wh',
:'p26_active_energy_total_import_t0_wh',
:'p26_active_energy_total_import_t1_wh',
:'p26_active_energy_total_import_t2_wh',
:'p26_active_power_l1_net_t0_w',
:'p26_active_power_l2_net_t0_w',
:'p26_active_power_l3_net_t0_w',
:'p26_apparent_power_total_net_t0_va',
:'p26_current_l1_any_t0_a',
:'p26_current_l2_any_t0_a',
:'p26_current_l3_any_t0_a',
:'p26_reactive_energy_total_export_t0_varh',
:'p26_reactive_energy_total_import_t0_varh',
:'p26_reactive_power_total_net_t0_var',
:'p26_voltage_l1_any_t0_v',
:'p26_voltage_l2_any_t0_v',
:'p26_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

insert into
  schneider_iem3xxx_measurements (
    timestamp,
    meter_id,
    measurement_location_id,
    active_energy_l1_import_t0_wh,
    active_energy_l2_import_t0_wh,
    active_energy_l3_import_t0_wh,
    active_energy_total_export_t0_wh,
    active_energy_total_import_t0_wh,
    active_energy_total_import_t1_wh,
    active_energy_total_import_t2_wh,
    active_power_l1_net_t0_w,
    active_power_l2_net_t0_w,
    active_power_l3_net_t0_w,
    apparent_power_total_net_t0_va,
    current_l1_any_t0_a,
    current_l2_any_t0_a,
    current_l3_any_t0_a,
    reactive_energy_total_export_t0_varh,
    reactive_energy_total_import_t0_varh,
    reactive_power_total_net_t0_var,
    voltage_l1_any_t0_v,
    voltage_l2_any_t0_v,
    voltage_l3_any_t0_v
  )
values
  (
:'p27_timestamp'::timestamptz,
:'p27_meter_id',
:'p27_measurement_location_id',
:'p27_active_energy_l1_import_t0_wh',
:'p27_active_energy_l2_import_t0_wh',
:'p27_active_energy_l3_import_t0_wh',
:'p27_active_energy_total_export_t0_wh',
:'p27_active_energy_total_import_t0_wh',
:'p27_active_energy_total_import_t1_wh',
:'p27_active_energy_total_import_t2_wh',
:'p27_active_power_l1_net_t0_w',
:'p27_active_power_l2_net_t0_w',
:'p27_active_power_l3_net_t0_w',
:'p27_apparent_power_total_net_t0_va',
:'p27_current_l1_any_t0_a',
:'p27_current_l2_any_t0_a',
:'p27_current_l3_any_t0_a',
:'p27_reactive_energy_total_export_t0_varh',
:'p27_reactive_energy_total_import_t0_varh',
:'p27_reactive_power_total_net_t0_var',
:'p27_voltage_l1_any_t0_v',
:'p27_voltage_l2_any_t0_v',
:'p27_voltage_l3_any_t0_v'
  )
on conflict (timestamp, meter_id, measurement_location_id) do NOTHING;

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
:'p33_timestamp'::timestamptz,
:'p33_interval'::interval_entity,
:'p33_meter_id',
:'p33_measurement_location_id',
:'p33_count',
:'p33_quarter_hour_count',
:'p33_active_energy_l1_export_t0_max_wh',
:'p33_active_energy_l1_export_t0_min_wh',
:'p33_active_energy_l1_import_t0_max_wh',
:'p33_active_energy_l1_import_t0_min_wh',
:'p33_active_energy_l2_export_t0_max_wh',
:'p33_active_energy_l2_export_t0_min_wh',
:'p33_active_energy_l2_import_t0_max_wh',
:'p33_active_energy_l2_import_t0_min_wh',
:'p33_active_energy_l3_export_t0_max_wh',
:'p33_active_energy_l3_export_t0_min_wh',
:'p33_active_energy_l3_import_t0_max_wh',
:'p33_active_energy_l3_import_t0_min_wh',
:'p33_active_energy_total_export_t0_max_wh',
:'p33_active_energy_total_export_t0_min_wh',
:'p33_active_energy_total_import_t0_max_wh',
:'p33_active_energy_total_import_t0_min_wh',
:'p33_active_energy_total_import_t1_max_wh',
:'p33_active_energy_total_import_t1_min_wh',
:'p33_active_energy_total_import_t2_max_wh',
:'p33_active_energy_total_import_t2_min_wh',
:'p33_active_power_l1_net_t0_avg_w',
:'p33_active_power_l1_net_t0_max_w',
:'p33_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p33_active_power_l1_net_t0_min_w',
:'p33_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p33_active_power_l2_net_t0_avg_w',
:'p33_active_power_l2_net_t0_max_w',
:'p33_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p33_active_power_l2_net_t0_min_w',
:'p33_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p33_active_power_l3_net_t0_avg_w',
:'p33_active_power_l3_net_t0_max_w',
:'p33_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p33_active_power_l3_net_t0_min_w',
:'p33_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p33_current_l1_any_t0_avg_a',
:'p33_current_l1_any_t0_max_a',
:'p33_current_l1_any_t0_max_timestamp'::timestamptz,
:'p33_current_l1_any_t0_min_a',
:'p33_current_l1_any_t0_min_timestamp'::timestamptz,
:'p33_current_l2_any_t0_avg_a',
:'p33_current_l2_any_t0_max_a',
:'p33_current_l2_any_t0_max_timestamp'::timestamptz,
:'p33_current_l2_any_t0_min_a',
:'p33_current_l2_any_t0_min_timestamp'::timestamptz,
:'p33_current_l3_any_t0_avg_a',
:'p33_current_l3_any_t0_max_a',
:'p33_current_l3_any_t0_max_timestamp'::timestamptz,
:'p33_current_l3_any_t0_min_a',
:'p33_current_l3_any_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_l1_export_t0_avg_w',
:'p33_derived_active_power_l1_export_t0_max_w',
:'p33_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_l1_export_t0_min_w',
:'p33_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_l1_import_t0_avg_w',
:'p33_derived_active_power_l1_import_t0_max_w',
:'p33_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_l1_import_t0_min_w',
:'p33_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_l2_export_t0_avg_w',
:'p33_derived_active_power_l2_export_t0_max_w',
:'p33_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_l2_export_t0_min_w',
:'p33_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_l2_import_t0_avg_w',
:'p33_derived_active_power_l2_import_t0_max_w',
:'p33_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_l2_import_t0_min_w',
:'p33_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_l3_export_t0_avg_w',
:'p33_derived_active_power_l3_export_t0_max_w',
:'p33_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_l3_export_t0_min_w',
:'p33_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_l3_import_t0_avg_w',
:'p33_derived_active_power_l3_import_t0_max_w',
:'p33_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_l3_import_t0_min_w',
:'p33_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_total_export_t0_avg_w',
:'p33_derived_active_power_total_export_t0_max_w',
:'p33_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_total_export_t0_min_w',
:'p33_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_total_import_t0_avg_w',
:'p33_derived_active_power_total_import_t0_max_w',
:'p33_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p33_derived_active_power_total_import_t0_min_w',
:'p33_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p33_derived_active_power_total_import_t1_avg_w',
:'p33_derived_active_power_total_import_t1_max_w',
:'p33_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p33_derived_active_power_total_import_t1_min_w',
:'p33_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p33_derived_active_power_total_import_t2_avg_w',
:'p33_derived_active_power_total_import_t2_max_w',
:'p33_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p33_derived_active_power_total_import_t2_min_w',
:'p33_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_l1_export_t0_avg_var',
:'p33_derived_reactive_power_l1_export_t0_max_var',
:'p33_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_l1_export_t0_min_var',
:'p33_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_l1_import_t0_avg_var',
:'p33_derived_reactive_power_l1_import_t0_max_var',
:'p33_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_l1_import_t0_min_var',
:'p33_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_l2_export_t0_avg_var',
:'p33_derived_reactive_power_l2_export_t0_max_var',
:'p33_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_l2_export_t0_min_var',
:'p33_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_l2_import_t0_avg_var',
:'p33_derived_reactive_power_l2_import_t0_max_var',
:'p33_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_l2_import_t0_min_var',
:'p33_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_l3_export_t0_avg_var',
:'p33_derived_reactive_power_l3_export_t0_max_var',
:'p33_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_l3_export_t0_min_var',
:'p33_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_l3_import_t0_avg_var',
:'p33_derived_reactive_power_l3_import_t0_max_var',
:'p33_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_l3_import_t0_min_var',
:'p33_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_total_export_t0_avg_var',
:'p33_derived_reactive_power_total_export_t0_max_var',
:'p33_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_total_export_t0_min_var',
:'p33_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p33_derived_reactive_power_total_import_t0_avg_var',
:'p33_derived_reactive_power_total_import_t0_max_var',
:'p33_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p33_derived_reactive_power_total_import_t0_min_var',
:'p33_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p33_reactive_energy_l1_export_t0_max_varh',
:'p33_reactive_energy_l1_export_t0_min_varh',
:'p33_reactive_energy_l1_import_t0_max_varh',
:'p33_reactive_energy_l1_import_t0_min_varh',
:'p33_reactive_energy_l2_export_t0_max_varh',
:'p33_reactive_energy_l2_export_t0_min_varh',
:'p33_reactive_energy_l2_import_t0_max_varh',
:'p33_reactive_energy_l2_import_t0_min_varh',
:'p33_reactive_energy_l3_export_t0_max_varh',
:'p33_reactive_energy_l3_export_t0_min_varh',
:'p33_reactive_energy_l3_import_t0_max_varh',
:'p33_reactive_energy_l3_import_t0_min_varh',
:'p33_reactive_energy_total_export_t0_max_varh',
:'p33_reactive_energy_total_export_t0_min_varh',
:'p33_reactive_energy_total_import_t0_max_varh',
:'p33_reactive_energy_total_import_t0_min_varh',
:'p33_reactive_power_l1_net_t0_avg_var',
:'p33_reactive_power_l1_net_t0_max_var',
:'p33_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p33_reactive_power_l1_net_t0_min_var',
:'p33_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p33_reactive_power_l2_net_t0_avg_var',
:'p33_reactive_power_l2_net_t0_max_var',
:'p33_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p33_reactive_power_l2_net_t0_min_var',
:'p33_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p33_reactive_power_l3_net_t0_avg_var',
:'p33_reactive_power_l3_net_t0_max_var',
:'p33_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p33_reactive_power_l3_net_t0_min_var',
:'p33_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p33_voltage_l1_any_t0_avg_v',
:'p33_voltage_l1_any_t0_max_v',
:'p33_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p33_voltage_l1_any_t0_min_v',
:'p33_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p33_voltage_l2_any_t0_avg_v',
:'p33_voltage_l2_any_t0_max_v',
:'p33_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p33_voltage_l2_any_t0_min_v',
:'p33_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p33_voltage_l3_any_t0_avg_v',
:'p33_voltage_l3_any_t0_max_v',
:'p33_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p33_voltage_l3_any_t0_min_v',
:'p33_voltage_l3_any_t0_min_timestamp'::timestamptz
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
:'p36_timestamp'::timestamptz,
:'p36_interval'::interval_entity,
:'p36_meter_id',
:'p36_measurement_location_id',
:'p36_count',
:'p36_quarter_hour_count',
:'p36_active_energy_l1_export_t0_max_wh',
:'p36_active_energy_l1_export_t0_min_wh',
:'p36_active_energy_l1_import_t0_max_wh',
:'p36_active_energy_l1_import_t0_min_wh',
:'p36_active_energy_l2_export_t0_max_wh',
:'p36_active_energy_l2_export_t0_min_wh',
:'p36_active_energy_l2_import_t0_max_wh',
:'p36_active_energy_l2_import_t0_min_wh',
:'p36_active_energy_l3_export_t0_max_wh',
:'p36_active_energy_l3_export_t0_min_wh',
:'p36_active_energy_l3_import_t0_max_wh',
:'p36_active_energy_l3_import_t0_min_wh',
:'p36_active_energy_total_export_t0_max_wh',
:'p36_active_energy_total_export_t0_min_wh',
:'p36_active_energy_total_import_t0_max_wh',
:'p36_active_energy_total_import_t0_min_wh',
:'p36_active_energy_total_import_t1_max_wh',
:'p36_active_energy_total_import_t1_min_wh',
:'p36_active_energy_total_import_t2_max_wh',
:'p36_active_energy_total_import_t2_min_wh',
:'p36_active_power_l1_net_t0_avg_w',
:'p36_active_power_l1_net_t0_max_w',
:'p36_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p36_active_power_l1_net_t0_min_w',
:'p36_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p36_active_power_l2_net_t0_avg_w',
:'p36_active_power_l2_net_t0_max_w',
:'p36_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p36_active_power_l2_net_t0_min_w',
:'p36_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p36_active_power_l3_net_t0_avg_w',
:'p36_active_power_l3_net_t0_max_w',
:'p36_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p36_active_power_l3_net_t0_min_w',
:'p36_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p36_current_l1_any_t0_avg_a',
:'p36_current_l1_any_t0_max_a',
:'p36_current_l1_any_t0_max_timestamp'::timestamptz,
:'p36_current_l1_any_t0_min_a',
:'p36_current_l1_any_t0_min_timestamp'::timestamptz,
:'p36_current_l2_any_t0_avg_a',
:'p36_current_l2_any_t0_max_a',
:'p36_current_l2_any_t0_max_timestamp'::timestamptz,
:'p36_current_l2_any_t0_min_a',
:'p36_current_l2_any_t0_min_timestamp'::timestamptz,
:'p36_current_l3_any_t0_avg_a',
:'p36_current_l3_any_t0_max_a',
:'p36_current_l3_any_t0_max_timestamp'::timestamptz,
:'p36_current_l3_any_t0_min_a',
:'p36_current_l3_any_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_l1_export_t0_avg_w',
:'p36_derived_active_power_l1_export_t0_max_w',
:'p36_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_l1_export_t0_min_w',
:'p36_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_l1_import_t0_avg_w',
:'p36_derived_active_power_l1_import_t0_max_w',
:'p36_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_l1_import_t0_min_w',
:'p36_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_l2_export_t0_avg_w',
:'p36_derived_active_power_l2_export_t0_max_w',
:'p36_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_l2_export_t0_min_w',
:'p36_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_l2_import_t0_avg_w',
:'p36_derived_active_power_l2_import_t0_max_w',
:'p36_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_l2_import_t0_min_w',
:'p36_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_l3_export_t0_avg_w',
:'p36_derived_active_power_l3_export_t0_max_w',
:'p36_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_l3_export_t0_min_w',
:'p36_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_l3_import_t0_avg_w',
:'p36_derived_active_power_l3_import_t0_max_w',
:'p36_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_l3_import_t0_min_w',
:'p36_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_total_export_t0_avg_w',
:'p36_derived_active_power_total_export_t0_max_w',
:'p36_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_total_export_t0_min_w',
:'p36_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_total_import_t0_avg_w',
:'p36_derived_active_power_total_import_t0_max_w',
:'p36_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p36_derived_active_power_total_import_t0_min_w',
:'p36_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p36_derived_active_power_total_import_t1_avg_w',
:'p36_derived_active_power_total_import_t1_max_w',
:'p36_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p36_derived_active_power_total_import_t1_min_w',
:'p36_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p36_derived_active_power_total_import_t2_avg_w',
:'p36_derived_active_power_total_import_t2_max_w',
:'p36_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p36_derived_active_power_total_import_t2_min_w',
:'p36_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_l1_export_t0_avg_var',
:'p36_derived_reactive_power_l1_export_t0_max_var',
:'p36_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_l1_export_t0_min_var',
:'p36_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_l1_import_t0_avg_var',
:'p36_derived_reactive_power_l1_import_t0_max_var',
:'p36_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_l1_import_t0_min_var',
:'p36_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_l2_export_t0_avg_var',
:'p36_derived_reactive_power_l2_export_t0_max_var',
:'p36_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_l2_export_t0_min_var',
:'p36_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_l2_import_t0_avg_var',
:'p36_derived_reactive_power_l2_import_t0_max_var',
:'p36_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_l2_import_t0_min_var',
:'p36_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_l3_export_t0_avg_var',
:'p36_derived_reactive_power_l3_export_t0_max_var',
:'p36_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_l3_export_t0_min_var',
:'p36_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_l3_import_t0_avg_var',
:'p36_derived_reactive_power_l3_import_t0_max_var',
:'p36_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_l3_import_t0_min_var',
:'p36_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_total_export_t0_avg_var',
:'p36_derived_reactive_power_total_export_t0_max_var',
:'p36_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_total_export_t0_min_var',
:'p36_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p36_derived_reactive_power_total_import_t0_avg_var',
:'p36_derived_reactive_power_total_import_t0_max_var',
:'p36_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p36_derived_reactive_power_total_import_t0_min_var',
:'p36_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p36_reactive_energy_l1_export_t0_max_varh',
:'p36_reactive_energy_l1_export_t0_min_varh',
:'p36_reactive_energy_l1_import_t0_max_varh',
:'p36_reactive_energy_l1_import_t0_min_varh',
:'p36_reactive_energy_l2_export_t0_max_varh',
:'p36_reactive_energy_l2_export_t0_min_varh',
:'p36_reactive_energy_l2_import_t0_max_varh',
:'p36_reactive_energy_l2_import_t0_min_varh',
:'p36_reactive_energy_l3_export_t0_max_varh',
:'p36_reactive_energy_l3_export_t0_min_varh',
:'p36_reactive_energy_l3_import_t0_max_varh',
:'p36_reactive_energy_l3_import_t0_min_varh',
:'p36_reactive_energy_total_export_t0_max_varh',
:'p36_reactive_energy_total_export_t0_min_varh',
:'p36_reactive_energy_total_import_t0_max_varh',
:'p36_reactive_energy_total_import_t0_min_varh',
:'p36_reactive_power_l1_net_t0_avg_var',
:'p36_reactive_power_l1_net_t0_max_var',
:'p36_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p36_reactive_power_l1_net_t0_min_var',
:'p36_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p36_reactive_power_l2_net_t0_avg_var',
:'p36_reactive_power_l2_net_t0_max_var',
:'p36_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p36_reactive_power_l2_net_t0_min_var',
:'p36_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p36_reactive_power_l3_net_t0_avg_var',
:'p36_reactive_power_l3_net_t0_max_var',
:'p36_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p36_reactive_power_l3_net_t0_min_var',
:'p36_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p36_voltage_l1_any_t0_avg_v',
:'p36_voltage_l1_any_t0_max_v',
:'p36_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p36_voltage_l1_any_t0_min_v',
:'p36_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p36_voltage_l2_any_t0_avg_v',
:'p36_voltage_l2_any_t0_max_v',
:'p36_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p36_voltage_l2_any_t0_min_v',
:'p36_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p36_voltage_l3_any_t0_avg_v',
:'p36_voltage_l3_any_t0_max_v',
:'p36_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p36_voltage_l3_any_t0_min_v',
:'p36_voltage_l3_any_t0_min_timestamp'::timestamptz
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
  schneider_iem3xxx_aggregates (
    timestamp,
    interval,
    meter_id,
    measurement_location_id,
    count,
    quarter_hour_count,
    active_energy_l1_import_t0_max_wh,
    active_energy_l1_import_t0_min_wh,
    active_energy_l2_import_t0_max_wh,
    active_energy_l2_import_t0_min_wh,
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
    apparent_power_total_net_t0_avg_va,
    apparent_power_total_net_t0_max_va,
    apparent_power_total_net_t0_max_timestamp,
    apparent_power_total_net_t0_min_va,
    apparent_power_total_net_t0_min_timestamp,
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
    derived_active_power_l1_import_t0_avg_w,
    derived_active_power_l1_import_t0_max_w,
    derived_active_power_l1_import_t0_max_timestamp,
    derived_active_power_l1_import_t0_min_w,
    derived_active_power_l1_import_t0_min_timestamp,
    derived_active_power_l2_import_t0_avg_w,
    derived_active_power_l2_import_t0_max_w,
    derived_active_power_l2_import_t0_max_timestamp,
    derived_active_power_l2_import_t0_min_w,
    derived_active_power_l2_import_t0_min_timestamp,
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
    reactive_energy_total_export_t0_max_varh,
    reactive_energy_total_export_t0_min_varh,
    reactive_energy_total_import_t0_max_varh,
    reactive_energy_total_import_t0_min_varh,
    reactive_power_total_net_t0_avg_var,
    reactive_power_total_net_t0_max_var,
    reactive_power_total_net_t0_max_timestamp,
    reactive_power_total_net_t0_min_var,
    reactive_power_total_net_t0_min_timestamp,
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
:'p39_timestamp'::timestamptz,
:'p39_interval'::interval_entity,
:'p39_meter_id',
:'p39_measurement_location_id',
:'p39_count',
:'p39_quarter_hour_count',
:'p39_active_energy_l1_import_t0_max_wh',
:'p39_active_energy_l1_import_t0_min_wh',
:'p39_active_energy_l2_import_t0_max_wh',
:'p39_active_energy_l2_import_t0_min_wh',
:'p39_active_energy_l3_import_t0_max_wh',
:'p39_active_energy_l3_import_t0_min_wh',
:'p39_active_energy_total_export_t0_max_wh',
:'p39_active_energy_total_export_t0_min_wh',
:'p39_active_energy_total_import_t0_max_wh',
:'p39_active_energy_total_import_t0_min_wh',
:'p39_active_energy_total_import_t1_max_wh',
:'p39_active_energy_total_import_t1_min_wh',
:'p39_active_energy_total_import_t2_max_wh',
:'p39_active_energy_total_import_t2_min_wh',
:'p39_active_power_l1_net_t0_avg_w',
:'p39_active_power_l1_net_t0_max_w',
:'p39_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p39_active_power_l1_net_t0_min_w',
:'p39_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p39_active_power_l2_net_t0_avg_w',
:'p39_active_power_l2_net_t0_max_w',
:'p39_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p39_active_power_l2_net_t0_min_w',
:'p39_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p39_active_power_l3_net_t0_avg_w',
:'p39_active_power_l3_net_t0_max_w',
:'p39_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p39_active_power_l3_net_t0_min_w',
:'p39_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p39_apparent_power_total_net_t0_avg_va',
:'p39_apparent_power_total_net_t0_max_va',
:'p39_apparent_power_total_net_t0_max_timestamp'::timestamptz,
:'p39_apparent_power_total_net_t0_min_va',
:'p39_apparent_power_total_net_t0_min_timestamp'::timestamptz,
:'p39_current_l1_any_t0_avg_a',
:'p39_current_l1_any_t0_max_a',
:'p39_current_l1_any_t0_max_timestamp'::timestamptz,
:'p39_current_l1_any_t0_min_a',
:'p39_current_l1_any_t0_min_timestamp'::timestamptz,
:'p39_current_l2_any_t0_avg_a',
:'p39_current_l2_any_t0_max_a',
:'p39_current_l2_any_t0_max_timestamp'::timestamptz,
:'p39_current_l2_any_t0_min_a',
:'p39_current_l2_any_t0_min_timestamp'::timestamptz,
:'p39_current_l3_any_t0_avg_a',
:'p39_current_l3_any_t0_max_a',
:'p39_current_l3_any_t0_max_timestamp'::timestamptz,
:'p39_current_l3_any_t0_min_a',
:'p39_current_l3_any_t0_min_timestamp'::timestamptz,
:'p39_derived_active_power_l1_import_t0_avg_w',
:'p39_derived_active_power_l1_import_t0_max_w',
:'p39_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p39_derived_active_power_l1_import_t0_min_w',
:'p39_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p39_derived_active_power_l2_import_t0_avg_w',
:'p39_derived_active_power_l2_import_t0_max_w',
:'p39_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p39_derived_active_power_l2_import_t0_min_w',
:'p39_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p39_derived_active_power_l3_import_t0_avg_w',
:'p39_derived_active_power_l3_import_t0_max_w',
:'p39_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p39_derived_active_power_l3_import_t0_min_w',
:'p39_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p39_derived_active_power_total_export_t0_avg_w',
:'p39_derived_active_power_total_export_t0_max_w',
:'p39_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p39_derived_active_power_total_export_t0_min_w',
:'p39_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p39_derived_active_power_total_import_t0_avg_w',
:'p39_derived_active_power_total_import_t0_max_w',
:'p39_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p39_derived_active_power_total_import_t0_min_w',
:'p39_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p39_derived_active_power_total_import_t1_avg_w',
:'p39_derived_active_power_total_import_t1_max_w',
:'p39_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p39_derived_active_power_total_import_t1_min_w',
:'p39_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p39_derived_active_power_total_import_t2_avg_w',
:'p39_derived_active_power_total_import_t2_max_w',
:'p39_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p39_derived_active_power_total_import_t2_min_w',
:'p39_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p39_derived_reactive_power_total_export_t0_avg_var',
:'p39_derived_reactive_power_total_export_t0_max_var',
:'p39_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p39_derived_reactive_power_total_export_t0_min_var',
:'p39_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p39_derived_reactive_power_total_import_t0_avg_var',
:'p39_derived_reactive_power_total_import_t0_max_var',
:'p39_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p39_derived_reactive_power_total_import_t0_min_var',
:'p39_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p39_reactive_energy_total_export_t0_max_varh',
:'p39_reactive_energy_total_export_t0_min_varh',
:'p39_reactive_energy_total_import_t0_max_varh',
:'p39_reactive_energy_total_import_t0_min_varh',
:'p39_reactive_power_total_net_t0_avg_var',
:'p39_reactive_power_total_net_t0_max_var',
:'p39_reactive_power_total_net_t0_max_timestamp'::timestamptz,
:'p39_reactive_power_total_net_t0_min_var',
:'p39_reactive_power_total_net_t0_min_timestamp'::timestamptz,
:'p39_voltage_l1_any_t0_avg_v',
:'p39_voltage_l1_any_t0_max_v',
:'p39_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p39_voltage_l1_any_t0_min_v',
:'p39_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p39_voltage_l2_any_t0_avg_v',
:'p39_voltage_l2_any_t0_max_v',
:'p39_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p39_voltage_l2_any_t0_min_v',
:'p39_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p39_voltage_l3_any_t0_avg_v',
:'p39_voltage_l3_any_t0_max_v',
:'p39_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p39_voltage_l3_any_t0_min_v',
:'p39_voltage_l3_any_t0_min_timestamp'::timestamptz
  )
on conflict (
  timestamp,
  interval,
  meter_id,
  measurement_location_id
) do
update
set
  count = schneider_iem3xxx_aggregates.count + EXCLUDED.count,
  active_energy_l1_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_l1_import_t0_max_wh,
    EXCLUDED.active_energy_l1_import_t0_max_wh
  ),
  active_energy_l1_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_l1_import_t0_min_wh,
    EXCLUDED.active_energy_l1_import_t0_min_wh
  ),
  active_energy_l2_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_l2_import_t0_max_wh,
    EXCLUDED.active_energy_l2_import_t0_max_wh
  ),
  active_energy_l2_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_l2_import_t0_min_wh,
    EXCLUDED.active_energy_l2_import_t0_min_wh
  ),
  active_energy_l3_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_l3_import_t0_max_wh,
    EXCLUDED.active_energy_l3_import_t0_max_wh
  ),
  active_energy_l3_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_l3_import_t0_min_wh,
    EXCLUDED.active_energy_l3_import_t0_min_wh
  ),
  active_energy_total_export_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_export_t0_max_wh,
    EXCLUDED.active_energy_total_export_t0_max_wh
  ),
  active_energy_total_export_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_export_t0_min_wh,
    EXCLUDED.active_energy_total_export_t0_min_wh
  ),
  active_energy_total_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_import_t0_max_wh,
    EXCLUDED.active_energy_total_import_t0_max_wh
  ),
  active_energy_total_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_import_t0_min_wh,
    EXCLUDED.active_energy_total_import_t0_min_wh
  ),
  active_energy_total_import_t1_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_import_t1_max_wh,
    EXCLUDED.active_energy_total_import_t1_max_wh
  ),
  active_energy_total_import_t1_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_import_t1_min_wh,
    EXCLUDED.active_energy_total_import_t1_min_wh
  ),
  active_energy_total_import_t2_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_import_t2_max_wh,
    EXCLUDED.active_energy_total_import_t2_max_wh
  ),
  active_energy_total_import_t2_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_import_t2_min_wh,
    EXCLUDED.active_energy_total_import_t2_min_wh
  ),
  active_power_l1_net_t0_avg_w = (
    schneider_iem3xxx_aggregates.active_power_l1_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l1_net_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  active_power_l1_net_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_w,
    EXCLUDED.active_power_l1_net_t0_max_w
  ),
  active_power_l1_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_w then EXCLUDED.active_power_l1_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_timestamp
  end,
  active_power_l1_net_t0_min_w = least(
    schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_w,
    EXCLUDED.active_power_l1_net_t0_min_w
  ),
  active_power_l1_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_w then EXCLUDED.active_power_l1_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_timestamp
  end,
  active_power_l2_net_t0_avg_w = (
    schneider_iem3xxx_aggregates.active_power_l2_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l2_net_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  active_power_l2_net_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_w,
    EXCLUDED.active_power_l2_net_t0_max_w
  ),
  active_power_l2_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_w then EXCLUDED.active_power_l2_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_timestamp
  end,
  active_power_l2_net_t0_min_w = least(
    schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_w,
    EXCLUDED.active_power_l2_net_t0_min_w
  ),
  active_power_l2_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_w then EXCLUDED.active_power_l2_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_timestamp
  end,
  active_power_l3_net_t0_avg_w = (
    schneider_iem3xxx_aggregates.active_power_l3_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l3_net_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  active_power_l3_net_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_w,
    EXCLUDED.active_power_l3_net_t0_max_w
  ),
  active_power_l3_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_w then EXCLUDED.active_power_l3_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_timestamp
  end,
  active_power_l3_net_t0_min_w = least(
    schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_w,
    EXCLUDED.active_power_l3_net_t0_min_w
  ),
  active_power_l3_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_w then EXCLUDED.active_power_l3_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_timestamp
  end,
  apparent_power_total_net_t0_avg_va = (
    schneider_iem3xxx_aggregates.apparent_power_total_net_t0_avg_va * schneider_iem3xxx_aggregates.count + EXCLUDED.apparent_power_total_net_t0_avg_va * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  apparent_power_total_net_t0_max_va = greatest(
    schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_va,
    EXCLUDED.apparent_power_total_net_t0_max_va
  ),
  apparent_power_total_net_t0_max_timestamp = case
    when EXCLUDED.apparent_power_total_net_t0_max_va > schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_va then EXCLUDED.apparent_power_total_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_timestamp
  end,
  apparent_power_total_net_t0_min_va = least(
    schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_va,
    EXCLUDED.apparent_power_total_net_t0_min_va
  ),
  apparent_power_total_net_t0_min_timestamp = case
    when EXCLUDED.apparent_power_total_net_t0_min_va < schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_va then EXCLUDED.apparent_power_total_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_timestamp
  end,
  current_l1_any_t0_avg_a = (
    schneider_iem3xxx_aggregates.current_l1_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l1_any_t0_avg_a * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  current_l1_any_t0_max_a = greatest(
    schneider_iem3xxx_aggregates.current_l1_any_t0_max_a,
    EXCLUDED.current_l1_any_t0_max_a
  ),
  current_l1_any_t0_max_timestamp = case
    when EXCLUDED.current_l1_any_t0_max_a > schneider_iem3xxx_aggregates.current_l1_any_t0_max_a then EXCLUDED.current_l1_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.current_l1_any_t0_max_timestamp
  end,
  current_l1_any_t0_min_a = least(
    schneider_iem3xxx_aggregates.current_l1_any_t0_min_a,
    EXCLUDED.current_l1_any_t0_min_a
  ),
  current_l1_any_t0_min_timestamp = case
    when EXCLUDED.current_l1_any_t0_min_a < schneider_iem3xxx_aggregates.current_l1_any_t0_min_a then EXCLUDED.current_l1_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.current_l1_any_t0_min_timestamp
  end,
  current_l2_any_t0_avg_a = (
    schneider_iem3xxx_aggregates.current_l2_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l2_any_t0_avg_a * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  current_l2_any_t0_max_a = greatest(
    schneider_iem3xxx_aggregates.current_l2_any_t0_max_a,
    EXCLUDED.current_l2_any_t0_max_a
  ),
  current_l2_any_t0_max_timestamp = case
    when EXCLUDED.current_l2_any_t0_max_a > schneider_iem3xxx_aggregates.current_l2_any_t0_max_a then EXCLUDED.current_l2_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.current_l2_any_t0_max_timestamp
  end,
  current_l2_any_t0_min_a = least(
    schneider_iem3xxx_aggregates.current_l2_any_t0_min_a,
    EXCLUDED.current_l2_any_t0_min_a
  ),
  current_l2_any_t0_min_timestamp = case
    when EXCLUDED.current_l2_any_t0_min_a < schneider_iem3xxx_aggregates.current_l2_any_t0_min_a then EXCLUDED.current_l2_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.current_l2_any_t0_min_timestamp
  end,
  current_l3_any_t0_avg_a = (
    schneider_iem3xxx_aggregates.current_l3_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l3_any_t0_avg_a * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  current_l3_any_t0_max_a = greatest(
    schneider_iem3xxx_aggregates.current_l3_any_t0_max_a,
    EXCLUDED.current_l3_any_t0_max_a
  ),
  current_l3_any_t0_max_timestamp = case
    when EXCLUDED.current_l3_any_t0_max_a > schneider_iem3xxx_aggregates.current_l3_any_t0_max_a then EXCLUDED.current_l3_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.current_l3_any_t0_max_timestamp
  end,
  current_l3_any_t0_min_a = least(
    schneider_iem3xxx_aggregates.current_l3_any_t0_min_a,
    EXCLUDED.current_l3_any_t0_min_a
  ),
  current_l3_any_t0_min_timestamp = case
    when EXCLUDED.current_l3_any_t0_min_a < schneider_iem3xxx_aggregates.current_l3_any_t0_min_a then EXCLUDED.current_l3_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.current_l3_any_t0_min_timestamp
  end,
  derived_active_power_l1_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_l1_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_l1_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w,
    EXCLUDED.derived_active_power_l1_import_t0_max_w
  ),
  derived_active_power_l1_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w then EXCLUDED.derived_active_power_l1_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_timestamp
  end,
  derived_active_power_l1_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w,
    EXCLUDED.derived_active_power_l1_import_t0_min_w
  ),
  derived_active_power_l1_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w then EXCLUDED.derived_active_power_l1_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_timestamp
  end,
  derived_active_power_l2_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_l2_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_l2_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w,
    EXCLUDED.derived_active_power_l2_import_t0_max_w
  ),
  derived_active_power_l2_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w then EXCLUDED.derived_active_power_l2_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_timestamp
  end,
  derived_active_power_l2_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w,
    EXCLUDED.derived_active_power_l2_import_t0_min_w
  ),
  derived_active_power_l2_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w then EXCLUDED.derived_active_power_l2_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_timestamp
  end,
  derived_active_power_l3_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_l3_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_l3_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w,
    EXCLUDED.derived_active_power_l3_import_t0_max_w
  ),
  derived_active_power_l3_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w then EXCLUDED.derived_active_power_l3_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_timestamp
  end,
  derived_active_power_l3_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w,
    EXCLUDED.derived_active_power_l3_import_t0_min_w
  ),
  derived_active_power_l3_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w then EXCLUDED.derived_active_power_l3_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_timestamp
  end,
  derived_active_power_total_export_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_export_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_export_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w,
    EXCLUDED.derived_active_power_total_export_t0_max_w
  ),
  derived_active_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w then EXCLUDED.derived_active_power_total_export_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_timestamp
  end,
  derived_active_power_total_export_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w,
    EXCLUDED.derived_active_power_total_export_t0_min_w
  ),
  derived_active_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w then EXCLUDED.derived_active_power_total_export_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_timestamp
  end,
  derived_active_power_total_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w,
    EXCLUDED.derived_active_power_total_import_t0_max_w
  ),
  derived_active_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w then EXCLUDED.derived_active_power_total_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_timestamp
  end,
  derived_active_power_total_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w,
    EXCLUDED.derived_active_power_total_import_t0_min_w
  ),
  derived_active_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w then EXCLUDED.derived_active_power_total_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_timestamp
  end,
  derived_active_power_total_import_t1_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_import_t1_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_import_t1_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w,
    EXCLUDED.derived_active_power_total_import_t1_max_w
  ),
  derived_active_power_total_import_t1_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w then EXCLUDED.derived_active_power_total_import_t1_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_timestamp
  end,
  derived_active_power_total_import_t1_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w,
    EXCLUDED.derived_active_power_total_import_t1_min_w
  ),
  derived_active_power_total_import_t1_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w then EXCLUDED.derived_active_power_total_import_t1_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_timestamp
  end,
  derived_active_power_total_import_t2_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_import_t2_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_import_t2_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w,
    EXCLUDED.derived_active_power_total_import_t2_max_w
  ),
  derived_active_power_total_import_t2_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w then EXCLUDED.derived_active_power_total_import_t2_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_timestamp
  end,
  derived_active_power_total_import_t2_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w,
    EXCLUDED.derived_active_power_total_import_t2_min_w
  ),
  derived_active_power_total_import_t2_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w then EXCLUDED.derived_active_power_total_import_t2_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_timestamp
  end,
  derived_reactive_power_total_export_t0_avg_var = (
    schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_reactive_power_total_export_t0_avg_var * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_reactive_power_total_export_t0_max_var = greatest(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var,
    EXCLUDED.derived_reactive_power_total_export_t0_max_var
  ),
  derived_reactive_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var then EXCLUDED.derived_reactive_power_total_export_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_timestamp
  end,
  derived_reactive_power_total_export_t0_min_var = least(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var,
    EXCLUDED.derived_reactive_power_total_export_t0_min_var
  ),
  derived_reactive_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var then EXCLUDED.derived_reactive_power_total_export_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_timestamp
  end,
  derived_reactive_power_total_import_t0_avg_var = (
    schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_reactive_power_total_import_t0_avg_var * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_reactive_power_total_import_t0_max_var = greatest(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var,
    EXCLUDED.derived_reactive_power_total_import_t0_max_var
  ),
  derived_reactive_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var then EXCLUDED.derived_reactive_power_total_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_timestamp
  end,
  derived_reactive_power_total_import_t0_min_var = least(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var,
    EXCLUDED.derived_reactive_power_total_import_t0_min_var
  ),
  derived_reactive_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var then EXCLUDED.derived_reactive_power_total_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_timestamp
  end,
  reactive_energy_total_export_t0_max_varh = greatest(
    schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_max_varh,
    EXCLUDED.reactive_energy_total_export_t0_max_varh
  ),
  reactive_energy_total_export_t0_min_varh = least(
    schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_min_varh,
    EXCLUDED.reactive_energy_total_export_t0_min_varh
  ),
  reactive_energy_total_import_t0_max_varh = greatest(
    schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_max_varh,
    EXCLUDED.reactive_energy_total_import_t0_max_varh
  ),
  reactive_energy_total_import_t0_min_varh = least(
    schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_min_varh,
    EXCLUDED.reactive_energy_total_import_t0_min_varh
  ),
  reactive_power_total_net_t0_avg_var = (
    schneider_iem3xxx_aggregates.reactive_power_total_net_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.reactive_power_total_net_t0_avg_var * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  reactive_power_total_net_t0_max_var = greatest(
    schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_var,
    EXCLUDED.reactive_power_total_net_t0_max_var
  ),
  reactive_power_total_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_total_net_t0_max_var > schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_var then EXCLUDED.reactive_power_total_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_timestamp
  end,
  reactive_power_total_net_t0_min_var = least(
    schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_var,
    EXCLUDED.reactive_power_total_net_t0_min_var
  ),
  reactive_power_total_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_total_net_t0_min_var < schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_var then EXCLUDED.reactive_power_total_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_timestamp
  end,
  voltage_l1_any_t0_avg_v = (
    schneider_iem3xxx_aggregates.voltage_l1_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l1_any_t0_avg_v * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  voltage_l1_any_t0_max_v = greatest(
    schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_v,
    EXCLUDED.voltage_l1_any_t0_max_v
  ),
  voltage_l1_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_v then EXCLUDED.voltage_l1_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_timestamp
  end,
  voltage_l1_any_t0_min_v = least(
    schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_v,
    EXCLUDED.voltage_l1_any_t0_min_v
  ),
  voltage_l1_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_v then EXCLUDED.voltage_l1_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_timestamp
  end,
  voltage_l2_any_t0_avg_v = (
    schneider_iem3xxx_aggregates.voltage_l2_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l2_any_t0_avg_v * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  voltage_l2_any_t0_max_v = greatest(
    schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_v,
    EXCLUDED.voltage_l2_any_t0_max_v
  ),
  voltage_l2_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_v then EXCLUDED.voltage_l2_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_timestamp
  end,
  voltage_l2_any_t0_min_v = least(
    schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_v,
    EXCLUDED.voltage_l2_any_t0_min_v
  ),
  voltage_l2_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_v then EXCLUDED.voltage_l2_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_timestamp
  end,
  voltage_l3_any_t0_avg_v = (
    schneider_iem3xxx_aggregates.voltage_l3_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l3_any_t0_avg_v * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  voltage_l3_any_t0_max_v = greatest(
    schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_v,
    EXCLUDED.voltage_l3_any_t0_max_v
  ),
  voltage_l3_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_v then EXCLUDED.voltage_l3_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_timestamp
  end,
  voltage_l3_any_t0_min_v = least(
    schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_v,
    EXCLUDED.voltage_l3_any_t0_min_v
  ),
  voltage_l3_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_v then EXCLUDED.voltage_l3_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_timestamp
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
:'p32_timestamp'::timestamptz,
:'p32_interval'::interval_entity,
:'p32_meter_id',
:'p32_measurement_location_id',
:'p32_count',
:'p32_quarter_hour_count',
:'p32_active_energy_l1_export_t0_max_wh',
:'p32_active_energy_l1_export_t0_min_wh',
:'p32_active_energy_l1_import_t0_max_wh',
:'p32_active_energy_l1_import_t0_min_wh',
:'p32_active_energy_l2_export_t0_max_wh',
:'p32_active_energy_l2_export_t0_min_wh',
:'p32_active_energy_l2_import_t0_max_wh',
:'p32_active_energy_l2_import_t0_min_wh',
:'p32_active_energy_l3_export_t0_max_wh',
:'p32_active_energy_l3_export_t0_min_wh',
:'p32_active_energy_l3_import_t0_max_wh',
:'p32_active_energy_l3_import_t0_min_wh',
:'p32_active_energy_total_export_t0_max_wh',
:'p32_active_energy_total_export_t0_min_wh',
:'p32_active_energy_total_import_t0_max_wh',
:'p32_active_energy_total_import_t0_min_wh',
:'p32_active_energy_total_import_t1_max_wh',
:'p32_active_energy_total_import_t1_min_wh',
:'p32_active_energy_total_import_t2_max_wh',
:'p32_active_energy_total_import_t2_min_wh',
:'p32_active_power_l1_net_t0_avg_w',
:'p32_active_power_l1_net_t0_max_w',
:'p32_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p32_active_power_l1_net_t0_min_w',
:'p32_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p32_active_power_l2_net_t0_avg_w',
:'p32_active_power_l2_net_t0_max_w',
:'p32_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p32_active_power_l2_net_t0_min_w',
:'p32_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p32_active_power_l3_net_t0_avg_w',
:'p32_active_power_l3_net_t0_max_w',
:'p32_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p32_active_power_l3_net_t0_min_w',
:'p32_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p32_current_l1_any_t0_avg_a',
:'p32_current_l1_any_t0_max_a',
:'p32_current_l1_any_t0_max_timestamp'::timestamptz,
:'p32_current_l1_any_t0_min_a',
:'p32_current_l1_any_t0_min_timestamp'::timestamptz,
:'p32_current_l2_any_t0_avg_a',
:'p32_current_l2_any_t0_max_a',
:'p32_current_l2_any_t0_max_timestamp'::timestamptz,
:'p32_current_l2_any_t0_min_a',
:'p32_current_l2_any_t0_min_timestamp'::timestamptz,
:'p32_current_l3_any_t0_avg_a',
:'p32_current_l3_any_t0_max_a',
:'p32_current_l3_any_t0_max_timestamp'::timestamptz,
:'p32_current_l3_any_t0_min_a',
:'p32_current_l3_any_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_l1_export_t0_avg_w',
:'p32_derived_active_power_l1_export_t0_max_w',
:'p32_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_l1_export_t0_min_w',
:'p32_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_l1_import_t0_avg_w',
:'p32_derived_active_power_l1_import_t0_max_w',
:'p32_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_l1_import_t0_min_w',
:'p32_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_l2_export_t0_avg_w',
:'p32_derived_active_power_l2_export_t0_max_w',
:'p32_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_l2_export_t0_min_w',
:'p32_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_l2_import_t0_avg_w',
:'p32_derived_active_power_l2_import_t0_max_w',
:'p32_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_l2_import_t0_min_w',
:'p32_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_l3_export_t0_avg_w',
:'p32_derived_active_power_l3_export_t0_max_w',
:'p32_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_l3_export_t0_min_w',
:'p32_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_l3_import_t0_avg_w',
:'p32_derived_active_power_l3_import_t0_max_w',
:'p32_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_l3_import_t0_min_w',
:'p32_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_total_export_t0_avg_w',
:'p32_derived_active_power_total_export_t0_max_w',
:'p32_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_total_export_t0_min_w',
:'p32_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_total_import_t0_avg_w',
:'p32_derived_active_power_total_import_t0_max_w',
:'p32_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p32_derived_active_power_total_import_t0_min_w',
:'p32_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p32_derived_active_power_total_import_t1_avg_w',
:'p32_derived_active_power_total_import_t1_max_w',
:'p32_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p32_derived_active_power_total_import_t1_min_w',
:'p32_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p32_derived_active_power_total_import_t2_avg_w',
:'p32_derived_active_power_total_import_t2_max_w',
:'p32_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p32_derived_active_power_total_import_t2_min_w',
:'p32_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_l1_export_t0_avg_var',
:'p32_derived_reactive_power_l1_export_t0_max_var',
:'p32_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_l1_export_t0_min_var',
:'p32_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_l1_import_t0_avg_var',
:'p32_derived_reactive_power_l1_import_t0_max_var',
:'p32_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_l1_import_t0_min_var',
:'p32_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_l2_export_t0_avg_var',
:'p32_derived_reactive_power_l2_export_t0_max_var',
:'p32_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_l2_export_t0_min_var',
:'p32_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_l2_import_t0_avg_var',
:'p32_derived_reactive_power_l2_import_t0_max_var',
:'p32_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_l2_import_t0_min_var',
:'p32_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_l3_export_t0_avg_var',
:'p32_derived_reactive_power_l3_export_t0_max_var',
:'p32_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_l3_export_t0_min_var',
:'p32_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_l3_import_t0_avg_var',
:'p32_derived_reactive_power_l3_import_t0_max_var',
:'p32_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_l3_import_t0_min_var',
:'p32_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_total_export_t0_avg_var',
:'p32_derived_reactive_power_total_export_t0_max_var',
:'p32_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_total_export_t0_min_var',
:'p32_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p32_derived_reactive_power_total_import_t0_avg_var',
:'p32_derived_reactive_power_total_import_t0_max_var',
:'p32_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p32_derived_reactive_power_total_import_t0_min_var',
:'p32_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p32_reactive_energy_l1_export_t0_max_varh',
:'p32_reactive_energy_l1_export_t0_min_varh',
:'p32_reactive_energy_l1_import_t0_max_varh',
:'p32_reactive_energy_l1_import_t0_min_varh',
:'p32_reactive_energy_l2_export_t0_max_varh',
:'p32_reactive_energy_l2_export_t0_min_varh',
:'p32_reactive_energy_l2_import_t0_max_varh',
:'p32_reactive_energy_l2_import_t0_min_varh',
:'p32_reactive_energy_l3_export_t0_max_varh',
:'p32_reactive_energy_l3_export_t0_min_varh',
:'p32_reactive_energy_l3_import_t0_max_varh',
:'p32_reactive_energy_l3_import_t0_min_varh',
:'p32_reactive_energy_total_export_t0_max_varh',
:'p32_reactive_energy_total_export_t0_min_varh',
:'p32_reactive_energy_total_import_t0_max_varh',
:'p32_reactive_energy_total_import_t0_min_varh',
:'p32_reactive_power_l1_net_t0_avg_var',
:'p32_reactive_power_l1_net_t0_max_var',
:'p32_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p32_reactive_power_l1_net_t0_min_var',
:'p32_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p32_reactive_power_l2_net_t0_avg_var',
:'p32_reactive_power_l2_net_t0_max_var',
:'p32_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p32_reactive_power_l2_net_t0_min_var',
:'p32_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p32_reactive_power_l3_net_t0_avg_var',
:'p32_reactive_power_l3_net_t0_max_var',
:'p32_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p32_reactive_power_l3_net_t0_min_var',
:'p32_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p32_voltage_l1_any_t0_avg_v',
:'p32_voltage_l1_any_t0_max_v',
:'p32_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p32_voltage_l1_any_t0_min_v',
:'p32_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p32_voltage_l2_any_t0_avg_v',
:'p32_voltage_l2_any_t0_max_v',
:'p32_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p32_voltage_l2_any_t0_min_v',
:'p32_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p32_voltage_l3_any_t0_avg_v',
:'p32_voltage_l3_any_t0_max_v',
:'p32_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p32_voltage_l3_any_t0_min_v',
:'p32_voltage_l3_any_t0_min_timestamp'::timestamptz
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
:'p35_timestamp'::timestamptz,
:'p35_interval'::interval_entity,
:'p35_meter_id',
:'p35_measurement_location_id',
:'p35_count',
:'p35_quarter_hour_count',
:'p35_active_energy_l1_export_t0_max_wh',
:'p35_active_energy_l1_export_t0_min_wh',
:'p35_active_energy_l1_import_t0_max_wh',
:'p35_active_energy_l1_import_t0_min_wh',
:'p35_active_energy_l2_export_t0_max_wh',
:'p35_active_energy_l2_export_t0_min_wh',
:'p35_active_energy_l2_import_t0_max_wh',
:'p35_active_energy_l2_import_t0_min_wh',
:'p35_active_energy_l3_export_t0_max_wh',
:'p35_active_energy_l3_export_t0_min_wh',
:'p35_active_energy_l3_import_t0_max_wh',
:'p35_active_energy_l3_import_t0_min_wh',
:'p35_active_energy_total_export_t0_max_wh',
:'p35_active_energy_total_export_t0_min_wh',
:'p35_active_energy_total_import_t0_max_wh',
:'p35_active_energy_total_import_t0_min_wh',
:'p35_active_energy_total_import_t1_max_wh',
:'p35_active_energy_total_import_t1_min_wh',
:'p35_active_energy_total_import_t2_max_wh',
:'p35_active_energy_total_import_t2_min_wh',
:'p35_active_power_l1_net_t0_avg_w',
:'p35_active_power_l1_net_t0_max_w',
:'p35_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p35_active_power_l1_net_t0_min_w',
:'p35_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p35_active_power_l2_net_t0_avg_w',
:'p35_active_power_l2_net_t0_max_w',
:'p35_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p35_active_power_l2_net_t0_min_w',
:'p35_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p35_active_power_l3_net_t0_avg_w',
:'p35_active_power_l3_net_t0_max_w',
:'p35_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p35_active_power_l3_net_t0_min_w',
:'p35_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p35_current_l1_any_t0_avg_a',
:'p35_current_l1_any_t0_max_a',
:'p35_current_l1_any_t0_max_timestamp'::timestamptz,
:'p35_current_l1_any_t0_min_a',
:'p35_current_l1_any_t0_min_timestamp'::timestamptz,
:'p35_current_l2_any_t0_avg_a',
:'p35_current_l2_any_t0_max_a',
:'p35_current_l2_any_t0_max_timestamp'::timestamptz,
:'p35_current_l2_any_t0_min_a',
:'p35_current_l2_any_t0_min_timestamp'::timestamptz,
:'p35_current_l3_any_t0_avg_a',
:'p35_current_l3_any_t0_max_a',
:'p35_current_l3_any_t0_max_timestamp'::timestamptz,
:'p35_current_l3_any_t0_min_a',
:'p35_current_l3_any_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_l1_export_t0_avg_w',
:'p35_derived_active_power_l1_export_t0_max_w',
:'p35_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_l1_export_t0_min_w',
:'p35_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_l1_import_t0_avg_w',
:'p35_derived_active_power_l1_import_t0_max_w',
:'p35_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_l1_import_t0_min_w',
:'p35_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_l2_export_t0_avg_w',
:'p35_derived_active_power_l2_export_t0_max_w',
:'p35_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_l2_export_t0_min_w',
:'p35_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_l2_import_t0_avg_w',
:'p35_derived_active_power_l2_import_t0_max_w',
:'p35_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_l2_import_t0_min_w',
:'p35_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_l3_export_t0_avg_w',
:'p35_derived_active_power_l3_export_t0_max_w',
:'p35_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_l3_export_t0_min_w',
:'p35_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_l3_import_t0_avg_w',
:'p35_derived_active_power_l3_import_t0_max_w',
:'p35_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_l3_import_t0_min_w',
:'p35_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_total_export_t0_avg_w',
:'p35_derived_active_power_total_export_t0_max_w',
:'p35_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_total_export_t0_min_w',
:'p35_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_total_import_t0_avg_w',
:'p35_derived_active_power_total_import_t0_max_w',
:'p35_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p35_derived_active_power_total_import_t0_min_w',
:'p35_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p35_derived_active_power_total_import_t1_avg_w',
:'p35_derived_active_power_total_import_t1_max_w',
:'p35_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p35_derived_active_power_total_import_t1_min_w',
:'p35_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p35_derived_active_power_total_import_t2_avg_w',
:'p35_derived_active_power_total_import_t2_max_w',
:'p35_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p35_derived_active_power_total_import_t2_min_w',
:'p35_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_l1_export_t0_avg_var',
:'p35_derived_reactive_power_l1_export_t0_max_var',
:'p35_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_l1_export_t0_min_var',
:'p35_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_l1_import_t0_avg_var',
:'p35_derived_reactive_power_l1_import_t0_max_var',
:'p35_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_l1_import_t0_min_var',
:'p35_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_l2_export_t0_avg_var',
:'p35_derived_reactive_power_l2_export_t0_max_var',
:'p35_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_l2_export_t0_min_var',
:'p35_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_l2_import_t0_avg_var',
:'p35_derived_reactive_power_l2_import_t0_max_var',
:'p35_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_l2_import_t0_min_var',
:'p35_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_l3_export_t0_avg_var',
:'p35_derived_reactive_power_l3_export_t0_max_var',
:'p35_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_l3_export_t0_min_var',
:'p35_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_l3_import_t0_avg_var',
:'p35_derived_reactive_power_l3_import_t0_max_var',
:'p35_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_l3_import_t0_min_var',
:'p35_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_total_export_t0_avg_var',
:'p35_derived_reactive_power_total_export_t0_max_var',
:'p35_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_total_export_t0_min_var',
:'p35_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p35_derived_reactive_power_total_import_t0_avg_var',
:'p35_derived_reactive_power_total_import_t0_max_var',
:'p35_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p35_derived_reactive_power_total_import_t0_min_var',
:'p35_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p35_reactive_energy_l1_export_t0_max_varh',
:'p35_reactive_energy_l1_export_t0_min_varh',
:'p35_reactive_energy_l1_import_t0_max_varh',
:'p35_reactive_energy_l1_import_t0_min_varh',
:'p35_reactive_energy_l2_export_t0_max_varh',
:'p35_reactive_energy_l2_export_t0_min_varh',
:'p35_reactive_energy_l2_import_t0_max_varh',
:'p35_reactive_energy_l2_import_t0_min_varh',
:'p35_reactive_energy_l3_export_t0_max_varh',
:'p35_reactive_energy_l3_export_t0_min_varh',
:'p35_reactive_energy_l3_import_t0_max_varh',
:'p35_reactive_energy_l3_import_t0_min_varh',
:'p35_reactive_energy_total_export_t0_max_varh',
:'p35_reactive_energy_total_export_t0_min_varh',
:'p35_reactive_energy_total_import_t0_max_varh',
:'p35_reactive_energy_total_import_t0_min_varh',
:'p35_reactive_power_l1_net_t0_avg_var',
:'p35_reactive_power_l1_net_t0_max_var',
:'p35_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p35_reactive_power_l1_net_t0_min_var',
:'p35_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p35_reactive_power_l2_net_t0_avg_var',
:'p35_reactive_power_l2_net_t0_max_var',
:'p35_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p35_reactive_power_l2_net_t0_min_var',
:'p35_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p35_reactive_power_l3_net_t0_avg_var',
:'p35_reactive_power_l3_net_t0_max_var',
:'p35_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p35_reactive_power_l3_net_t0_min_var',
:'p35_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p35_voltage_l1_any_t0_avg_v',
:'p35_voltage_l1_any_t0_max_v',
:'p35_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p35_voltage_l1_any_t0_min_v',
:'p35_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p35_voltage_l2_any_t0_avg_v',
:'p35_voltage_l2_any_t0_max_v',
:'p35_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p35_voltage_l2_any_t0_min_v',
:'p35_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p35_voltage_l3_any_t0_avg_v',
:'p35_voltage_l3_any_t0_max_v',
:'p35_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p35_voltage_l3_any_t0_min_v',
:'p35_voltage_l3_any_t0_min_timestamp'::timestamptz
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
  schneider_iem3xxx_aggregates (
    timestamp,
    interval,
    meter_id,
    measurement_location_id,
    count,
    quarter_hour_count,
    active_energy_l1_import_t0_max_wh,
    active_energy_l1_import_t0_min_wh,
    active_energy_l2_import_t0_max_wh,
    active_energy_l2_import_t0_min_wh,
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
    apparent_power_total_net_t0_avg_va,
    apparent_power_total_net_t0_max_va,
    apparent_power_total_net_t0_max_timestamp,
    apparent_power_total_net_t0_min_va,
    apparent_power_total_net_t0_min_timestamp,
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
    derived_active_power_l1_import_t0_avg_w,
    derived_active_power_l1_import_t0_max_w,
    derived_active_power_l1_import_t0_max_timestamp,
    derived_active_power_l1_import_t0_min_w,
    derived_active_power_l1_import_t0_min_timestamp,
    derived_active_power_l2_import_t0_avg_w,
    derived_active_power_l2_import_t0_max_w,
    derived_active_power_l2_import_t0_max_timestamp,
    derived_active_power_l2_import_t0_min_w,
    derived_active_power_l2_import_t0_min_timestamp,
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
    reactive_energy_total_export_t0_max_varh,
    reactive_energy_total_export_t0_min_varh,
    reactive_energy_total_import_t0_max_varh,
    reactive_energy_total_import_t0_min_varh,
    reactive_power_total_net_t0_avg_var,
    reactive_power_total_net_t0_max_var,
    reactive_power_total_net_t0_max_timestamp,
    reactive_power_total_net_t0_min_var,
    reactive_power_total_net_t0_min_timestamp,
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
:'p38_timestamp'::timestamptz,
:'p38_interval'::interval_entity,
:'p38_meter_id',
:'p38_measurement_location_id',
:'p38_count',
:'p38_quarter_hour_count',
:'p38_active_energy_l1_import_t0_max_wh',
:'p38_active_energy_l1_import_t0_min_wh',
:'p38_active_energy_l2_import_t0_max_wh',
:'p38_active_energy_l2_import_t0_min_wh',
:'p38_active_energy_l3_import_t0_max_wh',
:'p38_active_energy_l3_import_t0_min_wh',
:'p38_active_energy_total_export_t0_max_wh',
:'p38_active_energy_total_export_t0_min_wh',
:'p38_active_energy_total_import_t0_max_wh',
:'p38_active_energy_total_import_t0_min_wh',
:'p38_active_energy_total_import_t1_max_wh',
:'p38_active_energy_total_import_t1_min_wh',
:'p38_active_energy_total_import_t2_max_wh',
:'p38_active_energy_total_import_t2_min_wh',
:'p38_active_power_l1_net_t0_avg_w',
:'p38_active_power_l1_net_t0_max_w',
:'p38_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p38_active_power_l1_net_t0_min_w',
:'p38_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p38_active_power_l2_net_t0_avg_w',
:'p38_active_power_l2_net_t0_max_w',
:'p38_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p38_active_power_l2_net_t0_min_w',
:'p38_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p38_active_power_l3_net_t0_avg_w',
:'p38_active_power_l3_net_t0_max_w',
:'p38_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p38_active_power_l3_net_t0_min_w',
:'p38_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p38_apparent_power_total_net_t0_avg_va',
:'p38_apparent_power_total_net_t0_max_va',
:'p38_apparent_power_total_net_t0_max_timestamp'::timestamptz,
:'p38_apparent_power_total_net_t0_min_va',
:'p38_apparent_power_total_net_t0_min_timestamp'::timestamptz,
:'p38_current_l1_any_t0_avg_a',
:'p38_current_l1_any_t0_max_a',
:'p38_current_l1_any_t0_max_timestamp'::timestamptz,
:'p38_current_l1_any_t0_min_a',
:'p38_current_l1_any_t0_min_timestamp'::timestamptz,
:'p38_current_l2_any_t0_avg_a',
:'p38_current_l2_any_t0_max_a',
:'p38_current_l2_any_t0_max_timestamp'::timestamptz,
:'p38_current_l2_any_t0_min_a',
:'p38_current_l2_any_t0_min_timestamp'::timestamptz,
:'p38_current_l3_any_t0_avg_a',
:'p38_current_l3_any_t0_max_a',
:'p38_current_l3_any_t0_max_timestamp'::timestamptz,
:'p38_current_l3_any_t0_min_a',
:'p38_current_l3_any_t0_min_timestamp'::timestamptz,
:'p38_derived_active_power_l1_import_t0_avg_w',
:'p38_derived_active_power_l1_import_t0_max_w',
:'p38_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p38_derived_active_power_l1_import_t0_min_w',
:'p38_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p38_derived_active_power_l2_import_t0_avg_w',
:'p38_derived_active_power_l2_import_t0_max_w',
:'p38_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p38_derived_active_power_l2_import_t0_min_w',
:'p38_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p38_derived_active_power_l3_import_t0_avg_w',
:'p38_derived_active_power_l3_import_t0_max_w',
:'p38_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p38_derived_active_power_l3_import_t0_min_w',
:'p38_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p38_derived_active_power_total_export_t0_avg_w',
:'p38_derived_active_power_total_export_t0_max_w',
:'p38_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p38_derived_active_power_total_export_t0_min_w',
:'p38_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p38_derived_active_power_total_import_t0_avg_w',
:'p38_derived_active_power_total_import_t0_max_w',
:'p38_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p38_derived_active_power_total_import_t0_min_w',
:'p38_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p38_derived_active_power_total_import_t1_avg_w',
:'p38_derived_active_power_total_import_t1_max_w',
:'p38_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p38_derived_active_power_total_import_t1_min_w',
:'p38_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p38_derived_active_power_total_import_t2_avg_w',
:'p38_derived_active_power_total_import_t2_max_w',
:'p38_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p38_derived_active_power_total_import_t2_min_w',
:'p38_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p38_derived_reactive_power_total_export_t0_avg_var',
:'p38_derived_reactive_power_total_export_t0_max_var',
:'p38_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p38_derived_reactive_power_total_export_t0_min_var',
:'p38_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p38_derived_reactive_power_total_import_t0_avg_var',
:'p38_derived_reactive_power_total_import_t0_max_var',
:'p38_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p38_derived_reactive_power_total_import_t0_min_var',
:'p38_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p38_reactive_energy_total_export_t0_max_varh',
:'p38_reactive_energy_total_export_t0_min_varh',
:'p38_reactive_energy_total_import_t0_max_varh',
:'p38_reactive_energy_total_import_t0_min_varh',
:'p38_reactive_power_total_net_t0_avg_var',
:'p38_reactive_power_total_net_t0_max_var',
:'p38_reactive_power_total_net_t0_max_timestamp'::timestamptz,
:'p38_reactive_power_total_net_t0_min_var',
:'p38_reactive_power_total_net_t0_min_timestamp'::timestamptz,
:'p38_voltage_l1_any_t0_avg_v',
:'p38_voltage_l1_any_t0_max_v',
:'p38_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p38_voltage_l1_any_t0_min_v',
:'p38_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p38_voltage_l2_any_t0_avg_v',
:'p38_voltage_l2_any_t0_max_v',
:'p38_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p38_voltage_l2_any_t0_min_v',
:'p38_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p38_voltage_l3_any_t0_avg_v',
:'p38_voltage_l3_any_t0_max_v',
:'p38_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p38_voltage_l3_any_t0_min_v',
:'p38_voltage_l3_any_t0_min_timestamp'::timestamptz
  )
on conflict (
  timestamp,
  interval,
  meter_id,
  measurement_location_id
) do
update
set
  count = schneider_iem3xxx_aggregates.count + EXCLUDED.count,
  active_energy_l1_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_l1_import_t0_max_wh,
    EXCLUDED.active_energy_l1_import_t0_max_wh
  ),
  active_energy_l1_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_l1_import_t0_min_wh,
    EXCLUDED.active_energy_l1_import_t0_min_wh
  ),
  active_energy_l2_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_l2_import_t0_max_wh,
    EXCLUDED.active_energy_l2_import_t0_max_wh
  ),
  active_energy_l2_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_l2_import_t0_min_wh,
    EXCLUDED.active_energy_l2_import_t0_min_wh
  ),
  active_energy_l3_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_l3_import_t0_max_wh,
    EXCLUDED.active_energy_l3_import_t0_max_wh
  ),
  active_energy_l3_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_l3_import_t0_min_wh,
    EXCLUDED.active_energy_l3_import_t0_min_wh
  ),
  active_energy_total_export_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_export_t0_max_wh,
    EXCLUDED.active_energy_total_export_t0_max_wh
  ),
  active_energy_total_export_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_export_t0_min_wh,
    EXCLUDED.active_energy_total_export_t0_min_wh
  ),
  active_energy_total_import_t0_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_import_t0_max_wh,
    EXCLUDED.active_energy_total_import_t0_max_wh
  ),
  active_energy_total_import_t0_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_import_t0_min_wh,
    EXCLUDED.active_energy_total_import_t0_min_wh
  ),
  active_energy_total_import_t1_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_import_t1_max_wh,
    EXCLUDED.active_energy_total_import_t1_max_wh
  ),
  active_energy_total_import_t1_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_import_t1_min_wh,
    EXCLUDED.active_energy_total_import_t1_min_wh
  ),
  active_energy_total_import_t2_max_wh = greatest(
    schneider_iem3xxx_aggregates.active_energy_total_import_t2_max_wh,
    EXCLUDED.active_energy_total_import_t2_max_wh
  ),
  active_energy_total_import_t2_min_wh = least(
    schneider_iem3xxx_aggregates.active_energy_total_import_t2_min_wh,
    EXCLUDED.active_energy_total_import_t2_min_wh
  ),
  active_power_l1_net_t0_avg_w = (
    schneider_iem3xxx_aggregates.active_power_l1_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l1_net_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  active_power_l1_net_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_w,
    EXCLUDED.active_power_l1_net_t0_max_w
  ),
  active_power_l1_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_w then EXCLUDED.active_power_l1_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_timestamp
  end,
  active_power_l1_net_t0_min_w = least(
    schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_w,
    EXCLUDED.active_power_l1_net_t0_min_w
  ),
  active_power_l1_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l1_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_w then EXCLUDED.active_power_l1_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_timestamp
  end,
  active_power_l2_net_t0_avg_w = (
    schneider_iem3xxx_aggregates.active_power_l2_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l2_net_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  active_power_l2_net_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_w,
    EXCLUDED.active_power_l2_net_t0_max_w
  ),
  active_power_l2_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_w then EXCLUDED.active_power_l2_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_timestamp
  end,
  active_power_l2_net_t0_min_w = least(
    schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_w,
    EXCLUDED.active_power_l2_net_t0_min_w
  ),
  active_power_l2_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l2_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_w then EXCLUDED.active_power_l2_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_timestamp
  end,
  active_power_l3_net_t0_avg_w = (
    schneider_iem3xxx_aggregates.active_power_l3_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l3_net_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  active_power_l3_net_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_w,
    EXCLUDED.active_power_l3_net_t0_max_w
  ),
  active_power_l3_net_t0_max_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_w then EXCLUDED.active_power_l3_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_timestamp
  end,
  active_power_l3_net_t0_min_w = least(
    schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_w,
    EXCLUDED.active_power_l3_net_t0_min_w
  ),
  active_power_l3_net_t0_min_timestamp = case
    when EXCLUDED.active_power_l3_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_w then EXCLUDED.active_power_l3_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_timestamp
  end,
  apparent_power_total_net_t0_avg_va = (
    schneider_iem3xxx_aggregates.apparent_power_total_net_t0_avg_va * schneider_iem3xxx_aggregates.count + EXCLUDED.apparent_power_total_net_t0_avg_va * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  apparent_power_total_net_t0_max_va = greatest(
    schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_va,
    EXCLUDED.apparent_power_total_net_t0_max_va
  ),
  apparent_power_total_net_t0_max_timestamp = case
    when EXCLUDED.apparent_power_total_net_t0_max_va > schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_va then EXCLUDED.apparent_power_total_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_timestamp
  end,
  apparent_power_total_net_t0_min_va = least(
    schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_va,
    EXCLUDED.apparent_power_total_net_t0_min_va
  ),
  apparent_power_total_net_t0_min_timestamp = case
    when EXCLUDED.apparent_power_total_net_t0_min_va < schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_va then EXCLUDED.apparent_power_total_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_timestamp
  end,
  current_l1_any_t0_avg_a = (
    schneider_iem3xxx_aggregates.current_l1_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l1_any_t0_avg_a * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  current_l1_any_t0_max_a = greatest(
    schneider_iem3xxx_aggregates.current_l1_any_t0_max_a,
    EXCLUDED.current_l1_any_t0_max_a
  ),
  current_l1_any_t0_max_timestamp = case
    when EXCLUDED.current_l1_any_t0_max_a > schneider_iem3xxx_aggregates.current_l1_any_t0_max_a then EXCLUDED.current_l1_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.current_l1_any_t0_max_timestamp
  end,
  current_l1_any_t0_min_a = least(
    schneider_iem3xxx_aggregates.current_l1_any_t0_min_a,
    EXCLUDED.current_l1_any_t0_min_a
  ),
  current_l1_any_t0_min_timestamp = case
    when EXCLUDED.current_l1_any_t0_min_a < schneider_iem3xxx_aggregates.current_l1_any_t0_min_a then EXCLUDED.current_l1_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.current_l1_any_t0_min_timestamp
  end,
  current_l2_any_t0_avg_a = (
    schneider_iem3xxx_aggregates.current_l2_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l2_any_t0_avg_a * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  current_l2_any_t0_max_a = greatest(
    schneider_iem3xxx_aggregates.current_l2_any_t0_max_a,
    EXCLUDED.current_l2_any_t0_max_a
  ),
  current_l2_any_t0_max_timestamp = case
    when EXCLUDED.current_l2_any_t0_max_a > schneider_iem3xxx_aggregates.current_l2_any_t0_max_a then EXCLUDED.current_l2_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.current_l2_any_t0_max_timestamp
  end,
  current_l2_any_t0_min_a = least(
    schneider_iem3xxx_aggregates.current_l2_any_t0_min_a,
    EXCLUDED.current_l2_any_t0_min_a
  ),
  current_l2_any_t0_min_timestamp = case
    when EXCLUDED.current_l2_any_t0_min_a < schneider_iem3xxx_aggregates.current_l2_any_t0_min_a then EXCLUDED.current_l2_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.current_l2_any_t0_min_timestamp
  end,
  current_l3_any_t0_avg_a = (
    schneider_iem3xxx_aggregates.current_l3_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l3_any_t0_avg_a * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  current_l3_any_t0_max_a = greatest(
    schneider_iem3xxx_aggregates.current_l3_any_t0_max_a,
    EXCLUDED.current_l3_any_t0_max_a
  ),
  current_l3_any_t0_max_timestamp = case
    when EXCLUDED.current_l3_any_t0_max_a > schneider_iem3xxx_aggregates.current_l3_any_t0_max_a then EXCLUDED.current_l3_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.current_l3_any_t0_max_timestamp
  end,
  current_l3_any_t0_min_a = least(
    schneider_iem3xxx_aggregates.current_l3_any_t0_min_a,
    EXCLUDED.current_l3_any_t0_min_a
  ),
  current_l3_any_t0_min_timestamp = case
    when EXCLUDED.current_l3_any_t0_min_a < schneider_iem3xxx_aggregates.current_l3_any_t0_min_a then EXCLUDED.current_l3_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.current_l3_any_t0_min_timestamp
  end,
  derived_active_power_l1_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_l1_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_l1_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w,
    EXCLUDED.derived_active_power_l1_import_t0_max_w
  ),
  derived_active_power_l1_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w then EXCLUDED.derived_active_power_l1_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_timestamp
  end,
  derived_active_power_l1_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w,
    EXCLUDED.derived_active_power_l1_import_t0_min_w
  ),
  derived_active_power_l1_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l1_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w then EXCLUDED.derived_active_power_l1_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_timestamp
  end,
  derived_active_power_l2_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_l2_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_l2_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w,
    EXCLUDED.derived_active_power_l2_import_t0_max_w
  ),
  derived_active_power_l2_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w then EXCLUDED.derived_active_power_l2_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_timestamp
  end,
  derived_active_power_l2_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w,
    EXCLUDED.derived_active_power_l2_import_t0_min_w
  ),
  derived_active_power_l2_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l2_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w then EXCLUDED.derived_active_power_l2_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_timestamp
  end,
  derived_active_power_l3_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_l3_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_l3_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w,
    EXCLUDED.derived_active_power_l3_import_t0_max_w
  ),
  derived_active_power_l3_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w then EXCLUDED.derived_active_power_l3_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_timestamp
  end,
  derived_active_power_l3_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w,
    EXCLUDED.derived_active_power_l3_import_t0_min_w
  ),
  derived_active_power_l3_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_l3_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w then EXCLUDED.derived_active_power_l3_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_timestamp
  end,
  derived_active_power_total_export_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_export_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_export_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w,
    EXCLUDED.derived_active_power_total_export_t0_max_w
  ),
  derived_active_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w then EXCLUDED.derived_active_power_total_export_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_timestamp
  end,
  derived_active_power_total_export_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w,
    EXCLUDED.derived_active_power_total_export_t0_min_w
  ),
  derived_active_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_export_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w then EXCLUDED.derived_active_power_total_export_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_timestamp
  end,
  derived_active_power_total_import_t0_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_import_t0_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_import_t0_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w,
    EXCLUDED.derived_active_power_total_import_t0_max_w
  ),
  derived_active_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w then EXCLUDED.derived_active_power_total_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_timestamp
  end,
  derived_active_power_total_import_t0_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w,
    EXCLUDED.derived_active_power_total_import_t0_min_w
  ),
  derived_active_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w then EXCLUDED.derived_active_power_total_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_timestamp
  end,
  derived_active_power_total_import_t1_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_import_t1_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_import_t1_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w,
    EXCLUDED.derived_active_power_total_import_t1_max_w
  ),
  derived_active_power_total_import_t1_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w then EXCLUDED.derived_active_power_total_import_t1_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_timestamp
  end,
  derived_active_power_total_import_t1_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w,
    EXCLUDED.derived_active_power_total_import_t1_min_w
  ),
  derived_active_power_total_import_t1_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t1_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w then EXCLUDED.derived_active_power_total_import_t1_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_timestamp
  end,
  derived_active_power_total_import_t2_avg_w = (
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_active_power_total_import_t2_avg_w * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_active_power_total_import_t2_max_w = greatest(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w,
    EXCLUDED.derived_active_power_total_import_t2_max_w
  ),
  derived_active_power_total_import_t2_max_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w then EXCLUDED.derived_active_power_total_import_t2_max_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_timestamp
  end,
  derived_active_power_total_import_t2_min_w = least(
    schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w,
    EXCLUDED.derived_active_power_total_import_t2_min_w
  ),
  derived_active_power_total_import_t2_min_timestamp = case
    when EXCLUDED.derived_active_power_total_import_t2_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w then EXCLUDED.derived_active_power_total_import_t2_min_timestamp
    else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_timestamp
  end,
  derived_reactive_power_total_export_t0_avg_var = (
    schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_reactive_power_total_export_t0_avg_var * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_reactive_power_total_export_t0_max_var = greatest(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var,
    EXCLUDED.derived_reactive_power_total_export_t0_max_var
  ),
  derived_reactive_power_total_export_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var then EXCLUDED.derived_reactive_power_total_export_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_timestamp
  end,
  derived_reactive_power_total_export_t0_min_var = least(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var,
    EXCLUDED.derived_reactive_power_total_export_t0_min_var
  ),
  derived_reactive_power_total_export_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_export_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var then EXCLUDED.derived_reactive_power_total_export_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_timestamp
  end,
  derived_reactive_power_total_import_t0_avg_var = (
    schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.derived_reactive_power_total_import_t0_avg_var * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  derived_reactive_power_total_import_t0_max_var = greatest(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var,
    EXCLUDED.derived_reactive_power_total_import_t0_max_var
  ),
  derived_reactive_power_total_import_t0_max_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var then EXCLUDED.derived_reactive_power_total_import_t0_max_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_timestamp
  end,
  derived_reactive_power_total_import_t0_min_var = least(
    schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var,
    EXCLUDED.derived_reactive_power_total_import_t0_min_var
  ),
  derived_reactive_power_total_import_t0_min_timestamp = case
    when EXCLUDED.derived_reactive_power_total_import_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var then EXCLUDED.derived_reactive_power_total_import_t0_min_timestamp
    else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_timestamp
  end,
  reactive_energy_total_export_t0_max_varh = greatest(
    schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_max_varh,
    EXCLUDED.reactive_energy_total_export_t0_max_varh
  ),
  reactive_energy_total_export_t0_min_varh = least(
    schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_min_varh,
    EXCLUDED.reactive_energy_total_export_t0_min_varh
  ),
  reactive_energy_total_import_t0_max_varh = greatest(
    schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_max_varh,
    EXCLUDED.reactive_energy_total_import_t0_max_varh
  ),
  reactive_energy_total_import_t0_min_varh = least(
    schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_min_varh,
    EXCLUDED.reactive_energy_total_import_t0_min_varh
  ),
  reactive_power_total_net_t0_avg_var = (
    schneider_iem3xxx_aggregates.reactive_power_total_net_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.reactive_power_total_net_t0_avg_var * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  reactive_power_total_net_t0_max_var = greatest(
    schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_var,
    EXCLUDED.reactive_power_total_net_t0_max_var
  ),
  reactive_power_total_net_t0_max_timestamp = case
    when EXCLUDED.reactive_power_total_net_t0_max_var > schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_var then EXCLUDED.reactive_power_total_net_t0_max_timestamp
    else schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_timestamp
  end,
  reactive_power_total_net_t0_min_var = least(
    schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_var,
    EXCLUDED.reactive_power_total_net_t0_min_var
  ),
  reactive_power_total_net_t0_min_timestamp = case
    when EXCLUDED.reactive_power_total_net_t0_min_var < schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_var then EXCLUDED.reactive_power_total_net_t0_min_timestamp
    else schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_timestamp
  end,
  voltage_l1_any_t0_avg_v = (
    schneider_iem3xxx_aggregates.voltage_l1_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l1_any_t0_avg_v * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  voltage_l1_any_t0_max_v = greatest(
    schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_v,
    EXCLUDED.voltage_l1_any_t0_max_v
  ),
  voltage_l1_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_v then EXCLUDED.voltage_l1_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_timestamp
  end,
  voltage_l1_any_t0_min_v = least(
    schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_v,
    EXCLUDED.voltage_l1_any_t0_min_v
  ),
  voltage_l1_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l1_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_v then EXCLUDED.voltage_l1_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_timestamp
  end,
  voltage_l2_any_t0_avg_v = (
    schneider_iem3xxx_aggregates.voltage_l2_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l2_any_t0_avg_v * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  voltage_l2_any_t0_max_v = greatest(
    schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_v,
    EXCLUDED.voltage_l2_any_t0_max_v
  ),
  voltage_l2_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_v then EXCLUDED.voltage_l2_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_timestamp
  end,
  voltage_l2_any_t0_min_v = least(
    schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_v,
    EXCLUDED.voltage_l2_any_t0_min_v
  ),
  voltage_l2_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l2_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_v then EXCLUDED.voltage_l2_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_timestamp
  end,
  voltage_l3_any_t0_avg_v = (
    schneider_iem3xxx_aggregates.voltage_l3_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l3_any_t0_avg_v * EXCLUDED.count
  ) / (
    schneider_iem3xxx_aggregates.count + EXCLUDED.count
  ),
  voltage_l3_any_t0_max_v = greatest(
    schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_v,
    EXCLUDED.voltage_l3_any_t0_max_v
  ),
  voltage_l3_any_t0_max_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_v then EXCLUDED.voltage_l3_any_t0_max_timestamp
    else schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_timestamp
  end,
  voltage_l3_any_t0_min_v = least(
    schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_v,
    EXCLUDED.voltage_l3_any_t0_min_v
  ),
  voltage_l3_any_t0_min_timestamp = case
    when EXCLUDED.voltage_l3_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_v then EXCLUDED.voltage_l3_any_t0_min_timestamp
    else schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_timestamp
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
      quarter_hour_count = 1,
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
      *
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
      *
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
      *
  )
select
  *
from
  daily
union all
select
  *
from
  monthly;

with
  old as (
    select
      abb_b2x_aggregates.*
    from
      abb_b2x_aggregates
    where
      abb_b2x_aggregates.timestamp =:'p31_timestamp'::timestamptz
      and abb_b2x_aggregates.interval =:'p31_interval'::interval_entity
      and abb_b2x_aggregates.meter_id =:'p31_meter_id'
      and abb_b2x_aggregates.measurement_location_id =:'p31_measurement_location_id'
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
:'p31_timestamp'::timestamptz,
:'p31_interval'::interval_entity,
:'p31_meter_id',
:'p31_measurement_location_id',
:'p31_count',
:'p31_quarter_hour_count',
:'p31_active_energy_l1_export_t0_max_wh',
:'p31_active_energy_l1_export_t0_min_wh',
:'p31_active_energy_l1_import_t0_max_wh',
:'p31_active_energy_l1_import_t0_min_wh',
:'p31_active_energy_l2_export_t0_max_wh',
:'p31_active_energy_l2_export_t0_min_wh',
:'p31_active_energy_l2_import_t0_max_wh',
:'p31_active_energy_l2_import_t0_min_wh',
:'p31_active_energy_l3_export_t0_max_wh',
:'p31_active_energy_l3_export_t0_min_wh',
:'p31_active_energy_l3_import_t0_max_wh',
:'p31_active_energy_l3_import_t0_min_wh',
:'p31_active_energy_total_export_t0_max_wh',
:'p31_active_energy_total_export_t0_min_wh',
:'p31_active_energy_total_import_t0_max_wh',
:'p31_active_energy_total_import_t0_min_wh',
:'p31_active_energy_total_import_t1_max_wh',
:'p31_active_energy_total_import_t1_min_wh',
:'p31_active_energy_total_import_t2_max_wh',
:'p31_active_energy_total_import_t2_min_wh',
:'p31_active_power_l1_net_t0_avg_w',
:'p31_active_power_l1_net_t0_max_w',
:'p31_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p31_active_power_l1_net_t0_min_w',
:'p31_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p31_active_power_l2_net_t0_avg_w',
:'p31_active_power_l2_net_t0_max_w',
:'p31_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p31_active_power_l2_net_t0_min_w',
:'p31_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p31_active_power_l3_net_t0_avg_w',
:'p31_active_power_l3_net_t0_max_w',
:'p31_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p31_active_power_l3_net_t0_min_w',
:'p31_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p31_current_l1_any_t0_avg_a',
:'p31_current_l1_any_t0_max_a',
:'p31_current_l1_any_t0_max_timestamp'::timestamptz,
:'p31_current_l1_any_t0_min_a',
:'p31_current_l1_any_t0_min_timestamp'::timestamptz,
:'p31_current_l2_any_t0_avg_a',
:'p31_current_l2_any_t0_max_a',
:'p31_current_l2_any_t0_max_timestamp'::timestamptz,
:'p31_current_l2_any_t0_min_a',
:'p31_current_l2_any_t0_min_timestamp'::timestamptz,
:'p31_current_l3_any_t0_avg_a',
:'p31_current_l3_any_t0_max_a',
:'p31_current_l3_any_t0_max_timestamp'::timestamptz,
:'p31_current_l3_any_t0_min_a',
:'p31_current_l3_any_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_l1_export_t0_avg_w',
:'p31_derived_active_power_l1_export_t0_max_w',
:'p31_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_l1_export_t0_min_w',
:'p31_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_l1_import_t0_avg_w',
:'p31_derived_active_power_l1_import_t0_max_w',
:'p31_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_l1_import_t0_min_w',
:'p31_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_l2_export_t0_avg_w',
:'p31_derived_active_power_l2_export_t0_max_w',
:'p31_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_l2_export_t0_min_w',
:'p31_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_l2_import_t0_avg_w',
:'p31_derived_active_power_l2_import_t0_max_w',
:'p31_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_l2_import_t0_min_w',
:'p31_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_l3_export_t0_avg_w',
:'p31_derived_active_power_l3_export_t0_max_w',
:'p31_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_l3_export_t0_min_w',
:'p31_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_l3_import_t0_avg_w',
:'p31_derived_active_power_l3_import_t0_max_w',
:'p31_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_l3_import_t0_min_w',
:'p31_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_total_export_t0_avg_w',
:'p31_derived_active_power_total_export_t0_max_w',
:'p31_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_total_export_t0_min_w',
:'p31_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_total_import_t0_avg_w',
:'p31_derived_active_power_total_import_t0_max_w',
:'p31_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p31_derived_active_power_total_import_t0_min_w',
:'p31_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p31_derived_active_power_total_import_t1_avg_w',
:'p31_derived_active_power_total_import_t1_max_w',
:'p31_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p31_derived_active_power_total_import_t1_min_w',
:'p31_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p31_derived_active_power_total_import_t2_avg_w',
:'p31_derived_active_power_total_import_t2_max_w',
:'p31_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p31_derived_active_power_total_import_t2_min_w',
:'p31_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_l1_export_t0_avg_var',
:'p31_derived_reactive_power_l1_export_t0_max_var',
:'p31_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_l1_export_t0_min_var',
:'p31_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_l1_import_t0_avg_var',
:'p31_derived_reactive_power_l1_import_t0_max_var',
:'p31_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_l1_import_t0_min_var',
:'p31_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_l2_export_t0_avg_var',
:'p31_derived_reactive_power_l2_export_t0_max_var',
:'p31_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_l2_export_t0_min_var',
:'p31_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_l2_import_t0_avg_var',
:'p31_derived_reactive_power_l2_import_t0_max_var',
:'p31_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_l2_import_t0_min_var',
:'p31_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_l3_export_t0_avg_var',
:'p31_derived_reactive_power_l3_export_t0_max_var',
:'p31_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_l3_export_t0_min_var',
:'p31_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_l3_import_t0_avg_var',
:'p31_derived_reactive_power_l3_import_t0_max_var',
:'p31_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_l3_import_t0_min_var',
:'p31_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_total_export_t0_avg_var',
:'p31_derived_reactive_power_total_export_t0_max_var',
:'p31_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_total_export_t0_min_var',
:'p31_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p31_derived_reactive_power_total_import_t0_avg_var',
:'p31_derived_reactive_power_total_import_t0_max_var',
:'p31_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p31_derived_reactive_power_total_import_t0_min_var',
:'p31_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p31_reactive_energy_l1_export_t0_max_varh',
:'p31_reactive_energy_l1_export_t0_min_varh',
:'p31_reactive_energy_l1_import_t0_max_varh',
:'p31_reactive_energy_l1_import_t0_min_varh',
:'p31_reactive_energy_l2_export_t0_max_varh',
:'p31_reactive_energy_l2_export_t0_min_varh',
:'p31_reactive_energy_l2_import_t0_max_varh',
:'p31_reactive_energy_l2_import_t0_min_varh',
:'p31_reactive_energy_l3_export_t0_max_varh',
:'p31_reactive_energy_l3_export_t0_min_varh',
:'p31_reactive_energy_l3_import_t0_max_varh',
:'p31_reactive_energy_l3_import_t0_min_varh',
:'p31_reactive_energy_total_export_t0_max_varh',
:'p31_reactive_energy_total_export_t0_min_varh',
:'p31_reactive_energy_total_import_t0_max_varh',
:'p31_reactive_energy_total_import_t0_min_varh',
:'p31_reactive_power_l1_net_t0_avg_var',
:'p31_reactive_power_l1_net_t0_max_var',
:'p31_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p31_reactive_power_l1_net_t0_min_var',
:'p31_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p31_reactive_power_l2_net_t0_avg_var',
:'p31_reactive_power_l2_net_t0_max_var',
:'p31_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p31_reactive_power_l2_net_t0_min_var',
:'p31_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p31_reactive_power_l3_net_t0_avg_var',
:'p31_reactive_power_l3_net_t0_max_var',
:'p31_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p31_reactive_power_l3_net_t0_min_var',
:'p31_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p31_voltage_l1_any_t0_avg_v',
:'p31_voltage_l1_any_t0_max_v',
:'p31_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p31_voltage_l1_any_t0_min_v',
:'p31_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p31_voltage_l2_any_t0_avg_v',
:'p31_voltage_l2_any_t0_max_v',
:'p31_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p31_voltage_l2_any_t0_min_v',
:'p31_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p31_voltage_l3_any_t0_avg_v',
:'p31_voltage_l3_any_t0_max_v',
:'p31_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p31_voltage_l3_any_t0_min_v',
:'p31_voltage_l3_any_t0_min_timestamp'::timestamptz
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
      quarter_hour_count = 1,
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
      *
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
      *
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
      *
  )
select
  *
from
  daily
union all
select
  *
from
  monthly;

with
  old as (
    select
      abb_b2x_aggregates.*
    from
      abb_b2x_aggregates
    where
      abb_b2x_aggregates.timestamp =:'p34_timestamp'::timestamptz
      and abb_b2x_aggregates.interval =:'p34_interval'::interval_entity
      and abb_b2x_aggregates.meter_id =:'p34_meter_id'
      and abb_b2x_aggregates.measurement_location_id =:'p34_measurement_location_id'
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
:'p34_timestamp'::timestamptz,
:'p34_interval'::interval_entity,
:'p34_meter_id',
:'p34_measurement_location_id',
:'p34_count',
:'p34_quarter_hour_count',
:'p34_active_energy_l1_export_t0_max_wh',
:'p34_active_energy_l1_export_t0_min_wh',
:'p34_active_energy_l1_import_t0_max_wh',
:'p34_active_energy_l1_import_t0_min_wh',
:'p34_active_energy_l2_export_t0_max_wh',
:'p34_active_energy_l2_export_t0_min_wh',
:'p34_active_energy_l2_import_t0_max_wh',
:'p34_active_energy_l2_import_t0_min_wh',
:'p34_active_energy_l3_export_t0_max_wh',
:'p34_active_energy_l3_export_t0_min_wh',
:'p34_active_energy_l3_import_t0_max_wh',
:'p34_active_energy_l3_import_t0_min_wh',
:'p34_active_energy_total_export_t0_max_wh',
:'p34_active_energy_total_export_t0_min_wh',
:'p34_active_energy_total_import_t0_max_wh',
:'p34_active_energy_total_import_t0_min_wh',
:'p34_active_energy_total_import_t1_max_wh',
:'p34_active_energy_total_import_t1_min_wh',
:'p34_active_energy_total_import_t2_max_wh',
:'p34_active_energy_total_import_t2_min_wh',
:'p34_active_power_l1_net_t0_avg_w',
:'p34_active_power_l1_net_t0_max_w',
:'p34_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p34_active_power_l1_net_t0_min_w',
:'p34_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p34_active_power_l2_net_t0_avg_w',
:'p34_active_power_l2_net_t0_max_w',
:'p34_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p34_active_power_l2_net_t0_min_w',
:'p34_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p34_active_power_l3_net_t0_avg_w',
:'p34_active_power_l3_net_t0_max_w',
:'p34_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p34_active_power_l3_net_t0_min_w',
:'p34_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p34_current_l1_any_t0_avg_a',
:'p34_current_l1_any_t0_max_a',
:'p34_current_l1_any_t0_max_timestamp'::timestamptz,
:'p34_current_l1_any_t0_min_a',
:'p34_current_l1_any_t0_min_timestamp'::timestamptz,
:'p34_current_l2_any_t0_avg_a',
:'p34_current_l2_any_t0_max_a',
:'p34_current_l2_any_t0_max_timestamp'::timestamptz,
:'p34_current_l2_any_t0_min_a',
:'p34_current_l2_any_t0_min_timestamp'::timestamptz,
:'p34_current_l3_any_t0_avg_a',
:'p34_current_l3_any_t0_max_a',
:'p34_current_l3_any_t0_max_timestamp'::timestamptz,
:'p34_current_l3_any_t0_min_a',
:'p34_current_l3_any_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_l1_export_t0_avg_w',
:'p34_derived_active_power_l1_export_t0_max_w',
:'p34_derived_active_power_l1_export_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_l1_export_t0_min_w',
:'p34_derived_active_power_l1_export_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_l1_import_t0_avg_w',
:'p34_derived_active_power_l1_import_t0_max_w',
:'p34_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_l1_import_t0_min_w',
:'p34_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_l2_export_t0_avg_w',
:'p34_derived_active_power_l2_export_t0_max_w',
:'p34_derived_active_power_l2_export_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_l2_export_t0_min_w',
:'p34_derived_active_power_l2_export_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_l2_import_t0_avg_w',
:'p34_derived_active_power_l2_import_t0_max_w',
:'p34_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_l2_import_t0_min_w',
:'p34_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_l3_export_t0_avg_w',
:'p34_derived_active_power_l3_export_t0_max_w',
:'p34_derived_active_power_l3_export_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_l3_export_t0_min_w',
:'p34_derived_active_power_l3_export_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_l3_import_t0_avg_w',
:'p34_derived_active_power_l3_import_t0_max_w',
:'p34_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_l3_import_t0_min_w',
:'p34_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_total_export_t0_avg_w',
:'p34_derived_active_power_total_export_t0_max_w',
:'p34_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_total_export_t0_min_w',
:'p34_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_total_import_t0_avg_w',
:'p34_derived_active_power_total_import_t0_max_w',
:'p34_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p34_derived_active_power_total_import_t0_min_w',
:'p34_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p34_derived_active_power_total_import_t1_avg_w',
:'p34_derived_active_power_total_import_t1_max_w',
:'p34_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p34_derived_active_power_total_import_t1_min_w',
:'p34_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p34_derived_active_power_total_import_t2_avg_w',
:'p34_derived_active_power_total_import_t2_max_w',
:'p34_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p34_derived_active_power_total_import_t2_min_w',
:'p34_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_l1_export_t0_avg_var',
:'p34_derived_reactive_power_l1_export_t0_max_var',
:'p34_derived_reactive_power_l1_export_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_l1_export_t0_min_var',
:'p34_derived_reactive_power_l1_export_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_l1_import_t0_avg_var',
:'p34_derived_reactive_power_l1_import_t0_max_var',
:'p34_derived_reactive_power_l1_import_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_l1_import_t0_min_var',
:'p34_derived_reactive_power_l1_import_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_l2_export_t0_avg_var',
:'p34_derived_reactive_power_l2_export_t0_max_var',
:'p34_derived_reactive_power_l2_export_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_l2_export_t0_min_var',
:'p34_derived_reactive_power_l2_export_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_l2_import_t0_avg_var',
:'p34_derived_reactive_power_l2_import_t0_max_var',
:'p34_derived_reactive_power_l2_import_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_l2_import_t0_min_var',
:'p34_derived_reactive_power_l2_import_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_l3_export_t0_avg_var',
:'p34_derived_reactive_power_l3_export_t0_max_var',
:'p34_derived_reactive_power_l3_export_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_l3_export_t0_min_var',
:'p34_derived_reactive_power_l3_export_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_l3_import_t0_avg_var',
:'p34_derived_reactive_power_l3_import_t0_max_var',
:'p34_derived_reactive_power_l3_import_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_l3_import_t0_min_var',
:'p34_derived_reactive_power_l3_import_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_total_export_t0_avg_var',
:'p34_derived_reactive_power_total_export_t0_max_var',
:'p34_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_total_export_t0_min_var',
:'p34_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p34_derived_reactive_power_total_import_t0_avg_var',
:'p34_derived_reactive_power_total_import_t0_max_var',
:'p34_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p34_derived_reactive_power_total_import_t0_min_var',
:'p34_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p34_reactive_energy_l1_export_t0_max_varh',
:'p34_reactive_energy_l1_export_t0_min_varh',
:'p34_reactive_energy_l1_import_t0_max_varh',
:'p34_reactive_energy_l1_import_t0_min_varh',
:'p34_reactive_energy_l2_export_t0_max_varh',
:'p34_reactive_energy_l2_export_t0_min_varh',
:'p34_reactive_energy_l2_import_t0_max_varh',
:'p34_reactive_energy_l2_import_t0_min_varh',
:'p34_reactive_energy_l3_export_t0_max_varh',
:'p34_reactive_energy_l3_export_t0_min_varh',
:'p34_reactive_energy_l3_import_t0_max_varh',
:'p34_reactive_energy_l3_import_t0_min_varh',
:'p34_reactive_energy_total_export_t0_max_varh',
:'p34_reactive_energy_total_export_t0_min_varh',
:'p34_reactive_energy_total_import_t0_max_varh',
:'p34_reactive_energy_total_import_t0_min_varh',
:'p34_reactive_power_l1_net_t0_avg_var',
:'p34_reactive_power_l1_net_t0_max_var',
:'p34_reactive_power_l1_net_t0_max_timestamp'::timestamptz,
:'p34_reactive_power_l1_net_t0_min_var',
:'p34_reactive_power_l1_net_t0_min_timestamp'::timestamptz,
:'p34_reactive_power_l2_net_t0_avg_var',
:'p34_reactive_power_l2_net_t0_max_var',
:'p34_reactive_power_l2_net_t0_max_timestamp'::timestamptz,
:'p34_reactive_power_l2_net_t0_min_var',
:'p34_reactive_power_l2_net_t0_min_timestamp'::timestamptz,
:'p34_reactive_power_l3_net_t0_avg_var',
:'p34_reactive_power_l3_net_t0_max_var',
:'p34_reactive_power_l3_net_t0_max_timestamp'::timestamptz,
:'p34_reactive_power_l3_net_t0_min_var',
:'p34_reactive_power_l3_net_t0_min_timestamp'::timestamptz,
:'p34_voltage_l1_any_t0_avg_v',
:'p34_voltage_l1_any_t0_max_v',
:'p34_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p34_voltage_l1_any_t0_min_v',
:'p34_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p34_voltage_l2_any_t0_avg_v',
:'p34_voltage_l2_any_t0_max_v',
:'p34_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p34_voltage_l2_any_t0_min_v',
:'p34_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p34_voltage_l3_any_t0_avg_v',
:'p34_voltage_l3_any_t0_max_v',
:'p34_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p34_voltage_l3_any_t0_min_v',
:'p34_voltage_l3_any_t0_min_timestamp'::timestamptz
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
      quarter_hour_count = 1,
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
      *
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
      *
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
      *
  )
select
  *
from
  daily
union all
select
  *
from
  monthly;

with
  old as (
    select
      schneider_iem3xxx_aggregates.*
    from
      schneider_iem3xxx_aggregates
    where
      schneider_iem3xxx_aggregates.timestamp =:'p37_timestamp'::timestamptz
      and schneider_iem3xxx_aggregates.interval =:'p37_interval'::interval_entity
      and schneider_iem3xxx_aggregates.meter_id =:'p37_meter_id'
      and schneider_iem3xxx_aggregates.measurement_location_id =:'p37_measurement_location_id'
  ),
  new as (
    insert into
      schneider_iem3xxx_aggregates (
        timestamp,
        interval,
        meter_id,
        measurement_location_id,
        count,
        quarter_hour_count,
        active_energy_l1_import_t0_max_wh,
        active_energy_l1_import_t0_min_wh,
        active_energy_l2_import_t0_max_wh,
        active_energy_l2_import_t0_min_wh,
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
        apparent_power_total_net_t0_avg_va,
        apparent_power_total_net_t0_max_va,
        apparent_power_total_net_t0_max_timestamp,
        apparent_power_total_net_t0_min_va,
        apparent_power_total_net_t0_min_timestamp,
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
        derived_active_power_l1_import_t0_avg_w,
        derived_active_power_l1_import_t0_max_w,
        derived_active_power_l1_import_t0_max_timestamp,
        derived_active_power_l1_import_t0_min_w,
        derived_active_power_l1_import_t0_min_timestamp,
        derived_active_power_l2_import_t0_avg_w,
        derived_active_power_l2_import_t0_max_w,
        derived_active_power_l2_import_t0_max_timestamp,
        derived_active_power_l2_import_t0_min_w,
        derived_active_power_l2_import_t0_min_timestamp,
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
        reactive_energy_total_export_t0_max_varh,
        reactive_energy_total_export_t0_min_varh,
        reactive_energy_total_import_t0_max_varh,
        reactive_energy_total_import_t0_min_varh,
        reactive_power_total_net_t0_avg_var,
        reactive_power_total_net_t0_max_var,
        reactive_power_total_net_t0_max_timestamp,
        reactive_power_total_net_t0_min_var,
        reactive_power_total_net_t0_min_timestamp,
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
:'p37_timestamp'::timestamptz,
:'p37_interval'::interval_entity,
:'p37_meter_id',
:'p37_measurement_location_id',
:'p37_count',
:'p37_quarter_hour_count',
:'p37_active_energy_l1_import_t0_max_wh',
:'p37_active_energy_l1_import_t0_min_wh',
:'p37_active_energy_l2_import_t0_max_wh',
:'p37_active_energy_l2_import_t0_min_wh',
:'p37_active_energy_l3_import_t0_max_wh',
:'p37_active_energy_l3_import_t0_min_wh',
:'p37_active_energy_total_export_t0_max_wh',
:'p37_active_energy_total_export_t0_min_wh',
:'p37_active_energy_total_import_t0_max_wh',
:'p37_active_energy_total_import_t0_min_wh',
:'p37_active_energy_total_import_t1_max_wh',
:'p37_active_energy_total_import_t1_min_wh',
:'p37_active_energy_total_import_t2_max_wh',
:'p37_active_energy_total_import_t2_min_wh',
:'p37_active_power_l1_net_t0_avg_w',
:'p37_active_power_l1_net_t0_max_w',
:'p37_active_power_l1_net_t0_max_timestamp'::timestamptz,
:'p37_active_power_l1_net_t0_min_w',
:'p37_active_power_l1_net_t0_min_timestamp'::timestamptz,
:'p37_active_power_l2_net_t0_avg_w',
:'p37_active_power_l2_net_t0_max_w',
:'p37_active_power_l2_net_t0_max_timestamp'::timestamptz,
:'p37_active_power_l2_net_t0_min_w',
:'p37_active_power_l2_net_t0_min_timestamp'::timestamptz,
:'p37_active_power_l3_net_t0_avg_w',
:'p37_active_power_l3_net_t0_max_w',
:'p37_active_power_l3_net_t0_max_timestamp'::timestamptz,
:'p37_active_power_l3_net_t0_min_w',
:'p37_active_power_l3_net_t0_min_timestamp'::timestamptz,
:'p37_apparent_power_total_net_t0_avg_va',
:'p37_apparent_power_total_net_t0_max_va',
:'p37_apparent_power_total_net_t0_max_timestamp'::timestamptz,
:'p37_apparent_power_total_net_t0_min_va',
:'p37_apparent_power_total_net_t0_min_timestamp'::timestamptz,
:'p37_current_l1_any_t0_avg_a',
:'p37_current_l1_any_t0_max_a',
:'p37_current_l1_any_t0_max_timestamp'::timestamptz,
:'p37_current_l1_any_t0_min_a',
:'p37_current_l1_any_t0_min_timestamp'::timestamptz,
:'p37_current_l2_any_t0_avg_a',
:'p37_current_l2_any_t0_max_a',
:'p37_current_l2_any_t0_max_timestamp'::timestamptz,
:'p37_current_l2_any_t0_min_a',
:'p37_current_l2_any_t0_min_timestamp'::timestamptz,
:'p37_current_l3_any_t0_avg_a',
:'p37_current_l3_any_t0_max_a',
:'p37_current_l3_any_t0_max_timestamp'::timestamptz,
:'p37_current_l3_any_t0_min_a',
:'p37_current_l3_any_t0_min_timestamp'::timestamptz,
:'p37_derived_active_power_l1_import_t0_avg_w',
:'p37_derived_active_power_l1_import_t0_max_w',
:'p37_derived_active_power_l1_import_t0_max_timestamp'::timestamptz,
:'p37_derived_active_power_l1_import_t0_min_w',
:'p37_derived_active_power_l1_import_t0_min_timestamp'::timestamptz,
:'p37_derived_active_power_l2_import_t0_avg_w',
:'p37_derived_active_power_l2_import_t0_max_w',
:'p37_derived_active_power_l2_import_t0_max_timestamp'::timestamptz,
:'p37_derived_active_power_l2_import_t0_min_w',
:'p37_derived_active_power_l2_import_t0_min_timestamp'::timestamptz,
:'p37_derived_active_power_l3_import_t0_avg_w',
:'p37_derived_active_power_l3_import_t0_max_w',
:'p37_derived_active_power_l3_import_t0_max_timestamp'::timestamptz,
:'p37_derived_active_power_l3_import_t0_min_w',
:'p37_derived_active_power_l3_import_t0_min_timestamp'::timestamptz,
:'p37_derived_active_power_total_export_t0_avg_w',
:'p37_derived_active_power_total_export_t0_max_w',
:'p37_derived_active_power_total_export_t0_max_timestamp'::timestamptz,
:'p37_derived_active_power_total_export_t0_min_w',
:'p37_derived_active_power_total_export_t0_min_timestamp'::timestamptz,
:'p37_derived_active_power_total_import_t0_avg_w',
:'p37_derived_active_power_total_import_t0_max_w',
:'p37_derived_active_power_total_import_t0_max_timestamp'::timestamptz,
:'p37_derived_active_power_total_import_t0_min_w',
:'p37_derived_active_power_total_import_t0_min_timestamp'::timestamptz,
:'p37_derived_active_power_total_import_t1_avg_w',
:'p37_derived_active_power_total_import_t1_max_w',
:'p37_derived_active_power_total_import_t1_max_timestamp'::timestamptz,
:'p37_derived_active_power_total_import_t1_min_w',
:'p37_derived_active_power_total_import_t1_min_timestamp'::timestamptz,
:'p37_derived_active_power_total_import_t2_avg_w',
:'p37_derived_active_power_total_import_t2_max_w',
:'p37_derived_active_power_total_import_t2_max_timestamp'::timestamptz,
:'p37_derived_active_power_total_import_t2_min_w',
:'p37_derived_active_power_total_import_t2_min_timestamp'::timestamptz,
:'p37_derived_reactive_power_total_export_t0_avg_var',
:'p37_derived_reactive_power_total_export_t0_max_var',
:'p37_derived_reactive_power_total_export_t0_max_timestamp'::timestamptz,
:'p37_derived_reactive_power_total_export_t0_min_var',
:'p37_derived_reactive_power_total_export_t0_min_timestamp'::timestamptz,
:'p37_derived_reactive_power_total_import_t0_avg_var',
:'p37_derived_reactive_power_total_import_t0_max_var',
:'p37_derived_reactive_power_total_import_t0_max_timestamp'::timestamptz,
:'p37_derived_reactive_power_total_import_t0_min_var',
:'p37_derived_reactive_power_total_import_t0_min_timestamp'::timestamptz,
:'p37_reactive_energy_total_export_t0_max_varh',
:'p37_reactive_energy_total_export_t0_min_varh',
:'p37_reactive_energy_total_import_t0_max_varh',
:'p37_reactive_energy_total_import_t0_min_varh',
:'p37_reactive_power_total_net_t0_avg_var',
:'p37_reactive_power_total_net_t0_max_var',
:'p37_reactive_power_total_net_t0_max_timestamp'::timestamptz,
:'p37_reactive_power_total_net_t0_min_var',
:'p37_reactive_power_total_net_t0_min_timestamp'::timestamptz,
:'p37_voltage_l1_any_t0_avg_v',
:'p37_voltage_l1_any_t0_max_v',
:'p37_voltage_l1_any_t0_max_timestamp'::timestamptz,
:'p37_voltage_l1_any_t0_min_v',
:'p37_voltage_l1_any_t0_min_timestamp'::timestamptz,
:'p37_voltage_l2_any_t0_avg_v',
:'p37_voltage_l2_any_t0_max_v',
:'p37_voltage_l2_any_t0_max_timestamp'::timestamptz,
:'p37_voltage_l2_any_t0_min_v',
:'p37_voltage_l2_any_t0_min_timestamp'::timestamptz,
:'p37_voltage_l3_any_t0_avg_v',
:'p37_voltage_l3_any_t0_max_v',
:'p37_voltage_l3_any_t0_max_timestamp'::timestamptz,
:'p37_voltage_l3_any_t0_min_v',
:'p37_voltage_l3_any_t0_min_timestamp'::timestamptz
      )
    on conflict (
      timestamp,
      interval,
      meter_id,
      measurement_location_id
    ) do
    update
    set
      count = schneider_iem3xxx_aggregates.count + EXCLUDED.count,
      quarter_hour_count = 1,
      active_energy_l1_import_t0_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_l1_import_t0_max_wh,
        EXCLUDED.active_energy_l1_import_t0_max_wh
      ),
      active_energy_l1_import_t0_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_l1_import_t0_min_wh,
        EXCLUDED.active_energy_l1_import_t0_min_wh
      ),
      active_energy_l2_import_t0_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_l2_import_t0_max_wh,
        EXCLUDED.active_energy_l2_import_t0_max_wh
      ),
      active_energy_l2_import_t0_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_l2_import_t0_min_wh,
        EXCLUDED.active_energy_l2_import_t0_min_wh
      ),
      active_energy_l3_import_t0_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_l3_import_t0_max_wh,
        EXCLUDED.active_energy_l3_import_t0_max_wh
      ),
      active_energy_l3_import_t0_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_l3_import_t0_min_wh,
        EXCLUDED.active_energy_l3_import_t0_min_wh
      ),
      active_energy_total_export_t0_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_total_export_t0_max_wh,
        EXCLUDED.active_energy_total_export_t0_max_wh
      ),
      active_energy_total_export_t0_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_total_export_t0_min_wh,
        EXCLUDED.active_energy_total_export_t0_min_wh
      ),
      active_energy_total_import_t0_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_total_import_t0_max_wh,
        EXCLUDED.active_energy_total_import_t0_max_wh
      ),
      active_energy_total_import_t0_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_total_import_t0_min_wh,
        EXCLUDED.active_energy_total_import_t0_min_wh
      ),
      active_energy_total_import_t1_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_total_import_t1_max_wh,
        EXCLUDED.active_energy_total_import_t1_max_wh
      ),
      active_energy_total_import_t1_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_total_import_t1_min_wh,
        EXCLUDED.active_energy_total_import_t1_min_wh
      ),
      active_energy_total_import_t2_max_wh = greatest(
        schneider_iem3xxx_aggregates.active_energy_total_import_t2_max_wh,
        EXCLUDED.active_energy_total_import_t2_max_wh
      ),
      active_energy_total_import_t2_min_wh = least(
        schneider_iem3xxx_aggregates.active_energy_total_import_t2_min_wh,
        EXCLUDED.active_energy_total_import_t2_min_wh
      ),
      active_power_l1_net_t0_avg_w = (
        schneider_iem3xxx_aggregates.active_power_l1_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l1_net_t0_avg_w * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      active_power_l1_net_t0_max_w = greatest(
        schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_w,
        EXCLUDED.active_power_l1_net_t0_max_w
      ),
      active_power_l1_net_t0_max_timestamp = case
        when EXCLUDED.active_power_l1_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_w then EXCLUDED.active_power_l1_net_t0_max_timestamp
        else schneider_iem3xxx_aggregates.active_power_l1_net_t0_max_timestamp
      end,
      active_power_l1_net_t0_min_w = least(
        schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_w,
        EXCLUDED.active_power_l1_net_t0_min_w
      ),
      active_power_l1_net_t0_min_timestamp = case
        when EXCLUDED.active_power_l1_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_w then EXCLUDED.active_power_l1_net_t0_min_timestamp
        else schneider_iem3xxx_aggregates.active_power_l1_net_t0_min_timestamp
      end,
      active_power_l2_net_t0_avg_w = (
        schneider_iem3xxx_aggregates.active_power_l2_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l2_net_t0_avg_w * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      active_power_l2_net_t0_max_w = greatest(
        schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_w,
        EXCLUDED.active_power_l2_net_t0_max_w
      ),
      active_power_l2_net_t0_max_timestamp = case
        when EXCLUDED.active_power_l2_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_w then EXCLUDED.active_power_l2_net_t0_max_timestamp
        else schneider_iem3xxx_aggregates.active_power_l2_net_t0_max_timestamp
      end,
      active_power_l2_net_t0_min_w = least(
        schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_w,
        EXCLUDED.active_power_l2_net_t0_min_w
      ),
      active_power_l2_net_t0_min_timestamp = case
        when EXCLUDED.active_power_l2_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_w then EXCLUDED.active_power_l2_net_t0_min_timestamp
        else schneider_iem3xxx_aggregates.active_power_l2_net_t0_min_timestamp
      end,
      active_power_l3_net_t0_avg_w = (
        schneider_iem3xxx_aggregates.active_power_l3_net_t0_avg_w * schneider_iem3xxx_aggregates.count + EXCLUDED.active_power_l3_net_t0_avg_w * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      active_power_l3_net_t0_max_w = greatest(
        schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_w,
        EXCLUDED.active_power_l3_net_t0_max_w
      ),
      active_power_l3_net_t0_max_timestamp = case
        when EXCLUDED.active_power_l3_net_t0_max_w > schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_w then EXCLUDED.active_power_l3_net_t0_max_timestamp
        else schneider_iem3xxx_aggregates.active_power_l3_net_t0_max_timestamp
      end,
      active_power_l3_net_t0_min_w = least(
        schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_w,
        EXCLUDED.active_power_l3_net_t0_min_w
      ),
      active_power_l3_net_t0_min_timestamp = case
        when EXCLUDED.active_power_l3_net_t0_min_w < schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_w then EXCLUDED.active_power_l3_net_t0_min_timestamp
        else schneider_iem3xxx_aggregates.active_power_l3_net_t0_min_timestamp
      end,
      apparent_power_total_net_t0_avg_va = (
        schneider_iem3xxx_aggregates.apparent_power_total_net_t0_avg_va * schneider_iem3xxx_aggregates.count + EXCLUDED.apparent_power_total_net_t0_avg_va * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      apparent_power_total_net_t0_max_va = greatest(
        schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_va,
        EXCLUDED.apparent_power_total_net_t0_max_va
      ),
      apparent_power_total_net_t0_max_timestamp = case
        when EXCLUDED.apparent_power_total_net_t0_max_va > schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_va then EXCLUDED.apparent_power_total_net_t0_max_timestamp
        else schneider_iem3xxx_aggregates.apparent_power_total_net_t0_max_timestamp
      end,
      apparent_power_total_net_t0_min_va = least(
        schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_va,
        EXCLUDED.apparent_power_total_net_t0_min_va
      ),
      apparent_power_total_net_t0_min_timestamp = case
        when EXCLUDED.apparent_power_total_net_t0_min_va < schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_va then EXCLUDED.apparent_power_total_net_t0_min_timestamp
        else schneider_iem3xxx_aggregates.apparent_power_total_net_t0_min_timestamp
      end,
      current_l1_any_t0_avg_a = (
        schneider_iem3xxx_aggregates.current_l1_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l1_any_t0_avg_a * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      current_l1_any_t0_max_a = greatest(
        schneider_iem3xxx_aggregates.current_l1_any_t0_max_a,
        EXCLUDED.current_l1_any_t0_max_a
      ),
      current_l1_any_t0_max_timestamp = case
        when EXCLUDED.current_l1_any_t0_max_a > schneider_iem3xxx_aggregates.current_l1_any_t0_max_a then EXCLUDED.current_l1_any_t0_max_timestamp
        else schneider_iem3xxx_aggregates.current_l1_any_t0_max_timestamp
      end,
      current_l1_any_t0_min_a = least(
        schneider_iem3xxx_aggregates.current_l1_any_t0_min_a,
        EXCLUDED.current_l1_any_t0_min_a
      ),
      current_l1_any_t0_min_timestamp = case
        when EXCLUDED.current_l1_any_t0_min_a < schneider_iem3xxx_aggregates.current_l1_any_t0_min_a then EXCLUDED.current_l1_any_t0_min_timestamp
        else schneider_iem3xxx_aggregates.current_l1_any_t0_min_timestamp
      end,
      current_l2_any_t0_avg_a = (
        schneider_iem3xxx_aggregates.current_l2_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l2_any_t0_avg_a * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      current_l2_any_t0_max_a = greatest(
        schneider_iem3xxx_aggregates.current_l2_any_t0_max_a,
        EXCLUDED.current_l2_any_t0_max_a
      ),
      current_l2_any_t0_max_timestamp = case
        when EXCLUDED.current_l2_any_t0_max_a > schneider_iem3xxx_aggregates.current_l2_any_t0_max_a then EXCLUDED.current_l2_any_t0_max_timestamp
        else schneider_iem3xxx_aggregates.current_l2_any_t0_max_timestamp
      end,
      current_l2_any_t0_min_a = least(
        schneider_iem3xxx_aggregates.current_l2_any_t0_min_a,
        EXCLUDED.current_l2_any_t0_min_a
      ),
      current_l2_any_t0_min_timestamp = case
        when EXCLUDED.current_l2_any_t0_min_a < schneider_iem3xxx_aggregates.current_l2_any_t0_min_a then EXCLUDED.current_l2_any_t0_min_timestamp
        else schneider_iem3xxx_aggregates.current_l2_any_t0_min_timestamp
      end,
      current_l3_any_t0_avg_a = (
        schneider_iem3xxx_aggregates.current_l3_any_t0_avg_a * schneider_iem3xxx_aggregates.count + EXCLUDED.current_l3_any_t0_avg_a * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      current_l3_any_t0_max_a = greatest(
        schneider_iem3xxx_aggregates.current_l3_any_t0_max_a,
        EXCLUDED.current_l3_any_t0_max_a
      ),
      current_l3_any_t0_max_timestamp = case
        when EXCLUDED.current_l3_any_t0_max_a > schneider_iem3xxx_aggregates.current_l3_any_t0_max_a then EXCLUDED.current_l3_any_t0_max_timestamp
        else schneider_iem3xxx_aggregates.current_l3_any_t0_max_timestamp
      end,
      current_l3_any_t0_min_a = least(
        schneider_iem3xxx_aggregates.current_l3_any_t0_min_a,
        EXCLUDED.current_l3_any_t0_min_a
      ),
      current_l3_any_t0_min_timestamp = case
        when EXCLUDED.current_l3_any_t0_min_a < schneider_iem3xxx_aggregates.current_l3_any_t0_min_a then EXCLUDED.current_l3_any_t0_min_timestamp
        else schneider_iem3xxx_aggregates.current_l3_any_t0_min_timestamp
      end,
      derived_active_power_l1_import_t0_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l1_import_t0_max_wh,
          EXCLUDED.active_energy_l1_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l1_import_t0_min_wh,
          EXCLUDED.active_energy_l1_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_import_t0_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l1_import_t0_max_wh,
          EXCLUDED.active_energy_l1_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l1_import_t0_min_wh,
          EXCLUDED.active_energy_l1_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l1_import_t0_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l1_import_t0_max_wh,
          EXCLUDED.active_energy_l1_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l1_import_t0_min_wh,
          EXCLUDED.active_energy_l1_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_import_t0_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l2_import_t0_max_wh,
          EXCLUDED.active_energy_l2_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l2_import_t0_min_wh,
          EXCLUDED.active_energy_l2_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_import_t0_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l2_import_t0_max_wh,
          EXCLUDED.active_energy_l2_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l2_import_t0_min_wh,
          EXCLUDED.active_energy_l2_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l2_import_t0_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l2_import_t0_max_wh,
          EXCLUDED.active_energy_l2_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l2_import_t0_min_wh,
          EXCLUDED.active_energy_l2_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_import_t0_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l3_import_t0_max_wh,
          EXCLUDED.active_energy_l3_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l3_import_t0_min_wh,
          EXCLUDED.active_energy_l3_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_import_t0_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l3_import_t0_max_wh,
          EXCLUDED.active_energy_l3_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l3_import_t0_min_wh,
          EXCLUDED.active_energy_l3_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_l3_import_t0_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_l3_import_t0_max_wh,
          EXCLUDED.active_energy_l3_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_l3_import_t0_min_wh,
          EXCLUDED.active_energy_l3_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_export_t0_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_export_t0_max_wh,
          EXCLUDED.active_energy_total_export_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_export_t0_min_wh,
          EXCLUDED.active_energy_total_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_export_t0_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_export_t0_max_wh,
          EXCLUDED.active_energy_total_export_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_export_t0_min_wh,
          EXCLUDED.active_energy_total_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_export_t0_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_export_t0_max_wh,
          EXCLUDED.active_energy_total_export_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_export_t0_min_wh,
          EXCLUDED.active_energy_total_export_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t0_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t0_max_wh,
          EXCLUDED.active_energy_total_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t0_min_wh,
          EXCLUDED.active_energy_total_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t0_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t0_max_wh,
          EXCLUDED.active_energy_total_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t0_min_wh,
          EXCLUDED.active_energy_total_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t0_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t0_max_wh,
          EXCLUDED.active_energy_total_import_t0_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t0_min_wh,
          EXCLUDED.active_energy_total_import_t0_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t1_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t1_max_wh,
          EXCLUDED.active_energy_total_import_t1_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t1_min_wh,
          EXCLUDED.active_energy_total_import_t1_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t1_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t1_max_wh,
          EXCLUDED.active_energy_total_import_t1_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t1_min_wh,
          EXCLUDED.active_energy_total_import_t1_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t1_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t1_max_wh,
          EXCLUDED.active_energy_total_import_t1_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t1_min_wh,
          EXCLUDED.active_energy_total_import_t1_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t2_avg_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t2_max_wh,
          EXCLUDED.active_energy_total_import_t2_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t2_min_wh,
          EXCLUDED.active_energy_total_import_t2_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t2_max_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t2_max_wh,
          EXCLUDED.active_energy_total_import_t2_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t2_min_wh,
          EXCLUDED.active_energy_total_import_t2_min_wh
        )
      ) * 4,
      derived_active_power_total_import_t2_min_w = (
        greatest(
          schneider_iem3xxx_aggregates.active_energy_total_import_t2_max_wh,
          EXCLUDED.active_energy_total_import_t2_max_wh
        ) - least(
          schneider_iem3xxx_aggregates.active_energy_total_import_t2_min_wh,
          EXCLUDED.active_energy_total_import_t2_min_wh
        )
      ) * 4,
      derived_reactive_power_total_export_t0_avg_var = (
        greatest(
          schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_max_varh,
          EXCLUDED.reactive_energy_total_export_t0_max_varh
        ) - least(
          schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_min_varh,
          EXCLUDED.reactive_energy_total_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_export_t0_max_var = (
        greatest(
          schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_max_varh,
          EXCLUDED.reactive_energy_total_export_t0_max_varh
        ) - least(
          schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_min_varh,
          EXCLUDED.reactive_energy_total_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_export_t0_min_var = (
        greatest(
          schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_max_varh,
          EXCLUDED.reactive_energy_total_export_t0_max_varh
        ) - least(
          schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_min_varh,
          EXCLUDED.reactive_energy_total_export_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_import_t0_avg_var = (
        greatest(
          schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_max_varh,
          EXCLUDED.reactive_energy_total_import_t0_max_varh
        ) - least(
          schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_min_varh,
          EXCLUDED.reactive_energy_total_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_import_t0_max_var = (
        greatest(
          schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_max_varh,
          EXCLUDED.reactive_energy_total_import_t0_max_varh
        ) - least(
          schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_min_varh,
          EXCLUDED.reactive_energy_total_import_t0_min_varh
        )
      ) * 4,
      derived_reactive_power_total_import_t0_min_var = (
        greatest(
          schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_max_varh,
          EXCLUDED.reactive_energy_total_import_t0_max_varh
        ) - least(
          schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_min_varh,
          EXCLUDED.reactive_energy_total_import_t0_min_varh
        )
      ) * 4,
      reactive_energy_total_export_t0_max_varh = greatest(
        schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_max_varh,
        EXCLUDED.reactive_energy_total_export_t0_max_varh
      ),
      reactive_energy_total_export_t0_min_varh = least(
        schneider_iem3xxx_aggregates.reactive_energy_total_export_t0_min_varh,
        EXCLUDED.reactive_energy_total_export_t0_min_varh
      ),
      reactive_energy_total_import_t0_max_varh = greatest(
        schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_max_varh,
        EXCLUDED.reactive_energy_total_import_t0_max_varh
      ),
      reactive_energy_total_import_t0_min_varh = least(
        schneider_iem3xxx_aggregates.reactive_energy_total_import_t0_min_varh,
        EXCLUDED.reactive_energy_total_import_t0_min_varh
      ),
      reactive_power_total_net_t0_avg_var = (
        schneider_iem3xxx_aggregates.reactive_power_total_net_t0_avg_var * schneider_iem3xxx_aggregates.count + EXCLUDED.reactive_power_total_net_t0_avg_var * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      reactive_power_total_net_t0_max_var = greatest(
        schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_var,
        EXCLUDED.reactive_power_total_net_t0_max_var
      ),
      reactive_power_total_net_t0_max_timestamp = case
        when EXCLUDED.reactive_power_total_net_t0_max_var > schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_var then EXCLUDED.reactive_power_total_net_t0_max_timestamp
        else schneider_iem3xxx_aggregates.reactive_power_total_net_t0_max_timestamp
      end,
      reactive_power_total_net_t0_min_var = least(
        schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_var,
        EXCLUDED.reactive_power_total_net_t0_min_var
      ),
      reactive_power_total_net_t0_min_timestamp = case
        when EXCLUDED.reactive_power_total_net_t0_min_var < schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_var then EXCLUDED.reactive_power_total_net_t0_min_timestamp
        else schneider_iem3xxx_aggregates.reactive_power_total_net_t0_min_timestamp
      end,
      voltage_l1_any_t0_avg_v = (
        schneider_iem3xxx_aggregates.voltage_l1_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l1_any_t0_avg_v * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      voltage_l1_any_t0_max_v = greatest(
        schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_v,
        EXCLUDED.voltage_l1_any_t0_max_v
      ),
      voltage_l1_any_t0_max_timestamp = case
        when EXCLUDED.voltage_l1_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_v then EXCLUDED.voltage_l1_any_t0_max_timestamp
        else schneider_iem3xxx_aggregates.voltage_l1_any_t0_max_timestamp
      end,
      voltage_l1_any_t0_min_v = least(
        schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_v,
        EXCLUDED.voltage_l1_any_t0_min_v
      ),
      voltage_l1_any_t0_min_timestamp = case
        when EXCLUDED.voltage_l1_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_v then EXCLUDED.voltage_l1_any_t0_min_timestamp
        else schneider_iem3xxx_aggregates.voltage_l1_any_t0_min_timestamp
      end,
      voltage_l2_any_t0_avg_v = (
        schneider_iem3xxx_aggregates.voltage_l2_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l2_any_t0_avg_v * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      voltage_l2_any_t0_max_v = greatest(
        schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_v,
        EXCLUDED.voltage_l2_any_t0_max_v
      ),
      voltage_l2_any_t0_max_timestamp = case
        when EXCLUDED.voltage_l2_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_v then EXCLUDED.voltage_l2_any_t0_max_timestamp
        else schneider_iem3xxx_aggregates.voltage_l2_any_t0_max_timestamp
      end,
      voltage_l2_any_t0_min_v = least(
        schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_v,
        EXCLUDED.voltage_l2_any_t0_min_v
      ),
      voltage_l2_any_t0_min_timestamp = case
        when EXCLUDED.voltage_l2_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_v then EXCLUDED.voltage_l2_any_t0_min_timestamp
        else schneider_iem3xxx_aggregates.voltage_l2_any_t0_min_timestamp
      end,
      voltage_l3_any_t0_avg_v = (
        schneider_iem3xxx_aggregates.voltage_l3_any_t0_avg_v * schneider_iem3xxx_aggregates.count + EXCLUDED.voltage_l3_any_t0_avg_v * EXCLUDED.count
      ) / (
        schneider_iem3xxx_aggregates.count + EXCLUDED.count
      ),
      voltage_l3_any_t0_max_v = greatest(
        schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_v,
        EXCLUDED.voltage_l3_any_t0_max_v
      ),
      voltage_l3_any_t0_max_timestamp = case
        when EXCLUDED.voltage_l3_any_t0_max_v > schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_v then EXCLUDED.voltage_l3_any_t0_max_timestamp
        else schneider_iem3xxx_aggregates.voltage_l3_any_t0_max_timestamp
      end,
      voltage_l3_any_t0_min_v = least(
        schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_v,
        EXCLUDED.voltage_l3_any_t0_min_v
      ),
      voltage_l3_any_t0_min_timestamp = case
        when EXCLUDED.voltage_l3_any_t0_min_v < schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_v then EXCLUDED.voltage_l3_any_t0_min_timestamp
        else schneider_iem3xxx_aggregates.voltage_l3_any_t0_min_timestamp
      end
    returning
      *
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
    update schneider_iem3xxx_aggregates
    set
      quarter_hour_count = schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count,
      derived_active_power_l1_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_l1_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l1_import_t0_max_w = greatest(
        delta.derived_active_power_l1_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w
      ),
      derived_active_power_l1_import_t0_max_timestamp = case
        when delta.derived_active_power_l1_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w then delta.derived_active_power_l1_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_timestamp
      end,
      derived_active_power_l1_import_t0_min_w = least(
        delta.derived_active_power_l1_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w
      ),
      derived_active_power_l1_import_t0_min_timestamp = case
        when delta.derived_active_power_l1_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w then delta.derived_active_power_l1_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_timestamp
      end,
      derived_active_power_l2_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_l2_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l2_import_t0_max_w = greatest(
        delta.derived_active_power_l2_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w
      ),
      derived_active_power_l2_import_t0_max_timestamp = case
        when delta.derived_active_power_l2_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w then delta.derived_active_power_l2_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_timestamp
      end,
      derived_active_power_l2_import_t0_min_w = least(
        delta.derived_active_power_l2_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w
      ),
      derived_active_power_l2_import_t0_min_timestamp = case
        when delta.derived_active_power_l2_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w then delta.derived_active_power_l2_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_timestamp
      end,
      derived_active_power_l3_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_l3_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l3_import_t0_max_w = greatest(
        delta.derived_active_power_l3_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w
      ),
      derived_active_power_l3_import_t0_max_timestamp = case
        when delta.derived_active_power_l3_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w then delta.derived_active_power_l3_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_timestamp
      end,
      derived_active_power_l3_import_t0_min_w = least(
        delta.derived_active_power_l3_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w
      ),
      derived_active_power_l3_import_t0_min_timestamp = case
        when delta.derived_active_power_l3_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w then delta.derived_active_power_l3_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_timestamp
      end,
      derived_active_power_total_export_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_export_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_export_t0_max_w = greatest(
        delta.derived_active_power_total_export_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w
      ),
      derived_active_power_total_export_t0_max_timestamp = case
        when delta.derived_active_power_total_export_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w then delta.derived_active_power_total_export_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_timestamp
      end,
      derived_active_power_total_export_t0_min_w = least(
        delta.derived_active_power_total_export_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w
      ),
      derived_active_power_total_export_t0_min_timestamp = case
        when delta.derived_active_power_total_export_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w then delta.derived_active_power_total_export_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_timestamp
      end,
      derived_active_power_total_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t0_max_w = greatest(
        delta.derived_active_power_total_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w
      ),
      derived_active_power_total_import_t0_max_timestamp = case
        when delta.derived_active_power_total_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w then delta.derived_active_power_total_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_timestamp
      end,
      derived_active_power_total_import_t0_min_w = least(
        delta.derived_active_power_total_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w
      ),
      derived_active_power_total_import_t0_min_timestamp = case
        when delta.derived_active_power_total_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w then delta.derived_active_power_total_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_timestamp
      end,
      derived_active_power_total_import_t1_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t1_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t1_max_w = greatest(
        delta.derived_active_power_total_import_t1_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w
      ),
      derived_active_power_total_import_t1_max_timestamp = case
        when delta.derived_active_power_total_import_t1_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w then delta.derived_active_power_total_import_t1_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_timestamp
      end,
      derived_active_power_total_import_t1_min_w = least(
        delta.derived_active_power_total_import_t1_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w
      ),
      derived_active_power_total_import_t1_min_timestamp = case
        when delta.derived_active_power_total_import_t1_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w then delta.derived_active_power_total_import_t1_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_timestamp
      end,
      derived_active_power_total_import_t2_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t2_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t2_max_w = greatest(
        delta.derived_active_power_total_import_t2_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w
      ),
      derived_active_power_total_import_t2_max_timestamp = case
        when delta.derived_active_power_total_import_t2_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w then delta.derived_active_power_total_import_t2_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_timestamp
      end,
      derived_active_power_total_import_t2_min_w = least(
        delta.derived_active_power_total_import_t2_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w
      ),
      derived_active_power_total_import_t2_min_timestamp = case
        when delta.derived_active_power_total_import_t2_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w then delta.derived_active_power_total_import_t2_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_timestamp
      end,
      derived_reactive_power_total_export_t0_avg_var = (
        schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_avg_var * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_reactive_power_total_export_t0_avg_var
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_export_t0_max_var = greatest(
        delta.derived_reactive_power_total_export_t0_max_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var
      ),
      derived_reactive_power_total_export_t0_max_timestamp = case
        when delta.derived_reactive_power_total_export_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var then delta.derived_reactive_power_total_export_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_timestamp
      end,
      derived_reactive_power_total_export_t0_min_var = least(
        delta.derived_reactive_power_total_export_t0_min_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var
      ),
      derived_reactive_power_total_export_t0_min_timestamp = case
        when delta.derived_reactive_power_total_export_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var then delta.derived_reactive_power_total_export_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_timestamp
      end,
      derived_reactive_power_total_import_t0_avg_var = (
        schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_avg_var * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_reactive_power_total_import_t0_avg_var
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_import_t0_max_var = greatest(
        delta.derived_reactive_power_total_import_t0_max_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var
      ),
      derived_reactive_power_total_import_t0_max_timestamp = case
        when delta.derived_reactive_power_total_import_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var then delta.derived_reactive_power_total_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_timestamp
      end,
      derived_reactive_power_total_import_t0_min_var = least(
        delta.derived_reactive_power_total_import_t0_min_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var
      ),
      derived_reactive_power_total_import_t0_min_timestamp = case
        when delta.derived_reactive_power_total_import_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var then delta.derived_reactive_power_total_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_timestamp
      end
    from
      delta
    where
      schneider_iem3xxx_aggregates.timestamp = delta.daily_timestamp
      and schneider_iem3xxx_aggregates.interval = 'day'::interval_entity
      and schneider_iem3xxx_aggregates.meter_id = delta.meter_id
      and schneider_iem3xxx_aggregates.measurement_location_id = delta.measurement_location_id
    returning
      *
  ),
  monthly as (
    update schneider_iem3xxx_aggregates
    set
      quarter_hour_count = schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count,
      derived_active_power_l1_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_l1_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l1_import_t0_max_w = greatest(
        delta.derived_active_power_l1_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w
      ),
      derived_active_power_l1_import_t0_max_timestamp = case
        when delta.derived_active_power_l1_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_w then delta.derived_active_power_l1_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_max_timestamp
      end,
      derived_active_power_l1_import_t0_min_w = least(
        delta.derived_active_power_l1_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w
      ),
      derived_active_power_l1_import_t0_min_timestamp = case
        when delta.derived_active_power_l1_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_w then delta.derived_active_power_l1_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l1_import_t0_min_timestamp
      end,
      derived_active_power_l2_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_l2_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l2_import_t0_max_w = greatest(
        delta.derived_active_power_l2_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w
      ),
      derived_active_power_l2_import_t0_max_timestamp = case
        when delta.derived_active_power_l2_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_w then delta.derived_active_power_l2_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_max_timestamp
      end,
      derived_active_power_l2_import_t0_min_w = least(
        delta.derived_active_power_l2_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w
      ),
      derived_active_power_l2_import_t0_min_timestamp = case
        when delta.derived_active_power_l2_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_w then delta.derived_active_power_l2_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l2_import_t0_min_timestamp
      end,
      derived_active_power_l3_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_l3_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_l3_import_t0_max_w = greatest(
        delta.derived_active_power_l3_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w
      ),
      derived_active_power_l3_import_t0_max_timestamp = case
        when delta.derived_active_power_l3_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_w then delta.derived_active_power_l3_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_max_timestamp
      end,
      derived_active_power_l3_import_t0_min_w = least(
        delta.derived_active_power_l3_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w
      ),
      derived_active_power_l3_import_t0_min_timestamp = case
        when delta.derived_active_power_l3_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_w then delta.derived_active_power_l3_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_l3_import_t0_min_timestamp
      end,
      derived_active_power_total_export_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_export_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_export_t0_max_w = greatest(
        delta.derived_active_power_total_export_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w
      ),
      derived_active_power_total_export_t0_max_timestamp = case
        when delta.derived_active_power_total_export_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_w then delta.derived_active_power_total_export_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_max_timestamp
      end,
      derived_active_power_total_export_t0_min_w = least(
        delta.derived_active_power_total_export_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w
      ),
      derived_active_power_total_export_t0_min_timestamp = case
        when delta.derived_active_power_total_export_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_w then delta.derived_active_power_total_export_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_export_t0_min_timestamp
      end,
      derived_active_power_total_import_t0_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t0_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t0_max_w = greatest(
        delta.derived_active_power_total_import_t0_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w
      ),
      derived_active_power_total_import_t0_max_timestamp = case
        when delta.derived_active_power_total_import_t0_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_w then delta.derived_active_power_total_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_max_timestamp
      end,
      derived_active_power_total_import_t0_min_w = least(
        delta.derived_active_power_total_import_t0_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w
      ),
      derived_active_power_total_import_t0_min_timestamp = case
        when delta.derived_active_power_total_import_t0_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_w then delta.derived_active_power_total_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t0_min_timestamp
      end,
      derived_active_power_total_import_t1_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t1_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t1_max_w = greatest(
        delta.derived_active_power_total_import_t1_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w
      ),
      derived_active_power_total_import_t1_max_timestamp = case
        when delta.derived_active_power_total_import_t1_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_w then delta.derived_active_power_total_import_t1_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_max_timestamp
      end,
      derived_active_power_total_import_t1_min_w = least(
        delta.derived_active_power_total_import_t1_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w
      ),
      derived_active_power_total_import_t1_min_timestamp = case
        when delta.derived_active_power_total_import_t1_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_w then delta.derived_active_power_total_import_t1_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t1_min_timestamp
      end,
      derived_active_power_total_import_t2_avg_w = (
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_avg_w * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_active_power_total_import_t2_avg_w
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_active_power_total_import_t2_max_w = greatest(
        delta.derived_active_power_total_import_t2_max_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w
      ),
      derived_active_power_total_import_t2_max_timestamp = case
        when delta.derived_active_power_total_import_t2_max_w > schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_w then delta.derived_active_power_total_import_t2_max_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_max_timestamp
      end,
      derived_active_power_total_import_t2_min_w = least(
        delta.derived_active_power_total_import_t2_min_w,
        schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w
      ),
      derived_active_power_total_import_t2_min_timestamp = case
        when delta.derived_active_power_total_import_t2_min_w < schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_w then delta.derived_active_power_total_import_t2_min_timestamp
        else schneider_iem3xxx_aggregates.derived_active_power_total_import_t2_min_timestamp
      end,
      derived_reactive_power_total_export_t0_avg_var = (
        schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_avg_var * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_reactive_power_total_export_t0_avg_var
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_export_t0_max_var = greatest(
        delta.derived_reactive_power_total_export_t0_max_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var
      ),
      derived_reactive_power_total_export_t0_max_timestamp = case
        when delta.derived_reactive_power_total_export_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_var then delta.derived_reactive_power_total_export_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_max_timestamp
      end,
      derived_reactive_power_total_export_t0_min_var = least(
        delta.derived_reactive_power_total_export_t0_min_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var
      ),
      derived_reactive_power_total_export_t0_min_timestamp = case
        when delta.derived_reactive_power_total_export_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_var then delta.derived_reactive_power_total_export_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_export_t0_min_timestamp
      end,
      derived_reactive_power_total_import_t0_avg_var = (
        schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_avg_var * schneider_iem3xxx_aggregates.quarter_hour_count + delta.derived_reactive_power_total_import_t0_avg_var
      ) / (
        schneider_iem3xxx_aggregates.quarter_hour_count + delta.new_count
      ),
      derived_reactive_power_total_import_t0_max_var = greatest(
        delta.derived_reactive_power_total_import_t0_max_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var
      ),
      derived_reactive_power_total_import_t0_max_timestamp = case
        when delta.derived_reactive_power_total_import_t0_max_var > schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_var then delta.derived_reactive_power_total_import_t0_max_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_max_timestamp
      end,
      derived_reactive_power_total_import_t0_min_var = least(
        delta.derived_reactive_power_total_import_t0_min_var,
        schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var
      ),
      derived_reactive_power_total_import_t0_min_timestamp = case
        when delta.derived_reactive_power_total_import_t0_min_var < schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_var then delta.derived_reactive_power_total_import_t0_min_timestamp
        else schneider_iem3xxx_aggregates.derived_reactive_power_total_import_t0_min_timestamp
      end
    from
      delta
    where
      schneider_iem3xxx_aggregates.timestamp = delta.monthly_timestamp
      and schneider_iem3xxx_aggregates.interval = 'month'::interval_entity
      and schneider_iem3xxx_aggregates.meter_id = delta.meter_id
      and schneider_iem3xxx_aggregates.measurement_location_id = delta.measurement_location_id
    returning
      *
  )
select
  *
from
  daily
union all
select
  *
from
  monthly;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count
from
  abb_b2x_aggregates;

select
  timestamp,
  interval,
  meter_id,
  measurement_location_id,
  count,
  quarter_hour_count
from
  schneider_iem3xxx_aggregates;

rollback;
