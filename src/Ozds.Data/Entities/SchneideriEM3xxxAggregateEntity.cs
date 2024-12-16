using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  SchneideriEM3xxxAggregateEntity : AggregateEntity<SchneideriEM3xxxMeterEntity>
{
#pragma warning disable CA1707
  public InstantaneousAggregateMeasureEntity VoltageL1AnyT0_V { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity VoltageL2AnyT0_V { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity VoltageL3AnyT0_V { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity CurrentL1AnyT0_A { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity CurrentL2AnyT0_A { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity CurrentL3AnyT0_A { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity ActivePowerL1NetT0_W { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity ActivePowerL2NetT0_W { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity ActivePowerL3NetT0_W { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity ReactivePowerTotalNetT0_VAR { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity ApparentPowerTotalNetT0_VA { get; set; } = default!;
  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT0_Wh { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalImportT0_W { get; set; } = default!;
  public CumulativeAggregateMeasureEntity ActiveEnergyTotalExportT0_Wh { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalExportT0_W { get; set; } = default!;
  public CumulativeAggregateMeasureEntity ReactiveEnergyTotalImportT0_VARh { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity DerivedReactivePowerTotalImportT0_VAR { get; set; } = default!;
  public CumulativeAggregateMeasureEntity ReactiveEnergyTotalExportT0_VARh { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity DerivedReactivePowerTotalExportT0_VAR { get; set; } = default!;
  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT1_Wh { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalImportT1_W { get; set; } = default!;
  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT2_Wh { get; set; } = default!;
  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalImportT2_W { get; set; } = default!;
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
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.VoltageL1AnyT0_V))
      .InstantaneousAggregateMeasure("voltage_l1_any_t0", "v");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.VoltageL2AnyT0_V))
      .InstantaneousAggregateMeasure("voltage_l2_any_t0", "v");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.VoltageL3AnyT0_V))
      .InstantaneousAggregateMeasure("voltage_l3_any_t0", "v");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.CurrentL1AnyT0_A))
      .InstantaneousAggregateMeasure("current_l1_any_t0", "a");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.CurrentL2AnyT0_A))
      .InstantaneousAggregateMeasure("current_l2_any_t0", "a");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.CurrentL3AnyT0_A))
      .InstantaneousAggregateMeasure("current_l3_any_t0", "a");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL1NetT0_W))
      .InstantaneousAggregateMeasure("active_power_l1_net_t0", "w");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL2NetT0_W))
      .InstantaneousAggregateMeasure("active_power_l2_net_t0", "w");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActivePowerL3NetT0_W))
      .InstantaneousAggregateMeasure("active_power_l3_net_t0", "w");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ReactivePowerTotalNetT0_VAR))
      .InstantaneousAggregateMeasure("reactive_power_total_net_t0", "var");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ApparentPowerTotalNetT0_VA))
      .InstantaneousAggregateMeasure("apparent_power_total_net_t0", "va");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActiveEnergyTotalImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t0", "wh");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.DerivedActivePowerTotalImportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_total_import_t0", "w");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActiveEnergyTotalExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_total_export_t0", "wh");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.DerivedActivePowerTotalExportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_total_export_t0", "w");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ReactiveEnergyTotalImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_total_import_t0", "varh");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.DerivedReactivePowerTotalImportT0_VAR))
      .InstantaneousAggregateMeasure("derived_reactive_power_total_import_t0", "var");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ReactiveEnergyTotalExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_total_export_t0", "varh");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.DerivedReactivePowerTotalExportT0_VAR))
      .InstantaneousAggregateMeasure("derived_reactive_power_total_export_t0", "var");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActiveEnergyTotalImportT1_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t1", "wh");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.DerivedActivePowerTotalImportT1_W))
      .InstantaneousAggregateMeasure("derived_active_power_total_import_t1", "w");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.ActiveEnergyTotalImportT2_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t2", "wh");

    builder
      .ComplexProperty(nameof(SchneideriEM3xxxAggregateEntity.DerivedActivePowerTotalImportT2_W))
      .InstantaneousAggregateMeasure("derived_active_power_total_import_t2", "w");
  }
}
