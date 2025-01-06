using Ozds.Business.Conversion.Agnostic;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Observers.Abstractions;
using Ozds.Business.Observers.EventArgs;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Observers.Abstractions;
using Ozds.Data.Observers.EventArgs;

namespace Ozds.Business.Observers;

public class DataModelsChangedObserver(
  IEntitiesChangedSubscriber subscriber,
  AgnosticModelEntityConverter modelEntityConverter
) : IDataModelsChangedSubscriber
{
  public void Subscribe(EventHandler<DataModelsChangedEventArgs> eventHandler)
  {
    var modelEventHandler = new Handler(
      eventHandler,
      modelEntityConverter
    );
    subscriber.Subscribe(modelEventHandler.Handle);
  }

  public void Unsubscribe(EventHandler<DataModelsChangedEventArgs> eventHandler)
  {
    var modelEventHandler = new Handler(
      eventHandler,
      modelEntityConverter
    );
    subscriber.Unsubscribe(modelEventHandler.Handle);
  }

  private record struct Handler(
    EventHandler<DataModelsChangedEventArgs> EventHandler,
    AgnosticModelEntityConverter ModelEntityConverter
  )
  {
    public readonly void Handle(
      object? sender,
      EntitiesChangedEventArgs eventArgs)
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
      EventHandler(sender, modelsEventArgs);
    }
  }
}
