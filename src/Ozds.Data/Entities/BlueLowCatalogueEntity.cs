using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class BlueLowCatalogueEntity : CatalogueEntity
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  BlueLowCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  BlueLowCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<BlueLowCatalogueEntity> builder)
  {
    builder
      .Property(
        nameof(BlueLowCatalogueEntity.ActiveEnergyTotalImportT0Price_EUR))
      .HasColumnName("active_energy_total_import_t0_price_eur");

    builder
      .Property(nameof(BlueLowCatalogueEntity
        .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");

    builder
      .Property(nameof(BlueLowCatalogueEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
