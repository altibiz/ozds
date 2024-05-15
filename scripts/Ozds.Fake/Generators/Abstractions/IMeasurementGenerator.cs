using Ozds.Business.Iot;

namespace Ozds.Fake.Generators.Abstractions;

public interface IMeasurementGenerator
{
  bool CanGenerateMeasurementsFor(string meterId);

  Task<List<MessengerPushRequestMeasurement>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string meterId,
    CancellationToken cancellationToken = default
  );
}
