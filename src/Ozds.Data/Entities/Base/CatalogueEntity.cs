using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class CatalogueEntity : AuditableEntity
{
  public string LocationId { get; set; } = default!;

  public virtual LocationEntity Location { get; set; } = default!;

  public virtual ICollection<MeterEntity> Meters { get; set; } = default!;
}

public class
  CatalogueEntityTypeConfiguration : EntityTypeHierarchyConfiguration<
  CatalogueEntity>
{
  public override void ConfigureConcrete<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("catalogues")
      .HasDiscriminator<string>("kind");

    builder
      .HasOne(nameof(CatalogueEntity.Location))
      .WithOne()
      .HasForeignKey(typeof(T).Name, nameof(CatalogueEntity.LocationId));
  }
}
