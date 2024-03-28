using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// TODO: continuous aggregate attributes like this one

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TimescaleHypertableAttribute : Attribute
{
  public TimescaleHypertableAttribute(
    string timeColumn,
    string spaceColumn,
    string spacePartitioning
  )
  {
    TimeColumn = timeColumn;
    SpaceColumn = spaceColumn;
    SpacePartitioning = spacePartitioning;
  }

  public TimescaleHypertableAttribute(
    string timeColumn
  )
  {
    TimeColumn = timeColumn;
  }

  public string TimeColumn { get; }

  public string? SpaceColumn
  {
    get;
  }

  public string? SpacePartitioning
  {
    get;
  }
}

public static class TimescaleHypertableAttributeExtensions
{
  public static ModelBuilder ApplyTimescaleHypertableAttributes(
    this ModelBuilder builder)
  {
    return builder.Model
      .GetEntityTypes()
      .Select(entity => new
      {
        Entity = entity,
        Attribute = entity.ClrType
          .GetCustomAttribute<TimescaleHypertableAttribute>()
      })
      .Where(x => x.Attribute is not null)
      .Aggregate(
        builder,
        (builder, x) =>
        {
          var timeColumn = x.Attribute!.TimeColumn;
          var spaceColumn = x.Attribute!.SpaceColumn;
          var spacePartitioning = x.Attribute!.SpacePartitioning;

          builder
            .Entity(x.Entity.ClrType)
            .HasTimescaleHypertable(timeColumn, spaceColumn, spacePartitioning);

          return builder;
        }
      );
  }

  public static EntityTypeBuilder HasTimescaleHypertable(
    this EntityTypeBuilder builder,
    string timeColumn,
    string? spaceColumn = null,
    string? spacePartitioning = null
  )
  {
    var @value = timeColumn;
    if (spaceColumn is not null && spacePartitioning is not null)
    {
      @value += $",{spaceColumn}:{spacePartitioning}";
    }

    builder.Metadata.AddAnnotation("TimescaleHypertable", @value);

    return builder;
  }
}
