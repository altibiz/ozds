using System.Text.Json;
using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Reactors.Base;
using ErrorEventArgs = Ozds.Business.Observers.EventArgs.ErrorEventArgs;

namespace Ozds.Business.Reactors.Implementations;

public class ErrorReactor(
  IServiceProvider serviceProvider
) : Reactor<ErrorEventArgs, IErrorSubscriber, ErrorHandler>(serviceProvider)
{
}

public class ErrorHandler(
  ModelActivator activator,
  NotificationMutations notificationMutations,
  ReadonlyMutations readonlyMutations
) : Handler<ErrorEventArgs>
{
  public override async Task Handle(
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
    notification.Content =
      $"Exception: \n{eventArgs.Exception}"
      + $"\nStack trace: \n{eventArgs.Exception.StackTrace}\n";
    notification.EventId = @event.Id;
    notification.Topics = new HashSet<TopicModel>
    {
      TopicModel.All,
      TopicModel.Error
    };
    notification.Id = await notificationMutations
      .Create(notification, cancellationToken);
  }

  private sealed record EventContent(
    string Message,
    string Exception,
    string? StackTrace
  );
}
