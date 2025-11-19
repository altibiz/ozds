namespace Ozds.Business.Models.Abstractions;

// TODO: figure out how to make the id contract less flaky

public interface ICached
{
  public string CacheId { get; }
}

public interface ICachedIdentifiable : ICached, IIdentifiable
{
  string ICached.CacheId
  {
    get { return Id; }
  }
}

public interface ICachedComposite : ICached, IComposite
{
}

public interface ICachedJoin : ICached, IJoin
{
  private const string CacheIdSeparator = ":";

  string ICached.CacheId
  {
    get { return LeftId + CacheIdSeparator + RightId; }
  }
}
