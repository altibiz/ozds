using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  SchneideriEM3xxxAggregateEntity : AggregateEntity<SchneideriEM3xxxMeterEntity>
{
#pragma warning disable CA1707
  public float VoltageL1AnyT0Avg_V { get; set; }
  public float VoltageL2AnyT0Avg_V { get; set; }
  public float VoltageL3AnyT0Avg_V { get; set; }
  public float CurrentL1AnyT0Avg_A { get; set; }
  public float CurrentL2AnyT0Avg_A { get; set; }
  public float CurrentL3AnyT0Avg_A { get; set; }
  public float ActivePowerL1NetT0Avg_W { get; set; }
  public float ActivePowerL2NetT0Avg_W { get; set; }
  public float ActivePowerL3NetT0Avg_W { get; set; }
  public float ReactivePowerTotalNetT0Avg_VAR { get; set; }
  public float ApparentPowerTotalNetT0Avg_VA { get; set; }
  public float ActiveEnergyTotalImportT0Min_Wh { get; set; }
  public float ActiveEnergyTotalImportT0Max_Wh { get; set; }
  public float ActiveEnergyTotalExportT0Min_Wh { get; set; }
  public float ActiveEnergyTotalExportT0Max_Wh { get; set; }
  public float ReactiveEnergyTotalImportT0Min_VARh { get; set; }
  public float ReactiveEnergyTotalImportT0Max_VARh { get; set; }
  public float ReactiveEnergyTotalExportT0Min_VARh { get; set; }
  public float ReactiveEnergyTotalExportT0Max_VARh { get; set; }
  public float ActiveEnergyTotalImportT1Min_Wh { get; set; }
  public float ActiveEnergyTotalImportT1Max_Wh { get; set; }
  public float ActiveEnergyTotalImportT2Min_Wh { get; set; }
  public float ActiveEnergyTotalImportT2Max_Wh { get; set; }
#pragma warning restore CA1707
}

public class
  SchneideriEM3xxxAggregateEntityTypeConfiguration : EntityTypeConfiguration<
  SchneideriEM3xxxAggregateEntity>
{
  public override void Configure(
    EntityTypeBuilder<SchneideriEM3xxxAggregateEntity> builder)
  {
    builder.ToTable("schneider_iem3xxx_aggregates");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.VoltageL1AnyT0Avg_V))
      .HasColumnName("voltage_l1_any_t0_avg_v");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.VoltageL2AnyT0Avg_V))
      .HasColumnName("voltage_l2_any_t0_avg_v");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.VoltageL3AnyT0Avg_V))
      .HasColumnName("voltage_l3_any_t0_avg_v");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.CurrentL1AnyT0Avg_A))
      .HasColumnName("current_l1_any_t0_avg_a");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.CurrentL2AnyT0Avg_A))
      .HasColumnName("current_l2_any_t0_avg_a");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.CurrentL3AnyT0Avg_A))
      .HasColumnName("current_l3_any_t0_avg_a");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL1NetT0Avg_W))
      .HasColumnName("active_power_l1_net_t0_avg_w");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL2NetT0Avg_W))
      .HasColumnName("active_power_l2_net_t0_avg_w");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL3NetT0Avg_W))
      .HasColumnName("active_power_l3_net_t0_avg_w");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ReactivePowerTotalNetT0Avg_VAR))
      .HasColumnName("reactive_power_total_net_t0_avg_var");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ApparentPowerTotalNetT0Avg_VA))
      .HasColumnName("apparent_power_total_net_t0_avg_va");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalImportT0Min_Wh))
      .HasColumnName("active_energy_total_import_t0_min_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalImportT0Max_Wh))
      .HasColumnName("active_energy_total_import_t0_max_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalExportT0Min_Wh))
      .HasColumnName("active_energy_total_export_t0_min_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalExportT0Max_Wh))
      .HasColumnName("active_energy_total_export_t0_max_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ReactiveEnergyTotalImportT0Min_VARh))
      .HasColumnName("reactive_energy_total_import_t0_min_varh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ReactiveEnergyTotalImportT0Max_VARh))
      .HasColumnName("reactive_energy_total_import_t0_max_varh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ReactiveEnergyTotalExportT0Min_VARh))
      .HasColumnName("reactive_energy_total_export_t0_min_varh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ReactiveEnergyTotalExportT0Max_VARh))
      .HasColumnName("reactive_energy_total_export_t0_max_varh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalImportT1Min_Wh))
      .HasColumnName("active_energy_total_import_t1_min_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalImportT1Max_Wh))
      .HasColumnName("active_energy_total_import_t1_max_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalImportT2Min_Wh))
      .HasColumnName("active_energy_total_import_t2_min_wh");

    builder
      .Property(nameof(SchneideriEM3xxxAggregateEntity
        .ActiveEnergyTotalImportT2Max_Wh))
      .HasColumnName("active_energy_total_import_t2_max_wh");
  }
}
