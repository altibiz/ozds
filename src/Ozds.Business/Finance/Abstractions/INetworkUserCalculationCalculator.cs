using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Abstractions;

public interface INetworkUserCalculationCalculator
{
  bool CanCalculate(NetworkUserCalculationBasisModel calculationBasis);

  NetworkUserCalculationModel Calculate(
    NetworkUserCalculationBasisModel calculationBasis
  );
}
