using Ozds.Caching.Configuration;
using Ozds.Caching.Profiles.Base;

namespace Ozds.Caching.Entities.Abstractions;

public interface IMeasurementLocationEntity : ITrackableIdentifiableEntity
{
  public string MeterId { get; }
}

public class MeasurementLocationEntityProfiler
  : Profiler<IMeasurementLocationEntity>
{
  protected override CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder
  )
  {
    return builder.WithIndirectReverseDependencyEvictionPolicy(
      x => x is IMeasurementLocationEntity entity ? entity.MeterId : null,
      typeof(IMeterEntity)
    );
  }
}
