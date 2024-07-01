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
  public UsageActiveEnergyTotalImportT1CalculationItemEntity
    UsageActiveEnergyTotalImportT1 { get; set; } = default!;

  public UsageActiveEnergyTotalImportT2CalculationItemEntity
    UsageActiveEnergyTotalImportT2 { get; set; } = default!;

  public UsageActivePowerTotalImportT1PeakCalculationItemEntity
    UsageActivePowerTotalImportT1Peak { get; set; } = default!;

  public UsageReactiveEnergyTotalRampedT0CalculationItemEntity
    UsageReactiveEnergyTotalRampedT0 { get; set; } = default!;
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
      .ComplexProperty(
        nameof(WhiteMediumNetworkUserCalculationEntity
          .UsageActiveEnergyTotalImportT1))
      .UsageActiveEnergyTotalImportT1CalculationItem();

    builder
      .ComplexProperty(
        nameof(WhiteMediumNetworkUserCalculationEntity
          .UsageActiveEnergyTotalImportT2))
      .UsageActiveEnergyTotalImportT2CalculationItem();

    builder
      .ComplexProperty(
        nameof(WhiteMediumNetworkUserCalculationEntity
          .UsageActivePowerTotalImportT1Peak))
      .UsageActivePowerTotalImportT1PeakCalculationItem();

    builder
      .ComplexProperty(
        nameof(WhiteMediumNetworkUserCalculationEntity
          .UsageReactiveEnergyTotalRampedT0))
      .UsageReactiveEnergyTotalRampedT0CalculationItem();
  }
}
