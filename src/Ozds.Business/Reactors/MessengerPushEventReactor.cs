using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models.Base;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Iot.Observers.Abstractions;
using Ozds.Iot.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class MessengerPushEventReactor(
  IServiceScopeFactory serviceScopeFactory,
  IPushSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<PushEventArgs> channel =
    Channel.CreateUnbounded<PushEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribePush(OnPush);
    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribePush(OnPush);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnPush(object? sender, PushEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    PushEventArgs eventArgs)
  {
    var context = serviceProvider.GetRequiredService<DataDbContext>();

    var messenger = (await context.Messengers
        .Where(context.PrimaryKeyEquals<MessengerEntity>(eventArgs.MessengerId))
        .FirstOrDefaultAsync())
      ?.ToModel();

    if (messenger is null)
    {
      return;
    }

    var @event = new MessengerEventEntity
    {
      Title = $"Messenger \"{messenger.Title}\" has pushed",
      Timestamp = DateTimeOffset.UtcNow,
      MessengerId = messenger.Id,
      Level = LevelEntity.Info,
      Content = CreateEventContent(eventArgs, messenger),
      Categories =
      [
        CategoryEntity.All, CategoryEntity.Messenger,
        CategoryEntity.MessengerPush
      ]
    };

    context.Add(@event);
    await context.SaveChangesAsync();
  }

  private static JsonDocument CreateEventContent(
    PushEventArgs eventArgs,
    MessengerModel messenger)
  {
    var content = new EventContent(
      messenger.Id,
      eventArgs.Request.Measurements.Count,
      eventArgs.Request.Measurements
        .GroupBy(x => x.MeterId)
        .Select(group => new EventContentMeter(group.Key, group.Count()))
        .ToArray()
    );

    return JsonDocument.Parse(JsonSerializer.Serialize(content));
  }

  private sealed record EventContent(
    string MessengerId,
    int Count,
    EventContentMeter[] Meters
  );

  private sealed record EventContentMeter(
    string Id,
    int Count
  );
}
