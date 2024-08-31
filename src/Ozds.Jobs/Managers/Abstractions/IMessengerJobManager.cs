namespace Ozds.Jobs.Manager.Abstractions;

public interface IMessengerJobManager : IJobManager
{
  public Task EnsureInactivityMonitorJob(string id, TimeSpan inactivityDuration);

  public Task RescheduleInactivityMonitorJob(string id, TimeSpan inactivityDuration);

  public Task UnscheduleInactivityMonitorJob(string id);
}
