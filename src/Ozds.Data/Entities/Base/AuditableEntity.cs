using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[NotMapped]
public abstract class AuditableEntity : IdentifiableEntity
{
  public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

  [ForeignKey(nameof(CreatedBy))] public string? CreatedById { get; set; }

  public virtual RepresentativeEntity? CreatedBy { get; set; }

  public DateTimeOffset? LastUpdatedOn { get; set; }

  [ForeignKey(nameof(LastUpdatedBy))]
  public string? LastUpdatedById { get; set; }

  public virtual RepresentativeEntity? LastUpdatedBy { get; set; }

  public bool IsDeleted { get; set; }

  public DateTimeOffset? DeletedOn { get; set; }

  [ForeignKey(nameof(DeletedBy))] public string? DeletedById { get; set; }

  public virtual RepresentativeEntity? DeletedBy { get; set; }

  public virtual ICollection<AuditEventEntity> Audits { get; set; } = default!;
}

public class AuditableEntityConfiguration : InheritedEntityTypeConfiguration<AuditableEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.HasOne(x => x.CreatedBy)
      .WithMany()
      .HasForeignKey(x => x.CreatedById);
    builder.HasOne(x => x.LastUpdatedBy)
      .WithMany()
      .HasForeignKey(x => x.LastUpdatedById);
    builder.HasOne(x => x.DeletedBy)
      .WithMany()
      .HasForeignKey(x => x.DeletedById);
    builder.HasQueryFilter(x => !x.IsDeleted);
  }
}
