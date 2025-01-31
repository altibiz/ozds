using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

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

  public InstantaneousAggregateMeasureEntity DerivedActivePowerL1ImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL2ImportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerL2ImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL3ImportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerL3ImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL1ExportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerL1ExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL2ExportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerL2ExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyL3ExportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerL3ExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL1ImportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedReactivePowerL1ImportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL2ImportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedReactivePowerL2ImportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL3ImportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedReactivePowerL3ImportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL1ExportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedReactivePowerL1ExportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL2ExportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedReactivePowerL2ExportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyL3ExportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedReactivePowerL3ExportT0_VAR
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalImportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalExportT0_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalExportT0_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyTotalImportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity
    DerivedReactivePowerTotalImportT0_VAR { get; set; } = default!;

  public CumulativeAggregateMeasureEntity ReactiveEnergyTotalExportT0_VARh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity
    DerivedReactivePowerTotalExportT0_VAR { get; set; } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT1_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalImportT1_W
  {
    get;
    set;
  } = default!;

  public CumulativeAggregateMeasureEntity ActiveEnergyTotalImportT2_Wh
  {
    get;
    set;
  } = default!;

  public InstantaneousAggregateMeasureEntity DerivedActivePowerTotalImportT2_W
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
      .InstantaneousAggregateMeasure("derived_active_power_l1_import_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL2ImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l2_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL2ImportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_l2_import_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL3ImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l3_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL3ImportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_l3_import_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL1ExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l1_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL1ExportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_l1_export_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL2ExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l2_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL2ExportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_l2_export_t0", "w");

    builder
      .ComplexProperty(nameof(AbbB2xAggregateEntity.ActiveEnergyL3ExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_l3_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerL3ExportT0_W))
      .InstantaneousAggregateMeasure("derived_active_power_l3_export_t0", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL1ImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l1_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL1ImportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_l1_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL2ImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l2_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL2ImportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_l2_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL3ImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l3_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL3ImportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_l3_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL1ExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l1_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL1ExportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_l1_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL2ExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l2_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL2ExportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_l2_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyL3ExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_l3_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerL3ExportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_l3_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalImportT0_W))
      .InstantaneousAggregateMeasure(
        "derived_active_power_total_import_t0", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalExportT0_Wh))
      .CumulativeAggregateMeasure("active_energy_total_export_t0", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalExportT0_W))
      .InstantaneousAggregateMeasure(
        "derived_active_power_total_export_t0", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalImportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_total_import_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerTotalImportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_total_import_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ReactiveEnergyTotalExportT0_VARh))
      .CumulativeAggregateMeasure("reactive_energy_total_export_t0", "varh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedReactivePowerTotalExportT0_VAR))
      .InstantaneousAggregateMeasure(
        "derived_reactive_power_total_export_t0", "var");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT1_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t1", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalImportT1_W))
      .InstantaneousAggregateMeasure(
        "derived_active_power_total_import_t1", "w");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.ActiveEnergyTotalImportT2_Wh))
      .CumulativeAggregateMeasure("active_energy_total_import_t2", "wh");

    builder
      .ComplexProperty(
        nameof(AbbB2xAggregateEntity.DerivedActivePowerTotalImportT2_W))
      .InstantaneousAggregateMeasure(
        "derived_active_power_total_import_t2", "w");
  }
}
