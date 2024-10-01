using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Generators.Abstractions;

public interface IMeasurementGenerator
{
  bool CanGenerateMeasurementsFor(string meterId);

  Task<List<IMeterPushRequestEntity>> GenerateMeasurements(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    string messengerId,
    string meterId,
    CancellationToken cancellationToken = default
  );
}
