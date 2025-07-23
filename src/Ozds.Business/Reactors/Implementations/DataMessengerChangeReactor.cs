using Ozds.Business.Caching;
using Ozds.Business.Models.Base;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
using Ozds.Business.Reactors.Base;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors.Implementations;

public class DataMessengerChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataMessengerChangeHandler>(serviceProvider)
{
}

public class DataMessengerChangeHandler(
  IMessengerJobManager manager,
  AuditableQueries auditableQueries,
  TimeQueries timeQueries,
  MessengerCache messengerCache,
  MessengerByMeterCache messengerByMeterCache
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task AfterStartAsync(
    CancellationToken cancellationToken)
  {
    var page = 0;
    var result = await auditableQueries
      .Read<MessengerModel>(page, cancellationToken);
    while (result.Items.Count > 0)
    {
      await manager.EnsureInactivityMonitorJobs(
        result.Items.Select(x => new MessengerInactivityMonitorDetails(
          x.Id,
          timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );

      result = await auditableQueries
        .Read<MessengerModel>(++page, cancellationToken);
    }
  }

  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var added = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Added)
      .Select(x => x.Model)
      .OfType<MessengerModel>()
      .ToList();
    if (added.Count > 0)
    {
      await manager.EnsureInactivityMonitorJobs(
        added.Select(x => new MessengerInactivityMonitorDetails(
          x.Id,
          timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );
    }

    var modified = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<MessengerModel>()
      .ToList();
    if (modified.Count > 0)
    {
      await messengerCache.TryUpdateAsync(modified, cancellationToken);
      await messengerByMeterCache.TryUpdateAsync(modified, cancellationToken);
      await manager.RescheduleInactivityMonitorJobs(
        modified.Select(x => new MessengerInactivityMonitorDetails(
          x.Id,
          timeQueries.PeriodTimeSpan(x.MaxInactivityPeriod))),
        cancellationToken
      );
    }

    var removed = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Removed)
      .Select(x => x.Model)
      .OfType<MessengerModel>()
      .ToList();
    if (removed.Count > 0)
    {
      await messengerCache.TryRemoveAsync(removed, cancellationToken);
      foreach (var id in removed.Select(x => x.Id))
      {
        messengerByMeterCache.TryRemove(id);
      }
      await manager.UnscheduleInactivityMonitorJobs(
        removed.Select(x => x.Id),
        cancellationToken
      );
    }
  }
}
