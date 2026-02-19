using Ozds.Business.Activation;
using Ozds.Business.Models;
using Ozds.Business.Models.Enums;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors.Implementations;

public class MessagingNetworkUserInvoiceStateReactor(
  IServiceProvider serviceProvider
)
  : Reactor<
    MessagingNetworkUserInvoiceStateEventArgs,
    IMessagingNetworkUserInvoiceStateSubscriber,
    MessagingNetworkUserInvoiceStateHandler
  >(serviceProvider) { }

public class MessagingNetworkUserInvoiceStateHandler(
  ModelActivator activator,
  LocalizationQueries localizationQueries,
  NetworkUserInvoiceMutations invoiceMutations,
  IdentifiableQueries identifiableQueries,
  ModelMutations modelMutations
) : Handler<MessagingNetworkUserInvoiceStateEventArgs>
{
  public override async Task Handle(
    MessagingNetworkUserInvoiceStateEventArgs eventArgs,
    CancellationToken cancellationToken
  )
  {
    if (!eventArgs.State.Approved || eventArgs.State.BillId is null)
    {
      return;
    }

    await invoiceMutations.UpdateBillId(
      eventArgs.State.NetworkUserInvoiceId,
      eventArgs.State.BillId,
      cancellationToken
    );

    var invoice = await identifiableQueries.ReadById<NetworkUserInvoiceModel>(
      eventArgs.State.NetworkUserInvoiceId,
      cancellationToken
    );
    if (invoice is null)
    {
      return;
    }

    var networkUser = await identifiableQueries.ReadById<NetworkUserModel>(
      invoice.NetworkUserId,
      cancellationToken
    );
    if (networkUser is null)
    {
      return;
    }

    var notification =
      activator.Activate<NetworkUserInvoiceNotificationModel>();
    notification.InvoiceId = invoice.Id;
    notification.Topics = [TopicModel.All, TopicModel.NetworkUserInvoiceState];
    notification.Summary =
      localizationQueries.Translate(
        localizationQueries.CroatianCulture,
        "Invoice"
      )
      + $" \"{invoice.Title}\" "
      + localizationQueries.Translate(
        localizationQueries.CroatianCulture,
        "issued"
      );
    notification.Content =
      localizationQueries.Translate(
        localizationQueries.CroatianCulture,
        "Invoice url is"
      ) + $" 'invoices/{invoice.Id}'";
    await modelMutations.Create(notification, cancellationToken);
  }
}
