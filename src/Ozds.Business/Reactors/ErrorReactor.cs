using System.Text.Json;
using System.Threading.Channels;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Abstractions;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Reactors;

public class ErrorReactor(
  IServiceScopeFactory scopeFactory,
  IErrorSubscriber subscriber,
  ILogger<ErrorReactor> logger
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
      await using var scope = scopeFactory.CreateAsyncScope();
      try
      {
        await Handle(scope.ServiceProvider, eventArgs);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Measurement upsert failed");
      }
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
    var activator = serviceProvider
      .GetRequiredService<AgnosticModelActivator>();
    var notificationQueries = serviceProvider
      .GetRequiredService<NotificationQueries>();
    var notificationMutations = serviceProvider
      .GetRequiredService<NotificationMutations>();
    var readonlyMutations = serviceProvider
      .GetRequiredService<ReadonlyMutations>();

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
    @event.Id = await readonlyMutations
      .Create(@event, CancellationToken.None);

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
    notification.Id = await notificationMutations
      .Create(notification, CancellationToken.None);

    var recipients = await notificationQueries.Recipients(notification);
    await notificationMutations
      .AddRecipients(recipients, CancellationToken.None);
  }

  private sealed record EventContent(
    string Message,
    string Exception,
    string? StackTrace
  );
}
