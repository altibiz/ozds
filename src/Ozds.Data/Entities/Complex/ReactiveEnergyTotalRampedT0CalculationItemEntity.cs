using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public class ReactiveEnergyTotalRampedT0CalculationItemEntity
{
  public decimal ImportMin_Wh { get; set; }
  public decimal ImportMax_Wh { get; set; }
  public decimal ImportAmount_Wh { get; set; }
  public decimal ExportMin_Wh { get; set; }
  public decimal ExportMax_Wh { get; set; }
  public decimal ExportAmount_Wh { get; set; }
  public decimal RampedAmount_Wh { get; set; }
  public decimal RampedPrice_EUR { get; set; }
  public decimal RampedTotal_EUR { get; set; }
}

public static class ReactiveEnergyTotalRampedT0CalculationItemEntityExtensions
{
  public static void ReactiveEnergyTotalRampedT0CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ImportMin_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_import_min_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ImportMax_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_import_max_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ImportAmount_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_import_amount_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ExportMin_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_export_min_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ExportMax_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_export_max_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.ExportAmount_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_export_amount_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.RampedAmount_Wh))
      .HasColumnName($"reactive_energy_total_ramped_t0_ramped_amount_wh");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.RampedPrice_EUR))
      .HasColumnName($"reactive_energy_total_ramped_t0_ramped_price_eur");

    builder
      .Property(nameof(ReactiveEnergyTotalRampedT0CalculationItemEntity.RampedTotal_EUR))
      .HasColumnName($"reactive_energy_total_ramped_t0_ramped_total_eur");
  }
}
