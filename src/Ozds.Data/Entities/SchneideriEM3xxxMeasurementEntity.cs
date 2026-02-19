using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class SchneideriEM3xxxMeasurementEntity
  : MeasurementEntity<SchneideriEM3xxxMeterEntity>
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
  public float ReactivePowerTotalNetT0_VAR { get; set; }
  public float ApparentPowerTotalNetT0_VA { get; set; }
  public long ActiveEnergyL1ImportT0_Wh { get; set; }
  public long ActiveEnergyL2ImportT0_Wh { get; set; }
  public long ActiveEnergyL3ImportT0_Wh { get; set; }
  public long ActiveEnergyTotalImportT0_Wh { get; set; }
  public long ActiveEnergyTotalExportT0_Wh { get; set; }
  public long ReactiveEnergyTotalImportT0_VARh { get; set; }
  public long ReactiveEnergyTotalExportT0_VARh { get; set; }
  public long ActiveEnergyTotalImportT1_Wh { get; set; }
  public long ActiveEnergyTotalImportT2_Wh { get; set; }
#pragma warning restore CA1707
}

public class SchneideriEM3xxxMeasurementEntityTypeConfiguration
  : EntityTypeConfiguration<SchneideriEM3xxxMeasurementEntity>
{
  public override void Configure(
    EntityTypeBuilder<SchneideriEM3xxxMeasurementEntity> builder
  )
  {
    builder.ToTable("schneider_iem3xxx_measurements");

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.VoltageL1AnyT0_V),
      "voltage_l1_any_t0_v"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.VoltageL2AnyT0_V),
      "voltage_l2_any_t0_v"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.VoltageL3AnyT0_V),
      "voltage_l3_any_t0_v"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.CurrentL1AnyT0_A),
      "current_l1_any_t0_a"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.CurrentL2AnyT0_A),
      "current_l2_any_t0_a"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.CurrentL3AnyT0_A),
      "current_l3_any_t0_a"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActivePowerL1NetT0_W),
      "active_power_l1_net_t0_w"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActivePowerL2NetT0_W),
      "active_power_l2_net_t0_w"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActivePowerL3NetT0_W),
      "active_power_l3_net_t0_w"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ReactivePowerTotalNetT0_VAR),
      "reactive_power_total_net_t0_var"
    );

    builder.InstantaneousMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ApparentPowerTotalNetT0_VA),
      "apparent_power_total_net_t0_va"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyL1ImportT0_Wh),
      "active_energy_l1_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyL2ImportT0_Wh),
      "active_energy_l2_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyL3ImportT0_Wh),
      "active_energy_l3_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalImportT0_Wh),
      "active_energy_total_import_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalExportT0_Wh),
      "active_energy_total_export_t0_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(
        SchneideriEM3xxxMeasurementEntity.ReactiveEnergyTotalImportT0_VARh
      ),
      "reactive_energy_total_import_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(
        SchneideriEM3xxxMeasurementEntity.ReactiveEnergyTotalExportT0_VARh
      ),
      "reactive_energy_total_export_t0_varh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalImportT1_Wh),
      "active_energy_total_import_t1_wh"
    );

    builder.CumulativeMeasurementMeasure(
      nameof(SchneideriEM3xxxMeasurementEntity.ActiveEnergyTotalImportT2_Wh),
      "active_energy_total_import_t2_wh"
    );
  }
}
