using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class WhiteLowNetworkUserCatalogueEntity
  : NetworkUserCatalogueEntity<WhiteLowNetworkUserCalculationEntity>
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  WhiteLowNetworkUserCatalogueEntityTypeConfiguration : EntityTypeConfiguration<
  WhiteLowNetworkUserCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteLowNetworkUserCatalogueEntity> builder)
  {
    builder
      .MonetaryValue(
        nameof(WhiteLowNetworkUserCatalogueEntity
          .ActiveEnergyTotalImportT1Price_EUR),
        "active_energy_total_import_t1_price_eur"
      );

    builder
      .MonetaryValue(
        nameof(WhiteLowNetworkUserCatalogueEntity
          .ActiveEnergyTotalImportT2Price_EUR),
        "active_energy_total_import_t2_price_eur"
      );

    builder
      .MonetaryValue(
        nameof(WhiteLowNetworkUserCatalogueEntity
          .ReactiveEnergyTotalRampedT0Price_EUR),
        "reactive_energy_total_ramped_t0_price_eur"
      );
  }
}
