using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Abstractions;

public interface INetworkUserCalculationCalculator
{
  bool CanCalculateForNetworkUser(
    NetworkUserCalculationBasisModel calculationBasis);

  NetworkUserCalculationModel CalculateForNetworkUser(
    NetworkUserCalculationBasisModel calculationBasis
  );
}
