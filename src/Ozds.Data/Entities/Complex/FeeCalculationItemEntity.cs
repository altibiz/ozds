using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Complex;

public abstract class FeeCalculationItemEntity
  : CalculationItemEntity
{
  public decimal Amount_N { get; set; }
}

public class UsageMeterFeeCalculationItemEntity : FeeCalculationItemEntity
{
}

public static class FeeCalculationItemEntityExtensions
{
  public static void UsageMeterFeeCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.FeeCalculationItem("usage_meter");
  }

  private static void FeeCalculationItem(
    this ComplexPropertyBuilder builder,
    string prefix
  )
  {
    builder.CalculationItem(prefix);

    builder
      .Property(nameof(FeeCalculationItemEntity.Amount_N))
      .HasColumnName($"{prefix}_fee_amount");
  }
}
