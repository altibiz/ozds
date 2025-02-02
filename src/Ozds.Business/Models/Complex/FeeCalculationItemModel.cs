using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class FeeCalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal Amount_N { get; set; }

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

public class UsageMeterFeeCalculationItemModel : FeeCalculationItemModel
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
