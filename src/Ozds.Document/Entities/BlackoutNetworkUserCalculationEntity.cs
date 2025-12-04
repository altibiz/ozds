namespace Ozds.Document.Entities;

public class
  BlackoutNetworkUserCalculationEntity : NetworkUserCalculationEntity
{
  public NetworkUserCatalogueEntity
    ConcreteUsageNetworkUserCatalogue { get; set; } = default!;

  public override NetworkUserCatalogueEntity UsageNetworkUserCatalogue
  {
    get { return ConcreteUsageNetworkUserCatalogue; }
  }
}
