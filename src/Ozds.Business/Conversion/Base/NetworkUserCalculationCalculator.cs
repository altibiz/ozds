using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Base;

public abstract class NetworkUserCalculationCalculator<T> :
  INetworkUserCalculationCalculator
  where T : NetworkUserCatalogueModel
{
  public bool CanCalculateForNetworkUser(
    NetworkUserCalculationBasisModel calculationBasis)
  {
    return calculationBasis.UsageNetworkUserCatalogue.GetType() == typeof(T);
  }

  public NetworkUserCalculationModel CalculateForNetworkUser(
    NetworkUserCalculationBasisModel calculationBasis)
  {
    var usageCalculation = CalculateForNetworkUser((T)calculationBasis.UsageNetworkUserCatalogue,
      calculationBasis);

    return usageCalculation;
  }

  protected abstract NetworkUserCalculationModel CalculateForNetworkUser(
    T catalogue,
    NetworkUserCalculationBasisModel calculationBasis);
}
