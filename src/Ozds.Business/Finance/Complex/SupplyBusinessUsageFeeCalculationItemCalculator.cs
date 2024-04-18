using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class SupplyBusinessUsageFeeCalculationItemCalculator :
  CalculationItemCalculator<SupplyBusinessUsageFeeCalculationItemModel>
{
  protected override SupplyBusinessUsageFeeCalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    return new SupplyBusinessUsageFeeCalculationItemModel
    {
      Amount_N = 1,
      Price_EUR = calculationBasis.Price,
      Total_EUR = calculationBasis.Price
    };
  }
}
