# Database schema

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

    __OzdsDataDbContext {
        character_varying migration_id PK
        character_varying product_version
    }

    __OzdsJobsDbContext {
        character_varying MigrationId PK
        character_varying ProductVersion
    }

    __OzdsMessagingDbContext {
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
        interval_entity interval PK
        text meter_id PK,FK
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
        real active_power_l1_net_t0_w
        real active_power_l2_net_t0_w
        real active_power_l3_net_t0_w
        real current_l1_any_t0_a
        real current_l2_any_t0_a
        real current_l3_any_t0_a
        text meter_id PK,FK
        real reactive_energy_l1_export_t0_varh
        real reactive_energy_l1_import_t0_varh
        real reactive_energy_l2_export_t0_varh
        real reactive_energy_l2_import_t0_varh
        real reactive_energy_l3_export_t0_varh
        real reactive_energy_l3_import_t0_varh
        real reactive_energy_total_export_t0_varh
        real reactive_energy_total_import_t0_varh
        real reactive_power_l1_net_t0_var
        real reactive_power_l2_net_t0_var
        real reactive_power_l3_net_t0_var
        timestamp_with_time_zone timestamp PK
        real voltage_l1_any_t0_v
        real voltage_l2_any_t0_v
        real voltage_l3_any_t0_v
    }

    events {
        audit_entity audit
        text auditable_entity_id
        text auditable_entity_table
        text auditable_entity_type
        ARRAY categories
        jsonb content
        bigint id PK
        character_varying kind
        level_entity level
        text messenger_id FK
        text representative_id FK
        timestamp_with_time_zone timestamp
        text title
    }

    inbox_state {
        timestamp_with_time_zone consumed
        uuid consumer_id UK
        timestamp_with_time_zone delivered
        timestamp_with_time_zone expiration_time
        bigint id PK
        bigint last_sequence_number
        uuid lock_id
        uuid message_id UK
        integer receive_count
        timestamp_with_time_zone received
        bytea row_version
    }

    location_invoices {
        text al_alti_biz_sub_project_code
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
        numeric tax_rate_percent
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric total_with_tax_eur
    }

    location_representatives {
        bigint location_id PK,FK
        text representative_id PK,FK
    }

    locations {
        text alti_biz_sub_project_code
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
        bigint network_user_catalogue_id FK
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
        character_varying kind
        text last_updated_by_id FK
        timestamp_with_time_zone last_updated_on
        bigint location_id FK
        duration_entity max_inactivity_period_duration
        bigint max_inactivity_period_multiplier
        duration_entity push_delay_period_duration
        bigint push_delay_period_multiplier
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
        numeric jen_active_import_amount_kwh
        numeric jen_active_import_max_kwh
        numeric jen_active_import_min_kwh
        numeric jen_ramped_amount_kvarh
        numeric jen_ramped_price_eur
        numeric jen_ramped_total_eur
        numeric jen_reactive_export_amount_kvarh
        numeric jen_reactive_export_max_kvarh
        numeric jen_reactive_export_min_kvarh
        numeric jen_reactive_import_amount_kvarh
        numeric jen_reactive_import_max_kvarh
        numeric jen_reactive_import_min_kvarh
        character_varying kind
        text meter_id FK
        numeric mjt_amount_kwh
        numeric mjt_max_kwh
        numeric mjt_min_kwh
        numeric mjt_price_eur
        numeric mjt_total_eur
        numeric mnt_amount_kwh
        numeric mnt_max_kwh
        numeric mnt_min_kwh
        numeric mnt_price_eur
        numeric mnt_total_eur
        numeric mvt_amount_kwh
        numeric mvt_max_kwh
        numeric mvt_min_kwh
        numeric mvt_price_eur
        numeric mvt_total_eur
        bigint network_user_invoice_id FK
        bigint network_user_measurement_location_id FK
        numeric oie_amount_kwh
        numeric oie_max_kwh
        numeric oie_min_kwh
        numeric oie_price_eur
        numeric oie_total_eur
        numeric rnt_amount_kwh
        numeric rnt_max_kwh
        numeric rnt_min_kwh
        numeric rnt_price_eur
        numeric rnt_total_eur
        numeric rvt_amount_kwh
        numeric rvt_max_kwh
        numeric rvt_min_kwh
        numeric rvt_price_eur
        numeric rvt_total_eur
        numeric supply_fee_total_eur
        bigint supply_regulatory_catalogue_id FK
        numeric svt_amount_kw
        numeric svt_peak_kw
        numeric svt_price_eur
        numeric svt_total_eur
        text title
        timestamp_with_time_zone to_date
        numeric total_eur
        numeric trp_amount_kwh
        numeric trp_max_kwh
        numeric trp_min_kwh
        numeric trp_price_eur
        numeric trp_total_eur
        numeric usage_fee_total_eur
        numeric usage_meter_fee_amount
        numeric usage_meter_fee_price_eur
        numeric usage_meter_total_eur
        bigint usage_network_user_catalogue_id FK
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

    network_user_invoice_states {
        text bill_id
        text cancel_reason
        uuid correlation_id PK
        text current_state
        text network_user_invoice_id
    }

    network_user_invoices {
        text al_alti_biz_sub_project_code
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
        text anu_alti_biz_sub_project_code
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
        text bill_id
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
        numeric tax_rate_percent
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

    network_user_representatives {
        bigint network_user_id PK,FK
        text representative_id PK,FK
    }

    network_users {
        text alti_biz_sub_project_code
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

    notification_recipients {
        bigint notification_id PK,FK
        text representative_id PK,FK
        timestamp_with_time_zone seen_on
    }

    notifications {
        text content
        bigint event_id FK
        bigint id PK
        bigint invoice_id FK
        character_varying kind
        text messenger_id FK
        text resolved_by_id FK
        timestamp_with_time_zone resolved_on
        text summary
        timestamp_with_time_zone timestamp
        text title
        ARRAY topics
    }

    outbox_message {
        text body
        character_varying content_type
        uuid conversation_id
        uuid correlation_id
        character_varying destination_address
        timestamp_with_time_zone enqueue_time
        timestamp_with_time_zone expiration_time
        character_varying fault_address
        text headers
        uuid inbox_consumer_id
        uuid inbox_message_id
        uuid initiator_id
        uuid message_id
        text message_type
        uuid outbox_id
        text properties
        uuid request_id
        character_varying response_address
        timestamp_with_time_zone sent_time
        bigint sequence_number PK
        character_varying source_address
    }

    outbox_state {
        timestamp_with_time_zone created
        timestamp_with_time_zone delivered
        bigint last_sequence_number
        uuid lock_id
        uuid outbox_id PK
        bytea row_version
    }

    qrtz_blob_triggers {
        bytea blob_data
        text sched_name PK,FK
        text trigger_group PK,FK
        text trigger_name PK,FK
    }

    qrtz_calendars {
        bytea calendar
        text calendar_name PK
        text sched_name PK
    }

    qrtz_cron_triggers {
        text cron_expression
        text sched_name PK,FK
        text time_zone_id
        text trigger_group PK,FK
        text trigger_name PK,FK
    }

    qrtz_fired_triggers {
        text entry_id PK
        bigint fired_time
        text instance_name
        boolean is_nonconcurrent
        text job_group
        text job_name
        integer priority
        boolean requests_recovery
        text sched_name PK
        bigint sched_time
        text state
        text trigger_group
        text trigger_name
    }

    qrtz_job_details {
        text description
        boolean is_durable
        boolean is_nonconcurrent
        boolean is_update_data
        text job_class_name
        bytea job_data
        text job_group PK
        text job_name PK
        boolean requests_recovery
        text sched_name PK
    }

    qrtz_locks {
        text lock_name PK
        text sched_name PK
    }

    qrtz_paused_trigger_grps {
        text sched_name PK
        text trigger_group PK
    }

    qrtz_scheduler_state {
        bigint checkin_interval
        text instance_name PK
        bigint last_checkin_time
        text sched_name PK
    }

    qrtz_simple_triggers {
        bigint repeat_count
        bigint repeat_interval
        text sched_name PK,FK
        bigint times_triggered
        text trigger_group PK,FK
        text trigger_name PK,FK
    }

    qrtz_simprop_triggers {
        boolean bool_prop_1
        boolean bool_prop_2
        numeric dec_prop_1
        numeric dec_prop_2
        integer int_prop_1
        integer int_prop_2
        bigint long_prop_1
        bigint long_prop_2
        text sched_name PK,FK
        text str_prop_1
        text str_prop_2
        text str_prop_3
        text time_zone_id
        text trigger_group PK,FK
        text trigger_name PK,FK
    }

    qrtz_triggers {
        text calendar_name
        text description
        bigint end_time
        bytea job_data
        text job_group FK
        text job_name FK
        smallint misfire_instr
        bigint next_fire_time
        bigint prev_fire_time
        integer priority
        text sched_name PK,FK
        bigint start_time
        text trigger_group PK
        text trigger_name PK
        text trigger_state
        text trigger_type
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
        role_entity role
        text title
        ARRAY topics
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
        interval_entity interval PK
        text meter_id PK,FK
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
        real active_energy_l1_import_t0_wh
        real active_energy_l2_import_t0_wh
        real active_energy_l3_import_t0_wh
        real active_energy_total_export_t0_wh
        real active_energy_total_import_t0_wh
        real active_energy_total_import_t1_wh
        real active_energy_total_import_t2_wh
        real active_power_l1_net_t0_w
        real active_power_l2_net_t0_w
        real active_power_l3_net_t0_w
        real apparent_power_total_net_t0_va
        real current_l1_any_t0_a
        real current_l2_any_t0_a
        real current_l3_any_t0_a
        text meter_id PK,FK
        real reactive_energy_total_export_t0_varh
        real reactive_energy_total_import_t0_varh
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
    notifications }o--|| events : "event_id"
    location_invoices }o--|| locations : "location_id"
    location_invoices }o--|| representatives : "issued_by_id"
    location_representatives }o--|| locations : "location_id"
    location_representatives }o--|| representatives : "representative_id"
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
    measurement_locations }o--|| network_user_catalogues : "network_user_catalogue_id"
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
    notifications }o--|| messengers : "messenger_id"
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
    network_user_invoices }o--|| network_users : "network_user_id"
    network_user_invoices }o--|| representatives : "issued_by_id"
    notifications }o--|| network_user_invoices : "invoice_id"
    network_user_representatives }o--|| network_users : "network_user_id"
    network_user_representatives }o--|| representatives : "representative_id"
    network_users }o--|| representatives : "created_by_id"
    network_users }o--|| representatives : "deleted_by_id"
    network_users }o--|| representatives : "last_updated_by_id"
    notification_recipients }o--|| notifications : "notification_id"
    notification_recipients }o--|| representatives : "representative_id"
    notifications }o--|| representatives : "resolved_by_id"
    qrtz_blob_triggers }o--|| qrtz_triggers : "sched_name"
    qrtz_blob_triggers }o--|| qrtz_triggers : "trigger_group"
    qrtz_blob_triggers }o--|| qrtz_triggers : "trigger_name"
    qrtz_cron_triggers }o--|| qrtz_triggers : "sched_name"
    qrtz_cron_triggers }o--|| qrtz_triggers : "trigger_group"
    qrtz_cron_triggers }o--|| qrtz_triggers : "trigger_name"
    qrtz_triggers }o--|| qrtz_job_details : "sched_name"
    qrtz_triggers }o--|| qrtz_job_details : "job_group"
    qrtz_triggers }o--|| qrtz_job_details : "job_name"
    qrtz_simple_triggers }o--|| qrtz_triggers : "sched_name"
    qrtz_simple_triggers }o--|| qrtz_triggers : "trigger_group"
    qrtz_simple_triggers }o--|| qrtz_triggers : "trigger_name"
    qrtz_simprop_triggers }o--|| qrtz_triggers : "sched_name"
    qrtz_simprop_triggers }o--|| qrtz_triggers : "trigger_group"
    qrtz_simprop_triggers }o--|| qrtz_triggers : "trigger_name"
    regulatory_catalogues }o--|| representatives : "created_by_id"
    regulatory_catalogues }o--|| representatives : "deleted_by_id"
    regulatory_catalogues }o--|| representatives : "last_updated_by_id"
    representatives }o--|| representatives : "created_by_id"
    representatives }o--|| representatives : "deleted_by_id"
    representatives }o--|| representatives : "last_updated_by_id"
```
