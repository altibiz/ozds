using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public abstract class ActivePowerTotalImportT1PeakCalculationItemEntity
{
  public decimal Peak_kW { get; set; }
  public decimal Amount_kW { get; set; }
  public decimal Price_EUR { get; set; }
  public decimal Total_EUR { get; set; }
}

public class UsageActivePowerTotalImportT1PeakCalculationItemEntity
  : ActivePowerTotalImportT1PeakCalculationItemEntity
{
}

public static class ActivePowerTotalImportT1PeakCalculationItemEntityExtensions
{
  public static void UsageActivePowerTotalImportT1PeakCalculationItem(
    this ComplexPropertyBuilder builder
  )
  {
    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity.Peak_kW))
      .HasColumnName("svt_peak_kw");

    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity
          .Amount_kW))
      .HasColumnName("svt_amount_kw");

    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity
          .Price_EUR))
      .HasColumnName("svt_price_eur");

    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity
          .Total_EUR))
      .HasColumnName("svt_total_eur");
  }
}
