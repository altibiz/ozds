using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class UsageMeterFeeCalculationItemCalculator :
  CalculationItemCalculator<UsageMeterFeeCalculationItemModel>
{
  protected override UsageMeterFeeCalculationItemModel CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    return new()
    {
      Amount_N = 1,
      Price_EUR = calculationBasis.Price,
      Total_EUR = calculationBasis.Price
    };
  }
}
