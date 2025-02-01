using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;

namespace Ozds.Business.Reactors.Implementations;

// TODO: remove db context references

public class JobsMessengerJobReactor(
  IServiceProvider serviceProvider
) : Reactor<
  JobsMessengerJobEventArgs,
  IJobsMessengerJobSubscriber,
  JobsMessengerJobHandler>(serviceProvider)
{
}

public class JobsMessengerJobHandler(
  IDbContextFactory<DataDbContext> factory,
  IHostEnvironment environment,
  ModelActivator activator,
  ModelEntityConverter converter,
  AuditableQueries auditableQueries
) : Handler<JobsMessengerJobEventArgs>
{
  private static readonly JsonSerializerOptions
    EventContentSerializationOptions = new()
    {
      WriteIndented = true
    };

  public override async Task Handle(
    JobsMessengerJobEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var messenger = await auditableQueries
      .ReadSingle<MessengerModel>(eventArgs.Id, cancellationToken);
    if (messenger is null)
    {
      return;
    }

    var lastPushEvent = await context.Events
      .OfType<MessengerEventEntity>()
      .Where(x => x.MessengerId == messenger.Id)
      .Where(x => x.Categories.Contains(CategoryEntity.MessengerPush))
      .OrderByDescending(x => x.Timestamp)
      .FirstOrDefaultAsync(cancellationToken);

    var notification = activator.Activate<MessengerNotificationModel>();
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
    await context.SaveChangesAsync(cancellationToken);
    notification.Id = notificationEntity.Id;
  }
}
