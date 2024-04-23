using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Abstractions;
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
    return calculationBasis.UsageNetworkUserCatalogue.GetType()
      .IsAssignableTo(typeof(T));
  }

  public INetworkUserCalculation Calculate(
    NetworkUserCalculationBasisModel calculationBasis)
  {
    var usageCalculation = CalculateForNetworkUser(
      (T)calculationBasis.UsageNetworkUserCatalogue,
      calculationBasis);

    return usageCalculation;
  }

  protected abstract INetworkUserCalculation CalculateForNetworkUser(
    T catalogue,
    NetworkUserCalculationBasisModel calculationBasis);
}
