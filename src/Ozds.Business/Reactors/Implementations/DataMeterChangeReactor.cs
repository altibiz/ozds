using Ozds.Business.Caching;
using Ozds.Business.Models.Base;
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
  AuditableQueries auditableQueries,
  TimeQueries timeQueries,
  MeterCache meterCache,
  MeasurementLocationByMeterCache measurementLocationByMeterCache,
  MessengerByMeterCache messengerByMeterCache,
  MeasurementValidatorByMeterCache measurementValidatorByMeterCache
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task AfterStartAsync(
    CancellationToken cancellationToken)
  {
    var page = 0;
    var result = await auditableQueries
      .Read<MeterModel>(page, cancellationToken);
    while (result.Items.Count > 0)
    {
      await manager.EnsureInactivityMonitorJobs(
        result.Items.Select(
          x => new MeterInactivityMonitorDetails(
            x.Id,
            timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );

      result = await auditableQueries
        .Read<MeterModel>(
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
      .OfType<MeterModel>()
      .ToList();
    if (added.Count > 0)
    {
      await manager.EnsureInactivityMonitorJobs(
        added.Select(
          x => new MeterInactivityMonitorDetails(
            x.Id,
            timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );
    }

    var modified = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<MeterModel>()
      .ToList();
    if (modified.Count > 0)
    {
      await meterCache.TryUpdateAsync(modified, cancellationToken);
      foreach (var id in modified.Select(x => x.Id))
      {
        measurementLocationByMeterCache.TryRemove(id);
        messengerByMeterCache.TryRemove(id);
        measurementValidatorByMeterCache.TryRemove(id);
      }

      await manager.RescheduleInactivityMonitorJobs(
        modified.Select(
          x => new MeterInactivityMonitorDetails(
            x.Id,
            timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );
    }

    var removed = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Removed)
      .Select(x => x.Model)
      .OfType<MeterModel>()
      .ToList();
    if (removed.Count > 0)
    {
      await meterCache.TryRemoveAsync(removed, cancellationToken);
      foreach (var id in removed.Select(x => x.Id))
      {
        measurementLocationByMeterCache.TryRemove(id);
        messengerByMeterCache.TryRemove(id);
        measurementValidatorByMeterCache.TryRemove(id);
      }

      await manager.UnscheduleInactivityMonitorJobs(
        removed.Select(x => x.Id),
        cancellationToken
      );
    }
  }
}
