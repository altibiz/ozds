using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class WhiteMediumNetworkUserCatalogueEntity
  : NetworkUserCatalogueEntity<WhiteMediumNetworkUserCalculationEntity>
{
#pragma warning disable CA1707
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }
  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }
  public decimal ActivePowerTotalImportT1Price_EUR { get; set; }
  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
#pragma warning restore CA1707
}

public class WhiteMediumNetworkUserCatalogueEntityTypeConfiguration
  : EntityTypeConfiguration<WhiteMediumNetworkUserCatalogueEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteMediumNetworkUserCatalogueEntity> builder
  )
  {
    builder.MonetaryValue(
      nameof(
        WhiteMediumNetworkUserCatalogueEntity.ActiveEnergyTotalImportT1Price_EUR
      ),
      "active_energy_total_import_t1_price_eur"
    );

    builder.MonetaryValue(
      nameof(
        WhiteMediumNetworkUserCatalogueEntity.ActiveEnergyTotalImportT2Price_EUR
      ),
      "active_energy_total_import_t2_price_eur"
    );

    builder.MonetaryValue(
      nameof(
        WhiteMediumNetworkUserCatalogueEntity.ActivePowerTotalImportT1Price_EUR
      ),
      "active_power_total_import_t1_price_eur"
    );

    builder.MonetaryValue(
      nameof(
        WhiteMediumNetworkUserCatalogueEntity.ReactiveEnergyTotalRampedT0Price_EUR
      ),
      "reactive_energy_total_ramped_t0_price_eur"
    );
  }
}
