namespace Ozds.Document.Entities;

public class RegulatoryCatalogueEntity
{
  public decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  public decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  public decimal RenewableEnergyFeePrice_EUR { get; set; }

  public decimal BusinessUsageFeePrice_EUR { get; set; }

  public decimal TaxRate_Percent { get; set; }
}
