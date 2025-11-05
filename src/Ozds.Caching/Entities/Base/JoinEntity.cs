using Ozds.Caching.Entities.Abstractions;

namespace Ozds.Caching.Entities.Base;

public abstract class JoinEntity : Entity, IJoinEntity
{
  public abstract string LeftId { get; }

  public abstract string RightId { get; }
}
