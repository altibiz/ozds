using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public abstract class FeeCalculationItemEntity
{
  public decimal Amount_N { get; set; }
  public decimal Price_EUR { get; set; }
  public decimal Total_EUR { get; set; }
}

public class UsageMeterFeeCalculationItemEntity : FeeCalculationItemEntity
{
}

public class SupplyBusinessUsageFeeCalculationItemEntity : FeeCalculationItemEntity
{
}

public class SupplyRenewableEnergyFeeCalculationItemEntity : FeeCalculationItemEntity
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

  public static void SupplyBusinessUsageCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.FeeCalculationItem("supply_business_usage");
  }

  public static void SupplyRenewableEnergyCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.FeeCalculationItem("supply_renewable_energy");
  }

  private static void FeeCalculationItem(
    this ComplexPropertyBuilder builder,
    string prefix
  )
  {
    builder
      .Property(nameof(FeeCalculationItemEntity.Amount_N))
      .HasColumnName($"{prefix}_fee_amount");

    builder
      .Property(nameof(FeeCalculationItemEntity.Price_EUR))
      .HasColumnName($"{prefix}_fee_price_eur");

    builder
      .Property(nameof(FeeCalculationItemEntity.Total_EUR))
      .HasColumnName($"{prefix}_total_eur");
  }
}
