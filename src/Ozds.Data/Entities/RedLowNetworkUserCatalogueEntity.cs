using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class RedLowNetworkUserCatalogueEntity : NetworkUserCatalogueEntity
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
  RedLowNetworkUserCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  RedLowNetworkUserCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<RedLowNetworkUserCatalogueEntity> builder)
  {
    builder
      .Property(
        nameof(RedLowNetworkUserCatalogueEntity.ActiveEnergyTotalImportT1Price_EUR))
      .HasColumnName("active_energy_total_import_t1_price_eur");

    builder
      .Property(
        nameof(RedLowNetworkUserCatalogueEntity.ActiveEnergyTotalImportT2Price_EUR))
      .HasColumnName("active_energy_total_import_t2_price_eur");

    builder
      .Property(nameof(RedLowNetworkUserCatalogueEntity
        .ActivePowerTotalImportT1Price_EUR))
      .HasColumnName("active_power_total_import_t1_price_eur");

    builder
      .Property(nameof(RedLowNetworkUserCatalogueEntity
        .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(RedLowNetworkUserCatalogueEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
