using Ozds.Data.Observers.EventArgs;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntityChangesPublisher : IPublisher<IEntityChangesSubscriber>
{
  public void PublishEntitiesChanging(EntitiesChangingEventArgs eventArgs);

  public void PublishEntitiesChanged(EntitiesChangedEventArgs eventArgs);
}
