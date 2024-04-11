using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion.Agnostic;

public class AgnosticMeasurementAggregateConverter
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticMeasurementAggregateConverter(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public IAggregate ToAggregate(IMeasurement model, IntervalModel interval)
  {
    return _serviceProvider
      .GetServices<IMeasurementAggregateConverter>()
      .FirstOrDefault(converter => converter.CanConvertToAggregate(model.GetType()))
      ?.ToAggregate(model, interval)
        ?? throw new InvalidOperationException($"No converter found for measurement {model.GetType()}.");
  }

  public TAggregate ToAggregate<TAggregate>(IMeasurement model, IntervalModel interval)
    where TAggregate : class, IAggregate
  {
    return ToAggregate(model, interval) as TAggregate
      ?? throw new InvalidOperationException($"No converter found for measurement {model.GetType()}.");
  }
}
