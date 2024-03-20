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

[AttributeUsage(AttributeTargets.Property)]
public class TimescaleHypertableAttribute : Attribute
{
}

public static class TimescaleHypertableAttributeExtensions
{
  public static ModelBuilder ApplyTimescaleHypertables(this ModelBuilder builder) =>
    builder.Model
      .GetEntityTypes()
      .SelectMany(entity => entity
        .GetProperties()
        .Select(property => new
        {
          Entity = entity,
          Property = property
        })
      )
      .Where(
        x =>
          x.Property.PropertyInfo?.GetCustomAttribute<TimescaleHypertableAttribute>() is { }
      )
      .Aggregate(
        builder,
        (builder, x) =>
        {
          builder
            .Entity(x.Entity.ClrType)
            .Property(x.Property.Name)
            .HasAnnotation("TimescaleHypertable", true);

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
    IColumn column,
    bool designTime
  )
  {
    if (!designTime)
    {
      return Enumerable.Empty<IAnnotation>();
    }

#pragma warning disable EF1001
    var annotations = base.For(column, designTime);
#pragma warning restore EF1001
    if (column.FindAnnotation("TimescaleHypertable")?.Value is { } value)
    {
      annotations = annotations.Append(
        new Annotation(
          "TimescaleHypertable",
          value
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

    if (operation.Columns
      .Find(column => column
        .FindAnnotation("TimescaleHypertable")?.Value is true) is { } column)
    {
      builder.Append(
        $"SELECT create_hypertable('\"{operation.Name}\"', '{column.Name}')"
      );

      if (terminate)
      {
        builder.AppendLine(";");
        EndStatement(builder);
      }
    }
  }

  protected override void Generate(
    AddColumnOperation operation,
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

    if (operation.FindAnnotation("TimescaleHypertable")?.Value is true)
    {
      builder.Append(
        $"SELECT create_hypertable('\"{operation.Table}\"', '{operation.Name}')"
      );

      if (terminate)
      {
        builder.AppendLine(";");
        EndStatement(builder);
      }
    }
  }
}

