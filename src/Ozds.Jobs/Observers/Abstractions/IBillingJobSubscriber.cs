using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Abstractions;

public interface IBillingJobSubscriber : ISubscriber<
  IBillingJobPublisher,
  BillingJobEventArgs>
{
}
