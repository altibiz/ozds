using Ozds.Business.Caching;
using Ozds.Business.Models.Base;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Queries;
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
      foreach (var meter in result.Items)
      {
        await manager.EnsureInactivityMonitorJob(
          meter.Id,
          timeQueries.PeriodTimeSpan(meter.MaxInactivityPeriod),
          cancellationToken
        );
      }

      result = await auditableQueries
        .Read<MeterModel>(++page, cancellationToken);
    }
  }

  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var entry in eventArgs.Models)
    {
      if (entry.Model is not MeterModel meter)
      {
        continue;
      }

      if (entry.State is DataModelChangedState.Added)
      {
        await manager.EnsureInactivityMonitorJob(
          meter.Id,
          timeQueries.PeriodTimeSpan(meter.MaxInactivityPeriod),
          cancellationToken
        );
      }

      if (entry.State is DataModelChangedState.Removed)
      {
        await meterCache.TryRemoveAsync(meter, cancellationToken);
        measurementLocationByMeterCache.TryRemove(meter.Id, cancellationToken);
        messengerByMeterCache.TryRemove(meter.Id, cancellationToken);
        measurementValidatorByMeterCache.TryRemove(meter.Id, cancellationToken);
        await manager.UnscheduleInactivityMonitorJob(
          meter.Id,
          cancellationToken);
      }

      if (entry.State is DataModelChangedState.Modified)
      {
        await meterCache.TryUpdateAsync(meter, cancellationToken);
        measurementLocationByMeterCache.TryRemove(meter.Id, cancellationToken);
        messengerByMeterCache.TryRemove(meter.Id, cancellationToken);
        measurementValidatorByMeterCache.TryRemove(meter.Id, cancellationToken);
        await manager.RescheduleInactivityMonitorJob(
          meter.Id,
          timeQueries.PeriodTimeSpan(meter.MaxInactivityPeriod),
          cancellationToken
        );
      }
    }
  }
}
