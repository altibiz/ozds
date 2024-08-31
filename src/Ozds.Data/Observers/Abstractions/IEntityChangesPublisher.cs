using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Observers.Abstractions;

public interface IEntityChangesPublisher : IPublisher<IEntityChangesSubscriber>
{
  public void PublishEntityAdded(IEntity entity);

  public void PublishEntityModified(IEntity entity);

  public void PublishEntityRemoved(IEntity entity);
}
