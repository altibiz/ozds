using System.Threading.Channels;
using Ozds.Business.Extensions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Reactors.Abstractions;

namespace Ozds.Business.Reactors.Base;

// TODO: preserve events in db on shutdown and restore on startup and do not
// pass stopping token to handler
// TODO: pass a meaningful CancellationToken to BeforeStopAsync handlers

public abstract class Reactor<TEventArgs, TSubscriber, THandler>(
  IServiceProvider serviceProvider
) : BackgroundService, IReactor
  where TEventArgs : EventArgs
  where TSubscriber : ISubscriber<TEventArgs>
  where THandler : IHandler<TEventArgs>
{
  private readonly Channel<TEventArgs> channel =
    Channel.CreateUnbounded<TEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    var subscriber = serviceProvider.GetRequiredService<TSubscriber>();
    subscriber.Subscribe(OnEvent);
    await base.StartAsync(cancellationToken);
    var logger = serviceProvider.GetRequiredService<
      ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
    logger.LogInformation(
      "Reactor {Reactor} started",
      GetType().Name
    );
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    var subscriber = serviceProvider.GetRequiredService<TSubscriber>();
    subscriber.Unsubscribe(OnEvent);
    var logger = serviceProvider.GetRequiredService<
      ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
    if (!channel.Writer.TryComplete())
    {
      logger.LogWarning(
        "Reactor {Reactor} channel already completed",
        GetType().Name
      );
    }

    await base.StopAsync(cancellationToken);
    logger.LogInformation(
      "Reactor {Reactor} stopped",
      GetType().Name
    );
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
      var logger = scope.ServiceProvider.GetRequiredService<
        ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
      try
      {
        var handler = scope.ServiceProvider
          .GetRequiredService<THandler>();
        logger.LogDebug(
          "Invoking handler {Handler} for reactor {Reactor} after start",
          handler.GetType().Name,
          GetType().Name
        );
        await handler.AfterStartAsync(stoppingToken);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Reactor {Reactor} handler {Handler} after start failed",
          GetType().Name,
          typeof(THandler).Name
        );
      }
    }

    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = factory.CreateAsyncScope();
      var logger = scope.ServiceProvider.GetRequiredService<
        ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();

      logger.LogDebug(
        "Invoking handler {Handler} for reactor {Reactor} event {Event}",
        typeof(THandler).Name,
        GetType().Name,
        eventArgs.GetType().Name
      );
      try
      {
        var handler = scope.ServiceProvider
          .GetRequiredService<THandler>();
        await handler.Handle(eventArgs, stoppingToken);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Reactor {Reactor} handler {Handler} failed",
          GetType().Name,
          typeof(THandler).Name
        );
      }
    }

    {
      await using var scope = factory.CreateAsyncScope();
      var handler = scope.ServiceProvider
        .GetRequiredService<THandler>();
      var logger = scope.ServiceProvider.GetRequiredService<
        ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
      try
      {
        logger.LogDebug(
          "Invoking handler {Handler} for reactor {Reactor} before stop",
          handler.GetType().Name,
          GetType().Name
        );
        await handler.BeforeStopAsync(CancellationToken.None);
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "Reactor {Reactor} handler {Handler} before stop failed",
          GetType().Name,
          handler.GetType().Name
        );
      }
    }
  }

  private void OnEvent(object? sender, TEventArgs eventArgs)
  {
    if (!channel.Writer.TryWrite(eventArgs))
    {
      var logger = serviceProvider.GetRequiredService<
        ILogger<Reactor<TEventArgs, TSubscriber, THandler>>>();
      logger.LogWarning(
        "Reactor {Reactor} event {Event} dropped",
        GetType().Name,
        eventArgs.GetType().Name
      );
    }
  }
}
