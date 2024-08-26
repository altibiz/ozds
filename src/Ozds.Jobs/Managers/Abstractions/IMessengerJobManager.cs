namespace Ozds.Jobs.Manager.Abstractions;

public interface IMessengerJobManager : IJobManager
{
  public Task EnsureInactivityMonitorJob(string id, TimeSpan inactivityDuration);

  public Task RefreshInactivityMonitorJob(string id, TimeSpan inactivityDuration);
}
