using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Ozds.Business.Activation;
using Ozds.Business.Conversion;
using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Conversion.Joins;
using Ozds.Business.Localization.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Models.Joins;
using Ozds.Business.Mutations;
using Ozds.Business.Reactors.Abstractions;
using Ozds.Data.Context;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;
using Ozds.Data.Extensions;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Business.Reactors;

public class NetworkUserInvoiceStateReactor(
  INetworkUserInvoiceStateSubscriber subscriber,
  IServiceScopeFactory serviceScopeFactory
) : BackgroundService, IReactor
{
  private readonly Channel<NetworkUserInvoiceStateEventArgs> channel =
    Channel.CreateUnbounded<NetworkUserInvoiceStateEventArgs>();

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    subscriber.SubscribeRegistered(OnRegistered);
    return base.StartAsync(cancellationToken);
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    subscriber.UnsubscribeRegistered(OnRegistered);
    return base.StopAsync(cancellationToken);
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    await foreach (var state in channel.Reader.ReadAllAsync(stoppingToken))
    {
      await using var scope = serviceScopeFactory.CreateAsyncScope();
      await Handle(scope.ServiceProvider, state);
    }
  }

  public void OnRegistered(
    object? sender,
    NetworkUserInvoiceStateEventArgs state)
  {
    channel.Writer.TryWrite(state);
  }

  private static async Task Handle(
    IServiceProvider serviceProvider,
    NetworkUserInvoiceStateEventArgs eventArgs)
  {
    var mutations = serviceProvider
      .GetRequiredService<OzdsNetworkUserInvoiceMutations>();
    await mutations.UpdateBillId(
      eventArgs.State.NetworkUserInvoiceId, eventArgs.State.BillId!);

    var context = serviceProvider.GetRequiredService<DataDbContext>();
    var converter =
      serviceProvider.GetRequiredService<AgnosticModelEntityConverter>();
    var localizer = serviceProvider.GetRequiredService<ILocalizer>();

    var invoices = (await context.NetworkUserInvoices
        .Where(context.PrimaryKeyEquals<NetworkUserInvoiceEntity>(
          eventArgs.State.NetworkUserInvoiceId))
        .ToListAsync())
      .Select(converter.ToModel<NetworkUserInvoiceModel>);

    var networkUserIds = invoices
      .Select(x => x.NetworkUserId)
      .ToList();

    var recipients = await context.NetworkUserRepresentatives
      .Where(context.ForeignKeyIn<NetworkUserRepresentativeEntity>(
        nameof(NetworkUserRepresentativeEntity.NetworkUser),
        networkUserIds
      ))
      .Join(
        context.Representatives.Where(r =>
          r.Topics.Contains(TopicEntity.All)
          || r.Topics.Contains(TopicEntity.NetworkUserInvoiceState)),
        context.ForeignKeyOf<NetworkUserRepresentativeEntity>(
          nameof(NetworkUserRepresentativeEntity.Representative)),
        context.PrimaryKeyOf<RepresentativeEntity>(),
        (_, representative) => representative
      )
      .ToListAsync();

    var notifications = invoices.Select(invoice =>
    {
      var notification = NetworkUserInvoiceNotificationModelActivator.New();
      notification.InvoiceId = invoice.Id;
      notification.Topics =
      [
        TopicModel.All
      ];
      notification.Summary = $"{localizer["Invoice"]} \"{invoice.Title}\" {localizer["issued"]}";
      notification.Content =
        $"{localizer["Invoice url is"]} 'invoices/{invoice.Id}'";

      return notification.ToEntity();
    });
    context.AddRange(notifications);
    await context.SaveChangesAsync();

    var notificationRecipients = notifications.SelectMany(notification =>
      recipients.Select(recipient =>
          new NotificationRecipientModel
          {
            NotificationId = notification.Id,
            RepresentativeId = recipient.Id
          }.ToEntity()
      )
    );
    context.AddRange(notificationRecipients);
    await context.SaveChangesAsync();
  }
}
