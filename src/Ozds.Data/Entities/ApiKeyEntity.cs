using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Joins;

namespace Ozds.Data.Entities;

public class ApiKeyEntity : TrackableEntity, IGuidIdentifiableEntity
{
  public string PrincipalEntityType { get; set; } = default!;

  public string PrincipalEntityTable { get; set; } = default!;

  public string PrincipalEntityId { get; set; } = default!;

  public string Hash { get; set; } = default!;

  public DateTimeOffset? ExpiresOn { get; set; }

  public virtual ICollection<ScopeEntity> Scopes { get; set; } = default!;

  public virtual ICollection<ApiKeyScopeEntity> ApiKeyScopes { get; set; } =
    default!;
}

public class ApiKeyTypeConfiguration : EntityTypeConfiguration<ApiKeyEntity>
{
  public override void Configure(EntityTypeBuilder<ApiKeyEntity> builder)
  {
    builder.HasIndex(nameof(ApiKeyEntity.ExpiresOn));

    builder.HasIndex(
      new[]
      {
        nameof(ApiKeyEntity.PrincipalEntityType),
        nameof(ApiKeyEntity.PrincipalEntityId),
      },
      "ix_api_keys_principal_entity_type_principal_entity_id"
    );

    builder.HasIndex(
      new[]
      {
        nameof(ApiKeyEntity.PrincipalEntityTable),
        nameof(ApiKeyEntity.PrincipalEntityId),
      },
      "ix_api_keys_principal_entity_table_principal_entity_id"
    );
  }
}
