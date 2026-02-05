using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Entities.Base;

public abstract class IdentifiableEntity : IIdentifiableEntity
{
  protected Guid _guidId;
  protected long _id;

  protected string _stringId = default!;

  public virtual string Id
  {
#pragma warning disable S3060 // "is" should not be used with "this"
    get =>
      this is ICustomIdentifiableEntity ? _stringId
      : this is IGuidIdentifiableEntity ? _guidId.ToString()
      : _id.ToString();
#pragma warning restore S3060 // "is" should not be used with "this"
    set
    {
#pragma warning disable S3060 // "is" should not be used with "this"
      if (this is ICustomIdentifiableEntity)
      {
        _stringId = value;
      }
      else if (this is IGuidIdentifiableEntity)
      {
        _guidId = value is { } notNullValue
          ? Guid.Parse(notNullValue)
          : Guid.Empty;
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
      else if (entity.IsAssignableTo(typeof(IGuidIdentifiableEntity)))
      {
        builder.HasKey("_guidId");
      }
      else
      {
        builder.HasKey("_id");
      }
    }

    if (entity.IsAssignableTo(typeof(ICustomIdentifiableEntity)))
    {
      builder.Ignore("_id");
      builder.Ignore("_guidId");
      builder
        .Property("_stringId")
        .HasColumnName("id")
        .HasColumnType("text")
        .ValueGeneratedNever();
    }
    else if (entity.IsAssignableTo(typeof(IGuidIdentifiableEntity)))
    {
      builder.Ignore("_id");
      builder.Ignore("_stringId");
      builder
        .Property("_guidId")
        .HasColumnName("id")
        .HasColumnType("uuid")
        .HasDefaultValueSql("gen_random_uuid()")
        .ValueGeneratedOnAdd();
    }
    else
    {
      builder.Ignore("_stringId");
      builder.Ignore("_guidId");
      builder
        .Property("_id")
        .HasColumnName("id")
        .HasColumnType("bigint")
        .UseIdentityAlwaysColumn();
    }
  }
}
