using Ozds.Messaging.Sagas;

namespace Ozds.Messaging.Observers.Abstractions;

public interface
  INetworkUserInvoiceStatePublisher : IPublisher<
  INetworkUserInvoiceStateSubscriber>
{
  public void PublishRegistered(NetworkUserInvoiceState state);
}
