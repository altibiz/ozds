using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

// TODO: fix the id mess

public abstract class AuditableEntity : IAuditableEntity
{
  protected readonly long _id;

  public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? CreatedById { get; set; }

  public virtual RepresentativeEntity? CreatedBy { get; set; }

  public DateTimeOffset? LastUpdatedOn { get; set; }

  public string? LastUpdatedById { get; set; }

  public virtual RepresentativeEntity? LastUpdatedBy { get; set; }

  public bool IsDeleted { get; set; }

  public DateTimeOffset? DeletedOn { get; set; }

  public string? DeletedById { get; set; }

  public virtual RepresentativeEntity? DeletedBy { get; set; }

  public virtual string Id
  {
    get { return _id.ToString(); }
    init
    {
      _id = value is { } notNullValue ? long.Parse(notNullValue) : default;
    }
  }

  public string Title { get; set; } = default!;

  public bool Forget { get; set; } = false;
}

public class
  AuditableEntityConfiguration : EntityTypeHierarchyConfiguration<
  AuditableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    if (entity == typeof(AuditableEntity)
      || entity == typeof(CatalogueEntity))
    {
      return;
    }
    var builder = modelBuilder.Entity(entity);

    builder.Ignore(nameof(AuditableEntity.Id));
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

    builder
      .HasOne(nameof(AuditableEntity.CreatedBy))
      .WithMany()
      .HasForeignKey(nameof(AuditableEntity.CreatedById));

    builder
      .HasOne(nameof(AuditableEntity.LastUpdatedBy))
      .WithMany()
      .HasForeignKey(nameof(AuditableEntity.LastUpdatedById));

    builder
      .HasOne(nameof(AuditableEntity.DeletedBy))
      .WithMany()
      .HasForeignKey(nameof(AuditableEntity.DeletedById));

    builder.Ignore(nameof(AuditableEntity.Forget));
  }
}
