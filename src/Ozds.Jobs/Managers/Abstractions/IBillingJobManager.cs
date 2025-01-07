namespace Ozds.Jobs.Manager.Abstractions;

public interface IBillingJobManager : IJobManager
{
  public Task EnsureMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken);

  public Task RescheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken);

  public Task UnscheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken);
}
