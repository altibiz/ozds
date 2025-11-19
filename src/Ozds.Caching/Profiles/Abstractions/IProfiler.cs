using Ozds.Caching.Configuration;

namespace Ozds.Caching.Profiles.Abstractions;

public interface IProfiler
{
  public IProfile Profile(CacheConfigurationBuilder builder);
}

public interface IProfiler<T> : IProfiler
{
  IProfile IProfiler.Profile(CacheConfigurationBuilder builder)
  {
    return SubProfile(builder);
  }

  public IProfile<T> SubProfile(CacheConfigurationBuilder builder);
}
