using Ozds.Fake.Generators.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Generators.Agnostic;

public class AgnosticMeasurementGenerator(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public async Task<List<IMeterPushRequestEntity>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string messengerId,
    string meterId,
    CancellationToken cancellationToken = default
  )
  {
    var logger = _serviceProvider
      .GetRequiredService<ILogger<AgnosticMeasurementGenerator>>();
    var generators = _serviceProvider.GetServices<IMeasurementGenerator>();
    var generator =
      generators.FirstOrDefault(g => g.CanGenerateMeasurementsFor(meterId));
    var measurements =
      await (generator?.GenerateMeasurements(
          dateFrom, dateTo, messengerId, meterId, cancellationToken)
        ?? throw new InvalidOperationException(
          $"No generator found for meter {meterId}"));
    logger.LogInformation(
      "Generated {Count} measurements for messenger {MessengerId} and meter {MeterId} from {DateFrom} to {DateTo}",
      measurements.Count,
      messengerId,
      meterId,
      dateFrom,
      dateTo
    );
    return measurements;
  }
}
