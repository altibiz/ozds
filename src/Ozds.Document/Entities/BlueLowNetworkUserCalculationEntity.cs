namespace Ozds.Document.Entities;

public class BlueLowNetworkUserCalculationEntity
  : NetworkUserCalculationEntity<BlueLowNetworkUserCatalogueEntity>
{
  public UsageActiveEnergyTotalImportT0CalculationItemEntity
    UsageActiveEnergyTotalImportT0
  { get; set; } = default!;

  public UsageReactiveEnergyTotalRampedT0CalculationItemEntity
    UsageReactiveEnergyTotalRampedT0
  { get; set; } = default!;

  protected override IEnumerable<CalculationItemEntity> AdditionalUsageItems
  {
    get
    {
      return new CalculationItemEntity[]
      {
        UsageActiveEnergyTotalImportT0,
        UsageReactiveEnergyTotalRampedT0
      };
    }
  }
}
