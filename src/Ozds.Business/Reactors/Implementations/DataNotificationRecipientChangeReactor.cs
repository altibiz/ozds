using System.Text;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
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
  IDbContextFactory<DataDbContext> factory,
  IEmailSender sender,
  ModelEntityConverter converter
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
            .FirstOrDefault(y => y.Id == x.Key) is { } notification
            ? converter.ToModel<NotificationModel>(notification)
            : null,
          Recipients = x.ToList(),
          Representatives = x
            .Select(
              y => representatives
                .FirstOrDefault(z => z.Id == y.RepresentativeId))
            .OfType<RepresentativeEntity>()
            .Select(z => converter.ToModel<RepresentativeModel>(z))
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
