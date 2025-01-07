using Ozds.Jobs.Observers.Abstractions;
using Ozds.Jobs.Observers.EventArgs;
using Quartz;

namespace Ozds.Jobs;

public class NetworkUserMonthlyBillingJob(
  IBillingJobPublisher messengerJobPublisher
) : IJob
{
  public string NetworkUserId { get; set; } = default!;

  public Task Execute(IJobExecutionContext context)
  {
    messengerJobPublisher.Publish(
      new BillingJobEventArgs
      {
        NetworkUserId = NetworkUserId
      });

    return Task.CompletedTask;
  }
}
