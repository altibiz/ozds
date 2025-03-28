using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Data.Mutations.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;
using Ozds.Data.Procedures;

namespace Ozds.Data.Mutations;

public class MeasurementMutations(
  IDbContextFactory<DataDbContext> factory,
  ILogger<MeasurementMutations> logger,
  IEntitiesChangingPublisher changingPublisher,
  IEntitiesChangedPublisher changedPublisher,
  MeasurementProcedures procedures
) : IMutations
{
  public async Task<List<IMeasurementEntity>> CreateMeasurements(
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken,
    bool triggerEvents = true
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    if (triggerEvents)
    {
      measurements = measurements.ToList();
    }

    if (triggerEvents)
    {
      changingPublisher.Publish(
        new EntitiesChangingEventArgs
        {
          Entities = measurements
            .Select(
              measurement => new EntityChangingEntry(
                EntityChangingState.Adding,
                measurement))
            .ToList()
        });
    }

    var result = await CreateMeasurements(
      context,
      measurements,
      cancellationToken
    );

    if (triggerEvents)
    {
      changedPublisher.Publish(
        new EntitiesChangedEventArgs
        {
          Entities = result
            .Select(
              measurement => new EntityChangedEntry(
                EntityChangedState.Added,
                measurement))
            .ToList()
        });
    }

    return result;
  }

  public async Task<List<IMeasurementEntity>> CreateMeasurements(
    IAsyncEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken,
    bool triggerEvents = true
  )
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    List<IMeasurementEntity>? measurementsList = null;
    if (triggerEvents)
    {
      measurementsList = await measurements.ToListAsync(cancellationToken);
    }

    if (triggerEvents)
    {
      changingPublisher.Publish(
        new EntitiesChangingEventArgs
        {
          Entities = measurementsList!
            .Select(
              measurement => new EntityChangingEntry(
                EntityChangingState.Adding,
                measurement))
            .ToList()
        });
    }

    var result = measurementsList is not null
      ? await CreateMeasurements(
        context,
        measurementsList,
        cancellationToken
      )
      : await CreateMeasurements(
        context,
        measurements,
        cancellationToken
      );

    if (triggerEvents)
    {
      changedPublisher.Publish(
        new EntitiesChangedEventArgs
        {
          Entities = result
            .Select(
              measurement => new EntityChangedEntry(
                EntityChangedState.Added,
                measurement))
            .ToList()
        });
    }

    return result;
  }

  // NOTE: internal because used in tests
  internal async Task<List<IMeasurementEntity>> CreateMeasurements(
    DataDbContext context,
    IEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    var grouped = measurements
      .OrderBy(
        x => x is IAggregateEntity aggregate
          ? aggregate.Interval switch
          {
            IntervalEntity.Month or IntervalEntity.Day => 1,
            IntervalEntity.QuarterHour => 2,
            _ => throw new InvalidOperationException(
              $"Unknown interval {aggregate.Interval}.")
          }
          : 0)
      .GroupBy(
        x => (
          Type: x.GetType(),
          Interval:
          x is AggregateEntity aggregate
            ? (IntervalEntity?)aggregate.Interval
            : null))
      .Select(x => new MeasurementGroup(x.Key.Type, x.Key.Interval, x.ToList()))
      .ToList();
    if (grouped.Count == 0)
    {
      return new List<IMeasurementEntity>();
    }

    return await CreateMeasurements(context, grouped, cancellationToken);
  }

  // NOTE: internal because used in tests
  internal async Task<List<IMeasurementEntity>> CreateMeasurements(
    DataDbContext context,
    IAsyncEnumerable<IMeasurementEntity> measurements,
    CancellationToken cancellationToken
  )
  {
    var grouped = await measurements
      .OrderBy(
        x => x is IAggregateEntity aggregate
          ? aggregate.Interval switch
          {
            IntervalEntity.Month or IntervalEntity.Day => 1,
            IntervalEntity.QuarterHour => 2,
            _ => throw new InvalidOperationException(
              $"Unknown interval {aggregate.Interval}.")
          }
          : 0)
      .GroupBy(
        x => (
          Type: x.GetType(),
          Interval:
          x is AggregateEntity aggregate
            ? (IntervalEntity?)aggregate.Interval
            : null))
      .SelectAwait(
        async x =>
        {
          var measurements = await x.ToListAsync();
          return new MeasurementGroup(x.Key.Type, x.Key.Interval, measurements);
        })
      .ToListAsync(cancellationToken);
    if (grouped.Count == 0)
    {
      return new List<IMeasurementEntity>();
    }

    return await CreateMeasurements(context, grouped, cancellationToken);
  }

  private async Task<List<IMeasurementEntity>> CreateMeasurements(
    DataDbContext context,
    List<MeasurementGroup> grouped,
    CancellationToken cancellationToken
  )
  {
    var groupChunkSize = 10000;
    List<IMeasurementEntity>? results = null;
    if (context.Database.CurrentTransaction is not null)
    {
      results = await Execute(
        context,
        grouped,
        groupChunkSize,
        cancellationToken
      );
    }
    else
    {
      while (true)
      {
        try
        {
          var isolationLevel = IsolationLevel.RepeatableRead;

          await using var transaction = await context.Database
            .BeginTransactionAsync(isolationLevel, cancellationToken);

          results = await Execute(
            context,
            grouped,
            groupChunkSize,
            cancellationToken
          );

          await context.Database
            .CommitTransactionAsync(cancellationToken);

          break;
        }
        catch (PostgresException ex)
        {
          if (context.Database.CurrentTransaction is { } transaction)
          {
            await transaction.RollbackAsync(cancellationToken);
          }

          // NOTE: 40001 = could not serialize access due to
          // read/write dependencies among transactions
          // NOTE: 40P01 = deadlock detected
          if (ex.SqlState == "40001" || ex.SqlState == "40P01")
          {
            // NOTE: not logging exception because it prints way too much
#pragma warning disable S6667 // Logging in a catch clause should pass the caught exception as a parameter.
            logger.LogDebug(
              "Retying insert of {Count} measurements because of serialization issues...",
              grouped.Sum(x => x.Measurements.Count));
#pragma warning restore S6667 // Logging in a catch clause should pass the caught exception as a parameter.
            continue;
          }

          // NOTE: 21000 ON CONFLICT DO UPDATE command cannot affect
          // row a second time
          if (ex.SqlState == "21000")
          {
            var offendingAggregates = string.Join(
              "\n\n",
              grouped
                .SelectMany(group => group.Measurements)
                .OfType<IAggregateEntity>()
                .GroupBy(
                  aggregate => new
                  {
                    Type = aggregate.GetType(),
                    aggregate.Interval,
                    aggregate.MeterId,
                    aggregate.MeasurementLocationId,
                    aggregate.Timestamp
                  })
                .Select(
                  x => new
                  {
                    x.Key,
                    List = x.ToList()
                  })
                .Where(x => x.List.Count > 1)
                .Select(
                  x => string.Join(
                    "\n",
                    $"Type: {x.Key.Type.Name}",
                    $"Interval: {x.Key.Interval}",
                    $"Meter ID: {x.Key.MeterId}",
                    $"Measurement Location ID: {x.Key.MeasurementLocationId}",
                    $"Timestamp: {x.Key.Timestamp}",
                    $"Count: {x.List.Count}")));
            logger.LogError(
              "Aggregate update affected a row more than once."
              + " {Count} aggregates affected."
              + "\nOffending aggregates:\n{OffendingAggregates}",
              grouped.Sum(x => x.Measurements.Count),
              offendingAggregates
            );
          }

          throw;
        }
        catch (NpgsqlException ex)
        {
          if (ex.InnerException is TimeoutException)
          {
            // NOTE: not logging exception because it prints way too much
#pragma warning disable S6667 // Logging in a catch clause should pass the caught exception as a parameter.
            logger.LogDebug(
              "Retying insert of {Count} measurements because timeout with chunk size {ChunkSize}...",
              grouped.Sum(x => x.Measurements.Count),
              groupChunkSize);
#pragma warning restore S6667 // Logging in a catch clause should pass the caught exception as a parameter.

            // TODO: figure out what to do when we're already at 10
            if (groupChunkSize > 10)
            {
              groupChunkSize /= 10;
            }

            continue;
          }

          throw;
        }
        catch (Exception)
        {
          if (context.Database.CurrentTransaction is { } transaction)
          {
            await transaction.RollbackAsync(cancellationToken);
          }

          throw;
        }
      }
    }

    return results
      .GroupBy(
        x => new
        {
          x.MeterId,
          x.MeasurementLocationId,
          x.Timestamp,
          Interval = x is IAggregateEntity aggregate
            ? aggregate.Interval
            : (IntervalEntity?)null
        })
      .Select(x => x.Last())
      .ToList();
  }

  private async Task<List<IMeasurementEntity>> Execute(
    DataDbContext context,
    List<MeasurementGroup> groups,
    int groupChunkSize,
    CancellationToken cancellationToken
  )
  {
    var results = new List<IMeasurementEntity>();
    foreach (var group in groups)
    {
      foreach (var (index, chunk) in group.Measurements
        .Chunk(groupChunkSize)
        .Select((x, i) => (i, x)))
      {
        var json = context.CreateBulkJsonParameter(chunk);
        var jsonParameter = new JsonParameter(json);
        var objects = await ExecuteChunk(
          context,
          index,
          jsonParameter,
          group.Type,
          group.Interval,
          cancellationToken
        );
        results.AddRange(objects.OfType<IMeasurementEntity>());
      }
    }

    return results;
  }

  private async Task<List<object>> ExecuteChunk(
    DataDbContext context,
    int index,
    JsonParameter jsonParameter,
    Type type,
    IntervalEntity? interval,
    CancellationToken cancellationToken
  )
  {
    var jsonParameterName = "@p" + index;

    var sql = interval is { } aggregateInterval
      ? procedures.CallUpsertAggregates(
        context,
        type,
        aggregateInterval,
        jsonParameterName)
      : procedures.CallUpsertMeasurements(
        context,
        type,
        jsonParameterName);

    // NOTE: good for debugging - please don't remove
#pragma warning disable S125 // Sections of code should not be commented out
    // Console.WriteLine(sql);
    // logger.LogInformation(
    //   "{Json}",
    //   System.Text.Json.JsonSerializer.Serialize(
    //     json,
    //     new System.Text.Json.JsonSerializerOptions()
    //     {
    //       WriteIndented = true
    //     }));
    // logger.LogInformation(sql);
#pragma warning restore S125 // Sections of code should not be commented out

    return await context.DapperCommand(
      type,
      sql,
      cancellationToken,
      new Dictionary<string, object?>
      {
        { jsonParameterName, jsonParameter }
      }
    );
  }

  private sealed record MeasurementGroup(
    Type Type,
    IntervalEntity? Interval,
    List<IMeasurementEntity> Measurements
  );
}
