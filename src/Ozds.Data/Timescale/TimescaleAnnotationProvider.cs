using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

namespace Ozds.Data.Timescale;

#pragma warning disable EF1001
public class TimescaleAnnotationProvider : NpgsqlAnnotationProvider
#pragma warning restore EF1001
{
  public TimescaleAnnotationProvider(
    RelationalAnnotationProviderDependencies dependencies
#pragma warning disable EF1001
  ) : base(dependencies)
#pragma warning restore EF1001
  {
  }

  public override IEnumerable<IAnnotation> For(
    ITable table,
    bool designTime
  )
  {
    if (!designTime)
    {
      return Enumerable.Empty<IAnnotation>();
    }
#pragma warning disable EF1001
    var annotations = base.For(table, designTime);
#pragma warning restore EF1001

    var annotation = table.EntityTypeMappings
      .Select(mapping => new
      {
        Mapping = mapping,
        // NOTE: this is the exact way that annotations get added in
        // NOTE: do not change this
        Value = (mapping.TypeBase
          .GetAnnotations()
          .FirstOrDefault(
            annotation =>
              annotation.Name == "TimescaleHypertable" &&
              annotation.Value is string)
          ?.Value as string)!
      })
      .FirstOrDefault(x => x.Value is not null);

    if (annotation is null)
    {
      return annotations;
    }

    var clrColumns = annotation.Value.Split(",");
    var clrTimeColumn = clrColumns.FirstOrDefault();
    if (clrTimeColumn is null)
    {
      return annotations;
    }

    var space = clrColumns.Skip(1).FirstOrDefault()?.Split(":");
    var clrSpaceColumn = space?.FirstOrDefault();
    var spacePartitioning = space?.Skip(1).FirstOrDefault();

    var timeColumn = annotation.Mapping.ColumnMappings
      .FirstOrDefault(column => column.Property.Name == clrTimeColumn)?
      .Column.Name;
    if (timeColumn is null)
    {
      return annotations;
    }

    var spaceColumn = annotation.Mapping.ColumnMappings
      .FirstOrDefault(column => column.Property.Name == clrSpaceColumn)?
      .Column.Name;

    if (spaceColumn is not null && spacePartitioning is not null)
    {
      annotations = annotations.Append(
        new Annotation(
          "TimescaleHypertable",
          $"{timeColumn},{spaceColumn}:{spacePartitioning}"
        )
      );
    }
    else
    {
      annotations = annotations.Append(
        new Annotation(
          "TimescaleHypertable",
          timeColumn
        )
      );
    }

    return annotations;
  }
}
