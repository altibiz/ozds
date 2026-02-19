using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using static Ozds.Business.Extensions.PrimitiveRoundingExtensions;

namespace Ozds.Business.Finance.Complex;

public class UsageMeterFeeCalculationItemCalculator
  : CalculationItemCalculator<UsageMeterFeeCalculationItemModel>
{
  protected override UsageMeterFeeCalculationItemModel CalculateConcrete(
    CalculationItemBasisModel calculationBasis
  )
  {
    var amount = Round(1M, 0);

    var price = Round(calculationBasis.Price_EUR, 3);

    var total = Round(amount * price, 2);

    return new UsageMeterFeeCalculationItemModel
    {
      Amount_N = amount,
      Price_EUR = price,
      Total_EUR = total,
    };
  }
}
