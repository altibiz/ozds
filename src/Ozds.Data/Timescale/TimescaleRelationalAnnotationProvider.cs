using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;

namespace Ozds.Data.Timescale;

#pragma warning disable EF1001
public class TimescaleRelationalAnnotationProvider : NpgsqlAnnotationProvider
#pragma warning restore EF1001
{
  public TimescaleRelationalAnnotationProvider(
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

    var mappingTypeAttributes = table.EntityTypeMappings
      .Select(mapping => new
      {
        Mapping = mapping,
        // NOTE: this is the exact way that annotations get added in
        // NOTE: do not change this
        Annotation = mapping.TypeBase
          .GetAnnotations()
          .FirstOrDefault(annotation => annotation.Name == "TimescaleHypertable")
      });

    if (mappingTypeAttributes.FirstOrDefault(x => x is { Annotation.Value: string }) is { Annotation.Value: string } x &&
      x.Mapping.ColumnMappings.FirstOrDefault(column => column.Property.Name == x.Annotation.Value as string)?.Column.Name is { } timeColumn)
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
