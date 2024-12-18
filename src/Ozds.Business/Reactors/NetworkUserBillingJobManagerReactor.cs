using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors;

public class NetworkUserBillingJobManagerReactor(
  IServiceScopeFactory serviceScopeFactory,
  IEntityChangesSubscriber subscriber,
  IHostApplicationLifetime lifetime
) : BackgroundService, IReactor
{
  private readonly Channel<EntitiesChangedEventArgs> channel =
    Channel.CreateUnbounded<EntitiesChangedEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeEntitiesChanged(OnEntitiesChanged);
    await base.StartAsync(cancellationToken);
  }

  public override async Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribeEntitiesChanged(OnEntitiesChanged);
    await base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    if (!await WaitForAppStartup(lifetime, stoppingToken))
    {
      return;
    }

    await using (var scope = serviceScopeFactory.CreateAsyncScope())
    {
      var context = scope.ServiceProvider.GetRequiredService<DataDbContext>();
      var manager =
        scope.ServiceProvider
          .GetRequiredService<INetworkUserBillingJobManager>();

      var networkUsers = await context.NetworkUsers.ToListAsync(stoppingToken);
      foreach (var networkUser in networkUsers)
      {
        await manager.EnsureMonthlyBillingJob(networkUser.Id);
      }
    }

    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnEntitiesChanged(
    object? sender,
    EntitiesChangedEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    EntitiesChangedEventArgs eventArgs)
  {
    var manager = serviceProvider
      .GetRequiredService<INetworkUserBillingJobManager>();

    foreach (var entry in eventArgs.Entities)
    {
      if (entry.Entity is not NetworkUserEntity networkUser)
      {
        continue;
      }

      if (entry.State is EntityChangedState.Added)
      {
        await manager.EnsureMonthlyBillingJob(networkUser.Id);
      }

      if (entry.State is EntityChangedState.Removed)
      {
        await manager.UnscheduleMonthlyBillingJob(networkUser.Id);
      }

      if (entry.State is EntityChangedState.Modified)
      {
        await manager.RescheduleMonthlyBillingJob(networkUser.Id);
      }
    }
  }

  private static async Task<bool> WaitForAppStartup(
    IHostApplicationLifetime lifetime,
    CancellationToken stoppingToken)
  {
    var startedSource = new TaskCompletionSource();
    var cancelledSource = new TaskCompletionSource();

    using var reg1 =
      lifetime.ApplicationStarted.Register(() => startedSource.SetResult());
    using var reg2 = stoppingToken.Register(() => cancelledSource.SetResult());

    var completedTask = await Task.WhenAny(
      startedSource.Task, cancelledSource.Task);

    return completedTask == startedSource.Task;
  }
}
