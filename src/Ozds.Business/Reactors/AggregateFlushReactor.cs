using System.Diagnostics;
using System.Threading.Channels;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors;

public class AggregateFlushReactor(
  IAggregateFlushSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory,
  ILogger<AggregateFlushReactor> logger
) : BackgroundService, IReactor
{
  private readonly Channel<AggregateFlushEventArgs> channel =
    Channel.CreateUnbounded<AggregateFlushEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeFlush(OnFlush);
    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeFlush(OnFlush);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      try
      {
        await Handle(eventArgs);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Aggregate flush failed for {Count} aggregates",
          eventArgs.Aggregates.Count);
      }
    }
  }

  private void OnFlush(object? sender, AggregateFlushEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private async Task Handle(
    AggregateFlushEventArgs eventArgs)
  {
    await using var scope = serviceScopeFactory.CreateAsyncScope();
    var mutations = scope.ServiceProvider
      .GetRequiredService<AggregateUpsertMutations>();
    var aggregates = eventArgs.Aggregates;
    var stopwatch = Stopwatch.StartNew();
    var result = await mutations
      .UpsertAggregates(aggregates, CancellationToken.None);
    stopwatch.Stop();
    logger.LogDebug(
      "Flushed {Count} aggregates in {Elapsed}",
      result.Count,
      stopwatch.Elapsed);
  }
}
