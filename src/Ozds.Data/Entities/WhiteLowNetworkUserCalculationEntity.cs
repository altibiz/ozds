using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  WhiteLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  WhiteLowNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public UsageActiveEnergyTotalImportT1CalculationItemEntity
    UsageActiveEnergyTotalImportT1
  { get; set; } = default!;

  public UsageActiveEnergyTotalImportT2CalculationItemEntity
    UsageActiveEnergyTotalImportT2
  { get; set; } = default!;

  public UsageReactiveEnergyTotalRampedT0CalculationItemEntity
    UsageReactiveEnergyTotalRampedT0
  { get; set; } = default!;
#pragma warning restore CA1707
}

public class
  WhiteLowNetworkUserCalculationEntityTypeConfiguration :
  EntityTypeConfiguration<
    WhiteLowNetworkUserCalculationEntity>
{
  public override void Configure(
    EntityTypeBuilder<WhiteLowNetworkUserCalculationEntity> builder)
  {
    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity
        .UsageActiveEnergyTotalImportT1))
      .UsageActiveEnergyTotalImportT1CalculationItem();

    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity
        .UsageActiveEnergyTotalImportT2))
      .UsageActiveEnergyTotalImportT2CalculationItem();

    builder
      .ComplexProperty(nameof(WhiteLowNetworkUserCalculationEntity
        .UsageReactiveEnergyTotalRampedT0))
      .UsageReactiveEnergyTotalRampedT0CalculationItem();
  }
}
