using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

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
      .MonetaryValue(
        nameof(CalculationItemEntity.Price_EUR),
        $"{prefix}_price_eur"
      );

    builder
      .MonetaryValue(
        nameof(CalculationItemEntity.Total_EUR),
        $"{prefix}_total_eur"
      );
  }
}
