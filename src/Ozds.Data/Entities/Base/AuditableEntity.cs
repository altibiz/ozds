using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class AuditableEntity : IdentifiableEntity, IAuditableEntity
{
  public virtual RepresentativeEntity? DeletedBy { get; set; }
  public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? CreatedById { get; set; }

  public virtual RepresentativeEntity? CreatedBy { get; set; }

  public DateTimeOffset? LastUpdatedOn { get; set; }

  public string? LastUpdatedById { get; set; }

  public virtual RepresentativeEntity? LastUpdatedBy { get; set; }

  public bool IsDeleted { get; set; }

  public DateTimeOffset? DeletedOn { get; set; }

  public string? DeletedById { get; set; }

  public bool Restore { get; set; } = false;

  public bool Forget { get; set; } = false;

  public string? RepresentativeId { get; set; }
}

public class
  AuditableEntityConfiguration : EntityTypeHierarchyConfiguration<
  AuditableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

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

    builder.Ignore(nameof(AuditableEntity.Restore));
    builder.Ignore(nameof(AuditableEntity.Forget));
    builder.Ignore(nameof(AuditableEntity.RepresentativeId));
  }
}
