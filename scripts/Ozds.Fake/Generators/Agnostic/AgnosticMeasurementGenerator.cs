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

  public Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string meterId
  )
  {
    var generators = _serviceProvider.GetServices<IMeasurementGenerator>();
    var generator =
      generators.FirstOrDefault(g => g.CanGenerateMeasurementsFor(meterId));
    return generator?.GenerateMeasurements(dateFrom, dateTo, meterId)
           ?? throw new InvalidOperationException(
             $"No generator found for meter {meterId}");
  }
}
