using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity
{
  private readonly long _id;

  public virtual string Id
  {
    get { return _id.ToString(); }
    init { _id = long.Parse(value); }
  }

  public string Title { get; set; } = default!;
}

public class
  IdentifiableEntityConfiguration : EntityTypeHierarchyConfiguration<
  IdentifiableEntity>
{
  public override void Configure<TEntity>(EntityTypeBuilder<TEntity> builder)
  {
    if (typeof(TEntity) != typeof(RepresentativeEntity))
    {
      builder.Ignore(nameof(IdentifiableEntity.Id));
    }
  }

  public override void ConfigureConcrete<T>(EntityTypeBuilder<T> builder)
  {
    if (!AlreadyHasKey(typeof(T)))
    {
      builder.HasKey("_id");
    }

    if (typeof(T) != typeof(RepresentativeEntity))
    {
      builder
        .Property("_id")
        .HasColumnType("bigint")
        .HasColumnName("id")
        .ValueGeneratedOnAdd()
        .UseIdentityAlwaysColumn();
    }
  }

  private static bool AlreadyHasKey(Type type)
  {
    for (
      var currentType = type.BaseType;
      currentType != null;
      currentType = currentType.BaseType)
    {
      if (currentType is not { IsAbstract: true } and not
        { IsGenericType: true })
      {
        return true;
      }
    }

    return false;
  }
}
