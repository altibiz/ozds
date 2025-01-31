using Microsoft.EntityFrameworkCore;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity : IIdentifiableEntity
{
  protected long _id;

  protected string _stringId = default!;

  public virtual string Id
  {
#pragma warning disable S3060 // "is" should not be used with "this"
    get => this is ICustomIdentifiableEntity
      ? _stringId
      : _id.ToString();
#pragma warning restore S3060 // "is" should not be used with "this"
    set
    {
#pragma warning disable S3060 // "is" should not be used with "this"
      if (this is ICustomIdentifiableEntity)
      {
        _stringId = value;
      }
      else
      {
        _id = value is { } notNullValue ? long.Parse(notNullValue) : default;
      }
#pragma warning restore S3060 // "is" should not be used with "this"
    }
  }

  public string Title { get; set; } = default!;
}

public class IdentifiableEntityConfiguration
  : EntityTypeHierarchyConfiguration<IdentifiableEntity>
{
  public override void Configure(ModelBuilder modelBuilder, Type entity)
  {
    var builder = modelBuilder.Entity(entity);

    builder.Ignore(nameof(IdentifiableEntity.Id));
    if (entity.BaseType?.IsAbstract ?? false)
    {
      if (entity.IsAssignableTo(typeof(ICustomIdentifiableEntity)))
      {
        builder.HasKey("_stringId");
      }
      else
      {
        builder.HasKey("_id");
      }
    }

    if (entity.IsAssignableTo(typeof(ICustomIdentifiableEntity)))
    {
      builder.Ignore("_id");
      builder
        .Property("_stringId")
        .HasColumnName("id")
        .HasColumnType("text")
        .ValueGeneratedNever();
    }
    else
    {
      builder.Ignore("_stringId");
      builder
        .Property("_id")
        .HasColumnName("id")
        .HasColumnType("bigint")
        .UseIdentityAlwaysColumn();
    }
  }
}
