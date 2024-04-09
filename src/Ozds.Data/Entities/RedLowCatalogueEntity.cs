using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RedLowCatalogueEntity : CatalogueEntity
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public decimal ActivePowerTotalImportT1Price_EUR { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  RedLowCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  RedLowCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<RedLowCatalogueEntity> builder)
  {
    builder
      .Property(
        nameof(RedLowCatalogueEntity.ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(
        nameof(RedLowCatalogueEntity.ActiveEnergyTotalImportT2Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(RedLowCatalogueEntity
        .ActivePowerTotalImportT1Price_EUR))
      .HasColumnName("active_power_total_import_t1_price_eur");

    builder
      .Property(nameof(RedLowCatalogueEntity
        .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(RedLowCatalogueEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
