using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

public abstract class FeeCalculationItemModel : CalculationItemModel
{
  [Required] public required decimal Amount_N { get; set; }
  [Required] public required decimal Price_EUR { get; set; }
  [Required] public required decimal Total_EUR { get; set; }

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
}

public class UsageMeterFeeCalculationItemModel : FeeCalculationItemModel
{
  public override string Kind => "MU";

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

public class SupplyBusinessUsageFeeCalculationItemModel : FeeCalculationItemModel
{
  public override string Kind => "TRP";

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new SupplyExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR)
          )
        )
      );
    }
  }
}

public class SupplyRenewableEnergyFeeCalculationItemModel : FeeCalculationItemModel
{
  public override string Kind => "OIE";

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new SupplyExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new AnyDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR)
          )
        )
      );
    }
  }
}
