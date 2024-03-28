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

    builder
      .HasKey(nameof(InvoiceEntity.Id));

    builder
      .Property(nameof(InvoiceEntity.Id))
      .ValueGeneratedOnAdd()
      .HasColumnType("bigint")
      .HasConversion<StringToNumberConverter<long>>();
  }
}
