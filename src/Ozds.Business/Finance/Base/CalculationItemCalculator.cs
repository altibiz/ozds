using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Base;

public abstract class CalculationItemCalculator<TCalculationItem> : ICalculationItemCalculator
  where TCalculationItem : ICalculationItem
{
  public bool CanCalculate(Type calculationType)
  {
    return typeof(TCalculationItem) == calculationType;
  }

  public ICalculationItem Calculate(CalculationItemBasisModel calculationBasis)
  {
    return CalculateConcrete(calculationBasis);
  }

  protected abstract TCalculationItem CalculateConcrete(CalculationItemBasisModel calculationBasis);
}
