namespace Ozds.Document.Entities;

public abstract class
  BlackoutNetworkUserCalculationEntity : NetworkUserCalculationEntity
{
  public NetworkUserCatalogueEntity
    ConcreteUsageNetworkUserCatalogue
  { get; set; } = default!;

  public override NetworkUserCatalogueEntity UsageNetworkUserCatalogue
  {
    get { return ConcreteUsageNetworkUserCatalogue; }
  }
}
