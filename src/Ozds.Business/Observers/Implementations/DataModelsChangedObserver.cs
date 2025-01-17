using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class DataModelsChangedRelay(
  IServiceProvider serviceProvider,
  IEntitiesChangedSubscriber subscriber
) : Relay<
  EntitiesChangedEventArgs,
  DataModelsChangedEventArgs,
  DataModelsChangedPipe>(
  serviceProvider
), IDataModelsChangedSubscriber
{
  protected override void SubscribeIn(
    EventHandler<EntitiesChangedEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<EntitiesChangedEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class DataModelsChangedPipe(
  ModelEntityConverter ModelEntityConverter
) : IPipe<EntitiesChangedEventArgs, DataModelsChangedEventArgs>
{
  public Task<DataModelsChangedEventArgs> Transform(
    EntitiesChangedEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var models = new List<DataModelChangedEntry>();
    foreach (var entity in eventArgs.Entities)
    {
      models.Add(
        new DataModelChangedEntry(
          entity.State switch
          {
            EntityChangedState.Added => DataModelChangedState.Added,
            EntityChangedState.Modified => DataModelChangedState.Modified,
            EntityChangedState.Removed => DataModelChangedState.Removed,
            _ => throw new NotImplementedException()
          },
          ModelEntityConverter.ToModel<IModel>(entity.Entity)
        )
      );
    }

    var modelsEventArgs = new DataModelsChangedEventArgs
    {
      Models = models
    };

    return Task.FromResult(modelsEventArgs);
  }
}
