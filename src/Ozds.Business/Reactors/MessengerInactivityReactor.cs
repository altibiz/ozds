using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Workers;

// TODO: handle null meter
// TODO: paging when fetching

public class MeterInactivityWorker(
  IServiceScopeFactory serviceScopeFactory,
  IMessengerJobSubscriber subscriber
) : BackgroundService, IReactor
{
  private static readonly JsonSerializerOptions
    EventContentSerializationOptions = new()
    {
      WriteIndented = true
    };

  private readonly Channel<MessengerInactivityEventArgs> channel =
    Channel.CreateUnbounded<MessengerInactivityEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeInactivity(OnInactivity);

    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribeInactivity(OnInactivity);

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

  private void OnInactivity(object? sender, MessengerInactivityEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    MessengerInactivityEventArgs eventArgs)
  {
    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var converter = serviceProvider.GetRequiredService<AgnosticModelEntityConverter>();

    var meter = (await context.Messengers
        .FirstOrDefaultAsync(
          context.PrimaryKeyEquals<MessengerEntity>(eventArgs.Id)))
      ?.ToModel();

    if (meter is null)
    {
      return;
    }

    var lastPushEvent = await context.Events
      .OfType<MessengerEventEntity>()
      .Where(x => x.MessengerId == meter.Id)
      .Where(x => x.Categories.Contains(CategoryEntity.MessengerPush))
      .OrderByDescending(x => x.Timestamp)
      .FirstOrDefaultAsync();

    var recipients = (await context.Representatives
        .Where(
          x => x.Topics.Contains(TopicEntity.All)
            || x.Topics.Contains(TopicEntity.Messenger)
            || x.Topics.Contains(TopicEntity.MessengerInactivity))
        .ToListAsync())
      .Select(x => x.ToModel());

    var notification = MessengerNotificationModelActivator.New();
    notification.MeterId = meter.Id;
    notification.Topics =
    [
      TopicModel.All,
      TopicModel.Messenger,
      TopicModel.MessengerInactivity
    ];
    notification.Summary = $"Meter \"{meter.Title}\" is inactive";
    if (lastPushEvent is null)
    {
      notification.Content = "Meter never pushed";
    }
    else
    {
      var builder = new StringBuilder();
      var lastPushEventDetails = JsonSerializer.Serialize(
        lastPushEvent.Content,
        EventContentSerializationOptions
      );
      builder.AppendLine($"Meter: \"{meter.Title}\"");
      builder.AppendLine($"Last pushed at: {lastPushEvent.Timestamp}");
      builder.AppendLine($"Last push details: {lastPushEventDetails}");
      notification.Content = builder.ToString();
    }

    context.Add(converter.ToEntity(notification));
    context.AddRange(
      recipients
        .Select(
          recipient => new NotificationRecipientModel
          {
            NotificationId = notification.Id,
            RepresentativeId = recipient.Id
          })
        .Select(converter.ToEntity));
    await context.SaveChangesAsync();
  }
}
