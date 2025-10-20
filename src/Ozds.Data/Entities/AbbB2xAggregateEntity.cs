using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Procedures.Builders;

namespace Ozds.Data.Entities;

public class AbbB2xAggregateEntity : AggregateEntity<AbbB2xMeterEntity>
{
#pragma warning disable CA1707
  public InstantaneousAggregateMeasureEntity VoltageL1AnyT0_V { get; set; } =
    default!;

  public InstantaneousAggregateMeasureEntity VoltageL2AnyT0_V { get; set; } =
    default!;

  public InstantaneousAggregateMeasureEntity VoltageL3AnyT0_V { get; set; } =
    default!;

  public InstantaneousAggregateMeasureEntity CurrentL1AnyT0_A { get; set; } =
    default!;

  public InstantaneousAggregateMeasureEntity CurrentL2AnyT0_A { get; set; } =
    default!;

  public InstantaneousAggregateMeasureEntity CurrentL3AnyT0_A { get; set; } =
    default!;

  public InstantaneousAggregateMeasureEntity
    ActivePowerL1NetT0_W { get; set; } = default!;

  public InstantaneousAggregateMeasureEntity
    ActivePowerL2NetT0_W { get; set; } = default!;

  public InstantaneousAggregateMeasureEntity
    ActivePowerL3NetT0_W { get; set; } = default!;

  public InstantaneousAggregateMeasureEntity ReactivePowerL1NetT0_VAR
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity ReactivePowerL2NetT0_VAR
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity ReactivePowerL3NetT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL1ImportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerL1ImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL2ImportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerL2ImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL3ImportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerL3ImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL1ExportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerL1ExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL2ExportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerL2ExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL3ExportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerL3ExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL1ImportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedReactivePowerL1ImportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL2ImportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedReactivePowerL2ImportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL3ImportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedReactivePowerL3ImportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL1ExportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedReactivePowerL1ExportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL2ExportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedReactivePowerL2ExportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL3ExportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedReactivePowerL3ExportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerTotalImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalExportT0_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerTotalExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyTotalImportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity
    DerivedReactivePowerTotalImportT0_VAR { get; set; } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyTotalExportT0_VARh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity
    DerivedReactivePowerTotalExportT0_VAR { get; set; } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT1_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerTotalImportT1_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT2_Wh
  {
    get;
    set;
  } = default!;

  public DerivedAggregateMeasureEntity DerivedActivePowerTotalImportT2_W
  {
    get;
    set;
  } = default!;
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
      .ComplexProperty(nameof(AbbB2xAggregateEntity.VoltageL1AnyT0_V))
      .InstantaneousAggregateMeasure("voltage_l1_any_t0", "v");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.VoltageL2AnyT0_V))
      .InstantaneousAggregateMeasure("voltage_l2_any_t0", "v");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.VoltageL3AnyT0_V))
      .InstantaneousAggregateMeasure("voltage_l3_any_t0", "v");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.CurrentL1AnyT0_A))
      .InstantaneousAggregateMeasure("current_l1_any_t0", "a");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.CurrentL2AnyT0_A))
      .InstantaneousAggregateMeasure("current_l2_any_t0", "a");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.CurrentL3AnyT0_A))
      .InstantaneousAggregateMeasure("current_l3_any_t0", "a");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActivePowerL1NetT0_W))
      .InstantaneousAggregateMeasure("active_power_l1_net_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActivePowerL2NetT0_W))
      .InstantaneousAggregateMeasure("active_power_l2_net_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActivePowerL3NetT0_W))
      .InstantaneousAggregateMeasure("active_power_l3_net_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ReactivePowerL1NetT0_VAR))
      .InstantaneousAggregateMeasure("reactive_power_l1_net_t0", "var");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ReactivePowerL2NetT0_VAR))
      .InstantaneousAggregateMeasure("reactive_power_l2_net_t0", "var");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ReactivePowerL3NetT0_VAR))
      .InstantaneousAggregateMeasure("reactive_power_l3_net_t0", "var");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL1ImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l1_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL1ImportT0_W))
      .DerivedAggregateMeasure("derived_active_power_l1_import_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL2ImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l2_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL2ImportT0_W))
      .DerivedAggregateMeasure("derived_active_power_l2_import_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL3ImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l3_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL3ImportT0_W))
      .DerivedAggregateMeasure("derived_active_power_l3_import_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL1ExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l1_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL1ExportT0_W))
      .DerivedAggregateMeasure("derived_active_power_l1_export_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL2ExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l2_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL2ExportT0_W))
      .DerivedAggregateMeasure("derived_active_power_l2_export_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL3ExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l3_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL3ExportT0_W))
      .DerivedAggregateMeasure("derived_active_power_l3_export_t0", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL1ImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l1_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL1ImportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_l1_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL2ImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l2_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL2ImportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_l2_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL3ImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l3_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL3ImportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_l3_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL1ExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l1_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL1ExportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_l1_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL2ExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l2_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL2ExportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_l2_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL3ExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l3_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL3ExportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_l3_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalImportT0_W))
      .DerivedAggregateMeasure(
        "derived_active_power_total_import_t0", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_total_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalExportT0_W))
      .DerivedAggregateMeasure(
        "derived_active_power_total_export_t0", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_total_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerTotalImportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_total_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_total_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerTotalExportT0_VAR))
      .DerivedAggregateMeasure(
        "derived_reactive_power_total_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT1_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t1", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalImportT1_W))
      .DerivedAggregateMeasure(
        "derived_active_power_total_import_t1", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT2_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t2", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalImportT2_W))
      .DerivedAggregateMeasure(
        "derived_active_power_total_import_t2", "w");
  }
}

