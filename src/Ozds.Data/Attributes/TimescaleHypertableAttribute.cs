using System.Reflection;
using Microsoft.EntityFrameworkCore;

// TODO: continuous aggregate attributes like this one

namespace Ozds.Data.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class TimescaleHypertableAttribute : Attribute
{
  public string TimeColumn { get; }

  public TimescaleHypertableAttribute(string timeColumn)
  {
    TimeColumn = timeColumn;
  }
}

public static class TimescaleHypertableAttributeExtensions
{
  public static ModelBuilder ApplyTimescaleHypertables(this ModelBuilder builder) =>
    builder.Model
      .GetEntityTypes()
      .Select(entity => new
      {
        Entity = entity,
        Attribute = entity.ClrType.GetCustomAttribute<TimescaleHypertableAttribute>()
      })
      .Where(x => x.Attribute is { })
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
