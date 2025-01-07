using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors;

public class DataMeasurementLocationChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataMeasurementLocationChangeHandler>(serviceProvider)
{
}

public class DataMeasurementLocationChangeHandler(
  MeasurementLocationCache cache
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var entry in eventArgs.Models)
    {
      if (entry.Model is not IMeasurementLocation measurementLocation)
      {
        continue;
      }

      if (entry.State is not DataModelChangedState.Modified)
      {
        await cache.TryUpdateAsync(measurementLocation, cancellationToken);
      }
      else if (entry.State is DataModelChangedState.Removed)
      {
        await cache.TryRemoveAsync(measurementLocation, cancellationToken);
      }
    }
  }
}
