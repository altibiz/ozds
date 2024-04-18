using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Complex;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  BlueLowNetworkUserCalculationEntity : NetworkUserCalculationEntity<
  BlueLowNetworkUserCatalogueEntity>
{
#pragma warning disable CA1707
  public UsageActiveEnergyTotalImportT0CalculationItemEntity
    UsageActiveEnergyTotalImportT0 { get; set; } = default!;

  public UsageReactiveEnergyTotalRampedT0CalculationItemEntity
    UsageReactiveEnergyTotalRampedT0 { get; set; } = default!;
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
      .ComplexProperty(nameof(BlueLowNetworkUserCalculationEntity
        .UsageActiveEnergyTotalImportT0))
      .UsageActiveEnergyTotalImportT0CalculationItem();

    builder
      .ComplexProperty(nameof(BlueLowNetworkUserCalculationEntity
        .UsageReactiveEnergyTotalRampedT0))
      .UsageReactiveEnergyTotalRampedT0CalculationItem();
  }
}
