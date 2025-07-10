using System.Runtime.CompilerServices;
using Ozds.Business.Aggregation;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Queries;
using Ozds.Fake.Client;
using Ozds.Fake.Cloning;
using Ozds.Fake.Conversion;
using Ozds.Fake.Extensions;
using Ozds.Fake.Generation;
using Ozds.Fake.Identification;
using Ozds.Fake.Packing;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public class TestMeasurementFixture(
  ServiceComposition composition
)
{
  private const int BatchSize = 1000;

  public async IAsyncEnumerable<IMeasurement> Insert(
    IEnumerable<MeasurementLocationMeterId> ids,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    [EnumeratorCancellation] CancellationToken cancellationToken,
    bool aggregatesOnly = true
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var generator = scope.ServiceProvider
      .GetRequiredService<MeasurementRecordGenerator>();
    var converter = scope.ServiceProvider
      .GetRequiredService<MeasurementRecordConverter>();
    var aggregateUpserter = scope.ServiceProvider
      .GetRequiredService<AggregateUpserter>();
    var aggregateConverter = scope.ServiceProvider
      .GetRequiredService<MeasurementAggregateConverter>();
    var cloner = scope.ServiceProvider
      .GetRequiredService<MeasurementCloner>();
    var client = scope.ServiceProvider
      .GetRequiredService<InsertClient>();

    var dateRange = new DateTimeOffsetRange(dateFrom, dateTo);
    var splitInterval = (dateTo - dateFrom) / Environment.ProcessorCount;
    foreach (var date in dateRange.Split(splitInterval))
    {
      var generatedIds = ids
        .GroupBy(x => x.MeterModel)
        .Select(x => x.First())
        .ToList();

      var clonedIds = ids
        .Where(id => !generatedIds.Contains(id));

      var records = generator.BatchGenerateMeasurementRecords(
        date.DateFrom,
        date.DateTo,
        generatedIds,
        cancellationToken);

      var measurements = converter.ConvertToModels(
        records,
        cancellationToken
      );

      var aggregated = aggregatesOnly
        ? aggregateUpserter.UpsertAggregates(
          aggregateConverter.ToAggregates(measurements, cancellationToken),
          cancellationToken)
        : aggregateUpserter.UpsertMeasurements(
          aggregateConverter.WithAggregates(measurements, cancellationToken),
          cancellationToken);

      var cloned = cloner.CloneWith(
        aggregated,
        clonedIds,
        cancellationToken
      );

      await foreach (var batch in cloned
        .Batch(BatchSize, cancellationToken))
      {
        var inserted = await client.Insert(batch, cancellationToken);

        foreach (var measurement in inserted)
        {
          yield return measurement;
        }
      }
    }
  }

  public async IAsyncEnumerable<IMeasurement> Push(
    string messengerId,
    IEnumerable<MeasurementLocationMeterId> ids,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var generator = scope.ServiceProvider
      .GetRequiredService<MeasurementRecordGenerator>();
    var recordConverter = scope.ServiceProvider
      .GetRequiredService<MeasurementRecordConverter>();
    var pushRequestConverter = scope.ServiceProvider
      .GetRequiredService<PushRequestMeasurementConverter>();
    var client = scope.ServiceProvider
      .GetRequiredService<PushClient>();
    var packer = scope.ServiceProvider
      .GetRequiredService<MessengerPushRequestPacker>();
    var clock = scope.ServiceProvider
      .GetRequiredService<ClockQueries>();

    var dateRange = new DateTimeOffsetRange(dateFrom, dateTo);
    var splitInterval = (dateTo - dateFrom) / Environment.ProcessorCount;
    foreach (var date in dateRange.Split(splitInterval))
    {
      var generatedIds = ids
        .GroupBy(x => x.MeterModel)
        .Select(x => x.First())
        .ToList();

      var records = generator.BatchGenerateMeasurementRecords(
        date.DateFrom,
        date.DateTo,
        generatedIds,
        cancellationToken);

      var measurements = recordConverter.ConvertToModels(
        records,
        cancellationToken
      );

      var requests = pushRequestConverter.ToPushRequests(
        measurements,
        cancellationToken
      );

      await foreach (var (batchPushRequests, batchMeasurements) in requests
        .Batch(BatchSize, cancellationToken)
        .Zip(measurements.Batch(BatchSize, cancellationToken)))
      {
        var request = await packer.Pack(
          messengerId,
          clock.Timestamp(),
          batchPushRequests,
          cancellationToken
        );

        await client.Push(
          messengerId,
          PushClientBufferBehavior.Realtime,
          request,
          cancellationToken
        );

        await foreach (var measurement in batchMeasurements)
        {
          yield return measurement;
        }
      }
    }
  }
}
