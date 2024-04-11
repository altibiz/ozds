using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  WhiteMediumNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  WhiteMediumNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public ActiveEnergyTotalImportT1CalculationItemEntity
    ActiveEnergyTotalImportT1 { get; set; } = default!;

  public ActiveEnergyTotalImportT2CalculationItemEntity
    ActiveEnergyTotalImportT2 { get; set; } = default!;

  public ActivePowerTotalImportT1PeakCalculationItemEntity
    ActivePowerTotalImportT1Peak { get; set; } = default!;

  public ReactiveEnergyTotalRampedT0CalculationItemEntity
    ReactiveEnergyTotalRampedT0 { get; set; } = default!;

  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  WhiteMediumNetworkUserCalculationEntityTypeConfiguration :
  EntityTypeConfiguration<
    WhiteMediumNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteMediumNetworkUserCalculationEntity> builder)
  {
    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT1))
      .ActiveEnergyImportCalculationItem();

    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity
        .ActiveEnergyTotalImportT2))
      .ActiveEnergyImportCalculationItem();

    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity
        .ActivePowerTotalImportT1Peak))
      .ActivePowerTotalImportT1PeakCalculationItem();

    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity
        .ReactiveEnergyTotalRampedT0))
      .ReactiveEnergyTotalRampedT0CalculationItem();

    builder
      .Property(
        nameof(WhiteMediumNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
