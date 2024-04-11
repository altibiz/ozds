using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Agnostic;

public class AgnosticCalculationItemCalculator
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticCalculationItemCalculator(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public ICalculationItem Calculate(CalculationItemBasisModel basis, Type type)
  {
    return _serviceProvider
        .GetServices<ICalculationItemCalculator>()
        .FirstOrDefault(calculator => calculator.CanCalculate(type))
        ?.Calculate(basis)
            ?? throw new InvalidOperationException(
              $"No calculator found for calculation {type}.");
  }

  public TCalculation Calculate<TCalculation>(CalculationItemBasisModel basis)
      where TCalculation : class, ICalculationItem
  {
    return Calculate(basis, typeof(TCalculation)) as TCalculation
        ?? throw new InvalidOperationException(
          $"No calculator found for calculation {typeof(TCalculation)}.");
  }
}
