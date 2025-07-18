using Ozds.Business.Models.Base;
using Ozds.Business.Models.Joins;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors.Implementations;

public class DataNotificationChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataNotificationChangeHandler>(serviceProvider)
{
}

public class DataNotificationChangeHandler(
  NotificationQueries notificationQueries,
  ModelMutations modelMutations,
  INotificationRecipientCreatedPublisher notificationCreatedPublisher
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var notifications = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<NotificationModel>()
      .ToList();
    if (notifications.Count == 0)
    {
      return;
    }

    var recipients = new List<NotificationRecipientModel>();
    // FIXME: N + 1
    foreach (var notification in notifications)
    {
      var notificationRecipients = await notificationQueries
        .Recipients(notification);
      recipients.AddRange(notificationRecipients);
      foreach (var notificationRecipient in notificationRecipients)
      {
        var notificationCreatedEventArgs =
          new NotificationRecipientCreatedEventArgs
          {
            Notification = notification,
            Recipient = notificationRecipient
          };
        notificationCreatedPublisher.Publish(notificationCreatedEventArgs);
      }
    }

    await modelMutations.Create(recipients, cancellationToken);
  }
}
