using System.Text;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Joins;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Business.Reactors;

// TODO: paging when fetching

public class DataNotificationChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataNotificationChangeHandler>(serviceProvider)
{
}

public class DataNotificationChangeHandler(
  IDbContextFactory<DataDbContext> factory,
  INotificationCreatedPublisher publisher,
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

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    var notifications = await context.Notifications
      .Where(
        context.PrimaryKeyIn<NotificationEntity>(
          recipients
            .Select(x => x.NotificationId)
            .ToList()))
      .ToListAsync(cancellationToken);

    var representatives = await context.Representatives
      .Where(
        context.PrimaryKeyIn<RepresentativeEntity>(
          recipients
            .Select(x => x.RepresentativeId)
            .ToList()))
      .ToListAsync(cancellationToken);

    var groups = recipients
      .GroupBy(x => x.NotificationId)
      .Select(
        x => new
        {
          Notification = notifications
            .FirstOrDefault(y => y.Id == x.Key)
            ?.ToModel(),
          Recipients = x.ToList(),
          Representatives = x
            .Select(
              y => representatives
                .FirstOrDefault(z => z.Id == y.RepresentativeId))
            .OfType<RepresentativeEntity>()
            .Select(z => z.ToModel())
            .ToList()
        });

    var emails = new List<EmailMessage>();
    foreach (var group in groups)
    {
      if (group.Notification is null)
      {
        continue;
      }

      foreach (var recipient in group.Recipients)
      {
        publisher.Publish(
          new NotificationCreatedEventArgs
          {
            Notification = group.Notification,
            Recipient = recipient
          });
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
