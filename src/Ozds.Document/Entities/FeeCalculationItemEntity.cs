
namespace Ozds.Document.Entities;

public abstract class FeeCalculationItemEntity : CalculationItemEntity
{
  public decimal Amount_N { get; set; }

  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new AvgSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Amount_N)
          )
        )
      );
    }
  }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public class UsageMeterFeeCalculationItemEntity : FeeCalculationItemEntity
{
  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR)
          )
        )
      );
    }
  }
}
