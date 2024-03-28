using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public abstract class InvoiceEntity : ReadonlyEntity
{

  public string Id { get; set; } = default!;

  public DateTimeOffset IssuedOn { get; set; } = default!;

  public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;
}

public class InvoiceEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<InvoiceEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder.HasKey(nameof(InvoiceEntity.Id));

    builder.UseTpcMappingStrategy();

    builder
      .Property(nameof(InvoiceEntity.Id))
      .ValueGeneratedOnAdd()
      .HasColumnType("bigint")
      .HasConversion<StringToNumberConverter<long>>();

    builder
      .Property(x => x.IssuedOn)
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder
      .HasOne(nameof(InvoiceEntity.IssuedBy))
      .WithMany()
      .HasForeignKey(nameof(InvoiceEntity.IssuedById));
  }
}
