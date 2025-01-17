using Ozds.Business.Conversion;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.Base;
using Ozds.Business.Observers.EventArgs;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Observers.Implementations;

public class DataModelsChangingRelay(
  IServiceProvider serviceProvider,
  IEntitiesChangingSubscriber subscriber
) : Relay<
  EntitiesChangingEventArgs,
  DataModelsChangingEventArgs,
  DataModelsChangingPipe>(
  serviceProvider
), IDataModelsChangingSubscriber
{
  protected override void SubscribeIn(
    EventHandler<EntitiesChangingEventArgs> eventHandler)
  {
    subscriber.Subscribe(eventHandler);
  }

  protected override void UnsubscribeIn(
    EventHandler<EntitiesChangingEventArgs> eventHandler)
  {
    subscriber.Unsubscribe(eventHandler);
  }
}

public class DataModelsChangingPipe(
  ModelEntityConverter ModelEntityConverter
) : IPipe<EntitiesChangingEventArgs, DataModelsChangingEventArgs>
{
  public Task<DataModelsChangingEventArgs> Transform(
    EntitiesChangingEventArgs eventArgs,
    CancellationToken cancellationToken)
  {
    var models = new List<DataModelChangingEntry>();
    foreach (var entity in eventArgs.Entities)
    {
      models.Add(
        new DataModelChangingEntry(
          entity.State switch
          {
            EntityChangingState.Adding => DataModelChangingState.Adding,
            EntityChangingState.Modifying => DataModelChangingState.Modifying,
            EntityChangingState.Removing => DataModelChangingState.Removing,
            _ => throw new NotImplementedException()
          },
          ModelEntityConverter.ToModel<IModel>(entity.Entity)
        )
      );
    }

    var modelsEventArgs = new DataModelsChangingEventArgs
    {
      Models = models
    };

    return Task.FromResult(modelsEventArgs);
  }
}
