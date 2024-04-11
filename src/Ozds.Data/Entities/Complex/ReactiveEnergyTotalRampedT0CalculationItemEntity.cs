using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public class ReactiveEnergyTotalRampedT0CalculationItemEntity
{
  public decimal ImportMin_VARh { get; set; }
  public decimal ImportMax_VARh { get; set; }
  public decimal ImportAmount_VARh { get; set; }
  public decimal ExportMin_VARh { get; set; }
  public decimal ExportMax_VARh { get; set; }
  public decimal ExportAmount_VARh { get; set; }
  public decimal Amount_VARh { get; set; }
  public decimal Price_EUR { get; set; }
  public decimal Total_EUR { get; set; }
}

public static class ReactiveEnergyTotalRampedT0CalculationItemEntityExtensions
{
  public static void ReactiveEnergyTotalRampedT0CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ImportMin_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_import_min_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ImportMax_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_import_max_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ImportAmount_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_import_amount_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ExportMin_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_export_min_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ExportMax_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_export_max_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ExportAmount_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_export_amount_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.Amount_VARh))
      .HasColumnName($"reactive_energy_total_ramped_t0_ramped_amount_varh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.Price_EUR))
      .HasColumnName($"reactive_energy_total_ramped_t0_ramped_price_eur");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.Total_EUR))
      .HasColumnName($"reactive_energy_total_ramped_t0_ramped_total_eur");
  }
}
