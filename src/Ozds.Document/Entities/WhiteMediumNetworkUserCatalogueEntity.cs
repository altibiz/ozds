namespace Ozds.Document.Entities;

public class WhiteMediumNetworkUserCatalogueEntity : NetworkUserCatalogueEntity
{
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  public decimal ActivePowerTotalImportT1Price_EUR { get; set; }

  public decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }
}
