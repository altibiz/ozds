using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Abstractions;

public interface INetworkUserCalculationCalculator
{
  bool CanCalculateForNetworkUser(
    NetworkUserNetworkUserCalculationBasisModel calculationBasis);

  bool CanCalculateForLocation(LocationNetworkUserCalculationBasisModel calculationBasis);

  NetworkUserCalculationModel CalculateForNetworkUser(
    NetworkUserNetworkUserCalculationBasisModel calculationBasis
  );

  NetworkUserCalculationModel CalculateForLocation(
    LocationNetworkUserCalculationBasisModel calculationBasis
  );
}
