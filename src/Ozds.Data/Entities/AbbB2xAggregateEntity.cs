using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class AbbB2xAggregateEntity : AggregateEntity<AbbB2xMeterEntity>
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
  public float ReactivePowerL1NetT0Avg_VAR { get; set; }
  public float ReactivePowerL2NetT0Avg_VAR { get; set; }
  public float ReactivePowerL3NetT0Avg_VAR { get; set; }
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
  AbbB2xAggregateEntityTypeConfiguration : EntityTypeConfiguration<
  AbbB2xAggregateEntity>
{
  public override void Configure(
    EntityTypeBuilder<AbbB2xAggregateEntity> builder)
  {
    builder.ToTable("abb_b2x_aggregates");

    builder
      .Property(nameof(AbbB2xAggregateEntity.VoltageL1AnyT0Avg_V))
      .HasColumnName("voltage_l1_any_t0_avg_v");

    builder
      .Property(nameof(AbbB2xAggregateEntity.VoltageL2AnyT0Avg_V))
      .HasColumnName("voltage_l2_any_t0_avg_v");

    builder
      .Property(nameof(AbbB2xAggregateEntity.VoltageL3AnyT0Avg_V))
      .HasColumnName("voltage_l3_any_t0_avg_v");

    builder
      .Property(nameof(AbbB2xAggregateEntity.CurrentL1AnyT0Avg_A))
      .HasColumnName("current_l1_any_t0_avg_a");

    builder
      .Property(nameof(AbbB2xAggregateEntity.CurrentL2AnyT0Avg_A))
      .HasColumnName("current_l2_any_t0_avg_a");

    builder
      .Property(nameof(AbbB2xAggregateEntity.CurrentL3AnyT0Avg_A))
      .HasColumnName("current_l3_any_t0_avg_a");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActivePowerL1NetT0Avg_W))
      .HasColumnName("active_power_l1_net_t0_avg_w");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActivePowerL2NetT0Avg_W))
      .HasColumnName("active_power_l2_net_t0_avg_w");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActivePowerL3NetT0Avg_W))
      .HasColumnName("active_power_l3_net_t0_avg_w");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ReactivePowerL1NetT0Avg_VAR))
      .HasColumnName("reactive_power_l1_net_t0_avg_var");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ReactivePowerL2NetT0Avg_VAR))
      .HasColumnName("reactive_power_l2_net_t0_avg_var");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ReactivePowerL3NetT0Avg_VAR))
      .HasColumnName("reactive_power_l3_net_t0_avg_var");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT0Min_Wh))
      .HasColumnName("active_energy_total_import_t0_min_wh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT0Max_Wh))
      .HasColumnName("active_energy_total_import_t0_max_wh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalExportT0Min_Wh))
      .HasColumnName("active_energy_total_export_t0_min_wh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalExportT0Max_Wh))
      .HasColumnName("active_energy_total_export_t0_max_wh");

    builder
      .Property(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalImportT0Min_VARh))
      .HasColumnName("reactive_energy_total_import_t0_min_varh");

    builder
      .Property(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalImportT0Max_VARh))
      .HasColumnName("reactive_energy_total_import_t0_max_varh");

    builder
      .Property(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalExportT0Min_VARh))
      .HasColumnName("reactive_energy_total_export_t0_min_varh");

    builder
      .Property(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalExportT0Max_VARh))
      .HasColumnName("reactive_energy_total_export_t0_max_varh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT1Min_Wh))
      .HasColumnName("active_energy_total_import_t1_min_wh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT1Max_Wh))
      .HasColumnName("active_energy_total_import_t1_max_wh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT2Min_Wh))
      .HasColumnName("active_energy_total_import_t2_min_wh");

    builder
      .Property(nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT2Max_Wh))
      .HasColumnName("active_energy_total_import_t2_max_wh");
  }
}
