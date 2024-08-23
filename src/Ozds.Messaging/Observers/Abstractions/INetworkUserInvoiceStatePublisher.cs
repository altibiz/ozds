using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers.Abstractions;

public interface INetworkUserInvoiceStatePublisher
{
  public void PublishRegistered(NetworkUserInvoiceState state);
}
