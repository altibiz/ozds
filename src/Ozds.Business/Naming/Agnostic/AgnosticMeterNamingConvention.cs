using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Agnostic;

public class AgnosticMeterNamingConvention(IServiceProvider serviceProvider)
{
  public Type MeasurementTypeForLineAndMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var abbMeter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.AbbIdPrefix));

    if (abbMeter is not null)
    {
      return abbMeter.AbbB2xMeasurementType;
    }

    var schneiderMeter = meterNamingConvention.FirstOrDefault(x =>
        meterId.StartsWith(x.SchneiderIdPrefix));

    if (schneiderMeter is not null)
    {
      return schneiderMeter.SchneideriEM3xxxMeasurementType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }

  public Type AggregateTypeForLineAndMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var abbMeter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.AbbIdPrefix));

    if (abbMeter is not null)
    {
      return abbMeter.AbbB2xAggregateType;
    }

    var schneiderMeter = meterNamingConvention.FirstOrDefault(x =>
        meterId.StartsWith(x.SchneiderIdPrefix));

    if (schneiderMeter is not null)
    {
      return schneiderMeter.SchneideriEM3xxxAggregateType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }

  public Type ValidatorTypeForLineAndMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var abbMeter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.AbbIdPrefix));

    if (abbMeter is not null)
    {
      return abbMeter.AbbB2xMeasurementValidatorType;
    }

    var schneiderMeter = meterNamingConvention.FirstOrDefault(x =>
        meterId.StartsWith(x.SchneiderIdPrefix));

    if (schneiderMeter is not null)
    {
      return schneiderMeter.SchneideriEM3xxxMeasurementValidatorType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }

  public Type MeterTypeForLineAndMeterId(string meterId)
  {
    var meterNamingConvention = serviceProvider
      .GetServices<IMeterNamingConvention>();

    var abbMeter = meterNamingConvention.FirstOrDefault(
      x =>
        meterId.StartsWith(x.AbbIdPrefix));

    if (abbMeter is not null)
    {
      return abbMeter.AbbB2xMeterType;
    }

    var schneiderMeter = meterNamingConvention.FirstOrDefault(x =>
        meterId.StartsWith(x.SchneiderIdPrefix));

    if (schneiderMeter is not null)
    {
      return schneiderMeter.SchneideriEM3xxxMeterType;
    }

    throw new InvalidOperationException(
      $"No MeterNamingConvention found for {meterId}");
  }
}
