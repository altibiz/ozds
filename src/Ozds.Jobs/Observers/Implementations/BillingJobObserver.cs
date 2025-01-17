using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.Base;
using Ozds.Jobs.Observers.EventArgs;

namespace Ozds.Jobs.Observers.Implementations;

public class BillingJobObserver :
  Observer<BillingJobEventArgs>,
  IBillingJobPublisher,
  IBillingJobSubscriber
{
}
