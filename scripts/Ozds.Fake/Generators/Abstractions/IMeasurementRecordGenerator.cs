using Ozds.Fake.Identification;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Generation.Abstractions;

public interface IMeasurementRecordGenerator
{
  bool CanGenerateFor(string meterId);

  IAsyncEnumerable<IMeasurementRecord> GenerateMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    MeasurementLocationMeterId id,
    CancellationToken cancellationToken
  );

  IAsyncEnumerable<IMeasurementRecord> BatchMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    IEnumerable<MeasurementLocationMeterId> ids,
    CancellationToken cancellationToken
  );
}
