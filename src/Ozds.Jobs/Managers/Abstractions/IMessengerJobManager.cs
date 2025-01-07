namespace Ozds.Jobs.Manager.Abstractions;

public interface IMessengerJobManager : IJobManager
{
  public Task EnsureInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration,
    CancellationToken cancellationToken);

  public Task RescheduleInactivityMonitorJob(
    string id,
    TimeSpan inactivityDuration,
    CancellationToken cancellationToken);

  public Task UnscheduleInactivityMonitorJob(
    string id,
    CancellationToken cancellationToken);
}
