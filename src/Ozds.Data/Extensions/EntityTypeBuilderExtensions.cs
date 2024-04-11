using System.Collections.Immutable;
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

    var propertiesToIgnore = complexPropertyBuilder.Metadata
      .ComplexType
      .ClrType
      .GetProperties()
      .Where(property => property is { GetMethod.IsVirtual: true } or { GetMethod.IsAbstract: true })
      .ToList();

    var propertiesToShorten = complexPropertyBuilder.Metadata
      .ComplexType
      .ClrType
      .GetProperties()
      .Except(propertiesToIgnore)
      .ToList();

    foreach (var property in propertiesToIgnore)
    {
      complexPropertyBuilder.Ignore(property.Name);
    }

    foreach (var property in propertiesToShorten)
    {
      complexPropertyBuilder
        .Property(property.Name)
        .HasColumnName(
          propertyName.Abbreviation()
          + "_"
          + property.Name.ToSnakeCase());
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
