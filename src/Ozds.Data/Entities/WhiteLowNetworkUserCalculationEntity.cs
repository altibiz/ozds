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
  public ActiveEnergyTotalImportCalculationItemEntity ActiveEnergyImportT1 { get; set; } = default!;
  public ActiveEnergyTotalImportCalculationItemEntity ActiveEnergyImportT2 { get; set; } = default!;
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
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity.ActiveEnergyImportT1))
      .ActiveEnergyImportCalculationItem(TariffEntity.T1);

    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity.ActiveEnergyImportT2))
      .ActiveEnergyImportCalculationItem(TariffEntity.T2);

    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity.ReactiveEnergyTotalRampedT0))
      .ReactiveEnergyTotalRampedT0CalculationItem();

    builder
      .Property(nameof(WhiteLowNetworkUserCalculationEntity.MeterFeePrice_EUR))
      .HasColumnName("meter_fee_price_eur");
  }
}
