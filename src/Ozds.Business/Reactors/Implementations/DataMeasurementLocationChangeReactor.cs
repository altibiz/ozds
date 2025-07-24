using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors.Implementations;

public class DataMeasurementLocationChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataMeasurementLocationChangeHandler>(serviceProvider)
{
}

public class DataMeasurementLocationChangeHandler(
  MeasurementLocationByMeterCache cache
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var modified = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Modified)
      .Select(x => x.Model)
      .OfType<IMeasurementLocation>()
      .ToList();
    if (modified.Count > 0)
    {
      await cache.TryUpdateAsync(modified, cancellationToken);
    }

    var removed = eventArgs.Models
      .Where(x => x.State == DataModelChangedState.Removed)
      .Select(x => x.Model)
      .OfType<IMeasurementLocation>()
      .ToList();
    if (removed.Count > 0)
    {
      await cache.TryRemoveAsync(removed, cancellationToken);
    }
  }
}
