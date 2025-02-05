namespace Ozds.Document.Entities;

public class BlueLowNetworkUserCatalogueEntity : NetworkUserCatalogueEntity
{
  public decimal ActiveEnergyTotalImportT0Price_EUR { get; set; }

  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
}
