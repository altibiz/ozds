using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity
{
  public string Id { get; set; } = default!;

  public string Title { get; set; } = default!;
}

public class IdentifiableEntityConfiguration : ConcreteHierarchyEntityTypeConfiguration<IdentifiableEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    if (!AlreadyHasKey(typeof(T)))
    {
      builder.HasKey(nameof(IdentifiableEntity.Id));
    }

    builder
      .Property(nameof(IdentifiableEntity.Id))
      .ValueGeneratedOnAdd()
      .HasColumnType("bigint")
      .HasConversion<StringToNumberConverter<long>>();
  }

  private static bool AlreadyHasKey(Type type)
  {
    for (
      var currentType = type.BaseType;
      currentType != null;
      currentType = currentType.BaseType)
    {
      if (currentType is not { IsAbstract: true } and not { IsGenericType: true })
      {
        return true;
      }
    }

    return false;
  }
}
