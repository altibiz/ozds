namespace Ozds.Document.Entities;

public class WhiteLowNetworkUserCalculationEntity
  : NetworkUserCalculationEntity<WhiteLowNetworkUserCatalogueEntity>
{
  public UsageActiveEnergyTotalImportT1CalculationItemEntity
    UsageActiveEnergyTotalImportT1 { get; set; } = default!;

  public UsageActiveEnergyTotalImportT2CalculationItemEntity
    UsageActiveEnergyTotalImportT2 { get; set; } = default!;

  public UsageReactiveEnergyTotalRampedT0CalculationItemEntity
    UsageReactiveEnergyTotalRampedT0 { get; set; } = default!;

  protected override IEnumerable<CalculationItemEntity> AdditionalUsageItems
  {
    get
    {
      return new CalculationItemEntity[]
      {
        UsageActiveEnergyTotalImportT1,
        UsageActiveEnergyTotalImportT2,
        UsageReactiveEnergyTotalRampedT0
      };
    }
  }
}
