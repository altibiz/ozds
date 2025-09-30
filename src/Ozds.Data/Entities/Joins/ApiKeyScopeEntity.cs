using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities.Joins;

public class ApiKeyScopeEntity : AuditableJoinEntity
{
  private Guid _apiKeyId;

  private Guid _scopeId;

  public override string LeftId
  {
    get { return _apiKeyId.ToString(); }
  }

  public override string RightId
  {
    get { return _scopeId.ToString(); }
  }

  public string ApiKeyId
  {
    get { return _apiKeyId.ToString(); }
    set
    {
      _apiKeyId = value is { } notNullValue
        ? Guid.Parse(notNullValue)
        : Guid.Empty;
    }
  }

  public string ScopeId
  {
    get { return _scopeId.ToString(); }
    set
    {
      _scopeId = value is { } notNullValue
        ? Guid.Parse(notNullValue)
        : Guid.Empty;
    }
  }

  public virtual ApiKeyEntity ApiKey { get; set; } = default!;

  public virtual ScopeEntity Scope { get; set; } = default!;
}

public class
  ApiKeyScopeEntityModelConfiguration : IModelConfiguration
{
  public void Configure(ModelBuilder modelBuilder)
  {
    var entity = modelBuilder.Entity<ApiKeyEntity>();

    entity
      .HasMany(nameof(ApiKeyEntity.Scopes))
      .WithMany(nameof(ScopeEntity.ApiKeys))
      .UsingEntity(
        typeof(ApiKeyScopeEntity),
        configureLeft: l => l
          .HasOne(nameof(ApiKeyScopeEntity.ApiKey))
          .WithMany(nameof(ApiKeyEntity.ApiKeyScopes))
          .HasForeignKey("_apiKeyId"),
        configureRight: r => r
          .HasOne(nameof(ApiKeyScopeEntity.Scope))
          .WithMany(nameof(ScopeEntity.ApiKeyScopes))
          .HasForeignKey("_scopeId"),
        configureJoinEntityType: entity =>
        {
          entity.ToTable("api_key_scopes");

          entity.Ignore(nameof(ApiKeyScopeEntity.ApiKeyId));
          entity
            .Property("_apiKeyId")
            .HasColumnName("api_key_id");

          entity.Ignore(nameof(ApiKeyScopeEntity.ScopeId));
          entity
            .Property("_scopeId")
            .HasColumnName("scope_id");
        }
      );
  }
}
