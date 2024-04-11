using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  WhiteLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  WhiteLowNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public ActiveEnergyTotalImportT1CalculationItemEntity ActiveEnergyTotalImportT1 { get; set; } = default!;
  public ActiveEnergyTotalImportT2CalculationItemEntity ActiveEnergyTotalImportT2 { get; set; } = default!;
  public ReactiveEnergyTotalRampedT0CalculationItemEntity ReactiveEnergyTotalRampedT0 { get; set; } = default!;
  public decimal MeterFeePrice_EUR { get; set; }
#pragma warning restore CA1707
}

public class
  WhiteLowNetworkUserCalculationEntityTypeConfiguration : EntityTypeConfiguration<
  WhiteLowNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteLowNetworkUserCalculationEntity> builder)
  {
    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT1))
      .ActiveEnergyImportCalculationItem();

    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity.ActiveEnergyTotalImportT2))
      .ActiveEnergyImportCalculationItem();

    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity.ReactiveEnergyTotalRampedT0))
      .ReactiveEnergyTotalRampedT0CalculationItem();

    builder
      .Property(nameof(WhiteLowNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
