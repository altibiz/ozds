using Ozds.Caching.Configuration;
using Ozds.Caching.Profiles.Abstractions;

namespace Ozds.Caching.Profiles.Base;

public abstract class Profiler<T> : IProfiler<T>
{
  public IProfile<T> SubProfile(CacheConfigurationBuilder builder)
  {
    return new ProfilerProfile(Configure(builder).Build());
  }

  protected abstract CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder);

  private sealed class ProfilerProfile(
    CacheConfiguration configuration
  ) : IProfile<T>
  {
    public CacheConfiguration Configuration
    {
      get { return configuration; }
    }
  }
}
