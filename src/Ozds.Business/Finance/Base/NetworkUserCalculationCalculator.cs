using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Base;

public abstract class NetworkUserCalculationCalculator<T> :
  INetworkUserCalculationCalculator
  where T : NetworkUserCatalogueModel
{
  public bool CanCalculate(
    NetworkUserCalculationBasisModel calculationBasis)
  {
    return calculationBasis.UsageNetworkUserCatalogue.GetType().IsAssignableTo(typeof(T));
  }

  public NetworkUserCalculationModel Calculate(
    NetworkUserCalculationBasisModel calculationBasis)
  {
    var usageCalculation = CalculateForNetworkUser(
      (T)calculationBasis.UsageNetworkUserCatalogue,
      calculationBasis);

    return usageCalculation;
  }

  protected abstract NetworkUserCalculationModel CalculateForNetworkUser(
    T catalogue,
    NetworkUserCalculationBasisModel calculationBasis);
}
