using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;

namespace Ozds.Business.Reactors.Implementations;

public class MessagingNetworkUserInvoiceStateReactor(
  IServiceProvider serviceProvider
) : Reactor<
  MessagingNetworkUserInvoiceStateEventArgs,
  IMessagingNetworkUserInvoiceStateSubscriber,
  MessagingNetworkUserInvoiceStateHandler>(serviceProvider)
{
}

public class MessagingNetworkUserInvoiceStateHandler(
  ModelActivator activator,
  ModelEntityConverter converter,
  IDbContextFactory<DataDbContext> factory,
  ILocalizer localizer
) : Handler<MessagingNetworkUserInvoiceStateEventArgs>
{
  public override async Task Handle(
    MessagingNetworkUserInvoiceStateEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);

    await context.NetworkUserInvoices
      .Where(context
        .PrimaryKeyEquals<NetworkUserInvoiceEntity>(
          eventArgs.State.NetworkUserInvoiceId))
      .ExecuteUpdateAsync(
        s => s.SetProperty(x => x.BillId, eventArgs.State.BillId),
        cancellationToken);

    var invoices = (await context.NetworkUserInvoices
        .Where(
          context.PrimaryKeyEquals<NetworkUserInvoiceEntity>(
            eventArgs.State.NetworkUserInvoiceId))
        .ToListAsync(cancellationToken))
      .Select(converter.ToModel<NetworkUserInvoiceModel>);

    var networkUserIds = invoices
      .Select(x => x.NetworkUserId)
      .ToList();

    var recipients = await context.NetworkUserRepresentatives
      .Where(
        context.ForeignKeyIn<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.NetworkUser),
          networkUserIds
        ))
      .Join(
        context.Representatives.Where(
          r =>
            r.Topics.Contains(TopicEntity.All)
            || r.Topics.Contains(TopicEntity.NetworkUserInvoiceState)),
        context.ForeignKeyOf<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.Representative)),
        context.PrimaryKeyOf<RepresentativeEntity>(),
        (_, representative) => representative
      )
      .ToListAsync(cancellationToken);

    var notifications = invoices
      .Select(
        invoice =>
        {
          var notification = activator
            .Activate<NetworkUserInvoiceNotificationModel>();
          notification.InvoiceId = invoice.Id;
          notification.Topics =
          [
            TopicModel.All,
            TopicModel.NetworkUserInvoiceState
          ];
          notification.Summary =
            $"{localizer.Translate("Invoice")} \"{invoice.Title}\""
            + $" {localizer.Translate("issued")}";
          notification.Content =
            $"{localizer.Translate("Invoice url is")} 'invoices/{invoice.Id}'";

          return converter
            .ToEntity<NetworkUserInvoiceNotificationEntity>(notification);
        })
      .ToList();
    context.AddRange(notifications);
    await context.SaveChangesAsync(cancellationToken);

    var notificationRecipients = notifications
      .SelectMany(
        notification => recipients
          .Select(
            recipient =>
              converter.ToEntity<NotificationRecipientEntity>(
                new NotificationRecipientModel
                {
                  NotificationId = notification.Id,
                  RepresentativeId = recipient.Id
                })));
    context.AddRange(notificationRecipients);
    await context.SaveChangesAsync(cancellationToken);
  }
}
