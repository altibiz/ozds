using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Complex;

public abstract class ActivePowerTotalImportT1PeakCalculationItemEntity
{
  public decimal Peak_W { get; set; }
  public decimal Amount_W { get; set; }
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
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity.Peak_W))
      .HasColumnName("usage_active_power_total_import_t1_peak_w");

    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity
          .Amount_W))
      .HasColumnName("usage_active_power_total_import_t1_amount_w");

    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity
          .Price_EUR))
      .HasColumnName("usage_active_power_total_import_t1_price_eur");

    builder
      .Property(
        nameof(ActivePowerTotalImportT1PeakCalculationItemEntity
          .Total_EUR))
      .HasColumnName("usage_active_power_total_import_t1_total_eur");
  }
}
