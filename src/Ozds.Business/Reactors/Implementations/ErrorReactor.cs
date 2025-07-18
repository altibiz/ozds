using System.Text.Json;
using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Queries;
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
  ModelMutations mutations,
  ClockQueries clock
) : Handler<ErrorEventArgs>
{
  public override async Task Handle(
    ErrorEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var now = clock.Timestamp();

    var content = new EventContent(
      eventArgs.Message,
      eventArgs.Exception.ToString(),
      eventArgs.Exception.StackTrace
    );

    var @event = activator.Activate<SystemEventModel>();
    @event.Title = "Exception";
    @event.Timestamp = now;
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
    await mutations.Create(@event, cancellationToken);

    var notification = activator.Activate<SystemNotificationModel>();
    notification.Title = "Exception";
    notification.Summary = content.Message;
    notification.Timestamp = now;
    notification.Content =
      $"Exception: {Environment.NewLine}{eventArgs.Exception}"
      + $"{Environment.NewLine}Stack trace: "
      + $"{Environment.NewLine}{eventArgs.Exception.StackTrace}"
      + Environment.NewLine;
    notification.EventId = @event.Id;
    notification.Topics = new HashSet<TopicModel>
    {
      TopicModel.All,
      TopicModel.Error
    };
    await mutations.Create(notification, cancellationToken);
  }

  private sealed record EventContent(
    string Message,
    string Exception,
    string? StackTrace
  );
}
