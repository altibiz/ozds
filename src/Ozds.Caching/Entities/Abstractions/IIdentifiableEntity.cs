using Ozds.Caching.Configuration;
using Ozds.Caching.Profiles.Base;

namespace Ozds.Caching.Entities.Abstractions;

public interface IIdentifiableEntity : IEntity
{
  string Title { get; }

  string Id { get; set; }
}

public class IdentifiableEntityProfiler : Profiler<IIdentifiableEntity>
{
  protected override CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder
  )
  {
    return builder.WithReverseDependencyEviction();
  }
}
