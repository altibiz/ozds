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

public class CatalogueEntityTypeConfiguration : InheritedEntityTypeConfiguration<CatalogueEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .HasDiscriminator<string>("kind");
  }
}
