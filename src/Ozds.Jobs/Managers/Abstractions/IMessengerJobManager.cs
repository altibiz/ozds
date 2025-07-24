namespace Ozds.Jobs.Manager.Abstractions;

public record MessengerInactivityMonitorDetails(
  string MessengerId,
  TimeSpan InactivityDuration
);

public interface IMessengerJobManager : IJobManager
{
  public Task EnsureInactivityMonitorJob(
    MessengerInactivityMonitorDetails details,
    CancellationToken cancellationToken);

  public Task EnsureInactivityMonitorJobs(
    IEnumerable<MessengerInactivityMonitorDetails> details,
    CancellationToken cancellationToken);

  public Task RescheduleInactivityMonitorJob(
    MessengerInactivityMonitorDetails details,
    CancellationToken cancellationToken);

  public Task RescheduleInactivityMonitorJobs(
    IEnumerable<MessengerInactivityMonitorDetails> details,
    CancellationToken cancellationToken);

  public Task UnscheduleInactivityMonitorJob(
    string id,
    CancellationToken cancellationToken);

  public Task UnscheduleInactivityMonitorJobs(
    IEnumerable<string> ids,
    CancellationToken cancellationToken);
}
