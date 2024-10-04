using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Agnostic;

public class AgnosticMeterNamingConvention(IServiceProvider serviceProvider)
{
  public Type MeasurementTypeForMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var meter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.IdPrefix));

    if (meter is not null)
    {
      return meter.MeasurementType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }

  public Type AggregateTypeForMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var meter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.IdPrefix));

    if (meter is not null)
    {
      return meter.AggregateType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }

  public Type ValidatorTypeForMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var meter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.IdPrefix));

    if (meter is not null)
    {
      return meter.MeasurementValidatorType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }

  public Type MeterTypeForMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var meter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.IdPrefix));

    if (meter is not null)
    {
      return meter.MeterType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }
}
