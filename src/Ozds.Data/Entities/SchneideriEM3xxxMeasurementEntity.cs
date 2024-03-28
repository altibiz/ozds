using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  SchneideriEM3xxxMeasurementEntity : MeasurementEntity<
  SchneideriEM3xxxMeterEntity>
{
  public float VoltageL1AnyT0_V { get; set; }
  public float VoltageL2AnyT0_V { get; set; }
  public float VoltageL3AnyT0_V { get; set; }
  public float CurrentL1AnyT0_A { get; set; }
  public float CurrentL2AnyT0_A { get; set; }
  public float CurrentL3AnyT0_A { get; set; }
  public float ActivePowerL1NetT0_W { get; set; }
  public float ActivePowerL2NetT0_W { get; set; }
  public float ActivePowerL3NetT0_W { get; set; }
  public float ReactivePowerTotalNetT0_VAR { get; set; }
  public float ApparentPowerTotalNetT0_VA { get; set; }
  public float ActiveEnergyL1ImportT0_Wh { get; set; }
  public float ActiveEnergyL2ImportT0_Wh { get; set; }
  public float ActiveEnergyL3ImportT0_Wh { get; set; }
  public float ActiveEnergyTotalImportT0_Wh { get; set; }
  public float ActiveEnergyTotalExportT0_Wh { get; set; }
  public float ReactiveEnergyTotalImportT0_VARh { get; set; }
  public float ReactiveEnergyTotalExportT0_VARh { get; set; }
  public float ActiveEnergyTotalImportT1_Wh { get; set; } = default;
  public float ActiveEnergyTotalImportT2_Wh { get; set; } = default;
#pragma warning disable CA1707
}

public class SchneideriEM3xxxMeasurementEntityTypeConfiguration : EntityTypeConfiguration<SchneideriEM3xxxMeasurementEntity>
{
  public override void Configure(EntityTypeBuilder<SchneideriEM3xxxMeasurementEntity> builder)
  {
    builder.ToTable("schneider_iem3xxx_measurements");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.VoltageL1AnyT0_V))
      .HasColumnName("voltage_l1_any_t0_v");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.VoltageL2AnyT0_V))
      .HasColumnName("voltage_l2_any_t0_v");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.VoltageL3AnyT0_V))
      .HasColumnName("voltage_l3_any_t0_v");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.CurrentL1AnyT0_A))
      .HasColumnName("current_l1_any_t0_a");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.CurrentL2AnyT0_A))
      .HasColumnName("current_l2_any_t0_a");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.CurrentL3AnyT0_A))
      .HasColumnName("current_l3_any_t0_a");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActivePowerL1NetT0_W))
      .HasColumnName("active_power_l1_net_t0_w");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActivePowerL2NetT0_W))
      .HasColumnName("active_power_l2_net_t0_w");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActivePowerL3NetT0_W))
      .HasColumnName("active_power_l3_net_t0_w");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ReactivePowerTotalNetT0_VAR))
      .HasColumnName("reactive_power_total_net_t0_var");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ApparentPowerTotalNetT0_VA))
      .HasColumnName("apparent_power_total_net_t0_va");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyL1ImportT0_Wh))
      .HasColumnName("active_energy_import_l1_t0_wh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyL2ImportT0_Wh))
      .HasColumnName("active_energy_import_l2_t0_wh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyL3ImportT0_Wh))
      .HasColumnName("active_energy_import_l3_t0_wh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalImportT0_Wh))
      .HasColumnName("active_energy_import_total_t0_wh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalExportT0_Wh))
      .HasColumnName("active_energy_export_total_t0_wh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ReactiveEnergyTotalImportT0_VARh))
      .HasColumnName("reactive_energy_import_total_t0_varh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ReactiveEnergyTotalExportT0_VARh))
      .HasColumnName("reactive_energy_export_total_t0_varh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalImportT1_Wh))
      .HasColumnName("active_energy_import_total_t1_wh");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalImportT2_Wh))
      .HasColumnName("active_energy_import_total_t2_wh");
  }
}
