using System.Threading.Channels;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors.Base;

public class Reactor<TEventArgs, THandler>(
  ISubscriber<TEventArgs> subscriber,
  IServiceScopeFactory factory
) : BackgroundService, IReactor
  where TEventArgs : EventArgs
  where THandler : IHandler<TEventArgs>
{
  private readonly Channel<TEventArgs> channel =
    Channel.CreateUnbounded<TEventArgs>();

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.Subscribe(OnEvent);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.Unsubscribe(OnEvent);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = factory.CreateAsyncScope();
      try
      {
        var handler = scope.ServiceProvider.GetRequiredService<THandler>();
        await handler.Handle(eventArgs, stoppingToken);
      }
      catch (Exception ex)
      {
        var logger = scope.ServiceProvider
          .GetRequiredService<ILogger<Reactor<TEventArgs, THandler>>>();
        logger.LogError(ex, "Measurement upsert failed");
      }
    }
  }

  private void OnEvent(object? sender, TEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }
}
