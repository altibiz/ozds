using Ozds.Business.Queries;
using Ozds.Fake.Generation.Abstractions;
using Ozds.Fake.Identification;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Generation;

public class MeasurementRecordGenerator(IServiceProvider serviceProvider)
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
      cancellationToken
    );
    return measurements;
  }

  public IAsyncEnumerable<IMeasurementRecord> BatchGenerateMeasurementRecords(
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    IEnumerable<MeasurementLocationMeterId> ids,
    CancellationToken cancellationToken
  )
  {
    var enumerable = _serviceProvider.GetRequiredService<EnumerableQueries>();
    return enumerable.Concat(
      ids.GroupBy(id => GetGenerator(id.MeterId))
        .Select(group =>
          group.Key.BatchMeasurementRecords(
            dateFrom,
            dateTo,
            group,
            cancellationToken
          )
        ),
      cancellationToken
    );
  }

  private IMeasurementRecordGenerator GetGenerator(string meterId)
  {
    var generators =
      _serviceProvider.GetServices<IMeasurementRecordGenerator>();
    var generator = generators.FirstOrDefault(g => g.CanGenerateFor(meterId));
    return generator
      ?? throw new InvalidOperationException(
        $"No generator found for meter {meterId}"
      );
  }
}
