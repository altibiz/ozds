namespace Ozds.Caching.Entities.Abstractions;

public interface IJoinEntity : IEntity
{
  private const string CacheIdSeparator = ":";

  public string Id => LeftId + CacheIdSeparator + RightId;

  public string LeftId { get; }

  public string RightId { get; }
}
