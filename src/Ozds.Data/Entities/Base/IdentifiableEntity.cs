using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity
{
  public string Id { get; init; } = default!;

  public string Title { get; set; } = default!;
}

public class
  IdentifiableEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  IdentifiableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    if (
      type == typeof(IdentifiableEntity)
      || type == typeof(AuditableEntity)
    )
    {
      return;
    }

    var builder = modelBuilder.Entity(type);

    if (type.BaseType == typeof(AuditableEntity))
    {
      builder.HasKey(nameof(IdentifiableEntity.Id));
    }

    if (type == typeof(RepresentativeEntity))
    {
      builder
        .Property(nameof(IdentifiableEntity.Id))
        .ValueGeneratedNever()
        .HasColumnName("id")
        .HasColumnType("text");
    }
    else
    {
      builder
        .Property(nameof(IdentifiableEntity.Id))
        .HasColumnType("bigint")
        .HasColumnName("id")
        .HasConversion<StringToNumberConverter<long>>()
        .ValueGeneratedOnAdd()
        .UseIdentityAlwaysColumn();
    }
  }
}
