using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Complex;

public class ActiveEnergyTotalImportCalculationItemEntity
{
  public decimal Min_Wh { get; set; }
  public decimal Max_Wh { get; set; }
  public decimal Amount_Wh { get; set; }
  public decimal Price_EUR { get; set; }
  public decimal Total_EUR { get; set; }
}

public static class ActiveEnergyImportCalculationItemEntityExtensions
{
  public static void ActiveEnergyImportCalculationItem(
    this ComplexPropertyBuilder builder,
    TariffEntity tariff
  )
  {
    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Min_Wh))
      .HasColumnName($"active_energy_total_import_{tariff.ColumnPart()}_min_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Max_Wh))
      .HasColumnName($"active_energy_total_import_{tariff.ColumnPart()}_max_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Amount_Wh))
      .HasColumnName($"active_energy_total_import_{tariff.ColumnPart()}_amount_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Price_EUR))
      .HasColumnName($"active_energy_total_import_{tariff.ColumnPart()}_price_eur");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Total_EUR))
      .HasColumnName($"active_energy_total_import_{tariff.ColumnPart()}_total_eur");
  }
}
