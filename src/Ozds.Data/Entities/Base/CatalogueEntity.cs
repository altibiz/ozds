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

public class CatalogueEntityTypeConfiguration : EntityTypeConfiguration<CatalogueEntity>
{
  public override void Configure(EntityTypeBuilder<CatalogueEntity> builder)
  {
  }
}

public class
  CatalogueEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  CatalogueEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    var builder = modelBuilder.Entity(type);

    if (type == typeof(CatalogueEntity))
    {
      builder
        .UseTphMappingStrategy()
        .ToTable("catalogues")
        .HasDiscriminator<string>("kind");
    }

    builder
      .HasOne(nameof(CatalogueEntity.Location))
      .WithOne()
      .HasForeignKey(type.Name, nameof(CatalogueEntity.LocationId));
  }
}
