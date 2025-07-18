using System.Text;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

// TODO: remove db context references

public class DataNotificationRecipientChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataNotificationRecipientChangeHandler>(serviceProvider)
{
}

public class DataNotificationRecipientChangeHandler(
  ModelQueries modelQueries,
  IEmailSender sender
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var recipients = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<NotificationRecipientModel>()
      .ToList();
    if (recipients.Count == 0)
    {
      return;
    }

    var notifications = await modelQueries.ReadByIds<NotificationModel>(
      recipients.Select(x => x.NotificationId),
      cancellationToken);

    var representatives = await modelQueries.ReadByIds<RepresentativeModel>(
      recipients.Select(x => x.RepresentativeId),
      cancellationToken
    );

    var groups = recipients
      .GroupBy(x => x.NotificationId)
      .Select(
        x => new
        {
          Notification = notifications.FirstOrDefault(y => y.Id == x.Key),
          Recipients = x.ToList(),
          Representatives = x
            .Select(
              y => representatives
                .FirstOrDefault(z => z.Id == y.RepresentativeId))
            .OfType<RepresentativeModel>()
            .ToList()
        });

    var emails = new List<EmailMessage>();
    foreach (var group in groups)
    {
      if (group.Notification is null)
      {
        continue;
      }

      var notification = group.Notification;
      var titleBuilder = new StringBuilder(
        $"[{nameof(Ozds)}]: {notification.Title}");
      if (notification.Topics.Count > 0)
      {
        var topics = notification.Topics.Select(x => x.ToTitle());
        titleBuilder.Append(" ( ");
        titleBuilder.Append(string.Join(", ", topics));
        titleBuilder.Append(" )");
      }

      emails.AddRange(
        group.Representatives.Select(
          representative => new EmailMessage(
            representative.PhysicalPerson.Name,
            representative.PhysicalPerson.Email,
            titleBuilder.ToString(),
            $"""
              <p style="font-size: large;">
                <a href="app/hr/notification/{notification.Id}">
                  {notification.Summary}
                </a>
              </p>
              <p style="font-size: small;">
                <pre style="overflow-wrap: break-word;">
                  {notification.Content}
                </pre>
              </p>
            """
          )
        ));
    }

    await sender.SendBulkAsync(emails);
  }
}
