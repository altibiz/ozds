using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class AuditableEntity
{
  public string Id { get; init; } = default!;

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

  public virtual ICollection<AuditEventEntity> Audits { get; set; } = default!;
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
      builder.HasKey(nameof(AuditableEntity.Id));
    }

    if (type == typeof(RepresentativeEntity))
    {
      builder
        .Property(nameof(AuditableEntity.Id))
        .ValueGeneratedNever()
        .HasColumnName("id")
        .HasColumnType("text");
    }
    else
    {
      builder
        .Property(nameof(AuditableEntity.Id))
        .HasColumnType("bigint")
        .HasColumnName("id")
        .HasConversion<StringToNumberConverter<long>>()
        .ValueGeneratedOnAdd()
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
