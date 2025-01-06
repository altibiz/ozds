using System.Text.Json;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Mutations.Agnostic;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Business.Reactors.Base;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Reactors;

public class ErrorReactor(
  ISubscriber<ErrorEventArgs> subscriber,
  IServiceScopeFactory factory
) : Reactor<ErrorEventArgs, ErrorHandler>(subscriber, factory)
{
}

public class ErrorHandler(
  AgnosticModelActivator activator,
  NotificationQueries notificationQueries,
  NotificationMutations notificationMutations,
  ReadonlyMutations readonlyMutations
) : IHandler<ErrorEventArgs>
{
  public async Task Handle(
    ErrorEventArgs eventArgs,
    CancellationToken cancellationToken)
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
    @event.Id = await readonlyMutations.Create(@event, cancellationToken);

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
      .Create(notification, cancellationToken);

    var recipients = await notificationQueries.Recipients(notification);
    await notificationMutations.AddRecipients(recipients, cancellationToken);
  }

  private sealed record EventContent(
    string Message,
    string Exception,
    string? StackTrace
  );
}
