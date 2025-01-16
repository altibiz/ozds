using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation;
using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Conversion.Joins;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
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

namespace Ozds.Business.Reactors;

public class MessagingNetworkUserInvoiceStateReactor(
  IServiceProvider serviceProvider
) : Reactor<
  MessagingNetworkUserInvoiceStateEventArgs,
  IMessagingNetworkUserInvoiceStateSubscriber,
  MessagingNetworkUserInvoiceStateHandler>(serviceProvider)
{
}

public class MessagingNetworkUserInvoiceStateHandler(
  AgnosticModelActivator activator,
  AgnosticModelEntityConverter converter,
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
            $"{localizer["Invoice"]} \"{invoice.Title}\""
            + $" {localizer["issued"]}";
          notification.Content =
            $"{localizer["Invoice url is"]} 'invoices/{invoice.Id}'";

          return notification.ToEntity();
        })
      .ToList();
    context.AddRange(notifications);
    await context.SaveChangesAsync(cancellationToken);

    var notificationRecipients = notifications
      .SelectMany(
        notification => recipients
          .Select(
            recipient =>
              new NotificationRecipientModel
              {
                NotificationId = notification.Id,
                RepresentativeId = recipient.Id
              }.ToEntity()
          )
      );
    context.AddRange(notificationRecipients);
    await context.SaveChangesAsync(cancellationToken);
  }
}
