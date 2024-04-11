using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  BlueLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  BlueLowNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public ActiveEnergyTotalImportCalculationItemEntity ActiveEnergyImportT0 { get; set; } = default!;
  public ReactiveEnergyTotalRampedT0CalculationItemEntity ReactiveEnergyTotalRampedT0 { get; set; } = default!;
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  BlueLowNetworkUserCalculationEntityTypeConfiguration : EntityTypeConfiguration
<
  BlueLowNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<BlueLowNetworkUserCalculationEntity> builder)
  {
    builder
      .ComplexProperty(nameof(BlueLowNetworkUserCalculationEntity.ActiveEnergyImportT0))
      .ActiveEnergyImportCalculationItem(TariffEntity.T0);

    builder
      .ComplexProperty(nameof(BlueLowNetworkUserCalculationEntity.ReactiveEnergyTotalRampedT0))
      .ReactiveEnergyTotalRampedT0CalculationItem();

    builder
      .Property(nameof(BlueLowNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
