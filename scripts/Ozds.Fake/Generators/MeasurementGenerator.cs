using Ozds.Fake.Extensions;
using Ozds.Fake.Generators.Abstractions;
using Ozds.Fake.Identification;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Generators;

public class MeasurementRecordGenerator(
  IServiceProvider serviceProvider
)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public IAsyncEnumerable<IMeasurementRecord> GenerateMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    MeasurementLocationMeterId id,
    CancellationToken cancellationToken
  )
  {
    var generator = GetGenerator(id.MeterId);
    var measurements = generator.GenerateMeasurementRecords(
      dateFrom,
      dateTo,
      id,
      cancellationToken);
    return measurements;
  }

  public IAsyncEnumerable<IMeasurementRecord> BatchGenerateMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    IEnumerable<MeasurementLocationMeterId> ids,
    CancellationToken cancellationToken
  )
  {
    return ids
      .GroupBy(id => GetGenerator(id.MeterId))
      .Select(
        group => group.Key.BatchMeasurementRecords(
          dateFrom,
          dateTo,
          group,
          cancellationToken
        ))
      .Concat(cancellationToken);
  }

  private IMeasurementRecordGenerator GetGenerator(
    string meterId
  )
  {
    var generators =
      _serviceProvider.GetServices<IMeasurementRecordGenerator>();
    var generator =
      generators.FirstOrDefault(g => g.CanGenerateFor(meterId));
    return generator
      ?? throw new InvalidOperationException(
        $"No generator found for meter {meterId}");
  }
}
