using Ozds.Caching.Configuration;

namespace Ozds.Caching.Profiles.Abstractions;

public interface IProfile
{
  public Type Type { get; }

  public CacheConfiguration Configuration { get; }
}

public interface IProfile<T> : IProfile
{
  Type IProfile.Type
  {
    get { return typeof(T); }
  }
}
