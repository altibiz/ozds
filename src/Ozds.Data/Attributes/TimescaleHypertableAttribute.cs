using System.Reflection;
using Microsoft.EntityFrameworkCore;

// TODO: continuous aggregate attributes like this one

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TimescaleHypertableAttribute : Attribute
{
  public TimescaleHypertableAttribute(string timeColumn)
  {
    TimeColumn = timeColumn;
  }

  public string TimeColumn { get; }
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
          builder
            .Entity(x.Entity.ClrType)
            .HasAnnotation("TimescaleHypertable", x.Attribute!.TimeColumn);

          return builder;
        }
      );
  }
}
