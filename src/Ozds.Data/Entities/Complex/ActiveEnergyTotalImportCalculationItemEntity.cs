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

public class UsageActiveEnergyTotalImportT0CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class UsageActiveEnergyTotalImportT1CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class UsageActiveEnergyTotalImportT2CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class SupplyActiveEnergyTotalImportT1CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class SupplyActiveEnergyTotalImportT2CalculationItemEntity
  : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class
  SupplyBusinessUsageFeeCalculationItemEntity : ActiveEnergyTotalImportCalculationItemEntity
{
}

public class
  SupplyRenewableEnergyFeeCalculationItemEntity : ActiveEnergyTotalImportCalculationItemEntity
{
}

public static class ActiveEnergyImportCalculationItemEntityExtensions
{
  public static void UsageActiveEnergyTotalImportT0CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("usage", "t0");
  }

  public static void UsageActiveEnergyTotalImportT1CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("usage", "t1");
  }

  public static void UsageActiveEnergyTotalImportT2CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("usage", "t2");
  }

  public static void SupplyActiveEnergyTotalImportT1CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("supply", "t1");
  }

  public static void SupplyActiveEnergyTotalImportT2CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("supply", "t2");
  }

  public static void SupplyBusinessUsageCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("supply_business_usage_fee", "t0");
  }

  public static void SupplyRenewableEnergyCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("supply_renewable_energy_fee", "t0");
  }

  private static void ActiveEnergyImportCalculationItem(
    this ComplexPropertyBuilder builder,
    string prefix,
    string tariffColumnPart
  )
  {
    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Min_Wh))
      .HasColumnName(
        $"{prefix}_active_energy_total_import_{tariffColumnPart}_min_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Max_Wh))
      .HasColumnName(
        $"{prefix}_active_energy_total_import_{tariffColumnPart}_max_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Amount_Wh))
      .HasColumnName(
        $"{prefix}_active_energy_total_import_{tariffColumnPart}_amount_wh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Price_EUR))
      .HasColumnName(
        $"{prefix}_active_energy_total_import_{tariffColumnPart}_price_eur");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Total_EUR))
      .HasColumnName(
        $"{prefix}_active_energy_total_import_{tariffColumnPart}_total_eur");
  }
}
