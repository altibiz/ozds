using System.Threading.Channels;
using Ozds.Business.Observers.Abstractions;

namespace Ozds.Business.Observers.Base;

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

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    SubscribeIn(OnEvent);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    UnsubscribeIn(OnEvent);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var factory = serviceProvider
      .GetRequiredService<IServiceScopeFactory>();
    await foreach (var inEventArgs in inChannel.Reader
      .ReadAllAsync(stoppingToken))
    {
      await using var scope = factory.CreateAsyncScope();
      try
      {
        var pipe = scope.ServiceProvider.GetRequiredService<TPipe>();
        var outEventArgs = await pipe.Transform(inEventArgs, stoppingToken);
        OutEvent?.Invoke(this, outEventArgs);
      }
      catch (Exception ex)
      {
        var logger = scope.ServiceProvider.GetRequiredService<
          ILogger<Relay<TInEventArgs, TOutEventArgs, TPipe>>>();
        logger.LogError(ex, "Relay {Type} failed", GetType().Name);
      }
    }
  }

  private void OnEvent(object? sender, TInEventArgs eventArgs)
  {
    inChannel.Writer.TryWrite(eventArgs);
  }
}
