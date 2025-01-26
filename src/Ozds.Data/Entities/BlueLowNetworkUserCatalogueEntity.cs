using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class BlueLowNetworkUserCatalogueEntity
  : NetworkUserCatalogueEntity<BlueLowNetworkUserCalculationEntity>
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  BlueLowNetworkUserCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  BlueLowNetworkUserCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<BlueLowNetworkUserCatalogueEntity> builder)
  {
    builder
      .Property(
        nameof(BlueLowNetworkUserCatalogueEntity
          .ActiveEnergyTotalImportT0Price_EUR))
      .HasColumnName("active_energy_total_import_t0_price_eur");

    builder
      .Property(
        nameof(BlueLowNetworkUserCatalogueEntity
          .ReactiveEnergyTotalRampedT0Price_EUR))
      .HasColumnName("reactive_energy_total_ramped_t0_price_eur");
  }
}
