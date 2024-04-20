using Ozds.Business.Iot;
using Ozds.Fake.Generators.Abstractions;

namespace Ozds.Fake.Generators.Agnostic;

public class AgnosticMeasurementGenerator
{
  private readonly IServiceProvider _serviceProvider;

  public AgnosticMeasurementGenerator(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string meterId
  )
  {
    var logger = _serviceProvider.GetRequiredService<ILogger<AgnosticMeasurementGenerator>>();
    var generators = _serviceProvider.GetServices<IMeasurementGenerator>();
    var generator =
      generators.FirstOrDefault(g => g.CanGenerateMeasurementsFor(meterId));
    var measurements = await (generator?.GenerateMeasurements(dateFrom, dateTo, meterId)
           ?? throw new InvalidOperationException(
             $"No generator found for meter {meterId}"));
    logger.LogInformation(
      "Generated {count} measurements for meter {meterId} from {dateFrom} to {dateTo}",
      measurements.Count,
      meterId,
      dateFrom,
      dateTo
    );
    return measurements;
  }
}
