using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries.Agnostic;
using Ozds.Business.Reactors.Base;
using Ozds.Jobs.Manager.Abstractions;

namespace Ozds.Business.Reactors;

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
  AuditableQueries auditableQueries
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task AfterStartAsync(
    CancellationToken cancellationToken)
  {
    var page = 1;
    var result = await auditableQueries
      .Read<MessengerModel>(page, cancellationToken);
    while (result.Items.Count > 0)
    {
      foreach (var messenger in result.Items)
      {
        await manager.EnsureInactivityMonitorJob(
          messenger.Id,
          messenger.MaxInactivityPeriod.ToTimeSpan()
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
          messenger.MaxInactivityPeriod.ToTimeSpan()
        );
      }

      if (entry.State is DataModelChangedState.Removed)
      {
        await manager.UnscheduleInactivityMonitorJob(messenger.Id);
      }

      if (entry.State is DataModelChangedState.Modified)
      {
        await manager.RescheduleInactivityMonitorJob(
          messenger.Id,
          messenger.MaxInactivityPeriod.ToTimeSpan()
        );
      }
    }
  }
}
