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

public class JobsMeterInactivityJobReactor(IServiceProvider serviceProvider)
  : Reactor<
    JobsMeterJobEventArgs,
    IJobsMeterJobSubscriber,
    JobsMeterInactivityJobHandler
  >(serviceProvider) { }

public class JobsMeterInactivityJobHandler(
  EventQueries eventQueries,
  ModelMutations modelMutations,
  IHostEnvironment environment,
  ModelActivator activator,
  TrackableQueries trackableQueries
) : Handler<JobsMeterJobEventArgs>
{
  private static readonly JsonSerializerOptions EventContentSerializationOptions =
    new() { WriteIndented = true };

  public override async Task Handle(
    JobsMeterJobEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    var meter = await trackableQueries.ReadById<IMeter>(
      eventArgs.Id,
      cancellationToken
    );
    if (meter is null or { MessengerId: null })
    {
      return;
    }

    var messenger = await trackableQueries.ReadById<IMessenger>(
      meter.MessengerId,
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

    var notification = activator.Activate<MeterNotificationModel>();
    notification.MeterId = meter.Id;
    notification.Topics =
    [
      TopicModel.All,
      TopicModel.Meter,
      TopicModel.MeterInactivity,
    ];
    notification.Title = "Meter is inactive";
    notification.Summary = $"Meter \"{meter.Title}\" is inactive";
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
      builder.AppendLine($"Meter: \"{meter.Title}\"");
      builder.AppendLine($"Last pushed at: {lastPushEvent.Timestamp}");
      builder.AppendLine($"Last push details: {lastPushEventDetails}");
    }

    notification.Content = builder.ToString();

    await modelMutations.Create(notification, cancellationToken);
  }
}
