namespace Ozds.Jobs.Manager.Abstractions;

public interface INetworkUserBillingJobManager : IJobManager
{
  public Task EnsureMonthlyBillingJob(string networkUserId);

  public Task RescheduleMonthlyBillingJob(string networkUserId);

  public Task UnscheduleMonthlyBillingJob(string networkUserId);
}
