using System.Text.Json;
using System.Threading.Channels;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries.Agnostic;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Reactors;

public class ErrorReactor(
  IErrorSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory
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
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnError(object? sender, ErrorEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    ErrorEventArgs eventArgs)
  {
    var activator = serviceProvider.GetRequiredService<AgnosticModelActivator>();
    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var notificationQueries = serviceProvider
      .GetRequiredService<OzdsNotificationQueries>();

    var content = new EventContent(
      eventArgs.Message,
      eventArgs.Exception.ToString(),
      eventArgs.Exception.StackTrace
    );

    var @event = activator.Activate<SystemEventModel>();
    @event.Id = eventArgs.Message;
    @event.Title = eventArgs.Message;
    @event.Timestamp = DateTimeOffset.UtcNow;
    @event.Content = JsonDocument.Parse(eventArgs.Exception.ToString());
    @event.Level = LevelModel.Error;
    @event.Content = JsonSerializer.SerializeToDocument(content);
    @event.Categories = new List<CategoryModel>
    {
      CategoryModel.All,
      CategoryModel.Error
    };
    var eventEntity = @event.ToEntity();
    context.Add(eventEntity);
    await context.SaveChangesAsync();
    @event.Id = eventEntity.Id;

    var notification = activator.Activate<NotificationModel>();
    notification.Id = eventArgs.Message;
    notification.Title = $"[OZDS]: Exception";
    notification.Summary = content.Message;
    notification.Timestamp = DateTimeOffset.UtcNow;
    notification.Content = $"Exception: \n{eventArgs.Exception}\nStack trace: \n{eventArgs.Exception.StackTrace}\n";
    notification.EventId = @event.Id;
    notification.Topics = new HashSet<TopicModel>
    {
      TopicModel.All,
      TopicModel.Error
    };
    var notificationEntity = @event.ToEntity();
    context.Add(notificationEntity);
    notification.Id = notificationEntity.Id;

    var recipients = await notificationQueries.Recipients(notification);
    context.AddRange(recipients);
  }

  private sealed record EventContent(
    string Message,
    string Exception,
    string? StackTrace
  );
}