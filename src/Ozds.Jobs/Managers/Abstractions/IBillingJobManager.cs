namespace Ozds.Jobs.Manager.Abstractions;

public interface IBillingJobManager : IJobManager
{
  public Task EnsureMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  );

  public Task EnsureMonthlyBillingJobs(
    IEnumerable<string> networkUserIds,
    CancellationToken cancellationToken
  );

  public Task RescheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  );

  public Task RescheduleMonthlyBillingJobs(
    IEnumerable<string> networkUserIds,
    CancellationToken cancellationToken
  );

  public Task UnscheduleMonthlyBillingJob(
    string networkUserId,
    CancellationToken cancellationToken
  );

  public Task UnscheduleMonthlyBillingJobs(
    IEnumerable<string> networkUserIds,
    CancellationToken cancellationToken
  );
}
