namespace Ozds.Document.Entities;

public abstract class FeeCalculationItemEntity : CalculationItemEntity
{
  public decimal Amount_N { get; set; }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public class UsageMeterFeeCalculationItemEntity : FeeCalculationItemEntity
{
}
