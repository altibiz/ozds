using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Notifications.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;
using Ozds.Jobs.Manager.Abstractions;
using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Business.Services;

// TODO: handle null messenger
// TODO: messenger paging
// TODO: configurable inactivity duration

public class MessengerInactivityService(
  IMessengerJobSubscriber subscriber,
  IMessengerJobManager manager,
  IServiceScopeFactory serviceScopeFactory
) : BackgroundService
{
  private static readonly JsonSerializerOptions
    EventContentSerializationOptions = new()
    {
      WriteIndented = true
    };

  private readonly Channel<string> inactive =
    Channel.CreateUnbounded<string>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    await using (var scope = serviceScopeFactory.CreateAsyncScope())
    {
      var context = scope.ServiceProvider.GetRequiredService<DataDbContext>();
      var messengers = await context.Messengers.ToListAsync();
      foreach (var messenger in messengers)
      {
        await manager.EnsureInactivityMonitorJob(
          messenger.Id,
          TimeSpan.FromMinutes(5)
        );
      }
    }

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
    while (!stoppingToken.IsCancellationRequested)
    {
      var id = await inactive.Reader.ReadAsync(stoppingToken);
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Notify(scope.ServiceProvider, id);
    }
  }

  private void OnInactivity(object? sender, MessengerInactivityEventArgs args)
  {
    inactive.Writer.TryWrite(args.Id);
  }

  private static async Task Notify(IServiceProvider serviceProvider, string id)
  {
    var sender = serviceProvider.GetRequiredService<INotificationSender>();
    var context = serviceProvider.GetRequiredService<DataDbContext>();

    var messenger = (await context.Messengers
        .FirstOrDefaultAsync(context.PrimaryKeyEquals<MessengerEntity>(id)))
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

    var notification = MessengerNotificationModel.New();
    notification.MessengerId = messenger.Id;
    notification.Topics =
    [
      TopicModel.All,
      TopicModel.Messenger,
      TopicModel.MessengerInactivity
    ];
    notification.Summary = $"Messenger \"{messenger.Title}\" is inactive";
    if (lastPushEvent is null)
    {
      notification.Content = "Messenger never pushed";
    }
    else
    {
      var builder = new StringBuilder();
      var lastPushEventDetails = JsonSerializer.Serialize(
        lastPushEvent.Content,
        EventContentSerializationOptions
      );
      builder.AppendLine($"Messenger: \"{messenger.Title}\"");
      builder.AppendLine($"Last pushed at: {lastPushEvent.Timestamp}");
      builder.AppendLine($"Last push details: {lastPushEventDetails}");
      notification.Content = builder.ToString();
    }

    await sender.SendAsync(notification, recipients);
  }
}
