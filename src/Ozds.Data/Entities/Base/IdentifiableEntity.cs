using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

// TODO: fix the base class mess
//       - set types not in database as abstract
// TODO: fix id mess
//       - make marker interface for string id types

public abstract class IdentifiableEntity : IIdentifiableEntity
{
  protected long _id;

  public virtual string Id
  {
    get { return _id.ToString(); }
    set
    {
      _id = value is { } notNullValue ? long.Parse(notNullValue) : default;
    }
  }

  public string Title { get; set; } = default!;
}

public class IdentifiableEntityConfiguration
  : EntityTypeHierarchyConfiguration<IdentifiableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    if (entity == typeof(IdentifiableEntity)
      || entity == typeof(AuditableEntity)
      || entity == typeof(CatalogueEntity))
    {
      return;
    }
    var builder = modelBuilder.Entity(entity);

    builder.Ignore(nameof(IdentifiableEntity.Id));
    if (entity.BaseType == typeof(AuditableEntity)
      || entity.BaseType == typeof(CatalogueEntity))
    {
      if (entity == typeof(RepresentativeEntity)
        || entity == typeof(MeterEntity)
        || entity == typeof(MessengerEntity))
      {
        builder.HasKey("_stringId");
      }
      else
      {
        builder.HasKey("_id");
      }
    }
    if (entity == typeof(RepresentativeEntity) ||
      entity.IsAssignableTo(typeof(MeterEntity)) ||
      entity.IsAssignableTo(typeof(MessengerEntity)))
    {
      builder.Ignore("_id");
      builder
        .Property("_stringId")
        .HasColumnName("id")
        .HasColumnType("text")
        .ValueGeneratedNever();
    }
    else
    {
      builder
        .Property("_id")
        .HasColumnName("id")
        .HasColumnType("bigint")
        .UseIdentityAlwaysColumn();
    }
  }
}
