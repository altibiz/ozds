using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class CatalogueEntity : AuditableEntity
{
  public virtual LocationEntity Location { get; set; } = default!;
}

public class CatalogueEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<CatalogueEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("catalogues")
      .HasDiscriminator<string>("kind");
  }
}