public class AbbB2xAggregateEntityProcedureParts
  : MeasurementProcedureParts<AbbB2xAggregateEntity>
{
  protected override void Configure(
    MeasurementProcedureBuilder<AbbB2xAggregateEntity> builder
  )
  {
    builder
      .InstantaneousAggregateMeasure(x => x.VoltageL1AnyT0_V)
      .InstantaneousAggregateMeasure(x => x.VoltageL2AnyT0_V)
      .InstantaneousAggregateMeasure(x => x.VoltageL3AnyT0_V)
      .InstantaneousAggregateMeasure(x => x.CurrentL1AnyT0_A)
      .InstantaneousAggregateMeasure(x => x.CurrentL2AnyT0_A)
      .InstantaneousAggregateMeasure(x => x.CurrentL3AnyT0_A)
      .InstantaneousAggregateMeasure(x => x.ActivePowerL1NetT0_W)
      .InstantaneousAggregateMeasure(x => x.ActivePowerL2NetT0_W)
      .InstantaneousAggregateMeasure(x => x.ActivePowerL3NetT0_W)
      .InstantaneousAggregateMeasure(x => x.ReactivePowerL1NetT0_VAR)
      .InstantaneousAggregateMeasure(x => x.ReactivePowerL2NetT0_VAR)
      .InstantaneousAggregateMeasure(x => x.ReactivePowerL3NetT0_VAR)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyL1ImportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerL1ImportT0_W,
        x => x.ActiveEnergyL1ImportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyL2ImportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerL2ImportT0_W,
        x => x.ActiveEnergyL2ImportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyL3ImportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerL3ImportT0_W,
        x => x.ActiveEnergyL3ImportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyL1ExportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerL1ExportT0_W,
        x => x.ActiveEnergyL1ExportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyL2ExportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerL2ExportT0_W,
        x => x.ActiveEnergyL2ExportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyL3ExportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerL3ExportT0_W,
        x => x.ActiveEnergyL3ExportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyL1ImportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerL1ImportT0_VAR,
        x => x.ReactiveEnergyL1ImportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyL2ImportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerL2ImportT0_VAR,
        x => x.ReactiveEnergyL2ImportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyL3ImportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerL3ImportT0_VAR,
        x => x.ReactiveEnergyL3ImportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyL1ExportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerL1ExportT0_VAR,
        x => x.ReactiveEnergyL1ExportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyL2ExportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerL2ExportT0_VAR,
        x => x.ReactiveEnergyL2ExportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyL3ExportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerL3ExportT0_VAR,
        x => x.ReactiveEnergyL3ExportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyTotalImportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerTotalImportT0_W,
        x => x.ActiveEnergyTotalImportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyTotalExportT0_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerTotalExportT0_W,
        x => x.ActiveEnergyTotalExportT0_Wh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyTotalImportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerTotalImportT0_VAR,
        x => x.ReactiveEnergyTotalImportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ReactiveEnergyTotalExportT0_VARh)
      .DerivedAggregateMeasure(
        x => x.DerivedReactivePowerTotalExportT0_VAR,
        x => x.ReactiveEnergyTotalExportT0_VARh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyTotalImportT1_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerTotalImportT1_W,
        x => x.ActiveEnergyTotalImportT1_Wh)
      .CumulativeAggregateMeasure(x => x.ActiveEnergyTotalImportT2_Wh)
      .DerivedAggregateMeasure(
        x => x.DerivedActivePowerTotalImportT2_W,
        x => x.ActiveEnergyTotalImportT2_Wh);
  }
}
