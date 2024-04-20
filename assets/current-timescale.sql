SELECT create_hypertable('abb_b2x_measurements', 'timestamp');
SELECT add_dimension('abb_b2x_measurements', 'meter_id', number_partitions => 2);

SELECT create_hypertable('abb_b2x_aggregates', 'timestamp');
SELECT add_dimension('abb_b2x_aggregates', 'meter_id', number_partitions => 2);

SELECT create_hypertable('schneider_iem3xxx_measurements', 'timestamp');
SELECT add_dimension('schneider_iem3xxx_measurements', 'meter_id', number_partitions => 2);

SELECT create_hypertable('schneider_iem3xxx_aggregates', 'timestamp');
SELECT add_dimension('schneider_iem3xxx_aggregates', 'meter_id', number_partitions => 2);
