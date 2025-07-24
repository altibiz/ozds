using System.Runtime.CompilerServices;
using Ozds.Business.Aggregation;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Queries;
using Ozds.Fake.Client;
using Ozds.Fake.Cloning;
using Ozds.Fake.Conversion;
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
    var worker = ActivatorUtilities.CreateInstance<Worker>(
      scope.ServiceProvider
    );
    await foreach (var measurements in worker.Insert(
        ids,
        dateFrom,
        dateTo,
        cancellationToken,
        aggregatesOnly
      ))
    {
      yield return measurements;
    }
  }

  public async IAsyncEnumerable<IMeasurement> Push(
    string messengerId,
    string apiKey,
    IEnumerable<MeasurementLocationMeterId> ids,
    DateTimeOffset dateFrom,
    DateTimeOffset dateTo,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var worker = ActivatorUtilities.CreateInstance<Worker>(
      scope.ServiceProvider
    );
    await foreach (var measurements in worker.Push(
        messengerId,
        apiKey,
        ids,
        dateFrom,
        dateTo,
        cancellationToken
      ))
    {
      yield return measurements;
    }
  }

  public async IAsyncEnumerable<IMeasurement> Push(
    string messengerId,
    string apiKey,
    IEnumerable<MeasurementLocationMeterId> ids,
    TimeSpan interval,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    await using var scope = composition.Ozds.Services.CreateAsyncScope();
    var worker = ActivatorUtilities.CreateInstance<Worker>(
      scope.ServiceProvider
    );
    await foreach (var measurements in worker.Push(
        messengerId,
        apiKey,
        ids,
        interval,
        cancellationToken
      ))
    {
      yield return measurements;
    }
  }

  private sealed class Worker(
    MeasurementRecordGenerator generator,
    ClockQueries clock,
    MeasurementRecordConverter converter,
    PushRequestMeasurementConverter pushRequestConverter,
    MessengerPushRequestPacker packer,
    PushClient pushClient,
    InsertClient insertClient,
    EnumerableQueries enumerable,
    MeasurementCloner cloner,
    MeasurementAggregateConverter aggregateConverter,
    AggregateUpserter aggregateUpserter,
    MeasurementRecordConverter recordConverter
  )
  {
    public async IAsyncEnumerable<IMeasurement> Insert(
      IEnumerable<MeasurementLocationMeterId> ids,
      DateTimeOffset dateFrom,
      DateTimeOffset dateTo,
      [EnumeratorCancellation] CancellationToken cancellationToken,
      bool aggregatesOnly = true
    )
    {
      var generatedIds = ids
        .GroupBy(x => x.MeterModel)
        .Select(x => x.First())
        .ToList();

      var clonedIds = ids
        .Where(id => !generatedIds.Contains(id));

      foreach (var date in enumerable
        .Split(dateFrom, dateTo, Environment.ProcessorCount))
      {
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

        await foreach (var batch in enumerable
          .Batch(cloned, BatchSize, cancellationToken))
        {
          var inserted = await insertClient.Insert(batch, cancellationToken);

          foreach (var measurement in inserted)
          {
            yield return measurement;
          }
        }
      }
    }

    public async IAsyncEnumerable<IMeasurement> Push(
      string messengerId,
      string apiKey,
      IEnumerable<MeasurementLocationMeterId> ids,
      DateTimeOffset dateFrom,
      DateTimeOffset dateTo,
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
      foreach (var range in enumerable
        .Split(dateFrom, dateTo, Environment.ProcessorCount))
      {
        await foreach (var measurements in PushRange(
            messengerId,
            apiKey,
            ids,
            range,
            cancellationToken
          ))
        {
          yield return measurements;
        }
      }
    }

    public async IAsyncEnumerable<IMeasurement> Push(
      string messengerId,
      string apiKey,
      IEnumerable<MeasurementLocationMeterId> ids,
      TimeSpan interval,
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
      await foreach (var range in clock.Future(interval, cancellationToken))
      {
        await foreach (var measurements in PushRange(
            messengerId,
            apiKey,
            ids,
            range,
            cancellationToken
          ))
        {
          yield return measurements;
        }
      }
    }

    private async IAsyncEnumerable<IMeasurement> PushRange(
      string messengerId,
      string apiKey,
      IEnumerable<MeasurementLocationMeterId> ids,
      DateTimeOffsetRangeModel range,
      [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
      var generatedIds = ids
        .GroupBy(x => x.MeterModel)
        .Select(x => x.First())
        .ToList();

      var clonedIds = ids
        .Where(id => !generatedIds.Contains(id));

      var records = generator.BatchGenerateMeasurementRecords(
        range.DateFrom,
        range.DateTo,
        generatedIds,
        cancellationToken);

      var generated = recordConverter.ConvertToModels(
        records,
        cancellationToken
      );

      var measurements = cloner.CloneWith(
        generated,
        clonedIds,
        cancellationToken
      );

      var requests = pushRequestConverter.ToPushRequests(
        measurements,
        cancellationToken
      );

      await foreach (var (batchPushRequests, batchMeasurements) in
        enumerable.Batch(requests, BatchSize, cancellationToken)
          .Zip(enumerable.Batch(measurements, BatchSize, cancellationToken)))
      {
        var request = await packer.Pack(
          messengerId,
          clock.Timestamp(),
          batchPushRequests,
          cancellationToken
        );

        await pushClient.Push(
          messengerId,
          apiKey,
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
