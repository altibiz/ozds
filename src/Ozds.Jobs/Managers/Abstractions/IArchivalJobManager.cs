namespace Ozds.Jobs.Manager.Abstractions;

public interface IArchivalJobManager : IJobManager
{
  public Task EnsureDailyMeasurementDeletionJob(
    CancellationToken cancellationToken);

  public Task RescheduleDailyMeasurementDeletionJob(
    CancellationToken cancellationToken);

  public Task UnscheduleDailyMeasurementDeletionJob(
    CancellationToken cancellationToken);
}
