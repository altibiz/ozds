using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

[Table("catalogues")]
public class CatalogueEntity : AuditableEntity
{
  public virtual LocationEntity Location { get; set; } = default!;
}

public class CatalogueEntityTypeConfiguration : EntityTypeConfiguration<CatalogueEntity>
{
  public override void Configure(EntityTypeBuilder<CatalogueEntity> builder)
  {
    builder
      .UseTphMappingStrategy()
      .HasDiscriminator<string>("kind");
  }
}
