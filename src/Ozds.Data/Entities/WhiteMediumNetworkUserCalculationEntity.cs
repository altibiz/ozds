using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  WhiteMediumNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  WhiteMediumNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public ActiveEnergyTotalImportCalculationItemEntity ActiveEnergyImportT1 { get; set; } = default!;
  public ActiveEnergyTotalImportCalculationItemEntity ActiveEnergyImportT2 { get; set; } = default!;
  public ActivePowerTotalImportT1PeakCalculationItemEntity ActivePowerTotalImportT1Peak { get; set; } = default!;
  public ReactiveEnergyTotalRampedT0CalculationItemEntity ReactiveEnergyTotalRampedT0 { get; set; } = default!;
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  WhiteMediumNetworkUserCalculationEntityTypeConfiguration : EntityTypeConfiguration<
  WhiteMediumNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteMediumNetworkUserCalculationEntity> builder)
  {
    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity.ActiveEnergyImportT1))
      .ActiveEnergyImportCalculationItem(TariffEntity.T1);

    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity.ActiveEnergyImportT2))
      .ActiveEnergyImportCalculationItem(TariffEntity.T2);

    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity.ActivePowerTotalImportT1Peak))
      .ActivePowerTotalImportT1PeakCalculationItem();

    builder
      .ComplexProperty(nameof(WhiteMediumNetworkUserCalculationEntity.ReactiveEnergyTotalRampedT0))
      .ReactiveEnergyTotalRampedT0CalculationItem();

    builder
      .Property(nameof(WhiteMediumNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
