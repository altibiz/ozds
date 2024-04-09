using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Conversion.Base;

public abstract class CalculationCalculator<T> :
  ICalculationCalculator
  where T : CatalogueModel
{
  public bool CanCalculateForNetworkUser(NetworkUserCalculationBasisModel calculationBasis)
  {
    return calculationBasis.Catalogue.GetType() == typeof(T);
  }

  public bool CanCalculateForLocation(LocationCalculationBasisModel calculationBasis)
  {
    throw new NotImplementedException();
  }

  public CalculationModel CalculateForNetworkUser(NetworkUserCalculationBasisModel calculationBasis)
  {
    return CalculateForNetworkUser((T)calculationBasis.Catalogue, calculationBasis);
  }

  protected abstract CalculationModel CalculateForNetworkUser(T catalogue, NetworkUserCalculationBasisModel calculationBasis);

  public CalculationModel CalculateForLocation(LocationCalculationBasisModel calculationBasis)
  {
    throw new NotImplementedException();
  }
}
