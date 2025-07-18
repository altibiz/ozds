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
      foreach (var messenger in result.Items)
      {
        await manager.EnsureInactivityMonitorJob(
          messenger.Id,
          timeQueries.PeriodTimeSpan(messenger.MaxInactivityPeriod),
          cancellationToken
        );
      }

      result = await auditableQueries
        .Read<MessengerModel>(++page, cancellationToken);
    }
  }

  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var entry in eventArgs.Models)
    {
      if (entry.Model is not MessengerModel messenger)
      {
        continue;
      }

      if (entry.State is DataModelChangedState.Added)
      {
        await manager.EnsureInactivityMonitorJob(
          messenger.Id,
          timeQueries.PeriodTimeSpan(messenger.MaxInactivityPeriod),
          cancellationToken
        );
      }

      if (entry.State is DataModelChangedState.Removed)
      {
        await messengerCache.TryRemoveAsync(messenger, cancellationToken);
        await messengerByMeterCache.TryRemoveAsync(
          messenger, cancellationToken);
        await manager.UnscheduleInactivityMonitorJob(
          messenger.Id,
          cancellationToken);
      }

      if (entry.State is DataModelChangedState.Modified)
      {
        await messengerCache.TryUpdateAsync(messenger, cancellationToken);
        await messengerByMeterCache.TryUpdateAsync(
          messenger, cancellationToken);
        await manager.RescheduleInactivityMonitorJob(
          messenger.Id,
          timeQueries.PeriodTimeSpan(messenger.MaxInactivityPeriod),
          cancellationToken
        );
      }
    }
  }
}
