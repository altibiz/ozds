using Ozds.Business.Iot;
using Ozds.Fake.Generators.Abstractions;

namespace Ozds.Fake.Generators.Agnostic;

public class AgnosticMeasurementGenerator(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public async Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string meterId
  )
  {
    var logger = _serviceProvider
      .GetRequiredService<ILogger<AgnosticMeasurementGenerator>>();
    var generators = _serviceProvider.GetServices<IMeasurementGenerator>();
    var generator =
      generators.FirstOrDefault(g => g.CanGenerateMeasurementsFor(meterId));
    var measurements =
      await (generator?.GenerateMeasurements(dateFrom, dateTo, meterId)
        ?? throw new InvalidOperationException(
          $"No generator found for meter {meterId}"));
    logger.LogInformation(
      "Generated {Count} measurements for meter {MeterId} from {DateFrom} to {DateTo}",
      measurements.Count,
      meterId,
      dateFrom,
      dateTo
    );
    return measurements;
  }
}
