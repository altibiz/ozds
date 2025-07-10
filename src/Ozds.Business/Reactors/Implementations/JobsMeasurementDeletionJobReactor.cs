using Microsoft.Extensions.Options;
using Ozds.Business.Mutations;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Options;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

public class JobsMeasurementDeletionJobReactor(
  IServiceProvider serviceProvider
) : Reactor<
  JobsArchivalJobEventArgs,
  IJobsArchivalJobSubscriber,
  JobsMeasurementDeletionJobHandler>(serviceProvider)
{
}

public class JobsMeasurementDeletionJobHandler(
  MeasurementMutations mutations,
  IArchivalJobManager manager,
  ClockQueries clockQueries,
  IOptions<OzdsBusinessOptions> options
) : Handler<JobsArchivalJobEventArgs>
{
  public override Task AfterStartAsync(CancellationToken cancellationToken)
  {
    return manager.EnsureDailyMeasurementDeletionJob(cancellationToken);
  }

  public override async Task Handle(
    JobsArchivalJobEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var dateFrom = clockQueries
      .Now()
      .Subtract(
        TimeSpan.FromSeconds(
          options.Value.Reactor.MeasurementDeletionJobIntervalSeconds));

    await mutations.DeleteMeasurementsOlderThan(
      dateFrom,
      cancellationToken
    );
  }
}
