using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[Table("catalogues")]
public abstract class CatalogueEntity : AuditableEntity
{
  [ForeignKey(nameof(Location))]
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;
}

public class CatalogueEntityTypeConfiguration : EntityTypeConfiguration<CatalogueEntity>
{
  public override void Configure(EntityTypeBuilder<CatalogueEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("catalogues")
      .HasDiscriminator<int>("kind");
  }
}
