using Ozds.Business.Aggregation;
using Ozds.Business.Conversion;
using Ozds.Business.Queries;
using Ozds.Fake.Client;
using Ozds.Fake.Cloning;
using Ozds.Fake.Conversion;
using Ozds.Fake.Generation;
using Ozds.Fake.Identification;
using Ozds.Fake.Workers.Abstractions;

namespace Ozds.Fake.Workers;

public record InsertWorkerItem(
  DateTimeOffset DateFrom,
  DateTimeOffset DateTo,
  List<MeasurementLocationMeterId> Ids,
  int BatchSize,
  bool AggregatesOnly
);

public class InsertWorker(
  MeasurementRecordGenerator generator,
  MeasurementRecordConverter converter,
  MeasurementAggregateConverter aggregateConverter,
  AggregateUpserter aggregateUpserter,
  MeasurementCloner cloner,
  InsertClient client,
  EnumerableQueries enumerable
) : IEnumeratedBackgroundServiceWorker<InsertWorkerItem>
{
  public async Task ExecuteAsync(
    InsertWorkerItem item,
    CancellationToken stoppingToken
  )
  {
    var generatedIds = item.Ids
      .GroupBy(x => x.MeterModel)
      .Select(x => x.First())
      .ToList();

    var clonedIds = item.Ids
      .Where(id => !generatedIds.Contains(id));

    var records = generator.BatchGenerateMeasurementRecords(
      item.DateFrom,
      item.DateTo,
      generatedIds,
      stoppingToken);

    var measurements = converter.ConvertToModels(
      records,
      stoppingToken
    );

    var aggregated = item.AggregatesOnly
      ? aggregateUpserter.UpsertAggregates(
        aggregateConverter.ToAggregates(measurements, stoppingToken),
        stoppingToken)
      : aggregateUpserter.UpsertMeasurements(
        aggregateConverter.WithAggregates(measurements, stoppingToken),
        stoppingToken);

    var cloned = cloner.CloneWith(
      aggregated,
      clonedIds,
      stoppingToken
    );

    await foreach (var batch in enumerable
      .Batch(cloned, item.BatchSize, stoppingToken))
    {
      await client.Insert(batch, stoppingToken);
    }
  }
}
