using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class AbbB2xMeasurementEntity : MeasurementEntity<AbbB2xMeterEntity>
{
#pragma warning disable CA1707
  public float VoltageL1AnyT0_V { get; set; }
  public float VoltageL2AnyT0_V { get; set; }
  public float VoltageL3AnyT0_V { get; set; }
  public float CurrentL1AnyT0_A { get; set; }
  public float CurrentL2AnyT0_A { get; set; }
  public float CurrentL3AnyT0_A { get; set; }
  public float ActivePowerL1NetT0_W { get; set; }
  public float ActivePowerL2NetT0_W { get; set; }
  public float ActivePowerL3NetT0_W { get; set; }
  public float ReactivePowerL1NetT0_VAR { get; set; }
  public float ReactivePowerL2NetT0_VAR { get; set; }
  public float ReactivePowerL3NetT0_VAR { get; set; }
  public float ActiveEnergyL1ImportT0_Wh { get; set; }
  public float ActiveEnergyL2ImportT0_Wh { get; set; }
  public float ActiveEnergyL3ImportT0_Wh { get; set; }
  public float ActiveEnergyL1ExportT0_Wh { get; set; }
  public float ActiveEnergyL2ExportT0_Wh { get; set; }
  public float ActiveEnergyL3ExportT0_Wh { get; set; }
  public float ReactiveEnergyL1ImportT0_VARh { get; set; }
  public float ReactiveEnergyL2ImportT0_VARh { get; set; }
  public float ReactiveEnergyL3ImportT0_VARh { get; set; }
  public float ReactiveEnergyL1ExportT0_VARh { get; set; }
  public float ReactiveEnergyL2ExportT0_VARh { get; set; }
  public float ReactiveEnergyL3ExportT0_VARh { get; set; }
  public float ActiveEnergyTotalImportT0_Wh { get; set; }
  public float ActiveEnergyTotalExportT0_Wh { get; set; }
  public float ReactiveEnergyTotalImportT0_VARh { get; set; }
  public float ReactiveEnergyTotalExportT0_VARh { get; set; }
  public float ActiveEnergyTotalImportT1_Wh { get; set; } = default;
  public float ActiveEnergyTotalImportT2_Wh { get; set; } = default;
#pragma warning restore CA1707
}

public class AbbB2xMeasurementEntityTypeConfiguration : EntityTypeConfiguration<AbbB2xMeasurementEntity>
{
  public override void Configure(EntityTypeBuilder<AbbB2xMeasurementEntity> builder)
  {
    builder.ToTable("abb_b2x_measurements");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.VoltageL1AnyT0_V))
      .HasColumnName("voltage_l1_any_t0_v");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.VoltageL2AnyT0_V))
      .HasColumnName("voltage_l2_any_t0_v");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.VoltageL3AnyT0_V))
      .HasColumnName("voltage_l3_any_t0_v");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.CurrentL1AnyT0_A))
      .HasColumnName("current_l1_any_t0_a");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.CurrentL2AnyT0_A))
      .HasColumnName("current_l2_any_t0_a");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.CurrentL3AnyT0_A))
      .HasColumnName("current_l3_any_t0_a");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActivePowerL1NetT0_W))
      .HasColumnName("active_power_l1_any_t0_w");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActivePowerL2NetT0_W))
      .HasColumnName("active_power_l2_any_t0_w");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActivePowerL3NetT0_W))
      .HasColumnName("active_power_l3_any_t0_w");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactivePowerL1NetT0_VAR))
      .HasColumnName("reactive_power_l1_any_t0_var");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactivePowerL2NetT0_VAR))
      .HasColumnName("reactive_power_l2_any_t0_var");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactivePowerL3NetT0_VAR))
      .HasColumnName("reactive_power_l3_any_t0_var");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyL1ImportT0_Wh))
      .HasColumnName("active_energy_l1_import_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyL2ImportT0_Wh))
      .HasColumnName("active_energy_l2_import_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyL3ImportT0_Wh))
      .HasColumnName("active_energy_l3_import_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyL1ExportT0_Wh))
      .HasColumnName("active_energy_l1_export_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyL2ExportT0_Wh))
      .HasColumnName("active_energy_l2_export_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyL3ExportT0_Wh))
      .HasColumnName("active_energy_l3_export_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyL1ImportT0_VARh))
      .HasColumnName("reactive_energy_l1_import_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyL2ImportT0_VARh))
      .HasColumnName("reactive_energy_l2_import_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyL3ImportT0_VARh))
      .HasColumnName("reactive_energy_l3_import_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyL1ExportT0_VARh))
      .HasColumnName("reactive_energy_l1_export_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyL2ExportT0_VARh))
      .HasColumnName("reactive_energy_l2_export_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyL3ExportT0_VARh))
      .HasColumnName("reactive_energy_l3_export_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalImportT0_Wh))
      .HasColumnName("active_energy_total_import_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalExportT0_Wh))
      .HasColumnName("active_energy_total_export_t0_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyTotalImportT0_VARh))
      .HasColumnName("reactive_energy_total_import_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ReactiveEnergyTotalExportT0_VARh))
      .HasColumnName("reactive_energy_total_export_t0_varh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalImportT1_Wh))
      .HasColumnName("active_energy_total_import_t1_wh");

    builder
      .Property(nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalImportT2_Wh))
      .HasColumnName("active_energy_total_import_t2_wh");
  }
}
