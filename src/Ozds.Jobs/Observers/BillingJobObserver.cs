using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers;

public class BillingJobObserver : IBillingJobPublisher, IBillingJobSubscriber
{
  public void PublishInvoice(NetworkUserInvoiceEventArgs args)
  {
    Invoice?.Invoke(this, args);
  }

  public void SubscribeNetworkUserInvoiceCreate(
    EventHandler<NetworkUserInvoiceEventArgs> handler)
  {
    Invoice += handler;
  }

  public void UnsubscribeNetworkUserInvoiceCreate(
    EventHandler<NetworkUserInvoiceEventArgs> handler)
  {
    Invoice -= handler;
  }

  private event EventHandler<NetworkUserInvoiceEventArgs>? Invoice;
}
