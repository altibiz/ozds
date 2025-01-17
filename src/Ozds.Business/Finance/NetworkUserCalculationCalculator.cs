using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance;

public class NetworkUserCalculationCalculator(
  IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  // NOTE: virtual to allow mocking
  public virtual NetworkUserCalculationModel Calculate(
    NetworkUserCalculationBasisModel basis)
  {
    return _serviceProvider
        .GetServices<INetworkUserCalculationCalculator>()
        .FirstOrDefault(calculator => calculator.CanCalculate(basis))
        ?.Calculate(basis)
      ?? throw new InvalidOperationException(
        $"No calculator found for calculation {basis.GetType()}.");
  }

  public TCalculation Calculate<TCalculation>(
    NetworkUserCalculationBasisModel basis)
    where TCalculation : class, INetworkUserCalculation
  {
    return Calculate(basis) as TCalculation
      ?? throw new InvalidOperationException(
        $"No calculator found for calculation {basis.GetType()}.");
  }
}
