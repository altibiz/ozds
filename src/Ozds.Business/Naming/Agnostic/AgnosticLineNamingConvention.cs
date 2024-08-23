using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Agnostic;

public class AgnosticLineNamingConvention(IServiceProvider serviceProvider)
{
  public Type MeasurementTypeForLineAndMeterId(string lineId, string meterId)
  {
    var lineNamingConvention = serviceProvider
      .GetServices<ILineNamingConvention>()
      .FirstOrDefault(x =>
        lineId.StartsWith(x.LineIdPrefix)
        && meterId.StartsWith(x.MeterIdPrefix))
      ?? throw new InvalidOperationException(
        $"No LineNamingConvention found for {lineId}");

    return lineNamingConvention.MeasurementType;
  }

  public Type AggregateTypeForLineAndMeterId(string lineId, string meterId)
  {
    var lineNamingConvention = serviceProvider
      .GetServices<ILineNamingConvention>()
      .FirstOrDefault(x =>
        lineId.StartsWith(x.LineIdPrefix)
        && meterId.StartsWith(x.MeterIdPrefix))
      ?? throw new InvalidOperationException(
        $"No LineNamingConvention found for {lineId}");

    return lineNamingConvention.AggregateType;
  }
}
