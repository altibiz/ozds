using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Abstractions;

public interface INetworkUserCalculationCalculator
{
  bool CanCalculate(NetworkUserCalculationBasisModel calculationBasis);

  INetworkUserCalculation Calculate(
    NetworkUserCalculationBasisModel calculationBasis
  );
}
