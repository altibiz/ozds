using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

// TODO: check that all base classes are only used for inheritance

namespace Ozds.Data.Entities.Base;

public class CalculationEntity : IReadonlyEntity, IIdentifiableEntity
{
  protected readonly long _id;

  public DateTimeOffset IssuedOn { get; set; } = DateTimeOffset.UtcNow;

  public string? IssuedById { get; set; }

  public virtual RepresentativeEntity? IssuedBy { get; set; }

  public DateTimeOffset FromDate { get; set; } = default!;

  public DateTimeOffset ToDate { get; set; } = default!;

  public virtual MeterEntity Meter { get; set; } = default!;

  public string MeterId { get; set; } = default!;

  public decimal Total_EUR { get; set; }

  public MeterEntity ArchivedMeter { get; set; } = default!;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init
    {
      _id = value is { } notNullValue ? long.Parse(notNullValue) : default;
    }
  }

  public string Title { get; set; } = default!;
}

public class
  CalculationEntityTypeHierarchyConfiguration :
  EntityTypeHierarchyConfiguration
  <
    CalculationEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    if (entity == typeof(CalculationEntity))
    {
      return;
    }

    var builder = modelBuilder.Entity(entity);

    if (entity == typeof(NetworkUserCalculationEntity))
    {
      builder.HasKey("_id");
    }

    if (entity.IsAssignableTo(typeof(NetworkUserCalculationEntity)))
    {
      builder
        .HasOne(nameof(CalculationEntity.Meter))
        .WithMany(nameof(MeterEntity.NetworkUserCalculations))
        .HasForeignKey(nameof(CalculationEntity.MeterId));
    }

    builder.Ignore(nameof(CalculationEntity.Id));
    builder
      .Property("_id")
      .HasColumnName("id")
      .HasColumnType("bigint")
      .UseIdentityAlwaysColumn();

    builder
      .HasOne(nameof(CalculationEntity.IssuedBy))
      .WithMany()
      .HasForeignKey(nameof(CalculationEntity.IssuedById));

    builder
      .ArchivedProperty(nameof(CalculationEntity.ArchivedMeter));

    builder
      .Property<DateTimeOffset>(nameof(CalculationEntity.IssuedOn))
      .HasConversion(
        x => x.ToUniversalTime(),
        x => x.ToUniversalTime()
      );

    builder
      .Property(nameof(CalculationEntity.Total_EUR))
      .HasColumnName("total_eur");
  }
}
