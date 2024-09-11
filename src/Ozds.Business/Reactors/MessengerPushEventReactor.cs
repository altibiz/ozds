using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Business.Reactors;

public class MessengerPushEventWorker(
  IServiceScopeFactory serviceScopeFactory,
  IMeasurementUpsertSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<MeasurementPushEventArgs> channel =
    Channel.CreateUnbounded<MeasurementPushEventArgs>();

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

  private void OnPush(object? sender, MeasurementPushEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    MeasurementPushEventArgs eventArgs)
  {
    var context = serviceProvider.GetRequiredService<DataDbContext>();

    var messengerIds = eventArgs.Measurements.Select(x => x.MessengerId).Distinct().ToList();

    var messengers = await context.Messengers
        .Where(context.PrimaryKeyIn<MessengerEntity>(messengerIds))
        .Select(x => x.ToModel())
        .ToListAsync();

    var events = messengers.Select(
      messenger => new MessengerEventEntity
      {
        Title = $"Messenger \"{messenger.Title}\" has pushed",
        Timestamp = DateTimeOffset.UtcNow,
        MessengerId = messenger.Id,
        Level = LevelEntity.Info,
        Content = CreateEventContent(eventArgs, messenger),
        Categories = [CategoryEntity.All, CategoryEntity.Messenger, CategoryEntity.MessengerPush],
      });

    context.AddRange(events);
    await context.SaveChangesAsync();
  }

  private static JsonDocument CreateEventContent(
    MeasurementPushEventArgs eventArgs,
    MessengerModel messenger)
  {
    var content = new EventContent(
      messenger.Id,
      eventArgs.Measurements
        .Where(x => x.MessengerId == messenger.Id)
        .Select(x => x.LineId)
        .Distinct()
        .Count(),
      eventArgs.Measurements
        .Where(x => x.MessengerId == messenger.Id)
        .GroupBy(x => x.LineId)
        .Select(group => new EventContentLine(group.Key, group.Count()))
        .ToArray()
    );

    return JsonDocument.Parse(JsonSerializer.Serialize(content));
  }

  private sealed record EventContent(
    string MessengerId,
    int Count,
    EventContentLine[] Lines
  );

  private sealed record EventContentLine(
    string Id,
    int Count
  );
}
