using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Complex;

public abstract class ActiveEnergyTotalImportCalculationItemEntity
  : CalculationItemEntity
{
  public decimal Min_kWh { get; set; }
  public decimal Max_kWh { get; set; }
  public decimal Amount_kWh { get; set; }
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
  SupplyBusinessUsageFeeCalculationItemEntity :
  ActiveEnergyTotalImportCalculationItemEntity
{
}

public class
  SupplyRenewableEnergyFeeCalculationItemEntity :
  ActiveEnergyTotalImportCalculationItemEntity
{
}

public static class ActiveEnergyImportCalculationItemEntityExtensions
{
  public static void UsageActiveEnergyTotalImportT0CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("mjt");
  }

  public static void UsageActiveEnergyTotalImportT1CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("mvt");
  }

  public static void UsageActiveEnergyTotalImportT2CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("mnt");
  }

  public static void SupplyActiveEnergyTotalImportT1CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("rvt");
  }

  public static void SupplyActiveEnergyTotalImportT2CalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("rnt");
  }

  public static void SupplyBusinessUsageCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("trp");
  }

  public static void SupplyRenewableEnergyCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder.ActiveEnergyImportCalculationItem("oie");
  }

  private static void ActiveEnergyImportCalculationItem(
    this ComplexPropertyBuilder builder,
    string prefix
  )
  {
    builder.CalculationItem(prefix);

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Min_kWh))
      .HasColumnName(
        $"{prefix}_min_kwh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Max_kWh))
      .HasColumnName(
        $"{prefix}_max_kwh");

    builder
      .Property(nameof(ActiveEnergyTotalImportCalculationItemEntity.Amount_kWh))
      .HasColumnName(
        $"{prefix}_amount_kwh");
  }
}
