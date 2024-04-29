```mermaid
erDiagram
    Document {
        text Content
        bigint Id PK
        character_varying Type
        bigint Version
    }

    Identifiers {
        character_varying dimension PK
        bigint nextval
    }

    UserByClaimIndex {
        character_varying ClaimType
        character_varying ClaimValue
        bigint DocumentId FK
        integer Id PK
    }

    UserByLoginInfoIndex {
        bigint DocumentId FK
        integer Id PK
        character_varying LoginProvider
        character_varying ProviderKey
    }

    UserByRoleNameIndex {
        integer Count
        integer Id PK
        character_varying RoleName
    }

    UserByRoleNameIndex_Document {
        bigint DocumentId FK
        bigint UserByRoleNameIndexId FK
    }

    UserIndex {
        integer AccessFailedCount
        bigint DocumentId FK
        integer Id PK
        boolean IsEnabled
        boolean IsLockoutEnabled
        timestamp_without_time_zone LockoutEndUtc
        character_varying NormalizedEmail
        character_varying NormalizedUserName
        character_varying UserId
    }

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
        text al_blue_low_network_user_catalogue_id
        text al_created_by_id
        timestamp_with_time_zone al_created_on
        text al_deleted_by_id
        timestamp_with_time_zone al_deleted_on
        boolean al_is_deleted
        text al_last_updated_by_id
        timestamp_with_time_zone al_last_updated_on
        text al_lp_address
        text al_lp_city
        text al_lp_email
        text al_lp_name
        text al_lp_phone_number
        text al_lp_postal_code
        text al_lp_social_security_number
        text al_red_low_network_user_catalogue_id
        text al_regulatory_catalogue_id
        text al_white_low_network_user_catalogue_id
        text al_white_medium_network_user_catalogue_id
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
        text legal_person_address
        text legal_person_city
        text legal_person_email
        text legal_person_name
        text legal_person_phone_number
        text legal_person_postal_code
        text legal_person_social_security_number
        bigint red_low_catalogue_id FK
        bigint regulatory_catalogue_id FK
        text title
        bigint white_low_catalogue_id FK
        bigint white_medium_catalogue_id FK
    }

    measurement_locations {
        bigint _network_user_catalogue_id FK
        bigint catalogue_id
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
        real am_connection_power__w
        text am_created_by_id
        timestamp_with_time_zone am_created_on
        text am_deleted_by_id
        timestamp_with_time_zone am_deleted_on
        boolean am_is_deleted
        text am_last_updated_by_id
        timestamp_with_time_zone am_last_updated_on
        text am_messenger_id
        ARRAY am_phases
        text anuml_created_by_id
        timestamp_with_time_zone anuml_created_on
        text anuml_deleted_by_id
        timestamp_with_time_zone anuml_deleted_on
        boolean anuml_is_deleted
        text anuml_last_updated_by_id
        timestamp_with_time_zone anuml_last_updated_on
        text anuml_meter_id
        numeric asrc_active_energy_total_import_t1_price__eur
        numeric asrc_active_energy_total_import_t2_price__eur
        numeric asrc_business_usage_fee_price__eur
        text asrc_created_by_id
        timestamp_with_time_zone asrc_created_on
        text asrc_deleted_by_id
        timestamp_with_time_zone asrc_deleted_on
        boolean asrc_is_deleted
        text asrc_last_updated_by_id
        timestamp_with_time_zone asrc_last_updated_on
        numeric asrc_renewable_energy_fee_price__eur
        numeric asrc_tax_rate__percent
        numeric aunuc_active_energy_total_import_t0_price__eur
        numeric aunuc_active_energy_total_import_t1_price__eur
        numeric aunuc_active_energy_total_import_t2_price__eur
        numeric aunuc_active_power_total_import_t1_price__eur
        text aunuc_created_by_id
        timestamp_with_time_zone aunuc_created_on
        text aunuc_deleted_by_id
        timestamp_with_time_zone aunuc_deleted_on
        boolean aunuc_is_deleted
        text aunuc_last_updated_by_id
        timestamp_with_time_zone aunuc_last_updated_on
        numeric aunuc_meter_fee_price__eur
        numeric aunuc_reactive_energy_total_ramped_t0_price__eur
        timestamp_with_time_zone from_date
        bigint id PK
        text issued_by_id FK
        timestamp_with_time_zone issued_on
        character_varying kind
        text meter_id FK
        bigint network_user_invoice_id FK
        bigint network_user_measurement_location_id FK
        numeric supply_active_energy_total_import_t1_amount_wh
        numeric supply_active_energy_total_import_t1_max_wh
        numeric supply_active_energy_total_import_t1_min_wh
        numeric supply_active_energy_total_import_t1_price_eur
        numeric supply_active_energy_total_import_t1_total_eur
        numeric supply_active_energy_total_import_t2_amount_wh
        numeric supply_active_energy_total_import_t2_max_wh
        numeric supply_active_energy_total_import_t2_min_wh
        numeric supply_active_energy_total_import_t2_price_eur
        numeric supply_active_energy_total_import_t2_total_eur
        numeric supply_business_usage_fee_active_energy_total_import_t0_amount_
        numeric supply_business_usage_fee_active_energy_total_import_t0_max_wh
        numeric supply_business_usage_fee_active_energy_total_import_t0_min_wh
        numeric supply_business_usage_fee_active_energy_total_import_t0_price_e
        numeric supply_business_usage_fee_active_energy_total_import_t0_total_e
        numeric supply_fee_total_eur
        bigint supply_regulatory_catalogue_id FK
        numeric supply_renewable_energy_fee_active_energy_total_import_t0_amoun
        numeric supply_renewable_energy_fee_active_energy_total_import_t0_max_w
        numeric supply_renewable_energy_fee_active_energy_total_import_t0_min_w
        numeric supply_renewable_energy_fee_active_energy_total_import_t0_price
        numeric supply_renewable_energy_fee_active_energy_total_import_t0_total
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric usage_active_energy_total_import_t0_amount_wh
        numeric usage_active_energy_total_import_t0_max_wh
        numeric usage_active_energy_total_import_t0_min_wh
        numeric usage_active_energy_total_import_t0_price_eur
        numeric usage_active_energy_total_import_t0_total_eur
        numeric usage_active_energy_total_import_t1_amount_wh
        numeric usage_active_energy_total_import_t1_max_wh
        numeric usage_active_energy_total_import_t1_min_wh
        numeric usage_active_energy_total_import_t1_price_eur
        numeric usage_active_energy_total_import_t1_total_eur
        numeric usage_active_energy_total_import_t2_amount_wh
        numeric usage_active_energy_total_import_t2_max_wh
        numeric usage_active_energy_total_import_t2_min_wh
        numeric usage_active_energy_total_import_t2_price_eur
        numeric usage_active_energy_total_import_t2_total_eur
        numeric usage_active_power_total_import_t1_amount_w
        numeric usage_active_power_total_import_t1_peak_w
        numeric usage_active_power_total_import_t1_price_eur
        numeric usage_active_power_total_import_t1_total_eur
        numeric usage_fee_total_eur
        numeric usage_meter_fee_amount
        numeric usage_meter_fee_price_eur
        numeric usage_meter_total_eur
        bigint usage_network_user_catalogue_id FK
        numeric usage_reactive_energy_total_ramped_t0_export_amount_varh
        numeric usage_reactive_energy_total_ramped_t0_export_max_varh
        numeric usage_reactive_energy_total_ramped_t0_export_min_varh
        numeric usage_reactive_energy_total_ramped_t0_import_amount_varh
        numeric usage_reactive_energy_total_ramped_t0_import_max_varh
        numeric usage_reactive_energy_total_ramped_t0_import_min_varh
        numeric usage_reactive_energy_total_ramped_t0_ramped_amount_varh
        numeric usage_reactive_energy_total_ramped_t0_ramped_price_eur
        numeric usage_reactive_energy_total_ramped_t0_ramped_total_eur
    }

    network_user_catalogues {
        numeric active_energy_total_import_t0_price_eur
        numeric active_energy_total_import_t1_price_eur
        numeric active_energy_total_import_t2_price_eur
        numeric active_power_total_import_t1_price_eur
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
        text title
    }

    network_user_entity_representative_entity {
        bigint network_users_id PK
        text representatives_string_id PK
    }

    network_user_invoices {
        text al_blue_low_network_user_catalogue_id
        text al_created_by_id
        timestamp_with_time_zone al_created_on
        text al_deleted_by_id
        timestamp_with_time_zone al_deleted_on
        boolean al_is_deleted
        text al_last_updated_by_id
        timestamp_with_time_zone al_last_updated_on
        text al_lp_address
        text al_lp_city
        text al_lp_email
        text al_lp_name
        text al_lp_phone_number
        text al_lp_postal_code
        text al_lp_social_security_number
        text al_red_low_network_user_catalogue_id
        text al_regulatory_catalogue_id
        text al_white_low_network_user_catalogue_id
        text al_white_medium_network_user_catalogue_id
        text anu_created_by_id
        timestamp_with_time_zone anu_created_on
        text anu_deleted_by_id
        timestamp_with_time_zone anu_deleted_on
        boolean anu_is_deleted
        text anu_last_updated_by_id
        timestamp_with_time_zone anu_last_updated_on
        text anu_lp_address
        text anu_lp_city
        text anu_lp_email
        text anu_lp_name
        text anu_lp_phone_number
        text anu_lp_postal_code
        text anu_lp_social_security_number
        numeric arc_active_energy_total_import_t1_price__eur
        numeric arc_active_energy_total_import_t2_price__eur
        numeric arc_business_usage_fee_price__eur
        text arc_created_by_id
        timestamp_with_time_zone arc_created_on
        text arc_deleted_by_id
        timestamp_with_time_zone arc_deleted_on
        boolean arc_is_deleted
        text arc_last_updated_by_id
        timestamp_with_time_zone arc_last_updated_on
        numeric arc_renewable_energy_fee_price__eur
        numeric arc_tax_rate__percent
        timestamp_with_time_zone from_date
        bigint id PK
        text issued_by_id FK
        timestamp_with_time_zone issued_on
        bigint network_user_id FK
        numeric supply_active_energy_total_import_t1fee_eur
        numeric supply_active_energy_total_import_t2fee_eur
        numeric supply_business_usage_fee_eur
        numeric supply_fee_total_eur
        numeric supply_renewable_energy_fee_eur
        numeric tax_eur
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric total_with_tax_eur
        numeric usage_active_energy_total_import_t0fee_eur
        numeric usage_active_energy_total_import_t1fee_eur
        numeric usage_active_energy_total_import_t2fee_eur
        numeric usage_active_power_total_import_t1peak_fee_eur
        numeric usage_fee_total_eur
        numeric usage_meter_fee_eur
        numeric usage_reactive_energy_total_ramped_t0fee_eur
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
        text legal_person_address
        text legal_person_city
        text legal_person_email
        text legal_person_name
        text legal_person_phone_number
        text legal_person_postal_code
        text legal_person_social_security_number
        bigint location_id FK
        text title
    }

    regulatory_catalogues {
        numeric active_energy_total_import_t1_price_eur
        numeric active_energy_total_import_t2_price_eur
        numeric business_usage_fee_price_eur
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        bigint id PK
        boolean is_deleted
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        numeric renewable_energy_fee_price_eur
        numeric tax_rate_percent
        text title
    }

    representatives {
        text created_by_id FK
        timestamp_with_time_zone created_on
        text deleted_by_id FK
        timestamp_with_time_zone deleted_on
        text id PK
        boolean is_deleted
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        text physical_person_email
        text physical_person_name
        text physical_person_phone_number
        integer role
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

    UserByClaimIndex }o--|| Document : "DocumentId"
    UserByLoginInfoIndex }o--|| Document : "DocumentId"
    UserByRoleNameIndex_Document }o--|| Document : "DocumentId"
    UserIndex }o--|| Document : "DocumentId"
    UserByRoleNameIndex_Document }o--|| UserByRoleNameIndex : "UserByRoleNameIndexId"
    abb_b2x_aggregates }o--|| meters : "meter_id"
    abb_b2x_measurements }o--|| meters : "meter_id"
    events }o--|| messengers : "messenger_id"
    events }o--|| representatives : "representative_id"
    location_entity_representative_entity }o--|| locations : "locations_id"
    location_entity_representative_entity }o--|| representatives : "representatives_string_id"
    location_invoices }o--|| locations : "location_id"
    location_invoices }o--|| representatives : "issued_by_id"
    locations }o--|| network_user_catalogues : "blue_low_catalogue_id"
    locations }o--|| network_user_catalogues : "red_low_catalogue_id"
    locations }o--|| network_user_catalogues : "white_low_catalogue_id"
    locations }o--|| network_user_catalogues : "white_medium_catalogue_id"
    locations }o--|| regulatory_catalogues : "regulatory_catalogue_id"
    locations }o--|| representatives : "created_by_id"
    locations }o--|| representatives : "deleted_by_id"
    locations }o--|| representatives : "last_updated_by_id"
    measurement_locations }o--|| locations : "location_id"
    messengers }o--|| locations : "location_id"
    network_users }o--|| locations : "location_id"
    measurement_locations }o--|| meters : "meter_id"
    measurement_locations }o--|| network_user_catalogues : "_network_user_catalogue_id"
    measurement_locations }o--|| network_users : "network_user_id"
    measurement_locations }o--|| representatives : "created_by_id"
    measurement_locations }o--|| representatives : "deleted_by_id"
    measurement_locations }o--|| representatives : "last_updated_by_id"
    network_user_calculations }o--|| measurement_locations : "network_user_measurement_location_id"
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
    network_user_calculations }o--|| network_user_catalogues : "usage_network_user_catalogue_id"
    network_user_calculations }o--|| network_user_invoices : "network_user_invoice_id"
    network_user_calculations }o--|| regulatory_catalogues : "supply_regulatory_catalogue_id"
    network_user_calculations }o--|| representatives : "issued_by_id"
    network_user_catalogues }o--|| representatives : "created_by_id"
    network_user_catalogues }o--|| representatives : "deleted_by_id"
    network_user_catalogues }o--|| representatives : "last_updated_by_id"
    network_user_entity_representative_entity }o--|| network_users : "network_users_id"
    network_user_entity_representative_entity }o--|| representatives : "representatives_string_id"
    network_user_invoices }o--|| network_users : "network_user_id"
    network_user_invoices }o--|| representatives : "issued_by_id"
    network_users }o--|| representatives : "created_by_id"
    network_users }o--|| representatives : "deleted_by_id"
    network_users }o--|| representatives : "last_updated_by_id"
    regulatory_catalogues }o--|| representatives : "created_by_id"
    regulatory_catalogues }o--|| representatives : "deleted_by_id"
    regulatory_catalogues }o--|| representatives : "last_updated_by_id"
    representatives }o--|| representatives : "created_by_id"
    representatives }o--|| representatives : "deleted_by_id"
    representatives }o--|| representatives : "last_updated_by_id"
```
