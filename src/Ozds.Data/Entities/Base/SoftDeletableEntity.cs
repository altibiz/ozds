using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Base;

public abstract class SoftDeletableEntity : IdEntity
{
  public bool IsDeleted { get; set; } = false;
}

public class SoftDeletableEntityConfiguration : IEntityTypeConfiguration<SoftDeletableEntity>
{
  public void Configure(EntityTypeBuilder<SoftDeletableEntity> builder)
  {
    builder.HasQueryFilter(x => !x.IsDeleted);
  }
}
