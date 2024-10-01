using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Workers;

// TODO: handle null meter
// TODO: paging when fetching
// TODO: recipients by role

public class MeterInactivityReactor(
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

  private void OnInactivity(
    object? sender,
    MessengerInactivityEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    MessengerInactivityEventArgs eventArgs)
  {
    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var converter =
      serviceProvider.GetRequiredService<AgnosticModelEntityConverter>();
    var environment = serviceProvider.GetRequiredService<IHostEnvironment>();

    var messenger = (await context.Messengers
        .Where(context.PrimaryKeyEquals<MessengerEntity>(eventArgs.Id))
        .FirstOrDefaultAsync())
      ?.ToModel();

    if (messenger is null)
    {
      return;
    }

    var lastPushEvent = await context.Events
      .OfType<MessengerEventEntity>()
      .Where(x => x.MessengerId == messenger.Id)
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
    notification.MessengerId = messenger.Id;
    notification.Topics =
    [
      TopicModel.All,
      TopicModel.Messenger,
      TopicModel.MessengerInactivity
    ];
    notification.Title = "Meter is inactive";
    notification.Summary = $"Meter \"{messenger.Title}\" is inactive";
    var builder = new StringBuilder();
    if (environment.IsDevelopment())
    {
      builder.AppendLine($"Stared at: {eventArgs.StartedAt}");
      builder.AppendLine($"Scheduled to fire at: {eventArgs.ScheduledFireAt}");
      builder.AppendLine($"Fired at: {eventArgs.FiredAt}");
      builder.AppendLine($"Scheduled at: {eventArgs.ScheduledAt}");
      builder.AppendLine($"Refire count: {eventArgs.RefireCount}");
      builder.AppendLine();
    }

    if (lastPushEvent is null)
    {
      builder.AppendLine("Meter never pushed");
    }
    else
    {
      var lastPushEventDetails = JsonSerializer.Serialize(
        lastPushEvent.Content,
        EventContentSerializationOptions
      );
      builder.AppendLine($"Meter: \"{messenger.Title}\"");
      builder.AppendLine($"Last pushed at: {lastPushEvent.Timestamp}");
      builder.AppendLine($"Last push details: {lastPushEventDetails}");
    }

    notification.Content = builder.ToString();
    var notificationEntity =
      converter.ToEntity<MessengerNotificationEntity>(notification);
    context.Add(notificationEntity);
    await context.SaveChangesAsync();
    notification.Id = notificationEntity.Id;

    var notificationRecipients = recipients.Select(
      recipient => new NotificationRecipientModel
      {
        NotificationId = notification.Id,
        RepresentativeId = recipient.Id
      });
    context.AddRange(notificationRecipients.Select(converter.ToEntity));
    await context.SaveChangesAsync();
  }
}
