using Microsoft.EntityFrameworkCore;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class AuditableEntity
{
  private readonly long _id;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = long.Parse(value); }
  }

  public string Title { get; set; } = default!;

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
}

public class
  AuditableEntityConfiguration : EntityTypeHierarchyConfiguration<
  AuditableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    if (type == typeof(AuditableEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(type);

    if (type.BaseType == typeof(AuditableEntity))
    {
      if (type == typeof(RepresentativeEntity)
          || type == typeof(MeterEntity)
          || type == typeof(MessengerEntity))
      {
        builder.HasKey("_stringId");
      }
      else
      {
        builder.HasKey("_id");
      }
    }

    if (type == typeof(RepresentativeEntity) ||
        type.IsAssignableTo(typeof(MeterEntity)) ||
        type.IsAssignableTo(typeof(MessengerEntity)))
    {
      builder.Ignore("_id");
      builder.Ignore(nameof(AuditableEntity.Id));
      builder
        .Property("_stringId")
        .HasColumnName("id")
        .HasColumnType("text")
        .ValueGeneratedNever();
    }
    else
    {
      builder.Ignore(nameof(AuditableEntity.Id));
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
  }
}
