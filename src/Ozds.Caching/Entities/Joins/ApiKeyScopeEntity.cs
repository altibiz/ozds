using Ozds.Caching.Configuration;
using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Base;
using Ozds.Caching.Profiles.Base;

namespace Ozds.Caching.Entities.Joins;

public class ApiKeyScopeEntity : AuditableJoinEntity
{
  public override string LeftId
  {
    get { return ApiKeyId; }
  }

  public override string RightId
  {
    get { return ScopeId; }
  }

  public string ApiKeyId { get; set; } = default!;

  public string ScopeId { get; set; } = default!;
}

public class ApiKeyScopeEntityProfiler : Profiler<ApiKeyScopeEntity>
{
  protected override CacheConfigurationBuilder Configure(
    CacheConfigurationBuilder builder)
  {
    return builder
      .WithIndirectReverseDependencyEvictionPolicy(
        x => x is ApiKeyScopeEntity entity ? entity.ApiKeyId : null,
        typeof(ApiKeyEntity)
      )
      .WithIndirectReverseDependencyEvictionPolicy(
        x => x is ApiKeyScopeEntity entity ? entity.ScopeId : null,
        typeof(IScopeEntity)
      );
  }
}
