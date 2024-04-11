using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
  public static ComplexPropertyBuilder ArchivedProperty(
    this EntityTypeBuilder builder,
    string propertyName
  )
  {
    var complexPropertyBuilder = builder.ComplexProperty(propertyName);

    foreach (var property in complexPropertyBuilder.Metadata.ComplexType.GetProperties())
    {
      if (property.PropertyInfo is { GetMethod.IsVirtual: true } or { GetMethod.IsAbstract: true })
      {
        complexPropertyBuilder.Ignore(property.Name);
      }
      else
      {
        complexPropertyBuilder
          .Property(property.Name)
          .HasColumnName(
            propertyName.Abbreviation()
            + "_"
            + property.Name.ToSnakeCase());
      }
    }

    return complexPropertyBuilder;
  }

  private static string Abbreviation(this string name)
  {
    return string.Concat(name.Where(char.IsUpper)).ToLower();
  }

  private static string ToSnakeCase(this string name)
  {
    return string.Concat(
      name.Select((x, i) =>
        i > 0 && char.IsUpper(x)
        ? "_" + x.ToString().ToLower()
        : x.ToString().ToLower()
      )
    );
  }
}
