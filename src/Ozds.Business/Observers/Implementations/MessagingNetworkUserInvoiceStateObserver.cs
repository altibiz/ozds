using Ozds.Business.Conversion;
using Ozds.Business.Models;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Messaging.Observers.Abstractions;
using Ozds.Messaging.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class MessagingNetworkUserInvoiceStateRelay(
  IServiceProvider serviceProvider,
  INetworkUserInvoiceStateSubscriber subscriber
) : Relay<
  NetworkUserInvoiceStateEventArgs,
  MessagingNetworkUserInvoiceStateEventArgs,
  MessagingNetworkUserInvoiceStatePipe>(
  serviceProvider
), IMessagingNetworkUserInvoiceStateSubscriber
{
  protected override void SubscribeIn(
    EventHandler<NetworkUserInvoiceStateEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<NetworkUserInvoiceStateEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class MessagingNetworkUserInvoiceStatePipe(
  ModelEntityConverter modelEntityConverter
)
  : IPipe<
    NetworkUserInvoiceStateEventArgs,
    MessagingNetworkUserInvoiceStateEventArgs>
{
  public Task<MessagingNetworkUserInvoiceStateEventArgs> Transform(
    NetworkUserInvoiceStateEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var modelEventArgs = new MessagingNetworkUserInvoiceStateEventArgs
    {
      State = modelEntityConverter
        .ToModel<NetworkUserInvoiceStateModel>(eventArgs.State)
    };
    return Task.FromResult(modelEventArgs);
  }
}
