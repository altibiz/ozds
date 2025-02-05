namespace Ozds.Document.Entities;

public abstract class
  ReactiveEnergyTotalRampedT0CalculationItemEntity : CalculationItemEntity
{
  public decimal ReactiveImportMin_kVARh { get; set; }

  public decimal ReactiveImportMax_kVARh { get; set; }

  public decimal ReactiveImportAmount_kVARh { get; set; }

  public decimal ReactiveExportMin_kVARh { get; set; }

  public decimal ReactiveExportMax_kVARh { get; set; }

  public decimal ReactiveExportAmount_kVARh { get; set; }

  public decimal ActiveImportMin_kWh { get; set; }

  public decimal ActiveImportMax_kWh { get; set; }

  public decimal ActiveImportAmount_kWh { get; set; }

  public decimal Amount_kVARh { get; set; }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public class UsageReactiveEnergyTotalRampedT0CalculationItemEntity
  : ReactiveEnergyTotalRampedT0CalculationItemEntity
{
}
