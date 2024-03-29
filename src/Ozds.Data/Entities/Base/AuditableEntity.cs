using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class AuditableEntity : IdentifiableEntity
{
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
  public override void ConfigureConcrete<T>(EntityTypeBuilder<T> builder)
  {
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
