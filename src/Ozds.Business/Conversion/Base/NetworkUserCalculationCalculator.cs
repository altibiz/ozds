using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Base;

public abstract class NetworkUserCalculationCalculator<T> :
  INetworkUserCalculationCalculator
  where T : CatalogueModel
{
  public bool CanCalculateForNetworkUser(
    NetworkUserNetworkUserCalculationBasisModel calculationBasis)
  {
    return calculationBasis.Catalogue.GetType() == typeof(T);
  }

  public bool CanCalculateForLocation(
    LocationNetworkUserCalculationBasisModel calculationBasis)
  {
    throw new NotImplementedException();
  }

  public NetworkUserCalculationModel CalculateForNetworkUser(
    NetworkUserNetworkUserCalculationBasisModel calculationBasis)
  {
    return CalculateForNetworkUser((T)calculationBasis.Catalogue,
      calculationBasis);
  }

  public NetworkUserCalculationModel CalculateForLocation(
    LocationNetworkUserCalculationBasisModel calculationBasis)
  {
    throw new NotImplementedException();
  }

  protected abstract NetworkUserCalculationModel CalculateForNetworkUser(T catalogue,
    NetworkUserNetworkUserCalculationBasisModel calculationBasis);
}
