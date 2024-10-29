using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Agnostic;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities.Joins;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Reactors;

public class ErrorReactor(
  IDbContextFactory<DataDbContext> factory,
  AgnosticModelActivator activator,
  AgnosticModelEntityConverter converter,
  NotificationQueries notificationQueries,
  IErrorSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<ErrorEventArgs> channel =
    Channel.CreateUnbounded<ErrorEventArgs>();

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeError(OnError);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribeError(OnError);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var context = await factory
        .CreateDbContextAsync(stoppingToken);
      await Handle(
        context,
        activator,
        converter,
        notificationQueries,
        eventArgs
      );
    }
  }

  private void OnError(object? sender, ErrorEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    DataDbContext context,
    AgnosticModelActivator activator,
    AgnosticModelEntityConverter converter,
    NotificationQueries notificationQueries,
    ErrorEventArgs eventArgs)
  {

    var content = new EventContent(
      eventArgs.Message,
      eventArgs.Exception.ToString(),
      eventArgs.Exception.StackTrace
    );

    var @event = activator.Activate<SystemEventModel>();
    @event.Title = "Exception";
    @event.Timestamp = DateTimeOffset.UtcNow;
    @event.Level = LevelModel.Error;
    @event.Content = JsonSerializer.SerializeToDocument(
      new
      {
        content.Message,
        content.Exception,
        content.StackTrace
      });
    @event.Categories = new List<CategoryModel>
    {
      CategoryModel.All,
      CategoryModel.Error
    };
    var eventEntity = @event.ToEntity();
    context.Events.Add(eventEntity);
    await context.SaveChangesAsync();
    @event.Id = eventEntity.Id;

    var notification = activator.Activate<SystemNotificationModel>();
    notification.Title = "Exception";
    notification.Summary = content.Message;
    notification.Timestamp = DateTimeOffset.UtcNow;
    notification.Content = $"Exception: \n{eventArgs.Exception}\nStack trace: \n{eventArgs.Exception.StackTrace}\n";
    notification.EventId = @event.Id;
    notification.Topics = new HashSet<TopicModel>
    {
      TopicModel.All,
      TopicModel.Error
    };
    var notificationEntity = notification.ToEntity();
    context.Notifications.Add(notificationEntity);
    await context.SaveChangesAsync();
    notification.Id = notificationEntity.Id;

    var recipients = (await notificationQueries.Recipients(notification))
      .Select(converter.ToEntity<NotificationRecipientEntity>)
      .ToArray();
    context.NotificationRecipients.AddRange(recipients);
    await context.SaveChangesAsync();
  }

  private sealed record EventContent(
    string Message,
    string Exception,
    string? StackTrace
  );
}
