using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Business.Reactors.Base;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

public class DataMeterChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataMeterChangeHandler>(serviceProvider)
{
}

public class DataMeterChangeHandler(
  IMeterJobManager manager,
  TrackableQueries trackableQueries,
  TimeQueries timeQueries
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task AfterStartAsync(
    CancellationToken cancellationToken)
  {
    var page = 0;
    var result = await trackableQueries
      .Read<IMeter>(page, cancellationToken);
    while (result.Items.Count > 0)
    {
      await manager.EnsureInactivityMonitorJobs(
        result.Items.Select(x => new MeterInactivityMonitorDetails(
          x.Id,
          timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );

      result = await trackableQueries
        .Read<IMeter>(
          ++page,
          cancellationToken,
          QueryConstants.DefaultReactorPageCount);
    }
  }

  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var added = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<IMeter>()
      .ToList();
    if (added.Count > 0)
    {
      await manager.EnsureInactivityMonitorJobs(
        added.Select(x => new MeterInactivityMonitorDetails(
          x.Id,
          timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );
    }

    var modified = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<IMeter>()
      .ToList();
    if (modified.Count > 0)
    {
      await manager.RescheduleInactivityMonitorJobs(
        modified.Select(x => new MeterInactivityMonitorDetails(
          x.Id,
          timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );
    }

    var removed = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Removed)
      .Select(x => x.Model)
      .OfType<IMeter>()
      .ToList();
    if (removed.Count > 0)
    {
      await manager.UnscheduleInactivityMonitorJobs(
        removed.Select(x => x.Id),
        cancellationToken
      );
    }
  }
}
