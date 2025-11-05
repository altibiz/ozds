using Ozds.Caching.Cache;
using Ozds.Caching.Configuration;

namespace Ozds.Caching.Observers.EventArgs;

public class CacheEventArgs : System.EventArgs
{
  public required CacheConfiguration CacheConfiguration { get; init; }

  public required CacheOperation Operation { get; init; }

  public required string Key { get; init; }
}

public class CreateCacheEventArgs : CacheEventArgs
{
  public required object Value { get; init; }
}
