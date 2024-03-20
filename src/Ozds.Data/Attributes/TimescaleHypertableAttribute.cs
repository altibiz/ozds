using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations;

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

public class TimescaleMigrationSqlGenerator : NpgsqlMigrationsSqlGenerator
{
  public TimescaleMigrationSqlGenerator(
    MigrationsSqlGeneratorDependencies dependencies,
#pragma warning disable EF1001
    INpgsqlSingletonOptions options
#pragma warning restore EF1001
  ) : base(dependencies, options)
  {
  }

  protected override void Generate(
    CreateTableOperation operation,
    IModel? model,
    MigrationCommandListBuilder builder,
    bool terminate = true
  )
  {
    base.Generate(
      operation,
      model,
      builder,
      terminate
    );

    if (operation.FindAnnotation("TimescaleHypertable")?.Value is string column)
    {
      builder.Append(
        $"SELECT create_hypertable('\"{operation.Name}\"', '{column}')"
      );

      if (terminate)
      {
        builder.AppendLine(";");
        EndStatement(builder);
      }
    }
  }
}

