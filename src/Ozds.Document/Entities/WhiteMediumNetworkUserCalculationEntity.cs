namespace Ozds.Document.Entities;

public class WhiteMediumNetworkUserCalculationEntity
  : NetworkUserCalculationEntity<WhiteMediumNetworkUserCatalogueEntity>
{
  public UsageActiveEnergyTotalImportT1CalculationItemEntity
    UsageActiveEnergyTotalImportT1 { get; set; } = default!;

  public UsageActiveEnergyTotalImportT2CalculationItemEntity
    UsageActiveEnergyTotalImportT2 { get; set; } = default!;

  public UsageActivePowerTotalImportT1PeakCalculationItemEntity
    UsageActivePowerTotalImportT1Peak { get; set; } = default!;

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
        UsageActivePowerTotalImportT1Peak,
        UsageReactiveEnergyTotalRampedT0
      };
    }
  }
}
