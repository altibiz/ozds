using System.Text;
using System.Text.Json;
using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors.Implementations;

// TODO: remove db context references

public class JobsMessengerInactivityJobReactor(IServiceProvider serviceProvider)
  : Reactor<
    JobsMessengerJobEventArgs,
    IJobsMessengerJobSubscriber,
    JobsMessengerInactivityJobHandler
  >(serviceProvider) { }

public class JobsMessengerInactivityJobHandler(
  EventQueries eventQueries,
  ModelMutations modelMutations,
  TrackableQueries trackable,
  IHostEnvironment environment,
  ModelActivator activator
) : Handler<JobsMessengerJobEventArgs>
{
  private static readonly JsonSerializerOptions EventContentSerializationOptions =
    new() { WriteIndented = true };

  public override async Task Handle(
    JobsMessengerJobEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    var messenger = await trackable.ReadById<IMessenger>(
      eventArgs.Id,
      cancellationToken
    );
    if (messenger is null)
    {
      return;
    }

    var lastPushEvent = await eventQueries.ReadLastByMessengerId(
      messenger.Id,
      cancellationToken
    );

    var notification = activator.Activate<MessengerNotificationModel>();
    notification.MessengerId = messenger.Id;
    notification.Topics =
    [
      TopicModel.All,
      TopicModel.Messenger,
      TopicModel.MessengerInactivity,
    ];
    notification.Title = "Messenger is inactive";
    notification.Summary = $"Messenger \"{messenger.Title}\" is inactive";
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
      builder.AppendLine("Messenger never pushed");
    }
    else
    {
      var lastPushEventDetails = JsonSerializer.Serialize(
        lastPushEvent.Content,
        EventContentSerializationOptions
      );
      builder.AppendLine($"Messenger: \"{messenger.Title}\"");
      builder.AppendLine($"Last pushed at: {lastPushEvent.Timestamp}");
      builder.AppendLine($"Last push details: {lastPushEventDetails}");
    }

    notification.Content = builder.ToString();

    await modelMutations.Create(notification, cancellationToken);
  }
}
