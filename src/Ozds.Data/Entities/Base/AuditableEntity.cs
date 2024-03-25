using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Base;

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

public class
  SoftDeletableEntityConfiguration : IEntityTypeConfiguration<AuditableEntity>
{
  public void Configure(EntityTypeBuilder<AuditableEntity> builder)
  {
    builder.HasQueryFilter(x => !x.IsDeleted);
  }
}
