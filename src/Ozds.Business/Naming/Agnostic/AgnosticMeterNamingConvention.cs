using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Agnostic;

public class AgnosticMeterNamingConvention(IServiceProvider serviceProvider)
{
  public Type MeasurementTypeForLineAndMeterId(string meterId)
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

  public Type AggregateTypeForLineAndMeterId(string meterId)
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

  public Type ValidatorTypeForLineAndMeterId(string meterId)
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

  public Type MeterTypeForLineAndMeterId(string meterId)
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
