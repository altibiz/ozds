using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Abstractions;

public interface ICalculationCalculator
{
  bool CanCalculateForNetworkUser(
    NetworkUserCalculationBasisModel calculationBasis);

  bool CanCalculateForLocation(LocationCalculationBasisModel calculationBasis);

  CalculationModel CalculateForNetworkUser(
    NetworkUserCalculationBasisModel calculationBasis
  );

  CalculationModel CalculateForLocation(
    LocationCalculationBasisModel calculationBasis
  );
}
