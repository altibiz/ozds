using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public abstract class ActiveEnergyTotalImportCalculationItemEntity
{
  public decimal Min_Wh { get; set; }
  public decimal Max_Wh { get; set; }
  public decimal Amount_Wh { get; set; }
  public decimal Price_EUR { get; set; }
  public decimal Total_EUR { get; set; }
}

public class ActiveEnergyTotalImportT0CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class ActiveEnergyTotalImportT1CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class ActiveEnergyTotalImportT2CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public static class ActiveEnergyImportCalculationItemEntityExtensions
{
  public static void ActiveEnergyImportCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    var tariffColumnPart = builder.Metadata switch
    {
      var metadata when metadata.ClrType == typeof(ActiveEnergyTotalImportT0CalculationItemEntity) => "t0",
      var metadata when metadata.ClrType == typeof(ActiveEnergyTotalImportT1CalculationItemEntity) => "t1",
      var metadata when metadata.ClrType == typeof(ActiveEnergyTotalImportT2CalculationItemEntity) => "t2",
      _ => throw new InvalidOperationException("Unknown tariff type")
    };

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Min_Wh))
      .HasColumnName($"active_energy_total_import_{tariffColumnPart}_min_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Max_Wh))
      .HasColumnName($"active_energy_total_import_{tariffColumnPart}_max_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Amount_Wh))
      .HasColumnName($"active_energy_total_import_{tariffColumnPart}_amount_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Price_EUR))
      .HasColumnName($"active_energy_total_import_{tariffColumnPart}_price_eur");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Total_EUR))
      .HasColumnName($"active_energy_total_import_{tariffColumnPart}_total_eur");
  }
}
