namespace Ozds.Document.Entities;

public abstract class
  ActivePowerTotalImportT1PeakCalculationItemEntity : CalculationItemEntity
{
  public decimal Peak_kW { get; set; }

  public decimal Amount_kW { get; set; }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public class UsageActivePowerTotalImportT1PeakCalculationItemEntity
  : ActivePowerTotalImportT1PeakCalculationItemEntity
{
}
