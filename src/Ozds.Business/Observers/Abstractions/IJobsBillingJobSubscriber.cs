using Ozds.Business.Observers.EventArgs;

namespace Ozds.Business.Observers.Abstractions;

public interface IJobsBillingJobSubscriber
  : ISubscriber<JobsBillingJobEventArgs>
{
}
