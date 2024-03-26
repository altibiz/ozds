using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Entities.Base;

public abstract class CatalogueEntity : AuditableEntity
{
  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;
}

public class CatalogueEntityTypeConfiguration : IEntityTypeConfiguration<CatalogueEntity>
{
  public void Configure(EntityTypeBuilder<CatalogueEntity> builder)
  {
    builder.ToTable("catalogues").HasDiscriminator<int>("kind");
  }
}
