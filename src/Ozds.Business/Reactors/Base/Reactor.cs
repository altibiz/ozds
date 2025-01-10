using System.Threading.Channels;
using Ozds.Business.Extensions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors.Base;

// TODO: preserve events in db on shutdown and restore on startup and do not
// pass stopping token to handler

public abstract class Reactor<TEventArgs, TSubscriber, THandler>(
  IServiceProvider serviceProvider
) : BackgroundService, IReactor
  where TEventArgs : EventArgs
  where TSubscriber : ISubscriber<TEventArgs>
  where THandler : IHandler<TEventArgs>
{
  private readonly Channel<TEventArgs> channel =
    Channel.CreateUnbounded<TEventArgs>();

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    var subscriber = serviceProvider.GetRequiredService<TSubscriber>();
    subscriber.Subscribe(OnEvent);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    var subscriber = serviceProvider.GetRequiredService<TSubscriber>();
    subscriber.Unsubscribe(OnEvent);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    var lifetime = serviceProvider
      .GetRequiredService<IHostApplicationLifetime>();
    var factory = serviceProvider
      .GetRequiredService<IServiceScopeFactory>();

    if (!await lifetime.WaitForAppStartup(stoppingToken))
    {
      return;
    }

    {
      await using var scope = factory.CreateAsyncScope();
      try
      {
        var handler = scope.ServiceProvider
          .GetRequiredService<THandler>();
        await handler.AfterStartAsync(stoppingToken);
      }
      catch (Exception ex)
      {
        var logger = scope.ServiceProvider.GetRequiredService<
          ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
        logger.LogError(ex, "Reactor {Type} failed", GetType().Name);
      }
    }

    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = factory.CreateAsyncScope();
      try
      {
        var handler = scope.ServiceProvider
          .GetRequiredService<THandler>();
        await handler.Handle(eventArgs, stoppingToken);
      }
      catch (Exception ex)
      {
        var logger = scope.ServiceProvider.GetRequiredService<
          ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
        logger.LogError(ex, "Reactor {Type} failed", GetType().Name);
      }
    }

    if (!await lifetime.WaitForAppShutdown(stoppingToken))
    {
      return;
    }

    {
      await using var scope = factory.CreateAsyncScope();
      try
      {
        var handler = scope.ServiceProvider
          .GetRequiredService<THandler>();
        await handler.BeforeStopAsync(stoppingToken);
      }
      catch (Exception ex)
      {
        var logger = scope.ServiceProvider.GetRequiredService<
          ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
        logger.LogError(ex, "Reactor {Type} failed", GetType().Name);
      }
    }
  }

  private void OnEvent(object? sender, TEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }
}
