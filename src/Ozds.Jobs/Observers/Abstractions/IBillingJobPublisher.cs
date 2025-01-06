using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IBillingJobPublisher : IPublisher<
  IBillingJobSubscriber,
  BillingJobEventArgs>
{
}
