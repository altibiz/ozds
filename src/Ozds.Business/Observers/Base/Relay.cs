using System.Threading.Channels;
using Ozds.Business.Observers.Abstractions;

namespace Ozds.Business.Observers.Base;

// TODO: preserve events in db on shutdown and restore on startup and do not
// pass stopping token to handler

public abstract class Relay<TInEventArgs, TOutEventArgs, TPipe>(
  IServiceProvider serviceProvider
) : BackgroundService, ISubscriber<TOutEventArgs>
  where TInEventArgs : System.EventArgs
  where TOutEventArgs : System.EventArgs
  where TPipe : IPipe<TInEventArgs, TOutEventArgs>
{
  private readonly Channel<TInEventArgs> inChannel =
    Channel.CreateUnbounded<TInEventArgs>();

  private event EventHandler<TOutEventArgs>? OutEvent;

  public void Subscribe(EventHandler<TOutEventArgs> eventHandler)
  {
    OutEvent += eventHandler;
  }

  public void Unsubscribe(EventHandler<TOutEventArgs> eventHandler)
  {
    OutEvent -= eventHandler;
  }

  protected abstract void SubscribeIn(
    EventHandler<TInEventArgs> eventHandler);

  protected abstract void UnsubscribeIn(
    EventHandler<TInEventArgs> eventHandler);

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    SubscribeIn(OnEvent);
    await base.StartAsync(cancellationToken);
    var logger = serviceProvider.GetRequiredService<
      ILogger<Relay<TInEventArgs, TOutEventArgs, TPipe>>>();
    logger.LogInformation(
      "Relay {Relay} started",
      GetType().Name
    );
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    UnsubscribeIn(OnEvent);
    var logger = serviceProvider.GetRequiredService<
      ILogger<Relay<TInEventArgs, TOutEventArgs, TPipe>>>();
    if (!inChannel.Writer.TryComplete())
    {
      logger.LogWarning(
        "Relay {Relay} channel already completed",
        GetType().Name
      );
    }
    await base.StopAsync(cancellationToken);
    logger.LogInformation(
      "Relay {Relay} stopped",
      GetType().Name
    );
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var factory = serviceProvider
      .GetRequiredService<IServiceScopeFactory>();

    await foreach (var inEventArgs in inChannel.Reader
      .ReadAllAsync(stoppingToken))
    {
      await using var scope = factory.CreateAsyncScope();
      var logger = scope.ServiceProvider.GetRequiredService<
        ILogger<Relay<TInEventArgs, TOutEventArgs, TPipe>>>();
      var pipe = scope.ServiceProvider
        .GetRequiredService<TPipe>();

      logger.LogDebug(
        "Invoking pipe {Pipe} for relay {Relay} event {Event}",
        pipe.GetType().Name,
        GetType().Name,
        inEventArgs.GetType().Name
      );
      try
      {
        var outEventArgs = await pipe.Transform(inEventArgs, stoppingToken);
        OutEvent?.Invoke(this, outEventArgs);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Relay {Relay} handler {Handler} failed",
          GetType().Name,
          pipe.GetType().Name
        );
      }
    }
  }

  private void OnEvent(object? sender, TInEventArgs eventArgs)
  {
    if (!inChannel.Writer.TryWrite(eventArgs))
    {
      var logger = serviceProvider.GetRequiredService<
        ILogger<Relay<TInEventArgs, TOutEventArgs, TPipe>>>();
      logger.LogWarning(
        "Relay {Relay} event {Event} dropped",
        GetType().Name,
        eventArgs.GetType().Name
      );
    }
  }
}
