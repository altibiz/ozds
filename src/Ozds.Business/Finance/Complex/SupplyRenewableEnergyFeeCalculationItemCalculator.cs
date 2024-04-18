using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class SupplyRenewableEnergyFeeCalculationItemCalculator :
  CalculationItemCalculator<SupplyRenewableEnergyFeeCalculationItemModel>
{
  protected override SupplyRenewableEnergyFeeCalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    return new SupplyRenewableEnergyFeeCalculationItemModel
    {
      Amount_N = 1,
      Price_EUR = calculationBasis.Price,
      Total_EUR = calculationBasis.Price
    };
  }
}
