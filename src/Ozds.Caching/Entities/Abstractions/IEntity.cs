using Ozds.Caching.Configuration;
using Ozds.Caching.Profiles.Base;

namespace Ozds.Caching.Entities.Abstractions;

public interface IEntity
{
}

public class EntityProfiler : Profiler<IEntity>
{
  protected override CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder
  ) =>
    builder.WithPolymorphicTypeHierarchy(
      typeof(IEntity),
      typeof(IEntity).Assembly,
      "Ozds.Caching.Entities"
    );
}
