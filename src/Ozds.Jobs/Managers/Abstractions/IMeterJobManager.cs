namespace Ozds.Jobs.Manager.Abstractions;

public record MeterInactivityMonitorDetails(
  string MeterId,
  TimeSpan InactivityDuration
);

public interface IMeterJobManager : IJobManager
{
  public Task EnsureInactivityMonitorJob(
    MeterInactivityMonitorDetails details,
    CancellationToken cancellationToken);

  public Task EnsureInactivityMonitorJobs(
    IEnumerable<MeterInactivityMonitorDetails> details,
    CancellationToken cancellationToken);

  public Task RescheduleInactivityMonitorJob(
    MeterInactivityMonitorDetails details,
    CancellationToken cancellationToken);

  public Task RescheduleInactivityMonitorJobs(
    IEnumerable<MeterInactivityMonitorDetails> details,
    CancellationToken cancellationToken);

  public Task UnscheduleInactivityMonitorJob(
    string id,
    CancellationToken cancellationToken);

  public Task UnscheduleInactivityMonitorJobs(
    IEnumerable<string> ids,
    CancellationToken cancellationToken);
}
