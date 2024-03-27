using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

// TODO: continuous aggregate attributes like this one

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TimescaleHypertableAttribute : Attribute
{
  public TimescaleHypertableAttribute(string timeColumn, params string[] spaceColumns)
  {
    TimeColumn = timeColumn;
    SpaceColumns = spaceColumns;
  }

  public string TimeColumn { get; }

  public string[] SpaceColumns
  {
    get;
  }
}

public static class TimescaleHypertableAttributeExtensions
{
  public static ModelBuilder ApplyTimescaleHypertables(
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
          var spaceColumns = x.Attribute!.SpaceColumns;

          var @value = timeColumn;
          if (spaceColumns.Length > 0)
          {
            @value += $",{string.Join(",", spaceColumns)}";
          }

          builder
            .Entity(x.Entity.ClrType)
            .HasAnnotation("TimescaleHypertable", @value);

          return builder;
        }
      );
  }
}
