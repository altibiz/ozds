```mermaid
erDiagram
    __EFMigrationsHistory {
        character_varying migration_id PK
        character_varying product_version
    }

    abb_b2x_aggregates {
        real active_energy_total_export_t0_max_wh
        real active_energy_total_export_t0_min_wh
        real active_energy_total_import_t0_max_wh
        real active_energy_total_import_t0_min_wh
        real active_energy_total_import_t1_max_wh
        real active_energy_total_import_t1_min_wh
        real active_energy_total_import_t2_max_wh
        real active_energy_total_import_t2_min_wh
        real active_power_l1_net_t0_avg_w
        real active_power_l2_net_t0_avg_w
        real active_power_l3_net_t0_avg_w
        bigint count
        real current_l1_any_t0_avg_a
        real current_l2_any_t0_avg_a
        real current_l3_any_t0_avg_a
        integer interval PK
        text meter_id PK
        real reactive_energy_total_export_t0_max_varh
        real reactive_energy_total_export_t0_min_varh
        real reactive_energy_total_import_t0_max_varh
        real reactive_energy_total_import_t0_min_varh
        real reactive_power_l1_net_t0_avg_var
        real reactive_power_l2_net_t0_avg_var
        real reactive_power_l3_net_t0_avg_var
        timestamp_with_time_zone timestamp PK
        real voltage_l1_any_t0_avg_v
        real voltage_l2_any_t0_avg_v
        real voltage_l3_any_t0_avg_v
    }

    abb_b2x_measurements {
        real active_energy_l1_export_t0_wh
        real active_energy_l1_import_t0_wh
        real active_energy_l2_export_t0_wh
        real active_energy_l2_import_t0_wh
        real active_energy_l3_export_t0_wh
        real active_energy_l3_import_t0_wh
        real active_energy_total_export_t0_wh
        real active_energy_total_import_t0_wh
        real active_energy_total_import_t1_wh
        real active_energy_total_import_t2_wh
        real active_power_l1_any_t0_w
        real active_power_l2_any_t0_w
        real active_power_l3_any_t0_w
        real current_l1_any_t0_a
        real current_l2_any_t0_a
        real current_l3_any_t0_a
        text meter_id PK
        real reactive_energy_l1_export_t0_varh
        real reactive_energy_l1_import_t0_varh
        real reactive_energy_l2_export_t0_varh
        real reactive_energy_l2_import_t0_varh
        real reactive_energy_l3_export_t0_varh
        real reactive_energy_l3_import_t0_varh
        real reactive_energy_total_export_t0_varh
        real reactive_energy_total_import_t0_varh
        real reactive_power_l1_any_t0_var
        real reactive_power_l2_any_t0_var
        real reactive_power_l3_any_t0_var
        timestamp_with_time_zone timestamp PK
        real voltage_l1_any_t0_v
        real voltage_l2_any_t0_v
        real voltage_l3_any_t0_v
    }

    catalogues {
        numeric active_energy_total_import_t0_price_eur
        numeric active_energy_total_import_t1_price_eur
        numeric active_energy_total_import_t2_price_eur
        numeric active_power_total_import_t1_price_eur
        numeric business_usage_fee_price_eur
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        bigint id PK
        boolean is_deleted
        character_varying kind
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        numeric meter_fee_price_eur
        numeric reactive_energy_total_ramped_t0_price_eur
        numeric renewable_energy_fee_price_eur
        numeric tax_rate_percent
        text title
    }

    events {
        integer audit
        text auditable_entity_id
        text auditable_entity_table
        text auditable_entity_type
        text description
        bigint id PK
        character_varying kind
        integer level
        text messenger_id FK
        text representative_id FK
        timestamp_with_time_zone timestamp
        text title
    }

    location_entity_representative_entity {
        bigint locations_id PK
        text representatives_string_id PK
    }

    location_invoices {
        text archived_location_blue_low_catalogue_id
        text archived_location_created_by_id
        timestamp_with_time_zone archived_location_created_on
        text archived_location_deleted_by_id
        timestamp_with_time_zone archived_location_deleted_on
        text archived_location_id
        boolean archived_location_is_deleted
        text archived_location_last_updated_by_id
        timestamp_with_time_zone archived_location_last_updated_on
        text archived_location_red_low_catalogue_id
        text archived_location_regulatory_catalogue_id
        text archived_location_title
        text archived_location_white_low_catalogue_id
        text archived_location_white_medium_catalogue_id
        timestamp_with_time_zone from_date
        bigint id PK
        text issued_by_id FK
        timestamp_with_time_zone issued_on
        bigint location_id FK
        numeric tax_eur
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric total_with_tax_eur
    }

    locations {
        bigint blue_low_catalogue_id FK
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        bigint id PK
        boolean is_deleted
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        bigint red_low_catalogue_id FK
        bigint regulatory_catalogue_id FK
        text title
        bigint white_low_catalogue_id FK
        bigint white_medium_catalogue_id FK
    }

    measurement_locations {
        bigint catalogue_id FK
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        bigint id PK
        boolean is_deleted
        character_varying kind
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        bigint location_id FK
        text meter_id FK
        bigint network_user_id FK
        text title
    }

    measurement_validators {
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        bigint id PK
        boolean is_deleted
        character_varying kind
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        real max_active_power_w
        real max_apparent_power_va
        real max_current_a
        real max_reactive_power_var
        real max_voltage_v
        real min_active_power_w
        real min_apparent_power_va
        real min_current_a
        real min_reactive_power_var
        real min_voltage_v
        text title
    }

    messengers {
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        text id PK
        boolean is_deleted
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        bigint location_id FK
        text title
    }

    meters {
        real connection_power_w
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        text id PK
        boolean is_deleted
        character_varying kind
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        bigint measurement_validator_id FK
        text messenger_id FK
        ARRAY phases
        text title
    }

    network_user_calculations {
        numeric active_energy_total_import_t1_amount_wh
        numeric active_energy_total_import_t1_max_wh
        numeric active_energy_total_import_t1_min_wh
        numeric active_energy_total_import_t1_price_eur
        numeric active_energy_total_import_t1_total_eur
        numeric active_energy_total_import_t2_amount_wh
        numeric active_energy_total_import_t2_max_wh
        numeric active_energy_total_import_t2_min_wh
        numeric active_energy_total_import_t2_price_eur
        numeric active_energy_total_import_t2_total_eur
        numeric active_power_total_import_t1_amount_w
        numeric active_power_total_import_t1_peak_w
        numeric active_power_total_import_t1_price_eur
        numeric active_power_total_import_t1_total_eur
        text archived_catalogue_created_by_id
        timestamp_with_time_zone archived_catalogue_created_on
        text archived_catalogue_deleted_by_id
        timestamp_with_time_zone archived_catalogue_deleted_on
        text archived_catalogue_id
        boolean archived_catalogue_is_deleted
        text archived_catalogue_last_updated_by_id
        timestamp_with_time_zone archived_catalogue_last_updated_on
        text archived_catalogue_title
        text archived_measurement_location_catalogue_id
        text archived_measurement_location_created_by_id
        timestamp_with_time_zone archived_measurement_location_created_on
        text archived_measurement_location_deleted_by_id
        timestamp_with_time_zone archived_measurement_location_deleted_on
        text archived_measurement_location_id
        boolean archived_measurement_location_is_deleted
        text archived_measurement_location_last_updated_by_id
        timestamp_with_time_zone archived_measurement_location_last_updated_on
        text archived_measurement_location_meter_id
        text archived_measurement_location_network_user_id
        text archived_measurement_location_title
        real archived_meter_connection_power_w
        text archived_meter_created_by_id
        timestamp_with_time_zone archived_meter_created_on
        text archived_meter_deleted_by_id
        timestamp_with_time_zone archived_meter_deleted_on
        text archived_meter_id
        boolean archived_meter_is_deleted
        text archived_meter_last_updated_by_id
        timestamp_with_time_zone archived_meter_last_updated_on
        text archived_meter_messenger_id
        ARRAY archived_meter_phases
        text archived_meter_title
        bigint catalogue_id FK
        timestamp_with_time_zone from_date
        bigint id PK
        text issued_by_id FK
        timestamp_with_time_zone issued_on
        character_varying kind
        bigint location_invoice_entity_id FK
        numeric max_active_power_total_import_t1_amount_w
        numeric max_active_power_total_import_t1_peak_w
        numeric max_active_power_total_import_t1_price_eur
        numeric max_active_power_total_import_t1_total_eur
        bigint measurement_location_id FK
        numeric meter_fee_price_eur
        text meter_id FK
        bigint network_user_id FK
        bigint network_user_invoice_id
        numeric reactive_energy_total_export_t0_amount_varh
        numeric reactive_energy_total_export_t0_max_varh
        numeric reactive_energy_total_export_t0_min_varh
        numeric reactive_energy_total_import_t0_amount_varh
        numeric reactive_energy_total_import_t0_max_varh
        numeric reactive_energy_total_import_t0_min_varh
        numeric reactive_energy_total_ramped_t0_price_eur
        numeric reactive_energy_total_ramped_t0_total_eur
        numeric reactive_energy_total_ramped_t0amount_va_rh
        numeric red_low_network_user_calculation_entity_reactive_energy_total_r
        bigint regulatory_catalogue_entity_id FK
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric white_low_network_user_calculation_entity_reactive_energy_total
        numeric white_medium_network_user_calculation_entity_reactive_energy_to
    }

    network_user_entity_representative_entity {
        bigint network_users_id PK
        text representatives_string_id PK
    }

    network_user_invoices {
        text archived_location_blue_low_catalogue_id
        text archived_location_created_by_id
        timestamp_with_time_zone archived_location_created_on
        text archived_location_deleted_by_id
        timestamp_with_time_zone archived_location_deleted_on
        text archived_location_id
        boolean archived_location_is_deleted
        text archived_location_last_updated_by_id
        timestamp_with_time_zone archived_location_last_updated_on
        text archived_location_red_low_catalogue_id
        text archived_location_regulatory_catalogue_id
        text archived_location_title
        text archived_location_white_low_catalogue_id
        text archived_location_white_medium_catalogue_id
        text archived_network_user_created_by_id
        timestamp_with_time_zone archived_network_user_created_on
        text archived_network_user_deleted_by_id
        timestamp_with_time_zone archived_network_user_deleted_on
        text archived_network_user_id
        boolean archived_network_user_is_deleted
        text archived_network_user_last_updated_by_id
        timestamp_with_time_zone archived_network_user_last_updated_on
        text archived_network_user_location_id
        text archived_network_user_title
        timestamp_with_time_zone from_date
        bigint id PK
        text issued_by_id FK
        timestamp_with_time_zone issued_on
        bigint network_user_id FK
        numeric tax_eur
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric total_with_tax_eur
    }

    network_users {
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        bigint id PK
        boolean is_deleted
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        bigint location_id FK
        text title
    }

    representatives {
        text address
        text city
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        text email
        text id PK
        boolean is_deleted
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        text name
        text phone_number
        text postal_code
        integer role
        text social_security_number
        text title
    }

    schneider_iem3xxx_aggregates {
        real active_energy_total_export_t0_max_wh
        real active_energy_total_export_t0_min_wh
        real active_energy_total_import_t0_max_wh
        real active_energy_total_import_t0_min_wh
        real active_energy_total_import_t1_max_wh
        real active_energy_total_import_t1_min_wh
        real active_energy_total_import_t2_max_wh
        real active_energy_total_import_t2_min_wh
        real active_power_l1_net_t0_avg_w
        real active_power_l2_net_t0_avg_w
        real active_power_l3_net_t0_avg_w
        real apparent_power_total_net_t0_avg_va
        bigint count
        real current_l1_any_t0_avg_a
        real current_l2_any_t0_avg_a
        real current_l3_any_t0_avg_a
        integer interval PK
        text meter_id PK
        real reactive_energy_total_export_t0_max_varh
        real reactive_energy_total_export_t0_min_varh
        real reactive_energy_total_import_t0_max_varh
        real reactive_energy_total_import_t0_min_varh
        real reactive_power_total_net_t0_avg_var
        timestamp_with_time_zone timestamp PK
        real voltage_l1_any_t0_avg_v
        real voltage_l2_any_t0_avg_v
        real voltage_l3_any_t0_avg_v
    }

    schneider_iem3xxx_measurements {
        real active_energy_export_total_t0_wh
        real active_energy_import_l1_t0_wh
        real active_energy_import_l2_t0_wh
        real active_energy_import_l3_t0_wh
        real active_energy_import_total_t0_wh
        real active_energy_import_total_t1_wh
        real active_energy_import_total_t2_wh
        real active_power_l1_net_t0_w
        real active_power_l2_net_t0_w
        real active_power_l3_net_t0_w
        real apparent_power_total_net_t0_va
        real current_l1_any_t0_a
        real current_l2_any_t0_a
        real current_l3_any_t0_a
        text meter_id PK
        real reactive_energy_export_total_t0_varh
        real reactive_energy_import_total_t0_varh
        real reactive_power_total_net_t0_var
        timestamp_with_time_zone timestamp PK
        real voltage_l1_any_t0_v
        real voltage_l2_any_t0_v
        real voltage_l3_any_t0_v
    }

    abb_b2x_aggregates }o--|| meters : "meter_id"
    abb_b2x_measurements }o--|| meters : "meter_id"
    catalogues }o--|| representatives : "created_by_id"
    catalogues }o--|| representatives : "deleted_by_id"
    catalogues }o--|| representatives : "last_updated_by_id"
    locations }o--|| catalogues : "blue_low_catalogue_id"
    locations }o--|| catalogues : "red_low_catalogue_id"
    locations }o--|| catalogues : "regulatory_catalogue_id"
    locations }o--|| catalogues : "white_low_catalogue_id"
    locations }o--|| catalogues : "white_medium_catalogue_id"
    measurement_locations }o--|| catalogues : "catalogue_id"
    network_user_calculations }o--|| catalogues : "catalogue_id"
    network_user_calculations }o--|| catalogues : "regulatory_catalogue_entity_id"
    events }o--|| messengers : "messenger_id"
    events }o--|| representatives : "representative_id"
    location_entity_representative_entity }o--|| locations : "locations_id"
    location_entity_representative_entity }o--|| representatives : "representatives_string_id"
    location_invoices }o--|| locations : "location_id"
    location_invoices }o--|| representatives : "issued_by_id"
    network_user_calculations }o--|| location_invoices : "location_invoice_entity_id"
    locations }o--|| representatives : "created_by_id"
    locations }o--|| representatives : "deleted_by_id"
    locations }o--|| representatives : "last_updated_by_id"
    measurement_locations }o--|| locations : "location_id"
    messengers }o--|| locations : "location_id"
    network_users }o--|| locations : "location_id"
    measurement_locations }o--|| meters : "meter_id"
    measurement_locations }o--|| network_users : "network_user_id"
    measurement_locations }o--|| representatives : "created_by_id"
    measurement_locations }o--|| representatives : "deleted_by_id"
    measurement_locations }o--|| representatives : "last_updated_by_id"
    network_user_calculations }o--|| measurement_locations : "measurement_location_id"
    measurement_validators }o--|| representatives : "created_by_id"
    measurement_validators }o--|| representatives : "deleted_by_id"
    measurement_validators }o--|| representatives : "last_updated_by_id"
    meters }o--|| measurement_validators : "measurement_validator_id"
    messengers }o--|| representatives : "created_by_id"
    messengers }o--|| representatives : "deleted_by_id"
    messengers }o--|| representatives : "last_updated_by_id"
    meters }o--|| messengers : "messenger_id"
    meters }o--|| representatives : "created_by_id"
    meters }o--|| representatives : "deleted_by_id"
    meters }o--|| representatives : "last_updated_by_id"
    network_user_calculations }o--|| meters : "meter_id"
    schneider_iem3xxx_aggregates }o--|| meters : "meter_id"
    schneider_iem3xxx_measurements }o--|| meters : "meter_id"
    network_user_calculations }o--|| network_user_invoices : "network_user_id"
    network_user_calculations }o--|| representatives : "issued_by_id"
    network_user_entity_representative_entity }o--|| network_users : "network_users_id"
    network_user_entity_representative_entity }o--|| representatives : "representatives_string_id"
    network_user_invoices }o--|| network_users : "network_user_id"
    network_user_invoices }o--|| representatives : "issued_by_id"
    network_users }o--|| representatives : "created_by_id"
    network_users }o--|| representatives : "deleted_by_id"
    network_users }o--|| representatives : "last_updated_by_id"
    representatives }o--|| representatives : "created_by_id"
    representatives }o--|| representatives : "deleted_by_id"
    representatives }o--|| representatives : "last_updated_by_id"
```