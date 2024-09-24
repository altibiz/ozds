using System.Text;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Joins;
using Ozds.Business.Models.Enums;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;
using Ozds.Email.Options;
using Ozds.Email.Sender.Abstractions;

namespace Ozds.Business.Workers;

// TODO: paging when fetching

public class NotificationEmailSenderReactor(
  IServiceScopeFactory serviceScopeFactory,
  IEntityChangesSubscriber subscriber
) : BackgroundService, IReactor
{
  private readonly Channel<EntitiesChangedEventArgs> channel =
    Channel.CreateUnbounded<EntitiesChangedEventArgs>();

  public override async Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeEntitiesChanged(OnChanged);

    await base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribeEntitiesChanged(OnChanged);

    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var eventArgs in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, eventArgs);
    }
  }

  private void OnChanged(object? sender, EntitiesChangedEventArgs eventArgs)
  {
    channel.Writer.TryWrite(eventArgs);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    EntitiesChangedEventArgs eventArgs)
  {
    var recipients = eventArgs.Entities
      .Where(x => x.State == EntityChangedState.Added)
      .Select(x => x.Entity)
      .OfType<NotificationRecipientEntity>()
      .Select(x => x.ToModel())
      .ToList();
    if (recipients.Count == 0)
    {
      return;
    }

    var sender = serviceProvider.GetRequiredService<IEmailSender>();
    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var emailOptions =
      serviceProvider.GetRequiredService<IOptions<OzdsEmailOptions>>();

    var notifications = await context.Notifications
      .Where(
        context.PrimaryKeyIn<NotificationEntity>(
          recipients
            .Select(x => x.NotificationId)
            .ToList()))
      .ToListAsync();

    var representatives = await context.Representatives
      .Where(
        context.PrimaryKeyIn<RepresentativeEntity>(
          recipients
            .Select(x => x.RepresentativeId)
            .ToList()))
      .ToListAsync();

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
                <a href="{
                      emailOptions.Value.Host
                    }/app/hr/notification/{
                      notification.Id
                    }">
                  {
                          notification.Summary
                        }
                </a>
              </p>
              <p style="font-size: small;">
                <pre style="overflow-wrap: break-word;">
                  {
                          notification.Content
                        }
                </pre>
              </p>
            """
          )
        ));
    }

    await sender.SendBulkAsync(emails);
  }
}
