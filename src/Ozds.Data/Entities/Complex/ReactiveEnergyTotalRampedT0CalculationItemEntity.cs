using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Complex;

public abstract class ReactiveEnergyTotalRampedT0CalculationItemEntity
  : CalculationItemEntity
{
  public decimal ReactiveImportMin_kVARh { get; set; }
  public decimal ReactiveImportMax_kVARh { get; set; }
  public decimal ReactiveImportAmount_kVARh { get; set; }
  public decimal ReactiveExportMin_kVARh { get; set; }
  public decimal ReactiveExportMax_kVARh { get; set; }
  public decimal ReactiveExportAmount_kVARh { get; set; }
  public decimal ActiveImportMin_kWh { get; set; }
  public decimal ActiveImportMax_kWh { get; set; }
  public decimal ActiveImportAmount_kWh { get; set; }
  public decimal Amount_kVARh { get; set; }
}

public class UsageReactiveEnergyTotalRampedT0CalculationItemEntity
  : ReactiveEnergyTotalRampedT0CalculationItemEntity
{
}

public static class ReactiveEnergyTotalRampedT0CalculationItemEntityExtensions
{
  public static void UsageReactiveEnergyTotalRampedT0CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.CalculationItem("jen");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveImportMin_kVARh))
      .HasColumnName(
        "jen_reactive_import_min_kvarh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveImportMax_kVARh))
      .HasColumnName(
        "jen_reactive_import_max_kvarh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveImportAmount_kVARh))
      .HasColumnName(
        "jen_reactive_import_amount_kvarh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveExportMin_kVARh))
      .HasColumnName(
        "jen_reactive_export_min_kvarh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveExportMax_kVARh))
      .HasColumnName(
        "jen_reactive_export_max_kvarh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveExportAmount_kVARh))
      .HasColumnName(
        "jen_reactive_export_amount_kvarh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ActiveImportMin_kWh))
      .HasColumnName(
        "jen_active_import_min_kwh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ActiveImportMax_kWh))
      .HasColumnName(
        "jen_active_import_max_kwh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ActiveImportAmount_kWh))
      .HasColumnName(
        "jen_active_import_amount_kwh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .Amount_kVARh))
      .HasColumnName(
        "jen_ramped_amount_kvarh");
  }
}
