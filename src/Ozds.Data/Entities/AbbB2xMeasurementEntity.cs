using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
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
  public long ActiveEnergyL1ImportT0_Wh { get; set; }
  public long ActiveEnergyL2ImportT0_Wh { get; set; }
  public long ActiveEnergyL3ImportT0_Wh { get; set; }
  public long ActiveEnergyL1ExportT0_Wh { get; set; }
  public long ActiveEnergyL2ExportT0_Wh { get; set; }
  public long ActiveEnergyL3ExportT0_Wh { get; set; }
  public long ReactiveEnergyL1ImportT0_VARh { get; set; }
  public long ReactiveEnergyL2ImportT0_VARh { get; set; }
  public long ReactiveEnergyL3ImportT0_VARh { get; set; }
  public long ReactiveEnergyL1ExportT0_VARh { get; set; }
  public long ReactiveEnergyL2ExportT0_VARh { get; set; }
  public long ReactiveEnergyL3ExportT0_VARh { get; set; }
  public long ActiveEnergyTotalImportT0_Wh { get; set; }
  public long ActiveEnergyTotalExportT0_Wh { get; set; }
  public long ReactiveEnergyTotalImportT0_VARh { get; set; }
  public long ReactiveEnergyTotalExportT0_VARh { get; set; }
  public long ActiveEnergyTotalImportT1_Wh { get; set; }
  public long ActiveEnergyTotalImportT2_Wh { get; set; }
#pragma warning restore CA1707
}

public class AbbB2xMeasurementEntityTypeConfiguration
  : EntityTypeConfiguration<AbbB2xMeasurementEntity>
{
  public override void Configure(
    EntityTypeBuilder<AbbB2xMeasurementEntity> builder
  )
  {
    builder.ToTable("abb_b2x_measurements");

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.VoltageL1AnyT0_V),
      "voltage_l1_any_t0_v"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.VoltageL2AnyT0_V),
      "voltage_l2_any_t0_v"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.VoltageL3AnyT0_V),
      "voltage_l3_any_t0_v"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.CurrentL1AnyT0_A),
      "current_l1_any_t0_a"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.CurrentL2AnyT0_A),
      "current_l2_any_t0_a"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.CurrentL3AnyT0_A),
      "current_l3_any_t0_a"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActivePowerL1NetT0_W),
      "active_power_l1_net_t0_w"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActivePowerL2NetT0_W),
      "active_power_l2_net_t0_w"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActivePowerL3NetT0_W),
      "active_power_l3_net_t0_w"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactivePowerL1NetT0_VAR),
      "reactive_power_l1_net_t0_var"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactivePowerL2NetT0_VAR),
      "reactive_power_l2_net_t0_var"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactivePowerL3NetT0_VAR),
      "reactive_power_l3_net_t0_var"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyL1ImportT0_Wh),
      "active_energy_l1_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyL2ImportT0_Wh),
      "active_energy_l2_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyL3ImportT0_Wh),
      "active_energy_l3_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyL1ExportT0_Wh),
      "active_energy_l1_export_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyL2ExportT0_Wh),
      "active_energy_l2_export_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyL3ExportT0_Wh),
      "active_energy_l3_export_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyL1ImportT0_VARh),
      "reactive_energy_l1_import_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyL2ImportT0_VARh),
      "reactive_energy_l2_import_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyL3ImportT0_VARh),
      "reactive_energy_l3_import_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyL1ExportT0_VARh),
      "reactive_energy_l1_export_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyL2ExportT0_VARh),
      "reactive_energy_l2_export_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyL3ExportT0_VARh),
      "reactive_energy_l3_export_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalImportT0_Wh),
      "active_energy_total_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalExportT0_Wh),
      "active_energy_total_export_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyTotalImportT0_VARh),
      "reactive_energy_total_import_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ReactiveEnergyTotalExportT0_VARh),
      "reactive_energy_total_export_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalImportT1_Wh),
      "active_energy_total_import_t1_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(AbbB2xMeasurementEntity.ActiveEnergyTotalImportT2_Wh),
      "active_energy_total_import_t2_wh"
    );
  }
}
