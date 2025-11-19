using Ozds.Caching.Configuration;
using Ozds.Caching.Profiles.Base;

namespace Ozds.Caching.Entities.Abstractions;

public interface ICompositeEntity : IEntity
{
  public string Id { get; set; }

  public string Title { get; }
}

public class CompositeEntityProfiler : Profiler<ICompositeEntity>
{
  protected override CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder
  )
  {
    return builder
      .WithDependencyTracking();
  }
}
