using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public abstract class ReactiveEnergyTotalRampedT0CalculationItemEntity
{
  public decimal ReactiveImportMin_VARh { get; set; }
  public decimal ReactiveImportMax_VARh { get; set; }
  public decimal ReactiveImportAmount_VARh { get; set; }
  public decimal ReactiveExportMin_VARh { get; set; }
  public decimal ReactiveExportMax_VARh { get; set; }
  public decimal ReactiveExportAmount_VARh { get; set; }
  public decimal ActiveImportMin_Wh { get; set; }
  public decimal ActiveImportMax_Wh { get; set; }
  public decimal ActiveImportAmount_Wh { get; set; }
  public decimal Amount_VARh { get; set; }
  public decimal Price_EUR { get; set; }
  public decimal Total_EUR { get; set; }
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
    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveImportMin_VARh))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_reactive_import_min_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveImportMax_VARh))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_reactive_import_max_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveImportAmount_VARh))
      .HasColumnName(
        "usage_reactive_energy_total_ramped_t0_reactive_import_amount_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveExportMin_VARh))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_reactive_export_min_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveExportMax_VARh))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_reactive_export_max_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ReactiveExportAmount_VARh))
      .HasColumnName(
        "usage_reactive_energy_total_ramped_t0_reactive_export_amount_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ActiveImportMin_Wh))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_active_import_min_wh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ActiveImportMax_Wh))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_active_import_max_wh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .ActiveImportAmount_Wh))
      .HasColumnName(
        "usage_reactive_energy_total_ramped_t0_active_import_amount_wh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .Amount_VARh))
      .HasColumnName(
        "usage_reactive_energy_total_ramped_t0_ramped_amount_varh");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .Price_EUR))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_ramped_price_eur");

    builder
      .Property(
        nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity
          .Total_EUR))
      .HasColumnName("usage_reactive_energy_total_ramped_t0_ramped_total_eur");
  }
}
