using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class DataModelsChangingObserver(
  IEntitiesChangingSubscriber subscriber,
  AgnosticModelEntityConverter modelEntityConverter
) : IDataModelsChangingSubscriber
{
  public void Subscribe(EventHandler<DataModelsChangingEventArgs> eventHandler)
  {
    var modelEventHandler = new Handler(
      eventHandler,
      modelEntityConverter
    );
    subscriber.Subscribe(modelEventHandler.Handle);
  }

  public void Unsubscribe(EventHandler<DataModelsChangingEventArgs> eventHandler)
  {
    var modelEventHandler = new Handler(
      eventHandler,
      modelEntityConverter
    );
    subscriber.Unsubscribe(modelEventHandler.Handle);
  }

  private record struct Handler(
    EventHandler<DataModelsChangingEventArgs> EventHandler,
    AgnosticModelEntityConverter ModelEntityConverter
  )
  {
    public void Handle(object? sender, EntitiesChangingEventArgs eventArgs)
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
      EventHandler(sender, modelsEventArgs);
    }
  }
}
