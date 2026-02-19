using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Abstractions;

namespace Ozds.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
  public static void InstantaneousMeasurementMeasure(
    this EntityTypeBuilder builder,
    string propertyName,
    string columnName
  )
  {
    builder
      .Property(propertyName)
      .HasColumnName(columnName)
      // NOTE: we are not using these for any further calculation
      // and it would be more beneficial to save space on these
      // rather than to make them more precise
      .HasColumnType("real");
  }

  public static void CumulativeMeasurementMeasure(
    this EntityTypeBuilder builder,
    string propertyName,
    string columnName
  )
  {
    builder
      .Property(propertyName)
      .HasColumnName(columnName)
      // NOTE: used for calculations
      .HasColumnType("bigint");
  }

  public static void MonetaryValue(
    this EntityTypeBuilder builder,
    string propertyName,
    string columnName
  )
  {
    builder
      .Property(propertyName)
      .HasColumnName(columnName)
      // NOTE: https://stackoverflow.com/a/27328409
      .HasColumnType("decimal(19, 4)");
  }

  public static void InstantaneousMeasurementMeasure(
    this ComplexPropertyBuilder builder,
    string propertyName,
    string columnName
  )
  {
    builder
      .Property(propertyName)
      .HasColumnName(columnName)
      // NOTE: we are not using these for any further calculation
      // and it would be more beneficial to save space on these
      // rather than to make them more precise
      .HasColumnType("real");
  }

  public static void CumulativeMeasurementMeasure(
    this ComplexPropertyBuilder builder,
    string propertyName,
    string columnName
  )
  {
    builder
      .Property(propertyName)
      .HasColumnName(columnName)
      // NOTE: used for calculations
      .HasColumnType("bigint");
  }

  public static void MonetaryValue(
    this ComplexPropertyBuilder builder,
    string propertyName,
    string columnName
  )
  {
    builder
      .Property(propertyName)
      .HasColumnName(columnName)
      // NOTE: https://stackoverflow.com/a/27328409
      .HasColumnType("decimal(19, 4)");
  }

  // TODO: forget and entity properties are flakey because
  // they depend on the name of the property
  public static ComplexPropertyBuilder Archived(
    this ComplexPropertyBuilder complexPropertyBuilder,
    string? propertyName = null
  )
  {
    propertyName ??= complexPropertyBuilder.Metadata.Name;

    // NOTE: navigation properties are virtual and not final
    // if a property is virtual and final it is not possible to override it
    // which EF Core will do when it encounters a navigation property
    var propertiesToIgnore = complexPropertyBuilder
      .Metadata.ComplexType.ClrType.GetProperties()
      .Where(property =>
        property
          is { GetMethod.IsVirtual: true }
            and { GetMethod.IsFinal: false }
        || property.Name == nameof(IJoinEntity.LeftId)
        || property.Name == nameof(IJoinEntity.RightId)
        || property.Name == nameof(IAuditableEntity.AuditingId)
        || property.Name == nameof(IAuditableEntity.AuditingTitle)
        || property.Name == nameof(IAuditableEntity.AuditingRepresentativeId)
        || property.Name == nameof(ITrackableEntity.Forget)
        || property.Name == nameof(ITrackableEntity.Restore)
      )
      .ToList();

    var propertiesToArchive = complexPropertyBuilder
      .Metadata.ComplexType.ClrType.GetProperties()
      .Where(property =>
        property.PropertyType.Name.EndsWith("Entity")
        && !property.PropertyType.IsEnum
      )
      .Except(propertiesToIgnore)
      .ToList();

    var propertiesToShorten = complexPropertyBuilder
      .Metadata.ComplexType.ClrType.GetProperties()
      .Except(propertiesToIgnore)
      .Except(propertiesToArchive)
      .ToList();

    // NOTE: good for debug - please leave this in
#pragma warning disable S125 // Sections of code should not be commented out
    // Console.Write(
    //   "Archiving property {0} of {1}"
    //   + $"{Environment.NewLine}Properties to ignore: {2}"
    //   + $"{Environment.NewLine}Properties to archive: {3}"
    //   + $"{Environment.NewLine}Properties to shorten: {4}"
    //   + Environment.NewLine
    //   + Environment.NewLine,
    //   propertyName,
    //   complexPropertyBuilder.Metadata.DeclaringType.Name,
    //   string.Join(", ", propertiesToIgnore.Select(p => p.Name)),
    //   string.Join(", ", propertiesToArchive.Select(p => p.Name)),
    //   string.Join(", ", propertiesToShorten.Select(p => p.Name)));
#pragma warning restore S125 // Sections of code should not be commented out

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
          propertyName.Abbreviation() + "_" + property.Name.ToSnakeCase()
        );
    }

    foreach (var property in propertiesToArchive)
    {
      complexPropertyBuilder
        .ComplexProperty(property.Name)
        .Archived(
          propertyName.Abbreviation() + "_" + property.Name.Abbreviation()
        );
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
