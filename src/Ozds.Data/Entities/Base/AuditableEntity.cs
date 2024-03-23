using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Base;

public abstract class AuditableEntity : IdEntity
{
  public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;

  public virtual RepresentativeEntity? CreatedBy { get; set; } = default!;

  public DateTimeOffset? LastUpdateDate { get; set; }

  public virtual RepresentativeEntity? LastUpdatedBy { get; set; }

  public bool IsDeleted { get; set; } = false;

  public DateTimeOffset? DeletionDate { get; set; }

  public virtual RepresentativeEntity? DeletedBy { get; set; }
}

public class SoftDeletableEntityConfiguration : IEntityTypeConfiguration<AuditableEntity>
{
  public void Configure(EntityTypeBuilder<AuditableEntity> builder)
  {
    builder.HasQueryFilter(x => !x.IsDeleted);
  }
}
