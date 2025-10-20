using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Entities.Joins;

namespace Ozds.Data.Entities;

public class ScopeEntity : TrackableEntity, IGuidIdentifiableEntity
{
  public string? ScopeEntityId { get; set; } = default!;

  public string? ScopeEntityType { get; set; } = default!;

  public string? ScopeEntityTable { get; set; } = default!;

  public ActionEntity ScopeAction { get; set; } = default!;

  public virtual ICollection<ApiKeyEntity> ApiKeys { get; set; } = default!;

  public virtual ICollection<ApiKeyScopeEntity> ApiKeyScopes { get; set; } =
    default!;

  public string Kind { get; set; } = default!;
}

public class ScopeTypeConfiguration
  : EntityTypeHierarchyConfiguration<ScopeEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder
      .UseTphMappingStrategy()
      .ToTable("scopes")
      .HasDiscriminator<string>(nameof(ScopeEntity.Kind));

    builder.HasIndex(
      new[]
      {
        nameof(ScopeEntity.ScopeEntityType),
        nameof(ScopeEntity.ScopeEntityId)
      },
      "ix_scopes_scope_entity_type_scope_entity_id");

    builder.HasIndex(
      new[]
      {
        nameof(ScopeEntity.ScopeEntityTable),
        nameof(ScopeEntity.ScopeEntityId)
      },
      "ix_scopes_scope_entity_table_scope_entity_id");
  }
}
