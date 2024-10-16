using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ozds.Data.Extensions;

// TODO: better way to find nested archivable properties

public static class EntityTypeBuilderExtensions
{
  public static ComplexPropertyBuilder Archived(
    this ComplexPropertyBuilder complexPropertyBuilder,
    string? propertyName = null)
  {
    propertyName ??= complexPropertyBuilder.Metadata.Name;

    var propertiesToIgnore = complexPropertyBuilder.Metadata
      .ComplexType
      .ClrType
      .GetProperties()
      .Where(
        property => property is { GetMethod.IsVirtual: true } or
          { GetMethod.IsAbstract: true })
      .ToList();

    var propertiesToArchive = complexPropertyBuilder.Metadata
      .ComplexType
      .ClrType
      .GetProperties()
      .Where(property => property.PropertyType.Name.EndsWith("Entity"))
      .Except(propertiesToIgnore)
      .ToList();

    var propertiesToShorten = complexPropertyBuilder.Metadata
      .ComplexType
      .ClrType
      .GetProperties()
      .Except(propertiesToIgnore)
      .Except(propertiesToArchive)
      .ToList();

    foreach (var property in propertiesToIgnore)
    {
      complexPropertyBuilder.Ignore(property.Name);
    }

#pragma warning disable S3267
    foreach (var property in propertiesToShorten)
    {
      complexPropertyBuilder
        .Property(property.Name)
        .HasColumnName(
          propertyName.Abbreviation()
          + "_"
          + property.Name.ToSnakeCase());
    }

    foreach (var property in propertiesToArchive)
    {
      complexPropertyBuilder
        .ComplexProperty(property.Name)
        .Archived(
          propertyName.Abbreviation()
          + "_"
          + property.Name.Abbreviation());
    }
#pragma warning restore S3267

    return complexPropertyBuilder;
  }

  public static ComplexPropertyBuilder ArchivedProperty(
    this EntityTypeBuilder builder,
    string propertyName
  )
  {
    var complexPropertyBuilder = builder.ComplexProperty(propertyName);
    return complexPropertyBuilder.Archived();
  }

  private static string Abbreviation(this string name)
  {
    return string.Concat(
      !name.Any(char.IsUpper) ? name : name.Where(char.IsUpper)).ToLower();
  }

  private static string ToSnakeCase(this string name)
  {
    return string.Concat(
      name.Select(
        (x, i) =>
          i > 0 && char.IsUpper(x) && !char.IsUpper(name[i - 1])
            ? "_" + x.ToString().ToLower()
            : x.ToString().ToLower()
      )
    );
  }

  public static EntityTypeBuilder HasTimescaleHypertable(
    this EntityTypeBuilder builder,
    string timeColumn,
    string? spaceColumn = null,
    string? spacePartitioning = null
  )
  {
    var value = timeColumn;
    if (spaceColumn is not null && spacePartitioning is not null)
    {
      value += $",{spaceColumn}:{spacePartitioning}";
    }

    builder.Metadata.AddAnnotation("TimescaleHypertable", value);

    return builder;
  }
}
