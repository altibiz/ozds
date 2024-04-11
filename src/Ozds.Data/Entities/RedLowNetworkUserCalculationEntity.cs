using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  RedLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  RedLowNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public ActiveEnergyTotalImportT1CalculationItemEntity ActiveEnergyTotalImportT1 { get; set; } = default!;
  public ActiveEnergyTotalImportT2CalculationItemEntity ActiveEnergyTotalImportT2 { get; set; } = default!;
  public ActivePowerTotalImportT1PeakCalculationItemEntity ActivePowerTotalImportT1Peak { get; set; } = default!;
  public ReactiveEnergyTotalRampedT0CalculationItemEntity ReactiveEnergyTotalRampedT0 { get; set; } = default!;
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  RedLowNetworkUserCalculationEntityTypeConfiguration : EntityTypeConfiguration<
  RedLowNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<RedLowNetworkUserCalculationEntity> builder)
  {
    builder
      .ComplexProperty(nameof(RedLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT1))
      .ActiveEnergyImportCalculationItem();

    builder
      .ComplexProperty(nameof(RedLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT2))
      .ActiveEnergyImportCalculationItem();

    builder
      .ComplexProperty(nameof(RedLowNetworkUserCalculationEntity.ActivePowerTotalImportT1Peak))
      .ActivePowerTotalImportT1PeakCalculationItem();

    builder
      .ComplexProperty(nameof(RedLowNetworkUserCalculationEntity.ReactiveEnergyTotalRampedT0))
      .ReactiveEnergyTotalRampedT0CalculationItem();

    builder
      .Property(nameof(RedLowNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
