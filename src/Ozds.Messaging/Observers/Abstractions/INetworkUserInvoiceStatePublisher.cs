using Ozds.Messaging.Entities;

namespace Ozds.Messaging.Observers.Abstractions;

public interface
  INetworkUserInvoiceStatePublisher : IPublisher<
  INetworkUserInvoiceStateSubscriber>
{
  public void PublishRegistered(NetworkUserInvoiceStateEntity state);
}
