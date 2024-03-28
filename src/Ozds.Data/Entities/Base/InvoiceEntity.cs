using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public abstract class InvoiceEntity : ReadonlyEntity
{
  private readonly long _id;

  public virtual string Id { get => _id.ToString(); init => _id = long.Parse(value); }

  public DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

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

    builder.Ignore(nameof(EventEntity.Id));
    builder
      .Property("_id")
      .HasColumnType("bigint")
      .HasColumnName("id")
      .ValueGeneratedOnAdd()
      .UseIdentityAlwaysColumn();

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
