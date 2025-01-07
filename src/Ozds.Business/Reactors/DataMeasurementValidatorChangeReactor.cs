using Ozds.Business.Caching;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Business.Reactors.Base;

namespace Ozds.Business.Reactors;

public class DataMeasurementValidatorChangeReactor(
  IServiceProvider serviceProvider
) : Reactor<
  DataModelsChangedEventArgs,
  IDataModelsChangedSubscriber,
  DataMeasurementValidatorChangeHandler>(serviceProvider)
{
}

public class DataMeasurementValidatorChangeHandler(
  ValidationCache cache
) : Handler<DataModelsChangedEventArgs>
{
  public override async Task Handle(
    DataModelsChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    foreach (var entry in eventArgs.Models)
    {
      if (entry.Model is not IMeasurementValidator validator)
      {
        continue;
      }

      if (entry.State is not DataModelChangedState.Modified)
      {
        await cache.TryUpdateAsync(validator, cancellationToken);
      }
      else if (entry.State is DataModelChangedState.Removed)
      {
        await cache.TryRemoveAsync(validator, cancellationToken);
      }
    }
  }
}
