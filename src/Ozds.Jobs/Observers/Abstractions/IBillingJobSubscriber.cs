using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IBillingJobSubscriber : ISubscriber<IBillingJobPublisher>
{
  public void SubscribeNetworkUserInvoiceCreate(
    EventHandler<NetworkUserInvoiceEventArgs> handler);

  public void UnsubscribeNetworkUserInvoiceCreate(
    EventHandler<NetworkUserInvoiceEventArgs> handler);
}
