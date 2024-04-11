using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Abstractions;

public interface ICalculationItemCalculator
{
  bool CanCalculate(Type calculationType);

  ICalculationItem Calculate(CalculationItemBasisModel calculationBasis);
}
