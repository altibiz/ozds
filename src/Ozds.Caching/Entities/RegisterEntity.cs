using Ozds.Caching.Configuration;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Entities.Enums;
using Ozds.Caching.Profiles.Base;

namespace Ozds.Caching.Entities;

public class RegisterEntity : TrackableEntity
{
  public string ScopeId { get; set; } = default!;

  public string Name { get; set; } = default!;

  public MeasureEntity Measure { get; set; } = default!;

  public OrderOfMagnitudeEntity? OrderOfMagnitude { get; set; } = default!;

  public TariffEntity? Tariff { get; set; } = default!;

  public DuplexEntity? Duplex { get; set; } = default!;

  public PhaseEntity? Phase { get; set; } = default!;

  public AggregationEntity? Aggregation { get; set; } = default!;
}

public class RegisterEntityProfiler : Profiler<RegisterEntity>
{
  protected override CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder
  )
  {
    return builder.WithIndirectReverseDependencyEvictionPolicy(
      x => x is RegisterEntity entity ? entity.ScopeId : null,
      typeof(IScopeEntity)
    );
  }
}
