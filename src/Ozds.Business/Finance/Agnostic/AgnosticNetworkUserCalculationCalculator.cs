using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Agnostic;

public class AgnosticNetworkUserCalculationCalculator
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticNetworkUserCalculationCalculator(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public INetworkUserCalculation Calculate(NetworkUserCalculationBasisModel basis)
  {
    return _serviceProvider
        .GetServices<INetworkUserCalculationCalculator>()
        .FirstOrDefault(calculator => calculator.CanCalculate(basis))
        ?.Calculate(basis)
            ?? throw new InvalidOperationException($"No calculator found for calculation {basis.GetType()}.");
  }

  public TCalculation Calculate<TCalculation>(NetworkUserCalculationBasisModel basis)
      where TCalculation : class, INetworkUserCalculation
  {
    return Calculate(basis) as TCalculation
        ?? throw new InvalidOperationException($"No calculator found for calculation {basis.GetType()}.");
  }
}
