using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public abstract class InvoiceEntity : ReadonlyEntity
{
  public string Id { get; init; } = default!;

  public DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;
}

public class
  InvoiceEntityTypeHierarchyConfiguration : EntityTypeHierarchyConfiguration<
  InvoiceEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type type)
  {
    if (type == typeof(InvoiceEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(type);

    builder.HasKey(nameof(InvoiceEntity.Id));

    builder
      .Property(nameof(InvoiceEntity.Id))
      .HasColumnType("bigint")
      .HasColumnName("id")
      .HasConversion<StringToNumberConverter<long>>()
      .ValueGeneratedOnAdd()
      .UseIdentityAlwaysColumn();

    builder
      .Property<DateTimeOffset>(nameof(InvoiceEntity.IssuedOn))
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
