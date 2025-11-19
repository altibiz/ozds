namespace Ozds.Caching.Cache.Abstractions;

public interface ICacheFactory
{
  public ICache<TValue> Create<TValue>()
    where TValue : notnull;

  public ICache Create(Type type);
}
