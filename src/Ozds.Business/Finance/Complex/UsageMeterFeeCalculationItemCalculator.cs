using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class UsageMeterFeeCalculationItemCalculator :
  CalculationItemCalculator<UsageMeterFeeCalculationItemModel>
{
  protected override UsageMeterFeeCalculationItemModel CalculateConcrete(
    CalculationItemBasisModel calculationBasis)
  {
    var amount = System.Math.Round(1M, 0);

    var price = System.Math.Round(calculationBasis.Price, 3);

    var total = System.Math.Round(amount * price, 2);

    return new UsageMeterFeeCalculationItemModel
    {
      Amount_N = amount,
      Price_EUR = price,
      Total_EUR = total
    };
  }
}
