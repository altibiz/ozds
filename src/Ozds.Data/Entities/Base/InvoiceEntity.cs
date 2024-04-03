using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

// TODO: copy entities via complex properties

namespace Ozds.Data.Entities.Base;

public abstract class InvoiceEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  public DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = value is { } nonullValue ? long.Parse(nonullValue) : default; }
  }

  public string Title { get; set; } = default!;
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

    builder.HasKey("_id");
    builder.Ignore(nameof(InvoiceEntity.Id));
    builder
      .Property("_id")
      .HasColumnName("id")
      .HasColumnType("bigint")
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
