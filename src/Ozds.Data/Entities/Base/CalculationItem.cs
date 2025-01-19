using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Base;

public class CalculationItemEntity : ICalculationItemEntity
{
  public decimal Price_EUR { get; set; }

  public decimal Total_EUR { get; set; }
}

public static class CalculationItemEntityExtensions
{
  public static void CalculationItem(
    this ComplexPropertyBuilder builder,
    string prefix
  )
  {
    builder
      .Property(nameof(CalculationItemEntity.Price_EUR))
      .HasColumnName($"{prefix}_price_eur");

    builder
      .Property(nameof(CalculationItemEntity.Total_EUR))
      .HasColumnName($"{prefix}_total_eur");
  }
}
